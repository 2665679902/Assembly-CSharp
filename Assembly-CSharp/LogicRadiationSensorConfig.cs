using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000218 RID: 536
public class LogicRadiationSensorConfig : IBuildingConfig
{
	// Token: 0x06000A9E RID: 2718 RVA: 0x0003C101 File Offset: 0x0003A301
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x0003C108 File Offset: 0x0003A308
	public override BuildingDef CreateBuildingDef()
	{
		string id = LogicRadiationSensorConfig.ID;
		int num = 1;
		int num2 = 1;
		string text = "radiation_sensor_kanim";
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
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port>();
		buildingDef.LogicOutputPorts.Add(LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.LOGICRADIATIONSENSOR.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.LOGICRADIATIONSENSOR.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LOGICRADIATIONSENSOR.LOGIC_PORT_INACTIVE, true, false));
		SoundEventVolumeCache.instance.AddVolume("switchthermal_kanim", "PowerSwitch_on", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume("switchthermal_kanim", "PowerSwitch_off", NOISE_POLLUTION.NOISY.TIER3);
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, LogicRadiationSensorConfig.ID);
		return buildingDef;
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x0003C210 File Offset: 0x0003A410
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicRadiationSensor>().manuallyControlled = false;
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x0400063A RID: 1594
	public static string ID = "LogicRadiationSensor";
}
