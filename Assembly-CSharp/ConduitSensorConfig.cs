using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x02000041 RID: 65
public abstract class ConduitSensorConfig : IBuildingConfig
{
	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000131 RID: 305
	protected abstract ConduitType ConduitType { get; }

	// Token: 0x06000132 RID: 306 RVA: 0x000090C4 File Offset: 0x000072C4
	protected BuildingDef CreateBuildingDef(string ID, string anim, float[] required_mass, string[] required_materials, List<LogicPorts.Port> output_ports)
	{
		int num = 1;
		int num2 = 1;
		int num3 = 30;
		float num4 = 30f;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, num, num2, anim, num3, num4, required_mass, required_materials, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.ViewMode = OverlayModes.Logic.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.AlwaysOperational = true;
		buildingDef.LogicOutputPorts = output_ports;
		SoundEventVolumeCache.instance.AddVolume(anim, "PowerSwitch_on", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume(anim, "PowerSwitch_off", NOISE_POLLUTION.NOISY.TIER3);
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, ID);
		return buildingDef;
	}

	// Token: 0x06000133 RID: 307 RVA: 0x00009172 File Offset: 0x00007372
	public override void DoPostConfigureComplete(GameObject go)
	{
	}
}
