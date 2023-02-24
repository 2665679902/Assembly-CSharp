using System;

// Token: 0x020008EB RID: 2283
public class StorageUnloadMonitor : GameStateMachine<StorageUnloadMonitor, StorageUnloadMonitor.Instance, IStateMachineTarget, StorageUnloadMonitor.Def>
{
	// Token: 0x060041C2 RID: 16834 RVA: 0x00171C6C File Offset: 0x0016FE6C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.notFull;
		this.notFull.Transition(this.full, new StateMachine<StorageUnloadMonitor, StorageUnloadMonitor.Instance, IStateMachineTarget, StorageUnloadMonitor.Def>.Transition.ConditionCallback(StorageUnloadMonitor.WantsToUnload), UpdateRate.SIM_200ms);
		this.full.ToggleStatusItem(Db.Get().RobotStatusItems.DustBinFull, (StorageUnloadMonitor.Instance smi) => smi.gameObject).ToggleBehaviour(GameTags.Robots.Behaviours.UnloadBehaviour, (StorageUnloadMonitor.Instance data) => true, null).Transition(this.notFull, GameStateMachine<StorageUnloadMonitor, StorageUnloadMonitor.Instance, IStateMachineTarget, StorageUnloadMonitor.Def>.Not(new StateMachine<StorageUnloadMonitor, StorageUnloadMonitor.Instance, IStateMachineTarget, StorageUnloadMonitor.Def>.Transition.ConditionCallback(StorageUnloadMonitor.WantsToUnload)), UpdateRate.SIM_1000ms)
			.Enter(delegate(StorageUnloadMonitor.Instance smi)
			{
				if (smi.master.gameObject.GetComponents<Storage>()[1].RemainingCapacity() <= 0f)
				{
					smi.master.gameObject.GetSMI<AnimInterruptMonitor.Instance>().PlayAnim("react_full");
				}
			});
	}

	// Token: 0x060041C3 RID: 16835 RVA: 0x00171D48 File Offset: 0x0016FF48
	public static bool WantsToUnload(StorageUnloadMonitor.Instance smi)
	{
		Storage storage = smi.sm.sweepLocker.Get(smi);
		return !(storage == null) && !(smi.sm.internalStorage.Get(smi) == null) && !smi.HasTag(GameTags.Robots.Behaviours.RechargeBehaviour) && (smi.sm.internalStorage.Get(smi).IsFull() || (storage != null && !smi.sm.internalStorage.Get(smi).IsEmpty() && Grid.PosToCell(storage) == Grid.PosToCell(smi)));
	}

	// Token: 0x04002BCF RID: 11215
	public StateMachine<StorageUnloadMonitor, StorageUnloadMonitor.Instance, IStateMachineTarget, StorageUnloadMonitor.Def>.ObjectParameter<Storage> internalStorage = new StateMachine<StorageUnloadMonitor, StorageUnloadMonitor.Instance, IStateMachineTarget, StorageUnloadMonitor.Def>.ObjectParameter<Storage>();

	// Token: 0x04002BD0 RID: 11216
	public StateMachine<StorageUnloadMonitor, StorageUnloadMonitor.Instance, IStateMachineTarget, StorageUnloadMonitor.Def>.ObjectParameter<Storage> sweepLocker;

	// Token: 0x04002BD1 RID: 11217
	public GameStateMachine<StorageUnloadMonitor, StorageUnloadMonitor.Instance, IStateMachineTarget, StorageUnloadMonitor.Def>.State notFull;

	// Token: 0x04002BD2 RID: 11218
	public GameStateMachine<StorageUnloadMonitor, StorageUnloadMonitor.Instance, IStateMachineTarget, StorageUnloadMonitor.Def>.State full;

	// Token: 0x020016AC RID: 5804
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020016AD RID: 5805
	public new class Instance : GameStateMachine<StorageUnloadMonitor, StorageUnloadMonitor.Instance, IStateMachineTarget, StorageUnloadMonitor.Def>.GameInstance
	{
		// Token: 0x06008834 RID: 34868 RVA: 0x002F4E73 File Offset: 0x002F3073
		public Instance(IStateMachineTarget master, StorageUnloadMonitor.Def def)
			: base(master, def)
		{
		}
	}
}
