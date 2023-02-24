using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000027 RID: 39
public class BatterySmartConfig : BaseBatteryConfig
{
	// Token: 0x060000AF RID: 175 RVA: 0x0000626C File Offset: 0x0000446C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "BatterySmart";
		int num = 2;
		int num2 = 2;
		int num3 = 30;
		string text2 = "smartbattery_kanim";
		float num4 = 60f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 800f;
		float num6 = 0f;
		float num7 = 0.5f;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = base.CreateBuildingDef(text, num, num2, num3, text2, num4, tier, refined_METALS, num5, num6, num7, TUNING.BUILDINGS.DECOR.PENALTY.TIER2, tier2);
		SoundEventVolumeCache.instance.AddVolume("batterymed_kanim", "Battery_med_rattle", NOISE_POLLUTION.NOISY.TIER2);
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort(BatterySmart.PORT_ID, new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.BATTERYSMART.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.BATTERYSMART.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.BATTERYSMART.LOGIC_PORT_INACTIVE, true, false) };
		return buildingDef;
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x00006312 File Offset: 0x00004512
	public override void DoPostConfigureComplete(GameObject go)
	{
		BatterySmart batterySmart = go.AddOrGet<BatterySmart>();
		batterySmart.capacity = 20000f;
		batterySmart.joulesLostPerSecond = 0.6666667f;
		batterySmart.powerSortOrder = 1000;
		base.DoPostConfigureComplete(go);
	}

	// Token: 0x04000075 RID: 117
	public const string ID = "BatterySmart";
}
