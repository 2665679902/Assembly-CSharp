using System;

// Token: 0x02000051 RID: 81
public class StorageController : GameStateMachine<StorageController, StorageController.Instance>
{
	// Token: 0x06000169 RID: 361 RVA: 0x0000A2B4 File Offset: 0x000084B4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.root.EventTransition(GameHashes.OnStorageInteracted, this.working, null);
		this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (StorageController.Instance smi) => smi.GetComponent<Operational>().IsOperational);
		this.on.PlayAnim("on").EventTransition(GameHashes.OperationalChanged, this.off, (StorageController.Instance smi) => !smi.GetComponent<Operational>().IsOperational);
		this.working.PlayAnim("working").OnAnimQueueComplete(this.off);
	}

	// Token: 0x040000CF RID: 207
	public GameStateMachine<StorageController, StorageController.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x040000D0 RID: 208
	public GameStateMachine<StorageController, StorageController.Instance, IStateMachineTarget, object>.State on;

	// Token: 0x040000D1 RID: 209
	public GameStateMachine<StorageController, StorageController.Instance, IStateMachineTarget, object>.State working;

	// Token: 0x02000DDA RID: 3546
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000DDB RID: 3547
	public new class Instance : GameStateMachine<StorageController, StorageController.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06006AFC RID: 27388 RVA: 0x00295CD2 File Offset: 0x00293ED2
		public Instance(IStateMachineTarget master, StorageController.Def def)
			: base(master)
		{
		}
	}
}
