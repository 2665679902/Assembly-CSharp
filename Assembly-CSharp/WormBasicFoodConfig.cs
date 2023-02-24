using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200018C RID: 396
public class WormBasicFoodConfig : IEntityConfig
{
	// Token: 0x060007AF RID: 1967 RVA: 0x0002D817 File Offset: 0x0002BA17
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x0002D820 File Offset: 0x0002BA20
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("WormBasicFood", ITEMS.FOOD.WORMBASICFOOD.NAME, ITEMS.FOOD.WORMBASICFOOD.DESC, 1f, false, Assets.GetAnim("wormwood_roast_nuts_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.7f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.WORMBASICFOOD);
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x0002D884 File Offset: 0x0002BA84
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x0002D886 File Offset: 0x0002BA86
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004F9 RID: 1273
	public const string ID = "WormBasicFood";

	// Token: 0x040004FA RID: 1274
	public static ComplexRecipe recipe;
}
