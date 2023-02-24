using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000231 RID: 561
public class AntihistamineConfig : IEntityConfig
{
	// Token: 0x06000B0F RID: 2831 RVA: 0x0003E653 File Offset: 0x0003C853
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x0003E65C File Offset: 0x0003C85C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("Antihistamine", ITEMS.PILLS.ANTIHISTAMINE.NAME, ITEMS.PILLS.ANTIHISTAMINE.DESC, 1f, true, Assets.GetAnim("pill_allergies_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToMedicine(gameObject, MEDICINE.ANTIHISTAMINE);
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("PrickleFlowerSeed", 1f),
			new ComplexRecipe.RecipeElement(SimHashes.Dirt.CreateTag(), 1f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Antihistamine", 10f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		AntihistamineConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Apothecary", array, array2), array, array2)
		{
			time = 100f,
			description = ITEMS.PILLS.ANTIHISTAMINE.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "Apothecary" },
			sortOrder = 10
		};
		return gameObject;
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x0003E776 File Offset: 0x0003C976
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000B12 RID: 2834 RVA: 0x0003E778 File Offset: 0x0003C978
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400067C RID: 1660
	public const string ID = "Antihistamine";

	// Token: 0x0400067D RID: 1661
	public static ComplexRecipe recipe;
}
