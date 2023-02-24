using System;
using TUNING;
using UnityEngine;

// Token: 0x02000309 RID: 777
public class SmallSculptureConfig : IBuildingConfig
{
	// Token: 0x06000F79 RID: 3961 RVA: 0x00054444 File Offset: 0x00052644
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SmallSculpture";
		int num = 1;
		int num2 = 2;
		string text2 = "sculpture_1x2_kanim";
		int num3 = 10;
		float num4 = 60f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, new EffectorValues
		{
			amount = 5,
			radius = 4
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

	// Token: 0x06000F7A RID: 3962 RVA: 0x000544DF File Offset: 0x000526DF
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<BuildingComplete>().isArtable = true;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x000544FE File Offset: 0x000526FE
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddComponent<Sculpture>().defaultAnimName = "slab";
	}

	// Token: 0x04000883 RID: 2179
	public const string ID = "SmallSculpture";
}
