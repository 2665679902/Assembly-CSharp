using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class OilFloaterDecorBabyConfig : IEntityConfig
{
	// Token: 0x0600050A RID: 1290 RVA: 0x00022A91 File Offset: 0x00020C91
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00022A98 File Offset: 0x00020C98
	public GameObject CreatePrefab()
	{
		GameObject gameObject = OilFloaterDecorConfig.CreateOilFloater("OilfloaterDecorBaby", CREATURES.SPECIES.OILFLOATER.VARIANT_DECOR.BABY.NAME, CREATURES.SPECIES.OILFLOATER.VARIANT_DECOR.BABY.DESC, "baby_oilfloater_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "OilfloaterDecor", null, false, 5f);
		return gameObject;
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x00022AD6 File Offset: 0x00020CD6
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x00022AD8 File Offset: 0x00020CD8
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000361 RID: 865
	public const string ID = "OilfloaterDecorBaby";
}
