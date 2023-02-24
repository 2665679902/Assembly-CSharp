﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x0200029E RID: 670
public class POIFacilityDoorConfig : IBuildingConfig
{
	// Token: 0x06000D54 RID: 3412 RVA: 0x0004A0E8 File Offset: 0x000482E8
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("POIFacilityDoor", 2, 3, "door_facility_kanim", 30, 60f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER3, MATERIALS.ALL_METALS, 1600f, BuildLocationRule.Anywhere, BUILDINGS.DECOR.PENALTY.TIER2, NOISE_POLLUTION.NONE, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.Repairable = false;
		buildingDef.Floodable = false;
		buildingDef.Invincible = true;
		buildingDef.IsFoundation = true;
		buildingDef.TileLayer = ObjectLayer.FoundationTile;
		buildingDef.AudioCategory = "Metal";
		buildingDef.PermittedRotations = PermittedRotations.R90;
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.ForegroundLayer = Grid.SceneLayer.InteriorWall;
		buildingDef.ShowInBuildMenu = false;
		SoundEventVolumeCache.instance.AddVolume("door_manual_kanim", "ManualPressureDoor_gear_LP", NOISE_POLLUTION.NOISY.TIER1);
		SoundEventVolumeCache.instance.AddVolume("door_manual_kanim", "ManualPressureDoor_open", NOISE_POLLUTION.NOISY.TIER2);
		SoundEventVolumeCache.instance.AddVolume("door_manual_kanim", "ManualPressureDoor_close", NOISE_POLLUTION.NOISY.TIER2);
		return buildingDef;
	}

	// Token: 0x06000D55 RID: 3413 RVA: 0x0004A1CC File Offset: 0x000483CC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Door door = go.AddOrGet<Door>();
		door.hasComplexUserControls = false;
		door.unpoweredAnimSpeed = 1f;
		door.doorType = Door.DoorType.ManualPressure;
		go.AddOrGet<ZoneTile>();
		go.AddOrGet<AccessControl>();
		go.AddOrGet<Unsealable>();
		go.AddOrGet<KBoxCollider2D>();
		Prioritizable.AddRef(go);
		go.AddOrGet<Workable>().workTime = 5f;
		go.AddOrGet<KBatchedAnimController>().fgLayer = Grid.SceneLayer.BuildingFront;
		PrimaryElement component = go.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 273f;
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x0004A252 File Offset: 0x00048452
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<AccessControl>().controlEnabled = false;
		go.GetComponent<Deconstructable>().allowDeconstruction = true;
		go.GetComponent<KBatchedAnimController>().initialAnim = "closed";
	}

	// Token: 0x040007BD RID: 1981
	public const string ID = "POIFacilityDoor";
}
