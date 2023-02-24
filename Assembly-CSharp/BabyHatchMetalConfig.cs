using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000F3 RID: 243
public class BabyHatchMetalConfig : IEntityConfig
{
	// Token: 0x06000471 RID: 1137 RVA: 0x0002086D File Offset: 0x0001EA6D
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x00020874 File Offset: 0x0001EA74
	public GameObject CreatePrefab()
	{
		GameObject gameObject = HatchMetalConfig.CreateHatch("HatchMetalBaby", CREATURES.SPECIES.HATCH.VARIANT_METAL.BABY.NAME, CREATURES.SPECIES.HATCH.VARIANT_METAL.BABY.DESC, "baby_hatch_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "HatchMetal", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x000208B2 File Offset: 0x0001EAB2
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x000208B4 File Offset: 0x0001EAB4
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040002F7 RID: 759
	public const string ID = "HatchMetalBaby";
}
