﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x0200030F RID: 783
public class SolidCargoBayConfig : IBuildingConfig
{
	// Token: 0x06000F97 RID: 3991 RVA: 0x00054DCE File Offset: 0x00052FCE
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_VANILLA_ONLY;
	}

	// Token: 0x06000F98 RID: 3992 RVA: 0x00054DD8 File Offset: 0x00052FD8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "CargoBay";
		int num = 5;
		int num2 = 5;
		string text2 = "rocket_storage_solid_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] cargo_MASS = BUILDINGS.ROCKETRY_MASS_KG.CARGO_MASS;
		string[] array = new string[]
		{
			"BuildableRaw",
			SimHashes.Steel.ToString()
		};
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.BuildingAttachPoint;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, cargo_MASS, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.Invincible = true;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.RequiresPowerInput = false;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.CanMove = true;
		buildingDef.OutputConduitType = ConduitType.Solid;
		buildingDef.UtilityOutputOffset = new CellOffset(0, 3);
		return buildingDef;
	}

	// Token: 0x06000F99 RID: 3993 RVA: 0x00054EAC File Offset: 0x000530AC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 5), GameTags.Rocket, null)
		};
	}

	// Token: 0x06000F9A RID: 3994 RVA: 0x00054F10 File Offset: 0x00053110
	public override void DoPostConfigureComplete(GameObject go)
	{
		CargoBay cargoBay = go.AddOrGet<CargoBay>();
		cargoBay.storage = go.AddOrGet<Storage>();
		cargoBay.storageType = CargoBay.CargoType.Solids;
		cargoBay.storage.capacityKg = 1000f;
		cargoBay.storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		BuildingTemplates.ExtendBuildingToRocketModule(go, "rocket_storage_solid_bg_kanim", false);
		go.AddOrGet<SolidConduitDispenser>();
	}

	// Token: 0x04000891 RID: 2193
	public const string ID = "CargoBay";
}
