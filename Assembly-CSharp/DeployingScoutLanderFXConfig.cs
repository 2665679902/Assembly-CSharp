using System;
using UnityEngine;

// Token: 0x02000255 RID: 597
public class DeployingScoutLanderFXConfig : IEntityConfig
{
	// Token: 0x06000BD4 RID: 3028 RVA: 0x00042A56 File Offset: 0x00040C56
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x00042A5D File Offset: 0x00040C5D
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity("DeployingScoutLanderFXConfig", "DeployingScoutLanderFXConfig", false);
		ClusterFXEntity clusterFXEntity = gameObject.AddOrGet<ClusterFXEntity>();
		clusterFXEntity.kAnimName = "rover01_kanim";
		clusterFXEntity.animName = "landing";
		clusterFXEntity.animPlayMode = KAnim.PlayMode.Loop;
		return gameObject;
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x00042A91 File Offset: 0x00040C91
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x00042A93 File Offset: 0x00040C93
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006EB RID: 1771
	public const string ID = "DeployingScoutLanderFXConfig";
}
