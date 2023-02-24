using System;
using UnityEngine;

// Token: 0x02000265 RID: 613
public class MeterConfig : IEntityConfig
{
	// Token: 0x06000C2F RID: 3119 RVA: 0x0004451F File Offset: 0x0004271F
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x00044526 File Offset: 0x00042726
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(MeterConfig.ID, MeterConfig.ID, false);
		gameObject.AddOrGet<KBatchedAnimController>();
		gameObject.AddOrGet<KBatchedAnimTracker>();
		return gameObject;
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x00044546 File Offset: 0x00042746
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x00044548 File Offset: 0x00042748
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x0400072D RID: 1837
	public static readonly string ID = "Meter";
}
