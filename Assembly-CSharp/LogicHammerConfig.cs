using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000211 RID: 529
public class LogicHammerConfig : IBuildingConfig
{
	// Token: 0x06000A7F RID: 2687 RVA: 0x0003B794 File Offset: 0x00039994
	public override BuildingDef CreateBuildingDef()
	{
		string id = LogicHammerConfig.ID;
		int num = 1;
		int num2 = 1;
		string text = "hammer_kanim";
		int num3 = 30;
		float num4 = 10f;
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
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 60f;
		buildingDef.SelfHeatKilowattsWhenActive = 0f;
		buildingDef.ExhaustKilowattsWhenActive = 0f;
		buildingDef.ViewMode = OverlayModes.Logic.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.LogicInputPorts = new List<LogicPorts.Port> { LogicPorts.Port.InputPort(LogicHammer.PORT_ID, new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.LOGICHAMMER.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.LOGICHAMMER.INPUT_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LOGICHAMMER.INPUT_PORT_INACTIVE, false, false) };
		SoundEventVolumeCache.instance.AddVolume("door_internal_kanim", "Open_DoorInternal", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume("door_internal_kanim", "Close_DoorInternal", NOISE_POLLUTION.NOISY.TIER3);
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, LogicHammerConfig.ID);
		return buildingDef;
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x0003B8BF File Offset: 0x00039ABF
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicHammer>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x04000631 RID: 1585
	public static string ID = "LogicHammer";
}
