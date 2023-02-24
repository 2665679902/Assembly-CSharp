using System;
using TUNING;
using UnityEngine;

// Token: 0x02000160 RID: 352
public class FlowerVaseHangingConfig : IBuildingConfig
{
	// Token: 0x060006CF RID: 1743 RVA: 0x0002BB38 File Offset: 0x00029D38
	public override BuildingDef CreateBuildingDef()
	{
		string text = "FlowerVaseHanging";
		int num = 1;
		int num2 = 2;
		string text2 = "flowervase_hanging_basic_kanim";
		int num3 = 10;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnCeiling;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Decor.ID;
		buildingDef.AudioCategory = "Glass";
		buildingDef.AudioSize = "large";
		buildingDef.GenerateOffsets(1, 1);
		return buildingDef;
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x0002BBB8 File Offset: 0x00029DB8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<Storage>();
		Prioritizable.AddRef(go);
		PlantablePlot plantablePlot = go.AddOrGet<PlantablePlot>();
		plantablePlot.AddDepositTag(GameTags.DecorSeed);
		plantablePlot.occupyingObjectVisualOffset = new Vector3(0f, -0.25f, 0f);
		go.AddOrGet<FlowerVase>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x0002BC14 File Offset: 0x00029E14
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040004AF RID: 1199
	public const string ID = "FlowerVaseHanging";
}
