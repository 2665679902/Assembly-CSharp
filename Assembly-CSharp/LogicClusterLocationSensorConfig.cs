using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000200 RID: 512
public class LogicClusterLocationSensorConfig : IBuildingConfig
{
	// Token: 0x06000A13 RID: 2579 RVA: 0x0003A43D File Offset: 0x0003863D
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x0003A444 File Offset: 0x00038644
	public override BuildingDef CreateBuildingDef()
	{
		string id = LogicClusterLocationSensorConfig.ID;
		int num = 1;
		int num2 = 1;
		string text = "logic_location_kanim";
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
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.LOGICCLUSTERLOCATIONSENSOR.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.LOGICCLUSTERLOCATIONSENSOR.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LOGICCLUSTERLOCATIONSENSOR.LOGIC_PORT_INACTIVE, true, false) };
		SoundEventVolumeCache.instance.AddVolume("switchgaspressure_kanim", "PowerSwitch_on", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume("switchgaspressure_kanim", "PowerSwitch_off", NOISE_POLLUTION.NOISY.TIER3);
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, LogicClusterLocationSensorConfig.ID);
		return buildingDef;
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x0003A547 File Offset: 0x00038747
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		base.ConfigureBuildingTemplate(go, prefab_tag);
		go.GetComponent<KPrefabID>().AddTag(GameTags.RocketInteriorBuilding, false);
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x0003A562 File Offset: 0x00038762
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicClusterLocationSensor>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x0400061F RID: 1567
	public static string ID = "LogicClusterLocationSensor";
}
