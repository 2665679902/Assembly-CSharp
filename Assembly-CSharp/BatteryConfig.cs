using System;
using TUNING;
using UnityEngine;

// Token: 0x02000024 RID: 36
public class BatteryConfig : BaseBatteryConfig
{
	// Token: 0x060000A3 RID: 163 RVA: 0x00005F9C File Offset: 0x0000419C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Battery";
		int num = 1;
		int num2 = 2;
		int num3 = 30;
		string text2 = "batterysm_kanim";
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 800f;
		float num6 = 0.25f;
		float num7 = 1f;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = base.CreateBuildingDef(text, num, num2, num3, text2, num4, tier, all_METALS, num5, num6, num7, BUILDINGS.DECOR.PENALTY.TIER1, none);
		buildingDef.Breakable = true;
		SoundEventVolumeCache.instance.AddVolume("batterysm_kanim", "Battery_rattle", NOISE_POLLUTION.NOISY.TIER1);
		return buildingDef;
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00006007 File Offset: 0x00004207
	public override void DoPostConfigureComplete(GameObject go)
	{
		Battery battery = go.AddOrGet<Battery>();
		battery.capacity = 10000f;
		battery.joulesLostPerSecond = 1.6666666f;
		base.DoPostConfigureComplete(go);
	}

	// Token: 0x04000070 RID: 112
	public const string ID = "Battery";
}
