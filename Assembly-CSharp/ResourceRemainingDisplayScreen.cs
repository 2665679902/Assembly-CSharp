using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B71 RID: 2929
public class ResourceRemainingDisplayScreen : KScreen
{
	// Token: 0x06005BBD RID: 23485 RVA: 0x00216EC6 File Offset: 0x002150C6
	public static void DestroyInstance()
	{
		ResourceRemainingDisplayScreen.instance = null;
	}

	// Token: 0x06005BBE RID: 23486 RVA: 0x00216ECE File Offset: 0x002150CE
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Activate();
		ResourceRemainingDisplayScreen.instance = this;
		this.dispayPrefab.SetActive(false);
	}

	// Token: 0x06005BBF RID: 23487 RVA: 0x00216EEE File Offset: 0x002150EE
	public void ActivateDisplay(GameObject target)
	{
		this.numberOfPendingConstructions = 0;
		this.dispayPrefab.SetActive(true);
	}

	// Token: 0x06005BC0 RID: 23488 RVA: 0x00216F03 File Offset: 0x00215103
	public void DeactivateDisplay()
	{
		this.dispayPrefab.SetActive(false);
	}

	// Token: 0x06005BC1 RID: 23489 RVA: 0x00216F14 File Offset: 0x00215114
	public void SetResources(IList<Tag> _selected_elements, Recipe recipe)
	{
		this.selected_elements.Clear();
		foreach (Tag tag in _selected_elements)
		{
			this.selected_elements.Add(tag);
		}
		this.currentRecipe = recipe;
		global::Debug.Assert(this.selected_elements.Count == recipe.Ingredients.Count, string.Format("{0} Mismatch number of selected elements {1} and recipe requirements {2}", recipe.Name, this.selected_elements.Count, recipe.Ingredients.Count));
	}

	// Token: 0x06005BC2 RID: 23490 RVA: 0x00216FC0 File Offset: 0x002151C0
	public void SetNumberOfPendingConstructions(int number)
	{
		this.numberOfPendingConstructions = number;
	}

	// Token: 0x06005BC3 RID: 23491 RVA: 0x00216FCC File Offset: 0x002151CC
	public void Update()
	{
		if (!this.dispayPrefab.activeSelf)
		{
			return;
		}
		if (base.canvas != null)
		{
			if (this.rect == null)
			{
				this.rect = base.GetComponent<RectTransform>();
			}
			this.rect.anchoredPosition = base.WorldToScreen(PlayerController.GetCursorPos(KInputManager.GetMousePos()));
		}
		if (this.displayedConstructionCostMultiplier == this.numberOfPendingConstructions)
		{
			this.label.text = "";
			return;
		}
		this.displayedConstructionCostMultiplier = this.numberOfPendingConstructions;
	}

	// Token: 0x06005BC4 RID: 23492 RVA: 0x0021705C File Offset: 0x0021525C
	public string GetString()
	{
		string text = "";
		if (this.selected_elements != null && this.currentRecipe != null)
		{
			for (int i = 0; i < this.currentRecipe.Ingredients.Count; i++)
			{
				Tag tag = this.selected_elements[i];
				float num = this.currentRecipe.Ingredients[i].amount * (float)this.numberOfPendingConstructions;
				float num2 = ClusterManager.Instance.activeWorld.worldInventory.GetAmount(tag, true);
				num2 -= num;
				if (num2 < 0f)
				{
					num2 = 0f;
				}
				text = string.Concat(new string[]
				{
					text,
					tag.ProperName(),
					": ",
					GameUtil.GetFormattedMass(num2, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"),
					" / ",
					GameUtil.GetFormattedMass(this.currentRecipe.Ingredients[i].amount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")
				});
				if (i < this.selected_elements.Count - 1)
				{
					text += "\n";
				}
			}
		}
		return text;
	}

	// Token: 0x04003E92 RID: 16018
	public static ResourceRemainingDisplayScreen instance;

	// Token: 0x04003E93 RID: 16019
	public GameObject dispayPrefab;

	// Token: 0x04003E94 RID: 16020
	public LocText label;

	// Token: 0x04003E95 RID: 16021
	private Recipe currentRecipe;

	// Token: 0x04003E96 RID: 16022
	private List<Tag> selected_elements = new List<Tag>();

	// Token: 0x04003E97 RID: 16023
	private int numberOfPendingConstructions;

	// Token: 0x04003E98 RID: 16024
	private int displayedConstructionCostMultiplier;

	// Token: 0x04003E99 RID: 16025
	private RectTransform rect;
}
