using System;
using Klei.AI;
using UnityEngine;

// Token: 0x0200026F RID: 623
public class StoredMinionConfig : IEntityConfig
{
	// Token: 0x06000C6F RID: 3183 RVA: 0x00046562 File Offset: 0x00044762
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000C70 RID: 3184 RVA: 0x0004656C File Offset: 0x0004476C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(StoredMinionConfig.ID, StoredMinionConfig.ID, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<KPrefabID>();
		gameObject.AddOrGet<Traits>();
		gameObject.AddOrGet<Schedulable>();
		gameObject.AddOrGet<StoredMinionIdentity>();
		gameObject.AddOrGet<KSelectable>().IsSelectable = false;
		gameObject.AddOrGet<MinionModifiers>().addBaseTraits = false;
		return gameObject;
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x000465C4 File Offset: 0x000447C4
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x000465C6 File Offset: 0x000447C6
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000740 RID: 1856
	public static string ID = "StoredMinion";
}
