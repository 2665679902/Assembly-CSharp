using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020003B1 RID: 945
[SerializationConfig(MemberSerialization.OptIn)]
public class GameplayEventInstance : ISaveLoadable
{
	// Token: 0x1700007A RID: 122
	// (get) Token: 0x06001384 RID: 4996 RVA: 0x0006754E File Offset: 0x0006574E
	// (set) Token: 0x06001385 RID: 4997 RVA: 0x00067556 File Offset: 0x00065756
	public StateMachine.Instance smi { get; private set; }

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x06001386 RID: 4998 RVA: 0x0006755F File Offset: 0x0006575F
	// (set) Token: 0x06001387 RID: 4999 RVA: 0x00067567 File Offset: 0x00065767
	public bool seenNotification
	{
		get
		{
			return this._seenNotification;
		}
		set
		{
			this._seenNotification = value;
			this.monitorCallbackObjects.ForEach(delegate(GameObject x)
			{
				x.Trigger(-1122598290, this);
			});
		}
	}

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x06001388 RID: 5000 RVA: 0x00067587 File Offset: 0x00065787
	public GameplayEvent gameplayEvent
	{
		get
		{
			if (this._gameplayEvent == null)
			{
				this._gameplayEvent = Db.Get().GameplayEvents.TryGet(this.eventID);
			}
			return this._gameplayEvent;
		}
	}

	// Token: 0x06001389 RID: 5001 RVA: 0x000675B2 File Offset: 0x000657B2
	public GameplayEventInstance(GameplayEvent gameplayEvent, int worldId)
	{
		this.eventID = gameplayEvent.Id;
		this.tags = new List<Tag>();
		this.eventStartTime = GameUtil.GetCurrentTimeInCycles();
		this.worldId = worldId;
	}

	// Token: 0x0600138A RID: 5002 RVA: 0x000675E8 File Offset: 0x000657E8
	public StateMachine.Instance PrepareEvent(GameplayEventManager manager)
	{
		this.smi = this.gameplayEvent.GetSMI(manager, this);
		return this.smi;
	}

	// Token: 0x0600138B RID: 5003 RVA: 0x00067604 File Offset: 0x00065804
	public void StartEvent()
	{
		GameplayEventManager.Instance.Trigger(1491341646, this);
		StateMachine.Instance smi = this.smi;
		smi.OnStop = (Action<string, StateMachine.Status>)Delegate.Combine(smi.OnStop, new Action<string, StateMachine.Status>(this.OnStop));
		this.smi.StartSM();
	}

	// Token: 0x0600138C RID: 5004 RVA: 0x00067653 File Offset: 0x00065853
	public void RegisterMonitorCallback(GameObject go)
	{
		if (this.monitorCallbackObjects == null)
		{
			this.monitorCallbackObjects = new List<GameObject>();
		}
		if (!this.monitorCallbackObjects.Contains(go))
		{
			this.monitorCallbackObjects.Add(go);
		}
	}

	// Token: 0x0600138D RID: 5005 RVA: 0x00067682 File Offset: 0x00065882
	public void UnregisterMonitorCallback(GameObject go)
	{
		if (this.monitorCallbackObjects == null)
		{
			this.monitorCallbackObjects = new List<GameObject>();
		}
		this.monitorCallbackObjects.Remove(go);
	}

	// Token: 0x0600138E RID: 5006 RVA: 0x000676A4 File Offset: 0x000658A4
	public void OnStop(string reason, StateMachine.Status status)
	{
		GameplayEventManager.Instance.Trigger(1287635015, this);
		if (this.monitorCallbackObjects != null)
		{
			this.monitorCallbackObjects.ForEach(delegate(GameObject x)
			{
				x.Trigger(1287635015, this);
			});
		}
		if (status == StateMachine.Status.Success)
		{
			using (List<HashedString>.Enumerator enumerator = this.gameplayEvent.successEvents.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					HashedString hashedString = enumerator.Current;
					GameplayEvent gameplayEvent = Db.Get().GameplayEvents.TryGet(hashedString);
					DebugUtil.DevAssert(gameplayEvent != null, string.Format("GameplayEvent {0} is null", hashedString), null);
					if (gameplayEvent != null && gameplayEvent.IsAllowed())
					{
						GameplayEventManager.Instance.StartNewEvent(gameplayEvent, -1);
					}
				}
				return;
			}
		}
		if (status == StateMachine.Status.Failed)
		{
			foreach (HashedString hashedString2 in this.gameplayEvent.failureEvents)
			{
				GameplayEvent gameplayEvent2 = Db.Get().GameplayEvents.TryGet(hashedString2);
				DebugUtil.DevAssert(gameplayEvent2 != null, string.Format("GameplayEvent {0} is null", hashedString2), null);
				if (gameplayEvent2 != null && gameplayEvent2.IsAllowed())
				{
					GameplayEventManager.Instance.StartNewEvent(gameplayEvent2, -1);
				}
			}
		}
	}

	// Token: 0x0600138F RID: 5007 RVA: 0x000677FC File Offset: 0x000659FC
	public float AgeInCycles()
	{
		return GameUtil.GetCurrentTimeInCycles() - this.eventStartTime;
	}

	// Token: 0x04000A99 RID: 2713
	[Serialize]
	public readonly HashedString eventID;

	// Token: 0x04000A9A RID: 2714
	[Serialize]
	public List<Tag> tags;

	// Token: 0x04000A9B RID: 2715
	[Serialize]
	public float eventStartTime;

	// Token: 0x04000A9C RID: 2716
	[Serialize]
	public readonly int worldId;

	// Token: 0x04000A9D RID: 2717
	[Serialize]
	private bool _seenNotification;

	// Token: 0x04000A9E RID: 2718
	public List<GameObject> monitorCallbackObjects;

	// Token: 0x04000A9F RID: 2719
	public GameplayEventInstance.GameplayEventPopupDataCallback GetEventPopupData;

	// Token: 0x04000AA0 RID: 2720
	private GameplayEvent _gameplayEvent;

	// Token: 0x02000FBF RID: 4031
	// (Invoke) Token: 0x06007061 RID: 28769
	public delegate EventInfoData GameplayEventPopupDataCallback();
}
