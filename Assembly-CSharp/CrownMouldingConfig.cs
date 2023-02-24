﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x0200005B RID: 91
public class CrownMouldingConfig : IBuildingConfig
{
	// Token: 0x06000195 RID: 405 RVA: 0x0000BA88 File Offset: 0x00009C88
	public override BuildingDef CreateBuildingDef()
	{
		string text = "CrownMoulding";
		int num = 1;
		int num2 = 1;
		string text2 = "crown_moulding_kanim";
		int num3 = 10;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnCeiling;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, new EffectorValues
		{
			amount = 5,
			radius = 3
		}, none, 0.2f);
		buildingDef.DefaultAnimState = "S_U";
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Decor.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		return buildingDef;
	}

	// Token: 0x06000196 RID: 406 RVA: 0x0000BB1C File Offset: 0x00009D1C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
		go.AddOrGet<AnimTileable>();
	}

	// Token: 0x06000197 RID: 407 RVA: 0x0000BB36 File Offset: 0x00009D36
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040000E6 RID: 230
	public const string ID = "CrownMoulding";
}
