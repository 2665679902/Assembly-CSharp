using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000237 RID: 567
public class IntermediateRadPillConfig : IEntityConfig
{
	// Token: 0x06000B2D RID: 2861 RVA: 0x0003ED31 File Offset: 0x0003CF31
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x0003ED38 File Offset: 0x0003CF38
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("IntermediateRadPill", ITEMS.PILLS.INTERMEDIATERADPILL.NAME, ITEMS.PILLS.INTERMEDIATERADPILL.DESC, 1f, true, Assets.GetAnim("vial_radiation_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToMedicine(gameObject, MEDICINE.INTERMEDIATERADPILL);
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Carbon", 1f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("IntermediateRadPill".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		IntermediateRadPillConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("AdvancedApothecary", array, array2), array, array2)
		{
			time = 50f,
			description = ITEMS.PILLS.INTERMEDIATERADPILL.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "AdvancedApothecary" },
			sortOrder = 21
		};
		return gameObject;
	}

	// Token: 0x06000B2F RID: 2863 RVA: 0x0003EE3B File Offset: 0x0003D03B
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000B30 RID: 2864 RVA: 0x0003EE3D File Offset: 0x0003D03D
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000688 RID: 1672
	public const string ID = "IntermediateRadPill";

	// Token: 0x04000689 RID: 1673
	public static ComplexRecipe recipe;
}
