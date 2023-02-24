using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000BB RID: 187
public class EatStates : GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>
{
	// Token: 0x06000347 RID: 839 RVA: 0x00019E64 File Offset: 0x00018064
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.goingtoeat;
		this.root.Enter(new StateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State.Callback(EatStates.SetTarget)).Enter(new StateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State.Callback(EatStates.ReserveEdible)).Exit(new StateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State.Callback(EatStates.UnreserveEdible));
		this.goingtoeat.MoveTo(new Func<EatStates.Instance, int>(EatStates.GetEdibleCell), this.eating, null, false).ToggleStatusItem(CREATURES.STATUSITEMS.LOOKINGFORFOOD.NAME, CREATURES.STATUSITEMS.LOOKINGFORFOOD.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.eating.DefaultState(this.eating.pre).ToggleStatusItem(CREATURES.STATUSITEMS.EATING.NAME, CREATURES.STATUSITEMS.EATING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.eating.pre.QueueAnim("eat_pre", false, null).OnAnimQueueComplete(this.eating.loop);
		this.eating.loop.Enter(new StateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State.Callback(EatStates.EatComplete)).QueueAnim("eat_loop", true, null).ScheduleGoTo(3f, this.eating.pst);
		this.eating.pst.QueueAnim("eat_pst", false, null).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.WantsToEat, false);
	}

	// Token: 0x06000348 RID: 840 RVA: 0x0001A005 File Offset: 0x00018205
	private static void SetTarget(EatStates.Instance smi)
	{
		smi.sm.target.Set(smi.GetSMI<SolidConsumerMonitor.Instance>().targetEdible, smi, false);
	}

	// Token: 0x06000349 RID: 841 RVA: 0x0001A028 File Offset: 0x00018228
	private static void ReserveEdible(EatStates.Instance smi)
	{
		GameObject gameObject = smi.sm.target.Get(smi);
		if (gameObject != null)
		{
			DebugUtil.Assert(!gameObject.HasTag(GameTags.Creatures.ReservedByCreature));
			gameObject.AddTag(GameTags.Creatures.ReservedByCreature);
		}
	}

	// Token: 0x0600034A RID: 842 RVA: 0x0001A070 File Offset: 0x00018270
	private static void UnreserveEdible(EatStates.Instance smi)
	{
		GameObject gameObject = smi.sm.target.Get(smi);
		if (gameObject != null)
		{
			if (gameObject.HasTag(GameTags.Creatures.ReservedByCreature))
			{
				gameObject.RemoveTag(GameTags.Creatures.ReservedByCreature);
				return;
			}
			global::Debug.LogWarningFormat(smi.gameObject, "{0} UnreserveEdible but it wasn't reserved: {1}", new object[] { smi.gameObject, gameObject });
		}
	}

	// Token: 0x0600034B RID: 843 RVA: 0x0001A0D4 File Offset: 0x000182D4
	private static void EatComplete(EatStates.Instance smi)
	{
		PrimaryElement primaryElement = smi.sm.target.Get<PrimaryElement>(smi);
		if (primaryElement != null)
		{
			smi.lastMealElement = primaryElement.Element;
		}
		smi.Trigger(1386391852, smi.sm.target.Get<KPrefabID>(smi));
	}

	// Token: 0x0600034C RID: 844 RVA: 0x0001A124 File Offset: 0x00018324
	private static int GetEdibleCell(EatStates.Instance smi)
	{
		return Grid.PosToCell(smi.sm.target.Get(smi));
	}

	// Token: 0x04000224 RID: 548
	public GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.ApproachSubState<Pickupable> goingtoeat;

	// Token: 0x04000225 RID: 549
	public EatStates.EatingState eating;

	// Token: 0x04000226 RID: 550
	public GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State behaviourcomplete;

	// Token: 0x04000227 RID: 551
	public StateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.TargetParameter target;

	// Token: 0x02000E4F RID: 3663
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E50 RID: 3664
	public new class Instance : GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.GameInstance
	{
		// Token: 0x06006BFF RID: 27647 RVA: 0x002973D8 File Offset: 0x002955D8
		public Instance(Chore<EatStates.Instance> chore, EatStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsToEat);
		}

		// Token: 0x06006C00 RID: 27648 RVA: 0x002973FC File Offset: 0x002955FC
		public Element GetLatestMealElement()
		{
			return this.lastMealElement;
		}

		// Token: 0x04005147 RID: 20807
		public Element lastMealElement;
	}

	// Token: 0x02000E51 RID: 3665
	public class EatingState : GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State
	{
		// Token: 0x04005148 RID: 20808
		public GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State pre;

		// Token: 0x04005149 RID: 20809
		public GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State loop;

		// Token: 0x0400514A RID: 20810
		public GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State pst;
	}
}
