using System;
using UnityEngine;

// Token: 0x02000261 RID: 609
public class SleepLocator : IEntityConfig
{
	// Token: 0x06000C14 RID: 3092 RVA: 0x00043F99 File Offset: 0x00042199
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x00043FA0 File Offset: 0x000421A0
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(SleepLocator.ID, SleepLocator.ID, false);
		gameObject.AddTag(GameTags.NotConversationTopic);
		gameObject.AddOrGet<Approachable>();
		gameObject.AddOrGet<Sleepable>();
		return gameObject;
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x00043FCB File Offset: 0x000421CB
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x00043FCD File Offset: 0x000421CD
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x0400070F RID: 1807
	public static readonly string ID = "SleepLocator";
}
