using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001B5 RID: 437
public class HEPBatteryConfig : IBuildingConfig
{
	// Token: 0x0600088D RID: 2189 RVA: 0x00032907 File Offset: 0x00030B07
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x00032910 File Offset: 0x00030B10
	public override BuildingDef CreateBuildingDef()
	{
		string text = "HEPBattery";
		int num = 3;
		int num2 = 3;
		string text2 = "radbolt_battery_kanim";
		int num3 = 30;
		float num4 = 120f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.PENALTY.TIER2, none, 0.2f);
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Radiation.ID;
		buildingDef.UseHighEnergyParticleInputPort = true;
		buildingDef.HighEnergyParticleInputOffset = new CellOffset(0, 1);
		buildingDef.UseHighEnergyParticleOutputPort = true;
		buildingDef.HighEnergyParticleOutputOffset = new CellOffset(0, 2);
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(0, 0);
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.ExhaustKilowattsWhenActive = 0.25f;
		buildingDef.SelfHeatKilowattsWhenActive = 1f;
		buildingDef.AddLogicPowerPort = true;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.RadiationIDs, "HEPBattery");
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort("HEP_STORAGE", new CellOffset(1, 1), STRINGS.BUILDINGS.PREFABS.HEPBATTERY.LOGIC_PORT_STORAGE, STRINGS.BUILDINGS.PREFABS.HEPBATTERY.LOGIC_PORT_STORAGE_ACTIVE, STRINGS.BUILDINGS.PREFABS.HEPBATTERY.LOGIC_PORT_STORAGE_INACTIVE, false, false) };
		buildingDef.LogicInputPorts = new List<LogicPorts.Port> { LogicPorts.Port.InputPort(HEPBattery.FIRE_PORT_ID, new CellOffset(0, 2), STRINGS.BUILDINGS.PREFABS.HEPBATTERY.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.HEPBATTERY.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.HEPBATTERY.LOGIC_PORT_INACTIVE, false, false) };
		return buildingDef;
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x00032A84 File Offset: 0x00030C84
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Prioritizable.AddRef(go);
		HighEnergyParticleStorage highEnergyParticleStorage = go.AddOrGet<HighEnergyParticleStorage>();
		highEnergyParticleStorage.capacity = 1000f;
		highEnergyParticleStorage.autoStore = true;
		highEnergyParticleStorage.PORT_ID = "HEP_STORAGE";
		highEnergyParticleStorage.showCapacityStatusItem = true;
		highEnergyParticleStorage.showCapacityAsMainStatus = true;
		go.AddOrGet<LoopingSounds>();
		HEPBattery.Def def = go.AddOrGetDef<HEPBattery.Def>();
		def.minLaunchInterval = 1f;
		def.minSlider = 0f;
		def.maxSlider = 100f;
		def.particleDecayRate = 0.5f;
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x00032AFF File Offset: 0x00030CFF
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400055B RID: 1371
	public const string ID = "HEPBattery";

	// Token: 0x0400055C RID: 1372
	public const float MIN_LAUNCH_INTERVAL = 1f;

	// Token: 0x0400055D RID: 1373
	public const int MIN_SLIDER = 0;

	// Token: 0x0400055E RID: 1374
	public const int MAX_SLIDER = 100;

	// Token: 0x0400055F RID: 1375
	public const float HEP_CAPACITY = 1000f;

	// Token: 0x04000560 RID: 1376
	public const float DISABLED_DECAY_RATE = 0.5f;

	// Token: 0x04000561 RID: 1377
	public const string STORAGE_PORT_ID = "HEP_STORAGE";

	// Token: 0x04000562 RID: 1378
	public const string FIRE_PORT_ID = "HEP_FIRE";
}
