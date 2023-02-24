using System;
using TUNING;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class AutoMinerConfig : IBuildingConfig
{
	// Token: 0x06000087 RID: 135 RVA: 0x0000572C File Offset: 0x0000392C
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("AutoMiner", 2, 2, "auto_miner_kanim", 10, 10f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER3, MATERIALS.REFINED_METALS, 1600f, BuildLocationRule.OnFoundationRotatable, BUILDINGS.DECOR.PENALTY.TIER2, NOISE_POLLUTION.NOISY.TIER0, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.ExhaustKilowattsWhenActive = 0f;
		buildingDef.SelfHeatKilowattsWhenActive = 2f;
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SolidConveyorIDs, "AutoMiner");
		return buildingDef;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x000057D3 File Offset: 0x000039D3
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<Operational>();
		go.AddOrGet<LoopingSounds>();
		go.AddOrGet<MiningSounds>();
	}

	// Token: 0x06000089 RID: 137 RVA: 0x000057EA File Offset: 0x000039EA
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		AutoMinerConfig.AddVisualizer(go, true);
	}

	// Token: 0x0600008A RID: 138 RVA: 0x000057F3 File Offset: 0x000039F3
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		AutoMinerConfig.AddVisualizer(go, false);
	}

	// Token: 0x0600008B RID: 139 RVA: 0x000057FC File Offset: 0x000039FC
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		AutoMiner autoMiner = go.AddOrGet<AutoMiner>();
		autoMiner.x = -7;
		autoMiner.y = 0;
		autoMiner.width = 16;
		autoMiner.height = 9;
		autoMiner.vision_offset = new CellOffset(0, 1);
		AutoMinerConfig.AddVisualizer(go, false);
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00005848 File Offset: 0x00003A48
	private static void AddVisualizer(GameObject prefab, bool movable)
	{
		StationaryChoreRangeVisualizer stationaryChoreRangeVisualizer = prefab.AddOrGet<StationaryChoreRangeVisualizer>();
		stationaryChoreRangeVisualizer.x = -7;
		stationaryChoreRangeVisualizer.y = 0;
		stationaryChoreRangeVisualizer.width = 16;
		stationaryChoreRangeVisualizer.height = 9;
		stationaryChoreRangeVisualizer.vision_offset = new CellOffset(0, 1);
		stationaryChoreRangeVisualizer.movable = movable;
		stationaryChoreRangeVisualizer.blocking_tile_visible = false;
		prefab.GetComponent<KPrefabID>().instantiateFn += delegate(GameObject go)
		{
			go.GetComponent<StationaryChoreRangeVisualizer>().blocking_cb = new Func<int, bool>(AutoMiner.DigBlockingCB);
		};
	}

	// Token: 0x04000068 RID: 104
	public const string ID = "AutoMiner";

	// Token: 0x04000069 RID: 105
	private const int RANGE = 7;

	// Token: 0x0400006A RID: 106
	private const int X = -7;

	// Token: 0x0400006B RID: 107
	private const int Y = 0;

	// Token: 0x0400006C RID: 108
	private const int WIDTH = 16;

	// Token: 0x0400006D RID: 109
	private const int HEIGHT = 9;

	// Token: 0x0400006E RID: 110
	private const int VISION_OFFSET = 1;
}
