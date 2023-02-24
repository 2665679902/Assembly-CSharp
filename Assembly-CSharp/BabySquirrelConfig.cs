using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000121 RID: 289
public class BabySquirrelConfig : IEntityConfig
{
	// Token: 0x06000584 RID: 1412 RVA: 0x00024D05 File Offset: 0x00022F05
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x00024D0C File Offset: 0x00022F0C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = SquirrelConfig.CreateSquirrel("SquirrelBaby", CREATURES.SPECIES.SQUIRREL.BABY.NAME, CREATURES.SPECIES.SQUIRREL.BABY.DESC, "baby_squirrel_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Squirrel", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x00024D4A File Offset: 0x00022F4A
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x00024D4C File Offset: 0x00022F4C
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003BE RID: 958
	public const string ID = "SquirrelBaby";
}
