using System;
using UnityEngine;

// Token: 0x02000254 RID: 596
public class DeployingPioneerLanderFXConfig : IEntityConfig
{
	// Token: 0x06000BCF RID: 3023 RVA: 0x00042A0F File Offset: 0x00040C0F
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x00042A16 File Offset: 0x00040C16
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity("DeployingPioneerLanderFX", "DeployingPioneerLanderFX", false);
		ClusterFXEntity clusterFXEntity = gameObject.AddOrGet<ClusterFXEntity>();
		clusterFXEntity.kAnimName = "pioneer01_kanim";
		clusterFXEntity.animName = "landing";
		clusterFXEntity.animPlayMode = KAnim.PlayMode.Loop;
		return gameObject;
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x00042A4A File Offset: 0x00040C4A
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000BD2 RID: 3026 RVA: 0x00042A4C File Offset: 0x00040C4C
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006EA RID: 1770
	public const string ID = "DeployingPioneerLanderFX";
}
