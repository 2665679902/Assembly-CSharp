using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000210 RID: 528
public class LogicHEPSensorConfig : IBuildingConfig
{
	// Token: 0x06000A7A RID: 2682 RVA: 0x0003B645 File Offset: 0x00039845
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x0003B64C File Offset: 0x0003984C
	public override BuildingDef CreateBuildingDef()
	{
		string id = LogicHEPSensorConfig.ID;
		int num = 1;
		int num2 = 1;
		string text = LogicHEPSensorConfig.kanim;
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
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.LOGICHEPSENSOR.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.LOGICHEPSENSOR.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LOGICHEPSENSOR.LOGIC_PORT_INACTIVE, true, false) };
		SoundEventVolumeCache.instance.AddVolume(LogicHEPSensorConfig.kanim, "PowerSwitch_on", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume(LogicHEPSensorConfig.kanim, "PowerSwitch_off", NOISE_POLLUTION.NOISY.TIER3);
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, LogicHEPSensorConfig.ID);
		return buildingDef;
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x0003B74F File Offset: 0x0003994F
	public override void DoPostConfigureComplete(GameObject go)
	{
		LogicHEPSensor logicHEPSensor = go.AddOrGet<LogicHEPSensor>();
		logicHEPSensor.manuallyControlled = false;
		logicHEPSensor.activateOnHigherThan = true;
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x0400062F RID: 1583
	public static string ID = "LogicHEPSensor";

	// Token: 0x04000630 RID: 1584
	private static readonly string kanim = "radbolt_sensor_kanim";
}
