using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020002DE RID: 734
public class RailGunPayloadConfig : IEntityConfig
{
	// Token: 0x06000E8C RID: 3724 RVA: 0x0004EB90 File Offset: 0x0004CD90
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000E8D RID: 3725 RVA: 0x0004EB98 File Offset: 0x0004CD98
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("RailGunPayload", ITEMS.RAILGUNPAYLOAD.NAME, ITEMS.RAILGUNPAYLOAD.DESC, 200f, true, Assets.GetAnim("railgun_capsule_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.75f, 1f, true, 0, SimHashes.Creature, new List<Tag>
		{
			GameTags.IgnoreMaterialCategory,
			GameTags.Experimental
		});
		gameObject.AddOrGetDef<RailGunPayload.Def>().attractToBeacons = true;
		gameObject.AddComponent<LoopingSounds>();
		Storage storage = BuildingTemplates.CreateDefaultStorage(gameObject, false);
		storage.showInUI = true;
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		storage.allowSettingOnlyFetchMarkedItems = false;
		storage.allowItemRemoval = false;
		storage.capacityKg = 200f;
		DropAllWorkable dropAllWorkable = gameObject.AddOrGet<DropAllWorkable>();
		dropAllWorkable.dropWorkTime = 30f;
		dropAllWorkable.choreTypeID = Db.Get().ChoreTypes.Fetch.Id;
		dropAllWorkable.ConfigureMultitoolContext("build", EffectConfigs.BuildSplashId);
		ClusterDestinationSelector clusterDestinationSelector = gameObject.AddOrGet<ClusterDestinationSelector>();
		clusterDestinationSelector.assignable = false;
		clusterDestinationSelector.shouldPointTowardsPath = true;
		clusterDestinationSelector.requireAsteroidDestination = true;
		BallisticClusterGridEntity ballisticClusterGridEntity = gameObject.AddOrGet<BallisticClusterGridEntity>();
		ballisticClusterGridEntity.clusterAnimName = "payload01_kanim";
		ballisticClusterGridEntity.isWorldEntity = true;
		ballisticClusterGridEntity.nameKey = new StringKey("STRINGS.ITEMS.RAILGUNPAYLOAD.NAME");
		gameObject.AddOrGet<ClusterTraveler>();
		return gameObject;
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x0004ECDC File Offset: 0x0004CEDC
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000E8F RID: 3727 RVA: 0x0004ECDE File Offset: 0x0004CEDE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400080B RID: 2059
	public const string ID = "RailGunPayload";

	// Token: 0x0400080C RID: 2060
	public const float MASS = 200f;

	// Token: 0x0400080D RID: 2061
	public const int LANDING_EDGE_PADDING = 3;
}
