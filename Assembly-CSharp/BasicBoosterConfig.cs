using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000232 RID: 562
public class BasicBoosterConfig : IEntityConfig
{
	// Token: 0x06000B14 RID: 2836 RVA: 0x0003E782 File Offset: 0x0003C982
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x0003E78C File Offset: 0x0003C98C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("BasicBooster", ITEMS.PILLS.BASICBOOSTER.NAME, ITEMS.PILLS.BASICBOOSTER.DESC, 1f, true, Assets.GetAnim("pill_2_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToMedicine(gameObject, MEDICINE.BASICBOOSTER);
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Carbon", 1f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("BasicBooster".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		BasicBoosterConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Apothecary", array, array2), array, array2)
		{
			time = 50f,
			description = ITEMS.PILLS.BASICBOOSTER.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "Apothecary" },
			sortOrder = 1
		};
		return gameObject;
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x0003E88E File Offset: 0x0003CA8E
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x0003E890 File Offset: 0x0003CA90
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400067E RID: 1662
	public const string ID = "BasicBooster";

	// Token: 0x0400067F RID: 1663
	public static ComplexRecipe recipe;
}
