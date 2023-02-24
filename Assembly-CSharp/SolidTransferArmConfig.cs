using System;
using TUNING;
using UnityEngine;

// Token: 0x0200031B RID: 795
public class SolidTransferArmConfig : IBuildingConfig
{
	// Token: 0x06000FD3 RID: 4051 RVA: 0x00055DCC File Offset: 0x00053FCC
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("SolidTransferArm", 3, 1, "conveyor_transferarm_kanim", 10, 10f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER3, MATERIALS.REFINED_METALS, 1600f, BuildLocationRule.Anywhere, BUILDINGS.DECOR.PENALTY.TIER2, NOISE_POLLUTION.NOISY.TIER0, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.ExhaustKilowattsWhenActive = 0f;
		buildingDef.SelfHeatKilowattsWhenActive = 2f;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		buildingDef.PermittedRotations = PermittedRotations.R360;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SolidConveyorIDs, "SolidTransferArm");
		return buildingDef;
	}

	// Token: 0x06000FD4 RID: 4052 RVA: 0x00055E72 File Offset: 0x00054072
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<Operational>();
		go.AddOrGet<LoopingSounds>();
	}

	// Token: 0x06000FD5 RID: 4053 RVA: 0x00055E82 File Offset: 0x00054082
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		SolidTransferArmConfig.AddVisualizer(go, true);
	}

	// Token: 0x06000FD6 RID: 4054 RVA: 0x00055E8B File Offset: 0x0005408B
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		SolidTransferArmConfig.AddVisualizer(go, false);
		go.GetComponent<Constructable>().requiredSkillPerk = Db.Get().SkillPerks.ConveyorBuild.Id;
	}

	// Token: 0x06000FD7 RID: 4055 RVA: 0x00055EB3 File Offset: 0x000540B3
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		go.AddOrGet<SolidTransferArm>().pickupRange = 4;
		SolidTransferArmConfig.AddVisualizer(go, false);
	}

	// Token: 0x06000FD8 RID: 4056 RVA: 0x00055ECF File Offset: 0x000540CF
	private static void AddVisualizer(GameObject prefab, bool movable)
	{
		StationaryChoreRangeVisualizer stationaryChoreRangeVisualizer = prefab.AddOrGet<StationaryChoreRangeVisualizer>();
		stationaryChoreRangeVisualizer.x = -4;
		stationaryChoreRangeVisualizer.y = -4;
		stationaryChoreRangeVisualizer.width = 9;
		stationaryChoreRangeVisualizer.height = 9;
		stationaryChoreRangeVisualizer.movable = movable;
	}

	// Token: 0x040008A3 RID: 2211
	public const string ID = "SolidTransferArm";

	// Token: 0x040008A4 RID: 2212
	private const int RANGE = 4;
}
