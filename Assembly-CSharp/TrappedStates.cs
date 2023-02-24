using System;
using STRINGS;

// Token: 0x020000DC RID: 220
public class TrappedStates : GameStateMachine<TrappedStates, TrappedStates.Instance, IStateMachineTarget, TrappedStates.Def>
{
	// Token: 0x060003EB RID: 1003 RVA: 0x0001E450 File Offset: 0x0001C650
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.trapped;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.TRAPPED.NAME, CREATURES.STATUSITEMS.TRAPPED.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.trapped.Enter(delegate(TrappedStates.Instance smi)
		{
			Navigator component = smi.GetComponent<Navigator>();
			if (component.IsValidNavType(NavType.Floor))
			{
				component.SetCurrentNavType(NavType.Floor);
			}
		}).ToggleTag(GameTags.Creatures.Deliverable).PlayAnim("trapped", KAnim.PlayMode.Loop)
			.TagTransition(GameTags.Trapped, null, true);
	}

	// Token: 0x0400027A RID: 634
	private GameStateMachine<TrappedStates, TrappedStates.Instance, IStateMachineTarget, TrappedStates.Def>.State trapped;

	// Token: 0x02000EB5 RID: 3765
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000EB6 RID: 3766
	public new class Instance : GameStateMachine<TrappedStates, TrappedStates.Instance, IStateMachineTarget, TrappedStates.Def>.GameInstance
	{
		// Token: 0x06006CF5 RID: 27893 RVA: 0x002994CA File Offset: 0x002976CA
		public Instance(Chore<TrappedStates.Instance> chore, TrappedStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(TrappedStates.Instance.IsTrapped, null);
		}

		// Token: 0x04005223 RID: 21027
		public static readonly Chore.Precondition IsTrapped = new Chore.Precondition
		{
			id = "IsTrapped",
			fn = delegate(ref Chore.Precondition.Context context, object data)
			{
				return context.consumerState.prefabid.HasTag(GameTags.Trapped);
			}
		};
	}
}
