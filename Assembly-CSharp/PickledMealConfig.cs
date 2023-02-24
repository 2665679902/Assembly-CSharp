using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200017B RID: 379
public class PickledMealConfig : IEntityConfig
{
	// Token: 0x06000756 RID: 1878 RVA: 0x0002CDF2 File Offset: 0x0002AFF2
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x0002CDFC File Offset: 0x0002AFFC
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("PickledMeal", ITEMS.FOOD.PICKLEDMEAL.NAME, ITEMS.FOOD.PICKLEDMEAL.DESC, 1f, false, Assets.GetAnim("pickledmeal_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.6f, 0.7f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.PICKLEDMEAL);
		gameObject.GetComponent<KPrefabID>().AddTag(GameTags.Pickled, false);
		return gameObject;
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x0002CE71 File Offset: 0x0002B071
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x0002CE73 File Offset: 0x0002B073
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004DB RID: 1243
	public const string ID = "PickledMeal";

	// Token: 0x040004DC RID: 1244
	public static ComplexRecipe recipe;
}
