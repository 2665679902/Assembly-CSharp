using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A6E RID: 2670
public class CodexRecipePanel : CodexWidget<CodexRecipePanel>
{
	// Token: 0x17000613 RID: 1555
	// (get) Token: 0x060051A5 RID: 20901 RVA: 0x001D7778 File Offset: 0x001D5978
	// (set) Token: 0x060051A6 RID: 20902 RVA: 0x001D7780 File Offset: 0x001D5980
	public string linkID { get; set; }

	// Token: 0x060051A7 RID: 20903 RVA: 0x001D7789 File Offset: 0x001D5989
	public CodexRecipePanel()
	{
	}

	// Token: 0x060051A8 RID: 20904 RVA: 0x001D7791 File Offset: 0x001D5991
	public CodexRecipePanel(ComplexRecipe recipe)
	{
		this.complexRecipe = recipe;
	}

	// Token: 0x060051A9 RID: 20905 RVA: 0x001D77A0 File Offset: 0x001D59A0
	public CodexRecipePanel(Recipe rec)
	{
		this.recipe = rec;
	}

	// Token: 0x060051AA RID: 20906 RVA: 0x001D77B0 File Offset: 0x001D59B0
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		HierarchyReferences component = contentGameObject.GetComponent<HierarchyReferences>();
		this.title = component.GetReference<LocText>("Title");
		this.materialPrefab = component.GetReference<RectTransform>("MaterialPrefab").gameObject;
		this.fabricatorPrefab = component.GetReference<RectTransform>("FabricatorPrefab").gameObject;
		this.ingredientsContainer = component.GetReference<RectTransform>("IngredientsContainer").gameObject;
		this.resultsContainer = component.GetReference<RectTransform>("ResultsContainer").gameObject;
		this.fabricatorContainer = component.GetReference<RectTransform>("FabricatorContainer").gameObject;
		this.ClearPanel();
		if (this.recipe != null)
		{
			this.ConfigureRecipe();
			return;
		}
		if (this.complexRecipe != null)
		{
			this.ConfigureComplexRecipe();
		}
	}

	// Token: 0x060051AB RID: 20907 RVA: 0x001D7868 File Offset: 0x001D5A68
	private void ConfigureRecipe()
	{
		this.title.text = this.recipe.Result.ProperName();
		foreach (Recipe.Ingredient ingredient in this.recipe.Ingredients)
		{
			GameObject gameObject = Util.KInstantiateUI(this.materialPrefab, this.ingredientsContainer, true);
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(ingredient.tag, "ui", false);
			component.GetReference<Image>("Icon").sprite = uisprite.first;
			component.GetReference<Image>("Icon").color = uisprite.second;
			component.GetReference<LocText>("Amount").text = GameUtil.GetFormattedByTag(ingredient.tag, ingredient.amount, GameUtil.TimeSlice.None);
			component.GetReference<LocText>("Amount").color = Color.black;
			string text = ingredient.tag.ProperName();
			GameObject prefab = Assets.GetPrefab(ingredient.tag);
			if (prefab.GetComponent<Edible>() != null)
			{
				text = text + "\n    • " + string.Format(UI.GAMEOBJECTEFFECTS.FOOD_QUALITY, GameUtil.GetFormattedFoodQuality(prefab.GetComponent<Edible>().GetQuality()));
			}
			gameObject.GetComponent<ToolTip>().toolTip = text;
		}
		GameObject gameObject2 = Util.KInstantiateUI(this.materialPrefab, this.resultsContainer, true);
		HierarchyReferences component2 = gameObject2.GetComponent<HierarchyReferences>();
		global::Tuple<Sprite, Color> uisprite2 = Def.GetUISprite(this.recipe.Result, "ui", false);
		component2.GetReference<Image>("Icon").sprite = uisprite2.first;
		component2.GetReference<Image>("Icon").color = uisprite2.second;
		component2.GetReference<LocText>("Amount").text = GameUtil.GetFormattedByTag(this.recipe.Result, this.recipe.OutputUnits, GameUtil.TimeSlice.None);
		component2.GetReference<LocText>("Amount").color = Color.black;
		string text2 = this.recipe.Result.ProperName();
		GameObject prefab2 = Assets.GetPrefab(this.recipe.Result);
		if (prefab2.GetComponent<Edible>() != null)
		{
			text2 = text2 + "\n    • " + string.Format(UI.GAMEOBJECTEFFECTS.FOOD_QUALITY, GameUtil.GetFormattedFoodQuality(prefab2.GetComponent<Edible>().GetQuality()));
		}
		gameObject2.GetComponent<ToolTip>().toolTip = text2;
	}

	// Token: 0x060051AC RID: 20908 RVA: 0x001D7AEC File Offset: 0x001D5CEC
	private void ConfigureComplexRecipe()
	{
		this.title.text = this.complexRecipe.results[0].material.ProperName();
		ComplexRecipe.RecipeElement[] array = this.complexRecipe.ingredients;
		for (int i = 0; i < array.Length; i++)
		{
			ComplexRecipe.RecipeElement ing = array[i];
			HierarchyReferences component = Util.KInstantiateUI(this.materialPrefab, this.ingredientsContainer, true).GetComponent<HierarchyReferences>();
			global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(ing.material, "ui", false);
			component.GetReference<Image>("Icon").sprite = uisprite.first;
			component.GetReference<Image>("Icon").color = uisprite.second;
			component.GetReference<LocText>("Amount").text = GameUtil.GetFormattedByTag(ing.material, ing.amount, GameUtil.TimeSlice.None);
			component.GetReference<LocText>("Amount").color = Color.black;
			string text = ing.material.ProperName();
			GameObject prefab = Assets.GetPrefab(ing.material);
			if (prefab.GetComponent<Edible>() != null)
			{
				text = text + "\n    • " + string.Format(UI.GAMEOBJECTEFFECTS.FOOD_QUALITY, GameUtil.GetFormattedFoodQuality(prefab.GetComponent<Edible>().GetQuality()));
			}
			component.GetReference<ToolTip>("Tooltip").toolTip = text;
			component.GetReference<KButton>("Button").onClick += delegate
			{
				ManagementMenu.Instance.codexScreen.ChangeArticle(UI.ExtractLinkID(Assets.GetPrefab(ing.material).GetProperName()), false, default(Vector3), CodexScreen.HistoryDirection.NewArticle);
			};
		}
		array = this.complexRecipe.results;
		for (int i = 0; i < array.Length; i++)
		{
			ComplexRecipe.RecipeElement res = array[i];
			HierarchyReferences component2 = Util.KInstantiateUI(this.materialPrefab, this.resultsContainer, true).GetComponent<HierarchyReferences>();
			global::Tuple<Sprite, Color> uisprite2 = Def.GetUISprite(res.material, "ui", false);
			component2.GetReference<Image>("Icon").sprite = uisprite2.first;
			component2.GetReference<Image>("Icon").color = uisprite2.second;
			component2.GetReference<LocText>("Amount").text = GameUtil.GetFormattedByTag(res.material, res.amount, GameUtil.TimeSlice.None);
			component2.GetReference<LocText>("Amount").color = Color.black;
			string text2 = res.material.ProperName();
			GameObject prefab2 = Assets.GetPrefab(res.material);
			if (prefab2.GetComponent<Edible>() != null)
			{
				text2 = text2 + "\n    • " + string.Format(UI.GAMEOBJECTEFFECTS.FOOD_QUALITY, GameUtil.GetFormattedFoodQuality(prefab2.GetComponent<Edible>().GetQuality()));
			}
			component2.GetReference<ToolTip>("Tooltip").toolTip = text2;
			component2.GetReference<KButton>("Button").onClick += delegate
			{
				ManagementMenu.Instance.codexScreen.ChangeArticle(UI.ExtractLinkID(Assets.GetPrefab(res.material).GetProperName()), false, default(Vector3), CodexScreen.HistoryDirection.NewArticle);
			};
		}
		string text3 = this.complexRecipe.id.Substring(0, this.complexRecipe.id.IndexOf('_'));
		HierarchyReferences component3 = Util.KInstantiateUI(this.fabricatorPrefab, this.fabricatorContainer, true).GetComponent<HierarchyReferences>();
		global::Tuple<Sprite, Color> uisprite3 = Def.GetUISprite(text3, "ui", false);
		component3.GetReference<Image>("Icon").sprite = uisprite3.first;
		component3.GetReference<Image>("Icon").color = uisprite3.second;
		component3.GetReference<LocText>("Time").text = GameUtil.GetFormattedTime(this.complexRecipe.time, "F0");
		component3.GetReference<LocText>("Time").color = Color.black;
		GameObject fabricator = Assets.GetPrefab(text3.ToTag());
		component3.GetReference<ToolTip>("Tooltip").toolTip = fabricator.GetProperName();
		component3.GetReference<KButton>("Button").onClick += delegate
		{
			ManagementMenu.Instance.codexScreen.ChangeArticle(UI.ExtractLinkID(fabricator.GetProperName()), false, default(Vector3), CodexScreen.HistoryDirection.NewArticle);
		};
	}

	// Token: 0x060051AD RID: 20909 RVA: 0x001D7EF0 File Offset: 0x001D60F0
	private void ClearPanel()
	{
		foreach (object obj in this.ingredientsContainer.transform)
		{
			UnityEngine.Object.Destroy(((Transform)obj).gameObject);
		}
		foreach (object obj2 in this.resultsContainer.transform)
		{
			UnityEngine.Object.Destroy(((Transform)obj2).gameObject);
		}
		foreach (object obj3 in this.fabricatorContainer.transform)
		{
			UnityEngine.Object.Destroy(((Transform)obj3).gameObject);
		}
	}

	// Token: 0x040036B6 RID: 14006
	private LocText title;

	// Token: 0x040036B7 RID: 14007
	private GameObject materialPrefab;

	// Token: 0x040036B8 RID: 14008
	private GameObject fabricatorPrefab;

	// Token: 0x040036B9 RID: 14009
	private GameObject ingredientsContainer;

	// Token: 0x040036BA RID: 14010
	private GameObject resultsContainer;

	// Token: 0x040036BB RID: 14011
	private GameObject fabricatorContainer;

	// Token: 0x040036BC RID: 14012
	private ComplexRecipe complexRecipe;

	// Token: 0x040036BD RID: 14013
	private Recipe recipe;
}
