using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000233 RID: 563
public class BasicCureConfig : IEntityConfig
{
	// Token: 0x06000B19 RID: 2841 RVA: 0x0003E89A File Offset: 0x0003CA9A
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x0003E8A4 File Offset: 0x0003CAA4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("BasicCure", ITEMS.PILLS.BASICCURE.NAME, ITEMS.PILLS.BASICCURE.DESC, 1f, true, Assets.GetAnim("pill_foodpoisoning_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToMedicine(gameObject, MEDICINE.BASICCURE);
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(SimHashes.Carbon.CreateTag(), 1f),
			new ComplexRecipe.RecipeElement(SimHashes.Water.CreateTag(), 1f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("BasicCure", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		BasicCureConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Apothecary", array, array2), array, array2)
		{
			time = 50f,
			description = ITEMS.PILLS.BASICCURE.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "Apothecary" },
			sortOrder = 10
		};
		return gameObject;
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x0003E9BE File Offset: 0x0003CBBE
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x0003E9C0 File Offset: 0x0003CBC0
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000680 RID: 1664
	public const string ID = "BasicCure";

	// Token: 0x04000681 RID: 1665
	public static ComplexRecipe recipe;
}
