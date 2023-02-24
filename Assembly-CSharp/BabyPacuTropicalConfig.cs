using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000115 RID: 277
[EntityConfigOrder(1)]
public class BabyPacuTropicalConfig : IEntityConfig
{
	// Token: 0x0600053B RID: 1339 RVA: 0x000231E1 File Offset: 0x000213E1
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x000231E8 File Offset: 0x000213E8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = PacuTropicalConfig.CreatePacu("PacuTropicalBaby", CREATURES.SPECIES.PACU.VARIANT_TROPICAL.BABY.NAME, CREATURES.SPECIES.PACU.VARIANT_TROPICAL.BABY.DESC, "baby_pacu_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "PacuTropical", null, false, 5f);
		return gameObject;
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x00023226 File Offset: 0x00021426
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x00023228 File Offset: 0x00021428
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400037F RID: 895
	public const string ID = "PacuTropicalBaby";
}
