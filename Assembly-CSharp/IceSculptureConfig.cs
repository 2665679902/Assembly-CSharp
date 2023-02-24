using System;
using TUNING;
using UnityEngine;

// Token: 0x020001C6 RID: 454
public class IceSculptureConfig : IBuildingConfig
{
	// Token: 0x060008E8 RID: 2280 RVA: 0x00034E04 File Offset: 0x00033004
	public override BuildingDef CreateBuildingDef()
	{
		string text = "IceSculpture";
		int num = 2;
		int num2 = 2;
		string text2 = "icesculpture_kanim";
		int num3 = 10;
		float num4 = 120f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] array = new string[] { "Ice" };
		float num5 = 273.15f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, array, num5, buildLocationRule, new EffectorValues
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

	// Token: 0x060008E9 RID: 2281 RVA: 0x00034EA9 File Offset: 0x000330A9
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<BuildingComplete>().isArtable = true;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x00034EC8 File Offset: 0x000330C8
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddComponent<Sculpture>().defaultAnimName = "slab";
	}

	// Token: 0x0400059F RID: 1439
	public const string ID = "IceSculpture";
}
