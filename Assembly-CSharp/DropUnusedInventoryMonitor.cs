using System;

// Token: 0x02000828 RID: 2088
public class DropUnusedInventoryMonitor : GameStateMachine<DropUnusedInventoryMonitor, DropUnusedInventoryMonitor.Instance>
{
	// Token: 0x06003C76 RID: 15478 RVA: 0x00150FA0 File Offset: 0x0014F1A0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.EventTransition(GameHashes.OnStorageChange, this.hasinventory, (DropUnusedInventoryMonitor.Instance smi) => smi.GetComponent<Storage>().Count > 0);
		this.hasinventory.EventTransition(GameHashes.OnStorageChange, this.hasinventory, (DropUnusedInventoryMonitor.Instance smi) => smi.GetComponent<Storage>().Count == 0).ToggleChore((DropUnusedInventoryMonitor.Instance smi) => new DropUnusedInventoryChore(Db.Get().ChoreTypes.DropUnusedInventory, smi.master), this.satisfied);
	}

	// Token: 0x0400275F RID: 10079
	public GameStateMachine<DropUnusedInventoryMonitor, DropUnusedInventoryMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x04002760 RID: 10080
	public GameStateMachine<DropUnusedInventoryMonitor, DropUnusedInventoryMonitor.Instance, IStateMachineTarget, object>.State hasinventory;

	// Token: 0x0200159F RID: 5535
	public new class Instance : GameStateMachine<DropUnusedInventoryMonitor, DropUnusedInventoryMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008491 RID: 33937 RVA: 0x002EA47F File Offset: 0x002E867F
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}
	}
}
