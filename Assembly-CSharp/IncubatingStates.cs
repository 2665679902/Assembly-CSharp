using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000CE RID: 206
public class IncubatingStates : GameStateMachine<IncubatingStates, IncubatingStates.Instance, IStateMachineTarget, IncubatingStates.Def>
{
	// Token: 0x06000393 RID: 915 RVA: 0x0001BCEC File Offset: 0x00019EEC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.incubator;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.IN_INCUBATOR.NAME, CREATURES.STATUSITEMS.IN_INCUBATOR.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.incubator.DefaultState(this.incubator.idle).ToggleTag(GameTags.Creatures.Deliverable).TagTransition(GameTags.Creatures.InIncubator, null, true);
		this.incubator.idle.Enter("VariantUpdate", new StateMachine<IncubatingStates, IncubatingStates.Instance, IStateMachineTarget, IncubatingStates.Def>.State.Callback(IncubatingStates.VariantUpdate)).PlayAnim("incubator_idle_loop").OnAnimQueueComplete(this.incubator.choose);
		this.incubator.choose.Transition(this.incubator.variant, new StateMachine<IncubatingStates, IncubatingStates.Instance, IStateMachineTarget, IncubatingStates.Def>.Transition.ConditionCallback(IncubatingStates.DoVariant), UpdateRate.SIM_200ms).Transition(this.incubator.idle, GameStateMachine<IncubatingStates, IncubatingStates.Instance, IStateMachineTarget, IncubatingStates.Def>.Not(new StateMachine<IncubatingStates, IncubatingStates.Instance, IStateMachineTarget, IncubatingStates.Def>.Transition.ConditionCallback(IncubatingStates.DoVariant)), UpdateRate.SIM_200ms);
		this.incubator.variant.PlayAnim("incubator_variant").OnAnimQueueComplete(this.incubator.idle);
	}

	// Token: 0x06000394 RID: 916 RVA: 0x0001BE22 File Offset: 0x0001A022
	public static bool DoVariant(IncubatingStates.Instance smi)
	{
		return smi.variant_time == 0;
	}

	// Token: 0x06000395 RID: 917 RVA: 0x0001BE2D File Offset: 0x0001A02D
	public static void VariantUpdate(IncubatingStates.Instance smi)
	{
		if (smi.variant_time <= 0)
		{
			smi.variant_time = UnityEngine.Random.Range(3, 7);
			return;
		}
		smi.variant_time--;
	}

	// Token: 0x0400024F RID: 591
	public IncubatingStates.IncubatorStates incubator;

	// Token: 0x02000E84 RID: 3716
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E85 RID: 3717
	public new class Instance : GameStateMachine<IncubatingStates, IncubatingStates.Instance, IStateMachineTarget, IncubatingStates.Def>.GameInstance
	{
		// Token: 0x06006C71 RID: 27761 RVA: 0x00297F79 File Offset: 0x00296179
		public Instance(Chore<IncubatingStates.Instance> chore, IncubatingStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(IncubatingStates.Instance.IsInIncubator, null);
		}

		// Token: 0x0400519F RID: 20895
		public int variant_time = 3;

		// Token: 0x040051A0 RID: 20896
		public static readonly Chore.Precondition IsInIncubator = new Chore.Precondition
		{
			id = "IsInIncubator",
			fn = delegate(ref Chore.Precondition.Context context, object data)
			{
				return context.consumerState.prefabid.HasTag(GameTags.Creatures.InIncubator);
			}
		};
	}

	// Token: 0x02000E86 RID: 3718
	public class IncubatorStates : GameStateMachine<IncubatingStates, IncubatingStates.Instance, IStateMachineTarget, IncubatingStates.Def>.State
	{
		// Token: 0x040051A1 RID: 20897
		public GameStateMachine<IncubatingStates, IncubatingStates.Instance, IStateMachineTarget, IncubatingStates.Def>.State idle;

		// Token: 0x040051A2 RID: 20898
		public GameStateMachine<IncubatingStates, IncubatingStates.Instance, IStateMachineTarget, IncubatingStates.Def>.State choose;

		// Token: 0x040051A3 RID: 20899
		public GameStateMachine<IncubatingStates, IncubatingStates.Instance, IStateMachineTarget, IncubatingStates.Def>.State variant;
	}
}
