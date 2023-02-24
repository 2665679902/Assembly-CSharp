using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200007C RID: 124
public class EscapePodConfig : IEntityConfig
{
	// Token: 0x0600024D RID: 589 RVA: 0x00010CD6 File Offset: 0x0000EED6
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600024E RID: 590 RVA: 0x00010CE0 File Offset: 0x0000EEE0
	public GameObject CreatePrefab()
	{
		string text = "EscapePod";
		string text2 = STRINGS.BUILDINGS.PREFABS.ESCAPEPOD.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.ESCAPEPOD.DESC;
		float num = 100f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("escape_pod_kanim"), "grounded", Grid.SceneLayer.Building, 1, 2, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.AddOrGet<KBatchedAnimController>().fgLayer = Grid.SceneLayer.BuildingFront;
		TravellingCargoLander.Def def = gameObject.AddOrGetDef<TravellingCargoLander.Def>();
		def.landerWidth = 1;
		def.landingSpeed = 15f;
		def.deployOnLanding = true;
		CargoDropperMinion.Def def2 = gameObject.AddOrGetDef<CargoDropperMinion.Def>();
		def2.kAnimName = "anim_interacts_escape_pod_kanim";
		def2.animName = "deploying";
		def2.animLayer = Grid.SceneLayer.BuildingUse;
		def2.notifyOnJettison = true;
		BallisticClusterGridEntity ballisticClusterGridEntity = gameObject.AddOrGet<BallisticClusterGridEntity>();
		ballisticClusterGridEntity.clusterAnimName = "escape_pod01_kanim";
		ballisticClusterGridEntity.isWorldEntity = true;
		ballisticClusterGridEntity.nameKey = new StringKey("STRINGS.BUILDINGS.PREFABS.ESCAPEPOD.NAME");
		ClusterDestinationSelector clusterDestinationSelector = gameObject.AddOrGet<ClusterDestinationSelector>();
		clusterDestinationSelector.assignable = false;
		clusterDestinationSelector.shouldPointTowardsPath = true;
		clusterDestinationSelector.requireAsteroidDestination = true;
		clusterDestinationSelector.canNavigateFogOfWar = true;
		gameObject.AddOrGet<ClusterTraveler>();
		gameObject.AddOrGet<MinionStorage>();
		gameObject.AddOrGet<Prioritizable>();
		Prioritizable.AddRef(gameObject);
		gameObject.AddOrGet<Operational>();
		gameObject.AddOrGet<Deconstructable>().audioSize = "large";
		return gameObject;
	}

	// Token: 0x0600024F RID: 591 RVA: 0x00010E10 File Offset: 0x0000F010
	public void OnPrefabInit(GameObject inst)
	{
		OccupyArea component = inst.GetComponent<OccupyArea>();
		component.ApplyToCells = false;
		component.objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000250 RID: 592 RVA: 0x00010E2E File Offset: 0x0000F02E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000141 RID: 321
	public const string ID = "EscapePod";

	// Token: 0x04000142 RID: 322
	public const float MASS = 100f;
}
