using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000E6 RID: 230
public class BabyDivergentBeetleConfig : IEntityConfig
{
	// Token: 0x06000423 RID: 1059 RVA: 0x0001F3EB File Offset: 0x0001D5EB
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x0001F3F2 File Offset: 0x0001D5F2
	public GameObject CreatePrefab()
	{
		GameObject gameObject = DivergentBeetleConfig.CreateDivergentBeetle("DivergentBeetleBaby", CREATURES.SPECIES.DIVERGENT.VARIANT_BEETLE.BABY.NAME, CREATURES.SPECIES.DIVERGENT.VARIANT_BEETLE.BABY.DESC, "baby_critter_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "DivergentBeetle", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x0001F430 File Offset: 0x0001D630
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000426 RID: 1062 RVA: 0x0001F432 File Offset: 0x0001D632
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040002A9 RID: 681
	public const string ID = "DivergentBeetleBaby";
}
