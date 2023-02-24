using System;
using STRINGS;

// Token: 0x020000DB RID: 219
public class StunnedStates : GameStateMachine<StunnedStates, StunnedStates.Instance, IStateMachineTarget, StunnedStates.Def>
{
	// Token: 0x060003E9 RID: 1001 RVA: 0x0001E3CC File Offset: 0x0001C5CC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.stunned;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.GETTING_WRANGLED.NAME, CREATURES.STATUSITEMS.GETTING_WRANGLED.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.stunned.PlayAnim("idle_loop", KAnim.PlayMode.Loop).TagTransition(GameTags.Creatures.Stunned, null, true);
	}

	// Token: 0x04000279 RID: 633
	public GameStateMachine<StunnedStates, StunnedStates.Instance, IStateMachineTarget, StunnedStates.Def>.State stunned;

	// Token: 0x02000EB3 RID: 3763
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000EB4 RID: 3764
	public new class Instance : GameStateMachine<StunnedStates, StunnedStates.Instance, IStateMachineTarget, StunnedStates.Def>.GameInstance
	{
		// Token: 0x06006CF2 RID: 27890 RVA: 0x0029946E File Offset: 0x0029766E
		public Instance(Chore<StunnedStates.Instance> chore, StunnedStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(StunnedStates.Instance.IsStunned, null);
		}

		// Token: 0x04005222 RID: 21026
		public static readonly Chore.Precondition IsStunned = new Chore.Precondition
		{
			id = "IsStunned",
			fn = delegate(ref Chore.Precondition.Context context, object data)
			{
				return context.consumerState.prefabid.HasTag(GameTags.Creatures.Stunned);
			}
		};
	}
}
