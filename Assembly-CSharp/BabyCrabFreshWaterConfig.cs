using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000E2 RID: 226
public class BabyCrabFreshWaterConfig : IEntityConfig
{
	// Token: 0x0600040B RID: 1035 RVA: 0x0001EEE2 File Offset: 0x0001D0E2
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x0001EEE9 File Offset: 0x0001D0E9
	public GameObject CreatePrefab()
	{
		GameObject gameObject = CrabFreshWaterConfig.CreateCrabFreshWater("CrabFreshWaterBaby", CREATURES.SPECIES.CRAB.VARIANT_FRESH_WATER.BABY.NAME, CREATURES.SPECIES.CRAB.VARIANT_FRESH_WATER.BABY.DESC, "baby_pincher_kanim", true, null);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "CrabFreshWater", null, false, 5f);
		return gameObject;
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x0001EF28 File Offset: 0x0001D128
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x0001EF2A File Offset: 0x0001D12A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000295 RID: 661
	public const string ID = "CrabFreshWaterBaby";
}
