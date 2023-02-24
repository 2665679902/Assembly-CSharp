using System;
using TUNING;
using UnityEngine;

// Token: 0x020002A8 RID: 680
public class PlanterBoxConfig : IBuildingConfig
{
	// Token: 0x06000D7F RID: 3455 RVA: 0x0004AE34 File Offset: 0x00049034
	public override BuildingDef CreateBuildingDef()
	{
		string text = "PlanterBox";
		int num = 1;
		int num2 = 1;
		string text2 = "planterbox_kanim";
		int num3 = 10;
		float num4 = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] farmable = MATERIALS.FARMABLE;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, farmable, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
		buildingDef.ForegroundLayer = Grid.SceneLayer.BuildingBack;
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.AudioCategory = "Glass";
		buildingDef.AudioSize = "large";
		return buildingDef;
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x0004AEA8 File Offset: 0x000490A8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Storage storage = go.AddOrGet<Storage>();
		PlantablePlot plantablePlot = go.AddOrGet<PlantablePlot>();
		plantablePlot.AddDepositTag(GameTags.CropSeed);
		plantablePlot.SetFertilizationFlags(true, false);
		go.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.Farm;
		BuildingTemplates.CreateDefaultStorage(go, false);
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		go.AddOrGet<DropAllWorkable>();
		go.AddOrGet<PlanterBox>();
		go.AddOrGet<AnimTileable>();
		Prioritizable.AddRef(go);
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x0004AF10 File Offset: 0x00049110
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040007D1 RID: 2001
	public const string ID = "PlanterBox";
}
