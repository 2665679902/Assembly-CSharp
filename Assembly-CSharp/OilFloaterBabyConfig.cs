using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200010A RID: 266
public class OilFloaterBabyConfig : IEntityConfig
{
	// Token: 0x060004FE RID: 1278 RVA: 0x00022845 File Offset: 0x00020A45
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x0002284C File Offset: 0x00020A4C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = OilFloaterConfig.CreateOilFloater("OilfloaterBaby", CREATURES.SPECIES.OILFLOATER.BABY.NAME, CREATURES.SPECIES.OILFLOATER.BABY.DESC, "baby_oilfloater_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Oilfloater", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x0002288A File Offset: 0x00020A8A
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x0002288C File Offset: 0x00020A8C
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000359 RID: 857
	public const string ID = "OilfloaterBaby";
}
