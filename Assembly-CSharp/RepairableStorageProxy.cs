using System;
using UnityEngine;

// Token: 0x0200026C RID: 620
public class RepairableStorageProxy : IEntityConfig
{
	// Token: 0x06000C5E RID: 3166 RVA: 0x000464B4 File Offset: 0x000446B4
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x000464BB File Offset: 0x000446BB
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(RepairableStorageProxy.ID, RepairableStorageProxy.ID, true);
		gameObject.AddOrGet<Storage>();
		gameObject.AddTag(GameTags.NotConversationTopic);
		return gameObject;
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x000464DF File Offset: 0x000446DF
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x000464E1 File Offset: 0x000446E1
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x0400073D RID: 1853
	public static string ID = "RepairableStorageProxy";
}
