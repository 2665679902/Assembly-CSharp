using System;
using TUNING;
using UnityEngine;

// Token: 0x020001DF RID: 479
public class LadderConfig : IBuildingConfig
{
	// Token: 0x06000970 RID: 2416 RVA: 0x00036D3C File Offset: 0x00034F3C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Ladder";
		int num = 1;
		int num2 = 1;
		string text2 = "ladder_kanim";
		int num3 = 10;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] all_MINERALS = MATERIALS.ALL_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_MINERALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
		BuildingTemplates.CreateLadderDef(buildingDef);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.Entombable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.DragBuild = true;
		return buildingDef;
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x00036DC5 File Offset: 0x00034FC5
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		Ladder ladder = go.AddOrGet<Ladder>();
		ladder.upwardsMovementSpeedMultiplier = 1f;
		ladder.downwardsMovementSpeedMultiplier = 1f;
		go.AddOrGet<AnimTileable>();
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x00036DEF File Offset: 0x00034FEF
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040005E5 RID: 1509
	public const string ID = "Ladder";
}
