using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200021B RID: 539
public class LogicRibbonReaderConfig : IBuildingConfig
{
	// Token: 0x06000AAE RID: 2734 RVA: 0x0003C488 File Offset: 0x0003A688
	public override BuildingDef CreateBuildingDef()
	{
		string id = LogicRibbonReaderConfig.ID;
		int num = 2;
		int num2 = 1;
		string text = "logic_ribbon_reader_kanim";
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
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.InitialOrientation = Orientation.R180;
		buildingDef.ViewMode = OverlayModes.Logic.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.ObjectLayer = ObjectLayer.LogicGate;
		buildingDef.SceneLayer = Grid.SceneLayer.LogicGates;
		buildingDef.AlwaysOperational = true;
		buildingDef.LogicInputPorts = new List<LogicPorts.Port> { LogicPorts.Port.RibbonInputPort(LogicRibbonReader.INPUT_PORT_ID, new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.LOGICRIBBONREADER.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.LOGICRIBBONREADER.INPUT_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LOGICRIBBONREADER.INPUT_PORT_INACTIVE, true, false) };
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort(LogicRibbonReader.OUTPUT_PORT_ID, new CellOffset(1, 0), STRINGS.BUILDINGS.PREFABS.LOGICRIBBONREADER.LOGIC_PORT_OUTPUT, STRINGS.BUILDINGS.PREFABS.LOGICRIBBONREADER.OUTPUT_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LOGICRIBBONREADER.OUTPUT_PORT_INACTIVE, true, false) };
		SoundEventVolumeCache.instance.AddVolume("door_internal_kanim", "Open_DoorInternal", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume("door_internal_kanim", "Close_DoorInternal", NOISE_POLLUTION.NOISY.TIER3);
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, LogicRibbonReaderConfig.ID);
		return buildingDef;
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x0003C5E3 File Offset: 0x0003A7E3
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicRibbonReader>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x0400063E RID: 1598
	public static string ID = "LogicRibbonReader";
}
