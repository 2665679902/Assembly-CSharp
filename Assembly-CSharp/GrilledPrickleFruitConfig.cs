using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000174 RID: 372
public class GrilledPrickleFruitConfig : IEntityConfig
{
	// Token: 0x06000730 RID: 1840 RVA: 0x0002C8D8 File Offset: 0x0002AAD8
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x0002C8E0 File Offset: 0x0002AAE0
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("GrilledPrickleFruit", ITEMS.FOOD.GRILLEDPRICKLEFRUIT.NAME, ITEMS.FOOD.GRILLEDPRICKLEFRUIT.DESC, 1f, false, Assets.GetAnim("gristleberry_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.7f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.GRILLED_PRICKLEFRUIT);
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x0002C944 File Offset: 0x0002AB44
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x0002C946 File Offset: 0x0002AB46
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004CF RID: 1231
	public const string ID = "GrilledPrickleFruit";

	// Token: 0x040004D0 RID: 1232
	public static ComplexRecipe recipe;
}
