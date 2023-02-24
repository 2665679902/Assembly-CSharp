using System;
using TUNING;
using UnityEngine;

// Token: 0x02000054 RID: 84
public class CornerMouldingConfig : IBuildingConfig
{
	// Token: 0x06000172 RID: 370 RVA: 0x0000ADEC File Offset: 0x00008FEC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "CornerMoulding";
		int num = 1;
		int num2 = 1;
		string text2 = "corner_tile_kanim";
		int num3 = 10;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.InCorner;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, new EffectorValues
		{
			amount = 5,
			radius = 3
		}, none, 0.2f);
		buildingDef.DefaultAnimState = "corner";
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Decor.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		return buildingDef;
	}

	// Token: 0x06000173 RID: 371 RVA: 0x0000AE87 File Offset: 0x00009087
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x06000174 RID: 372 RVA: 0x0000AE9A File Offset: 0x0000909A
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040000D9 RID: 217
	public const string ID = "CornerMoulding";
}
