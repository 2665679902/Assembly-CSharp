using System;
using TUNING;
using UnityEngine;

// Token: 0x0200015F RID: 351
public class FlowerVaseConfig : IBuildingConfig
{
	// Token: 0x060006CB RID: 1739 RVA: 0x0002BA80 File Offset: 0x00029C80
	public override BuildingDef CreateBuildingDef()
	{
		string text = "FlowerVase";
		int num = 1;
		int num2 = 1;
		string text2 = "flowervase_kanim";
		int num3 = 10;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Decor.ID;
		buildingDef.AudioCategory = "Glass";
		buildingDef.AudioSize = "large";
		return buildingDef;
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x0002BAF5 File Offset: 0x00029CF5
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<Storage>();
		Prioritizable.AddRef(go);
		go.AddOrGet<PlantablePlot>().AddDepositTag(GameTags.DecorSeed);
		go.AddOrGet<FlowerVase>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x0002BB2C File Offset: 0x00029D2C
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040004AE RID: 1198
	public const string ID = "FlowerVase";
}
