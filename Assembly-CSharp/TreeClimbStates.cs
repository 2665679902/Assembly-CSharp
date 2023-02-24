using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000DD RID: 221
public class TreeClimbStates : GameStateMachine<TreeClimbStates, TreeClimbStates.Instance, IStateMachineTarget, TreeClimbStates.Def>
{
	// Token: 0x060003ED RID: 1005 RVA: 0x0001E500 File Offset: 0x0001C700
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.moving;
		this.root.Enter(new StateMachine<TreeClimbStates, TreeClimbStates.Instance, IStateMachineTarget, TreeClimbStates.Def>.State.Callback(TreeClimbStates.SetTarget)).Enter(delegate(TreeClimbStates.Instance smi)
		{
			if (!TreeClimbStates.ReserveClimbable(smi))
			{
				smi.GoTo(this.behaviourcomplete);
			}
		}).Exit(new StateMachine<TreeClimbStates, TreeClimbStates.Instance, IStateMachineTarget, TreeClimbStates.Def>.State.Callback(TreeClimbStates.UnreserveClimbable))
			.ToggleStatusItem(CREATURES.STATUSITEMS.RUMMAGINGSEED.NAME, CREATURES.STATUSITEMS.RUMMAGINGSEED.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.moving.MoveTo(new Func<TreeClimbStates.Instance, int>(TreeClimbStates.GetClimbableCell), this.climbing, this.behaviourcomplete, false);
		this.climbing.DefaultState(this.climbing.pre);
		this.climbing.pre.PlayAnim("rummage_pre").OnAnimQueueComplete(this.climbing.loop);
		this.climbing.loop.QueueAnim("rummage_loop", true, null).ScheduleGoTo(3.5f, this.climbing.pst).Update(new Action<TreeClimbStates.Instance, float>(TreeClimbStates.Rummage), UpdateRate.SIM_1000ms, false);
		this.climbing.pst.QueueAnim("rummage_pst", false, null).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.WantsToClimbTree, false);
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x0001E666 File Offset: 0x0001C866
	private static void SetTarget(TreeClimbStates.Instance smi)
	{
		smi.sm.target.Set(smi.GetSMI<ClimbableTreeMonitor.Instance>().climbTarget, smi, false);
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x0001E688 File Offset: 0x0001C888
	private static bool ReserveClimbable(TreeClimbStates.Instance smi)
	{
		GameObject gameObject = smi.sm.target.Get(smi);
		if (gameObject != null && !gameObject.HasTag(GameTags.Creatures.ReservedByCreature))
		{
			gameObject.AddTag(GameTags.Creatures.ReservedByCreature);
			return true;
		}
		return false;
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x0001E6CC File Offset: 0x0001C8CC
	private static void UnreserveClimbable(TreeClimbStates.Instance smi)
	{
		GameObject gameObject = smi.sm.target.Get(smi);
		if (gameObject != null)
		{
			gameObject.RemoveTag(GameTags.Creatures.ReservedByCreature);
		}
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x0001E700 File Offset: 0x0001C900
	private static void Rummage(TreeClimbStates.Instance smi, float dt)
	{
		GameObject gameObject = smi.sm.target.Get(smi);
		if (gameObject != null)
		{
			BuddingTrunk component = gameObject.GetComponent<BuddingTrunk>();
			if (component)
			{
				component.ExtractExtraSeed();
				return;
			}
			Storage component2 = gameObject.GetComponent<Storage>();
			if (component2 && component2.items.Count > 0)
			{
				int num = UnityEngine.Random.Range(0, component2.items.Count - 1);
				GameObject gameObject2 = component2.items[num];
				Pickupable pickupable = (gameObject2 ? gameObject2.GetComponent<Pickupable>() : null);
				if (pickupable && pickupable.UnreservedAmount > 0.01f)
				{
					smi.Toss(pickupable);
				}
			}
		}
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x0001E7B4 File Offset: 0x0001C9B4
	private static int GetClimbableCell(TreeClimbStates.Instance smi)
	{
		return Grid.PosToCell(smi.sm.target.Get(smi));
	}

	// Token: 0x0400027B RID: 635
	public GameStateMachine<TreeClimbStates, TreeClimbStates.Instance, IStateMachineTarget, TreeClimbStates.Def>.ApproachSubState<Uprootable> moving;

	// Token: 0x0400027C RID: 636
	public TreeClimbStates.ClimbState climbing;

	// Token: 0x0400027D RID: 637
	public GameStateMachine<TreeClimbStates, TreeClimbStates.Instance, IStateMachineTarget, TreeClimbStates.Def>.State behaviourcomplete;

	// Token: 0x0400027E RID: 638
	public StateMachine<TreeClimbStates, TreeClimbStates.Instance, IStateMachineTarget, TreeClimbStates.Def>.TargetParameter target;

	// Token: 0x02000EB8 RID: 3768
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000EB9 RID: 3769
	public new class Instance : GameStateMachine<TreeClimbStates, TreeClimbStates.Instance, IStateMachineTarget, TreeClimbStates.Def>.GameInstance
	{
		// Token: 0x06006CFB RID: 27899 RVA: 0x00299560 File Offset: 0x00297760
		public Instance(Chore<TreeClimbStates.Instance> chore, TreeClimbStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsToClimbTree);
			this.storage = base.GetComponent<Storage>();
		}

		// Token: 0x06006CFC RID: 27900 RVA: 0x00299590 File Offset: 0x00297790
		public void Toss(Pickupable pu)
		{
			Pickupable pickupable = pu.Take(Mathf.Min(1f, pu.UnreservedAmount));
			if (pickupable != null)
			{
				this.storage.Store(pickupable.gameObject, true, false, true, false);
				this.storage.Drop(pickupable.gameObject, true);
				this.Throw(pickupable.gameObject);
			}
		}

		// Token: 0x06006CFD RID: 27901 RVA: 0x002995F4 File Offset: 0x002977F4
		private void Throw(GameObject ore_go)
		{
			Vector3 position = base.transform.GetPosition();
			position.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
			int num = Grid.PosToCell(position);
			int num2 = Grid.CellAbove(num);
			Vector2 zero;
			if ((Grid.IsValidCell(num) && Grid.Solid[num]) || (Grid.IsValidCell(num2) && Grid.Solid[num2]))
			{
				zero = Vector2.zero;
			}
			else
			{
				position.y += 0.5f;
				zero = new Vector2(UnityEngine.Random.Range(TreeClimbStates.Instance.VEL_MIN.x, TreeClimbStates.Instance.VEL_MAX.x), UnityEngine.Random.Range(TreeClimbStates.Instance.VEL_MIN.y, TreeClimbStates.Instance.VEL_MAX.y));
			}
			ore_go.transform.SetPosition(position);
			if (GameComps.Fallers.Has(ore_go))
			{
				GameComps.Fallers.Remove(ore_go);
			}
			GameComps.Fallers.Add(ore_go, zero);
		}

		// Token: 0x04005226 RID: 21030
		private Storage storage;

		// Token: 0x04005227 RID: 21031
		private static readonly Vector2 VEL_MIN = new Vector2(-1f, 2f);

		// Token: 0x04005228 RID: 21032
		private static readonly Vector2 VEL_MAX = new Vector2(1f, 4f);
	}

	// Token: 0x02000EBA RID: 3770
	public class ClimbState : GameStateMachine<TreeClimbStates, TreeClimbStates.Instance, IStateMachineTarget, TreeClimbStates.Def>.State
	{
		// Token: 0x04005229 RID: 21033
		public GameStateMachine<TreeClimbStates, TreeClimbStates.Instance, IStateMachineTarget, TreeClimbStates.Def>.State pre;

		// Token: 0x0400522A RID: 21034
		public GameStateMachine<TreeClimbStates, TreeClimbStates.Instance, IStateMachineTarget, TreeClimbStates.Def>.State loop;

		// Token: 0x0400522B RID: 21035
		public GameStateMachine<TreeClimbStates, TreeClimbStates.Instance, IStateMachineTarget, TreeClimbStates.Def>.State pst;
	}
}
