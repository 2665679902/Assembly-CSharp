using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020005A5 RID: 1445
public class DevAutoPlumber
{
	// Token: 0x0600239E RID: 9118 RVA: 0x000C055F File Offset: 0x000BE75F
	public static void AutoPlumbBuilding(Building building)
	{
		DevAutoPlumber.DoElectricalPlumbing(building);
		DevAutoPlumber.DoLiquidAndGasPlumbing(building);
		DevAutoPlumber.SetupSolidOreDelivery(building);
	}

	// Token: 0x0600239F RID: 9119 RVA: 0x000C0574 File Offset: 0x000BE774
	public static void DoElectricalPlumbing(Building building)
	{
		if (!building.Def.RequiresPowerInput)
		{
			return;
		}
		int num = Grid.OffsetCell(Grid.PosToCell(building), building.Def.PowerInputOffset);
		GameObject gameObject = Grid.Objects[num, 26];
		if (gameObject != null)
		{
			gameObject.Trigger(-790448070, null);
		}
		DevAutoPlumber.PlaceSourceAndUtilityConduit(building, Assets.GetBuildingDef("DevGenerator"), Assets.GetBuildingDef("WireRefined"), Game.Instance.electricalConduitSystem, new int[] { 26, 29 }, DevAutoPlumber.PortSelection.PowerInput);
	}

	// Token: 0x060023A0 RID: 9120 RVA: 0x000C05FF File Offset: 0x000BE7FF
	public static void DoLiquidAndGasPlumbing(Building building)
	{
		DevAutoPlumber.SetupPlumbingInput(building);
		DevAutoPlumber.SetupPlumbingOutput(building);
	}

	// Token: 0x060023A1 RID: 9121 RVA: 0x000C0610 File Offset: 0x000BE810
	public static void SetupSolidOreDelivery(Building building)
	{
		ManualDeliveryKG component = building.GetComponent<ManualDeliveryKG>();
		if (component != null)
		{
			DevAutoPlumber.TrySpawnElementOreFromTag(component.RequestedItemTag, Grid.PosToCell(building), component.Capacity) == null;
			return;
		}
		foreach (ComplexRecipe complexRecipe in ComplexRecipeManager.Get().recipes)
		{
			using (List<Tag>.Enumerator enumerator2 = complexRecipe.fabricators.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current == building.Def.PrefabID)
					{
						foreach (ComplexRecipe.RecipeElement recipeElement in complexRecipe.ingredients)
						{
							DevAutoPlumber.TrySpawnElementOreFromTag(recipeElement.material, Grid.PosToCell(building), recipeElement.amount * 10f) == null;
						}
					}
				}
			}
		}
	}

	// Token: 0x060023A2 RID: 9122 RVA: 0x000C0730 File Offset: 0x000BE930
	private static GameObject TrySpawnElementOreFromTag(Tag t, int cell, float amount)
	{
		Element element = ElementLoader.GetElement(t);
		if (element == null)
		{
			element = ElementLoader.elements.Find((Element match) => match.HasTag(t));
		}
		if (element != null)
		{
			return element.substance.SpawnResource(Grid.CellToPos(cell), amount, element.defaultValues.temperature, byte.MaxValue, 0, false, false, false);
		}
		return null;
	}

	// Token: 0x060023A3 RID: 9123 RVA: 0x000C079C File Offset: 0x000BE99C
	private static void SetupPlumbingInput(Building building)
	{
		ConduitConsumer component = building.GetComponent<ConduitConsumer>();
		if (component == null)
		{
			return;
		}
		BuildingDef buildingDef = null;
		BuildingDef buildingDef2 = null;
		int[] array = null;
		UtilityNetworkManager<FlowUtilityNetwork, Vent> utilityNetworkManager = null;
		ConduitType conduitType = component.ConduitType;
		if (conduitType != ConduitType.Gas)
		{
			if (conduitType == ConduitType.Liquid)
			{
				buildingDef2 = Assets.GetBuildingDef("InsulatedLiquidConduit");
				buildingDef = Assets.GetBuildingDef("DevPumpLiquid");
				utilityNetworkManager = Game.Instance.liquidConduitSystem;
				array = new int[] { 16, 19 };
			}
		}
		else
		{
			buildingDef2 = Assets.GetBuildingDef("InsulatedGasConduit");
			buildingDef = Assets.GetBuildingDef("DevPumpGas");
			utilityNetworkManager = Game.Instance.gasConduitSystem;
			array = new int[] { 12, 15 };
		}
		GameObject gameObject = DevAutoPlumber.PlaceSourceAndUtilityConduit(building, buildingDef, buildingDef2, utilityNetworkManager, array, DevAutoPlumber.PortSelection.UtilityInput);
		Element element = DevAutoPlumber.GuessMostRelevantElementForPump(building);
		if (element != null)
		{
			gameObject.GetComponent<DevPump>().SelectedTag = element.tag;
			return;
		}
		gameObject.GetComponent<DevPump>().SelectedTag = ElementLoader.FindElementByHash(SimHashes.Vacuum).tag;
	}

	// Token: 0x060023A4 RID: 9124 RVA: 0x000C0888 File Offset: 0x000BEA88
	private static void SetupPlumbingOutput(Building building)
	{
		ConduitDispenser component = building.GetComponent<ConduitDispenser>();
		if (component == null)
		{
			return;
		}
		BuildingDef buildingDef = null;
		BuildingDef buildingDef2 = null;
		int[] array = null;
		UtilityNetworkManager<FlowUtilityNetwork, Vent> utilityNetworkManager = null;
		ConduitType conduitType = component.ConduitType;
		if (conduitType != ConduitType.Gas)
		{
			if (conduitType == ConduitType.Liquid)
			{
				buildingDef2 = Assets.GetBuildingDef("InsulatedLiquidConduit");
				buildingDef = Assets.GetBuildingDef("LiquidVent");
				utilityNetworkManager = Game.Instance.liquidConduitSystem;
				array = new int[] { 16, 19 };
			}
		}
		else
		{
			buildingDef2 = Assets.GetBuildingDef("InsulatedGasConduit");
			buildingDef = Assets.GetBuildingDef("GasVent");
			utilityNetworkManager = Game.Instance.gasConduitSystem;
			array = new int[] { 12, 15 };
		}
		DevAutoPlumber.PlaceSourceAndUtilityConduit(building, buildingDef, buildingDef2, utilityNetworkManager, array, DevAutoPlumber.PortSelection.UtilityOutput);
	}

	// Token: 0x060023A5 RID: 9125 RVA: 0x000C0938 File Offset: 0x000BEB38
	private static Element GuessMostRelevantElementForPump(Building destinationBuilding)
	{
		ConduitConsumer consumer = destinationBuilding.GetComponent<ConduitConsumer>();
		Tag targetTag = destinationBuilding.GetComponent<ConduitConsumer>().capacityTag;
		ElementConverter elementConverter = destinationBuilding.GetComponent<ElementConverter>();
		ElementConsumer elementConsumer = destinationBuilding.GetComponent<ElementConsumer>();
		RocketEngineCluster rocketEngineCluster = destinationBuilding.GetComponent<RocketEngineCluster>();
		return ElementLoader.elements.Find(delegate(Element match)
		{
			if (elementConverter != null)
			{
				bool flag = false;
				for (int i = 0; i < elementConverter.consumedElements.Length; i++)
				{
					if (elementConverter.consumedElements[i].Tag == match.tag)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			else if (elementConsumer != null)
			{
				bool flag2 = false;
				if (ElementLoader.FindElementByHash(elementConsumer.elementToConsume).tag == match.tag)
				{
					flag2 = true;
				}
				if (!flag2)
				{
					return false;
				}
			}
			else if (rocketEngineCluster != null)
			{
				bool flag3 = false;
				if (rocketEngineCluster.fuelTag == match.tag)
				{
					flag3 = true;
				}
				if (!flag3)
				{
					return false;
				}
			}
			return (consumer.ConduitType != ConduitType.Liquid || match.IsLiquid) && (consumer.ConduitType != ConduitType.Gas || match.IsGas) && (match.HasTag(targetTag) || !(targetTag != GameTags.Any));
		});
	}

	// Token: 0x060023A6 RID: 9126 RVA: 0x000C09A4 File Offset: 0x000BEBA4
	private static GameObject PlaceSourceAndUtilityConduit(Building destinationBuilding, BuildingDef sourceDef, BuildingDef conduitDef, IUtilityNetworkMgr utlityNetworkManager, int[] conduitTypeLayers, DevAutoPlumber.PortSelection portSelection)
	{
		Building building = null;
		List<int> list = new List<int>();
		int num = DevAutoPlumber.FindClearPlacementLocation(Grid.PosToCell(destinationBuilding), new List<int>(conduitTypeLayers) { 1 }.ToArray(), list);
		bool flag = false;
		int num2 = 10;
		while (!flag)
		{
			num2--;
			building = DevAutoPlumber.PlaceConduitSourceBuilding(num, sourceDef);
			if (building == null)
			{
				return null;
			}
			List<int> list2 = DevAutoPlumber.GenerateClearConduitPath(building, destinationBuilding, conduitTypeLayers, portSelection);
			if (list2 == null)
			{
				list.Add(Grid.PosToCell(building));
				building.Trigger(-790448070, null);
			}
			else
			{
				flag = true;
				DevAutoPlumber.BuildConduits(list2, conduitDef, utlityNetworkManager);
			}
		}
		return building.gameObject;
	}

	// Token: 0x060023A7 RID: 9127 RVA: 0x000C0A40 File Offset: 0x000BEC40
	private static int FindClearPlacementLocation(int nearStartingCell, int[] placementBlockingObjectLayers, List<int> rejectLocations)
	{
		Func<int, object, bool> func = delegate(int test, object unusedData)
		{
			foreach (int num2 in new int[]
			{
				test,
				Grid.OffsetCell(test, 1, 0),
				Grid.OffsetCell(test, 1, -1),
				Grid.OffsetCell(test, 0, -1),
				Grid.OffsetCell(test, 0, 1),
				Grid.OffsetCell(test, 1, 1)
			})
			{
				if (!Grid.IsValidCell(num2))
				{
					return false;
				}
				if (Grid.Solid[num2])
				{
					return false;
				}
				if (Grid.ObjectLayers[1].ContainsKey(num2))
				{
					return false;
				}
				foreach (int num3 in placementBlockingObjectLayers)
				{
					if (Grid.ObjectLayers[num3].ContainsKey(num2))
					{
						return false;
					}
				}
				if (rejectLocations.Contains(test))
				{
					return false;
				}
			}
			return true;
		};
		int num = 20;
		return GameUtil.FloodFillFind<object>(func, null, nearStartingCell, num, false, false);
	}

	// Token: 0x060023A8 RID: 9128 RVA: 0x000C0A78 File Offset: 0x000BEC78
	private static List<int> GenerateClearConduitPath(Building sourceBuilding, Building destinationBuilding, int[] conduitTypeLayers, DevAutoPlumber.PortSelection portSelection)
	{
		new List<int>();
		if (sourceBuilding == null)
		{
			return null;
		}
		int conduitStart = -1;
		int conduitEnd = -1;
		switch (portSelection)
		{
		case DevAutoPlumber.PortSelection.UtilityInput:
			conduitStart = Grid.OffsetCell(Grid.PosToCell(sourceBuilding), sourceBuilding.Def.UtilityOutputOffset);
			conduitEnd = Grid.OffsetCell(Grid.PosToCell(destinationBuilding), destinationBuilding.Def.UtilityInputOffset);
			break;
		case DevAutoPlumber.PortSelection.UtilityOutput:
			conduitStart = Grid.OffsetCell(Grid.PosToCell(destinationBuilding), destinationBuilding.Def.UtilityOutputOffset);
			conduitEnd = Grid.OffsetCell(Grid.PosToCell(sourceBuilding), sourceBuilding.Def.UtilityInputOffset);
			break;
		case DevAutoPlumber.PortSelection.PowerInput:
			conduitStart = Grid.OffsetCell(Grid.PosToCell(sourceBuilding), sourceBuilding.Def.PowerOutputOffset);
			conduitEnd = Grid.OffsetCell(Grid.PosToCell(destinationBuilding), destinationBuilding.Def.PowerInputOffset);
			break;
		}
		return DevAutoPlumber.GetGridPath(conduitStart, conduitEnd, delegate(int cell)
		{
			if (!Grid.IsValidCell(cell))
			{
				return false;
			}
			foreach (int num in conduitTypeLayers)
			{
				GameObject gameObject = Grid.Objects[cell, num];
				bool flag = gameObject == sourceBuilding.gameObject || gameObject == destinationBuilding.gameObject;
				bool flag2 = cell == conduitEnd || cell == conduitStart;
				if (gameObject != null && (!flag || (flag && !flag2)))
				{
					return false;
				}
			}
			return true;
		}, 20);
	}

	// Token: 0x060023A9 RID: 9129 RVA: 0x000C0BE8 File Offset: 0x000BEDE8
	private static Building PlaceConduitSourceBuilding(int cell, BuildingDef def)
	{
		List<Tag> list = new List<Tag> { SimHashes.Cuprite.CreateTag() };
		return def.Build(cell, Orientation.Neutral, null, list, 273.15f, true, GameClock.Instance.GetTime()).GetComponent<Building>();
	}

	// Token: 0x060023AA RID: 9130 RVA: 0x000C0C2C File Offset: 0x000BEE2C
	private static void BuildConduits(List<int> path, BuildingDef conduitDef, object utilityNetwork)
	{
		List<Tag> list = new List<Tag> { SimHashes.Cuprite.CreateTag() };
		List<GameObject> list2 = new List<GameObject>();
		for (int i = 0; i < path.Count; i++)
		{
			list2.Add(conduitDef.Build(path[i], Orientation.Neutral, null, list, 273.15f, true, GameClock.Instance.GetTime()));
		}
		if (list2.Count < 2)
		{
			return;
		}
		IUtilityNetworkMgr utilityNetworkMgr = (IUtilityNetworkMgr)utilityNetwork;
		for (int j = 1; j < list2.Count; j++)
		{
			UtilityConnections utilityConnections = UtilityConnectionsExtensions.DirectionFromToCell(Grid.PosToCell(list2[j - 1]), Grid.PosToCell(list2[j]));
			utilityNetworkMgr.AddConnection(utilityConnections, Grid.PosToCell(list2[j - 1]), true);
			utilityNetworkMgr.AddConnection(utilityConnections.InverseDirection(), Grid.PosToCell(list2[j]), true);
			IUtilityItem component = list2[j].GetComponent<KAnimGraphTileVisualizer>();
			if (component != null)
			{
				component.UpdateConnections(utilityNetworkMgr.GetConnections(Grid.PosToCell(list2[j]), true));
			}
		}
	}

	// Token: 0x060023AB RID: 9131 RVA: 0x000C0D3C File Offset: 0x000BEF3C
	private static List<int> GetGridPath(int startCell, int endCell, Func<int, bool> testFunction, int maxDepth = 20)
	{
		DevAutoPlumber.<>c__DisplayClass14_0 CS$<>8__locals1;
		CS$<>8__locals1.testFunction = testFunction;
		CS$<>8__locals1.endCell = endCell;
		List<int> list = new List<int>();
		CS$<>8__locals1.frontier = new List<int>();
		CS$<>8__locals1.touched = new List<int>();
		CS$<>8__locals1.crumbs = new Dictionary<int, int>();
		CS$<>8__locals1.frontier.Add(startCell);
		CS$<>8__locals1.newFrontier = new List<int>();
		int num = 0;
		while (!CS$<>8__locals1.touched.Contains(CS$<>8__locals1.endCell))
		{
			num++;
			if (num > maxDepth || CS$<>8__locals1.frontier.Count == 0)
			{
				break;
			}
			foreach (int num2 in CS$<>8__locals1.frontier)
			{
				DevAutoPlumber.<GetGridPath>g___ExpandFrontier|14_0(num2, ref CS$<>8__locals1);
			}
			CS$<>8__locals1.frontier.Clear();
			foreach (int num3 in CS$<>8__locals1.newFrontier)
			{
				CS$<>8__locals1.frontier.Add(num3);
			}
			CS$<>8__locals1.newFrontier.Clear();
		}
		int num4 = CS$<>8__locals1.endCell;
		list.Add(num4);
		while (CS$<>8__locals1.crumbs.ContainsKey(num4))
		{
			num4 = CS$<>8__locals1.crumbs[num4];
			list.Add(num4);
		}
		list.Reverse();
		return list;
	}

	// Token: 0x060023AD RID: 9133 RVA: 0x000C0EBC File Offset: 0x000BF0BC
	[CompilerGenerated]
	internal static void <GetGridPath>g___ExpandFrontier|14_0(int fromCell, ref DevAutoPlumber.<>c__DisplayClass14_0 A_1)
	{
		foreach (int num in new int[]
		{
			Grid.CellAbove(fromCell),
			Grid.CellBelow(fromCell),
			Grid.CellLeft(fromCell),
			Grid.CellRight(fromCell)
		})
		{
			if (!A_1.newFrontier.Contains(num) && !A_1.frontier.Contains(num) && !A_1.touched.Contains(num) && A_1.testFunction(num))
			{
				A_1.newFrontier.Add(num);
				A_1.crumbs.Add(num, fromCell);
			}
			A_1.touched.Add(num);
			if (num == A_1.endCell)
			{
				break;
			}
		}
		A_1.touched.Add(fromCell);
	}

	// Token: 0x020011D9 RID: 4569
	private enum PortSelection
	{
		// Token: 0x04005C31 RID: 23601
		UtilityInput,
		// Token: 0x04005C32 RID: 23602
		UtilityOutput,
		// Token: 0x04005C33 RID: 23603
		PowerInput
	}
}
