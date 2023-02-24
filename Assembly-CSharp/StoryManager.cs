using System;
using System.Collections.Generic;
using Database;
using Klei.CustomSettings;
using KSerialization;
using UnityEngine;

// Token: 0x02000996 RID: 2454
[SerializationConfig(MemberSerialization.OptIn)]
public class StoryManager : KMonoBehaviour
{
	// Token: 0x1700056B RID: 1387
	// (get) Token: 0x0600489C RID: 18588 RVA: 0x00196F00 File Offset: 0x00195100
	// (set) Token: 0x0600489D RID: 18589 RVA: 0x00196F07 File Offset: 0x00195107
	public static StoryManager Instance { get; private set; }

	// Token: 0x0600489E RID: 18590 RVA: 0x00196F0F File Offset: 0x0019510F
	public static IReadOnlyList<StoryManager.StoryTelemetry> GetTelemetry()
	{
		return StoryManager.storyTelemetry;
	}

	// Token: 0x0600489F RID: 18591 RVA: 0x00196F18 File Offset: 0x00195118
	protected override void OnPrefabInit()
	{
		StoryManager.Instance = this;
		GameClock.Instance.Subscribe(631075836, new Action<object>(this.OnNewDayStarted));
		Game instance = Game.Instance;
		instance.OnLoad = (Action<Game.GameSaveData>)Delegate.Combine(instance.OnLoad, new Action<Game.GameSaveData>(this.OnGameLoaded));
	}

	// Token: 0x060048A0 RID: 18592 RVA: 0x00196F70 File Offset: 0x00195170
	protected override void OnCleanUp()
	{
		GameClock.Instance.Unsubscribe(631075836, new Action<object>(this.OnNewDayStarted));
		Game instance = Game.Instance;
		instance.OnLoad = (Action<Game.GameSaveData>)Delegate.Remove(instance.OnLoad, new Action<Game.GameSaveData>(this.OnGameLoaded));
	}

	// Token: 0x060048A1 RID: 18593 RVA: 0x00196FC0 File Offset: 0x001951C0
	public void InitialSaveSetup()
	{
		this.highestStoryCoordinateWhenGenerated = Db.Get().Stories.GetHighestCoordinateOffset();
		foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
		{
			foreach (string text in worldContainer.StoryTraitIds)
			{
				Story storyFromStoryTrait = Db.Get().Stories.GetStoryFromStoryTrait(text);
				this.CreateStory(storyFromStoryTrait, worldContainer.id);
			}
		}
		this.LogInitialSaveSetup();
	}

	// Token: 0x060048A2 RID: 18594 RVA: 0x00197084 File Offset: 0x00195284
	public StoryInstance CreateStory(string id, int worldId)
	{
		Story story = Db.Get().Stories.Get(id);
		return this.CreateStory(story, worldId);
	}

	// Token: 0x060048A3 RID: 18595 RVA: 0x001970AC File Offset: 0x001952AC
	public StoryInstance CreateStory(Story story, int worldId)
	{
		StoryInstance storyInstance = new StoryInstance(story, worldId);
		this._stories.Add(story.HashId, storyInstance);
		StoryManager.InitTelemetry(storyInstance);
		if (story.autoStart)
		{
			this.BeginStoryEvent(story);
		}
		return storyInstance;
	}

	// Token: 0x060048A4 RID: 18596 RVA: 0x001970EC File Offset: 0x001952EC
	public StoryInstance GetStoryInstance(int hash)
	{
		StoryInstance storyInstance;
		this._stories.TryGetValue(hash, out storyInstance);
		return storyInstance;
	}

	// Token: 0x060048A5 RID: 18597 RVA: 0x00197109 File Offset: 0x00195309
	public Dictionary<int, StoryInstance> GetStoryInstances()
	{
		return this._stories;
	}

	// Token: 0x060048A6 RID: 18598 RVA: 0x00197111 File Offset: 0x00195311
	public int GetHighestCoordinate()
	{
		return this.highestStoryCoordinateWhenGenerated;
	}

	// Token: 0x060048A7 RID: 18599 RVA: 0x00197119 File Offset: 0x00195319
	private string GetCompleteUnlockId(string id)
	{
		return id + "_STORY_COMPLETE";
	}

	// Token: 0x060048A8 RID: 18600 RVA: 0x00197126 File Offset: 0x00195326
	public void ForceCreateStory(Story story, int worldId)
	{
		if (this.GetStoryInstance(story.HashId) == null)
		{
			this.CreateStory(story, worldId);
		}
	}

	// Token: 0x060048A9 RID: 18601 RVA: 0x00197140 File Offset: 0x00195340
	public void DiscoverStoryEvent(Story story)
	{
		StoryInstance storyInstance = this.GetStoryInstance(story.HashId);
		if (storyInstance == null || this.CheckState(StoryInstance.State.DISCOVERED, story))
		{
			return;
		}
		storyInstance.CurrentState = StoryInstance.State.DISCOVERED;
	}

	// Token: 0x060048AA RID: 18602 RVA: 0x00197170 File Offset: 0x00195370
	public void BeginStoryEvent(Story story)
	{
		StoryInstance storyInstance = this.GetStoryInstance(story.HashId);
		if (storyInstance == null || this.CheckState(StoryInstance.State.IN_PROGRESS, story))
		{
			return;
		}
		storyInstance.CurrentState = StoryInstance.State.IN_PROGRESS;
	}

	// Token: 0x060048AB RID: 18603 RVA: 0x0019719F File Offset: 0x0019539F
	public void CompleteStoryEvent(Story story, MonoBehaviour keepsakeSpawnTarget, FocusTargetSequence.Data sequenceData)
	{
		if (this.GetStoryInstance(story.HashId) == null || this.CheckState(StoryInstance.State.COMPLETE, story))
		{
			return;
		}
		FocusTargetSequence.Start(keepsakeSpawnTarget, sequenceData);
	}

	// Token: 0x060048AC RID: 18604 RVA: 0x001971C4 File Offset: 0x001953C4
	public void CompleteStoryEvent(Story story, Vector3 keepsakeSpawnPosition)
	{
		StoryInstance storyInstance = this.GetStoryInstance(story.HashId);
		if (storyInstance == null)
		{
			return;
		}
		GameObject prefab = Assets.GetPrefab(storyInstance.GetStory().keepsakePrefabId);
		if (prefab != null)
		{
			keepsakeSpawnPosition.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
			GameObject gameObject = Util.KInstantiate(prefab, keepsakeSpawnPosition);
			gameObject.SetActive(true);
			new UpgradeFX.Instance(gameObject.GetComponent<KMonoBehaviour>(), new Vector3(0f, -0.5f, -0.1f)).StartSM();
		}
		storyInstance.CurrentState = StoryInstance.State.COMPLETE;
		Game.Instance.unlocks.Unlock(this.GetCompleteUnlockId(story.Id), true);
	}

	// Token: 0x060048AD RID: 18605 RVA: 0x00197264 File Offset: 0x00195464
	public bool CheckState(StoryInstance.State state, Story story)
	{
		StoryInstance storyInstance = this.GetStoryInstance(story.HashId);
		return storyInstance != null && storyInstance.CurrentState >= state;
	}

	// Token: 0x060048AE RID: 18606 RVA: 0x0019728F File Offset: 0x0019548F
	public bool IsStoryComplete(Story story)
	{
		return this.CheckState(StoryInstance.State.COMPLETE, story);
	}

	// Token: 0x060048AF RID: 18607 RVA: 0x00197299 File Offset: 0x00195499
	public bool IsStoryCompleteGlobal(Story story)
	{
		return Game.Instance.unlocks.IsUnlocked(this.GetCompleteUnlockId(story.Id));
	}

	// Token: 0x060048B0 RID: 18608 RVA: 0x001972B8 File Offset: 0x001954B8
	public StoryInstance DisplayPopup(Story story, StoryManager.PopupInfo info, System.Action popupCB = null, Notification.ClickCallback notificationCB = null)
	{
		StoryInstance storyInstance = this.GetStoryInstance(story.HashId);
		if (storyInstance == null || storyInstance.HasDisplayedPopup(info.PopupType))
		{
			return null;
		}
		EventInfoData eventInfoData = EventInfoDataHelper.GenerateStoryTraitData(info.Title, info.Description, info.CloseButtonText, info.TextureName, info.PopupType, info.CloseButtonToolTip, info.Minions, popupCB);
		Notification notification = null;
		if (!info.DisplayImmediate)
		{
			notification = EventInfoScreen.CreateNotification(eventInfoData, notificationCB);
		}
		storyInstance.SetPopupData(info, eventInfoData, notification);
		return storyInstance;
	}

	// Token: 0x060048B1 RID: 18609 RVA: 0x00197334 File Offset: 0x00195534
	public bool HasDisplayedPopup(Story story, EventInfoDataHelper.PopupType type)
	{
		StoryInstance storyInstance = this.GetStoryInstance(story.HashId);
		return storyInstance != null && storyInstance.HasDisplayedPopup(type);
	}

	// Token: 0x060048B2 RID: 18610 RVA: 0x0019735C File Offset: 0x0019555C
	private void LogInitialSaveSetup()
	{
		int num = 0;
		StoryManager.StoryCreationTelemetry[] array = new StoryManager.StoryCreationTelemetry[CustomGameSettings.Instance.CurrentStoryLevelsBySetting.Count];
		foreach (KeyValuePair<string, string> keyValuePair in CustomGameSettings.Instance.CurrentStoryLevelsBySetting)
		{
			array[num] = new StoryManager.StoryCreationTelemetry
			{
				StoryId = keyValuePair.Key,
				Enabled = CustomGameSettings.Instance.IsStoryActive(keyValuePair.Key, keyValuePair.Value)
			};
			num++;
		}
		OniMetrics.LogEvent(OniMetrics.Event.NewSave, "StoryTraitsCreation", array);
	}

	// Token: 0x060048B3 RID: 18611 RVA: 0x00197400 File Offset: 0x00195600
	private void OnNewDayStarted(object _)
	{
		OniMetrics.LogEvent(OniMetrics.Event.EndOfCycle, "SavedHighestStoryCoordinate", this.highestStoryCoordinateWhenGenerated);
		OniMetrics.LogEvent(OniMetrics.Event.EndOfCycle, "StoryTraits", StoryManager.storyTelemetry);
	}

	// Token: 0x060048B4 RID: 18612 RVA: 0x00197428 File Offset: 0x00195628
	private static void InitTelemetry(StoryInstance story)
	{
		WorldContainer world = ClusterManager.Instance.GetWorld(story.worldId);
		if (world == null)
		{
			return;
		}
		story.Telemetry.StoryId = story.storyId;
		story.Telemetry.WorldId = world.worldName;
		StoryManager.storyTelemetry.Add(story.Telemetry);
	}

	// Token: 0x060048B5 RID: 18613 RVA: 0x00197484 File Offset: 0x00195684
	private void OnGameLoaded(object _)
	{
		StoryManager.storyTelemetry.Clear();
		foreach (KeyValuePair<int, StoryInstance> keyValuePair in this._stories)
		{
			StoryManager.InitTelemetry(keyValuePair.Value);
		}
		CustomGameSettings.Instance.DisableAllStories();
		foreach (KeyValuePair<int, StoryInstance> keyValuePair2 in this._stories)
		{
			SettingConfig settingConfig;
			if (keyValuePair2.Value.Telemetry.Retrofitted < 0f && CustomGameSettings.Instance.StorySettings.TryGetValue(keyValuePair2.Value.storyId, out settingConfig))
			{
				CustomGameSettings.Instance.SetStorySetting(settingConfig, true);
			}
		}
	}

	// Token: 0x060048B6 RID: 18614 RVA: 0x00197570 File Offset: 0x00195770
	public static void DestroyInstance()
	{
		StoryManager.storyTelemetry.Clear();
		StoryManager.Instance = null;
	}

	// Token: 0x04002FCA RID: 12234
	public const int BEFORE_STORIES = -2;

	// Token: 0x04002FCC RID: 12236
	private static List<StoryManager.StoryTelemetry> storyTelemetry = new List<StoryManager.StoryTelemetry>();

	// Token: 0x04002FCD RID: 12237
	[Serialize]
	private Dictionary<int, StoryInstance> _stories = new Dictionary<int, StoryInstance>();

	// Token: 0x04002FCE RID: 12238
	[Serialize]
	private int highestStoryCoordinateWhenGenerated = -2;

	// Token: 0x04002FCF RID: 12239
	private const string STORY_TRAIT_KEY = "StoryTraits";

	// Token: 0x04002FD0 RID: 12240
	private const string STORY_CREATION_KEY = "StoryTraitsCreation";

	// Token: 0x04002FD1 RID: 12241
	private const string STORY_COORDINATE_KEY = "SavedHighestStoryCoordinate";

	// Token: 0x02001790 RID: 6032
	public struct PopupInfo
	{
		// Token: 0x04006D6B RID: 28011
		public string Title;

		// Token: 0x04006D6C RID: 28012
		public string Description;

		// Token: 0x04006D6D RID: 28013
		public string CloseButtonText;

		// Token: 0x04006D6E RID: 28014
		public string CloseButtonToolTip;

		// Token: 0x04006D6F RID: 28015
		public string TextureName;

		// Token: 0x04006D70 RID: 28016
		public GameObject[] Minions;

		// Token: 0x04006D71 RID: 28017
		public bool DisplayImmediate;

		// Token: 0x04006D72 RID: 28018
		public EventInfoDataHelper.PopupType PopupType;
	}

	// Token: 0x02001791 RID: 6033
	[SerializationConfig(MemberSerialization.OptIn)]
	public class StoryTelemetry : ISaveLoadable
	{
		// Token: 0x06008B43 RID: 35651 RVA: 0x002FF23C File Offset: 0x002FD43C
		public void LogStateChange(StoryInstance.State state, float time)
		{
			switch (state)
			{
			case StoryInstance.State.RETROFITTED:
				this.Retrofitted = ((this.Retrofitted >= 0f) ? this.Retrofitted : time);
				return;
			case StoryInstance.State.NOT_STARTED:
				break;
			case StoryInstance.State.DISCOVERED:
				this.Discovered = ((this.Discovered >= 0f) ? this.Discovered : time);
				return;
			case StoryInstance.State.IN_PROGRESS:
				this.InProgress = ((this.InProgress >= 0f) ? this.InProgress : time);
				return;
			case StoryInstance.State.COMPLETE:
				this.Completed = ((this.Completed >= 0f) ? this.Completed : time);
				break;
			default:
				return;
			}
		}

		// Token: 0x04006D73 RID: 28019
		public string StoryId;

		// Token: 0x04006D74 RID: 28020
		public string WorldId;

		// Token: 0x04006D75 RID: 28021
		[Serialize]
		public float Retrofitted = -1f;

		// Token: 0x04006D76 RID: 28022
		[Serialize]
		public float Discovered = -1f;

		// Token: 0x04006D77 RID: 28023
		[Serialize]
		public float InProgress = -1f;

		// Token: 0x04006D78 RID: 28024
		[Serialize]
		public float Completed = -1f;
	}

	// Token: 0x02001792 RID: 6034
	public class StoryCreationTelemetry
	{
		// Token: 0x04006D79 RID: 28025
		public string StoryId;

		// Token: 0x04006D7A RID: 28026
		public bool Enabled;
	}
}
