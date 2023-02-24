using System;
using UnityEngine;

// Token: 0x02000244 RID: 580
public class AsteroidConfig : IEntityConfig
{
	// Token: 0x06000B67 RID: 2919 RVA: 0x0004152F File Offset: 0x0003F72F
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x00041538 File Offset: 0x0003F738
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity("Asteroid", "Asteroid", true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<WorldInventory>();
		gameObject.AddOrGet<WorldContainer>();
		gameObject.AddOrGet<AsteroidGridEntity>();
		gameObject.AddOrGet<OrbitalMechanics>();
		gameObject.AddOrGetDef<GameplaySeasonManager.Def>();
		gameObject.AddOrGetDef<AlertStateManager.Def>();
		return gameObject;
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x00041586 File Offset: 0x0003F786
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x00041588 File Offset: 0x0003F788
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006D1 RID: 1745
	public const string ID = "Asteroid";
}
