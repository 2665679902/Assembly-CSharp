using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002EC RID: 748
public class RocketControlStationConfig : IBuildingConfig
{
	// Token: 0x06000EDF RID: 3807 RVA: 0x000515B3 File Offset: 0x0004F7B3
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000EE0 RID: 3808 RVA: 0x000515BC File Offset: 0x0004F7BC
	public override BuildingDef CreateBuildingDef()
	{
		string id = RocketControlStationConfig.ID;
		int num = 2;
		int num2 = 2;
		string text = "rocket_control_station_kanim";
		int num3 = 30;
		float num4 = 60f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER3;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, num, num2, text, num3, num4, tier, raw_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.BONUS.TIER2, tier2, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.Repairable = false;
		buildingDef.Floodable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.DefaultAnimState = "off";
		buildingDef.OnePerWorld = true;
		buildingDef.LogicInputPorts = new List<LogicPorts.Port> { LogicPorts.Port.InputPort(RocketControlStation.PORT_ID, new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.ROCKETCONTROLSTATION.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.ROCKETCONTROLSTATION.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.ROCKETCONTROLSTATION.LOGIC_PORT_INACTIVE, false, false) };
		return buildingDef;
	}

	// Token: 0x06000EE1 RID: 3809 RVA: 0x00051681 File Offset: 0x0004F881
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		KPrefabID component = go.GetComponent<KPrefabID>();
		component.AddTag(GameTags.RocketInteriorBuilding, false);
		component.AddTag(GameTags.UniquePerWorld, false);
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x000516A0 File Offset: 0x0004F8A0
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		go.AddOrGet<RocketControlStationIdleWorkable>().workLayer = Grid.SceneLayer.BuildingUse;
		go.AddOrGet<RocketControlStationLaunchWorkable>().workLayer = Grid.SceneLayer.BuildingUse;
		go.AddOrGet<RocketControlStation>();
		go.AddOrGetDef<PoweredController.Def>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.RocketInterior, false);
	}

	// Token: 0x04000843 RID: 2115
	public static string ID = "RocketControlStation";

	// Token: 0x04000844 RID: 2116
	public const float CONSOLE_WORK_TIME = 30f;

	// Token: 0x04000845 RID: 2117
	public const float CONSOLE_IDLE_TIME = 120f;

	// Token: 0x04000846 RID: 2118
	public const float WARNING_COOLDOWN = 30f;

	// Token: 0x04000847 RID: 2119
	public const float DEFAULT_SPEED = 1f;

	// Token: 0x04000848 RID: 2120
	public const float SLOW_SPEED = 0.5f;

	// Token: 0x04000849 RID: 2121
	public const float DEFAULT_PILOT_MODIFIER = 1f;
}
