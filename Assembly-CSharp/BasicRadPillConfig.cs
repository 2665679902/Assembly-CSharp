using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000234 RID: 564
public class BasicRadPillConfig : IEntityConfig
{
	// Token: 0x06000B1E RID: 2846 RVA: 0x0003E9CA File Offset: 0x0003CBCA
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x0003E9D4 File Offset: 0x0003CBD4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("BasicRadPill", ITEMS.PILLS.BASICRADPILL.NAME, ITEMS.PILLS.BASICRADPILL.DESC, 1f, true, Assets.GetAnim("pill_radiation_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToMedicine(gameObject, MEDICINE.BASICRADPILL);
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Carbon", 1f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("BasicRadPill".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		BasicRadPillConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Apothecary", array, array2), array, array2)
		{
			time = 50f,
			description = ITEMS.PILLS.BASICRADPILL.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "Apothecary" },
			sortOrder = 10
		};
		return gameObject;
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x0003EAD7 File Offset: 0x0003CCD7
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x0003EAD9 File Offset: 0x0003CCD9
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000682 RID: 1666
	public const string ID = "BasicRadPill";

	// Token: 0x04000683 RID: 1667
	public static ComplexRecipe recipe;
}
