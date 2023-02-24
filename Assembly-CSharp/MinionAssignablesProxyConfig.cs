using System;
using UnityEngine;

// Token: 0x02000266 RID: 614
public class MinionAssignablesProxyConfig : IEntityConfig
{
	// Token: 0x06000C35 RID: 3125 RVA: 0x0004455E File Offset: 0x0004275E
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x00044565 File Offset: 0x00042765
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(MinionAssignablesProxyConfig.ID, MinionAssignablesProxyConfig.ID, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<Ownables>();
		gameObject.AddOrGet<Equipment>();
		gameObject.AddOrGet<MinionAssignablesProxy>();
		return gameObject;
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x00044593 File Offset: 0x00042793
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x00044595 File Offset: 0x00042795
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400072E RID: 1838
	public static string ID = "MinionAssignablesProxy";
}
