using System;
using TUNING;
using UnityEngine;

// Token: 0x02000164 RID: 356
public class FlyingCreatureBaitConfig : IBuildingConfig
{
	// Token: 0x060006DF RID: 1759 RVA: 0x0002C098 File Offset: 0x0002A298
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("FlyingCreatureBait", 1, 2, "airborne_critter_bait_kanim", 10, 10f, new float[] { 50f, 10f }, new string[] { "Metal", "FlyingCritterEdible" }, 1600f, BuildLocationRule.Anywhere, BUILDINGS.DECOR.PENALTY.TIER2, NOISE_POLLUTION.NOISY.TIER0, 0.2f);
		buildingDef.AudioCategory = "Metal";
		return buildingDef;
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x0002C109 File Offset: 0x0002A309
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<CreatureBait>();
		go.AddTag(GameTags.OneTimeUseLure);
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x0002C11D File Offset: 0x0002A31D
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x0002C11F File Offset: 0x0002A31F
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x0002C124 File Offset: 0x0002A324
	public override void DoPostConfigureComplete(GameObject go)
	{
		BuildingTemplates.DoPostConfigure(go);
		SymbolOverrideControllerUtil.AddToPrefab(go);
		go.AddOrGet<SymbolOverrideController>().applySymbolOverridesEveryFrame = true;
		Lure.Def def = go.AddOrGetDef<Lure.Def>();
		def.lurePoints = new CellOffset[]
		{
			new CellOffset(0, 0)
		};
		def.radius = 32;
		Prioritizable.AddRef(go);
	}

	// Token: 0x040004B4 RID: 1204
	public const string ID = "FlyingCreatureBait";
}
