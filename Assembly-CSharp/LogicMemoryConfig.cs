using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000214 RID: 532
public class LogicMemoryConfig : IBuildingConfig
{
	// Token: 0x06000A8E RID: 2702 RVA: 0x0003BB0C File Offset: 0x00039D0C
	public override BuildingDef CreateBuildingDef()
	{
		string id = LogicMemoryConfig.ID;
		int num = 2;
		int num2 = 2;
		string text = "logic_memory_kanim";
		int num3 = 10;
		float num4 = 30f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, num, num2, text, num3, num4, tier, refined_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.Deprecated = false;
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.InitialOrientation = Orientation.R90;
		buildingDef.ViewMode = OverlayModes.Logic.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.SceneLayer = Grid.SceneLayer.LogicGates;
		buildingDef.ObjectLayer = ObjectLayer.LogicGate;
		buildingDef.AlwaysOperational = true;
		buildingDef.LogicInputPorts = new List<LogicPorts.Port>
		{
			new LogicPorts.Port(LogicMemory.SET_PORT_ID, new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.LOGICMEMORY.SET_PORT, STRINGS.BUILDINGS.PREFABS.LOGICMEMORY.SET_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LOGICMEMORY.SET_PORT_INACTIVE, true, LogicPortSpriteType.Input, true),
			new LogicPorts.Port(LogicMemory.RESET_PORT_ID, new CellOffset(1, 0), STRINGS.BUILDINGS.PREFABS.LOGICMEMORY.RESET_PORT, STRINGS.BUILDINGS.PREFABS.LOGICMEMORY.RESET_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LOGICMEMORY.RESET_PORT_INACTIVE, true, LogicPortSpriteType.ResetUpdate, true)
		};
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port>
		{
			new LogicPorts.Port(LogicMemory.READ_PORT_ID, new CellOffset(0, 1), STRINGS.BUILDINGS.PREFABS.LOGICMEMORY.READ_PORT, STRINGS.BUILDINGS.PREFABS.LOGICMEMORY.READ_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LOGICMEMORY.READ_PORT_INACTIVE, true, LogicPortSpriteType.Output, true)
		};
		SoundEventVolumeCache.instance.AddVolume("logic_memory_kanim", "PowerMemory_on", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume("logic_memory_kanim", "PowerMemory_off", NOISE_POLLUTION.NOISY.TIER3);
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, LogicMemoryConfig.ID);
		return buildingDef;
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x0003BCA8 File Offset: 0x00039EA8
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicMemory>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits, false);
	}

	// Token: 0x04000636 RID: 1590
	public static string ID = "LogicMemory";
}
