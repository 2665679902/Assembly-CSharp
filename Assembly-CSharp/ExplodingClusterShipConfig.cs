using System;
using UnityEngine;

// Token: 0x02000257 RID: 599
public class ExplodingClusterShipConfig : IEntityConfig
{
	// Token: 0x06000BDB RID: 3035 RVA: 0x00042C5D File Offset: 0x00040E5D
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x00042C64 File Offset: 0x00040E64
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity("ExplodingClusterShip", "ExplodingClusterShip", false);
		ClusterFXEntity clusterFXEntity = gameObject.AddOrGet<ClusterFXEntity>();
		clusterFXEntity.kAnimName = "rocket_self_destruct_kanim";
		clusterFXEntity.animName = "explode";
		return gameObject;
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x00042C91 File Offset: 0x00040E91
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000BDE RID: 3038 RVA: 0x00042C93 File Offset: 0x00040E93
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006EC RID: 1772
	public const string ID = "ExplodingClusterShip";
}
