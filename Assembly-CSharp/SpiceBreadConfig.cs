using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000182 RID: 386
public class SpiceBreadConfig : IEntityConfig
{
	// Token: 0x0600077C RID: 1916 RVA: 0x0002D2E6 File Offset: 0x0002B4E6
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x0002D2F0 File Offset: 0x0002B4F0
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("SpiceBread", ITEMS.FOOD.SPICEBREAD.NAME, ITEMS.FOOD.SPICEBREAD.DESC, 1f, false, Assets.GetAnim("pepperbread_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.9f, 0.6f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.SPICEBREAD);
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x0002D354 File Offset: 0x0002B554
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x0002D356 File Offset: 0x0002B556
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004E6 RID: 1254
	public const string ID = "SpiceBread";

	// Token: 0x040004E7 RID: 1255
	public static ComplexRecipe recipe;
}
