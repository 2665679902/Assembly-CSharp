using System;
using TUNING;
using UnityEngine;

// Token: 0x0200002A RID: 42
public class BottleEmptierConfig : IBuildingConfig
{
	// Token: 0x060000BA RID: 186 RVA: 0x0000657C File Offset: 0x0000477C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "BottleEmptier";
		int num = 1;
		int num2 = 3;
		string text2 = "liquidator_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = false;
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		return buildingDef;
	}

	// Token: 0x060000BB RID: 187 RVA: 0x000065E2 File Offset: 0x000047E2
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Prioritizable.AddRef(go);
		Storage storage = go.AddOrGet<Storage>();
		storage.storageFilters = STORAGEFILTERS.LIQUIDS;
		storage.showInUI = true;
		storage.showDescriptor = true;
		storage.capacityKg = 200f;
		go.AddOrGet<TreeFilterable>();
		go.AddOrGet<BottleEmptier>();
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00006621 File Offset: 0x00004821
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400007B RID: 123
	public const string ID = "BottleEmptier";
}
