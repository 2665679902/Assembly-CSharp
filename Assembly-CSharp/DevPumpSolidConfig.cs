﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x02000063 RID: 99
public class DevPumpSolidConfig : IBuildingConfig
{
	// Token: 0x060001B9 RID: 441 RVA: 0x0000C5DC File Offset: 0x0000A7DC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "DevPumpSolid";
		int num = 2;
		int num2 = 2;
		string text2 = "dev_pump_solid_kanim";
		int num3 = 100;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, tier2, 0.2f);
		buildingDef.RequiresPowerInput = false;
		buildingDef.OutputConduitType = ConduitType.Solid;
		buildingDef.Floodable = false;
		buildingDef.Invincible = true;
		buildingDef.Overheatable = false;
		buildingDef.Entombable = false;
		buildingDef.ViewMode = OverlayModes.SolidConveyor.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.UtilityOutputOffset = this.primaryPort.offset;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SolidConveyorIDs, "DevPumpSolid");
		buildingDef.DebugOnly = true;
		return buildingDef;
	}

	// Token: 0x060001BA RID: 442 RVA: 0x0000C689 File Offset: 0x0000A889
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddTag(GameTags.DevBuilding);
		base.ConfigureBuildingTemplate(go, prefab_tag);
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
	}

	// Token: 0x060001BB RID: 443 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<LoopingSounds>();
		go.AddOrGet<DevPump>().elementState = Filterable.ElementState.Solid;
		go.AddOrGet<Storage>().capacityKg = 20f;
		go.AddTag(GameTags.CorrosionProof);
		SolidConduitDispenser solidConduitDispenser = go.AddOrGet<SolidConduitDispenser>();
		solidConduitDispenser.alwaysDispense = true;
		solidConduitDispenser.elementFilter = null;
		go.AddOrGetDef<OperationalController.Def>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits, false);
	}

	// Token: 0x04000105 RID: 261
	public const string ID = "DevPumpSolid";

	// Token: 0x04000106 RID: 262
	private const ConduitType CONDUIT_TYPE = ConduitType.Solid;

	// Token: 0x04000107 RID: 263
	private ConduitPortInfo primaryPort = new ConduitPortInfo(ConduitType.Solid, new CellOffset(1, 1));
}
