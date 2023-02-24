using System;
using TUNING;
using UnityEngine;

// Token: 0x02000300 RID: 768
public class SculptureConfig : IBuildingConfig
{
	// Token: 0x06000F50 RID: 3920 RVA: 0x00053684 File Offset: 0x00051884
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Sculpture";
		int num = 1;
		int num2 = 3;
		string text2 = "sculpture_kanim";
		int num3 = 30;
		float num4 = 120f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, new EffectorValues
		{
			amount = 10,
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

	// Token: 0x06000F51 RID: 3921 RVA: 0x00053720 File Offset: 0x00051920
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<BuildingComplete>().isArtable = true;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x06000F52 RID: 3922 RVA: 0x0005373F File Offset: 0x0005193F
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddComponent<Sculpture>().defaultAnimName = "slab";
	}

	// Token: 0x0400087C RID: 2172
	public const string ID = "Sculpture";
}
