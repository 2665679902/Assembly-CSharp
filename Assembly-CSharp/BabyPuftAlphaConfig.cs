using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000117 RID: 279
public class BabyPuftAlphaConfig : IEntityConfig
{
	// Token: 0x06000547 RID: 1351 RVA: 0x00023522 File Offset: 0x00021722
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00023529 File Offset: 0x00021729
	public GameObject CreatePrefab()
	{
		GameObject gameObject = PuftAlphaConfig.CreatePuftAlpha("PuftAlphaBaby", CREATURES.SPECIES.PUFT.VARIANT_ALPHA.BABY.NAME, CREATURES.SPECIES.PUFT.VARIANT_ALPHA.BABY.DESC, "baby_puft_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "PuftAlpha", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x00023567 File Offset: 0x00021767
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x00023569 File Offset: 0x00021769
	public void OnSpawn(GameObject inst)
	{
		BasePuftConfig.OnSpawn(inst);
	}

	// Token: 0x0400038B RID: 907
	public const string ID = "PuftAlphaBaby";
}
