using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000110 RID: 272
[EntityConfigOrder(1)]
public class BabyPacuCleanerConfig : IEntityConfig
{
	// Token: 0x06000522 RID: 1314 RVA: 0x00022F64 File Offset: 0x00021164
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x00022F6B File Offset: 0x0002116B
	public GameObject CreatePrefab()
	{
		GameObject gameObject = PacuCleanerConfig.CreatePacu("PacuCleanerBaby", CREATURES.SPECIES.PACU.VARIANT_CLEANER.BABY.NAME, CREATURES.SPECIES.PACU.VARIANT_CLEANER.BABY.DESC, "baby_pacu_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "PacuCleaner", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x00022FA9 File Offset: 0x000211A9
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x00022FAB File Offset: 0x000211AB
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000374 RID: 884
	public const string ID = "PacuCleanerBaby";
}
