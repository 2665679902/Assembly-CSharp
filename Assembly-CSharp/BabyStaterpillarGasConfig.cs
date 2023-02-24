using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000127 RID: 295
public class BabyStaterpillarGasConfig : IEntityConfig
{
	// Token: 0x060005A8 RID: 1448 RVA: 0x000255D5 File Offset: 0x000237D5
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x000255DC File Offset: 0x000237DC
	public GameObject CreatePrefab()
	{
		GameObject gameObject = StaterpillarGasConfig.CreateStaterpillarGas("StaterpillarGasBaby", CREATURES.SPECIES.STATERPILLAR.VARIANT_GAS.BABY.NAME, CREATURES.SPECIES.STATERPILLAR.VARIANT_GAS.BABY.DESC, "baby_caterpillar_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "StaterpillarGas", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x0002561A File Offset: 0x0002381A
	public void OnPrefabInit(GameObject prefab)
	{
		prefab.GetComponent<KBatchedAnimController>().SetSymbolVisiblity("electric_bolt_c_bloom", false);
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x00025632 File Offset: 0x00023832
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003E1 RID: 993
	public const string ID = "StaterpillarGasBaby";
}
