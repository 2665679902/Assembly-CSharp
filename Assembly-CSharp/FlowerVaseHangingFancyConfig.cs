using System;
using TUNING;
using UnityEngine;

// Token: 0x02000161 RID: 353
public class FlowerVaseHangingFancyConfig : IBuildingConfig
{
	// Token: 0x060006D3 RID: 1747 RVA: 0x0002BC20 File Offset: 0x00029E20
	public override BuildingDef CreateBuildingDef()
	{
		string text = "FlowerVaseHangingFancy";
		int num = 1;
		int num2 = 2;
		string text2 = "flowervase_hanging_kanim";
		int num3 = 10;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] transparents = MATERIALS.TRANSPARENTS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnCeiling;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, transparents, num5, buildLocationRule, new EffectorValues
		{
			amount = BUILDINGS.DECOR.BONUS.TIER1.amount,
			radius = BUILDINGS.DECOR.BONUS.TIER3.radius
		}, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Decor.ID;
		buildingDef.AudioCategory = "Glass";
		buildingDef.AudioSize = "large";
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingBack;
		buildingDef.ForegroundLayer = Grid.SceneLayer.BuildingUse;
		buildingDef.GenerateOffsets(1, 1);
		return buildingDef;
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x0002BCD4 File Offset: 0x00029ED4
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<Storage>();
		Prioritizable.AddRef(go);
		PlantablePlot plantablePlot = go.AddOrGet<PlantablePlot>();
		plantablePlot.AddDepositTag(GameTags.DecorSeed);
		plantablePlot.plantLayer = Grid.SceneLayer.BuildingUse;
		plantablePlot.occupyingObjectVisualOffset = new Vector3(0f, -0.45f, 0f);
		go.AddOrGet<FlowerVase>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x0002BD38 File Offset: 0x00029F38
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040004B0 RID: 1200
	public const string ID = "FlowerVaseHangingFancy";
}
