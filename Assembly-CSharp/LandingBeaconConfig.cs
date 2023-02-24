using System;
using TUNING;
using UnityEngine;

// Token: 0x020001E2 RID: 482
public class LandingBeaconConfig : IBuildingConfig
{
	// Token: 0x0600097D RID: 2429 RVA: 0x00036FBE File Offset: 0x000351BE
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x00036FC8 File Offset: 0x000351C8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LandingBeacon";
		int num = 1;
		int num2 = 3;
		string text2 = "landing_beacon_kanim";
		int num3 = 1000;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, tier2, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.OverheatTemperature = 398.15f;
		buildingDef.Floodable = false;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.RequiresPowerInput = false;
		buildingDef.CanMove = false;
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 60f;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		return buildingDef;
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x00037063 File Offset: 0x00035263
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGetDef<LandingBeacon.Def>();
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x00037084 File Offset: 0x00035284
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x00037086 File Offset: 0x00035286
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x00037088 File Offset: 0x00035288
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040005E7 RID: 1511
	public const string ID = "LandingBeacon";

	// Token: 0x040005E8 RID: 1512
	public const int LANDING_ACCURACY = 3;
}
