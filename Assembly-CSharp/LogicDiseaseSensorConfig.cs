using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000203 RID: 515
public class LogicDiseaseSensorConfig : IBuildingConfig
{
	// Token: 0x06000A21 RID: 2593 RVA: 0x0003A894 File Offset: 0x00038A94
	public override BuildingDef CreateBuildingDef()
	{
		string id = LogicDiseaseSensorConfig.ID;
		int num = 1;
		int num2 = 1;
		string text = "diseasesensor_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] array = new float[]
		{
			TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0[0],
			TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER1[0]
		};
		string[] array2 = new string[] { "RefinedMetal", "Plastic" };
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, num, num2, text, num3, num4, array, array2, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.ViewMode = OverlayModes.Logic.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.AlwaysOperational = true;
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.LOGICDISEASESENSOR.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.LOGICDISEASESENSOR.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LOGICDISEASESENSOR.LOGIC_PORT_INACTIVE, true, false) };
		SoundEventVolumeCache.instance.AddVolume("diseasesensor_kanim", "PowerSwitch_on", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume("diseasesensor_kanim", "PowerSwitch_off", NOISE_POLLUTION.NOISY.TIER3);
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, LogicDiseaseSensorConfig.ID);
		return buildingDef;
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x0003A9BD File Offset: 0x00038BBD
	public override void DoPostConfigureComplete(GameObject go)
	{
		LogicDiseaseSensor logicDiseaseSensor = go.AddOrGet<LogicDiseaseSensor>();
		logicDiseaseSensor.Threshold = 0f;
		logicDiseaseSensor.ActivateAboveThreshold = true;
		logicDiseaseSensor.manuallyControlled = false;
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x04000622 RID: 1570
	public static string ID = "LogicDiseaseSensor";
}
