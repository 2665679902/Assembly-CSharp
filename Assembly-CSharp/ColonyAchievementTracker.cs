using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Database;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000694 RID: 1684
[AddComponentMenu("KMonoBehaviour/scripts/ColonyAchievementTracker")]
public class ColonyAchievementTracker : KMonoBehaviour, ISaveLoadableDetails, IRenderEveryTick
{
	// Token: 0x17000332 RID: 818
	// (get) Token: 0x06002DBD RID: 11709 RVA: 0x000F05FD File Offset: 0x000EE7FD
	public List<string> achievementsToDisplay
	{
		get
		{
			return this.completedAchievementsToDisplay;
		}
	}

	// Token: 0x06002DBE RID: 11710 RVA: 0x000F0605 File Offset: 0x000EE805
	public void ClearDisplayAchievements()
	{
		this.achievementsToDisplay.Clear();
	}

	// Token: 0x06002DBF RID: 11711 RVA: 0x000F0614 File Offset: 0x000EE814
	protected override void OnSpawn()
	{
		base.OnSpawn();
		foreach (ColonyAchievement colonyAchievement in Db.Get().ColonyAchievements.resources)
		{
			if (!this.achievements.ContainsKey(colonyAchievement.Id))
			{
				ColonyAchievementStatus colonyAchievementStatus = new ColonyAchievementStatus(colonyAchievement.Id);
				this.achievements.Add(colonyAchievement.Id, colonyAchievementStatus);
			}
		}
		this.forceCheckAchievementHandle = Game.Instance.Subscribe(395452326, new Action<object>(this.CheckAchievements));
		base.Subscribe<ColonyAchievementTracker>(631075836, ColonyAchievementTracker.OnNewDayDelegate);
		this.UpgradeTamedCritterAchievements();
	}

	// Token: 0x06002DC0 RID: 11712 RVA: 0x000F06D8 File Offset: 0x000EE8D8
	private void UpgradeTamedCritterAchievements()
	{
		foreach (ColonyAchievementRequirement colonyAchievementRequirement in Db.Get().ColonyAchievements.TameAllBasicCritters.requirementChecklist)
		{
			CritterTypesWithTraits critterTypesWithTraits = colonyAchievementRequirement as CritterTypesWithTraits;
			if (critterTypesWithTraits != null)
			{
				critterTypesWithTraits.UpdateSavedState();
			}
		}
		foreach (ColonyAchievementRequirement colonyAchievementRequirement2 in Db.Get().ColonyAchievements.TameAGassyMoo.requirementChecklist)
		{
			CritterTypesWithTraits critterTypesWithTraits2 = colonyAchievementRequirement2 as CritterTypesWithTraits;
			if (critterTypesWithTraits2 != null)
			{
				critterTypesWithTraits2.UpdateSavedState();
			}
		}
	}

	// Token: 0x06002DC1 RID: 11713 RVA: 0x000F0798 File Offset: 0x000EE998
	public void RenderEveryTick(float dt)
	{
		if (this.updatingAchievement >= this.achievements.Count)
		{
			this.updatingAchievement = 0;
		}
		KeyValuePair<string, ColonyAchievementStatus> keyValuePair = this.achievements.ElementAt(this.updatingAchievement);
		this.updatingAchievement++;
		if (!keyValuePair.Value.success && !keyValuePair.Value.failed)
		{
			keyValuePair.Value.UpdateAchievement();
			if (keyValuePair.Value.success && !keyValuePair.Value.failed)
			{
				ColonyAchievementTracker.UnlockPlatformAchievement(keyValuePair.Key);
				this.completedAchievementsToDisplay.Add(keyValuePair.Key);
				this.TriggerNewAchievementCompleted(keyValuePair.Key, null);
				RetireColonyUtility.SaveColonySummaryData();
			}
		}
	}

	// Token: 0x06002DC2 RID: 11714 RVA: 0x000F0858 File Offset: 0x000EEA58
	private void CheckAchievements(object data = null)
	{
		foreach (KeyValuePair<string, ColonyAchievementStatus> keyValuePair in this.achievements)
		{
			if (!keyValuePair.Value.success && !keyValuePair.Value.failed)
			{
				keyValuePair.Value.UpdateAchievement();
				if (keyValuePair.Value.success && !keyValuePair.Value.failed)
				{
					ColonyAchievementTracker.UnlockPlatformAchievement(keyValuePair.Key);
					this.completedAchievementsToDisplay.Add(keyValuePair.Key);
					this.TriggerNewAchievementCompleted(keyValuePair.Key, null);
				}
			}
		}
		RetireColonyUtility.SaveColonySummaryData();
	}

	// Token: 0x06002DC3 RID: 11715 RVA: 0x000F0920 File Offset: 0x000EEB20
	private static void UnlockPlatformAchievement(string achievement_id)
	{
		if (DebugHandler.InstantBuildMode)
		{
			global::Debug.LogWarningFormat("UnlockPlatformAchievement {0} skipping: instant build mode", new object[] { achievement_id });
			return;
		}
		if (SaveGame.Instance.sandboxEnabled)
		{
			global::Debug.LogWarningFormat("UnlockPlatformAchievement {0} skipping: sandbox mode", new object[] { achievement_id });
			return;
		}
		if (Game.Instance.debugWasUsed)
		{
			global::Debug.LogWarningFormat("UnlockPlatformAchievement {0} skipping: debug was used.", new object[] { achievement_id });
			return;
		}
		ColonyAchievement colonyAchievement = Db.Get().ColonyAchievements.Get(achievement_id);
		if (colonyAchievement != null && !string.IsNullOrEmpty(colonyAchievement.platformAchievementId))
		{
			if (SteamAchievementService.Instance)
			{
				SteamAchievementService.Instance.Unlock(colonyAchievement.platformAchievementId);
				return;
			}
			global::Debug.LogWarningFormat("Steam achievement [{0}] was achieved, but achievement service was null", new object[] { colonyAchievement.platformAchievementId });
		}
	}

	// Token: 0x06002DC4 RID: 11716 RVA: 0x000F09E2 File Offset: 0x000EEBE2
	public void DebugTriggerAchievement(string id)
	{
		this.achievements[id].failed = false;
		this.achievements[id].success = true;
	}

	// Token: 0x06002DC5 RID: 11717 RVA: 0x000F0A08 File Offset: 0x000EEC08
	private void BeginVictorySequence(string achievementID)
	{
		RootMenu.Instance.canTogglePauseScreen = false;
		CameraController.Instance.DisableUserCameraControl = true;
		if (!SpeedControlScreen.Instance.IsPaused)
		{
			SpeedControlScreen.Instance.Pause(false, false);
		}
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().VictoryMessageSnapshot);
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().MuteDynamicMusicSnapshot);
		this.ToggleVictoryUI(true);
		StoryMessageScreen component = GameScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.StoryMessageScreen.gameObject, null, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay).GetComponent<StoryMessageScreen>();
		component.restoreInterfaceOnClose = false;
		component.title = COLONY_ACHIEVEMENTS.PRE_VICTORY_MESSAGE_HEADER;
		component.body = string.Format(COLONY_ACHIEVEMENTS.PRE_VICTORY_MESSAGE_BODY, "<b>" + Db.Get().ColonyAchievements.Get(achievementID).Name + "</b>\n" + Db.Get().ColonyAchievements.Get(achievementID).description);
		component.Show(true);
		CameraController.Instance.SetWorldInteractive(false);
		component.OnClose = (System.Action)Delegate.Combine(component.OnClose, new System.Action(delegate
		{
			SpeedControlScreen.Instance.SetSpeed(1);
			if (!SpeedControlScreen.Instance.IsPaused)
			{
				SpeedControlScreen.Instance.Pause(false, false);
			}
			CameraController.Instance.SetWorldInteractive(true);
			Db.Get().ColonyAchievements.Get(achievementID).victorySequence(this);
		}));
	}

	// Token: 0x06002DC6 RID: 11718 RVA: 0x000F0B4C File Offset: 0x000EED4C
	public bool IsAchievementUnlocked(ColonyAchievement achievement)
	{
		foreach (KeyValuePair<string, ColonyAchievementStatus> keyValuePair in this.achievements)
		{
			if (keyValuePair.Key == achievement.Id)
			{
				if (keyValuePair.Value.success)
				{
					return true;
				}
				keyValuePair.Value.UpdateAchievement();
				return keyValuePair.Value.success;
			}
		}
		return false;
	}

	// Token: 0x06002DC7 RID: 11719 RVA: 0x000F0BDC File Offset: 0x000EEDDC
	protected override void OnCleanUp()
	{
		this.victorySchedulerHandle.ClearScheduler();
		Game.Instance.Unsubscribe(this.forceCheckAchievementHandle);
		this.checkAchievementsHandle.ClearScheduler();
		base.OnCleanUp();
	}

	// Token: 0x06002DC8 RID: 11720 RVA: 0x000F0C0C File Offset: 0x000EEE0C
	private void TriggerNewAchievementCompleted(string achievement, GameObject cameraTarget = null)
	{
		this.unlockedAchievementMetric[ColonyAchievementTracker.UnlockedAchievementKey] = achievement;
		ThreadedHttps<KleiMetrics>.Instance.SendEvent(this.unlockedAchievementMetric, "TriggerNewAchievementCompleted");
		bool flag = false;
		if (Db.Get().ColonyAchievements.Get(achievement).isVictoryCondition)
		{
			flag = true;
			this.BeginVictorySequence(achievement);
		}
		if (!flag)
		{
			AchievementEarnedMessage achievementEarnedMessage = new AchievementEarnedMessage();
			Messenger.Instance.QueueMessage(achievementEarnedMessage);
		}
	}

	// Token: 0x06002DC9 RID: 11721 RVA: 0x000F0C78 File Offset: 0x000EEE78
	private void ToggleVictoryUI(bool victoryUIActive)
	{
		List<KScreen> list = new List<KScreen>();
		list.Add(NotificationScreen.Instance);
		list.Add(OverlayMenu.Instance);
		if (PlanScreen.Instance != null)
		{
			list.Add(PlanScreen.Instance);
		}
		if (BuildMenu.Instance != null)
		{
			list.Add(BuildMenu.Instance);
		}
		list.Add(ManagementMenu.Instance);
		list.Add(ToolMenu.Instance);
		list.Add(ToolMenu.Instance.PriorityScreen);
		list.Add(ResourceCategoryScreen.Instance);
		list.Add(TopLeftControlScreen.Instance);
		list.Add(global::DateTime.Instance);
		list.Add(BuildWatermark.Instance);
		list.Add(HoverTextScreen.Instance);
		list.Add(DetailsScreen.Instance);
		list.Add(DebugPaintElementScreen.Instance);
		list.Add(DebugBaseTemplateButton.Instance);
		list.Add(StarmapScreen.Instance);
		foreach (KScreen kscreen in list)
		{
			if (kscreen != null)
			{
				kscreen.Show(!victoryUIActive);
			}
		}
	}

	// Token: 0x06002DCA RID: 11722 RVA: 0x000F0DA8 File Offset: 0x000EEFA8
	public void Serialize(BinaryWriter writer)
	{
		writer.Write(this.achievements.Count);
		foreach (KeyValuePair<string, ColonyAchievementStatus> keyValuePair in this.achievements)
		{
			writer.WriteKleiString(keyValuePair.Key);
			keyValuePair.Value.Serialize(writer);
		}
	}

	// Token: 0x06002DCB RID: 11723 RVA: 0x000F0E20 File Offset: 0x000EF020
	public void Deserialize(IReader reader)
	{
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 10))
		{
			return;
		}
		int num = reader.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			string text = reader.ReadKleiString();
			ColonyAchievementStatus colonyAchievementStatus = ColonyAchievementStatus.Deserialize(reader, text);
			if (Db.Get().ColonyAchievements.Exists(text))
			{
				this.achievements.Add(text, colonyAchievementStatus);
			}
		}
	}

	// Token: 0x06002DCC RID: 11724 RVA: 0x000F0E88 File Offset: 0x000EF088
	public void LogFetchChore(GameObject fetcher, ChoreType choreType)
	{
		if (choreType == Db.Get().ChoreTypes.StorageFetch || choreType == Db.Get().ChoreTypes.BuildFetch || choreType == Db.Get().ChoreTypes.RepairFetch || choreType == Db.Get().ChoreTypes.FoodFetch || choreType == Db.Get().ChoreTypes.Transport)
		{
			return;
		}
		Dictionary<int, int> dictionary = null;
		if (fetcher.GetComponent<SolidTransferArm>() != null)
		{
			dictionary = this.fetchAutomatedChoreDeliveries;
		}
		else if (fetcher.GetComponent<MinionIdentity>() != null)
		{
			dictionary = this.fetchDupeChoreDeliveries;
		}
		if (dictionary != null)
		{
			int cycle = GameClock.Instance.GetCycle();
			if (!dictionary.ContainsKey(cycle))
			{
				dictionary.Add(cycle, 0);
			}
			Dictionary<int, int> dictionary2 = dictionary;
			int num = cycle;
			int num2 = dictionary2[num];
			dictionary2[num] = num2 + 1;
		}
	}

	// Token: 0x06002DCD RID: 11725 RVA: 0x000F0F51 File Offset: 0x000EF151
	public void LogCritterTamed(Tag prefabId)
	{
		this.tamedCritterTypes.Add(prefabId);
	}

	// Token: 0x06002DCE RID: 11726 RVA: 0x000F0F60 File Offset: 0x000EF160
	public void LogSuitChore(ChoreDriver driver)
	{
		if (driver == null || driver.GetComponent<MinionIdentity>() == null)
		{
			return;
		}
		bool flag = false;
		foreach (AssignableSlotInstance assignableSlotInstance in driver.GetComponent<MinionIdentity>().GetEquipment().Slots)
		{
			Equippable equippable = ((EquipmentSlotInstance)assignableSlotInstance).assignable as Equippable;
			if (equippable && equippable.GetComponent<KPrefabID>().IsAnyPrefabID(ColonyAchievementTracker.SuitTags))
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			int cycle = GameClock.Instance.GetCycle();
			int instanceID = driver.GetComponent<KPrefabID>().InstanceID;
			if (!this.dupesCompleteChoresInSuits.ContainsKey(cycle))
			{
				this.dupesCompleteChoresInSuits.Add(cycle, new List<int> { instanceID });
				return;
			}
			if (!this.dupesCompleteChoresInSuits[cycle].Contains(instanceID))
			{
				this.dupesCompleteChoresInSuits[cycle].Add(instanceID);
			}
		}
	}

	// Token: 0x06002DCF RID: 11727 RVA: 0x000F1068 File Offset: 0x000EF268
	public void LogAnalyzedSeed(Tag seed)
	{
		this.analyzedSeeds.Add(seed);
	}

	// Token: 0x06002DD0 RID: 11728 RVA: 0x000F1078 File Offset: 0x000EF278
	public void OnNewDay(object data)
	{
		foreach (MinionStorage minionStorage in Components.MinionStorages.Items)
		{
			if (minionStorage.GetComponent<CommandModule>() != null)
			{
				List<MinionStorage.Info> storedMinionInfo = minionStorage.GetStoredMinionInfo();
				if (storedMinionInfo.Count > 0)
				{
					int cycle = GameClock.Instance.GetCycle();
					if (!this.dupesCompleteChoresInSuits.ContainsKey(cycle))
					{
						this.dupesCompleteChoresInSuits.Add(cycle, new List<int>());
					}
					for (int i = 0; i < storedMinionInfo.Count; i++)
					{
						KPrefabID kprefabID = storedMinionInfo[i].serializedMinion.Get();
						if (kprefabID != null)
						{
							this.dupesCompleteChoresInSuits[cycle].Add(kprefabID.InstanceID);
						}
					}
				}
			}
		}
		if (DlcManager.IsExpansion1Active())
		{
			SurviveARocketWithMinimumMorale surviveARocketWithMinimumMorale = Db.Get().ColonyAchievements.SurviveInARocket.requirementChecklist[0] as SurviveARocketWithMinimumMorale;
			if (surviveARocketWithMinimumMorale != null)
			{
				float minimumMorale = surviveARocketWithMinimumMorale.minimumMorale;
				int numberOfCycles = surviveARocketWithMinimumMorale.numberOfCycles;
				foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
				{
					if (worldContainer.IsModuleInterior)
					{
						if (!this.cyclesRocketDupeMoraleAboveRequirement.ContainsKey(worldContainer.id))
						{
							this.cyclesRocketDupeMoraleAboveRequirement.Add(worldContainer.id, 0);
						}
						if (worldContainer.GetComponent<Clustercraft>().Status != Clustercraft.CraftStatus.Grounded)
						{
							List<MinionIdentity> worldItems = Components.MinionIdentities.GetWorldItems(worldContainer.id, false);
							bool flag = worldItems.Count > 0;
							foreach (MinionIdentity minionIdentity in worldItems)
							{
								if (Db.Get().Attributes.QualityOfLife.Lookup(minionIdentity).GetTotalValue() < minimumMorale)
								{
									flag = false;
									break;
								}
							}
							this.cyclesRocketDupeMoraleAboveRequirement[worldContainer.id] = (flag ? (this.cyclesRocketDupeMoraleAboveRequirement[worldContainer.id] + 1) : 0);
						}
						else if (this.cyclesRocketDupeMoraleAboveRequirement[worldContainer.id] < numberOfCycles)
						{
							this.cyclesRocketDupeMoraleAboveRequirement[worldContainer.id] = 0;
						}
					}
				}
			}
		}
	}

	// Token: 0x04001B0E RID: 6926
	public Dictionary<string, ColonyAchievementStatus> achievements = new Dictionary<string, ColonyAchievementStatus>();

	// Token: 0x04001B0F RID: 6927
	[Serialize]
	public Dictionary<int, int> fetchAutomatedChoreDeliveries = new Dictionary<int, int>();

	// Token: 0x04001B10 RID: 6928
	[Serialize]
	public Dictionary<int, int> fetchDupeChoreDeliveries = new Dictionary<int, int>();

	// Token: 0x04001B11 RID: 6929
	[Serialize]
	public Dictionary<int, List<int>> dupesCompleteChoresInSuits = new Dictionary<int, List<int>>();

	// Token: 0x04001B12 RID: 6930
	[Serialize]
	public HashSet<Tag> tamedCritterTypes = new HashSet<Tag>();

	// Token: 0x04001B13 RID: 6931
	[Serialize]
	public bool defrostedDuplicant;

	// Token: 0x04001B14 RID: 6932
	[Serialize]
	public HashSet<Tag> analyzedSeeds = new HashSet<Tag>();

	// Token: 0x04001B15 RID: 6933
	[Serialize]
	public float totalMaterialsHarvestFromPOI;

	// Token: 0x04001B16 RID: 6934
	[Serialize]
	public float radBoltTravelDistance;

	// Token: 0x04001B17 RID: 6935
	[Serialize]
	public bool harvestAHiveWithoutGettingStung;

	// Token: 0x04001B18 RID: 6936
	[Serialize]
	public Dictionary<int, int> cyclesRocketDupeMoraleAboveRequirement = new Dictionary<int, int>();

	// Token: 0x04001B19 RID: 6937
	private SchedulerHandle checkAchievementsHandle;

	// Token: 0x04001B1A RID: 6938
	private int forceCheckAchievementHandle = -1;

	// Token: 0x04001B1B RID: 6939
	[Serialize]
	private int updatingAchievement;

	// Token: 0x04001B1C RID: 6940
	[Serialize]
	private List<string> completedAchievementsToDisplay = new List<string>();

	// Token: 0x04001B1D RID: 6941
	private SchedulerHandle victorySchedulerHandle;

	// Token: 0x04001B1E RID: 6942
	public static readonly string UnlockedAchievementKey = "UnlockedAchievement";

	// Token: 0x04001B1F RID: 6943
	private Dictionary<string, object> unlockedAchievementMetric = new Dictionary<string, object> { 
	{
		ColonyAchievementTracker.UnlockedAchievementKey,
		null
	} };

	// Token: 0x04001B20 RID: 6944
	private static readonly Tag[] SuitTags = new Tag[]
	{
		GameTags.AtmoSuit,
		GameTags.JetSuit,
		GameTags.LeadSuit
	};

	// Token: 0x04001B21 RID: 6945
	private static readonly EventSystem.IntraObjectHandler<ColonyAchievementTracker> OnNewDayDelegate = new EventSystem.IntraObjectHandler<ColonyAchievementTracker>(delegate(ColonyAchievementTracker component, object data)
	{
		component.OnNewDay(data);
	});
}
