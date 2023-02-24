using System;
using UnityEngine;

// Token: 0x02000271 RID: 625
public class TemporalTearConfig : IEntityConfig
{
	// Token: 0x06000C7A RID: 3194 RVA: 0x00046608 File Offset: 0x00044808
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x0004660F File Offset: 0x0004480F
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity("TemporalTear", "TemporalTear", true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<TemporalTear>();
		return gameObject;
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x0004662F File Offset: 0x0004482F
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x00046631 File Offset: 0x00044831
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000742 RID: 1858
	public const string ID = "TemporalTear";
}
