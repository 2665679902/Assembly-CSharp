using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000F1 RID: 241
public class BabyHatchHardConfig : IEntityConfig
{
	// Token: 0x06000464 RID: 1124 RVA: 0x000205AD File Offset: 0x0001E7AD
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x000205B4 File Offset: 0x0001E7B4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = HatchHardConfig.CreateHatch("HatchHardBaby", CREATURES.SPECIES.HATCH.VARIANT_HARD.BABY.NAME, CREATURES.SPECIES.HATCH.VARIANT_HARD.BABY.DESC, "baby_hatch_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "HatchHard", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x000205F2 File Offset: 0x0001E7F2
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x000205F4 File Offset: 0x0001E7F4
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040002EF RID: 751
	public const string ID = "HatchHardBaby";
}
