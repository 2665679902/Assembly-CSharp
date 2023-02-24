using System;
using TUNING;
using UnityEngine;

// Token: 0x020001B3 RID: 435
public class GravitasLabLightConfig : IBuildingConfig
{
	// Token: 0x06000884 RID: 2180 RVA: 0x00032768 File Offset: 0x00030968
	public override BuildingDef CreateBuildingDef()
	{
		string text = "GravitasLabLight";
		int num = 1;
		int num2 = 1;
		string text2 = "gravitas_lab_light_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 2400f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnCeiling;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.ShowInBuildMenu = false;
		buildingDef.Entombable = false;
		buildingDef.Floodable = false;
		buildingDef.Invincible = true;
		buildingDef.AudioCategory = "Metal";
		return buildingDef;
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x000327D5 File Offset: 0x000309D5
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddTag(GameTags.Gravitas);
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x000327E2 File Offset: 0x000309E2
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000559 RID: 1369
	public const string ID = "GravitasLabLight";
}
