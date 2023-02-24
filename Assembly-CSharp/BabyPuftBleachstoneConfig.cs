using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000119 RID: 281
public class BabyPuftBleachstoneConfig : IEntityConfig
{
	// Token: 0x06000553 RID: 1363 RVA: 0x0002379F File Offset: 0x0002199F
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x000237A6 File Offset: 0x000219A6
	public GameObject CreatePrefab()
	{
		GameObject gameObject = PuftBleachstoneConfig.CreatePuftBleachstone("PuftBleachstoneBaby", CREATURES.SPECIES.PUFT.VARIANT_BLEACHSTONE.BABY.NAME, CREATURES.SPECIES.PUFT.VARIANT_BLEACHSTONE.BABY.DESC, "baby_puft_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "PuftBleachstone", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x000237E4 File Offset: 0x000219E4
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x000237E6 File Offset: 0x000219E6
	public void OnSpawn(GameObject inst)
	{
		BasePuftConfig.OnSpawn(inst);
	}

	// Token: 0x04000395 RID: 917
	public const string ID = "PuftBleachstoneBaby";
}
