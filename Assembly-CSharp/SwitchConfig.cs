using System;
using TUNING;
using UnityEngine;

// Token: 0x02000334 RID: 820
public class SwitchConfig : IBuildingConfig
{
	// Token: 0x06001059 RID: 4185 RVA: 0x000590D0 File Offset: 0x000572D0
	public override BuildingDef CreateBuildingDef()
	{
		string id = SwitchConfig.ID;
		int num = 1;
		int num2 = 1;
		string text = "switchpower_kanim";
		int num3 = 10;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, num, num2, text, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		SoundEventVolumeCache.instance.AddVolume("switchpower_kanim", "PowerSwitch_on", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume("switchpower_kanim", "PowerSwitch_off", NOISE_POLLUTION.NOISY.TIER3);
		return buildingDef;
	}

	// Token: 0x0600105A RID: 4186 RVA: 0x00059174 File Offset: 0x00057374
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<BuildingComplete>().isManuallyOperated = false;
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		CircuitSwitch circuitSwitch = go.AddOrGet<CircuitSwitch>();
		circuitSwitch.objectLayer = ObjectLayer.Wire;
		circuitSwitch.manuallyControlled = false;
		Prioritizable.AddRef(go);
	}

	// Token: 0x0600105B RID: 4187 RVA: 0x000591A2 File Offset: 0x000573A2
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddComponent<BuildingCellVisualizer>();
	}

	// Token: 0x040008E7 RID: 2279
	public static string ID = "Switch";
}
