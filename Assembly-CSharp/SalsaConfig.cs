using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000180 RID: 384
public class SalsaConfig : IEntityConfig
{
	// Token: 0x06000772 RID: 1906 RVA: 0x0002D1F4 File Offset: 0x0002B3F4
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x0002D1FC File Offset: 0x0002B3FC
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("Salsa", ITEMS.FOOD.SALSA.NAME, ITEMS.FOOD.SALSA.DESC, 1f, false, Assets.GetAnim("zestysalsa_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.5f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.SALSA);
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x0002D260 File Offset: 0x0002B460
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x0002D262 File Offset: 0x0002B462
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004E3 RID: 1251
	public const string ID = "Salsa";

	// Token: 0x040004E4 RID: 1252
	public static ComplexRecipe recipe;
}
