using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001B2 RID: 434
public class GravitasDoorConfig : IBuildingConfig
{
	// Token: 0x0600087E RID: 2174 RVA: 0x000325AD File Offset: 0x000307AD
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x000325B4 File Offset: 0x000307B4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "GravitasDoor";
		int num = 1;
		int num2 = 3;
		string text2 = "gravitas_door_internal_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Tile;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.NONE, none, 1f);
		buildingDef.ShowInBuildMenu = false;
		buildingDef.Entombable = false;
		buildingDef.Floodable = false;
		buildingDef.Invincible = true;
		buildingDef.AudioCategory = "Metal";
		buildingDef.PermittedRotations = PermittedRotations.R90;
		buildingDef.ForegroundLayer = Grid.SceneLayer.InteriorWall;
		buildingDef.LogicInputPorts = DoorConfig.CreateSingleInputPortList(new CellOffset(0, 0));
		buildingDef.LogicInputPorts = GravitasDoorConfig.CreateSingleInputPortList(new CellOffset(0, 0));
		SoundEventVolumeCache.instance.AddVolume("gravitas_door_internal_kanim", "GravitasDoorInternal_open", NOISE_POLLUTION.NOISY.TIER2);
		SoundEventVolumeCache.instance.AddVolume("gravitas_door_internal_kanim", "GravitasDoorInternal_close", NOISE_POLLUTION.NOISY.TIER2);
		return buildingDef;
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x00032688 File Offset: 0x00030888
	public static List<LogicPorts.Port> CreateSingleInputPortList(CellOffset offset)
	{
		return new List<LogicPorts.Port> { LogicPorts.Port.InputPort(Door.OPEN_CLOSE_PORT_ID, offset, STRINGS.BUILDINGS.PREFABS.DOOR.LOGIC_OPEN, STRINGS.BUILDINGS.PREFABS.DOOR.LOGIC_OPEN_ACTIVE, STRINGS.BUILDINGS.PREFABS.DOOR.LOGIC_OPEN_INACTIVE, false, false) };
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x000326CC File Offset: 0x000308CC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddTag(GameTags.Gravitas);
		Door door = go.AddOrGet<Door>();
		door.unpoweredAnimSpeed = 1f;
		door.doorType = Door.DoorType.Internal;
		door.doorOpeningSoundEventName = "GravitasDoorInternal_open";
		door.doorClosingSoundEventName = "GravitasDoorInternal_close";
		go.AddOrGet<ZoneTile>();
		go.AddOrGet<AccessControl>().controlEnabled = true;
		go.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.Door;
		go.AddOrGet<Workable>().workTime = 3f;
		go.AddOrGet<KBoxCollider2D>();
		Prioritizable.AddRef(go);
		UnityEngine.Object.DestroyImmediate(go.GetComponent<BuildingEnabledButton>());
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x0003275C File Offset: 0x0003095C
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000558 RID: 1368
	public const string ID = "GravitasDoor";
}
