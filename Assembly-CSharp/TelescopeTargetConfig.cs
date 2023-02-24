using System;
using UnityEngine;

// Token: 0x02000270 RID: 624
public class TelescopeTargetConfig : IEntityConfig
{
	// Token: 0x06000C75 RID: 3189 RVA: 0x000465DC File Offset: 0x000447DC
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000C76 RID: 3190 RVA: 0x000465E3 File Offset: 0x000447E3
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity("TelescopeTarget", "TelescopeTarget", true);
		gameObject.AddOrGet<TelescopeTarget>();
		return gameObject;
	}

	// Token: 0x06000C77 RID: 3191 RVA: 0x000465FC File Offset: 0x000447FC
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000C78 RID: 3192 RVA: 0x000465FE File Offset: 0x000447FE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000741 RID: 1857
	public const string ID = "TelescopeTarget";
}
