using System;
using TUNING;
using UnityEngine;

// Token: 0x0200029C RID: 668
public class POIBunkerExteriorDoor : IBuildingConfig
{
	// Token: 0x06000D4B RID: 3403 RVA: 0x00049DD4 File Offset: 0x00047FD4
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("POIBunkerExteriorDoor", 1, 2, "door_poi_kanim", 30, 60f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER3, MATERIALS.ALL_METALS, 1600f, BuildLocationRule.Anywhere, BUILDINGS.DECOR.PENALTY.TIER2, NOISE_POLLUTION.NONE, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.Repairable = false;
		buildingDef.Floodable = false;
		buildingDef.Invincible = true;
		buildingDef.Entombable = false;
		buildingDef.IsFoundation = true;
		buildingDef.TileLayer = ObjectLayer.FoundationTile;
		buildingDef.AudioCategory = "Metal";
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.ForegroundLayer = Grid.SceneLayer.InteriorWall;
		buildingDef.ShowInBuildMenu = false;
		SoundEventVolumeCache.instance.AddVolume("door_manual_kanim", "ManualPressureDoor_gear_LP", NOISE_POLLUTION.NOISY.TIER1);
		SoundEventVolumeCache.instance.AddVolume("door_manual_kanim", "ManualPressureDoor_open", NOISE_POLLUTION.NOISY.TIER2);
		SoundEventVolumeCache.instance.AddVolume("door_manual_kanim", "ManualPressureDoor_close", NOISE_POLLUTION.NOISY.TIER2);
		return buildingDef;
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x00049EC0 File Offset: 0x000480C0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Door door = go.AddOrGet<Door>();
		door.hasComplexUserControls = false;
		door.unpoweredAnimSpeed = 1f;
		door.doorType = Door.DoorType.Sealed;
		go.AddOrGet<ZoneTile>();
		go.AddOrGet<AccessControl>();
		go.AddOrGet<Unsealable>();
		go.AddOrGet<KBoxCollider2D>();
		Prioritizable.AddRef(go);
		go.AddOrGet<Workable>().workTime = 5f;
		go.AddOrGet<KBatchedAnimController>().fgLayer = Grid.SceneLayer.BuildingFront;
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x00049F2A File Offset: 0x0004812A
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<AccessControl>().controlEnabled = false;
		go.GetComponent<Deconstructable>().allowDeconstruction = false;
		go.GetComponent<KBatchedAnimController>().initialAnim = "closed";
	}

	// Token: 0x040007BB RID: 1979
	public const string ID = "POIBunkerExteriorDoor";
}
