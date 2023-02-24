using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005B6 RID: 1462
[AddComponentMenu("KMonoBehaviour/scripts/FabricatorIngredientStatusManager")]
public class FabricatorIngredientStatusManager : KMonoBehaviour, ISim1000ms
{
	// Token: 0x06002435 RID: 9269 RVA: 0x000C3E27 File Offset: 0x000C2027
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.selectable = base.GetComponent<KSelectable>();
		this.fabricator = base.GetComponent<ComplexFabricator>();
		this.InitializeBalances();
	}

	// Token: 0x06002436 RID: 9270 RVA: 0x000C3E50 File Offset: 0x000C2050
	private void InitializeBalances()
	{
		foreach (ComplexRecipe complexRecipe in this.fabricator.GetRecipes())
		{
			this.recipeRequiredResourceBalances.Add(complexRecipe, new Dictionary<Tag, float>());
			foreach (ComplexRecipe.RecipeElement recipeElement in complexRecipe.ingredients)
			{
				this.recipeRequiredResourceBalances[complexRecipe].Add(recipeElement.material, 0f);
			}
		}
	}

	// Token: 0x06002437 RID: 9271 RVA: 0x000C3EC8 File Offset: 0x000C20C8
	public void Sim1000ms(float dt)
	{
		this.RefreshStatusItems();
	}

	// Token: 0x06002438 RID: 9272 RVA: 0x000C3ED0 File Offset: 0x000C20D0
	private void RefreshStatusItems()
	{
		foreach (KeyValuePair<ComplexRecipe, Guid> keyValuePair in this.statusItems)
		{
			if (!this.fabricator.IsRecipeQueued(keyValuePair.Key))
			{
				this.deadOrderKeys.Add(keyValuePair.Key);
			}
		}
		foreach (ComplexRecipe complexRecipe in this.deadOrderKeys)
		{
			this.recipeRequiredResourceBalances[complexRecipe].Clear();
			foreach (ComplexRecipe.RecipeElement recipeElement in complexRecipe.ingredients)
			{
				this.recipeRequiredResourceBalances[complexRecipe].Add(recipeElement.material, 0f);
			}
			this.selectable.RemoveStatusItem(this.statusItems[complexRecipe], false);
			this.statusItems.Remove(complexRecipe);
		}
		this.deadOrderKeys.Clear();
		foreach (ComplexRecipe complexRecipe2 in this.fabricator.GetRecipes())
		{
			if (this.fabricator.IsRecipeQueued(complexRecipe2))
			{
				bool flag = false;
				foreach (ComplexRecipe.RecipeElement recipeElement2 in complexRecipe2.ingredients)
				{
					float num = this.fabricator.inStorage.GetAmountAvailable(recipeElement2.material) + this.fabricator.buildStorage.GetAmountAvailable(recipeElement2.material) + this.fabricator.GetMyWorld().worldInventory.GetTotalAmount(recipeElement2.material, true) - recipeElement2.amount;
					flag = flag || this.ChangeRecipeRequiredResourceBalance(complexRecipe2, recipeElement2.material, num) || (this.statusItems.ContainsKey(complexRecipe2) && this.fabricator.GetRecipeQueueCount(complexRecipe2) == 0);
				}
				if (flag)
				{
					if (this.statusItems.ContainsKey(complexRecipe2))
					{
						this.selectable.RemoveStatusItem(this.statusItems[complexRecipe2], false);
						this.statusItems.Remove(complexRecipe2);
					}
					if (this.fabricator.IsRecipeQueued(complexRecipe2))
					{
						using (Dictionary<Tag, float>.ValueCollection.Enumerator enumerator3 = this.recipeRequiredResourceBalances[complexRecipe2].Values.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								if (enumerator3.Current < 0f)
								{
									Dictionary<Tag, float> dictionary = new Dictionary<Tag, float>();
									foreach (KeyValuePair<Tag, float> keyValuePair2 in this.recipeRequiredResourceBalances[complexRecipe2])
									{
										if (keyValuePair2.Value < 0f)
										{
											dictionary.Add(keyValuePair2.Key, -keyValuePair2.Value);
										}
									}
									Guid guid = this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.MaterialsUnavailable, dictionary);
									this.statusItems.Add(complexRecipe2, guid);
									break;
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06002439 RID: 9273 RVA: 0x000C4250 File Offset: 0x000C2450
	private bool ChangeRecipeRequiredResourceBalance(ComplexRecipe recipe, Tag tag, float newBalance)
	{
		bool flag = false;
		if (this.recipeRequiredResourceBalances[recipe][tag] >= 0f != newBalance >= 0f)
		{
			flag = true;
		}
		this.recipeRequiredResourceBalances[recipe][tag] = newBalance;
		return flag;
	}

	// Token: 0x040014D5 RID: 5333
	private KSelectable selectable;

	// Token: 0x040014D6 RID: 5334
	private ComplexFabricator fabricator;

	// Token: 0x040014D7 RID: 5335
	private Dictionary<ComplexRecipe, Guid> statusItems = new Dictionary<ComplexRecipe, Guid>();

	// Token: 0x040014D8 RID: 5336
	private Dictionary<ComplexRecipe, Dictionary<Tag, float>> recipeRequiredResourceBalances = new Dictionary<ComplexRecipe, Dictionary<Tag, float>>();

	// Token: 0x040014D9 RID: 5337
	private List<ComplexRecipe> deadOrderKeys = new List<ComplexRecipe>();
}
