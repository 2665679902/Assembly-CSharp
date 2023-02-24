using System;

// Token: 0x0200078D RID: 1933
public class SafeCellMonitor : GameStateMachine<SafeCellMonitor, SafeCellMonitor.Instance>
{
	// Token: 0x0600360B RID: 13835 RVA: 0x0012BEA0 File Offset: 0x0012A0A0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		base.serializable = StateMachine.SerializeType.Never;
		this.root.ToggleUrge(Db.Get().Urges.MoveToSafety);
		this.satisfied.EventTransition(GameHashes.SafeCellDetected, this.danger, (SafeCellMonitor.Instance smi) => smi.IsAreaUnsafe());
		this.danger.EventTransition(GameHashes.SafeCellLost, this.satisfied, (SafeCellMonitor.Instance smi) => !smi.IsAreaUnsafe()).ToggleChore((SafeCellMonitor.Instance smi) => new MoveToSafetyChore(smi.master), this.satisfied);
	}

	// Token: 0x04002413 RID: 9235
	public GameStateMachine<SafeCellMonitor, SafeCellMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x04002414 RID: 9236
	public GameStateMachine<SafeCellMonitor, SafeCellMonitor.Instance, IStateMachineTarget, object>.State danger;

	// Token: 0x020014BF RID: 5311
	public new class Instance : GameStateMachine<SafeCellMonitor, SafeCellMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060081D7 RID: 33239 RVA: 0x002E332C File Offset: 0x002E152C
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.safeCellSensor = base.GetComponent<Sensors>().GetSensor<SafeCellSensor>();
		}

		// Token: 0x060081D8 RID: 33240 RVA: 0x002E3346 File Offset: 0x002E1546
		public bool IsAreaUnsafe()
		{
			return this.safeCellSensor.HasSafeCell();
		}

		// Token: 0x040064A7 RID: 25767
		private SafeCellSensor safeCellSensor;
	}
}
