using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200005D RID: 93
public class DebrisPayloadConfig : IEntityConfig
{
	// Token: 0x0600019F RID: 415 RVA: 0x0000BD1E File Offset: 0x00009F1E
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x0000BD28 File Offset: 0x00009F28
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("DebrisPayload", ITEMS.DEBRISPAYLOAD.NAME, ITEMS.DEBRISPAYLOAD.DESC, 100f, true, Assets.GetAnim("rocket_debris_combined_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 1f, 1f, true, 0, SimHashes.Creature, new List<Tag>
		{
			GameTags.IgnoreMaterialCategory,
			GameTags.Experimental
		});
		RailGunPayload.Def def = gameObject.AddOrGetDef<RailGunPayload.Def>();
		def.attractToBeacons = false;
		def.clusterAnimSymbolSwapTarget = "debris1";
		def.randomClusterSymbolSwaps = new List<string> { "debris1", "debris2", "debris3" };
		def.worldAnimSymbolSwapTarget = "debris";
		def.randomWorldSymbolSwaps = new List<string> { "debris", "2_debris", "3_debris" };
		SymbolOverrideControllerUtil.AddToPrefab(gameObject);
		gameObject.AddOrGet<LoopingSounds>();
		Storage storage = BuildingTemplates.CreateDefaultStorage(gameObject, false);
		storage.showInUI = true;
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		storage.allowSettingOnlyFetchMarkedItems = false;
		storage.allowItemRemoval = false;
		storage.capacityKg = 5000f;
		DropAllWorkable dropAllWorkable = gameObject.AddOrGet<DropAllWorkable>();
		dropAllWorkable.dropWorkTime = 30f;
		dropAllWorkable.choreTypeID = Db.Get().ChoreTypes.Fetch.Id;
		dropAllWorkable.ConfigureMultitoolContext("build", EffectConfigs.BuildSplashId);
		ClusterDestinationSelector clusterDestinationSelector = gameObject.AddOrGet<ClusterDestinationSelector>();
		clusterDestinationSelector.assignable = false;
		clusterDestinationSelector.shouldPointTowardsPath = true;
		clusterDestinationSelector.requireAsteroidDestination = true;
		clusterDestinationSelector.canNavigateFogOfWar = true;
		BallisticClusterGridEntity ballisticClusterGridEntity = gameObject.AddOrGet<BallisticClusterGridEntity>();
		ballisticClusterGridEntity.clusterAnimName = "rocket_debris_kanim";
		ballisticClusterGridEntity.isWorldEntity = true;
		ballisticClusterGridEntity.nameKey = new StringKey("STRINGS.ITEMS.DEBRISPAYLOAD.NAME");
		gameObject.AddOrGet<ClusterTraveler>();
		return gameObject;
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x0000BEE8 File Offset: 0x0000A0E8
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x0000BEEA File Offset: 0x0000A0EA
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040000ED RID: 237
	public const string ID = "DebrisPayload";

	// Token: 0x040000EE RID: 238
	public const float MASS = 100f;

	// Token: 0x040000EF RID: 239
	public const float MAX_STORAGE_KG_MASS = 5000f;

	// Token: 0x040000F0 RID: 240
	public const float STARMAP_SPEED = 10f;
}
