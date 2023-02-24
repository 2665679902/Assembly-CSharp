using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class SeedPlantingStates : GameStateMachine<SeedPlantingStates, SeedPlantingStates.Instance, IStateMachineTarget, SeedPlantingStates.Def>
{
	// Token: 0x060003C3 RID: 963 RVA: 0x0001D008 File Offset: 0x0001B208
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.findSeed;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.PLANTINGSEED.NAME, CREATURES.STATUSITEMS.PLANTINGSEED.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).Exit(new StateMachine<SeedPlantingStates, SeedPlantingStates.Instance, IStateMachineTarget, SeedPlantingStates.Def>.State.Callback(SeedPlantingStates.UnreserveSeed)).Exit(new StateMachine<SeedPlantingStates, SeedPlantingStates.Instance, IStateMachineTarget, SeedPlantingStates.Def>.State.Callback(SeedPlantingStates.DropAll))
			.Exit(new StateMachine<SeedPlantingStates, SeedPlantingStates.Instance, IStateMachineTarget, SeedPlantingStates.Def>.State.Callback(SeedPlantingStates.RemoveMouthOverride));
		this.findSeed.Enter(delegate(SeedPlantingStates.Instance smi)
		{
			SeedPlantingStates.FindSeed(smi);
			if (smi.targetSeed == null)
			{
				smi.GoTo(this.behaviourcomplete);
				return;
			}
			SeedPlantingStates.ReserveSeed(smi);
			smi.GoTo(this.moveToSeed);
		});
		this.moveToSeed.MoveTo(new Func<SeedPlantingStates.Instance, int>(SeedPlantingStates.GetSeedCell), this.findPlantLocation, this.behaviourcomplete, false);
		this.findPlantLocation.Enter(delegate(SeedPlantingStates.Instance smi)
		{
			if (!smi.targetSeed)
			{
				smi.GoTo(this.behaviourcomplete);
				return;
			}
			SeedPlantingStates.FindDirtPlot(smi);
			if (smi.targetPlot != null || smi.targetDirtPlotCell != Grid.InvalidCell)
			{
				smi.GoTo(this.pickupSeed);
				return;
			}
			smi.GoTo(this.behaviourcomplete);
		});
		this.pickupSeed.PlayAnim("gather").Enter(new StateMachine<SeedPlantingStates, SeedPlantingStates.Instance, IStateMachineTarget, SeedPlantingStates.Def>.State.Callback(SeedPlantingStates.PickupComplete)).OnAnimQueueComplete(this.moveToPlantLocation);
		this.moveToPlantLocation.Enter(delegate(SeedPlantingStates.Instance smi)
		{
			if (smi.targetSeed == null)
			{
				smi.GoTo(this.behaviourcomplete);
				return;
			}
			if (smi.targetPlot != null)
			{
				smi.GoTo(this.moveToPlot);
				return;
			}
			if (smi.targetDirtPlotCell != Grid.InvalidCell)
			{
				smi.GoTo(this.moveToDirt);
				return;
			}
			smi.GoTo(this.behaviourcomplete);
		});
		this.moveToDirt.MoveTo((SeedPlantingStates.Instance smi) => smi.targetDirtPlotCell, this.planting, this.behaviourcomplete, false);
		this.moveToPlot.Enter(delegate(SeedPlantingStates.Instance smi)
		{
			if (smi.targetPlot == null || smi.targetSeed == null)
			{
				smi.GoTo(this.behaviourcomplete);
			}
		}).MoveTo(new Func<SeedPlantingStates.Instance, int>(SeedPlantingStates.GetPlantableCell), this.planting, this.behaviourcomplete, false);
		this.planting.Enter(new StateMachine<SeedPlantingStates, SeedPlantingStates.Instance, IStateMachineTarget, SeedPlantingStates.Def>.State.Callback(SeedPlantingStates.RemoveMouthOverride)).PlayAnim("plant").Exit(new StateMachine<SeedPlantingStates, SeedPlantingStates.Instance, IStateMachineTarget, SeedPlantingStates.Def>.State.Callback(SeedPlantingStates.PlantComplete))
			.OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.WantsToPlantSeed, false);
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x0001D1F0 File Offset: 0x0001B3F0
	private static void AddMouthOverride(SeedPlantingStates.Instance smi)
	{
		SymbolOverrideController component = smi.GetComponent<SymbolOverrideController>();
		KAnim.Build.Symbol symbol = smi.GetComponent<KBatchedAnimController>().AnimFiles[0].GetData().build.GetSymbol(smi.def.prefix + "sq_mouth_cheeks");
		if (symbol != null)
		{
			component.AddSymbolOverride("sq_mouth", symbol, 1);
		}
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x0001D251 File Offset: 0x0001B451
	private static void RemoveMouthOverride(SeedPlantingStates.Instance smi)
	{
		smi.GetComponent<SymbolOverrideController>().TryRemoveSymbolOverride("sq_mouth", 1);
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x0001D26C File Offset: 0x0001B46C
	private static void PickupComplete(SeedPlantingStates.Instance smi)
	{
		if (!smi.targetSeed)
		{
			global::Debug.LogWarningFormat("PickupComplete seed {0} is null", new object[] { smi.targetSeed });
			return;
		}
		SeedPlantingStates.UnreserveSeed(smi);
		int num = Grid.PosToCell(smi.targetSeed);
		if (smi.seed_cell != num)
		{
			global::Debug.LogWarningFormat("PickupComplete seed {0} moved {1} != {2}", new object[] { smi.targetSeed, num, smi.seed_cell });
			smi.targetSeed = null;
			return;
		}
		if (smi.targetSeed.HasTag(GameTags.Stored))
		{
			global::Debug.LogWarningFormat("PickupComplete seed {0} was stored by {1}", new object[]
			{
				smi.targetSeed,
				smi.targetSeed.storage
			});
			smi.targetSeed = null;
			return;
		}
		smi.targetSeed = EntitySplitter.Split(smi.targetSeed, 1f, null);
		smi.GetComponent<Storage>().Store(smi.targetSeed.gameObject, false, false, true, false);
		SeedPlantingStates.AddMouthOverride(smi);
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x0001D36C File Offset: 0x0001B56C
	private static void PlantComplete(SeedPlantingStates.Instance smi)
	{
		PlantableSeed plantableSeed = (smi.targetSeed ? smi.targetSeed.GetComponent<PlantableSeed>() : null);
		PlantablePlot plantablePlot;
		if (plantableSeed && SeedPlantingStates.CheckValidPlotCell(smi, plantableSeed, smi.targetDirtPlotCell, out plantablePlot))
		{
			if (plantablePlot)
			{
				if (plantablePlot.Occupant == null)
				{
					plantablePlot.ForceDeposit(smi.targetSeed.gameObject);
				}
			}
			else
			{
				plantableSeed.TryPlant(true);
			}
		}
		smi.targetSeed = null;
		smi.seed_cell = Grid.InvalidCell;
		smi.targetPlot = null;
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x0001D3F8 File Offset: 0x0001B5F8
	private static void DropAll(SeedPlantingStates.Instance smi)
	{
		smi.GetComponent<Storage>().DropAll(false, false, default(Vector3), true, null);
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x0001D420 File Offset: 0x0001B620
	private static int GetPlantableCell(SeedPlantingStates.Instance smi)
	{
		int num = Grid.PosToCell(smi.targetPlot);
		if (Grid.IsValidCell(num))
		{
			return Grid.CellAbove(num);
		}
		return num;
	}

	// Token: 0x060003CA RID: 970 RVA: 0x0001D44C File Offset: 0x0001B64C
	private static void FindDirtPlot(SeedPlantingStates.Instance smi)
	{
		smi.targetDirtPlotCell = Grid.InvalidCell;
		PlantableSeed component = smi.targetSeed.GetComponent<PlantableSeed>();
		PlantableCellQuery plantableCellQuery = PathFinderQueries.plantableCellQuery.Reset(component, 20);
		smi.GetComponent<Navigator>().RunQuery(plantableCellQuery);
		if (plantableCellQuery.result_cells.Count > 0)
		{
			smi.targetDirtPlotCell = plantableCellQuery.result_cells[UnityEngine.Random.Range(0, plantableCellQuery.result_cells.Count)];
		}
	}

	// Token: 0x060003CB RID: 971 RVA: 0x0001D4BC File Offset: 0x0001B6BC
	private static bool CheckValidPlotCell(SeedPlantingStates.Instance smi, PlantableSeed seed, int cell, out PlantablePlot plot)
	{
		plot = null;
		if (!Grid.IsValidCell(cell))
		{
			return false;
		}
		int num;
		if (seed.Direction == SingleEntityReceptacle.ReceptacleDirection.Bottom)
		{
			num = Grid.CellAbove(cell);
		}
		else
		{
			num = Grid.CellBelow(cell);
		}
		if (!Grid.IsValidCell(num))
		{
			return false;
		}
		if (!Grid.Solid[num])
		{
			return false;
		}
		GameObject gameObject = Grid.Objects[num, 1];
		if (gameObject)
		{
			plot = gameObject.GetComponent<PlantablePlot>();
			return plot != null;
		}
		return seed.TestSuitableGround(cell);
	}

	// Token: 0x060003CC RID: 972 RVA: 0x0001D535 File Offset: 0x0001B735
	private static int GetSeedCell(SeedPlantingStates.Instance smi)
	{
		global::Debug.Assert(smi.targetSeed);
		global::Debug.Assert(smi.seed_cell != Grid.InvalidCell);
		return smi.seed_cell;
	}

	// Token: 0x060003CD RID: 973 RVA: 0x0001D564 File Offset: 0x0001B764
	private static void FindSeed(SeedPlantingStates.Instance smi)
	{
		Navigator component = smi.GetComponent<Navigator>();
		Pickupable pickupable = null;
		int num = 100;
		foreach (object obj in Components.PlantableSeeds)
		{
			PlantableSeed plantableSeed = (PlantableSeed)obj;
			if ((plantableSeed.HasTag(GameTags.Seed) || plantableSeed.HasTag(GameTags.CropSeed)) && !plantableSeed.HasTag(GameTags.Creatures.ReservedByCreature) && Vector2.Distance(smi.transform.position, plantableSeed.transform.position) <= 25f)
			{
				int navigationCost = component.GetNavigationCost(Grid.PosToCell(plantableSeed));
				if (navigationCost != -1 && navigationCost < num)
				{
					pickupable = plantableSeed.GetComponent<Pickupable>();
					num = navigationCost;
				}
			}
		}
		smi.targetSeed = pickupable;
		smi.seed_cell = (smi.targetSeed ? Grid.PosToCell(smi.targetSeed) : Grid.InvalidCell);
	}

	// Token: 0x060003CE RID: 974 RVA: 0x0001D674 File Offset: 0x0001B874
	private static void ReserveSeed(SeedPlantingStates.Instance smi)
	{
		GameObject gameObject = (smi.targetSeed ? smi.targetSeed.gameObject : null);
		if (gameObject != null)
		{
			DebugUtil.Assert(!gameObject.HasTag(GameTags.Creatures.ReservedByCreature));
			gameObject.AddTag(GameTags.Creatures.ReservedByCreature);
		}
	}

	// Token: 0x060003CF RID: 975 RVA: 0x0001D6C4 File Offset: 0x0001B8C4
	private static void UnreserveSeed(SeedPlantingStates.Instance smi)
	{
		GameObject gameObject = (smi.targetSeed ? smi.targetSeed.gameObject : null);
		if (smi.targetSeed != null)
		{
			gameObject.RemoveTag(GameTags.Creatures.ReservedByCreature);
		}
	}

	// Token: 0x0400026C RID: 620
	private const int MAX_NAVIGATE_DISTANCE = 100;

	// Token: 0x0400026D RID: 621
	public GameStateMachine<SeedPlantingStates, SeedPlantingStates.Instance, IStateMachineTarget, SeedPlantingStates.Def>.State findSeed;

	// Token: 0x0400026E RID: 622
	public GameStateMachine<SeedPlantingStates, SeedPlantingStates.Instance, IStateMachineTarget, SeedPlantingStates.Def>.State moveToSeed;

	// Token: 0x0400026F RID: 623
	public GameStateMachine<SeedPlantingStates, SeedPlantingStates.Instance, IStateMachineTarget, SeedPlantingStates.Def>.State pickupSeed;

	// Token: 0x04000270 RID: 624
	public GameStateMachine<SeedPlantingStates, SeedPlantingStates.Instance, IStateMachineTarget, SeedPlantingStates.Def>.State findPlantLocation;

	// Token: 0x04000271 RID: 625
	public GameStateMachine<SeedPlantingStates, SeedPlantingStates.Instance, IStateMachineTarget, SeedPlantingStates.Def>.State moveToPlantLocation;

	// Token: 0x04000272 RID: 626
	public GameStateMachine<SeedPlantingStates, SeedPlantingStates.Instance, IStateMachineTarget, SeedPlantingStates.Def>.State moveToPlot;

	// Token: 0x04000273 RID: 627
	public GameStateMachine<SeedPlantingStates, SeedPlantingStates.Instance, IStateMachineTarget, SeedPlantingStates.Def>.State moveToDirt;

	// Token: 0x04000274 RID: 628
	public GameStateMachine<SeedPlantingStates, SeedPlantingStates.Instance, IStateMachineTarget, SeedPlantingStates.Def>.State planting;

	// Token: 0x04000275 RID: 629
	public GameStateMachine<SeedPlantingStates, SeedPlantingStates.Instance, IStateMachineTarget, SeedPlantingStates.Def>.State behaviourcomplete;

	// Token: 0x02000EA6 RID: 3750
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06006CCC RID: 27852 RVA: 0x00298B6A File Offset: 0x00296D6A
		public Def(string prefix)
		{
			this.prefix = prefix;
		}

		// Token: 0x040051E7 RID: 20967
		public string prefix;
	}

	// Token: 0x02000EA7 RID: 3751
	public new class Instance : GameStateMachine<SeedPlantingStates, SeedPlantingStates.Instance, IStateMachineTarget, SeedPlantingStates.Def>.GameInstance
	{
		// Token: 0x06006CCD RID: 27853 RVA: 0x00298B7C File Offset: 0x00296D7C
		public Instance(Chore<SeedPlantingStates.Instance> chore, SeedPlantingStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsToPlantSeed);
		}

		// Token: 0x040051E8 RID: 20968
		public PlantablePlot targetPlot;

		// Token: 0x040051E9 RID: 20969
		public int targetDirtPlotCell = Grid.InvalidCell;

		// Token: 0x040051EA RID: 20970
		public Element plantElement = ElementLoader.FindElementByHash(SimHashes.Dirt);

		// Token: 0x040051EB RID: 20971
		public Pickupable targetSeed;

		// Token: 0x040051EC RID: 20972
		public int seed_cell = Grid.InvalidCell;
	}
}
