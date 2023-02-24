using System;
using UnityEngine;

// Token: 0x020003F9 RID: 1017
public abstract class GameplayEventStateMachine<StateMachineType, StateMachineInstanceType, MasterType, SecondMasterType> : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType> where StateMachineType : GameplayEventStateMachine<StateMachineType, StateMachineInstanceType, MasterType, SecondMasterType> where StateMachineInstanceType : GameplayEventStateMachine<StateMachineType, StateMachineInstanceType, MasterType, SecondMasterType>.GameplayEventStateMachineInstance where MasterType : IStateMachineTarget where SecondMasterType : GameplayEvent<StateMachineInstanceType>
{
	// Token: 0x06001505 RID: 5381 RVA: 0x0006DED4 File Offset: 0x0006C0D4
	public void MonitorStart(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, object>.TargetParameter target, StateMachineInstanceType smi)
	{
		GameObject gameObject = target.Get(smi);
		if (gameObject != null)
		{
			gameObject.Trigger(-1660384580, smi.eventInstance);
		}
	}

	// Token: 0x06001506 RID: 5382 RVA: 0x0006DF08 File Offset: 0x0006C108
	public void MonitorChanged(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, object>.TargetParameter target, StateMachineInstanceType smi)
	{
		GameObject gameObject = target.Get(smi);
		if (gameObject != null)
		{
			gameObject.Trigger(-1122598290, smi.eventInstance);
		}
	}

	// Token: 0x06001507 RID: 5383 RVA: 0x0006DF3C File Offset: 0x0006C13C
	public void MonitorStop(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, object>.TargetParameter target, StateMachineInstanceType smi)
	{
		GameObject gameObject = target.Get(smi);
		if (gameObject != null)
		{
			gameObject.Trigger(-828272459, smi.eventInstance);
		}
	}

	// Token: 0x06001508 RID: 5384 RVA: 0x0006DF70 File Offset: 0x0006C170
	public virtual EventInfoData GenerateEventPopupData(StateMachineInstanceType smi)
	{
		return null;
	}

	// Token: 0x02001012 RID: 4114
	public class GameplayEventStateMachineInstance : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, object>.GameInstance
	{
		// Token: 0x060071F3 RID: 29171 RVA: 0x002AC2B1 File Offset: 0x002AA4B1
		public GameplayEventStateMachineInstance(MasterType master, GameplayEventInstance eventInstance, SecondMasterType gameplayEvent)
			: base(master)
		{
			this.gameplayEvent = gameplayEvent;
			this.eventInstance = eventInstance;
			eventInstance.GetEventPopupData = () => base.smi.sm.GenerateEventPopupData(base.smi);
			this.serializationSuffix = gameplayEvent.Id;
		}

		// Token: 0x04005652 RID: 22098
		public GameplayEventInstance eventInstance;

		// Token: 0x04005653 RID: 22099
		public SecondMasterType gameplayEvent;
	}
}
