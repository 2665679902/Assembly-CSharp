using System;
using TUNING;
using UnityEngine;

// Token: 0x020001E0 RID: 480
public class LadderFastConfig : IBuildingConfig
{
	// Token: 0x06000974 RID: 2420 RVA: 0x00036DFC File Offset: 0x00034FFC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LadderFast";
		int num = 1;
		int num2 = 1;
		string text2 = "ladder_plastic_kanim";
		int num3 = 10;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] plastics = MATERIALS.PLASTICS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, plastics, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
		BuildingTemplates.CreateLadderDef(buildingDef);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.Entombable = false;
		buildingDef.AudioCategory = "Plastic";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.DragBuild = true;
		return buildingDef;
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x00036E85 File Offset: 0x00035085
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		Ladder ladder = go.AddOrGet<Ladder>();
		ladder.upwardsMovementSpeedMultiplier = 1.2f;
		ladder.downwardsMovementSpeedMultiplier = 1.2f;
		go.AddOrGet<AnimTileable>();
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x00036EAF File Offset: 0x000350AF
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040005E6 RID: 1510
	public const string ID = "LadderFast";
}
