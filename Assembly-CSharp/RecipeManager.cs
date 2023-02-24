using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009EA RID: 2538
public class RecipeManager
{
	// Token: 0x06004BD9 RID: 19417 RVA: 0x001A97F3 File Offset: 0x001A79F3
	public static RecipeManager Get()
	{
		if (RecipeManager._Instance == null)
		{
			RecipeManager._Instance = new RecipeManager();
		}
		return RecipeManager._Instance;
	}

	// Token: 0x06004BDA RID: 19418 RVA: 0x001A980B File Offset: 0x001A7A0B
	public static void DestroyInstance()
	{
		RecipeManager._Instance = null;
	}

	// Token: 0x06004BDB RID: 19419 RVA: 0x001A9813 File Offset: 0x001A7A13
	public void Add(Recipe recipe)
	{
		this.recipes.Add(recipe);
		if (recipe.FabricationVisualizer != null)
		{
			UnityEngine.Object.DontDestroyOnLoad(recipe.FabricationVisualizer);
		}
	}

	// Token: 0x040031F8 RID: 12792
	private static RecipeManager _Instance;

	// Token: 0x040031F9 RID: 12793
	public List<Recipe> recipes = new List<Recipe>();
}
