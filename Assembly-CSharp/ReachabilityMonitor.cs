using System;

// Token: 0x020004BD RID: 1213
public class ReachabilityMonitor : GameStateMachine<ReachabilityMonitor, ReachabilityMonitor.Instance, Workable>
{
	// Token: 0x06001BFE RID: 7166 RVA: 0x00094A18 File Offset: 0x00092C18
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.unreachable;
		base.serializable = StateMachine.SerializeType.Never;
		this.root.FastUpdate("UpdateReachability", ReachabilityMonitor.updateReachabilityCB, UpdateRate.SIM_1000ms, true);
		this.reachable.ToggleTag(GameTags.Reachable).Enter("TriggerEvent", delegate(ReachabilityMonitor.Instance smi)
		{
			smi.TriggerEvent();
		}).ParamTransition<bool>(this.isReachable, this.unreachable, GameStateMachine<ReachabilityMonitor, ReachabilityMonitor.Instance, Workable, object>.IsFalse);
		this.unreachable.Enter("TriggerEvent", delegate(ReachabilityMonitor.Instance smi)
		{
			smi.TriggerEvent();
		}).ParamTransition<bool>(this.isReachable, this.reachable, GameStateMachine<ReachabilityMonitor, ReachabilityMonitor.Instance, Workable, object>.IsTrue);
	}

	// Token: 0x04000FA5 RID: 4005
	public GameStateMachine<ReachabilityMonitor, ReachabilityMonitor.Instance, Workable, object>.State reachable;

	// Token: 0x04000FA6 RID: 4006
	public GameStateMachine<ReachabilityMonitor, ReachabilityMonitor.Instance, Workable, object>.State unreachable;

	// Token: 0x04000FA7 RID: 4007
	public StateMachine<ReachabilityMonitor, ReachabilityMonitor.Instance, Workable, object>.BoolParameter isReachable = new StateMachine<ReachabilityMonitor, ReachabilityMonitor.Instance, Workable, object>.BoolParameter(false);

	// Token: 0x04000FA8 RID: 4008
	private static ReachabilityMonitor.UpdateReachabilityCB updateReachabilityCB = new ReachabilityMonitor.UpdateReachabilityCB();

	// Token: 0x020010F7 RID: 4343
	private class UpdateReachabilityCB : UpdateBucketWithUpdater<ReachabilityMonitor.Instance>.IUpdater
	{
		// Token: 0x06007500 RID: 29952 RVA: 0x002B4942 File Offset: 0x002B2B42
		public void Update(ReachabilityMonitor.Instance smi, float dt)
		{
			smi.UpdateReachability();
		}
	}

	// Token: 0x020010F8 RID: 4344
	public new class Instance : GameStateMachine<ReachabilityMonitor, ReachabilityMonitor.Instance, Workable, object>.GameInstance
	{
		// Token: 0x06007502 RID: 29954 RVA: 0x002B4952 File Offset: 0x002B2B52
		public Instance(Workable workable)
			: base(workable)
		{
			this.UpdateReachability();
		}

		// Token: 0x06007503 RID: 29955 RVA: 0x002B4964 File Offset: 0x002B2B64
		public void TriggerEvent()
		{
			bool flag = base.sm.isReachable.Get(base.smi);
			base.Trigger(-1432940121, flag);
		}

		// Token: 0x06007504 RID: 29956 RVA: 0x002B499C File Offset: 0x002B2B9C
		public void UpdateReachability()
		{
			if (base.master == null)
			{
				return;
			}
			int num = Grid.PosToCell(base.master);
			base.sm.isReachable.Set(MinionGroupProber.Get().IsAllReachable(num, base.master.GetOffsets(num)), base.smi, false);
		}
	}
}
