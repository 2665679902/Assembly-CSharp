﻿using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000205 RID: 517
public class LogicElementSensorGasConfig : IBuildingConfig
{
	// Token: 0x06000A2A RID: 2602 RVA: 0x0003AB4C File Offset: 0x00038D4C
	public override BuildingDef CreateBuildingDef()
	{
		string id = LogicElementSensorGasConfig.ID;
		int num = 1;
		int num2 = 1;
		string text = "world_element_sensor_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, num, num2, text, num3, num4, tier, refined_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.Floodable = true;
		buildingDef.Entombable = true;
		buildingDef.ViewMode = OverlayModes.Logic.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.LOGICELEMENTSENSORGAS.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.LOGICELEMENTSENSORGAS.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LOGICELEMENTSENSORGAS.LOGIC_PORT_INACTIVE, true, false) };
		SoundEventVolumeCache.instance.AddVolume("world_element_sensor_kanim", "PowerSwitch_on", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume("world_element_sensor_kanim", "PowerSwitch_off", NOISE_POLLUTION.NOISY.TIER3);
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, LogicElementSensorGasConfig.ID);
		return buildingDef;
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x0003AC48 File Offset: 0x00038E48
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<Filterable>().filterElementState = Filterable.ElementState.Gas;
		LogicElementSensor logicElementSensor = go.AddOrGet<LogicElementSensor>();
		logicElementSensor.manuallyControlled = false;
		logicElementSensor.desiredState = Element.State.Gas;
	}

	// Token: 0x04000625 RID: 1573
	public static string ID = "LogicElementSensorGas";
}
