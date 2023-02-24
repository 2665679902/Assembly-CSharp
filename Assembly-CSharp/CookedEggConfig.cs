using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200016A RID: 362
public class CookedEggConfig : IEntityConfig
{
	// Token: 0x060006FE RID: 1790 RVA: 0x0002C400 File Offset: 0x0002A600
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x0002C408 File Offset: 0x0002A608
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("CookedEgg", ITEMS.FOOD.COOKEDEGG.NAME, ITEMS.FOOD.COOKEDEGG.DESC, 1f, false, Assets.GetAnim("cookedegg_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.COOKED_EGG);
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x0002C46C File Offset: 0x0002A66C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x0002C46E File Offset: 0x0002A66E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004BE RID: 1214
	public const string ID = "CookedEgg";

	// Token: 0x040004BF RID: 1215
	public static ComplexRecipe recipe;
}
