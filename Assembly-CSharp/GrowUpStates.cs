using System;
using STRINGS;

// Token: 0x020000C1 RID: 193
public class GrowUpStates : GameStateMachine<GrowUpStates, GrowUpStates.Instance, IStateMachineTarget, GrowUpStates.Def>
{
	// Token: 0x06000363 RID: 867 RVA: 0x0001AB60 File Offset: 0x00018D60
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.grow_up_pre;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.GROWINGUP.NAME, CREATURES.STATUSITEMS.GROWINGUP.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.grow_up_pre.QueueAnim("growup_pre", false, null).OnAnimQueueComplete(this.spawn_adult);
		this.spawn_adult.Enter(new StateMachine<GrowUpStates, GrowUpStates.Instance, IStateMachineTarget, GrowUpStates.Def>.State.Callback(GrowUpStates.SpawnAdult));
	}

	// Token: 0x06000364 RID: 868 RVA: 0x0001ABF2 File Offset: 0x00018DF2
	private static void SpawnAdult(GrowUpStates.Instance smi)
	{
		smi.GetSMI<BabyMonitor.Instance>().SpawnAdult();
	}

	// Token: 0x04000238 RID: 568
	public GameStateMachine<GrowUpStates, GrowUpStates.Instance, IStateMachineTarget, GrowUpStates.Def>.State grow_up_pre;

	// Token: 0x04000239 RID: 569
	public GameStateMachine<GrowUpStates, GrowUpStates.Instance, IStateMachineTarget, GrowUpStates.Def>.State spawn_adult;

	// Token: 0x02000E60 RID: 3680
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E61 RID: 3681
	public new class Instance : GameStateMachine<GrowUpStates, GrowUpStates.Instance, IStateMachineTarget, GrowUpStates.Def>.GameInstance
	{
		// Token: 0x06006C20 RID: 27680 RVA: 0x00297715 File Offset: 0x00295915
		public Instance(Chore<GrowUpStates.Instance> chore, GrowUpStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Behaviours.GrowUpBehaviour);
		}
	}
}
