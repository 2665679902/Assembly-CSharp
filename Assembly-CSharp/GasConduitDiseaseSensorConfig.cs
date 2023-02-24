﻿using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000042 RID: 66
public class GasConduitDiseaseSensorConfig : ConduitSensorConfig
{
	// Token: 0x1700000E RID: 14
	// (get) Token: 0x06000135 RID: 309 RVA: 0x0000917C File Offset: 0x0000737C
	protected override ConduitType ConduitType
	{
		get
		{
			return ConduitType.Gas;
		}
	}

	// Token: 0x06000136 RID: 310 RVA: 0x00009180 File Offset: 0x00007380
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = base.CreateBuildingDef(GasConduitDiseaseSensorConfig.ID, "gas_germs_sensor_kanim", new float[]
		{
			TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0[0],
			TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER1[0]
		}, new string[] { "RefinedMetal", "Plastic" }, new List<LogicPorts.Port> { LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.GASCONDUITDISEASESENSOR.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.GASCONDUITDISEASESENSOR.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.GASCONDUITDISEASESENSOR.LOGIC_PORT_INACTIVE, true, false) });
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.GasVentIDs, GasConduitDiseaseSensorConfig.ID);
		return buildingDef;
	}

	// Token: 0x06000137 RID: 311 RVA: 0x00009218 File Offset: 0x00007418
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

	// Token: 0x040000A6 RID: 166
	public static string ID = "GasConduitDiseaseSensor";
}
