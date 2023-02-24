using System;
using System.Collections.Generic;
using System.Diagnostics;
using Klei;
using STRINGS;
using UnityEngine;

// Token: 0x020008B6 RID: 2230
[DebuggerDisplay("{Name}")]
public class Recipe : IHasSortOrder
{
	// Token: 0x1700047B RID: 1147
	// (get) Token: 0x06004028 RID: 16424 RVA: 0x00166551 File Offset: 0x00164751
	// (set) Token: 0x06004029 RID: 16425 RVA: 0x00166559 File Offset: 0x00164759
	public int sortOrder { get; set; }

	// Token: 0x1700047C RID: 1148
	// (get) Token: 0x0600402B RID: 16427 RVA: 0x0016656B File Offset: 0x0016476B
	// (set) Token: 0x0600402A RID: 16426 RVA: 0x00166562 File Offset: 0x00164762
	public string Name
	{
		get
		{
			if (this.nameOverride != null)
			{
				return this.nameOverride;
			}
			return this.Result.ProperName();
		}
		set
		{
			this.nameOverride = value;
		}
	}

	// Token: 0x0600402C RID: 16428 RVA: 0x00166587 File Offset: 0x00164787
	public Recipe()
	{
	}

	// Token: 0x0600402D RID: 16429 RVA: 0x0016659C File Offset: 0x0016479C
	public Recipe(string prefabId, float outputUnits = 1f, SimHashes elementOverride = (SimHashes)0, string nameOverride = null, string recipeDescription = null, int sortOrder = 0)
	{
		global::Debug.Assert(prefabId != null);
		this.Result = TagManager.Create(prefabId);
		this.ResultElementOverride = elementOverride;
		this.nameOverride = nameOverride;
		this.OutputUnits = outputUnits;
		this.Ingredients = new List<Recipe.Ingredient>();
		this.recipeDescription = recipeDescription;
		this.sortOrder = sortOrder;
		this.FabricationVisualizer = null;
	}

	// Token: 0x0600402E RID: 16430 RVA: 0x00166607 File Offset: 0x00164807
	public Recipe SetFabricator(string fabricator, float fabricationTime)
	{
		this.fabricators = new string[] { fabricator };
		this.FabricationTime = fabricationTime;
		RecipeManager.Get().Add(this);
		return this;
	}

	// Token: 0x0600402F RID: 16431 RVA: 0x0016662C File Offset: 0x0016482C
	public Recipe SetFabricators(string[] fabricators, float fabricationTime)
	{
		this.fabricators = fabricators;
		this.FabricationTime = fabricationTime;
		RecipeManager.Get().Add(this);
		return this;
	}

	// Token: 0x06004030 RID: 16432 RVA: 0x00166648 File Offset: 0x00164848
	public Recipe SetIcon(Sprite Icon)
	{
		this.Icon = Icon;
		this.IconColor = Color.white;
		return this;
	}

	// Token: 0x06004031 RID: 16433 RVA: 0x0016665D File Offset: 0x0016485D
	public Recipe SetIcon(Sprite Icon, Color IconColor)
	{
		this.Icon = Icon;
		this.IconColor = IconColor;
		return this;
	}

	// Token: 0x06004032 RID: 16434 RVA: 0x0016666E File Offset: 0x0016486E
	public Recipe AddIngredient(Recipe.Ingredient ingredient)
	{
		this.Ingredients.Add(ingredient);
		return this;
	}

	// Token: 0x06004033 RID: 16435 RVA: 0x00166680 File Offset: 0x00164880
	public Recipe.Ingredient[] GetAllIngredients(IList<Tag> selectedTags)
	{
		List<Recipe.Ingredient> list = new List<Recipe.Ingredient>();
		for (int i = 0; i < this.Ingredients.Count; i++)
		{
			float amount = this.Ingredients[i].amount;
			if (i < selectedTags.Count)
			{
				list.Add(new Recipe.Ingredient(selectedTags[i], amount));
			}
			else
			{
				list.Add(new Recipe.Ingredient(this.Ingredients[i].tag, amount));
			}
		}
		return list.ToArray();
	}

	// Token: 0x06004034 RID: 16436 RVA: 0x001666FC File Offset: 0x001648FC
	public Recipe.Ingredient[] GetAllIngredients(IList<Element> selected_elements)
	{
		List<Recipe.Ingredient> list = new List<Recipe.Ingredient>();
		for (int i = 0; i < this.Ingredients.Count; i++)
		{
			int num = (int)this.Ingredients[i].amount;
			bool flag = false;
			if (i < selected_elements.Count)
			{
				Element element = selected_elements[i];
				if (element != null && element.HasTag(this.Ingredients[i].tag))
				{
					list.Add(new Recipe.Ingredient(GameTagExtensions.Create(element.id), (float)num));
					flag = true;
				}
			}
			if (!flag)
			{
				list.Add(new Recipe.Ingredient(this.Ingredients[i].tag, (float)num));
			}
		}
		return list.ToArray();
	}

	// Token: 0x06004035 RID: 16437 RVA: 0x001667B4 File Offset: 0x001649B4
	public GameObject Craft(Storage resource_storage, IList<Tag> selectedTags)
	{
		Recipe.Ingredient[] allIngredients = this.GetAllIngredients(selectedTags);
		return this.CraftRecipe(resource_storage, allIngredients);
	}

	// Token: 0x06004036 RID: 16438 RVA: 0x001667D4 File Offset: 0x001649D4
	private GameObject CraftRecipe(Storage resource_storage, Recipe.Ingredient[] ingredientTags)
	{
		SimUtil.DiseaseInfo diseaseInfo = SimUtil.DiseaseInfo.Invalid;
		float num = 0f;
		float num2 = 0f;
		foreach (Recipe.Ingredient ingredient in ingredientTags)
		{
			GameObject gameObject = resource_storage.FindFirst(ingredient.tag);
			if (gameObject != null)
			{
				Edible component = gameObject.GetComponent<Edible>();
				if (component)
				{
					ReportManager.Instance.ReportValue(ReportManager.ReportType.CaloriesCreated, -component.Calories, StringFormatter.Replace(UI.ENDOFDAYREPORT.NOTES.CRAFTED_USED, "{0}", component.GetProperName()), UI.ENDOFDAYREPORT.NOTES.CRAFTED_CONTEXT);
				}
			}
			SimUtil.DiseaseInfo diseaseInfo2;
			float num3;
			resource_storage.ConsumeAndGetDisease(ingredient, out diseaseInfo2, out num3);
			diseaseInfo = SimUtil.CalculateFinalDiseaseInfo(diseaseInfo, diseaseInfo2);
			num = SimUtil.CalculateFinalTemperature(num2, num, ingredient.amount, num3);
			num2 += ingredient.amount;
		}
		GameObject prefab = Assets.GetPrefab(this.Result);
		GameObject gameObject2 = null;
		if (prefab != null)
		{
			gameObject2 = GameUtil.KInstantiate(prefab, Grid.SceneLayer.Ore, null, 0);
			PrimaryElement component2 = gameObject2.GetComponent<PrimaryElement>();
			gameObject2.GetComponent<KSelectable>().entityName = this.Name;
			if (component2 != null)
			{
				gameObject2.GetComponent<KPrefabID>().RemoveTag(TagManager.Create("Vacuum"));
				if (this.ResultElementOverride != (SimHashes)0)
				{
					if (component2.GetComponent<ElementChunk>() != null)
					{
						component2.SetElement(this.ResultElementOverride, true);
					}
					else
					{
						component2.ElementID = this.ResultElementOverride;
					}
				}
				component2.Temperature = num;
				component2.Units = this.OutputUnits;
			}
			Edible component3 = gameObject2.GetComponent<Edible>();
			if (component3)
			{
				ReportManager.Instance.ReportValue(ReportManager.ReportType.CaloriesCreated, component3.Calories, StringFormatter.Replace(UI.ENDOFDAYREPORT.NOTES.CRAFTED, "{0}", component3.GetProperName()), UI.ENDOFDAYREPORT.NOTES.CRAFTED_CONTEXT);
			}
			gameObject2.SetActive(true);
			if (component2 != null)
			{
				component2.AddDisease(diseaseInfo.idx, diseaseInfo.count, "Recipe.CraftRecipe");
			}
			gameObject2.GetComponent<KMonoBehaviour>().Trigger(748399584, null);
		}
		return gameObject2;
	}

	// Token: 0x1700047D RID: 1149
	// (get) Token: 0x06004037 RID: 16439 RVA: 0x001669DC File Offset: 0x00164BDC
	public string[] MaterialOptionNames
	{
		get
		{
			List<string> list = new List<string>();
			foreach (Element element in ElementLoader.elements)
			{
				if (Array.IndexOf<Tag>(element.oreTags, this.Ingredients[0].tag) >= 0)
				{
					list.Add(element.id.ToString());
				}
			}
			return list.ToArray();
		}
	}

	// Token: 0x06004038 RID: 16440 RVA: 0x00166A6C File Offset: 0x00164C6C
	public Element[] MaterialOptions()
	{
		List<Element> list = new List<Element>();
		foreach (Element element in ElementLoader.elements)
		{
			if (Array.IndexOf<Tag>(element.oreTags, this.Ingredients[0].tag) >= 0)
			{
				list.Add(element);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06004039 RID: 16441 RVA: 0x00166AEC File Offset: 0x00164CEC
	public BuildingDef GetBuildingDef()
	{
		BuildingComplete component = Assets.GetPrefab(this.Result).GetComponent<BuildingComplete>();
		if (component != null)
		{
			return component.Def;
		}
		return null;
	}

	// Token: 0x0600403A RID: 16442 RVA: 0x00166B1C File Offset: 0x00164D1C
	public Sprite GetUIIcon()
	{
		Sprite sprite = null;
		if (this.Icon != null)
		{
			sprite = this.Icon;
		}
		else
		{
			KBatchedAnimController component = Assets.GetPrefab(this.Result).GetComponent<KBatchedAnimController>();
			if (component != null)
			{
				sprite = Def.GetUISpriteFromMultiObjectAnim(component.AnimFiles[0], "ui", false, "");
			}
		}
		return sprite;
	}

	// Token: 0x0600403B RID: 16443 RVA: 0x00166B76 File Offset: 0x00164D76
	public Color GetUIColor()
	{
		if (!(this.Icon != null))
		{
			return Color.white;
		}
		return this.IconColor;
	}

	// Token: 0x04002A0D RID: 10765
	private string nameOverride;

	// Token: 0x04002A0E RID: 10766
	public string HotKey;

	// Token: 0x04002A0F RID: 10767
	public string Type;

	// Token: 0x04002A10 RID: 10768
	public List<Recipe.Ingredient> Ingredients;

	// Token: 0x04002A11 RID: 10769
	public string recipeDescription;

	// Token: 0x04002A12 RID: 10770
	public Tag Result;

	// Token: 0x04002A13 RID: 10771
	public GameObject FabricationVisualizer;

	// Token: 0x04002A14 RID: 10772
	public SimHashes ResultElementOverride;

	// Token: 0x04002A15 RID: 10773
	public Sprite Icon;

	// Token: 0x04002A16 RID: 10774
	public Color IconColor = Color.white;

	// Token: 0x04002A17 RID: 10775
	public string[] fabricators;

	// Token: 0x04002A18 RID: 10776
	public float OutputUnits;

	// Token: 0x04002A19 RID: 10777
	public float FabricationTime;

	// Token: 0x04002A1A RID: 10778
	public string TechUnlock;

	// Token: 0x0200168A RID: 5770
	[DebuggerDisplay("{tag} {amount}")]
	[Serializable]
	public class Ingredient
	{
		// Token: 0x060087EC RID: 34796 RVA: 0x002F442C File Offset: 0x002F262C
		public Ingredient(string tag, float amount)
		{
			this.tag = TagManager.Create(tag);
			this.amount = amount;
		}

		// Token: 0x060087ED RID: 34797 RVA: 0x002F4447 File Offset: 0x002F2647
		public Ingredient(Tag tag, float amount)
		{
			this.tag = tag;
			this.amount = amount;
		}

		// Token: 0x060087EE RID: 34798 RVA: 0x002F4460 File Offset: 0x002F2660
		public List<Element> GetElementOptions()
		{
			List<Element> list = new List<Element>(ElementLoader.elements);
			list.RemoveAll((Element e) => !e.IsSolid);
			list.RemoveAll((Element e) => !e.HasTag(this.tag));
			return list;
		}

		// Token: 0x04006A1A RID: 27162
		public Tag tag;

		// Token: 0x04006A1B RID: 27163
		public float amount;
	}
}
