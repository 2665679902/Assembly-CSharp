using System;
using UnityEngine;

// Token: 0x0200025F RID: 607
public class TargetLocator : IEntityConfig
{
	// Token: 0x06000C08 RID: 3080 RVA: 0x00043F1A File Offset: 0x0004211A
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x00043F21 File Offset: 0x00042121
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(TargetLocator.ID, TargetLocator.ID, false);
		gameObject.AddTag(GameTags.NotConversationTopic);
		return gameObject;
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x00043F3E File Offset: 0x0004213E
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x00043F40 File Offset: 0x00042140
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x0400070D RID: 1805
	public static readonly string ID = "TargetLocator";
}
