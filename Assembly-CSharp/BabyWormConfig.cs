using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000E8 RID: 232
public class BabyWormConfig : IEntityConfig
{
	// Token: 0x0600042F RID: 1071 RVA: 0x0001F75D File Offset: 0x0001D95D
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x0001F764 File Offset: 0x0001D964
	public GameObject CreatePrefab()
	{
		GameObject gameObject = DivergentWormConfig.CreateWorm("DivergentWormBaby", CREATURES.SPECIES.DIVERGENT.VARIANT_WORM.BABY.NAME, CREATURES.SPECIES.DIVERGENT.VARIANT_WORM.BABY.DESC, "baby_worm_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "DivergentWorm", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x0001F7A2 File Offset: 0x0001D9A2
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x0001F7A4 File Offset: 0x0001D9A4
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040002B8 RID: 696
	public const string ID = "DivergentWormBaby";
}
