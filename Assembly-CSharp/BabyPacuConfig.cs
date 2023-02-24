using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000113 RID: 275
[EntityConfigOrder(1)]
public class BabyPacuConfig : IEntityConfig
{
	// Token: 0x0600052F RID: 1327 RVA: 0x000230A1 File Offset: 0x000212A1
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x000230A8 File Offset: 0x000212A8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = PacuConfig.CreatePacu("PacuBaby", CREATURES.SPECIES.PACU.BABY.NAME, CREATURES.SPECIES.PACU.BABY.DESC, "baby_pacu_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Pacu", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x000230E6 File Offset: 0x000212E6
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x000230E8 File Offset: 0x000212E8
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000379 RID: 889
	public const string ID = "PacuBaby";
}
