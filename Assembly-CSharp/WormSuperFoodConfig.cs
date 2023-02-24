using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200018E RID: 398
public class WormSuperFoodConfig : IEntityConfig
{
	// Token: 0x060007B9 RID: 1977 RVA: 0x0002D908 File Offset: 0x0002BB08
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x0002D910 File Offset: 0x0002BB10
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("WormSuperFood", ITEMS.FOOD.WORMSUPERFOOD.NAME, ITEMS.FOOD.WORMSUPERFOOD.DESC, 1f, false, Assets.GetAnim("wormwood_preserved_berries_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.7f, 0.6f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.WORMSUPERFOOD);
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x0002D974 File Offset: 0x0002BB74
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x0002D976 File Offset: 0x0002BB76
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004FC RID: 1276
	public const string ID = "WormSuperFood";

	// Token: 0x040004FD RID: 1277
	public static ComplexRecipe recipe;
}
