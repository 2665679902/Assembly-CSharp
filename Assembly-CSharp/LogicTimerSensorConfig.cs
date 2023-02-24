using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000220 RID: 544
public class LogicTimerSensorConfig : IBuildingConfig
{
	// Token: 0x06000AC3 RID: 2755 RVA: 0x0003CB70 File Offset: 0x0003AD70
	public override BuildingDef CreateBuildingDef()
	{
		string id = LogicTimerSensorConfig.ID;
		int num = 1;
		int num2 = 1;
		string text = "time_sensor_kanim";
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
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.LOGICTIMERSENSOR.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.LOGICTIMERSENSOR.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LOGICTIMERSENSOR.LOGIC_PORT_INACTIVE, true, false) };
		SoundEventVolumeCache.instance.AddVolume("switchgaspressure_kanim", "PowerSwitch_on", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume("switchgaspressure_kanim", "PowerSwitch_off", NOISE_POLLUTION.NOISY.TIER3);
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, LogicTimerSensorConfig.ID);
		return buildingDef;
	}

	// Token: 0x06000AC4 RID: 2756 RVA: 0x0003CC73 File Offset: 0x0003AE73
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicTimerSensor>().manuallyControlled = false;
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x04000643 RID: 1603
	public static string ID = "LogicTimerSensor";
}
