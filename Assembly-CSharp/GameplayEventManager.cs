using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;

// Token: 0x02000789 RID: 1929
public class GameplayEventManager : KMonoBehaviour
{
	// Token: 0x060035EE RID: 13806 RVA: 0x0012B597 File Offset: 0x00129797
	public static void DestroyInstance()
	{
		GameplayEventManager.Instance = null;
	}

	// Token: 0x060035EF RID: 13807 RVA: 0x0012B59F File Offset: 0x0012979F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		GameplayEventManager.Instance = this;
		this.notifier = base.GetComponent<Notifier>();
	}

	// Token: 0x060035F0 RID: 13808 RVA: 0x0012B5B9 File Offset: 0x001297B9
	protected override void OnSpawn()
	{
		base.OnSpawn();
		GameScheduler.Instance.ScheduleNextFrame("GameplayEventManager", delegate(object obj)
		{
			this.RestoreEvents();
		}, null, null);
	}

	// Token: 0x060035F1 RID: 13809 RVA: 0x0012B5DF File Offset: 0x001297DF
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		GameplayEventManager.Instance = null;
	}

	// Token: 0x060035F2 RID: 13810 RVA: 0x0012B5F0 File Offset: 0x001297F0
	private void RestoreEvents()
	{
		this.activeEvents.RemoveAll((GameplayEventInstance x) => Db.Get().GameplayEvents.TryGet(x.eventID) == null);
		foreach (GameplayEventInstance gameplayEventInstance in this.activeEvents)
		{
			this.StartEventInstance(gameplayEventInstance);
		}
	}

	// Token: 0x060035F3 RID: 13811 RVA: 0x0012B670 File Offset: 0x00129870
	public bool IsGameplayEventActive(GameplayEvent eventType)
	{
		return this.activeEvents.Find((GameplayEventInstance e) => e.eventID == eventType.IdHash) != null;
	}

	// Token: 0x060035F4 RID: 13812 RVA: 0x0012B6A4 File Offset: 0x001298A4
	public bool IsGameplayEventRunningWithTag(Tag tag)
	{
		using (List<GameplayEventInstance>.Enumerator enumerator = this.activeEvents.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.tags.Contains(tag))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060035F5 RID: 13813 RVA: 0x0012B704 File Offset: 0x00129904
	public void GetActiveEventsOfType<T>(int worldID, ref List<GameplayEventInstance> results) where T : GameplayEvent
	{
		foreach (GameplayEventInstance gameplayEventInstance in this.activeEvents)
		{
			if (gameplayEventInstance.worldId == worldID && gameplayEventInstance.gameplayEvent is T)
			{
				results.Add(gameplayEventInstance);
			}
		}
	}

	// Token: 0x060035F6 RID: 13814 RVA: 0x0012B778 File Offset: 0x00129978
	private GameplayEventInstance CreateGameplayEvent(GameplayEvent gameplayEvent, int worldId)
	{
		return gameplayEvent.CreateInstance(worldId);
	}

	// Token: 0x060035F7 RID: 13815 RVA: 0x0012B784 File Offset: 0x00129984
	public GameplayEventInstance GetGameplayEventInstance(HashedString eventID, int worldId = -1)
	{
		return this.activeEvents.Find((GameplayEventInstance e) => e.eventID == eventID && (worldId == -1 || e.worldId == worldId));
	}

	// Token: 0x060035F8 RID: 13816 RVA: 0x0012B7BC File Offset: 0x001299BC
	public GameplayEventInstance CreateOrGetEventInstance(GameplayEvent eventType, int worldId = -1)
	{
		GameplayEventInstance gameplayEventInstance = this.GetGameplayEventInstance(eventType.Id, worldId);
		if (gameplayEventInstance == null)
		{
			gameplayEventInstance = this.StartNewEvent(eventType, worldId);
		}
		return gameplayEventInstance;
	}

	// Token: 0x060035F9 RID: 13817 RVA: 0x0012B7EC File Offset: 0x001299EC
	public GameplayEventInstance StartNewEvent(GameplayEvent eventType, int worldId = -1)
	{
		GameplayEventInstance gameplayEventInstance = this.CreateGameplayEvent(eventType, worldId);
		this.StartEventInstance(gameplayEventInstance);
		this.activeEvents.Add(gameplayEventInstance);
		int num;
		this.pastEvents.TryGetValue(gameplayEventInstance.eventID, out num);
		this.pastEvents[gameplayEventInstance.eventID] = num + 1;
		return gameplayEventInstance;
	}

	// Token: 0x060035FA RID: 13818 RVA: 0x0012B840 File Offset: 0x00129A40
	private void StartEventInstance(GameplayEventInstance gameplayEventInstance)
	{
		StateMachine.Instance instance = gameplayEventInstance.PrepareEvent(this);
		instance.OnStop = (Action<string, StateMachine.Status>)Delegate.Combine(instance.OnStop, new Action<string, StateMachine.Status>(delegate(string reason, StateMachine.Status status)
		{
			this.activeEvents.Remove(gameplayEventInstance);
		}));
		gameplayEventInstance.StartEvent();
	}

	// Token: 0x060035FB RID: 13819 RVA: 0x0012B89C File Offset: 0x00129A9C
	public int NumberOfPastEvents(HashedString eventID)
	{
		int num;
		this.pastEvents.TryGetValue(eventID, out num);
		return num;
	}

	// Token: 0x060035FC RID: 13820 RVA: 0x0012B8BC File Offset: 0x00129ABC
	public static Notification CreateStandardCancelledNotification(EventInfoData eventInfoData)
	{
		if (eventInfoData == null)
		{
			DebugUtil.LogWarningArgs(new object[] { "eventPopup is null in CreateStandardCancelledNotification" });
			return null;
		}
		eventInfoData.FinalizeText();
		return new Notification(string.Format(GAMEPLAY_EVENTS.CANCELED, eventInfoData.title), NotificationType.Event, (List<Notification> list, object data) => string.Format(GAMEPLAY_EVENTS.CANCELED_TOOLTIP, eventInfoData.title), null, true, 0f, null, null, null, true, false, false);
	}

	// Token: 0x04002407 RID: 9223
	public static GameplayEventManager Instance;

	// Token: 0x04002408 RID: 9224
	public Notifier notifier;

	// Token: 0x04002409 RID: 9225
	[Serialize]
	private List<GameplayEventInstance> activeEvents = new List<GameplayEventInstance>();

	// Token: 0x0400240A RID: 9226
	[Serialize]
	private Dictionary<HashedString, int> pastEvents = new Dictionary<HashedString, int>();
}
