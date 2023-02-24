using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200011B RID: 283
public class BabyPuftConfig : IEntityConfig
{
	// Token: 0x0600055F RID: 1375 RVA: 0x00023A19 File Offset: 0x00021C19
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x00023A20 File Offset: 0x00021C20
	public GameObject CreatePrefab()
	{
		GameObject gameObject = PuftConfig.CreatePuft("PuftBaby", CREATURES.SPECIES.PUFT.BABY.NAME, CREATURES.SPECIES.PUFT.BABY.DESC, "baby_puft_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Puft", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x00023A5E File Offset: 0x00021C5E
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x00023A60 File Offset: 0x00021C60
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003A1 RID: 929
	public const string ID = "PuftBaby";
}
