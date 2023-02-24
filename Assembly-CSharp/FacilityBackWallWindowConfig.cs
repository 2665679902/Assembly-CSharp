﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x02000081 RID: 129
public class FacilityBackWallWindowConfig : IBuildingConfig
{
	// Token: 0x06000265 RID: 613 RVA: 0x00011688 File Offset: 0x0000F888
	public override BuildingDef CreateBuildingDef()
	{
		string text = "FacilityBackWallWindow";
		int num = 1;
		int num2 = 6;
		string text2 = "gravitas_window_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] glasses = MATERIALS.GLASSES;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.NotInTiles;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, glasses, num5, buildLocationRule, DECOR.BONUS.TIER3, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.DefaultAnimState = "off";
		buildingDef.ObjectLayer = ObjectLayer.Backwall;
		buildingDef.SceneLayer = Grid.SceneLayer.Backwall;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x06000266 RID: 614 RVA: 0x00011714 File Offset: 0x0000F914
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<AnimTileable>().objectLayer = ObjectLayer.Backwall;
		go.AddComponent<ZoneTile>();
		go.GetComponent<PrimaryElement>().SetElement(SimHashes.Glass, true);
		go.GetComponent<PrimaryElement>().Temperature = 273f;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Gravitas, false);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0001177B File Offset: 0x0000F97B
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400014E RID: 334
	public const string ID = "FacilityBackWallWindow";
}
