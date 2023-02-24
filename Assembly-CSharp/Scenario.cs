using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Klei.AI;
using ProcGenGame;
using UnityEngine;

// Token: 0x02000900 RID: 2304
[AddComponentMenu("KMonoBehaviour/scripts/Scenario")]
public class Scenario : KMonoBehaviour
{
	// Token: 0x170004C6 RID: 1222
	// (get) Token: 0x060042D3 RID: 17107 RVA: 0x001798DB File Offset: 0x00177ADB
	// (set) Token: 0x060042D4 RID: 17108 RVA: 0x001798E3 File Offset: 0x00177AE3
	public bool[] ReplaceElementMask { get; set; }

	// Token: 0x060042D5 RID: 17109 RVA: 0x001798EC File Offset: 0x00177AEC
	public static void DestroyInstance()
	{
		Scenario.Instance = null;
	}

	// Token: 0x060042D6 RID: 17110 RVA: 0x001798F4 File Offset: 0x00177AF4
	protected override void OnPrefabInit()
	{
		Scenario.Instance = this;
		SaveLoader instance = SaveLoader.Instance;
		instance.OnWorldGenComplete = (Action<Cluster>)Delegate.Combine(instance.OnWorldGenComplete, new Action<Cluster>(this.OnWorldGenComplete));
	}

	// Token: 0x060042D7 RID: 17111 RVA: 0x00179922 File Offset: 0x00177B22
	private void OnWorldGenComplete(Cluster clusterLayout)
	{
		this.Init();
	}

	// Token: 0x060042D8 RID: 17112 RVA: 0x0017992C File Offset: 0x00177B2C
	private void Init()
	{
		this.Bot = Grid.HeightInCells / 4;
		this.Left = 150;
		this.RootCell = Grid.OffsetCell(0, this.Left, this.Bot);
		this.ReplaceElementMask = new bool[Grid.CellCount];
	}

	// Token: 0x060042D9 RID: 17113 RVA: 0x0017997C File Offset: 0x00177B7C
	private void DigHole(int x, int y, int width, int height)
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				SimMessages.ReplaceElement(Grid.OffsetCell(this.RootCell, x + i, y + j), SimHashes.Oxygen, CellEventLogger.Instance.Scenario, 200f, -1f, byte.MaxValue, 0, -1);
				SimMessages.ReplaceElement(Grid.OffsetCell(this.RootCell, x, y + j), SimHashes.Ice, CellEventLogger.Instance.Scenario, 1000f, -1f, byte.MaxValue, 0, -1);
				SimMessages.ReplaceElement(Grid.OffsetCell(this.RootCell, x + width, y + j), SimHashes.Ice, CellEventLogger.Instance.Scenario, 1000f, -1f, byte.MaxValue, 0, -1);
			}
			SimMessages.ReplaceElement(Grid.OffsetCell(this.RootCell, x + i, y - 1), SimHashes.Ice, CellEventLogger.Instance.Scenario, 1000f, -1f, byte.MaxValue, 0, -1);
			SimMessages.ReplaceElement(Grid.OffsetCell(this.RootCell, x + i, y + height), SimHashes.Ice, CellEventLogger.Instance.Scenario, 1000f, -1f, byte.MaxValue, 0, -1);
		}
	}

	// Token: 0x060042DA RID: 17114 RVA: 0x00179ABB File Offset: 0x00177CBB
	private void Fill(int x, int y, SimHashes id = SimHashes.Ice)
	{
		SimMessages.ReplaceElement(Grid.OffsetCell(this.RootCell, x, y), id, CellEventLogger.Instance.Scenario, 10000f, -1f, byte.MaxValue, 0, -1);
	}

	// Token: 0x060042DB RID: 17115 RVA: 0x00179AEC File Offset: 0x00177CEC
	private void PlaceColumn(int x, int y, int height)
	{
		for (int i = 0; i < height; i++)
		{
			SimMessages.ReplaceElement(Grid.OffsetCell(this.RootCell, x, y + i), SimHashes.Ice, CellEventLogger.Instance.Scenario, 10000f, -1f, byte.MaxValue, 0, -1);
		}
	}

	// Token: 0x060042DC RID: 17116 RVA: 0x00179B3C File Offset: 0x00177D3C
	private void PlaceTileX(int left, int bot, int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			this.PlaceBuilding(left + i, bot, "Tile", SimHashes.Cuprite);
		}
	}

	// Token: 0x060042DD RID: 17117 RVA: 0x00179B6C File Offset: 0x00177D6C
	private void PlaceTileY(int left, int bot, int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			this.PlaceBuilding(left, bot + i, "Tile", SimHashes.Cuprite);
		}
	}

	// Token: 0x060042DE RID: 17118 RVA: 0x00179B9A File Offset: 0x00177D9A
	private void Clear(int x, int y)
	{
		SimMessages.ReplaceElement(Grid.OffsetCell(this.RootCell, x, y), SimHashes.Oxygen, CellEventLogger.Instance.Scenario, 10000f, -1f, byte.MaxValue, 0, -1);
	}

	// Token: 0x060042DF RID: 17119 RVA: 0x00179BD0 File Offset: 0x00177DD0
	private void PlacerLadder(int x, int y, int amount)
	{
		int num = 1;
		if (amount < 0)
		{
			amount = -amount;
			num = -1;
		}
		for (int i = 0; i < amount; i++)
		{
			this.PlaceBuilding(x, y + i * num, "Ladder", SimHashes.Cuprite);
		}
	}

	// Token: 0x060042E0 RID: 17120 RVA: 0x00179C0C File Offset: 0x00177E0C
	private void PlaceBuildings(int left, int bot)
	{
		this.PlaceBuilding(++left, bot, "ManualGenerator", SimHashes.Iron);
		this.PlaceBuilding(left += 2, bot, "OxygenMachine", SimHashes.Steel);
		this.PlaceBuilding(left += 2, bot, "SpaceHeater", SimHashes.Steel);
		this.PlaceBuilding(++left, bot, "Electrolyzer", SimHashes.Steel);
		this.PlaceBuilding(++left, bot, "Smelter", SimHashes.Steel);
		this.SpawnOre(left, bot + 1, SimHashes.Ice);
	}

	// Token: 0x060042E1 RID: 17121 RVA: 0x00179CA0 File Offset: 0x00177EA0
	private IEnumerator TurnOn(GameObject go)
	{
		yield return null;
		yield return null;
		go.GetComponent<BuildingEnabledButton>().IsEnabled = true;
		yield break;
	}

	// Token: 0x060042E2 RID: 17122 RVA: 0x00179CB0 File Offset: 0x00177EB0
	private void SetupPlacerTest(Scenario.Builder b, Element element)
	{
		foreach (BuildingDef buildingDef in Assets.BuildingDefs)
		{
			if (buildingDef.Name != "Excavator")
			{
				b.Placer(buildingDef.PrefabID, element);
			}
		}
		b.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x060042E3 RID: 17123 RVA: 0x00179D2C File Offset: 0x00177F2C
	private void SetupBuildingTest(Scenario.RowLayout row_layout, bool is_powered, bool break_building)
	{
		Scenario.Builder builder = null;
		int num = 0;
		foreach (BuildingDef buildingDef in Assets.BuildingDefs)
		{
			if (builder == null)
			{
				builder = row_layout.NextRow();
				num = this.Left;
				if (is_powered)
				{
					builder.Minion(null);
					builder.Minion(null);
				}
			}
			if (buildingDef.Name != "Excavator")
			{
				GameObject gameObject = builder.Building(buildingDef.PrefabID);
				if (break_building)
				{
					BuildingHP component = gameObject.GetComponent<BuildingHP>();
					if (component != null)
					{
						component.DoDamage(int.MaxValue);
					}
				}
			}
			if (builder.Left > num + 100)
			{
				builder.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
				builder = null;
			}
		}
		builder.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x060042E4 RID: 17124 RVA: 0x00179E18 File Offset: 0x00178018
	private IEnumerator RunAfterNextUpdateRoutine(System.Action action)
	{
		yield return null;
		yield return null;
		yield return null;
		yield return null;
		yield return null;
		yield return null;
		action();
		yield break;
	}

	// Token: 0x060042E5 RID: 17125 RVA: 0x00179E27 File Offset: 0x00178027
	private void RunAfterNextUpdate(System.Action action)
	{
		base.StartCoroutine(this.RunAfterNextUpdateRoutine(action));
	}

	// Token: 0x060042E6 RID: 17126 RVA: 0x00179E38 File Offset: 0x00178038
	public void SetupFabricatorTest(Scenario.Builder b)
	{
		b.Minion(null);
		b.Building("ManualGenerator");
		b.Ore(3, SimHashes.Cuprite);
		b.Minion(null);
		b.Building("Masonry");
		b.InAndOuts();
		b.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x060042E7 RID: 17127 RVA: 0x00179E8E File Offset: 0x0017808E
	public void SetupDoorTest(Scenario.Builder b)
	{
		b.Minion(null);
		b.Jump(1, 0);
		b.Building("Door");
		b.Building("ManualGenerator");
		b.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x060042E8 RID: 17128 RVA: 0x00179EC8 File Offset: 0x001780C8
	public void SetupHatchTest(Scenario.Builder b)
	{
		b.Minion(null);
		b.Building("Door");
		b.Building("ManualGenerator");
		b.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x060042E9 RID: 17129 RVA: 0x00179EFA File Offset: 0x001780FA
	public void SetupPropaneGeneratorTest(Scenario.Builder b)
	{
		b.Building("PropaneGenerator");
		b.Building("OxygenMachine");
		b.FinalizeRoom(SimHashes.Propane, SimHashes.Steel);
	}

	// Token: 0x060042EA RID: 17130 RVA: 0x00179F28 File Offset: 0x00178128
	public void SetupAirLockTest(Scenario.Builder b)
	{
		b.Minion(null);
		b.Jump(1, 0);
		b.Minion(null);
		b.Jump(1, 0);
		b.Building("PoweredAirlock");
		b.Building("ManualGenerator");
		b.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x060042EB RID: 17131 RVA: 0x00179F7C File Offset: 0x0017817C
	public void SetupBedTest(Scenario.Builder b)
	{
		b.Minion(delegate(GameObject go)
		{
			go.GetAmounts().SetValue("Stamina", 10f);
		});
		b.Building("ManualGenerator");
		b.Minion(delegate(GameObject go)
		{
			go.GetAmounts().SetValue("Stamina", 10f);
		});
		b.Building("ComfyBed");
		b.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x060042EC RID: 17132 RVA: 0x00179FFC File Offset: 0x001781FC
	public void SetupHexapedTest(Scenario.Builder b)
	{
		b.Fill(4, 4, SimHashes.Oxygen);
		b.Prefab("Hexaped", null);
		b.Jump(2, 0);
		b.Ore(1, SimHashes.Iron);
		b.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x060042ED RID: 17133 RVA: 0x0017A03C File Offset: 0x0017823C
	public void SetupElectrolyzerTest(Scenario.Builder b)
	{
		b.Minion(null);
		b.Building("ManualGenerator");
		b.Ore(3, SimHashes.Ice);
		b.Minion(null);
		b.Building("Electrolyzer");
		b.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x060042EE RID: 17134 RVA: 0x0017A08C File Offset: 0x0017828C
	public void SetupOrePerformanceTest(Scenario.Builder b)
	{
		int num = 20;
		int num2 = 20;
		int left = b.Left;
		int bot = b.Bot;
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j += 2)
			{
				b.Jump(i, j);
				b.Ore(1, SimHashes.Cuprite);
				b.JumpTo(left, bot);
			}
		}
		b.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x060042EF RID: 17135 RVA: 0x0017A0FC File Offset: 0x001782FC
	public void SetupFeedingTest(Scenario.Builder b)
	{
		b.FillOffsets(SimHashes.IgneousRock, new int[]
		{
			1, 0, 3, 0, 3, 1, 5, 0, 5, 1,
			5, 2, 7, 0, 7, 1, 7, 2, 9, 0,
			9, 1, 11, 0
		});
		b.PrefabOffsets("Hexaped", new int[]
		{
			0, 0, 2, 0, 4, 0, 7, 3, 9, 2,
			11, 1
		});
		b.OreOffsets(1, SimHashes.IronOre, new int[]
		{
			1, 1, 3, 2, 5, 3, 8, 0, 10, 0,
			12, 0
		});
		b.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x060042F0 RID: 17136 RVA: 0x0017A174 File Offset: 0x00178374
	public void SetupLiquifierTest(Scenario.Builder b)
	{
		b.Minion(null);
		b.Minion(null);
		b.Ore(2, SimHashes.Ice);
		b.Building("ManualGenerator");
		b.Building("Liquifier");
		b.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x060042F1 RID: 17137 RVA: 0x0017A1C4 File Offset: 0x001783C4
	public void SetupFallTest(Scenario.Builder b)
	{
		b.Jump(0, 5);
		b.Minion(null);
		b.Jump(0, -1);
		b.Building("Tile");
		b.Building("Tile");
		b.Building("Tile");
		b.Jump(-1, 1);
		b.Minion(null);
		b.Jump(2, 0);
		b.Minion(null);
		b.Jump(0, -1);
		b.Building("Tile");
		b.Jump(2, 1);
		b.Minion(null);
		b.Building("Ladder");
		b.Jump(-1, -1);
		b.Building("Tile");
		b.Jump(-1, -3);
		b.Building("Ladder");
		b.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x060042F2 RID: 17138 RVA: 0x0017A294 File Offset: 0x00178494
	public void SetupClimbTest(int left, int bot)
	{
		this.DigHole(left, bot, 13, 5);
		this.SpawnPrefab(left + 1, bot + 1, "Minion", Grid.SceneLayer.Ore);
		int num = left + 2;
		this.Clear(num++, bot - 1);
		num++;
		this.Fill(num++, bot, SimHashes.Ice);
		num++;
		this.Clear(num, bot - 1);
		this.Clear(num++, bot - 2);
		this.Fill(num++, bot, SimHashes.Ice);
		this.Clear(num, bot - 1);
		this.Clear(num++, bot - 2);
		num++;
		this.Fill(num, bot, SimHashes.Ice);
		this.Fill(num, bot + 1, SimHashes.Ice);
	}

	// Token: 0x060042F3 RID: 17139 RVA: 0x0017A34C File Offset: 0x0017854C
	private void SetupSuitRechargeTest(Scenario.Builder b)
	{
		b.Prefab("PressureSuit", delegate(GameObject go)
		{
			go.GetComponent<SuitTank>().Empty();
		});
		b.Building("ManualGenerator");
		b.Minion(null);
		b.Building("SuitRecharger");
		b.Minion(null);
		b.Building("GasVent");
		b.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x060042F4 RID: 17140 RVA: 0x0017A3C8 File Offset: 0x001785C8
	private void SetupSuitTest(Scenario.Builder b)
	{
		b.Minion(null);
		b.Prefab("PressureSuit", null);
		b.Jump(1, 2);
		b.Building("Tile");
		b.Jump(-1, -2);
		b.Building("Door");
		b.Building("ManualGenerator");
		b.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x060042F5 RID: 17141 RVA: 0x0017A430 File Offset: 0x00178630
	private void SetupTwoKelvinsOneSuitTest(Scenario.Builder b)
	{
		b.Minion(null);
		b.Jump(2, 0);
		b.Building("Door");
		b.Jump(2, 0);
		b.Minion(null);
		b.Prefab("PressureSuit", null);
		b.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x060042F6 RID: 17142 RVA: 0x0017A484 File Offset: 0x00178684
	public void Clear()
	{
		foreach (Brain brain in Components.Brains.Items)
		{
			UnityEngine.Object.Destroy(brain.gameObject);
		}
		foreach (Pickupable pickupable in Components.Pickupables.Items)
		{
			UnityEngine.Object.Destroy(pickupable.gameObject);
		}
		foreach (BuildingComplete buildingComplete in Components.BuildingCompletes.Items)
		{
			UnityEngine.Object.Destroy(buildingComplete.gameObject);
		}
	}

	// Token: 0x060042F7 RID: 17143 RVA: 0x0017A570 File Offset: 0x00178770
	public void SetupGameplayTest()
	{
		this.Init();
		Vector3 vector = Grid.CellToPosCCC(this.RootCell, Grid.SceneLayer.Background);
		CameraController.Instance.SnapTo(vector);
		if (this.ClearExistingScene)
		{
			this.Clear();
		}
		Scenario.RowLayout rowLayout = new Scenario.RowLayout(0, 0);
		if (this.CementMixerTest)
		{
			this.SetupCementMixerTest(rowLayout.NextRow());
		}
		if (this.RockCrusherTest)
		{
			this.SetupRockCrusherTest(rowLayout.NextRow());
		}
		if (this.PropaneGeneratorTest)
		{
			this.SetupPropaneGeneratorTest(rowLayout.NextRow());
		}
		if (this.DoorTest)
		{
			this.SetupDoorTest(rowLayout.NextRow());
		}
		if (this.HatchTest)
		{
			this.SetupHatchTest(rowLayout.NextRow());
		}
		if (this.AirLockTest)
		{
			this.SetupAirLockTest(rowLayout.NextRow());
		}
		if (this.BedTest)
		{
			this.SetupBedTest(rowLayout.NextRow());
		}
		if (this.LiquifierTest)
		{
			this.SetupLiquifierTest(rowLayout.NextRow());
		}
		if (this.SuitTest)
		{
			this.SetupSuitTest(rowLayout.NextRow());
		}
		if (this.SuitRechargeTest)
		{
			this.SetupSuitRechargeTest(rowLayout.NextRow());
		}
		if (this.TwoKelvinsOneSuitTest)
		{
			this.SetupTwoKelvinsOneSuitTest(rowLayout.NextRow());
		}
		if (this.FabricatorTest)
		{
			this.SetupFabricatorTest(rowLayout.NextRow());
		}
		if (this.ElectrolyzerTest)
		{
			this.SetupElectrolyzerTest(rowLayout.NextRow());
		}
		if (this.HexapedTest)
		{
			this.SetupHexapedTest(rowLayout.NextRow());
		}
		if (this.FallTest)
		{
			this.SetupFallTest(rowLayout.NextRow());
		}
		if (this.FeedingTest)
		{
			this.SetupFeedingTest(rowLayout.NextRow());
		}
		if (this.OrePerformanceTest)
		{
			this.SetupOrePerformanceTest(rowLayout.NextRow());
		}
		if (this.KilnTest)
		{
			this.SetupKilnTest(rowLayout.NextRow());
		}
	}

	// Token: 0x060042F8 RID: 17144 RVA: 0x0017A719 File Offset: 0x00178919
	private GameObject SpawnMinion(int x, int y)
	{
		return this.SpawnPrefab(x, y, "Minion", Grid.SceneLayer.Move);
	}

	// Token: 0x060042F9 RID: 17145 RVA: 0x0017A72C File Offset: 0x0017892C
	private void SetupLadderTest(int left, int bot)
	{
		int num = 5;
		this.DigHole(left, bot, 13, num);
		this.SpawnMinion(left + 1, bot);
		int num2 = left + 1;
		this.PlacerLadder(num2++, bot, num);
		this.PlaceColumn(num2++, bot, num);
		this.SpawnMinion(num2, bot);
		this.PlacerLadder(num2++, bot + 1, num - 1);
		this.PlaceColumn(num2++, bot, num);
		this.SpawnMinion(num2++, bot);
		this.PlacerLadder(num2++, bot, num);
		this.PlaceColumn(num2++, bot, num);
		this.SpawnMinion(num2++, bot);
		this.PlacerLadder(num2++, bot + 1, num - 1);
		this.PlaceColumn(num2++, bot, num);
		this.SpawnMinion(num2++, bot);
		this.PlacerLadder(num2++, bot - 1, -num);
	}

	// Token: 0x060042FA RID: 17146 RVA: 0x0017A808 File Offset: 0x00178A08
	public void PlaceUtilitiesX(int left, int bot, int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			this.PlaceUtilities(left + i, bot);
		}
	}

	// Token: 0x060042FB RID: 17147 RVA: 0x0017A82B File Offset: 0x00178A2B
	public void PlaceUtilities(int left, int bot)
	{
		this.PlaceBuilding(left, bot, "Wire", SimHashes.Cuprite);
		this.PlaceBuilding(left, bot, "GasConduit", SimHashes.Cuprite);
	}

	// Token: 0x060042FC RID: 17148 RVA: 0x0017A854 File Offset: 0x00178A54
	public void SetupVisualTest()
	{
		this.Init();
		Scenario.RowLayout rowLayout = new Scenario.RowLayout(this.Left, this.Bot);
		this.SetupBuildingTest(rowLayout, false, false);
	}

	// Token: 0x060042FD RID: 17149 RVA: 0x0017A884 File Offset: 0x00178A84
	private void SpawnMaterialTest(Scenario.Builder b)
	{
		foreach (Element element in ElementLoader.elements)
		{
			if (element.IsSolid)
			{
				b.Element = element.id;
				b.Building("Generator");
			}
		}
		b.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x060042FE RID: 17150 RVA: 0x0017A900 File Offset: 0x00178B00
	public GameObject PlaceBuilding(int x, int y, string prefab_id, SimHashes element = SimHashes.Cuprite)
	{
		return Scenario.PlaceBuilding(this.RootCell, x, y, prefab_id, element);
	}

	// Token: 0x060042FF RID: 17151 RVA: 0x0017A914 File Offset: 0x00178B14
	public static GameObject PlaceBuilding(int root_cell, int x, int y, string prefab_id, SimHashes element = SimHashes.Cuprite)
	{
		int num = Grid.OffsetCell(root_cell, x, y);
		BuildingDef buildingDef = Assets.GetBuildingDef(prefab_id);
		if (buildingDef == null || buildingDef.PlacementOffsets == null)
		{
			DebugUtil.LogErrorArgs(new object[] { "Missing def for", prefab_id });
		}
		Element element2 = ElementLoader.FindElementByHash(element);
		global::Debug.Assert(element2 != null, string.Concat(new string[]
		{
			"Missing primary element '",
			Enum.GetName(typeof(SimHashes), element),
			"' in '",
			prefab_id,
			"'"
		}));
		GameObject gameObject = buildingDef.Build(buildingDef.GetBuildingCell(num), Orientation.Neutral, null, new Tag[]
		{
			element2.tag,
			ElementLoader.FindElementByHash(SimHashes.SedimentaryRock).tag
		}, 293.15f, false, -1f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.InternalTemperature = 300f;
		component.Temperature = 300f;
		return gameObject;
	}

	// Token: 0x06004300 RID: 17152 RVA: 0x0017AA08 File Offset: 0x00178C08
	private void SpawnOre(int x, int y, SimHashes element = SimHashes.Cuprite)
	{
		this.RunAfterNextUpdate(delegate
		{
			Vector3 vector = Grid.CellToPosCCC(Grid.OffsetCell(this.RootCell, x, y), Grid.SceneLayer.Ore);
			vector.x += UnityEngine.Random.Range(-0.1f, 0.1f);
			ElementLoader.FindElementByHash(element).substance.SpawnResource(vector, 4000f, 293f, byte.MaxValue, 0, false, false, false);
		});
	}

	// Token: 0x06004301 RID: 17153 RVA: 0x0017AA49 File Offset: 0x00178C49
	public GameObject SpawnPrefab(int x, int y, string name, Grid.SceneLayer scene_layer = Grid.SceneLayer.Ore)
	{
		return Scenario.SpawnPrefab(this.RootCell, x, y, name, scene_layer);
	}

	// Token: 0x06004302 RID: 17154 RVA: 0x0017AA5C File Offset: 0x00178C5C
	public void SpawnPrefabLate(int x, int y, string name, Grid.SceneLayer scene_layer = Grid.SceneLayer.Ore)
	{
		this.RunAfterNextUpdate(delegate
		{
			Scenario.SpawnPrefab(this.RootCell, x, y, name, scene_layer);
		});
	}

	// Token: 0x06004303 RID: 17155 RVA: 0x0017AAA8 File Offset: 0x00178CA8
	public static GameObject SpawnPrefab(int RootCell, int x, int y, string name, Grid.SceneLayer scene_layer = Grid.SceneLayer.Ore)
	{
		int num = Grid.OffsetCell(RootCell, x, y);
		GameObject prefab = Assets.GetPrefab(TagManager.Create(name));
		if (prefab == null)
		{
			return null;
		}
		return GameUtil.KInstantiate(prefab, Grid.CellToPosCBC(num, scene_layer), scene_layer, null, 0);
	}

	// Token: 0x06004304 RID: 17156 RVA: 0x0017AAE8 File Offset: 0x00178CE8
	public void SetupElementTest()
	{
		this.Init();
		PropertyTextures.FogOfWarScale = 1f;
		Vector3 vector = Grid.CellToPosCCC(this.RootCell, Grid.SceneLayer.Background);
		CameraController.Instance.SnapTo(vector);
		this.Clear();
		Scenario.Builder builder = new Scenario.RowLayout(0, 0).NextRow();
		HashSet<Element> elements = new HashSet<Element>();
		int bot = builder.Bot;
		foreach (Element element5 in (from element in ElementLoader.elements
			where element.IsSolid
			orderby element.highTempTransitionTarget
			select element).ToList<Element>())
		{
			if (element5.IsSolid)
			{
				Element element2 = element5;
				int left = builder.Left;
				bool hasTransitionUp;
				do
				{
					elements.Add(element2);
					builder.Hole(2, 3);
					builder.Fill(2, 2, element2.id);
					builder.FinalizeRoom(SimHashes.Vacuum, SimHashes.Unobtanium);
					builder = new Scenario.Builder(left, builder.Bot + 4, SimHashes.Copper);
					hasTransitionUp = element2.HasTransitionUp;
					if (hasTransitionUp)
					{
						element2 = element2.highTempTransition;
					}
				}
				while (hasTransitionUp);
				builder = new Scenario.Builder(left + 3, bot, SimHashes.Copper);
			}
		}
		foreach (Element element3 in (from element in ElementLoader.elements
			where element.IsLiquid && !elements.Contains(element)
			orderby element.highTempTransitionTarget
			select element).ToList<Element>())
		{
			int left2 = builder.Left;
			bool hasTransitionUp2;
			do
			{
				elements.Add(element3);
				builder.Hole(2, 3);
				builder.Fill(2, 2, element3.id);
				builder.FinalizeRoom(SimHashes.Vacuum, SimHashes.Unobtanium);
				builder = new Scenario.Builder(left2, builder.Bot + 4, SimHashes.Copper);
				hasTransitionUp2 = element3.HasTransitionUp;
				if (hasTransitionUp2)
				{
					element3 = element3.highTempTransition;
				}
			}
			while (hasTransitionUp2);
			builder = new Scenario.Builder(left2 + 3, bot, SimHashes.Copper);
		}
		foreach (Element element4 in ElementLoader.elements.Where((Element element) => element.state == Element.State.Gas && !elements.Contains(element)).ToList<Element>())
		{
			int left3 = builder.Left;
			builder.Hole(2, 3);
			builder.Fill(2, 2, element4.id);
			builder.FinalizeRoom(SimHashes.Vacuum, SimHashes.Unobtanium);
			builder = new Scenario.Builder(left3, builder.Bot + 4, SimHashes.Copper);
			builder = new Scenario.Builder(left3 + 3, bot, SimHashes.Copper);
		}
	}

	// Token: 0x06004305 RID: 17157 RVA: 0x0017AE0C File Offset: 0x0017900C
	private void InitDebugScenario()
	{
		this.Init();
		PropertyTextures.FogOfWarScale = 1f;
		Vector3 vector = Grid.CellToPosCCC(this.RootCell, Grid.SceneLayer.Background);
		CameraController.Instance.SnapTo(vector);
		this.Clear();
	}

	// Token: 0x06004306 RID: 17158 RVA: 0x0017AE48 File Offset: 0x00179048
	public void SetupTileTest()
	{
		this.InitDebugScenario();
		for (int i = 0; i < Grid.HeightInCells; i++)
		{
			for (int j = 0; j < Grid.WidthInCells; j++)
			{
				SimMessages.ReplaceElement(Grid.XYToCell(j, i), SimHashes.Oxygen, CellEventLogger.Instance.Scenario, 100f, -1f, byte.MaxValue, 0, -1);
			}
		}
		Scenario.Builder builder = new Scenario.RowLayout(0, 0).NextRow();
		for (int k = 0; k < 16; k++)
		{
			builder.Jump(0, 0);
			builder.Fill(1, 1, ((k & 1) != 0) ? SimHashes.Copper : SimHashes.Diamond);
			builder.Jump(1, 0);
			builder.Fill(1, 1, ((k & 2) != 0) ? SimHashes.Copper : SimHashes.Diamond);
			builder.Jump(-1, 1);
			builder.Fill(1, 1, ((k & 4) != 0) ? SimHashes.Copper : SimHashes.Diamond);
			builder.Jump(1, 0);
			builder.Fill(1, 1, ((k & 8) != 0) ? SimHashes.Copper : SimHashes.Diamond);
			builder.Jump(2, -1);
		}
	}

	// Token: 0x06004307 RID: 17159 RVA: 0x0017AF54 File Offset: 0x00179154
	public void SetupRiverTest()
	{
		this.InitDebugScenario();
		int num = Mathf.Min(64, Grid.WidthInCells);
		int num2 = Mathf.Min(64, Grid.HeightInCells);
		List<Element> list = new List<Element>();
		foreach (Element element in ElementLoader.elements)
		{
			if (element.IsLiquid)
			{
				list.Add(element);
			}
		}
		for (int i = 0; i < num2; i++)
		{
			for (int j = 0; j < num; j++)
			{
				SimHashes simHashes = ((i == 0) ? SimHashes.Unobtanium : SimHashes.Oxygen);
				SimMessages.ReplaceElement(Grid.XYToCell(j, i), simHashes, CellEventLogger.Instance.Scenario, 1000f, -1f, byte.MaxValue, 0, -1);
			}
		}
	}

	// Token: 0x06004308 RID: 17160 RVA: 0x0017B034 File Offset: 0x00179234
	public void SetupRockCrusherTest(Scenario.Builder b)
	{
		this.InitDebugScenario();
		b.Building("ManualGenerator");
		b.Minion(null);
		b.Building("Crusher");
		b.Minion(null);
		b.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x06004309 RID: 17161 RVA: 0x0017B074 File Offset: 0x00179274
	public void SetupCementMixerTest(Scenario.Builder b)
	{
		this.InitDebugScenario();
		b.Building("Generator");
		b.Minion(null);
		b.Building("Crusher");
		b.Minion(null);
		b.Minion(null);
		b.Building("Mixer");
		b.Ore(20, SimHashes.SedimentaryRock);
		b.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x0600430A RID: 17162 RVA: 0x0017B0E0 File Offset: 0x001792E0
	public void SetupKilnTest(Scenario.Builder b)
	{
		this.InitDebugScenario();
		b.Building("ManualGenerator");
		b.Minion(null);
		b.Building("Kiln");
		b.Minion(null);
		b.Ore(20, SimHashes.SandCement);
		b.FinalizeRoom(SimHashes.Oxygen, SimHashes.Steel);
	}

	// Token: 0x04002C9A RID: 11418
	private int Bot;

	// Token: 0x04002C9B RID: 11419
	private int Left;

	// Token: 0x04002C9C RID: 11420
	public int RootCell;

	// Token: 0x04002C9E RID: 11422
	public static Scenario Instance;

	// Token: 0x04002C9F RID: 11423
	public bool PropaneGeneratorTest = true;

	// Token: 0x04002CA0 RID: 11424
	public bool HatchTest = true;

	// Token: 0x04002CA1 RID: 11425
	public bool DoorTest = true;

	// Token: 0x04002CA2 RID: 11426
	public bool AirLockTest = true;

	// Token: 0x04002CA3 RID: 11427
	public bool BedTest = true;

	// Token: 0x04002CA4 RID: 11428
	public bool SuitTest = true;

	// Token: 0x04002CA5 RID: 11429
	public bool SuitRechargeTest = true;

	// Token: 0x04002CA6 RID: 11430
	public bool FabricatorTest = true;

	// Token: 0x04002CA7 RID: 11431
	public bool ElectrolyzerTest = true;

	// Token: 0x04002CA8 RID: 11432
	public bool HexapedTest = true;

	// Token: 0x04002CA9 RID: 11433
	public bool FallTest = true;

	// Token: 0x04002CAA RID: 11434
	public bool FeedingTest = true;

	// Token: 0x04002CAB RID: 11435
	public bool OrePerformanceTest = true;

	// Token: 0x04002CAC RID: 11436
	public bool TwoKelvinsOneSuitTest = true;

	// Token: 0x04002CAD RID: 11437
	public bool LiquifierTest = true;

	// Token: 0x04002CAE RID: 11438
	public bool RockCrusherTest = true;

	// Token: 0x04002CAF RID: 11439
	public bool CementMixerTest = true;

	// Token: 0x04002CB0 RID: 11440
	public bool KilnTest = true;

	// Token: 0x04002CB1 RID: 11441
	public bool ClearExistingScene = true;

	// Token: 0x020016DC RID: 5852
	public class RowLayout
	{
		// Token: 0x060088D0 RID: 35024 RVA: 0x002F6BF8 File Offset: 0x002F4DF8
		public RowLayout(int left, int bot)
		{
			this.Left = left;
			this.Bot = bot;
		}

		// Token: 0x060088D1 RID: 35025 RVA: 0x002F6C10 File Offset: 0x002F4E10
		public Scenario.Builder NextRow()
		{
			if (this.Builder != null)
			{
				this.Bot = this.Builder.Max.y + 1;
			}
			this.Builder = new Scenario.Builder(this.Left, this.Bot, SimHashes.Copper);
			return this.Builder;
		}

		// Token: 0x04006B2F RID: 27439
		public int Left;

		// Token: 0x04006B30 RID: 27440
		public int Bot;

		// Token: 0x04006B31 RID: 27441
		public Scenario.Builder Builder;
	}

	// Token: 0x020016DD RID: 5853
	public class Builder
	{
		// Token: 0x060088D2 RID: 35026 RVA: 0x002F6C60 File Offset: 0x002F4E60
		public Builder(int left, int bot, SimHashes element = SimHashes.Copper)
		{
			this.Left = left;
			this.Bot = bot;
			this.Element = element;
			this.Scenario = Scenario.Instance;
			this.PlaceUtilities = true;
			this.Min = new Vector2I(left, bot);
			this.Max = new Vector2I(left, bot);
		}

		// Token: 0x060088D3 RID: 35027 RVA: 0x002F6CB4 File Offset: 0x002F4EB4
		private void UpdateMinMax(int x, int y)
		{
			this.Min.x = Math.Min(x, this.Min.x);
			this.Min.y = Math.Min(y, this.Min.y);
			this.Max.x = Math.Max(x + 1, this.Max.x);
			this.Max.y = Math.Max(y + 1, this.Max.y);
		}

		// Token: 0x060088D4 RID: 35028 RVA: 0x002F6D38 File Offset: 0x002F4F38
		public void Utilities(int count)
		{
			for (int i = 0; i < count; i++)
			{
				this.Scenario.PlaceUtilities(this.Left, this.Bot);
				this.Left++;
			}
		}

		// Token: 0x060088D5 RID: 35029 RVA: 0x002F6D78 File Offset: 0x002F4F78
		public void BuildingOffsets(string prefab_id, params int[] offsets)
		{
			int left = this.Left;
			int bot = this.Bot;
			for (int i = 0; i < offsets.Length / 2; i++)
			{
				this.Jump(offsets[i * 2], offsets[i * 2 + 1]);
				this.Building(prefab_id);
				this.JumpTo(left, bot);
			}
		}

		// Token: 0x060088D6 RID: 35030 RVA: 0x002F6DC8 File Offset: 0x002F4FC8
		public void Placer(string prefab_id, Element element)
		{
			BuildingDef buildingDef = Assets.GetBuildingDef(prefab_id);
			int buildingCell = buildingDef.GetBuildingCell(Grid.OffsetCell(Scenario.Instance.RootCell, this.Left, this.Bot));
			Vector3 pos = Grid.CellToPosCBC(buildingCell, Grid.SceneLayer.Building);
			this.UpdateMinMax(this.Left, this.Bot);
			this.UpdateMinMax(this.Left + buildingDef.WidthInCells - 1, this.Bot + buildingDef.HeightInCells - 1);
			this.Left += buildingDef.WidthInCells;
			this.Scenario.RunAfterNextUpdate(delegate
			{
				Assets.GetBuildingDef(prefab_id).TryPlace(null, pos, Orientation.Neutral, new Tag[]
				{
					element.tag,
					ElementLoader.FindElementByHash(SimHashes.SedimentaryRock).tag
				}, 0);
			});
		}

		// Token: 0x060088D7 RID: 35031 RVA: 0x002F6E88 File Offset: 0x002F5088
		public GameObject Building(string prefab_id)
		{
			GameObject gameObject = this.Scenario.PlaceBuilding(this.Left, this.Bot, prefab_id, this.Element);
			BuildingDef buildingDef = Assets.GetBuildingDef(prefab_id);
			this.UpdateMinMax(this.Left, this.Bot);
			this.UpdateMinMax(this.Left + buildingDef.WidthInCells - 1, this.Bot + buildingDef.HeightInCells - 1);
			if (this.PlaceUtilities)
			{
				for (int i = 0; i < buildingDef.WidthInCells; i++)
				{
					this.UpdateMinMax(this.Left + i, this.Bot);
					this.Scenario.PlaceUtilities(this.Left + i, this.Bot);
				}
			}
			this.Left += buildingDef.WidthInCells;
			return gameObject;
		}

		// Token: 0x060088D8 RID: 35032 RVA: 0x002F6F4C File Offset: 0x002F514C
		public void Minion(Action<GameObject> on_spawn = null)
		{
			this.UpdateMinMax(this.Left, this.Bot);
			int left = this.Left;
			int bot = this.Bot;
			this.Scenario.RunAfterNextUpdate(delegate
			{
				GameObject gameObject = this.Scenario.SpawnMinion(left, bot);
				if (on_spawn != null)
				{
					on_spawn(gameObject);
				}
			});
		}

		// Token: 0x060088D9 RID: 35033 RVA: 0x002F6FAE File Offset: 0x002F51AE
		private GameObject Hexaped()
		{
			return this.Scenario.SpawnPrefab(this.Left, this.Bot, "Hexaped", Grid.SceneLayer.Front);
		}

		// Token: 0x060088DA RID: 35034 RVA: 0x002F6FD0 File Offset: 0x002F51D0
		public void OreOffsets(int count, SimHashes element, params int[] offsets)
		{
			int left = this.Left;
			int bot = this.Bot;
			for (int i = 0; i < offsets.Length / 2; i++)
			{
				this.Jump(offsets[i * 2], offsets[i * 2 + 1]);
				this.Ore(count, element);
				this.JumpTo(left, bot);
			}
		}

		// Token: 0x060088DB RID: 35035 RVA: 0x002F7020 File Offset: 0x002F5220
		public void Ore(int count = 1, SimHashes element = SimHashes.Cuprite)
		{
			this.UpdateMinMax(this.Left, this.Bot);
			for (int i = 0; i < count; i++)
			{
				this.Scenario.SpawnOre(this.Left, this.Bot, element);
			}
		}

		// Token: 0x060088DC RID: 35036 RVA: 0x002F7064 File Offset: 0x002F5264
		public void PrefabOffsets(string prefab_id, params int[] offsets)
		{
			int left = this.Left;
			int bot = this.Bot;
			for (int i = 0; i < offsets.Length / 2; i++)
			{
				this.Jump(offsets[i * 2], offsets[i * 2 + 1]);
				this.Prefab(prefab_id, null);
				this.JumpTo(left, bot);
			}
		}

		// Token: 0x060088DD RID: 35037 RVA: 0x002F70B4 File Offset: 0x002F52B4
		public void Prefab(string prefab_id, Action<GameObject> on_spawn = null)
		{
			this.UpdateMinMax(this.Left, this.Bot);
			int left = this.Left;
			int bot = this.Bot;
			this.Scenario.RunAfterNextUpdate(delegate
			{
				GameObject gameObject = this.Scenario.SpawnPrefab(left, bot, prefab_id, Grid.SceneLayer.Ore);
				if (on_spawn != null)
				{
					on_spawn(gameObject);
				}
			});
		}

		// Token: 0x060088DE RID: 35038 RVA: 0x002F7120 File Offset: 0x002F5320
		public void Wall(int height)
		{
			for (int i = 0; i < height; i++)
			{
				this.Scenario.PlaceBuilding(this.Left, this.Bot + i, "Tile", SimHashes.Cuprite);
				this.UpdateMinMax(this.Left, this.Bot + i);
				if (this.PlaceUtilities)
				{
					this.Scenario.PlaceUtilities(this.Left, this.Bot + i);
				}
			}
			this.Left++;
		}

		// Token: 0x060088DF RID: 35039 RVA: 0x002F71A0 File Offset: 0x002F53A0
		public void Jump(int x = 0, int y = 0)
		{
			this.Left += x;
			this.Bot += y;
		}

		// Token: 0x060088E0 RID: 35040 RVA: 0x002F71BE File Offset: 0x002F53BE
		public void JumpTo(int left, int bot)
		{
			this.Left = left;
			this.Bot = bot;
		}

		// Token: 0x060088E1 RID: 35041 RVA: 0x002F71D0 File Offset: 0x002F53D0
		public void Hole(int width, int height)
		{
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					int num = Grid.OffsetCell(this.Scenario.RootCell, this.Left + i, this.Bot + j);
					this.UpdateMinMax(this.Left + i, this.Bot + j);
					SimMessages.ReplaceElement(num, SimHashes.Vacuum, CellEventLogger.Instance.Scenario, 0f, -1f, byte.MaxValue, 0, -1);
					this.Scenario.ReplaceElementMask[num] = true;
				}
			}
		}

		// Token: 0x060088E2 RID: 35042 RVA: 0x002F7260 File Offset: 0x002F5460
		public void FillOffsets(SimHashes element, params int[] offsets)
		{
			int left = this.Left;
			int bot = this.Bot;
			for (int i = 0; i < offsets.Length / 2; i++)
			{
				this.Jump(offsets[i * 2], offsets[i * 2 + 1]);
				this.Fill(1, 1, element);
				this.JumpTo(left, bot);
			}
		}

		// Token: 0x060088E3 RID: 35043 RVA: 0x002F72B0 File Offset: 0x002F54B0
		public void Fill(int width, int height, SimHashes element)
		{
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					int num = Grid.OffsetCell(this.Scenario.RootCell, this.Left + i, this.Bot + j);
					this.UpdateMinMax(this.Left + i, this.Bot + j);
					SimMessages.ReplaceElement(num, element, CellEventLogger.Instance.Scenario, 5000f, -1f, byte.MaxValue, 0, -1);
					this.Scenario.ReplaceElementMask[num] = true;
				}
			}
		}

		// Token: 0x060088E4 RID: 35044 RVA: 0x002F733C File Offset: 0x002F553C
		public void InAndOuts()
		{
			this.Wall(3);
			this.Building("GasVent");
			this.Hole(3, 3);
			this.Utilities(2);
			this.Wall(3);
			this.Building("LiquidVent");
			this.Hole(3, 3);
			this.Utilities(2);
			this.Wall(3);
			this.Fill(3, 3, SimHashes.Water);
			this.Utilities(2);
			GameObject pump = this.Building("Pump");
			this.Scenario.RunAfterNextUpdate(delegate
			{
				pump.GetComponent<BuildingEnabledButton>().IsEnabled = true;
			});
		}

		// Token: 0x060088E5 RID: 35045 RVA: 0x002F73D8 File Offset: 0x002F55D8
		public Scenario.Builder FinalizeRoom(SimHashes element = SimHashes.Oxygen, SimHashes tileElement = SimHashes.Steel)
		{
			for (int i = this.Min.x - 1; i <= this.Max.x; i++)
			{
				if (i == this.Min.x - 1 || i == this.Max.x)
				{
					for (int j = this.Min.y - 1; j <= this.Max.y; j++)
					{
						this.Scenario.PlaceBuilding(i, j, "Tile", tileElement);
					}
				}
				else
				{
					int num = 500;
					if (element == SimHashes.Void)
					{
						num = 0;
					}
					for (int k = this.Min.y; k < this.Max.y; k++)
					{
						int num2 = Grid.OffsetCell(this.Scenario.RootCell, i, k);
						if (!this.Scenario.ReplaceElementMask[num2])
						{
							SimMessages.ReplaceElement(num2, element, CellEventLogger.Instance.Scenario, (float)num, -1f, byte.MaxValue, 0, -1);
						}
					}
				}
				this.Scenario.PlaceBuilding(i, this.Min.y - 1, "Tile", tileElement);
				this.Scenario.PlaceBuilding(i, this.Max.y, "Tile", tileElement);
			}
			return new Scenario.Builder(this.Max.x + 1, this.Min.y, SimHashes.Copper);
		}

		// Token: 0x04006B32 RID: 27442
		public bool PlaceUtilities;

		// Token: 0x04006B33 RID: 27443
		public int Left;

		// Token: 0x04006B34 RID: 27444
		public int Bot;

		// Token: 0x04006B35 RID: 27445
		public Vector2I Min;

		// Token: 0x04006B36 RID: 27446
		public Vector2I Max;

		// Token: 0x04006B37 RID: 27447
		public SimHashes Element;

		// Token: 0x04006B38 RID: 27448
		private Scenario Scenario;
	}
}
