using System;
using System.Collections.Generic;
using Database;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BDC RID: 3036
public class SelectedRecipeQueueScreen : KScreen
{
	// Token: 0x06005FA7 RID: 24487 RVA: 0x00230464 File Offset: 0x0022E664
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.DecrementButton.onClick = delegate
		{
			this.target.DecrementRecipeQueueCount(this.selectedRecipe, false);
			this.RefreshQueueCountDisplay();
			this.ownerScreen.RefreshQueueCountDisplayForRecipe(this.selectedRecipe, this.target);
		};
		this.IncrementButton.onClick = delegate
		{
			this.target.IncrementRecipeQueueCount(this.selectedRecipe);
			this.RefreshQueueCountDisplay();
			this.ownerScreen.RefreshQueueCountDisplayForRecipe(this.selectedRecipe, this.target);
		};
		this.InfiniteButton.GetComponentInChildren<LocText>().text = UI.UISIDESCREENS.FABRICATORSIDESCREEN.RECIPE_FOREVER;
		this.InfiniteButton.onClick += delegate
		{
			if (this.target.GetRecipeQueueCount(this.selectedRecipe) != ComplexFabricator.QUEUE_INFINITE)
			{
				this.target.SetRecipeQueueCount(this.selectedRecipe, ComplexFabricator.QUEUE_INFINITE);
			}
			else
			{
				this.target.SetRecipeQueueCount(this.selectedRecipe, 0);
			}
			this.RefreshQueueCountDisplay();
			this.ownerScreen.RefreshQueueCountDisplayForRecipe(this.selectedRecipe, this.target);
		};
		this.QueueCount.onEndEdit += delegate
		{
			base.isEditing = false;
			this.target.SetRecipeQueueCount(this.selectedRecipe, Mathf.RoundToInt(this.QueueCount.currentValue));
			this.RefreshQueueCountDisplay();
			this.ownerScreen.RefreshQueueCountDisplayForRecipe(this.selectedRecipe, this.target);
		};
		this.QueueCount.onStartEdit += delegate
		{
			base.isEditing = true;
			KScreenManager.Instance.RefreshStack();
		};
		MultiToggle multiToggle = this.previousRecipeButton;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(this.CyclePreviousRecipe));
		MultiToggle multiToggle2 = this.nextRecipeButton;
		multiToggle2.onClick = (System.Action)Delegate.Combine(multiToggle2.onClick, new System.Action(this.CycleNextRecipe));
	}

	// Token: 0x06005FA8 RID: 24488 RVA: 0x00230554 File Offset: 0x0022E754
	public void SetRecipe(ComplexFabricatorSideScreen owner, ComplexFabricator target, ComplexRecipe recipe)
	{
		this.ownerScreen = owner;
		this.target = target;
		this.selectedRecipe = recipe;
		this.recipeName.text = recipe.GetUIName(false);
		global::Tuple<Sprite, Color> tuple = ((recipe.nameDisplay == ComplexRecipe.RecipeNameDisplay.Ingredient) ? Def.GetUISprite(recipe.ingredients[0].material, "ui", false) : Def.GetUISprite(recipe.results[0].material, recipe.results[0].facadeID));
		if (recipe.nameDisplay == ComplexRecipe.RecipeNameDisplay.HEP)
		{
			this.recipeIcon.sprite = owner.radboltSprite;
		}
		else
		{
			this.recipeIcon.sprite = tuple.first;
			this.recipeIcon.color = tuple.second;
		}
		this.recipeMainDescription.SetText(recipe.description);
		this.RefreshIngredientDescriptors();
		this.RefreshResultDescriptors();
		this.RefreshQueueCountDisplay();
		this.ToggleAndRefreshMinionDisplay();
	}

	// Token: 0x06005FA9 RID: 24489 RVA: 0x00230636 File Offset: 0x0022E836
	private void CyclePreviousRecipe()
	{
		this.ownerScreen.CycleRecipe(-1);
	}

	// Token: 0x06005FAA RID: 24490 RVA: 0x00230644 File Offset: 0x0022E844
	private void CycleNextRecipe()
	{
		this.ownerScreen.CycleRecipe(1);
	}

	// Token: 0x06005FAB RID: 24491 RVA: 0x00230652 File Offset: 0x0022E852
	private void ToggleAndRefreshMinionDisplay()
	{
		this.minionWidget.gameObject.SetActive(this.RefreshMinionDisplayAnim());
	}

	// Token: 0x06005FAC RID: 24492 RVA: 0x0023066C File Offset: 0x0022E86C
	private bool RefreshMinionDisplayAnim()
	{
		GameObject prefab = Assets.GetPrefab(this.selectedRecipe.results[0].material);
		if (prefab == null)
		{
			return false;
		}
		Equippable component = prefab.GetComponent<Equippable>();
		if (component == null)
		{
			return false;
		}
		KAnimFile buildOverride = component.GetBuildOverride();
		if (buildOverride == null)
		{
			return false;
		}
		this.minionWidget.SetDefaultPortraitAnimator();
		KAnimFile kanimFile = buildOverride;
		if (!this.selectedRecipe.results[0].facadeID.IsNullOrWhiteSpace())
		{
			EquippableFacadeResource equippableFacadeResource = Db.GetEquippableFacades().TryGet(this.selectedRecipe.results[0].facadeID);
			if (equippableFacadeResource != null)
			{
				kanimFile = Assets.GetAnim(equippableFacadeResource.BuildOverride);
			}
		}
		this.minionWidget.UpdateEquipment(component, kanimFile);
		return true;
	}

	// Token: 0x06005FAD RID: 24493 RVA: 0x00230728 File Offset: 0x0022E928
	private void RefreshQueueCountDisplay()
	{
		bool flag = this.target.GetRecipeQueueCount(this.selectedRecipe) == ComplexFabricator.QUEUE_INFINITE;
		if (!flag)
		{
			this.QueueCount.SetAmount((float)this.target.GetRecipeQueueCount(this.selectedRecipe));
		}
		else
		{
			this.QueueCount.SetDisplayValue("");
		}
		this.InfiniteIcon.gameObject.SetActive(flag);
	}

	// Token: 0x06005FAE RID: 24494 RVA: 0x00230794 File Offset: 0x0022E994
	private void RefreshResultDescriptors()
	{
		List<SelectedRecipeQueueScreen.DescriptorWithSprite> list = new List<SelectedRecipeQueueScreen.DescriptorWithSprite>();
		list.AddRange(this.GetResultDescriptions(this.selectedRecipe));
		foreach (Descriptor descriptor in this.target.AdditionalEffectsForRecipe(this.selectedRecipe))
		{
			list.Add(new SelectedRecipeQueueScreen.DescriptorWithSprite(descriptor, null, false));
		}
		if (list.Count > 0)
		{
			this.EffectsDescriptorPanel.gameObject.SetActive(true);
			foreach (KeyValuePair<SelectedRecipeQueueScreen.DescriptorWithSprite, GameObject> keyValuePair in this.recipeEffectsDescriptorRows)
			{
				Util.KDestroyGameObject(keyValuePair.Value);
			}
			this.recipeEffectsDescriptorRows.Clear();
			foreach (SelectedRecipeQueueScreen.DescriptorWithSprite descriptorWithSprite in list)
			{
				GameObject gameObject = Util.KInstantiateUI(this.recipeElementDescriptorPrefab, this.EffectsDescriptorPanel.gameObject, true);
				HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
				component.GetReference<LocText>("Label").SetText(descriptorWithSprite.descriptor.IndentedText());
				component.GetReference<Image>("Icon").sprite = ((descriptorWithSprite.tintedSprite == null) ? null : descriptorWithSprite.tintedSprite.first);
				component.GetReference<Image>("Icon").color = ((descriptorWithSprite.tintedSprite == null) ? Color.white : descriptorWithSprite.tintedSprite.second);
				component.GetReference<RectTransform>("FilterControls").gameObject.SetActive(false);
				component.GetReference<ToolTip>("Tooltip").SetSimpleTooltip(descriptorWithSprite.descriptor.tooltipText);
				this.recipeEffectsDescriptorRows.Add(descriptorWithSprite, gameObject);
			}
		}
	}

	// Token: 0x06005FAF RID: 24495 RVA: 0x00230994 File Offset: 0x0022EB94
	private List<SelectedRecipeQueueScreen.DescriptorWithSprite> GetResultDescriptions(ComplexRecipe recipe)
	{
		List<SelectedRecipeQueueScreen.DescriptorWithSprite> list = new List<SelectedRecipeQueueScreen.DescriptorWithSprite>();
		if (recipe.producedHEP > 0)
		{
			list.Add(new SelectedRecipeQueueScreen.DescriptorWithSprite(new Descriptor(string.Format("<b>{0}</b>: {1}", UI.FormatAsLink(ITEMS.RADIATION.HIGHENERGYPARITCLE.NAME, "HEP"), recipe.producedHEP), string.Format("<b>{0}</b>: {1}", ITEMS.RADIATION.HIGHENERGYPARITCLE.NAME, recipe.producedHEP), Descriptor.DescriptorType.Requirement, false), new global::Tuple<Sprite, Color>(Assets.GetSprite("radbolt"), Color.white), false));
		}
		foreach (ComplexRecipe.RecipeElement recipeElement in recipe.results)
		{
			GameObject prefab = Assets.GetPrefab(recipeElement.material);
			string formattedByTag = GameUtil.GetFormattedByTag(recipeElement.material, recipeElement.amount, GameUtil.TimeSlice.None);
			list.Add(new SelectedRecipeQueueScreen.DescriptorWithSprite(new Descriptor(string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.RECIPEPRODUCT, recipeElement.facadeID.IsNullOrWhiteSpace() ? recipeElement.material.ProperName() : recipeElement.facadeID.ProperName(), formattedByTag), string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.TOOLTIPS.RECIPEPRODUCT, recipeElement.facadeID.IsNullOrWhiteSpace() ? recipeElement.material.ProperName() : recipeElement.facadeID.ProperName(), formattedByTag), Descriptor.DescriptorType.Requirement, false), Def.GetUISprite(recipeElement.material, recipeElement.facadeID), false));
			Element element = ElementLoader.GetElement(recipeElement.material);
			if (element != null)
			{
				List<SelectedRecipeQueueScreen.DescriptorWithSprite> list2 = new List<SelectedRecipeQueueScreen.DescriptorWithSprite>();
				foreach (Descriptor descriptor in GameUtil.GetMaterialDescriptors(element))
				{
					list2.Add(new SelectedRecipeQueueScreen.DescriptorWithSprite(descriptor, null, false));
				}
				foreach (SelectedRecipeQueueScreen.DescriptorWithSprite descriptorWithSprite in list2)
				{
					descriptorWithSprite.descriptor.IncreaseIndent();
				}
				list.AddRange(list2);
			}
			else
			{
				List<SelectedRecipeQueueScreen.DescriptorWithSprite> list3 = new List<SelectedRecipeQueueScreen.DescriptorWithSprite>();
				foreach (Descriptor descriptor2 in GameUtil.GetEffectDescriptors(GameUtil.GetAllDescriptors(prefab, false)))
				{
					list3.Add(new SelectedRecipeQueueScreen.DescriptorWithSprite(descriptor2, null, false));
				}
				foreach (SelectedRecipeQueueScreen.DescriptorWithSprite descriptorWithSprite2 in list3)
				{
					descriptorWithSprite2.descriptor.IncreaseIndent();
				}
				list.AddRange(list3);
			}
		}
		return list;
	}

	// Token: 0x06005FB0 RID: 24496 RVA: 0x00230C64 File Offset: 0x0022EE64
	private void RefreshIngredientDescriptors()
	{
		new List<SelectedRecipeQueueScreen.DescriptorWithSprite>();
		List<SelectedRecipeQueueScreen.DescriptorWithSprite> ingredientDescriptions = this.GetIngredientDescriptions(this.selectedRecipe);
		this.IngredientsDescriptorPanel.gameObject.SetActive(true);
		foreach (KeyValuePair<SelectedRecipeQueueScreen.DescriptorWithSprite, GameObject> keyValuePair in this.recipeIngredientDescriptorRows)
		{
			Util.KDestroyGameObject(keyValuePair.Value);
		}
		this.recipeIngredientDescriptorRows.Clear();
		foreach (SelectedRecipeQueueScreen.DescriptorWithSprite descriptorWithSprite in ingredientDescriptions)
		{
			GameObject gameObject = Util.KInstantiateUI(this.recipeElementDescriptorPrefab, this.IngredientsDescriptorPanel.gameObject, true);
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			component.GetReference<LocText>("Label").SetText(descriptorWithSprite.descriptor.IndentedText());
			component.GetReference<Image>("Icon").sprite = ((descriptorWithSprite.tintedSprite == null) ? null : descriptorWithSprite.tintedSprite.first);
			component.GetReference<Image>("Icon").color = ((descriptorWithSprite.tintedSprite == null) ? Color.white : descriptorWithSprite.tintedSprite.second);
			component.GetReference<RectTransform>("FilterControls").gameObject.SetActive(false);
			component.GetReference<ToolTip>("Tooltip").SetSimpleTooltip(descriptorWithSprite.descriptor.tooltipText);
			this.recipeIngredientDescriptorRows.Add(descriptorWithSprite, gameObject);
		}
	}

	// Token: 0x06005FB1 RID: 24497 RVA: 0x00230DFC File Offset: 0x0022EFFC
	private List<SelectedRecipeQueueScreen.DescriptorWithSprite> GetIngredientDescriptions(ComplexRecipe recipe)
	{
		List<SelectedRecipeQueueScreen.DescriptorWithSprite> list = new List<SelectedRecipeQueueScreen.DescriptorWithSprite>();
		foreach (ComplexRecipe.RecipeElement recipeElement in recipe.ingredients)
		{
			GameObject prefab = Assets.GetPrefab(recipeElement.material);
			string formattedByTag = GameUtil.GetFormattedByTag(recipeElement.material, recipeElement.amount, GameUtil.TimeSlice.None);
			float amount = this.target.GetMyWorld().worldInventory.GetAmount(recipeElement.material, true);
			string formattedByTag2 = GameUtil.GetFormattedByTag(recipeElement.material, amount, GameUtil.TimeSlice.None);
			string text = ((amount >= recipeElement.amount) ? string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.RECIPERQUIREMENT, prefab.GetProperName(), formattedByTag, formattedByTag2) : ("<color=#F44A47>" + string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.RECIPERQUIREMENT, prefab.GetProperName(), formattedByTag, formattedByTag2) + "</color>"));
			list.Add(new SelectedRecipeQueueScreen.DescriptorWithSprite(new Descriptor(text, text, Descriptor.DescriptorType.Requirement, false), Def.GetUISprite(recipeElement.material, "ui", false), Assets.GetPrefab(recipeElement.material).GetComponent<MutantPlant>() != null));
		}
		if (recipe.consumedHEP > 0)
		{
			HighEnergyParticleStorage component = this.target.GetComponent<HighEnergyParticleStorage>();
			list.Add(new SelectedRecipeQueueScreen.DescriptorWithSprite(new Descriptor(string.Format("<b>{0}</b>: {1} / {2}", UI.FormatAsLink(ITEMS.RADIATION.HIGHENERGYPARITCLE.NAME, "HEP"), recipe.consumedHEP, component.Particles), string.Format("<b>{0}</b>: {1} / {2}", ITEMS.RADIATION.HIGHENERGYPARITCLE.NAME, recipe.consumedHEP, component.Particles), Descriptor.DescriptorType.Requirement, false), new global::Tuple<Sprite, Color>(Assets.GetSprite("radbolt"), Color.white), false));
		}
		return list;
	}

	// Token: 0x04004186 RID: 16774
	public Image recipeIcon;

	// Token: 0x04004187 RID: 16775
	public LocText recipeName;

	// Token: 0x04004188 RID: 16776
	public LocText recipeMainDescription;

	// Token: 0x04004189 RID: 16777
	public GameObject IngredientsDescriptorPanel;

	// Token: 0x0400418A RID: 16778
	public GameObject EffectsDescriptorPanel;

	// Token: 0x0400418B RID: 16779
	public KNumberInputField QueueCount;

	// Token: 0x0400418C RID: 16780
	public MultiToggle DecrementButton;

	// Token: 0x0400418D RID: 16781
	public MultiToggle IncrementButton;

	// Token: 0x0400418E RID: 16782
	public KButton InfiniteButton;

	// Token: 0x0400418F RID: 16783
	public GameObject InfiniteIcon;

	// Token: 0x04004190 RID: 16784
	private ComplexFabricator target;

	// Token: 0x04004191 RID: 16785
	private ComplexFabricatorSideScreen ownerScreen;

	// Token: 0x04004192 RID: 16786
	private ComplexRecipe selectedRecipe;

	// Token: 0x04004193 RID: 16787
	[SerializeField]
	private GameObject recipeElementDescriptorPrefab;

	// Token: 0x04004194 RID: 16788
	private Dictionary<SelectedRecipeQueueScreen.DescriptorWithSprite, GameObject> recipeIngredientDescriptorRows = new Dictionary<SelectedRecipeQueueScreen.DescriptorWithSprite, GameObject>();

	// Token: 0x04004195 RID: 16789
	private Dictionary<SelectedRecipeQueueScreen.DescriptorWithSprite, GameObject> recipeEffectsDescriptorRows = new Dictionary<SelectedRecipeQueueScreen.DescriptorWithSprite, GameObject>();

	// Token: 0x04004196 RID: 16790
	[SerializeField]
	private FullBodyUIMinionWidget minionWidget;

	// Token: 0x04004197 RID: 16791
	[SerializeField]
	private MultiToggle previousRecipeButton;

	// Token: 0x04004198 RID: 16792
	[SerializeField]
	private MultiToggle nextRecipeButton;

	// Token: 0x02001A84 RID: 6788
	private class DescriptorWithSprite
	{
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06009343 RID: 37699 RVA: 0x003196C3 File Offset: 0x003178C3
		public Descriptor descriptor { get; }

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06009344 RID: 37700 RVA: 0x003196CB File Offset: 0x003178CB
		public global::Tuple<Sprite, Color> tintedSprite { get; }

		// Token: 0x06009345 RID: 37701 RVA: 0x003196D3 File Offset: 0x003178D3
		public DescriptorWithSprite(Descriptor desc, global::Tuple<Sprite, Color> sprite, bool filterRowVisible = false)
		{
			this.descriptor = desc;
			this.tintedSprite = sprite;
			this.showFilterRow = filterRowVisible;
		}

		// Token: 0x040077C2 RID: 30658
		public bool showFilterRow;
	}
}
