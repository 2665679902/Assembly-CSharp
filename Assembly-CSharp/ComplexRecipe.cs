using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020009EC RID: 2540
public class ComplexRecipe
{
	// Token: 0x170005B5 RID: 1461
	// (get) Token: 0x06004BE7 RID: 19431 RVA: 0x001A9BB2 File Offset: 0x001A7DB2
	// (set) Token: 0x06004BE8 RID: 19432 RVA: 0x001A9BBA File Offset: 0x001A7DBA
	public bool ProductHasFacade { get; set; }

	// Token: 0x170005B6 RID: 1462
	// (get) Token: 0x06004BE9 RID: 19433 RVA: 0x001A9BC3 File Offset: 0x001A7DC3
	public Tag FirstResult
	{
		get
		{
			return this.results[0].material;
		}
	}

	// Token: 0x06004BEA RID: 19434 RVA: 0x001A9BD2 File Offset: 0x001A7DD2
	public ComplexRecipe(string id, ComplexRecipe.RecipeElement[] ingredients, ComplexRecipe.RecipeElement[] results)
	{
		this.id = id;
		this.ingredients = ingredients;
		this.results = results;
		ComplexRecipeManager.Get().Add(this);
	}

	// Token: 0x06004BEB RID: 19435 RVA: 0x001A9C05 File Offset: 0x001A7E05
	public ComplexRecipe(string id, ComplexRecipe.RecipeElement[] ingredients, ComplexRecipe.RecipeElement[] results, int consumedHEP, int producedHEP)
		: this(id, ingredients, results)
	{
		this.consumedHEP = consumedHEP;
		this.producedHEP = producedHEP;
	}

	// Token: 0x06004BEC RID: 19436 RVA: 0x001A9C20 File Offset: 0x001A7E20
	public ComplexRecipe(string id, ComplexRecipe.RecipeElement[] ingredients, ComplexRecipe.RecipeElement[] results, int consumedHEP)
		: this(id, ingredients, results, consumedHEP, 0)
	{
	}

	// Token: 0x06004BED RID: 19437 RVA: 0x001A9C30 File Offset: 0x001A7E30
	public float TotalResultUnits()
	{
		float num = 0f;
		foreach (ComplexRecipe.RecipeElement recipeElement in this.results)
		{
			num += recipeElement.amount;
		}
		return num;
	}

	// Token: 0x06004BEE RID: 19438 RVA: 0x001A9C66 File Offset: 0x001A7E66
	public bool RequiresTechUnlock()
	{
		return !string.IsNullOrEmpty(this.requiredTech);
	}

	// Token: 0x06004BEF RID: 19439 RVA: 0x001A9C76 File Offset: 0x001A7E76
	public bool IsRequiredTechUnlocked()
	{
		return string.IsNullOrEmpty(this.requiredTech) || Db.Get().Techs.Get(this.requiredTech).IsComplete();
	}

	// Token: 0x06004BF0 RID: 19440 RVA: 0x001A9CA4 File Offset: 0x001A7EA4
	public Sprite GetUIIcon()
	{
		Sprite sprite = null;
		KBatchedAnimController component = Assets.GetPrefab((this.nameDisplay == ComplexRecipe.RecipeNameDisplay.Ingredient) ? this.ingredients[0].material : this.results[0].material).GetComponent<KBatchedAnimController>();
		if (component != null)
		{
			sprite = Def.GetUISpriteFromMultiObjectAnim(component.AnimFiles[0], "ui", false, "");
		}
		return sprite;
	}

	// Token: 0x06004BF1 RID: 19441 RVA: 0x001A9D05 File Offset: 0x001A7F05
	public Color GetUIColor()
	{
		return Color.white;
	}

	// Token: 0x06004BF2 RID: 19442 RVA: 0x001A9D0C File Offset: 0x001A7F0C
	public string GetUIName(bool includeAmounts)
	{
		string text = (this.results[0].facadeID.IsNullOrWhiteSpace() ? this.results[0].material.ProperName() : this.results[0].facadeID.ProperName());
		switch (this.nameDisplay)
		{
		case ComplexRecipe.RecipeNameDisplay.Result:
			if (includeAmounts)
			{
				return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_SIMPLE_INCLUDE_AMOUNTS, text, this.results[0].amount);
			}
			return text;
		case ComplexRecipe.RecipeNameDisplay.IngredientToResult:
			if (includeAmounts)
			{
				return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_FROM_TO_INCLUDE_AMOUNTS, new object[]
				{
					this.ingredients[0].material.ProperName(),
					text,
					this.ingredients[0].amount,
					this.results[0].amount
				});
			}
			return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_FROM_TO, this.ingredients[0].material.ProperName(), text);
		case ComplexRecipe.RecipeNameDisplay.ResultWithIngredient:
			if (includeAmounts)
			{
				return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_WITH_INCLUDE_AMOUNTS, new object[]
				{
					this.ingredients[0].material.ProperName(),
					text,
					this.ingredients[0].amount,
					this.results[0].amount
				});
			}
			return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_WITH, this.ingredients[0].material.ProperName(), text);
		case ComplexRecipe.RecipeNameDisplay.Composite:
			if (includeAmounts)
			{
				return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_FROM_TO_COMPOSITE_INCLUDE_AMOUNTS, new object[]
				{
					this.ingredients[0].material.ProperName(),
					text,
					this.results[1].material.ProperName(),
					this.ingredients[0].amount,
					this.results[0].amount,
					this.results[1].amount
				});
			}
			return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_FROM_TO_COMPOSITE, this.ingredients[0].material.ProperName(), text, this.results[1].material.ProperName());
		case ComplexRecipe.RecipeNameDisplay.HEP:
			if (includeAmounts)
			{
				return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_FROM_TO_HEP_INCLUDE_AMOUNTS, new object[]
				{
					this.ingredients[0].material.ProperName(),
					this.results[1].material.ProperName(),
					this.ingredients[0].amount,
					this.producedHEP,
					this.results[1].amount
				});
			}
			return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_FROM_TO_HEP, this.ingredients[0].material.ProperName(), text);
		case ComplexRecipe.RecipeNameDisplay.Custom:
			return this.customName;
		}
		if (includeAmounts)
		{
			return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_SIMPLE_INCLUDE_AMOUNTS, this.ingredients[0].material.ProperName(), this.ingredients[0].amount);
		}
		return this.ingredients[0].material.ProperName();
	}

	// Token: 0x040031FD RID: 12797
	public string id;

	// Token: 0x040031FE RID: 12798
	public ComplexRecipe.RecipeElement[] ingredients;

	// Token: 0x040031FF RID: 12799
	public ComplexRecipe.RecipeElement[] results;

	// Token: 0x04003200 RID: 12800
	public float time;

	// Token: 0x04003201 RID: 12801
	public GameObject FabricationVisualizer;

	// Token: 0x04003202 RID: 12802
	public int consumedHEP;

	// Token: 0x04003203 RID: 12803
	public int producedHEP;

	// Token: 0x04003204 RID: 12804
	public string recipeCategoryID = "";

	// Token: 0x04003206 RID: 12806
	public ComplexRecipe.RecipeNameDisplay nameDisplay;

	// Token: 0x04003207 RID: 12807
	public string customName;

	// Token: 0x04003208 RID: 12808
	public string description;

	// Token: 0x04003209 RID: 12809
	public List<Tag> fabricators;

	// Token: 0x0400320A RID: 12810
	public int sortOrder;

	// Token: 0x0400320B RID: 12811
	public string requiredTech;

	// Token: 0x020017F4 RID: 6132
	public enum RecipeNameDisplay
	{
		// Token: 0x04006E7D RID: 28285
		Ingredient,
		// Token: 0x04006E7E RID: 28286
		Result,
		// Token: 0x04006E7F RID: 28287
		IngredientToResult,
		// Token: 0x04006E80 RID: 28288
		ResultWithIngredient,
		// Token: 0x04006E81 RID: 28289
		Composite,
		// Token: 0x04006E82 RID: 28290
		HEP,
		// Token: 0x04006E83 RID: 28291
		Custom
	}

	// Token: 0x020017F5 RID: 6133
	public class RecipeElement
	{
		// Token: 0x06008C71 RID: 35953 RVA: 0x00302987 File Offset: 0x00300B87
		public RecipeElement(Tag material, float amount, bool inheritElement)
		{
			this.material = material;
			this.amount = amount;
			this.temperatureOperation = ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature;
			this.inheritElement = inheritElement;
		}

		// Token: 0x06008C72 RID: 35954 RVA: 0x003029AB File Offset: 0x00300BAB
		public RecipeElement(Tag material, float amount)
		{
			this.material = material;
			this.amount = amount;
			this.temperatureOperation = ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature;
		}

		// Token: 0x06008C73 RID: 35955 RVA: 0x003029C8 File Offset: 0x00300BC8
		public RecipeElement(Tag material, float amount, ComplexRecipe.RecipeElement.TemperatureOperation temperatureOperation, bool storeElement = false)
		{
			this.material = material;
			this.amount = amount;
			this.temperatureOperation = temperatureOperation;
			this.storeElement = storeElement;
		}

		// Token: 0x06008C74 RID: 35956 RVA: 0x003029ED File Offset: 0x00300BED
		public RecipeElement(Tag material, float amount, ComplexRecipe.RecipeElement.TemperatureOperation temperatureOperation, string facadeID, bool storeElement = false)
		{
			this.material = material;
			this.amount = amount;
			this.temperatureOperation = temperatureOperation;
			this.storeElement = storeElement;
			this.facadeID = facadeID;
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06008C75 RID: 35957 RVA: 0x00302A1A File Offset: 0x00300C1A
		// (set) Token: 0x06008C76 RID: 35958 RVA: 0x00302A22 File Offset: 0x00300C22
		public float amount { get; private set; }

		// Token: 0x04006E84 RID: 28292
		public Tag material;

		// Token: 0x04006E86 RID: 28294
		public ComplexRecipe.RecipeElement.TemperatureOperation temperatureOperation;

		// Token: 0x04006E87 RID: 28295
		public bool storeElement;

		// Token: 0x04006E88 RID: 28296
		public bool inheritElement;

		// Token: 0x04006E89 RID: 28297
		public string facadeID;

		// Token: 0x020020DF RID: 8415
		public enum TemperatureOperation
		{
			// Token: 0x0400926E RID: 37486
			AverageTemperature,
			// Token: 0x0400926F RID: 37487
			Heated,
			// Token: 0x04009270 RID: 37488
			Melted
		}
	}
}
