using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000123 RID: 291
public class BabySquirrelHugConfig : IEntityConfig
{
	// Token: 0x06000590 RID: 1424 RVA: 0x00024F65 File Offset: 0x00023165
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x00024F6C File Offset: 0x0002316C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = SquirrelHugConfig.CreateSquirrelHug("SquirrelHugBaby", CREATURES.SPECIES.SQUIRREL.VARIANT_HUG.BABY.NAME, CREATURES.SPECIES.SQUIRREL.VARIANT_HUG.BABY.DESC, "baby_squirrel_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "SquirrelHug", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x00024FAA File Offset: 0x000231AA
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x00024FAC File Offset: 0x000231AC
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003CA RID: 970
	public const string ID = "SquirrelHugBaby";
}
