﻿using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000043 RID: 67
public class LiquidConduitDiseaseSensorConfig : ConduitSensorConfig
{
	// Token: 0x1700000F RID: 15
	// (get) Token: 0x0600013A RID: 314 RVA: 0x00009282 File Offset: 0x00007482
	protected override ConduitType ConduitType
	{
		get
		{
			return ConduitType.Liquid;
		}
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00009288 File Offset: 0x00007488
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = base.CreateBuildingDef(LiquidConduitDiseaseSensorConfig.ID, "liquid_germs_sensor_kanim", new float[]
		{
			TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0[0],
			TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER1[0]
		}, new string[] { "RefinedMetal", "Plastic" }, new List<LogicPorts.Port> { LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.LIQUIDCONDUITDISEASESENSOR.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.LIQUIDCONDUITDISEASESENSOR.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LIQUIDCONDUITDISEASESENSOR.LOGIC_PORT_INACTIVE, true, false) });
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.LiquidVentIDs, LiquidConduitDiseaseSensorConfig.ID);
		return buildingDef;
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00009320 File Offset: 0x00007520
	public override void DoPostConfigureComplete(GameObject go)
	{
		base.DoPostConfigureComplete(go);
		ConduitDiseaseSensor conduitDiseaseSensor = go.AddComponent<ConduitDiseaseSensor>();
		conduitDiseaseSensor.conduitType = this.ConduitType;
		conduitDiseaseSensor.Threshold = 0f;
		conduitDiseaseSensor.ActivateAboveThreshold = true;
		conduitDiseaseSensor.manuallyControlled = false;
		conduitDiseaseSensor.defaultState = false;
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x040000A7 RID: 167
	public static string ID = "LiquidConduitDiseaseSensor";
}
