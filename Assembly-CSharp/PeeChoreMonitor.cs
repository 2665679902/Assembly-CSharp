using System;

// Token: 0x0200083B RID: 2107
public class PeeChoreMonitor : GameStateMachine<PeeChoreMonitor, PeeChoreMonitor.Instance>
{
	// Token: 0x06003CD1 RID: 15569 RVA: 0x00153428 File Offset: 0x00151628
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.building;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.building.Update(delegate(PeeChoreMonitor.Instance smi, float dt)
		{
			this.pee_fuse.Delta(-dt, smi);
		}, UpdateRate.SIM_200ms, false).Transition(this.paused, (PeeChoreMonitor.Instance smi) => this.IsSleeping(smi), UpdateRate.SIM_200ms).Transition(this.critical, (PeeChoreMonitor.Instance smi) => this.pee_fuse.Get(smi) <= 60f, UpdateRate.SIM_200ms);
		this.critical.Update(delegate(PeeChoreMonitor.Instance smi, float dt)
		{
			this.pee_fuse.Delta(-dt, smi);
		}, UpdateRate.SIM_200ms, false).Transition(this.paused, (PeeChoreMonitor.Instance smi) => this.IsSleeping(smi), UpdateRate.SIM_200ms).Transition(this.pee, (PeeChoreMonitor.Instance smi) => this.pee_fuse.Get(smi) <= 0f, UpdateRate.SIM_200ms);
		this.paused.Transition(this.building, (PeeChoreMonitor.Instance smi) => !this.IsSleeping(smi), UpdateRate.SIM_200ms);
		this.pee.ToggleChore(new Func<PeeChoreMonitor.Instance, Chore>(this.CreatePeeChore), this.building);
	}

	// Token: 0x06003CD2 RID: 15570 RVA: 0x00153518 File Offset: 0x00151718
	private bool IsSleeping(PeeChoreMonitor.Instance smi)
	{
		StaminaMonitor.Instance smi2 = smi.master.gameObject.GetSMI<StaminaMonitor.Instance>();
		if (smi2 != null)
		{
			smi2.IsSleeping();
		}
		return false;
	}

	// Token: 0x06003CD3 RID: 15571 RVA: 0x00153541 File Offset: 0x00151741
	private Chore CreatePeeChore(PeeChoreMonitor.Instance smi)
	{
		return new PeeChore(smi.master);
	}

	// Token: 0x040027B1 RID: 10161
	public GameStateMachine<PeeChoreMonitor, PeeChoreMonitor.Instance, IStateMachineTarget, object>.State building;

	// Token: 0x040027B2 RID: 10162
	public GameStateMachine<PeeChoreMonitor, PeeChoreMonitor.Instance, IStateMachineTarget, object>.State critical;

	// Token: 0x040027B3 RID: 10163
	public GameStateMachine<PeeChoreMonitor, PeeChoreMonitor.Instance, IStateMachineTarget, object>.State paused;

	// Token: 0x040027B4 RID: 10164
	public GameStateMachine<PeeChoreMonitor, PeeChoreMonitor.Instance, IStateMachineTarget, object>.State pee;

	// Token: 0x040027B5 RID: 10165
	private StateMachine<PeeChoreMonitor, PeeChoreMonitor.Instance, IStateMachineTarget, object>.FloatParameter pee_fuse = new StateMachine<PeeChoreMonitor, PeeChoreMonitor.Instance, IStateMachineTarget, object>.FloatParameter(120f);

	// Token: 0x020015CB RID: 5579
	public new class Instance : GameStateMachine<PeeChoreMonitor, PeeChoreMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008558 RID: 34136 RVA: 0x002EC4C8 File Offset: 0x002EA6C8
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x06008559 RID: 34137 RVA: 0x002EC4D1 File Offset: 0x002EA6D1
		public bool IsCritical()
		{
			return base.IsInsideState(base.sm.critical);
		}
	}
}
