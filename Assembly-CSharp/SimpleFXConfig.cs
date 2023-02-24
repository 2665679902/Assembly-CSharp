using System;
using UnityEngine;

// Token: 0x0200026E RID: 622
public class SimpleFXConfig : IEntityConfig
{
	// Token: 0x06000C69 RID: 3177 RVA: 0x0004652A File Offset: 0x0004472A
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x00046531 File Offset: 0x00044731
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(SimpleFXConfig.ID, SimpleFXConfig.ID, false);
		gameObject.AddOrGet<KBatchedAnimController>();
		return gameObject;
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x0004654A File Offset: 0x0004474A
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000C6C RID: 3180 RVA: 0x0004654C File Offset: 0x0004474C
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x0400073F RID: 1855
	public static readonly string ID = "SimpleFX";
}
