using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200011D RID: 285
public class BabyPuftOxyliteConfig : IEntityConfig
{
	// Token: 0x0600056B RID: 1387 RVA: 0x00023C8F File Offset: 0x00021E8F
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x00023C96 File Offset: 0x00021E96
	public GameObject CreatePrefab()
	{
		GameObject gameObject = PuftOxyliteConfig.CreatePuftOxylite("PuftOxyliteBaby", CREATURES.SPECIES.PUFT.VARIANT_OXYLITE.BABY.NAME, CREATURES.SPECIES.PUFT.VARIANT_OXYLITE.BABY.DESC, "baby_puft_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "PuftOxylite", null, false, 5f);
		return gameObject;
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x00023CD4 File Offset: 0x00021ED4
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x00023CD6 File Offset: 0x00021ED6
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003AB RID: 939
	public const string ID = "PuftOxyliteBaby";
}
