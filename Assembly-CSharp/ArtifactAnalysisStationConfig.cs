using System;
using TUNING;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class ArtifactAnalysisStationConfig : IBuildingConfig
{
	// Token: 0x06000075 RID: 117 RVA: 0x00005024 File Offset: 0x00003224
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x0000502C File Offset: 0x0000322C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "ArtifactAnalysisStation";
		int num = 4;
		int num2 = 4;
		string text2 = "artifact_analysis_kanim";
		int num3 = 30;
		float num4 = 60f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 2400f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER6;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER2, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 480f;
		buildingDef.SelfHeatKilowattsWhenActive = 1f;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.AudioSize = "large";
		return buildingDef;
	}

	// Token: 0x06000077 RID: 119 RVA: 0x000050B0 File Offset: 0x000032B0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<DropAllWorkable>();
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		go.AddOrGetDef<ArtifactAnalysisStation.Def>();
		go.AddOrGet<ArtifactAnalysisStationWorkable>();
		Prioritizable.AddRef(go);
		Storage storage = go.AddOrGet<Storage>();
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
		manualDeliveryKG.RequestedItemTag = GameTags.CharmedArtifact;
		manualDeliveryKG.refillMass = 1f;
		manualDeliveryKG.MinimumMass = 1f;
		manualDeliveryKG.capacity = 1f;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x0000513D File Offset: 0x0000333D
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400005C RID: 92
	public const string ID = "ArtifactAnalysisStation";

	// Token: 0x0400005D RID: 93
	public const float WORK_TIME = 150f;
}
