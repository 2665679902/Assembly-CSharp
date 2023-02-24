using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000F5 RID: 245
public class BabyHatchVeggieConfig : IEntityConfig
{
	// Token: 0x0600047D RID: 1149 RVA: 0x00020AD1 File Offset: 0x0001ECD1
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x00020AD8 File Offset: 0x0001ECD8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = HatchVeggieConfig.CreateHatch("HatchVeggieBaby", CREATURES.SPECIES.HATCH.VARIANT_VEGGIE.BABY.NAME, CREATURES.SPECIES.HATCH.VARIANT_VEGGIE.BABY.DESC, "baby_hatch_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "HatchVeggie", null, false, 5f);
		return gameObject;
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x00020B16 File Offset: 0x0001ED16
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x00020B18 File Offset: 0x0001ED18
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000300 RID: 768
	public const string ID = "HatchVeggieBaby";
}
