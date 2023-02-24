﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x02000310 RID: 784
public class SolidCargoBaySmallConfig : IBuildingConfig
{
	// Token: 0x06000F9C RID: 3996 RVA: 0x00054F71 File Offset: 0x00053171
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x00054F78 File Offset: 0x00053178
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SolidCargoBaySmall";
		int num = 3;
		int num2 = 3;
		string text2 = "rocket_storage_solid_small_kanim";
		int num3 = 1000;
		float num4 = 30f;
		float[] hollow_TIER = BUILDINGS.ROCKETRY_MASS_KG.HOLLOW_TIER1;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, hollow_TIER, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.Invincible = true;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.RequiresPowerInput = false;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		return buildingDef;
	}

	// Token: 0x06000F9E RID: 3998 RVA: 0x0005501C File Offset: 0x0005321C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 3), GameTags.Rocket, null)
		};
	}

	// Token: 0x06000F9F RID: 3999 RVA: 0x00055080 File Offset: 0x00053280
	public override void DoPostConfigureComplete(GameObject go)
	{
		go = BuildingTemplates.ExtendBuildingToClusterCargoBay(go, this.CAPACITY, STORAGEFILTERS.NOT_EDIBLE_SOLIDS, CargoBay.CargoType.Solids);
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MODERATE, 0f, 0f);
	}

	// Token: 0x04000892 RID: 2194
	public const string ID = "SolidCargoBaySmall";

	// Token: 0x04000893 RID: 2195
	public float CAPACITY = 1200f * ROCKETRY.CARGO_CAPACITY_SCALE;
}
