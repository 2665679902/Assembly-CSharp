using System;
using TUNING;
using UnityEngine;

// Token: 0x02000162 RID: 354
public class FlowerVaseWallConfig : IBuildingConfig
{
	// Token: 0x060006D7 RID: 1751 RVA: 0x0002BD44 File Offset: 0x00029F44
	public override BuildingDef CreateBuildingDef()
	{
		string text = "FlowerVaseWall";
		int num = 1;
		int num2 = 1;
		string text2 = "flowervase_wall_kanim";
		int num3 = 10;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnWall;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Decor.ID;
		buildingDef.AudioCategory = "Glass";
		buildingDef.AudioSize = "large";
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		return buildingDef;
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x0002BDC0 File Offset: 0x00029FC0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<Storage>();
		Prioritizable.AddRef(go);
		PlantablePlot plantablePlot = go.AddOrGet<PlantablePlot>();
		plantablePlot.AddDepositTag(GameTags.DecorSeed);
		plantablePlot.occupyingObjectVisualOffset = new Vector3(0f, -0.25f, 0f);
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x0002BE15 File Offset: 0x0002A015
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040004B1 RID: 1201
	public const string ID = "FlowerVaseWall";
}
