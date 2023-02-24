using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000EA RID: 234
public class BabyDreckoConfig : IEntityConfig
{
	// Token: 0x0600043B RID: 1083 RVA: 0x0001FAB9 File Offset: 0x0001DCB9
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x0001FAC0 File Offset: 0x0001DCC0
	public GameObject CreatePrefab()
	{
		GameObject gameObject = DreckoConfig.CreateDrecko("DreckoBaby", CREATURES.SPECIES.DRECKO.BABY.NAME, CREATURES.SPECIES.DRECKO.BABY.DESC, "baby_drecko_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Drecko", null, false, 5f);
		return gameObject;
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x0001FAFE File Offset: 0x0001DCFE
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x0001FB00 File Offset: 0x0001DD00
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040002C6 RID: 710
	public const string ID = "DreckoBaby";
}
