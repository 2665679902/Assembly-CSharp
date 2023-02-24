using System;
using UnityEngine;

// Token: 0x02000260 RID: 608
public class ApproachableLocator : IEntityConfig
{
	// Token: 0x06000C0E RID: 3086 RVA: 0x00043F56 File Offset: 0x00042156
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000C0F RID: 3087 RVA: 0x00043F5D File Offset: 0x0004215D
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(ApproachableLocator.ID, ApproachableLocator.ID, false);
		gameObject.AddTag(GameTags.NotConversationTopic);
		gameObject.AddOrGet<Approachable>();
		return gameObject;
	}

	// Token: 0x06000C10 RID: 3088 RVA: 0x00043F81 File Offset: 0x00042181
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000C11 RID: 3089 RVA: 0x00043F83 File Offset: 0x00042183
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x0400070E RID: 1806
	public static readonly string ID = "ApproachableLocator";
}
