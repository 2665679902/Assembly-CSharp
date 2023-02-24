using System;
using TUNING;
using UnityEngine;

// Token: 0x02000025 RID: 37
public class BatteryMediumConfig : BaseBatteryConfig
{
	// Token: 0x060000A6 RID: 166 RVA: 0x00006034 File Offset: 0x00004234
	public override BuildingDef CreateBuildingDef()
	{
		string text = "BatteryMedium";
		int num = 2;
		int num2 = 2;
		int num3 = 30;
		string text2 = "batterymed_kanim";
		float num4 = 60f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 800f;
		float num6 = 0.25f;
		float num7 = 1f;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = base.CreateBuildingDef(text, num, num2, num3, text2, num4, tier, all_METALS, num5, num6, num7, BUILDINGS.DECOR.PENALTY.TIER2, tier2);
		SoundEventVolumeCache.instance.AddVolume("batterymed_kanim", "Battery_med_rattle", NOISE_POLLUTION.NOISY.TIER2);
		return buildingDef;
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00006098 File Offset: 0x00004298
	public override void DoPostConfigureComplete(GameObject go)
	{
		Battery battery = go.AddOrGet<Battery>();
		battery.capacity = 40000f;
		battery.joulesLostPerSecond = 3.3333333f;
		base.DoPostConfigureComplete(go);
	}

	// Token: 0x04000071 RID: 113
	public const string ID = "BatteryMedium";
}
