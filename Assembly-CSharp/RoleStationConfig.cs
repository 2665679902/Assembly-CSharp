using System;
using TUNING;
using UnityEngine;

// Token: 0x020002FA RID: 762
public class RoleStationConfig : IBuildingConfig
{
	// Token: 0x06000F35 RID: 3893 RVA: 0x00052C9C File Offset: 0x00050E9C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "RoleStation";
		int num = 2;
		int num2 = 2;
		string text2 = "job_station_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.Deprecated = true;
		return buildingDef;
	}

	// Token: 0x06000F36 RID: 3894 RVA: 0x00052D06 File Offset: 0x00050F06
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		Prioritizable.AddRef(go);
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x00052D1A File Offset: 0x00050F1A
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000869 RID: 2153
	public const string ID = "RoleStation";
}
