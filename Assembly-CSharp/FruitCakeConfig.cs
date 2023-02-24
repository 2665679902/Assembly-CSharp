using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000172 RID: 370
public class FruitCakeConfig : IEntityConfig
{
	// Token: 0x06000726 RID: 1830 RVA: 0x0002C7C4 File Offset: 0x0002A9C4
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x0002C7CC File Offset: 0x0002A9CC
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("FruitCake", ITEMS.FOOD.FRUITCAKE.NAME, ITEMS.FOOD.FRUITCAKE.DESC, 1f, false, Assets.GetAnim("fruitcake_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		gameObject = EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.FRUITCAKE);
		ComplexRecipeManager.Get().GetRecipe(FruitCakeConfig.recipe.id).FabricationVisualizer = MushBarConfig.CreateFabricationVisualizer(gameObject);
		return gameObject;
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x0002C853 File Offset: 0x0002AA53
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x0002C855 File Offset: 0x0002AA55
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004CB RID: 1227
	public const string ID = "FruitCake";

	// Token: 0x040004CC RID: 1228
	public static ComplexRecipe recipe;
}
