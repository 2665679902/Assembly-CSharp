using System;

// Token: 0x02000837 RID: 2103
public class MingleMonitor : GameStateMachine<MingleMonitor, MingleMonitor.Instance>
{
	// Token: 0x06003CB0 RID: 15536 RVA: 0x00152C5E File Offset: 0x00150E5E
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.mingle;
		base.serializable = StateMachine.SerializeType.Never;
		this.mingle.ToggleRecurringChore(new Func<MingleMonitor.Instance, Chore>(this.CreateMingleChore), null);
	}

	// Token: 0x06003CB1 RID: 15537 RVA: 0x00152C88 File Offset: 0x00150E88
	private Chore CreateMingleChore(MingleMonitor.Instance smi)
	{
		return new MingleChore(smi.master);
	}

	// Token: 0x040027A4 RID: 10148
	public GameStateMachine<MingleMonitor, MingleMonitor.Instance, IStateMachineTarget, object>.State mingle;

	// Token: 0x020015C4 RID: 5572
	public new class Instance : GameStateMachine<MingleMonitor, MingleMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008545 RID: 34117 RVA: 0x002EC30A File Offset: 0x002EA50A
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}
	}
}
