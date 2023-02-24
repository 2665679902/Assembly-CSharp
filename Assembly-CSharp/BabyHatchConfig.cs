using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000EF RID: 239
public class BabyHatchConfig : IEntityConfig
{
	// Token: 0x06000458 RID: 1112 RVA: 0x0002034B File Offset: 0x0001E54B
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x00020352 File Offset: 0x0001E552
	public GameObject CreatePrefab()
	{
		GameObject gameObject = HatchConfig.CreateHatch("HatchBaby", CREATURES.SPECIES.HATCH.BABY.NAME, CREATURES.SPECIES.HATCH.BABY.DESC, "baby_hatch_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Hatch", null, false, 5f);
		return gameObject;
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x00020390 File Offset: 0x0001E590
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x00020392 File Offset: 0x0001E592
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040002E6 RID: 742
	public const string ID = "HatchBaby";
}
