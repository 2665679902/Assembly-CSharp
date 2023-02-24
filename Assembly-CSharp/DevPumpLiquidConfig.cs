using System;
using TUNING;
using UnityEngine;

// Token: 0x02000062 RID: 98
public class DevPumpLiquidConfig : IBuildingConfig
{
	// Token: 0x060001B5 RID: 437 RVA: 0x0000C478 File Offset: 0x0000A678
	public override BuildingDef CreateBuildingDef()
	{
		string text = "DevPumpLiquid";
		int num = 2;
		int num2 = 2;
		string text2 = "dev_pump_liquid_kanim";
		int num3 = 100;
		float num4 = 60f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
		buildingDef.RequiresPowerInput = false;
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.Floodable = false;
		buildingDef.Invincible = true;
		buildingDef.Overheatable = false;
		buildingDef.Entombable = false;
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.UtilityOutputOffset = this.primaryPort.offset;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.LiquidVentIDs, "DevPumpLiquid");
		buildingDef.DebugOnly = true;
		return buildingDef;
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x0000C525 File Offset: 0x0000A725
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddTag(GameTags.DevBuilding);
		base.ConfigureBuildingTemplate(go, prefab_tag);
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x0000C540 File Offset: 0x0000A740
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<LoopingSounds>();
		go.AddOrGet<DevPump>().elementState = Filterable.ElementState.Liquid;
		go.AddOrGet<Storage>().capacityKg = 20f;
		go.AddTag(GameTags.CorrosionProof);
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Liquid;
		conduitDispenser.alwaysDispense = true;
		conduitDispenser.elementFilter = null;
		go.AddOrGetDef<OperationalController.Def>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits, false);
	}

	// Token: 0x04000102 RID: 258
	public const string ID = "DevPumpLiquid";

	// Token: 0x04000103 RID: 259
	private const ConduitType CONDUIT_TYPE = ConduitType.Liquid;

	// Token: 0x04000104 RID: 260
	private ConduitPortInfo primaryPort = new ConduitPortInfo(ConduitType.Liquid, new CellOffset(1, 1));
}
