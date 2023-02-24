﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x0200002E RID: 46
public class BunkerDoorConfig : IBuildingConfig
{
	// Token: 0x060000D1 RID: 209 RVA: 0x00006CF8 File Offset: 0x00004EF8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "BunkerDoor";
		int num = 4;
		int num2 = 1;
		string text2 = "door_bunker_kanim";
		int num3 = 1000;
		float num4 = 120f;
		float[] array = new float[] { 500f };
		string[] array2 = new string[] { SimHashes.Steel.ToString() };
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Tile;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 1f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.OverheatTemperature = 1273.15f;
		buildingDef.Entombable = false;
		buildingDef.IsFoundation = true;
		buildingDef.AudioCategory = "Metal";
		buildingDef.PermittedRotations = PermittedRotations.R90;
		buildingDef.SceneLayer = Grid.SceneLayer.TileMain;
		buildingDef.ForegroundLayer = Grid.SceneLayer.InteriorWall;
		buildingDef.TileLayer = ObjectLayer.FoundationTile;
		buildingDef.LogicInputPorts = DoorConfig.CreateSingleInputPortList(new CellOffset(-1, 0));
		SoundEventVolumeCache.instance.AddVolume("door_internal_kanim", "Open_DoorInternal", NOISE_POLLUTION.NOISY.TIER2);
		SoundEventVolumeCache.instance.AddVolume("door_internal_kanim", "Close_DoorInternal", NOISE_POLLUTION.NOISY.TIER2);
		return buildingDef;
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00006DFC File Offset: 0x00004FFC
	public override void DoPostConfigureComplete(GameObject go)
	{
		Door door = go.AddOrGet<Door>();
		door.unpoweredAnimSpeed = 0.01f;
		door.poweredAnimSpeed = 0.1f;
		door.hasComplexUserControls = true;
		door.allowAutoControl = false;
		door.doorOpeningSoundEventName = "BunkerDoor_opening";
		door.doorClosingSoundEventName = "BunkerDoor_closing";
		door.verticalOrientation = Orientation.R90;
		go.AddOrGet<Workable>().workTime = 3f;
		KBatchedAnimController component = go.GetComponent<KBatchedAnimController>();
		component.initialAnim = "closed";
		component.visibilityType = KAnimControllerBase.VisibilityType.OffscreenUpdate;
		go.AddOrGet<ZoneTile>();
		go.AddOrGet<KBoxCollider2D>();
		Prioritizable.AddRef(go);
		UnityEngine.Object.DestroyImmediate(go.GetComponent<BuildingEnabledButton>());
		go.GetComponent<KPrefabID>().AddTag(GameTags.Bunker, false);
	}

	// Token: 0x0400007F RID: 127
	public const string ID = "BunkerDoor";
}
