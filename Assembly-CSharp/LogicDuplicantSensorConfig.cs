using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000204 RID: 516
public class LogicDuplicantSensorConfig : IBuildingConfig
{
	// Token: 0x06000A25 RID: 2597 RVA: 0x0003AA04 File Offset: 0x00038C04
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("LogicDuplicantSensor", 1, 1, "presence_sensor_kanim", 30, 30f, TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0, MATERIALS.REFINED_METALS, 1600f, BuildLocationRule.OnFoundationRotatable, TUNING.BUILDINGS.DECOR.PENALTY.TIER0, NOISE_POLLUTION.NOISY.TIER0, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.Entombable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.ViewMode = OverlayModes.Logic.ID;
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.AlwaysOperational = true;
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.LOGICDUPLICANTSENSOR.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.LOGICDUPLICANTSENSOR.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LOGICDUPLICANTSENSOR.LOGIC_PORT_INACTIVE, true, false) };
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, "LogicDuplicantSensor");
		return buildingDef;
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x0003AADB File Offset: 0x00038CDB
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		LogicDuplicantSensorConfig.AddVisualizer(go, true);
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x0003AAE4 File Offset: 0x00038CE4
	public override void DoPostConfigureComplete(GameObject go)
	{
		LogicDuplicantSensor logicDuplicantSensor = go.AddOrGet<LogicDuplicantSensor>();
		logicDuplicantSensor.defaultState = false;
		logicDuplicantSensor.manuallyControlled = false;
		logicDuplicantSensor.pickupRange = 4;
		LogicDuplicantSensorConfig.AddVisualizer(go, false);
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x0003AB18 File Offset: 0x00038D18
	private static void AddVisualizer(GameObject prefab, bool movable)
	{
		StationaryChoreRangeVisualizer stationaryChoreRangeVisualizer = prefab.AddOrGet<StationaryChoreRangeVisualizer>();
		stationaryChoreRangeVisualizer.x = -2;
		stationaryChoreRangeVisualizer.y = 0;
		stationaryChoreRangeVisualizer.width = 5;
		stationaryChoreRangeVisualizer.height = 5;
		stationaryChoreRangeVisualizer.movable = movable;
	}

	// Token: 0x04000623 RID: 1571
	public const string ID = "LogicDuplicantSensor";

	// Token: 0x04000624 RID: 1572
	private const int RANGE = 4;
}
