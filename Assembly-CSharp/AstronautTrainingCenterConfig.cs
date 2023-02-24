using System;
using TUNING;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class AstronautTrainingCenterConfig : IBuildingConfig
{
	// Token: 0x0600007F RID: 127 RVA: 0x000052F0 File Offset: 0x000034F0
	public override BuildingDef CreateBuildingDef()
	{
		string text = "AstronautTrainingCenter";
		int num = 5;
		int num2 = 5;
		string text2 = "centrifuge_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 480f;
		buildingDef.ExhaustKilowattsWhenActive = 0.5f;
		buildingDef.SelfHeatKilowattsWhenActive = 4f;
		buildingDef.PowerInputOffset = new CellOffset(-2, 0);
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.Deprecated = true;
		return buildingDef;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00005394 File Offset: 0x00003594
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		Prioritizable.AddRef(go);
		AstronautTrainingCenter astronautTrainingCenter = go.AddOrGet<AstronautTrainingCenter>();
		astronautTrainingCenter.workTime = float.PositiveInfinity;
		astronautTrainingCenter.requiredSkillPerk = Db.Get().SkillPerks.CanTrainToBeAstronaut.Id;
		astronautTrainingCenter.daysToMasterRole = 10f;
		astronautTrainingCenter.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_centrifuge_kanim") };
		astronautTrainingCenter.workLayer = Grid.SceneLayer.BuildingFront;
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00005410 File Offset: 0x00003610
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400005F RID: 95
	public const string ID = "AstronautTrainingCenter";
}
