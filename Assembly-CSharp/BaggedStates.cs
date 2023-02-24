using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020000A6 RID: 166
public class BaggedStates : GameStateMachine<BaggedStates, BaggedStates.Instance, IStateMachineTarget, BaggedStates.Def>
{
	// Token: 0x060002D6 RID: 726 RVA: 0x00016E18 File Offset: 0x00015018
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.bagged;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.BAGGED.NAME, CREATURES.STATUSITEMS.BAGGED.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.bagged.Enter(new StateMachine<BaggedStates, BaggedStates.Instance, IStateMachineTarget, BaggedStates.Def>.State.Callback(BaggedStates.BagStart)).ToggleTag(GameTags.Creatures.Deliverable).PlayAnim("trussed", KAnim.PlayMode.Loop)
			.TagTransition(GameTags.Creatures.Bagged, null, true)
			.Transition(this.escape, new StateMachine<BaggedStates, BaggedStates.Instance, IStateMachineTarget, BaggedStates.Def>.Transition.ConditionCallback(BaggedStates.ShouldEscape), UpdateRate.SIM_4000ms)
			.EventHandler(GameHashes.OnStore, new StateMachine<BaggedStates, BaggedStates.Instance, IStateMachineTarget, BaggedStates.Def>.State.Callback(BaggedStates.OnStore))
			.Exit(new StateMachine<BaggedStates, BaggedStates.Instance, IStateMachineTarget, BaggedStates.Def>.State.Callback(BaggedStates.BagEnd));
		this.escape.Enter(new StateMachine<BaggedStates, BaggedStates.Instance, IStateMachineTarget, BaggedStates.Def>.State.Callback(BaggedStates.Unbag)).PlayAnim("escape").OnAnimQueueComplete(null);
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x00016F1B File Offset: 0x0001511B
	private static void BagStart(BaggedStates.Instance smi)
	{
		if (smi.baggedTime == 0f)
		{
			smi.baggedTime = GameClock.Instance.GetTime();
		}
		smi.UpdateFaller(true);
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x00016F41 File Offset: 0x00015141
	private static void BagEnd(BaggedStates.Instance smi)
	{
		smi.baggedTime = 0f;
		smi.UpdateFaller(false);
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x00016F58 File Offset: 0x00015158
	private static void Unbag(BaggedStates.Instance smi)
	{
		Baggable component = smi.gameObject.GetComponent<Baggable>();
		if (component)
		{
			component.Free();
		}
	}

	// Token: 0x060002DA RID: 730 RVA: 0x00016F7F File Offset: 0x0001517F
	private static void OnStore(BaggedStates.Instance smi)
	{
		smi.UpdateFaller(true);
	}

	// Token: 0x060002DB RID: 731 RVA: 0x00016F88 File Offset: 0x00015188
	private static bool ShouldEscape(BaggedStates.Instance smi)
	{
		return !smi.gameObject.HasTag(GameTags.Stored) && GameClock.Instance.GetTime() - smi.baggedTime >= smi.def.escapeTime;
	}

	// Token: 0x040001E4 RID: 484
	public GameStateMachine<BaggedStates, BaggedStates.Instance, IStateMachineTarget, BaggedStates.Def>.State bagged;

	// Token: 0x040001E5 RID: 485
	public GameStateMachine<BaggedStates, BaggedStates.Instance, IStateMachineTarget, BaggedStates.Def>.State escape;

	// Token: 0x02000E02 RID: 3586
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040050AB RID: 20651
		public float escapeTime = 300f;
	}

	// Token: 0x02000E03 RID: 3587
	public new class Instance : GameStateMachine<BaggedStates, BaggedStates.Instance, IStateMachineTarget, BaggedStates.Def>.GameInstance
	{
		// Token: 0x06006B5E RID: 27486 RVA: 0x0029655F File Offset: 0x0029475F
		public Instance(Chore<BaggedStates.Instance> chore, BaggedStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(BaggedStates.Instance.IsBagged, null);
		}

		// Token: 0x06006B5F RID: 27487 RVA: 0x00296578 File Offset: 0x00294778
		public void UpdateFaller(bool bagged)
		{
			bool flag = bagged && !base.gameObject.HasTag(GameTags.Stored);
			bool flag2 = GameComps.Fallers.Has(base.gameObject);
			if (flag != flag2)
			{
				if (flag)
				{
					GameComps.Fallers.Add(base.gameObject, Vector2.zero);
					return;
				}
				GameComps.Fallers.Remove(base.gameObject);
			}
		}

		// Token: 0x040050AC RID: 20652
		[Serialize]
		public float baggedTime;

		// Token: 0x040050AD RID: 20653
		public static readonly Chore.Precondition IsBagged = new Chore.Precondition
		{
			id = "IsBagged",
			fn = delegate(ref Chore.Precondition.Context context, object data)
			{
				return context.consumerState.prefabid.HasTag(GameTags.Creatures.Bagged);
			}
		};
	}
}
