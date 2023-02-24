﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x020001AF RID: 431
public class GravitasContainerConfig : IBuildingConfig
{
	// Token: 0x06000861 RID: 2145 RVA: 0x00031C88 File Offset: 0x0002FE88
	public override BuildingDef CreateBuildingDef()
	{
		string text = "GravitasContainer";
		int num = 2;
		int num2 = 2;
		string text2 = "gravitas_container_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 2400f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.ShowInBuildMenu = false;
		buildingDef.Entombable = false;
		buildingDef.Floodable = false;
		buildingDef.Invincible = true;
		buildingDef.AudioCategory = "Metal";
		return buildingDef;
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x00031CF5 File Offset: 0x0002FEF5
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddTag(GameTags.Gravitas);
		go.AddOrGet<KBatchedAnimController>().sceneLayer = Grid.SceneLayer.Building;
		Prioritizable.AddRef(go);
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x00031D18 File Offset: 0x0002FF18
	public override void DoPostConfigureComplete(GameObject go)
	{
		PajamaDispenser pajamaDispenser = go.AddComponent<PajamaDispenser>();
		pajamaDispenser.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_gravitas_container_kanim") };
		pajamaDispenser.SetWorkTime(30f);
	}

	// Token: 0x0400054B RID: 1355
	public const string ID = "GravitasContainer";

	// Token: 0x0400054C RID: 1356
	private const float WORK_TIME = 1.5f;
}
