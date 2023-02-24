using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000A7 RID: 167
public class BeeForageStates : GameStateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>
{
	// Token: 0x060002DD RID: 733 RVA: 0x00016FC8 File Offset: 0x000151C8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.collect.findTarget;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.FORAGINGMATERIAL.NAME, CREATURES.STATUSITEMS.FORAGINGMATERIAL.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).Exit(new StateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State.Callback(BeeForageStates.UnreserveTarget)).Exit(new StateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State.Callback(BeeForageStates.DropAll));
		this.collect.findTarget.Enter(delegate(BeeForageStates.Instance smi)
		{
			BeeForageStates.FindTarget(smi);
			smi.targetHive = smi.master.GetComponent<Bee>().FindHiveInRoom();
			if (smi.targetHive != null)
			{
				if (smi.forageTarget != null)
				{
					BeeForageStates.ReserveTarget(smi);
					smi.GoTo(this.collect.forage.moveToTarget);
					return;
				}
				if (Grid.IsValidCell(smi.targetMiningCell))
				{
					smi.GoTo(this.collect.mine.moveToTarget);
					return;
				}
			}
			smi.GoTo(this.behaviourcomplete);
		});
		this.collect.forage.moveToTarget.MoveTo(new Func<BeeForageStates.Instance, int>(BeeForageStates.GetOreCell), this.collect.forage.pickupTarget, this.behaviourcomplete, false);
		this.collect.forage.pickupTarget.PlayAnim("pickup_pre").Enter(new StateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State.Callback(BeeForageStates.PickupComplete)).OnAnimQueueComplete(this.storage.moveToHive);
		this.collect.mine.moveToTarget.MoveTo((BeeForageStates.Instance smi) => smi.targetMiningCell, this.collect.mine.mineTarget, this.behaviourcomplete, false);
		this.collect.mine.mineTarget.PlayAnim("mining_pre").QueueAnim("mining_loop", false, null).QueueAnim("mining_pst", false, null)
			.Enter(new StateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State.Callback(BeeForageStates.MineTarget))
			.OnAnimQueueComplete(this.storage.moveToHive);
		this.storage.Enter(new StateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State.Callback(this.HoldOre)).Exit(new StateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State.Callback(this.DropOre));
		this.storage.moveToHive.Enter(delegate(BeeForageStates.Instance smi)
		{
			if (!smi.targetHive)
			{
				smi.targetHive = smi.master.GetComponent<Bee>().FindHiveInRoom();
			}
			if (!smi.targetHive)
			{
				smi.GoTo(this.storage.dropMaterial);
			}
		}).MoveTo((BeeForageStates.Instance smi) => Grid.OffsetCell(Grid.PosToCell(smi.targetHive.transform.GetPosition()), smi.hiveCellOffset), this.storage.storeMaterial, this.behaviourcomplete, false);
		this.storage.storeMaterial.PlayAnim("deposit").Exit(new StateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State.Callback(BeeForageStates.StoreOre)).OnAnimQueueComplete(this.behaviourcomplete.pre);
		this.storage.dropMaterial.Enter(delegate(BeeForageStates.Instance smi)
		{
			smi.GoTo(this.behaviourcomplete);
		}).Exit(new StateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State.Callback(BeeForageStates.DropAll));
		this.behaviourcomplete.DefaultState(this.behaviourcomplete.pst);
		this.behaviourcomplete.pre.PlayAnim("spawn").OnAnimQueueComplete(this.behaviourcomplete.pst);
		this.behaviourcomplete.pst.BehaviourComplete(GameTags.Creatures.WantsToForage, false);
	}

	// Token: 0x060002DE RID: 734 RVA: 0x000172B2 File Offset: 0x000154B2
	private static void FindTarget(BeeForageStates.Instance smi)
	{
		if (BeeForageStates.FindOre(smi))
		{
			return;
		}
		BeeForageStates.FindMineableCell(smi);
	}

	// Token: 0x060002DF RID: 735 RVA: 0x000172C4 File Offset: 0x000154C4
	private void HoldOre(BeeForageStates.Instance smi)
	{
		GameObject gameObject = smi.GetComponent<Storage>().FindFirst(smi.def.oreTag);
		if (!gameObject)
		{
			return;
		}
		KBatchedAnimController component = smi.GetComponent<KBatchedAnimController>();
		KAnim.Build.Symbol symbol = gameObject.GetComponent<KBatchedAnimController>().CurrentAnim.animFile.build.symbols[0];
		component.GetComponent<SymbolOverrideController>().AddSymbolOverride(smi.oreSymbolHash, symbol, 5);
		component.SetSymbolVisiblity(smi.oreSymbolHash, true);
		component.SetSymbolVisiblity(smi.oreLegSymbolHash, true);
		component.SetSymbolVisiblity(smi.noOreLegSymbolHash, false);
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x00017353 File Offset: 0x00015553
	private void DropOre(BeeForageStates.Instance smi)
	{
		KBatchedAnimController component = smi.GetComponent<KBatchedAnimController>();
		component.SetSymbolVisiblity(smi.oreSymbolHash, false);
		component.SetSymbolVisiblity(smi.oreLegSymbolHash, false);
		component.SetSymbolVisiblity(smi.noOreLegSymbolHash, true);
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x00017384 File Offset: 0x00015584
	private static void PickupComplete(BeeForageStates.Instance smi)
	{
		if (!smi.forageTarget)
		{
			global::Debug.LogWarningFormat("PickupComplete forageTarget {0} is null", new object[] { smi.forageTarget });
			return;
		}
		BeeForageStates.UnreserveTarget(smi);
		int num = Grid.PosToCell(smi.forageTarget);
		if (smi.forageTarget_cell != num)
		{
			global::Debug.LogWarningFormat("PickupComplete forageTarget {0} moved {1} != {2}", new object[] { smi.forageTarget, num, smi.forageTarget_cell });
			smi.forageTarget = null;
			return;
		}
		if (smi.forageTarget.HasTag(GameTags.Stored))
		{
			global::Debug.LogWarningFormat("PickupComplete forageTarget {0} was stored by {1}", new object[]
			{
				smi.forageTarget,
				smi.forageTarget.storage
			});
			smi.forageTarget = null;
			return;
		}
		smi.forageTarget = EntitySplitter.Split(smi.forageTarget, 10f, null);
		smi.GetComponent<Storage>().Store(smi.forageTarget.gameObject, false, false, true, false);
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x00017480 File Offset: 0x00015680
	private static void MineTarget(BeeForageStates.Instance smi)
	{
		Storage storage = smi.master.GetComponent<Storage>();
		HandleVector<Game.ComplexCallbackInfo<Sim.MassConsumedCallback>>.Handle handle = Game.Instance.massConsumedCallbackManager.Add(delegate(Sim.MassConsumedCallback mass_cb_info, object data)
		{
			if (mass_cb_info.mass > 0f)
			{
				storage.AddOre(ElementLoader.elements[(int)mass_cb_info.elemIdx].id, mass_cb_info.mass, mass_cb_info.temperature, mass_cb_info.diseaseIdx, mass_cb_info.diseaseCount, false, true);
			}
		}, null, "BeetaMine");
		SimMessages.ConsumeMass(smi.cellToMine, Grid.Element[smi.cellToMine].id, smi.def.amountToMine, 1, handle.index);
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x000174F5 File Offset: 0x000156F5
	private static void StoreOre(BeeForageStates.Instance smi)
	{
		smi.master.GetComponent<Storage>().Transfer(smi.targetHive.GetComponent<Storage>(), false, false);
		smi.forageTarget = null;
		smi.forageTarget_cell = Grid.InvalidCell;
		smi.targetHive = null;
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x00017530 File Offset: 0x00015730
	private static void DropAll(BeeForageStates.Instance smi)
	{
		smi.GetComponent<Storage>().DropAll(false, false, default(Vector3), true, null);
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x00017558 File Offset: 0x00015758
	private static bool FindMineableCell(BeeForageStates.Instance smi)
	{
		smi.targetMiningCell = Grid.InvalidCell;
		MineableCellQuery mineableCellQuery = PathFinderQueries.mineableCellQuery.Reset(smi.def.oreTag, 20);
		smi.GetComponent<Navigator>().RunQuery(mineableCellQuery);
		if (mineableCellQuery.result_cells.Count > 0)
		{
			smi.targetMiningCell = mineableCellQuery.result_cells[UnityEngine.Random.Range(0, mineableCellQuery.result_cells.Count)];
			foreach (Direction direction in MineableCellQuery.DIRECTION_CHECKS)
			{
				int cellInDirection = Grid.GetCellInDirection(smi.targetMiningCell, direction);
				if (Grid.IsValidCell(cellInDirection) && Grid.IsSolidCell(cellInDirection) && Grid.Element[cellInDirection].tag == smi.def.oreTag)
				{
					smi.cellToMine = cellInDirection;
					return true;
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x00017650 File Offset: 0x00015850
	private static bool FindOre(BeeForageStates.Instance smi)
	{
		Navigator component = smi.GetComponent<Navigator>();
		Vector3 position = smi.transform.GetPosition();
		Pickupable pickupable = null;
		int num = 100;
		Extents extents = new Extents((int)position.x, (int)position.y, 15);
		ListPool<ScenePartitionerEntry, BeeForageStates>.PooledList pooledList = ListPool<ScenePartitionerEntry, BeeForageStates>.Allocate();
		GameScenePartitioner.Instance.GatherEntries(extents, GameScenePartitioner.Instance.pickupablesLayer, pooledList);
		Element element = ElementLoader.GetElement(smi.def.oreTag);
		foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
		{
			Pickupable pickupable2 = scenePartitionerEntry.obj as Pickupable;
			if (pickupable2 && pickupable2.GetComponent<ElementChunk>() && pickupable2.GetComponent<PrimaryElement>() && pickupable2.GetComponent<PrimaryElement>().Element == element && !pickupable2.HasTag(GameTags.Creatures.ReservedByCreature))
			{
				int navigationCost = component.GetNavigationCost(Grid.PosToCell(pickupable2));
				if (navigationCost != -1 && navigationCost < num)
				{
					pickupable = pickupable2.GetComponent<Pickupable>();
					num = navigationCost;
				}
			}
		}
		smi.forageTarget = pickupable;
		smi.forageTarget_cell = (smi.forageTarget ? Grid.PosToCell(smi.forageTarget) : Grid.InvalidCell);
		return smi.forageTarget != null;
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x000177A8 File Offset: 0x000159A8
	private static void ReserveTarget(BeeForageStates.Instance smi)
	{
		GameObject gameObject = (smi.forageTarget ? smi.forageTarget.gameObject : null);
		if (gameObject != null)
		{
			DebugUtil.Assert(!gameObject.HasTag(GameTags.Creatures.ReservedByCreature));
			gameObject.AddTag(GameTags.Creatures.ReservedByCreature);
		}
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x000177F8 File Offset: 0x000159F8
	private static void UnreserveTarget(BeeForageStates.Instance smi)
	{
		GameObject gameObject = (smi.forageTarget ? smi.forageTarget.gameObject : null);
		if (smi.forageTarget != null)
		{
			gameObject.RemoveTag(GameTags.Creatures.ReservedByCreature);
		}
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x0001783A File Offset: 0x00015A3A
	private static int GetOreCell(BeeForageStates.Instance smi)
	{
		global::Debug.Assert(smi.forageTarget);
		global::Debug.Assert(smi.forageTarget_cell != Grid.InvalidCell);
		return smi.forageTarget_cell;
	}

	// Token: 0x040001E6 RID: 486
	private const int MAX_NAVIGATE_DISTANCE = 100;

	// Token: 0x040001E7 RID: 487
	private const string oreSymbol = "snapto_thing";

	// Token: 0x040001E8 RID: 488
	private const string oreLegSymbol = "legBeeOre";

	// Token: 0x040001E9 RID: 489
	private const string noOreLegSymbol = "legBeeNoOre";

	// Token: 0x040001EA RID: 490
	public BeeForageStates.CollectionBehaviourStates collect;

	// Token: 0x040001EB RID: 491
	public BeeForageStates.StorageBehaviourStates storage;

	// Token: 0x040001EC RID: 492
	public BeeForageStates.ExitStates behaviourcomplete;

	// Token: 0x02000E04 RID: 3588
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06006B61 RID: 27489 RVA: 0x0029661E File Offset: 0x0029481E
		public Def(Tag tag, float amount_to_mine)
		{
			this.oreTag = tag;
			this.amountToMine = amount_to_mine;
		}

		// Token: 0x040050AE RID: 20654
		public Tag oreTag;

		// Token: 0x040050AF RID: 20655
		public float amountToMine;
	}

	// Token: 0x02000E05 RID: 3589
	public new class Instance : GameStateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.GameInstance
	{
		// Token: 0x06006B62 RID: 27490 RVA: 0x00296634 File Offset: 0x00294834
		public Instance(Chore<BeeForageStates.Instance> chore, BeeForageStates.Def def)
			: base(chore, def)
		{
			this.oreSymbolHash = new KAnimHashedString("snapto_thing");
			this.oreLegSymbolHash = new KAnimHashedString("legBeeOre");
			this.noOreLegSymbolHash = new KAnimHashedString("legBeeNoOre");
			base.smi.GetComponent<KBatchedAnimController>().SetSymbolVisiblity(base.smi.oreSymbolHash, false);
			base.smi.GetComponent<KBatchedAnimController>().SetSymbolVisiblity(base.smi.oreLegSymbolHash, false);
			base.smi.GetComponent<KBatchedAnimController>().SetSymbolVisiblity(base.smi.noOreLegSymbolHash, true);
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsToForage);
		}

		// Token: 0x040050B0 RID: 20656
		public int targetMiningCell = Grid.InvalidCell;

		// Token: 0x040050B1 RID: 20657
		public int cellToMine = Grid.InvalidCell;

		// Token: 0x040050B2 RID: 20658
		public Pickupable forageTarget;

		// Token: 0x040050B3 RID: 20659
		public int forageTarget_cell = Grid.InvalidCell;

		// Token: 0x040050B4 RID: 20660
		public KPrefabID targetHive;

		// Token: 0x040050B5 RID: 20661
		public KAnimHashedString oreSymbolHash;

		// Token: 0x040050B6 RID: 20662
		public KAnimHashedString oreLegSymbolHash;

		// Token: 0x040050B7 RID: 20663
		public KAnimHashedString noOreLegSymbolHash;

		// Token: 0x040050B8 RID: 20664
		public CellOffset hiveCellOffset = new CellOffset(1, 1);
	}

	// Token: 0x02000E06 RID: 3590
	public class ForageBehaviourStates : GameStateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State
	{
		// Token: 0x040050B9 RID: 20665
		public GameStateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State moveToTarget;

		// Token: 0x040050BA RID: 20666
		public GameStateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State pickupTarget;
	}

	// Token: 0x02000E07 RID: 3591
	public class MiningBehaviourStates : GameStateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State
	{
		// Token: 0x040050BB RID: 20667
		public GameStateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State moveToTarget;

		// Token: 0x040050BC RID: 20668
		public GameStateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State mineTarget;
	}

	// Token: 0x02000E08 RID: 3592
	public class CollectionBehaviourStates : GameStateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State
	{
		// Token: 0x040050BD RID: 20669
		public GameStateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State findTarget;

		// Token: 0x040050BE RID: 20670
		public BeeForageStates.ForageBehaviourStates forage;

		// Token: 0x040050BF RID: 20671
		public BeeForageStates.MiningBehaviourStates mine;
	}

	// Token: 0x02000E09 RID: 3593
	public class StorageBehaviourStates : GameStateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State
	{
		// Token: 0x040050C0 RID: 20672
		public GameStateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State moveToHive;

		// Token: 0x040050C1 RID: 20673
		public GameStateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State storeMaterial;

		// Token: 0x040050C2 RID: 20674
		public GameStateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State dropMaterial;
	}

	// Token: 0x02000E0A RID: 3594
	public class ExitStates : GameStateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State
	{
		// Token: 0x040050C3 RID: 20675
		public GameStateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State pre;

		// Token: 0x040050C4 RID: 20676
		public GameStateMachine<BeeForageStates, BeeForageStates.Instance, IStateMachineTarget, BeeForageStates.Def>.State pst;
	}
}
