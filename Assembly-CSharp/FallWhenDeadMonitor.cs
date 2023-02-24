using System;

// Token: 0x0200047F RID: 1151
public class FallWhenDeadMonitor : GameStateMachine<FallWhenDeadMonitor, FallWhenDeadMonitor.Instance>
{
	// Token: 0x060019B7 RID: 6583 RVA: 0x0008A45C File Offset: 0x0008865C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.standing;
		this.standing.Transition(this.entombed, (FallWhenDeadMonitor.Instance smi) => smi.IsEntombed(), UpdateRate.SIM_200ms).Transition(this.falling, (FallWhenDeadMonitor.Instance smi) => smi.IsFalling(), UpdateRate.SIM_200ms);
		this.falling.ToggleGravity(this.standing);
		this.entombed.Transition(this.standing, (FallWhenDeadMonitor.Instance smi) => !smi.IsEntombed(), UpdateRate.SIM_200ms);
	}

	// Token: 0x04000E6A RID: 3690
	public GameStateMachine<FallWhenDeadMonitor, FallWhenDeadMonitor.Instance, IStateMachineTarget, object>.State standing;

	// Token: 0x04000E6B RID: 3691
	public GameStateMachine<FallWhenDeadMonitor, FallWhenDeadMonitor.Instance, IStateMachineTarget, object>.State falling;

	// Token: 0x04000E6C RID: 3692
	public GameStateMachine<FallWhenDeadMonitor, FallWhenDeadMonitor.Instance, IStateMachineTarget, object>.State entombed;

	// Token: 0x020010CA RID: 4298
	public new class Instance : GameStateMachine<FallWhenDeadMonitor, FallWhenDeadMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600746B RID: 29803 RVA: 0x002B327B File Offset: 0x002B147B
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x0600746C RID: 29804 RVA: 0x002B3284 File Offset: 0x002B1484
		public bool IsEntombed()
		{
			Pickupable component = base.GetComponent<Pickupable>();
			return component != null && component.IsEntombed;
		}

		// Token: 0x0600746D RID: 29805 RVA: 0x002B32AC File Offset: 0x002B14AC
		public bool IsFalling()
		{
			int num = Grid.CellBelow(Grid.PosToCell(base.master.transform.GetPosition()));
			return Grid.IsValidCell(num) && !Grid.Solid[num];
		}
	}
}
