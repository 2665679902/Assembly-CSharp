using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000236 RID: 566
public class IntermediateCureConfig : IEntityConfig
{
	// Token: 0x06000B28 RID: 2856 RVA: 0x0003EBFA File Offset: 0x0003CDFA
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000B29 RID: 2857 RVA: 0x0003EC04 File Offset: 0x0003CE04
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("IntermediateCure", ITEMS.PILLS.INTERMEDIATECURE.NAME, ITEMS.PILLS.INTERMEDIATECURE.DESC, 1f, true, Assets.GetAnim("iv_slimelung_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		gameObject = EntityTemplates.ExtendEntityToMedicine(gameObject, MEDICINE.INTERMEDIATECURE);
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(SwampLilyFlowerConfig.ID, 1f),
			new ComplexRecipe.RecipeElement(SimHashes.Phosphorite.CreateTag(), 1f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("IntermediateCure", 1f)
		};
		string text = "Apothecary";
		IntermediateCureConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(text, array, array2), array, array2)
		{
			time = 100f,
			description = ITEMS.PILLS.INTERMEDIATECURE.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { text },
			sortOrder = 10,
			requiredTech = "MedicineII"
		};
		return gameObject;
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x0003ED25 File Offset: 0x0003CF25
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000B2B RID: 2859 RVA: 0x0003ED27 File Offset: 0x0003CF27
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000686 RID: 1670
	public const string ID = "IntermediateCure";

	// Token: 0x04000687 RID: 1671
	public static ComplexRecipe recipe;
}
