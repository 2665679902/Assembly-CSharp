using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000221 RID: 545
public class LogicWattageSensorConfig : IBuildingConfig
{
	// Token: 0x06000AC7 RID: 2759 RVA: 0x0003CCA8 File Offset: 0x0003AEA8
	public override BuildingDef CreateBuildingDef()
	{
		string id = LogicWattageSensorConfig.ID;
		int num = 1;
		int num2 = 1;
		string text = LogicWattageSensorConfig.kanim;
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
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.LOGICWATTAGESENSOR.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.LOGICWATTAGESENSOR.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LOGICWATTAGESENSOR.LOGIC_PORT_INACTIVE, true, false) };
		SoundEventVolumeCache.instance.AddVolume(LogicWattageSensorConfig.kanim, "PowerSwitch_on", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume(LogicWattageSensorConfig.kanim, "PowerSwitch_off", NOISE_POLLUTION.NOISY.TIER3);
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, LogicWattageSensorConfig.ID);
		return buildingDef;
	}

	// Token: 0x06000AC8 RID: 2760 RVA: 0x0003CDAB File Offset: 0x0003AFAB
	public override void DoPostConfigureComplete(GameObject go)
	{
		LogicWattageSensor logicWattageSensor = go.AddOrGet<LogicWattageSensor>();
		logicWattageSensor.manuallyControlled = false;
		logicWattageSensor.activateOnHigherThan = true;
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x04000644 RID: 1604
	public static string ID = "LogicWattageSensor";

	// Token: 0x04000645 RID: 1605
	private static readonly string kanim = "wattage_sensor_kanim";
}
