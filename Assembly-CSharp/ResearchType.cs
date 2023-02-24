using System;
using UnityEngine;

// Token: 0x020008DB RID: 2267
public class ResearchType
{
	// Token: 0x06004148 RID: 16712 RVA: 0x0016E141 File Offset: 0x0016C341
	public ResearchType(string id, string name, string description, Sprite sprite, Color color, Recipe.Ingredient[] fabricationIngredients, float fabricationTime, HashedString kAnim_ID, string[] fabricators, string recipeDescription)
	{
		this._id = id;
		this._name = name;
		this._description = description;
		this._sprite = sprite;
		this._color = color;
		this.CreatePrefab(fabricationIngredients, fabricationTime, kAnim_ID, fabricators, recipeDescription, color);
	}

	// Token: 0x06004149 RID: 16713 RVA: 0x0016E181 File Offset: 0x0016C381
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600414A RID: 16714 RVA: 0x0016E188 File Offset: 0x0016C388
	public GameObject CreatePrefab(Recipe.Ingredient[] fabricationIngredients, float fabricationTime, HashedString kAnim_ID, string[] fabricators, string recipeDescription, Color color)
	{
		GameObject gameObject = EntityTemplates.CreateBasicEntity(this.id, this.name, this.description, 1f, true, Assets.GetAnim(kAnim_ID), "ui", Grid.SceneLayer.BuildingFront, SimHashes.Creature, null, 293f);
		gameObject.AddOrGet<ResearchPointObject>().TypeID = this.id;
		this._recipe = new Recipe(this.id, 1f, (SimHashes)0, this.name, recipeDescription, 0);
		this._recipe.SetFabricators(fabricators, fabricationTime);
		this._recipe.SetIcon(Assets.GetSprite("research_type_icon"), color);
		if (fabricationIngredients != null)
		{
			foreach (Recipe.Ingredient ingredient in fabricationIngredients)
			{
				this._recipe.AddIngredient(ingredient);
			}
		}
		return gameObject;
	}

	// Token: 0x0600414B RID: 16715 RVA: 0x0016E24D File Offset: 0x0016C44D
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600414C RID: 16716 RVA: 0x0016E24F File Offset: 0x0016C44F
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x1700048E RID: 1166
	// (get) Token: 0x0600414D RID: 16717 RVA: 0x0016E251 File Offset: 0x0016C451
	public string id
	{
		get
		{
			return this._id;
		}
	}

	// Token: 0x1700048F RID: 1167
	// (get) Token: 0x0600414E RID: 16718 RVA: 0x0016E259 File Offset: 0x0016C459
	public string name
	{
		get
		{
			return this._name;
		}
	}

	// Token: 0x17000490 RID: 1168
	// (get) Token: 0x0600414F RID: 16719 RVA: 0x0016E261 File Offset: 0x0016C461
	public string description
	{
		get
		{
			return this._description;
		}
	}

	// Token: 0x17000491 RID: 1169
	// (get) Token: 0x06004150 RID: 16720 RVA: 0x0016E269 File Offset: 0x0016C469
	public string recipe
	{
		get
		{
			return this.recipe;
		}
	}

	// Token: 0x17000492 RID: 1170
	// (get) Token: 0x06004151 RID: 16721 RVA: 0x0016E271 File Offset: 0x0016C471
	public Color color
	{
		get
		{
			return this._color;
		}
	}

	// Token: 0x17000493 RID: 1171
	// (get) Token: 0x06004152 RID: 16722 RVA: 0x0016E279 File Offset: 0x0016C479
	public Sprite sprite
	{
		get
		{
			return this._sprite;
		}
	}

	// Token: 0x04002B8F RID: 11151
	private string _id;

	// Token: 0x04002B90 RID: 11152
	private string _name;

	// Token: 0x04002B91 RID: 11153
	private string _description;

	// Token: 0x04002B92 RID: 11154
	private Recipe _recipe;

	// Token: 0x04002B93 RID: 11155
	private Sprite _sprite;

	// Token: 0x04002B94 RID: 11156
	private Color _color;
}
