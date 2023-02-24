using System;

// Token: 0x020006E9 RID: 1769
public class RocketSelfDestructMonitor : GameStateMachine<RocketSelfDestructMonitor, RocketSelfDestructMonitor.Instance>
{
	// Token: 0x0600301E RID: 12318 RVA: 0x000FE4A8 File Offset: 0x000FC6A8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		this.idle.EventTransition(GameHashes.RocketSelfDestructRequested, this.exploding, null);
		this.exploding.Update(delegate(RocketSelfDestructMonitor.Instance smi, float dt)
		{
			if (smi.timeinstate >= 3f)
			{
				smi.master.Trigger(-1311384361, null);
				smi.GoTo(this.idle);
			}
		}, UpdateRate.SIM_200ms, false);
	}

	// Token: 0x04001D05 RID: 7429
	public GameStateMachine<RocketSelfDestructMonitor, RocketSelfDestructMonitor.Instance, IStateMachineTarget, object>.State idle;

	// Token: 0x04001D06 RID: 7430
	public GameStateMachine<RocketSelfDestructMonitor, RocketSelfDestructMonitor.Instance, IStateMachineTarget, object>.State exploding;

	// Token: 0x020013FD RID: 5117
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020013FE RID: 5118
	public new class Instance : GameStateMachine<RocketSelfDestructMonitor, RocketSelfDestructMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06007FC3 RID: 32707 RVA: 0x002DDB0A File Offset: 0x002DBD0A
		public Instance(IStateMachineTarget master, RocketSelfDestructMonitor.Def def)
			: base(master)
		{
		}

		// Token: 0x04006230 RID: 25136
		public KBatchedAnimController eyes;
	}
}
