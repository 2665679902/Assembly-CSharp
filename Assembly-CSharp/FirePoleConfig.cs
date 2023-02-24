using System;
using TUNING;
using UnityEngine;

// Token: 0x0200012B RID: 299
public class FirePoleConfig : IBuildingConfig
{
	// Token: 0x060005BD RID: 1469 RVA: 0x00025D3C File Offset: 0x00023F3C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "FirePole";
		int num = 1;
		int num2 = 1;
		string text2 = "firepole_kanim";
		int num3 = 10;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
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

	// Token: 0x060005BE RID: 1470 RVA: 0x00025DC5 File Offset: 0x00023FC5
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		Ladder ladder = go.AddOrGet<Ladder>();
		ladder.isPole = true;
		ladder.upwardsMovementSpeedMultiplier = 0.25f;
		ladder.downwardsMovementSpeedMultiplier = 4f;
		go.AddOrGet<AnimTileable>();
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x00025DF6 File Offset: 0x00023FF6
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040003FA RID: 1018
	public const string ID = "FirePole";
}
