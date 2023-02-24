using System;
using TUNING;
using UnityEngine;

// Token: 0x0200022B RID: 555
public class MarbleSculptureConfig : IBuildingConfig
{
	// Token: 0x06000AF7 RID: 2807 RVA: 0x0003DDE8 File Offset: 0x0003BFE8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "MarbleSculpture";
		int num = 2;
		int num2 = 3;
		string text2 = "sculpture_marble_kanim";
		int num3 = 10;
		float num4 = 120f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] precious_ROCKS = MATERIALS.PRECIOUS_ROCKS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, precious_ROCKS, num5, buildLocationRule, new EffectorValues
		{
			amount = 20,
			radius = 8
		}, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.ViewMode = OverlayModes.Decor.ID;
		buildingDef.DefaultAnimState = "slab";
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		return buildingDef;
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x0003DE84 File Offset: 0x0003C084
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<BuildingComplete>().isArtable = true;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x0003DEA3 File Offset: 0x0003C0A3
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddComponent<Sculpture>().defaultAnimName = "slab";
	}

	// Token: 0x04000671 RID: 1649
	public const string ID = "MarbleSculpture";
}
