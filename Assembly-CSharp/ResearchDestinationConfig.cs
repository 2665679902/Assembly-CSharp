using System;
using UnityEngine;

// Token: 0x0200026D RID: 621
public class ResearchDestinationConfig : IEntityConfig
{
	// Token: 0x06000C64 RID: 3172 RVA: 0x000464F7 File Offset: 0x000446F7
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x000464FE File Offset: 0x000446FE
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity("ResearchDestination", "ResearchDestination", true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<ResearchDestination>();
		return gameObject;
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x0004651E File Offset: 0x0004471E
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x00046520 File Offset: 0x00044720
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400073E RID: 1854
	public const string ID = "ResearchDestination";
}
