using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200010E RID: 270
public class OilFloaterHighTempBabyConfig : IEntityConfig
{
	// Token: 0x06000516 RID: 1302 RVA: 0x00022CDF File Offset: 0x00020EDF
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x00022CE6 File Offset: 0x00020EE6
	public GameObject CreatePrefab()
	{
		GameObject gameObject = OilFloaterHighTempConfig.CreateOilFloater("OilfloaterHighTempBaby", CREATURES.SPECIES.OILFLOATER.VARIANT_HIGHTEMP.BABY.NAME, CREATURES.SPECIES.OILFLOATER.VARIANT_HIGHTEMP.BABY.DESC, "baby_oilfloater_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "OilfloaterHighTemp", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x00022D24 File Offset: 0x00020F24
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x00022D26 File Offset: 0x00020F26
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400036B RID: 875
	public const string ID = "OilfloaterHighTempBaby";
}
