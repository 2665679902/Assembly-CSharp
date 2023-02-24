using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000E4 RID: 228
public class BabyCrabWoodConfig : IEntityConfig
{
	// Token: 0x06000417 RID: 1047 RVA: 0x0001F182 File Offset: 0x0001D382
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x0001F18C File Offset: 0x0001D38C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = CrabWoodConfig.CreateCrabWood("CrabWoodBaby", CREATURES.SPECIES.CRAB.VARIANT_WOOD.BABY.NAME, CREATURES.SPECIES.CRAB.VARIANT_WOOD.BABY.DESC, "baby_pincher_kanim", true, "BabyCrabWoodShell");
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "CrabWood", "BabyCrabWoodShell", false, 5f);
		return gameObject;
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x0001F1DE File Offset: 0x0001D3DE
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x0001F1E0 File Offset: 0x0001D3E0
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400029F RID: 671
	public const string ID = "CrabWoodBaby";
}
