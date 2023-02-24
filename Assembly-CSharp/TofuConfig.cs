using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200018B RID: 395
public class TofuConfig : IEntityConfig
{
	// Token: 0x060007AA RID: 1962 RVA: 0x0002D77A File Offset: 0x0002B97A
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x0002D784 File Offset: 0x0002B984
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("Tofu", ITEMS.FOOD.TOFU.NAME, ITEMS.FOOD.TOFU.DESC, 1f, false, Assets.GetAnim("loafu_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.9f, 0.6f, true, 0, SimHashes.Creature, null);
		gameObject = EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.TOFU);
		ComplexRecipeManager.Get().GetRecipe(TofuConfig.recipe.id).FabricationVisualizer = MushBarConfig.CreateFabricationVisualizer(gameObject);
		return gameObject;
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x0002D80B File Offset: 0x0002BA0B
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x0002D80D File Offset: 0x0002BA0D
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004F7 RID: 1271
	public const string ID = "Tofu";

	// Token: 0x040004F8 RID: 1272
	public static ComplexRecipe recipe;
}
