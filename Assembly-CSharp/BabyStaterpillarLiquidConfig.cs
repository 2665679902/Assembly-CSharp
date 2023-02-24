using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000129 RID: 297
public class BabyStaterpillarLiquidConfig : IEntityConfig
{
	// Token: 0x060005B4 RID: 1460 RVA: 0x000259CD File Offset: 0x00023BCD
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x000259D4 File Offset: 0x00023BD4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = StaterpillarLiquidConfig.CreateStaterpillarLiquid("StaterpillarLiquidBaby", CREATURES.SPECIES.STATERPILLAR.VARIANT_LIQUID.BABY.NAME, CREATURES.SPECIES.STATERPILLAR.VARIANT_LIQUID.BABY.DESC, "baby_caterpillar_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "StaterpillarLiquid", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x00025A12 File Offset: 0x00023C12
	public void OnPrefabInit(GameObject prefab)
	{
		prefab.GetComponent<KBatchedAnimController>().SetSymbolVisiblity("electric_bolt_c_bloom", false);
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x00025A2A File Offset: 0x00023C2A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003F1 RID: 1009
	public const string ID = "StaterpillarLiquidBaby";
}
