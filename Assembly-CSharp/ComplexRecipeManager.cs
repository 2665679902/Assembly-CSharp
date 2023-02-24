using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020009EB RID: 2539
public class ComplexRecipeManager
{
	// Token: 0x06004BDD RID: 19421 RVA: 0x001A984D File Offset: 0x001A7A4D
	public static ComplexRecipeManager Get()
	{
		if (ComplexRecipeManager._Instance == null)
		{
			ComplexRecipeManager._Instance = new ComplexRecipeManager();
		}
		return ComplexRecipeManager._Instance;
	}

	// Token: 0x06004BDE RID: 19422 RVA: 0x001A9865 File Offset: 0x001A7A65
	public static void DestroyInstance()
	{
		ComplexRecipeManager._Instance = null;
	}

	// Token: 0x06004BDF RID: 19423 RVA: 0x001A9870 File Offset: 0x001A7A70
	public static string MakeObsoleteRecipeID(string fabricator, Tag signatureElement)
	{
		string text = "_";
		Tag tag = signatureElement;
		return fabricator + text + tag.ToString();
	}

	// Token: 0x06004BE0 RID: 19424 RVA: 0x001A9898 File Offset: 0x001A7A98
	public static string MakeRecipeID(string fabricator, IList<ComplexRecipe.RecipeElement> inputs, IList<ComplexRecipe.RecipeElement> outputs)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(fabricator);
		stringBuilder.Append("_I");
		foreach (ComplexRecipe.RecipeElement recipeElement in inputs)
		{
			stringBuilder.Append("_");
			stringBuilder.Append(recipeElement.material.ToString());
		}
		stringBuilder.Append("_O");
		foreach (ComplexRecipe.RecipeElement recipeElement2 in outputs)
		{
			stringBuilder.Append("_");
			stringBuilder.Append(recipeElement2.material.ToString());
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06004BE1 RID: 19425 RVA: 0x001A9980 File Offset: 0x001A7B80
	public static string MakeRecipeID(string fabricator, IList<ComplexRecipe.RecipeElement> inputs, IList<ComplexRecipe.RecipeElement> outputs, string facadeID)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(fabricator);
		stringBuilder.Append("_I");
		foreach (ComplexRecipe.RecipeElement recipeElement in inputs)
		{
			stringBuilder.Append("_");
			stringBuilder.Append(recipeElement.material.ToString());
		}
		stringBuilder.Append("_O");
		foreach (ComplexRecipe.RecipeElement recipeElement2 in outputs)
		{
			stringBuilder.Append("_");
			stringBuilder.Append(recipeElement2.material.ToString());
		}
		stringBuilder.Append("_" + facadeID);
		return stringBuilder.ToString();
	}

	// Token: 0x06004BE2 RID: 19426 RVA: 0x001A9A78 File Offset: 0x001A7C78
	public void Add(ComplexRecipe recipe)
	{
		using (List<ComplexRecipe>.Enumerator enumerator = this.recipes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.id == recipe.id)
				{
					global::Debug.LogError(string.Format("DUPLICATE RECIPE ID! '{0}' is being added to the recipe manager multiple times. This will result in the failure to save/load certain queued recipes at fabricators.", recipe.id));
				}
			}
		}
		this.recipes.Add(recipe);
		if (recipe.FabricationVisualizer != null)
		{
			UnityEngine.Object.DontDestroyOnLoad(recipe.FabricationVisualizer);
		}
	}

	// Token: 0x06004BE3 RID: 19427 RVA: 0x001A9B10 File Offset: 0x001A7D10
	public ComplexRecipe GetRecipe(string id)
	{
		if (string.IsNullOrEmpty(id))
		{
			return null;
		}
		return this.recipes.Find((ComplexRecipe r) => r.id == id);
	}

	// Token: 0x06004BE4 RID: 19428 RVA: 0x001A9B50 File Offset: 0x001A7D50
	public void AddObsoleteIDMapping(string obsolete_id, string new_id)
	{
		this.obsoleteIDMapping[obsolete_id] = new_id;
	}

	// Token: 0x06004BE5 RID: 19429 RVA: 0x001A9B60 File Offset: 0x001A7D60
	public ComplexRecipe GetObsoleteRecipe(string id)
	{
		if (string.IsNullOrEmpty(id))
		{
			return null;
		}
		ComplexRecipe complexRecipe = null;
		string text = null;
		if (this.obsoleteIDMapping.TryGetValue(id, out text))
		{
			complexRecipe = this.GetRecipe(text);
		}
		return complexRecipe;
	}

	// Token: 0x040031FA RID: 12794
	private static ComplexRecipeManager _Instance;

	// Token: 0x040031FB RID: 12795
	public List<ComplexRecipe> recipes = new List<ComplexRecipe>();

	// Token: 0x040031FC RID: 12796
	private Dictionary<string, string> obsoleteIDMapping = new Dictionary<string, string>();
}
