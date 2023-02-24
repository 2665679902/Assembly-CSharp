using System;
using STRINGS;

// Token: 0x020000B9 RID: 185
public class DropElementStates : GameStateMachine<DropElementStates, DropElementStates.Instance, IStateMachineTarget, DropElementStates.Def>
{
	// Token: 0x06000342 RID: 834 RVA: 0x00019C68 File Offset: 0x00017E68
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.dropping;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.EXPELLING_GAS.NAME, CREATURES.STATUSITEMS.EXPELLING_GAS.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.dropping.PlayAnim("dirty").OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.Enter("DropElement", delegate(DropElementStates.Instance smi)
		{
			smi.GetSMI<ElementDropperMonitor.Instance>().DropPeriodicElement();
		}).QueueAnim("idle_loop", true, null).BehaviourComplete(GameTags.Creatures.WantsToDropElements, false);
	}

	// Token: 0x0400021F RID: 543
	public GameStateMachine<DropElementStates, DropElementStates.Instance, IStateMachineTarget, DropElementStates.Def>.State dropping;

	// Token: 0x04000220 RID: 544
	public GameStateMachine<DropElementStates, DropElementStates.Instance, IStateMachineTarget, DropElementStates.Def>.State behaviourcomplete;

	// Token: 0x02000E48 RID: 3656
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E49 RID: 3657
	public new class Instance : GameStateMachine<DropElementStates, DropElementStates.Instance, IStateMachineTarget, DropElementStates.Def>.GameInstance
	{
		// Token: 0x06006BF3 RID: 27635 RVA: 0x0029731B File Offset: 0x0029551B
		public Instance(Chore<DropElementStates.Instance> chore, DropElementStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsToDropElements);
		}
	}
}
