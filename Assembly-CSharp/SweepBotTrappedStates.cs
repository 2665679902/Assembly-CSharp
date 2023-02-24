using System;

// Token: 0x020002E8 RID: 744
public class SweepBotTrappedStates : GameStateMachine<SweepBotTrappedStates, SweepBotTrappedStates.Instance, IStateMachineTarget, SweepBotTrappedStates.Def>
{
	// Token: 0x06000EC2 RID: 3778 RVA: 0x0004FF74 File Offset: 0x0004E174
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.blockedStates.evaluating;
		this.blockedStates.ToggleStatusItem(Db.Get().RobotStatusItems.CantReachStation, (SweepBotTrappedStates.Instance smi) => smi.gameObject, Db.Get().StatusItemCategories.Main).TagTransition(GameTags.Robots.Behaviours.TrappedBehaviour, this.behaviourcomplete, true);
		this.blockedStates.evaluating.Enter(delegate(SweepBotTrappedStates.Instance smi)
		{
			if (smi.sm.GetSweepLocker(smi) == null)
			{
				smi.GoTo(this.blockedStates.noHome);
				return;
			}
			smi.GoTo(this.blockedStates.blocked);
		});
		this.blockedStates.blocked.ToggleChore((SweepBotTrappedStates.Instance smi) => new RescueSweepBotChore(smi.master, smi.master.gameObject, smi.sm.GetSweepLocker(smi).gameObject), this.behaviourcomplete, this.blockedStates.evaluating).PlayAnim("react_stuck", KAnim.PlayMode.Loop);
		this.blockedStates.noHome.PlayAnim("react_stuck", KAnim.PlayMode.Once).OnAnimQueueComplete(this.blockedStates.evaluating);
		this.behaviourcomplete.BehaviourComplete(GameTags.Robots.Behaviours.TrappedBehaviour, false);
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x0005008C File Offset: 0x0004E28C
	public Storage GetSweepLocker(SweepBotTrappedStates.Instance smi)
	{
		StorageUnloadMonitor.Instance smi2 = smi.master.gameObject.GetSMI<StorageUnloadMonitor.Instance>();
		if (smi2 == null)
		{
			return null;
		}
		return smi2.sm.sweepLocker.Get(smi2);
	}

	// Token: 0x04000828 RID: 2088
	public SweepBotTrappedStates.BlockedStates blockedStates;

	// Token: 0x04000829 RID: 2089
	public GameStateMachine<SweepBotTrappedStates, SweepBotTrappedStates.Instance, IStateMachineTarget, SweepBotTrappedStates.Def>.State behaviourcomplete;

	// Token: 0x02000F02 RID: 3842
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000F03 RID: 3843
	public new class Instance : GameStateMachine<SweepBotTrappedStates, SweepBotTrappedStates.Instance, IStateMachineTarget, SweepBotTrappedStates.Def>.GameInstance
	{
		// Token: 0x06006D9C RID: 28060 RVA: 0x0029A756 File Offset: 0x00298956
		public Instance(Chore<SweepBotTrappedStates.Instance> chore, SweepBotTrappedStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Robots.Behaviours.TrappedBehaviour);
		}
	}

	// Token: 0x02000F04 RID: 3844
	public class BlockedStates : GameStateMachine<SweepBotTrappedStates, SweepBotTrappedStates.Instance, IStateMachineTarget, SweepBotTrappedStates.Def>.State
	{
		// Token: 0x040052E6 RID: 21222
		public GameStateMachine<SweepBotTrappedStates, SweepBotTrappedStates.Instance, IStateMachineTarget, SweepBotTrappedStates.Def>.State evaluating;

		// Token: 0x040052E7 RID: 21223
		public GameStateMachine<SweepBotTrappedStates, SweepBotTrappedStates.Instance, IStateMachineTarget, SweepBotTrappedStates.Def>.State blocked;

		// Token: 0x040052E8 RID: 21224
		public GameStateMachine<SweepBotTrappedStates, SweepBotTrappedStates.Instance, IStateMachineTarget, SweepBotTrappedStates.Def>.State noHome;
	}
}
