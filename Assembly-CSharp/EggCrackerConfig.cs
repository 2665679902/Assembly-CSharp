using System;
using TUNING;
using UnityEngine;

// Token: 0x0200006A RID: 106
[EntityConfigOrder(2)]
public class EggCrackerConfig : IBuildingConfig
{
	// Token: 0x060001D8 RID: 472 RVA: 0x0000D17C File Offset: 0x0000B37C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "EggCracker";
		int num = 2;
		int num2 = 2;
		string text2 = "egg_cracker_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER0, none, 0.2f);
		buildingDef.AudioCategory = "Metal";
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.ForegroundLayer = Grid.SceneLayer.BuildingFront;
		return buildingDef;
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x0000D1E0 File Offset: 0x0000B3E0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<DropAllWorkable>();
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		go.AddOrGet<KBatchedAnimController>().SetSymbolVisiblity("snapto_egg", false);
		ComplexFabricator complexFabricator = go.AddOrGet<ComplexFabricator>();
		complexFabricator.labelByResult = false;
		complexFabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
		complexFabricator.duplicantOperated = true;
		go.AddOrGet<FabricatorIngredientStatusManager>();
		go.AddOrGet<CopyBuildingSettings>();
		Workable workable = go.AddOrGet<ComplexFabricatorWorkable>();
		BuildingTemplates.CreateComplexFabricatorStorage(go, complexFabricator);
		workable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_egg_cracker_kanim") };
		complexFabricator.outputOffset = new Vector3(1f, 1f, 0f);
		Prioritizable.AddRef(go);
		go.AddOrGet<EggCracker>();
	}

	// Token: 0x060001DA RID: 474 RVA: 0x0000D291 File Offset: 0x0000B491
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000118 RID: 280
	public const string ID = "EggCracker";
}
