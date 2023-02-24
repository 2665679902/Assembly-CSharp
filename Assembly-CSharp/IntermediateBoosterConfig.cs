using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000235 RID: 565
public class IntermediateBoosterConfig : IEntityConfig
{
	// Token: 0x06000B23 RID: 2851 RVA: 0x0003EAE3 File Offset: 0x0003CCE3
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x0003EAEC File Offset: 0x0003CCEC
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("IntermediateBooster", ITEMS.PILLS.INTERMEDIATEBOOSTER.NAME, ITEMS.PILLS.INTERMEDIATEBOOSTER.DESC, 1f, true, Assets.GetAnim("pill_3_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToMedicine(gameObject, MEDICINE.INTERMEDIATEBOOSTER);
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(SpiceNutConfig.ID, 1f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("IntermediateBooster", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		IntermediateBoosterConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Apothecary", array, array2), array, array2)
		{
			time = 100f,
			description = ITEMS.PILLS.INTERMEDIATEBOOSTER.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "Apothecary" },
			sortOrder = 5
		};
		return gameObject;
	}

	// Token: 0x06000B25 RID: 2853 RVA: 0x0003EBEE File Offset: 0x0003CDEE
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000B26 RID: 2854 RVA: 0x0003EBF0 File Offset: 0x0003CDF0
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000684 RID: 1668
	public const string ID = "IntermediateBooster";

	// Token: 0x04000685 RID: 1669
	public static ComplexRecipe recipe;
}
