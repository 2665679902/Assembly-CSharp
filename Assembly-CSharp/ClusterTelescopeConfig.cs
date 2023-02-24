using System;
using TUNING;
using UnityEngine;

// Token: 0x0200003B RID: 59
public class ClusterTelescopeConfig : IBuildingConfig
{
	// Token: 0x06000113 RID: 275 RVA: 0x00008648 File Offset: 0x00006848
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00008650 File Offset: 0x00006850
	public override BuildingDef CreateBuildingDef()
	{
		string text = "ClusterTelescope";
		int num = 3;
		int num2 = 3;
		string text2 = "telescope_low_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.ExhaustKilowattsWhenActive = 0.125f;
		buildingDef.SelfHeatKilowattsWhenActive = 0f;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		return buildingDef;
	}

	// Token: 0x06000115 RID: 277 RVA: 0x000086E0 File Offset: 0x000068E0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.ScienceBuilding, false);
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		Prioritizable.AddRef(go);
		ClusterTelescope.Def def = go.AddOrGetDef<ClusterTelescope.Def>();
		def.clearScanCellRadius = 5;
		def.workableOverrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_telescope_low_kanim") };
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 1000f;
		storage.showInUI = true;
		go.AddOrGetDef<PoweredController.Def>();
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00008758 File Offset: 0x00006958
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000098 RID: 152
	public const string ID = "ClusterTelescope";
}
