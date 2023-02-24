using System;
using STRINGS;

// Token: 0x020000CD RID: 205
public class ImmobileDrowningStates : GameStateMachine<ImmobileDrowningStates, ImmobileDrowningStates.Instance, IStateMachineTarget, ImmobileDrowningStates.Def>
{
	// Token: 0x06000391 RID: 913 RVA: 0x0001BC2C File Offset: 0x00019E2C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.drown;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.DROWNING.NAME, CREATURES.STATUSITEMS.DROWNING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).TagTransition(GameTags.Creatures.Drowning, this.drown_pst, true);
		this.drown.PlayAnim("drown_pre").QueueAnim("drown_loop", true, null);
		this.drown_pst.PlayAnim("drown_pst").OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.Drowning, false);
	}

	// Token: 0x0400024C RID: 588
	public GameStateMachine<ImmobileDrowningStates, ImmobileDrowningStates.Instance, IStateMachineTarget, ImmobileDrowningStates.Def>.State drown;

	// Token: 0x0400024D RID: 589
	public GameStateMachine<ImmobileDrowningStates, ImmobileDrowningStates.Instance, IStateMachineTarget, ImmobileDrowningStates.Def>.State drown_pst;

	// Token: 0x0400024E RID: 590
	public GameStateMachine<ImmobileDrowningStates, ImmobileDrowningStates.Instance, IStateMachineTarget, ImmobileDrowningStates.Def>.State behaviourcomplete;

	// Token: 0x02000E82 RID: 3714
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E83 RID: 3715
	public new class Instance : GameStateMachine<ImmobileDrowningStates, ImmobileDrowningStates.Instance, IStateMachineTarget, ImmobileDrowningStates.Def>.GameInstance
	{
		// Token: 0x06006C6F RID: 27759 RVA: 0x00297F4D File Offset: 0x0029614D
		public Instance(Chore<ImmobileDrowningStates.Instance> chore, ImmobileDrowningStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.HasTag, GameTags.Creatures.Drowning);
		}
	}
}
