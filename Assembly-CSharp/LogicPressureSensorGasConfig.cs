﻿using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000216 RID: 534
public class LogicPressureSensorGasConfig : IBuildingConfig
{
	// Token: 0x06000A96 RID: 2710 RVA: 0x0003BE1C File Offset: 0x0003A01C
	public override BuildingDef CreateBuildingDef()
	{
		string id = LogicPressureSensorGasConfig.ID;
		int num = 1;
		int num2 = 1;
		string text = "switchgaspressure_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, num, num2, text, num3, num4, tier, refined_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.ViewMode = OverlayModes.Logic.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.AlwaysOperational = true;
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.LOGICPRESSURESENSORGAS.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.LOGICPRESSURESENSORGAS.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LOGICPRESSURESENSORGAS.LOGIC_PORT_INACTIVE, true, false) };
		SoundEventVolumeCache.instance.AddVolume("switchgaspressure_kanim", "PowerSwitch_on", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume("switchgaspressure_kanim", "PowerSwitch_off", NOISE_POLLUTION.NOISY.TIER3);
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, LogicPressureSensorGasConfig.ID);
		return buildingDef;
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x0003BF20 File Offset: 0x0003A120
	public override void DoPostConfigureComplete(GameObject go)
	{
		LogicPressureSensor logicPressureSensor = go.AddOrGet<LogicPressureSensor>();
		logicPressureSensor.rangeMin = 0f;
		logicPressureSensor.rangeMax = 20f;
		logicPressureSensor.Threshold = 1f;
		logicPressureSensor.ActivateAboveThreshold = false;
		logicPressureSensor.manuallyControlled = false;
		logicPressureSensor.desiredState = Element.State.Gas;
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x04000638 RID: 1592
	public static string ID = "LogicPressureSensorGas";
}
