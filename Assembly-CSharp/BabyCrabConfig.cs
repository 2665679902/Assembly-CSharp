using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000E0 RID: 224
public class BabyCrabConfig : IEntityConfig
{
	// Token: 0x060003FF RID: 1023 RVA: 0x0001EB0D File Offset: 0x0001CD0D
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x0001EB14 File Offset: 0x0001CD14
	public GameObject CreatePrefab()
	{
		GameObject gameObject = CrabConfig.CreateCrab("CrabBaby", CREATURES.SPECIES.CRAB.BABY.NAME, CREATURES.SPECIES.CRAB.BABY.DESC, "baby_pincher_kanim", true, "BabyCrabShell");
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Crab", "BabyCrabShell", false, 5f);
		return gameObject;
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x0001EB66 File Offset: 0x0001CD66
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x0001EB68 File Offset: 0x0001CD68
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400028B RID: 651
	public const string ID = "CrabBaby";
}
