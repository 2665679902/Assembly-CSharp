using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000125 RID: 293
public class BabyStaterpillarConfig : IEntityConfig
{
	// Token: 0x0600059C RID: 1436 RVA: 0x000251F1 File Offset: 0x000233F1
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x000251F8 File Offset: 0x000233F8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = StaterpillarConfig.CreateStaterpillar("StaterpillarBaby", CREATURES.SPECIES.STATERPILLAR.BABY.NAME, CREATURES.SPECIES.STATERPILLAR.BABY.DESC, "baby_caterpillar_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Staterpillar", null, false, 5f);
		return gameObject;
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x00025236 File Offset: 0x00023436
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x00025238 File Offset: 0x00023438
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003D1 RID: 977
	public const string ID = "StaterpillarBaby";
}
