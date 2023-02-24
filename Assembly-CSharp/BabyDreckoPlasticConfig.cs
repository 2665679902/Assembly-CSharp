using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000EC RID: 236
public class BabyDreckoPlasticConfig : IEntityConfig
{
	// Token: 0x06000447 RID: 1095 RVA: 0x0001FDF5 File Offset: 0x0001DFF5
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x0001FDFC File Offset: 0x0001DFFC
	public GameObject CreatePrefab()
	{
		GameObject gameObject = DreckoPlasticConfig.CreateDrecko("DreckoPlasticBaby", CREATURES.SPECIES.DRECKO.VARIANT_PLASTIC.BABY.NAME, CREATURES.SPECIES.DRECKO.VARIANT_PLASTIC.BABY.DESC, "baby_drecko_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "DreckoPlastic", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x0001FE3A File Offset: 0x0001E03A
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x0001FE3C File Offset: 0x0001E03C
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040002D4 RID: 724
	public const string ID = "DreckoPlasticBaby";
}
