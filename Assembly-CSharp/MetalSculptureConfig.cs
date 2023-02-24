using System;
using TUNING;
using UnityEngine;

// Token: 0x0200023B RID: 571
public class MetalSculptureConfig : IBuildingConfig
{
	// Token: 0x06000B43 RID: 2883 RVA: 0x0003FC34 File Offset: 0x0003DE34
	public override BuildingDef CreateBuildingDef()
	{
		string text = "MetalSculpture";
		int num = 1;
		int num2 = 3;
		string text2 = "sculpture_metal_kanim";
		int num3 = 10;
		float num4 = 120f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, new EffectorValues
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

	// Token: 0x06000B44 RID: 2884 RVA: 0x0003FCD0 File Offset: 0x0003DED0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<BuildingComplete>().isArtable = true;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x0003FCEF File Offset: 0x0003DEEF
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddComponent<Sculpture>().defaultAnimName = "slab";
	}

	// Token: 0x040006B0 RID: 1712
	public const string ID = "MetalSculpture";
}
