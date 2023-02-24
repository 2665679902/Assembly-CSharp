using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000165 RID: 357
public class BasicPlantBarConfig : IEntityConfig
{
	// Token: 0x060006E5 RID: 1765 RVA: 0x0002C181 File Offset: 0x0002A381
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x0002C188 File Offset: 0x0002A388
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("BasicPlantBar", ITEMS.FOOD.BASICPLANTBAR.NAME, ITEMS.FOOD.BASICPLANTBAR.DESC, 1f, false, Assets.GetAnim("liceloaf_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		gameObject = EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.BASICPLANTBAR);
		ComplexRecipeManager.Get().GetRecipe(BasicPlantBarConfig.recipe.id).FabricationVisualizer = MushBarConfig.CreateFabricationVisualizer(gameObject);
		return gameObject;
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x0002C20F File Offset: 0x0002A40F
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x0002C211 File Offset: 0x0002A411
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004B5 RID: 1205
	public const string ID = "BasicPlantBar";

	// Token: 0x040004B6 RID: 1206
	public static ComplexRecipe recipe;
}
