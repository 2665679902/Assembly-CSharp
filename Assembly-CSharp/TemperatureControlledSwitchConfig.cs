using System;
using TUNING;
using UnityEngine;

// Token: 0x02000338 RID: 824
public class TemperatureControlledSwitchConfig : IBuildingConfig
{
	// Token: 0x0600106D RID: 4205 RVA: 0x000596FC File Offset: 0x000578FC
	public override BuildingDef CreateBuildingDef()
	{
		string id = TemperatureControlledSwitchConfig.ID;
		int num = 1;
		int num2 = 1;
		string text = "switchthermal_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, num, num2, text, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
		buildingDef.Deprecated = true;
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		SoundEventVolumeCache.instance.AddVolume("switchthermal_kanim", "PowerSwitch_on", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume("switchthermal_kanim", "PowerSwitch_off", NOISE_POLLUTION.NOISY.TIER3);
		return buildingDef;
	}

	// Token: 0x0600106E RID: 4206 RVA: 0x000597A7 File Offset: 0x000579A7
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		TemperatureControlledSwitch temperatureControlledSwitch = go.AddOrGet<TemperatureControlledSwitch>();
		temperatureControlledSwitch.objectLayer = ObjectLayer.Wire;
		temperatureControlledSwitch.manuallyControlled = false;
		temperatureControlledSwitch.minTemp = 0f;
		temperatureControlledSwitch.maxTemp = 573.15f;
	}

	// Token: 0x0600106F RID: 4207 RVA: 0x000597D9 File Offset: 0x000579D9
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddComponent<BuildingCellVisualizer>();
	}

	// Token: 0x040008F5 RID: 2293
	public static string ID = "TemperatureControlledSwitch";
}
