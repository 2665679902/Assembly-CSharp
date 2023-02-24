using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000230 RID: 560
public class AdvancedCureConfig : IEntityConfig
{
	// Token: 0x06000B0A RID: 2826 RVA: 0x0003E51D File Offset: 0x0003C71D
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x0003E524 File Offset: 0x0003C724
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("AdvancedCure", ITEMS.PILLS.ADVANCEDCURE.NAME, ITEMS.PILLS.ADVANCEDCURE.DESC, 1f, true, Assets.GetAnim("vial_spore_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		gameObject = EntityTemplates.ExtendEntityToMedicine(gameObject, MEDICINE.ADVANCEDCURE);
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(SimHashes.Steel.CreateTag(), 1f),
			new ComplexRecipe.RecipeElement("LightBugOrangeEgg", 1f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("AdvancedCure", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		string text = "Apothecary";
		AdvancedCureConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(text, array, array2), array, array2)
		{
			time = 200f,
			description = ITEMS.PILLS.ADVANCEDCURE.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { text },
			sortOrder = 20,
			requiredTech = "MedicineIV"
		};
		return gameObject;
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x0003E647 File Offset: 0x0003C847
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x0003E649 File Offset: 0x0003C849
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400067A RID: 1658
	public const string ID = "AdvancedCure";

	// Token: 0x0400067B RID: 1659
	public static ComplexRecipe recipe;
}
