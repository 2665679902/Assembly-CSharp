using System;
using UnityEngine;

// Token: 0x02000249 RID: 585
public class ClustercraftConfig : IEntityConfig
{
	// Token: 0x06000B8E RID: 2958 RVA: 0x000419CD File Offset: 0x0003FBCD
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x000419D4 File Offset: 0x0003FBD4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity("Clustercraft", "Clustercraft", true);
		SaveLoadRoot saveLoadRoot = gameObject.AddOrGet<SaveLoadRoot>();
		saveLoadRoot.DeclareOptionalComponent<WorldInventory>();
		saveLoadRoot.DeclareOptionalComponent<WorldContainer>();
		saveLoadRoot.DeclareOptionalComponent<OrbitalMechanics>();
		gameObject.AddOrGet<Clustercraft>();
		gameObject.AddOrGet<CraftModuleInterface>();
		gameObject.AddOrGet<UserNameable>();
		RocketClusterDestinationSelector rocketClusterDestinationSelector = gameObject.AddOrGet<RocketClusterDestinationSelector>();
		rocketClusterDestinationSelector.requireLaunchPadOnAsteroidDestination = true;
		rocketClusterDestinationSelector.assignable = true;
		rocketClusterDestinationSelector.shouldPointTowardsPath = true;
		gameObject.AddOrGet<ClusterTraveler>().stopAndNotifyWhenPathChanges = true;
		gameObject.AddOrGetDef<AlertStateManager.Def>();
		gameObject.AddOrGet<Notifier>();
		gameObject.AddOrGetDef<RocketSelfDestructMonitor.Def>();
		return gameObject;
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x00041A58 File Offset: 0x0003FC58
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x00041A5A File Offset: 0x0003FC5A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006DC RID: 1756
	public const string ID = "Clustercraft";
}
