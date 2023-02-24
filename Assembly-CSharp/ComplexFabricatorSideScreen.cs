using System;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BA3 RID: 2979
public class ComplexFabricatorSideScreen : SideScreenContent
{
	// Token: 0x06005DBA RID: 23994 RVA: 0x00223B7C File Offset: 0x00221D7C
	public override string GetTitle()
	{
		if (this.targetFab == null)
		{
			return Strings.Get(this.titleKey).ToString().Replace("{0}", "");
		}
		return string.Format(Strings.Get(this.titleKey), this.targetFab.GetProperName());
	}

	// Token: 0x06005DBB RID: 23995 RVA: 0x00223BD7 File Offset: 0x00221DD7
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<ComplexFabricator>() != null;
	}

	// Token: 0x06005DBC RID: 23996 RVA: 0x00223BE8 File Offset: 0x00221DE8
	public override void SetTarget(GameObject target)
	{
		ComplexFabricator component = target.GetComponent<ComplexFabricator>();
		if (component == null)
		{
			global::Debug.LogError("The object selected doesn't have a ComplexFabricator!");
			return;
		}
		if (this.targetOrdersUpdatedSubHandle != -1)
		{
			base.Unsubscribe(this.targetOrdersUpdatedSubHandle);
		}
		this.Initialize(component);
		this.targetOrdersUpdatedSubHandle = this.targetFab.Subscribe(1721324763, new Action<object>(this.UpdateQueueCountLabels));
		this.UpdateQueueCountLabels(null);
	}

	// Token: 0x06005DBD RID: 23997 RVA: 0x00223C58 File Offset: 0x00221E58
	private void UpdateQueueCountLabels(object data = null)
	{
		ComplexRecipe[] recipes = this.targetFab.GetRecipes();
		for (int i = 0; i < recipes.Length; i++)
		{
			ComplexRecipe r = recipes[i];
			GameObject gameObject = this.recipeToggles.Find((GameObject match) => this.recipeMap[match] == r);
			if (gameObject != null)
			{
				this.RefreshQueueCountDisplay(gameObject, this.targetFab);
			}
		}
		if (this.targetFab.CurrentWorkingOrder != null)
		{
			this.currentOrderLabel.text = string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.CURRENT_ORDER, this.targetFab.CurrentWorkingOrder.GetUIName(false));
		}
		else
		{
			this.currentOrderLabel.text = string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.CURRENT_ORDER, UI.UISIDESCREENS.FABRICATORSIDESCREEN.NO_WORKABLE_ORDER);
		}
		if (this.targetFab.NextOrder != null)
		{
			this.nextOrderLabel.text = string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.NEXT_ORDER, this.targetFab.NextOrder.GetUIName(false));
			return;
		}
		this.nextOrderLabel.text = string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.NEXT_ORDER, UI.UISIDESCREENS.FABRICATORSIDESCREEN.NO_WORKABLE_ORDER);
	}

	// Token: 0x06005DBE RID: 23998 RVA: 0x00223D74 File Offset: 0x00221F74
	protected override void OnShow(bool show)
	{
		if (show)
		{
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().FabricatorSideScreenOpenSnapshot);
		}
		else
		{
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().FabricatorSideScreenOpenSnapshot, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			DetailsScreen.Instance.ClearSecondarySideScreen();
			this.selectedRecipe = null;
			this.selectedToggle = null;
		}
		base.OnShow(show);
	}

	// Token: 0x06005DBF RID: 23999 RVA: 0x00223DD0 File Offset: 0x00221FD0
	public void Initialize(ComplexFabricator target)
	{
		if (target == null)
		{
			global::Debug.LogError("ComplexFabricator provided was null.");
			return;
		}
		this.targetFab = target;
		base.gameObject.SetActive(true);
		this.recipeMap = new Dictionary<GameObject, ComplexRecipe>();
		this.recipeToggles.ForEach(delegate(GameObject rbi)
		{
			UnityEngine.Object.Destroy(rbi.gameObject);
		});
		this.recipeToggles.Clear();
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.recipeCategories)
		{
			UnityEngine.Object.Destroy(keyValuePair.Value.transform.parent.gameObject);
		}
		this.recipeCategories.Clear();
		int num = 0;
		ComplexRecipe[] recipes = this.targetFab.GetRecipes();
		for (int i = 0; i < recipes.Length; i++)
		{
			ComplexRecipe recipe = recipes[i];
			bool flag = false;
			if (DebugHandler.InstantBuildMode)
			{
				flag = true;
			}
			else if (recipe.RequiresTechUnlock())
			{
				if (recipe.IsRequiredTechUnlocked())
				{
					flag = true;
				}
			}
			else if (target.GetRecipeQueueCount(recipe) != 0)
			{
				flag = true;
			}
			else if (this.AnyRecipeRequirementsDiscovered(recipe))
			{
				flag = true;
			}
			else if (this.HasAnyRecipeRequirements(recipe))
			{
				flag = true;
			}
			if (flag)
			{
				num++;
				global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(recipe.ingredients[0].material, "ui", false);
				global::Tuple<Sprite, Color> uisprite2 = Def.GetUISprite(recipe.results[0].material, recipe.results[0].facadeID);
				KToggle newToggle = null;
				ComplexFabricatorSideScreen.StyleSetting sideScreenStyle = target.sideScreenStyle;
				GameObject entryGO;
				if (sideScreenStyle - ComplexFabricatorSideScreen.StyleSetting.ListInputOutput > 1)
				{
					if (sideScreenStyle != ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid)
					{
						newToggle = global::Util.KInstantiateUI<KToggle>(this.recipeButton, this.recipeGrid, false);
						entryGO = newToggle.gameObject;
						Image componentInChildrenOnly = newToggle.gameObject.GetComponentInChildrenOnly<Image>();
						if (target.sideScreenStyle == ComplexFabricatorSideScreen.StyleSetting.GridInput || target.sideScreenStyle == ComplexFabricatorSideScreen.StyleSetting.ListInput)
						{
							componentInChildrenOnly.sprite = uisprite.first;
							componentInChildrenOnly.color = uisprite.second;
						}
						else
						{
							componentInChildrenOnly.sprite = uisprite2.first;
							componentInChildrenOnly.color = uisprite2.second;
						}
					}
					else
					{
						newToggle = global::Util.KInstantiateUI<KToggle>(this.recipeButtonQueueHybrid, this.recipeGrid, false);
						entryGO = newToggle.gameObject;
						this.recipeMap.Add(entryGO, recipe);
						if (recipe.recipeCategoryID != "")
						{
							if (!this.recipeCategories.ContainsKey(recipe.recipeCategoryID))
							{
								GameObject gameObject = global::Util.KInstantiateUI(this.recipeCategoryHeader, this.recipeGrid, true);
								gameObject.GetComponentInChildren<LocText>().SetText(Strings.Get("STRINGS.UI.UISIDESCREENS.FABRICATORSIDESCREEN.RECIPE_CATEGORIES." + recipe.recipeCategoryID.ToUpper()).String);
								HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
								RectTransform categoryContent = component.GetReference<RectTransform>("content");
								component.GetReference<Image>("icon").sprite = recipe.GetUIIcon();
								categoryContent.gameObject.SetActive(false);
								MultiToggle toggle = gameObject.GetComponentInChildren<MultiToggle>();
								MultiToggle toggle2 = toggle;
								toggle2.onClick = (System.Action)Delegate.Combine(toggle2.onClick, new System.Action(delegate
								{
									categoryContent.gameObject.SetActive(!categoryContent.gameObject.activeSelf);
									toggle.ChangeState(categoryContent.gameObject.activeSelf ? 1 : 0);
								}));
								this.recipeCategories.Add(recipe.recipeCategoryID, categoryContent.gameObject);
							}
							newToggle.transform.SetParent(this.recipeCategories[recipe.recipeCategoryID].rectTransform());
						}
						Image image = entryGO.GetComponentsInChildrenOnly<Image>()[2];
						if (recipe.nameDisplay == ComplexRecipe.RecipeNameDisplay.Ingredient)
						{
							image.sprite = uisprite.first;
							image.color = uisprite.second;
						}
						else if (recipe.nameDisplay == ComplexRecipe.RecipeNameDisplay.HEP)
						{
							image.sprite = this.radboltSprite;
						}
						else
						{
							image.sprite = uisprite2.first;
							image.color = uisprite2.second;
						}
						entryGO.GetComponentInChildren<LocText>().text = recipe.GetUIName(false);
						bool flag2 = this.HasAllRecipeRequirements(recipe);
						image.material = (flag2 ? Assets.UIPrefabs.TableScreenWidgets.DefaultUIMaterial : Assets.UIPrefabs.TableScreenWidgets.DesaturatedUIMaterial);
						this.RefreshQueueCountDisplay(entryGO, this.targetFab);
						entryGO.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("DecrementButton").onClick = delegate
						{
							target.DecrementRecipeQueueCount(recipe, false);
							this.RefreshQueueCountDisplay(entryGO, target);
						};
						entryGO.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("IncrementButton").onClick = delegate
						{
							target.IncrementRecipeQueueCount(recipe);
							this.RefreshQueueCountDisplay(entryGO, target);
						};
						entryGO.gameObject.SetActive(true);
					}
				}
				else
				{
					newToggle = global::Util.KInstantiateUI<KToggle>(this.recipeButtonMultiple, this.recipeGrid, false);
					entryGO = newToggle.gameObject;
					HierarchyReferences component2 = newToggle.GetComponent<HierarchyReferences>();
					foreach (ComplexRecipe.RecipeElement recipeElement in recipe.ingredients)
					{
						GameObject gameObject2 = global::Util.KInstantiateUI(component2.GetReference("FromIconPrefab").gameObject, component2.GetReference("FromIcons").gameObject, true);
						gameObject2.GetComponent<Image>().sprite = Def.GetUISprite(recipeElement.material, "ui", false).first;
						gameObject2.GetComponent<Image>().color = Def.GetUISprite(recipeElement.material, "ui", false).second;
						gameObject2.gameObject.name = recipeElement.material.Name;
					}
					foreach (ComplexRecipe.RecipeElement recipeElement2 in recipe.results)
					{
						GameObject gameObject3 = global::Util.KInstantiateUI(component2.GetReference("ToIconPrefab").gameObject, component2.GetReference("ToIcons").gameObject, true);
						gameObject3.GetComponent<Image>().sprite = Def.GetUISprite(recipeElement2.material, "ui", false).first;
						gameObject3.GetComponent<Image>().color = Def.GetUISprite(recipeElement2.material, "ui", false).second;
						gameObject3.gameObject.name = recipeElement2.material.Name;
					}
				}
				if (this.targetFab.sideScreenStyle == ComplexFabricatorSideScreen.StyleSetting.ClassicFabricator)
				{
					newToggle.GetComponentInChildren<LocText>().text = recipe.results[0].material.ProperName();
				}
				else if (this.targetFab.sideScreenStyle != ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid)
				{
					newToggle.GetComponentInChildren<LocText>().text = string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_FROM_TO_WITH_NEWLINES, recipe.ingredients[0].material.ProperName(), recipe.results[0].material.ProperName());
				}
				ToolTip component3 = entryGO.GetComponent<ToolTip>();
				component3.toolTipPosition = ToolTip.TooltipPosition.Custom;
				component3.parentPositionAnchor = new Vector2(0f, 0.5f);
				component3.tooltipPivot = new Vector2(1f, 1f);
				component3.tooltipPositionOffset = new Vector2(-24f, 20f);
				component3.ClearMultiStringTooltip();
				component3.AddMultiStringTooltip(recipe.GetUIName(false), this.styleTooltipHeader);
				component3.AddMultiStringTooltip(recipe.description, this.styleTooltipBody);
				newToggle.onClick += delegate
				{
					this.ToggleClicked(newToggle);
				};
				entryGO.SetActive(true);
				this.recipeToggles.Add(entryGO);
			}
		}
		if (this.recipeToggles.Count > 0)
		{
			this.buttonScrollContainer.GetComponent<LayoutElement>().minHeight = Mathf.Min(451f, 2f + (float)num * this.recipeButtonQueueHybrid.GetComponent<LayoutElement>().minHeight);
			this.subtitleLabel.SetText(UI.UISIDESCREENS.FABRICATORSIDESCREEN.SUBTITLE);
			this.noRecipesDiscoveredLabel.gameObject.SetActive(false);
		}
		else
		{
			this.subtitleLabel.SetText(UI.UISIDESCREENS.FABRICATORSIDESCREEN.NORECIPEDISCOVERED);
			this.noRecipesDiscoveredLabel.SetText(UI.UISIDESCREENS.FABRICATORSIDESCREEN.NORECIPEDISCOVERED_BODY);
			this.noRecipesDiscoveredLabel.gameObject.SetActive(true);
			this.buttonScrollContainer.GetComponent<LayoutElement>().minHeight = this.noRecipesDiscoveredLabel.rectTransform.sizeDelta.y + 10f;
		}
		this.RefreshIngredientAvailabilityVis();
	}

	// Token: 0x06005DC0 RID: 24000 RVA: 0x00224770 File Offset: 0x00222970
	public void RefreshQueueCountDisplayForRecipe(ComplexRecipe recipe, ComplexFabricator fabricator)
	{
		GameObject gameObject = this.recipeToggles.Find((GameObject match) => this.recipeMap[match] == recipe);
		if (gameObject != null)
		{
			this.RefreshQueueCountDisplay(gameObject, fabricator);
		}
	}

	// Token: 0x06005DC1 RID: 24001 RVA: 0x002247BC File Offset: 0x002229BC
	private void RefreshQueueCountDisplay(GameObject entryGO, ComplexFabricator fabricator)
	{
		HierarchyReferences component = entryGO.GetComponent<HierarchyReferences>();
		bool flag = fabricator.GetRecipeQueueCount(this.recipeMap[entryGO]) == ComplexFabricator.QUEUE_INFINITE;
		component.GetReference<LocText>("CountLabel").text = (flag ? "" : fabricator.GetRecipeQueueCount(this.recipeMap[entryGO]).ToString());
		component.GetReference<RectTransform>("InfiniteIcon").gameObject.SetActive(flag);
	}

	// Token: 0x06005DC2 RID: 24002 RVA: 0x00224834 File Offset: 0x00222A34
	private void ToggleClicked(KToggle toggle)
	{
		if (!this.recipeMap.ContainsKey(toggle.gameObject))
		{
			global::Debug.LogError("Recipe not found on recipe list.");
			return;
		}
		if (this.selectedToggle == toggle)
		{
			this.selectedToggle.isOn = false;
			this.selectedToggle = null;
			this.selectedRecipe = null;
		}
		else
		{
			this.selectedToggle = toggle;
			this.selectedToggle.isOn = true;
			this.selectedRecipe = this.recipeMap[toggle.gameObject];
			this.selectedRecipeFabricatorMap[this.targetFab] = this.recipeToggles.IndexOf(toggle.gameObject);
		}
		this.RefreshIngredientAvailabilityVis();
		if (toggle.isOn)
		{
			this.recipeScreen = (SelectedRecipeQueueScreen)DetailsScreen.Instance.SetSecondarySideScreen(this.recipeScreenPrefab, UI.UISIDESCREENS.FABRICATORSIDESCREEN.RECIPE_DETAILS);
			this.recipeScreen.SetRecipe(this, this.targetFab, this.selectedRecipe);
			return;
		}
		DetailsScreen.Instance.ClearSecondarySideScreen();
	}

	// Token: 0x06005DC3 RID: 24003 RVA: 0x0022492C File Offset: 0x00222B2C
	public void CycleRecipe(int increment)
	{
		int num = 0;
		if (this.selectedToggle != null)
		{
			num = this.recipeToggles.IndexOf(this.selectedToggle.gameObject);
		}
		int num2 = (num + increment) % this.recipeToggles.Count;
		if (num2 < 0)
		{
			num2 = this.recipeToggles.Count + num2;
		}
		this.ToggleClicked(this.recipeToggles[num2].GetComponent<KToggle>());
	}

	// Token: 0x06005DC4 RID: 24004 RVA: 0x0022499C File Offset: 0x00222B9C
	private bool HasAnyRecipeRequirements(ComplexRecipe recipe)
	{
		foreach (ComplexRecipe.RecipeElement recipeElement in recipe.ingredients)
		{
			if (this.targetFab.GetMyWorld().worldInventory.GetAmountWithoutTag(recipeElement.material, true, this.targetFab.ForbiddenTags) + this.targetFab.inStorage.GetAmountAvailable(recipeElement.material, this.targetFab.ForbiddenTags) + this.targetFab.buildStorage.GetAmountAvailable(recipeElement.material, this.targetFab.ForbiddenTags) >= recipeElement.amount)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06005DC5 RID: 24005 RVA: 0x00224A3C File Offset: 0x00222C3C
	private bool HasAllRecipeRequirements(ComplexRecipe recipe)
	{
		bool flag = true;
		foreach (ComplexRecipe.RecipeElement recipeElement in recipe.ingredients)
		{
			if (this.targetFab.GetMyWorld().worldInventory.GetAmountWithoutTag(recipeElement.material, true, this.targetFab.ForbiddenTags) + this.targetFab.inStorage.GetAmountAvailable(recipeElement.material, this.targetFab.ForbiddenTags) + this.targetFab.buildStorage.GetAmountAvailable(recipeElement.material, this.targetFab.ForbiddenTags) < recipeElement.amount)
			{
				flag = false;
				break;
			}
		}
		return flag;
	}

	// Token: 0x06005DC6 RID: 24006 RVA: 0x00224AE0 File Offset: 0x00222CE0
	private bool AnyRecipeRequirementsDiscovered(ComplexRecipe recipe)
	{
		foreach (ComplexRecipe.RecipeElement recipeElement in recipe.ingredients)
		{
			if (DiscoveredResources.Instance.IsDiscovered(recipeElement.material))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06005DC7 RID: 24007 RVA: 0x00224B1B File Offset: 0x00222D1B
	private void Update()
	{
		this.RefreshIngredientAvailabilityVis();
	}

	// Token: 0x06005DC8 RID: 24008 RVA: 0x00224B24 File Offset: 0x00222D24
	private void RefreshIngredientAvailabilityVis()
	{
		foreach (KeyValuePair<GameObject, ComplexRecipe> keyValuePair in this.recipeMap)
		{
			HierarchyReferences component = keyValuePair.Key.GetComponent<HierarchyReferences>();
			bool flag = this.HasAllRecipeRequirements(keyValuePair.Value);
			KToggle component2 = keyValuePair.Key.GetComponent<KToggle>();
			if (flag)
			{
				if (this.selectedRecipe == keyValuePair.Value)
				{
					component2.ActivateFlourish(true, ImageToggleState.State.Active);
				}
				else
				{
					component2.ActivateFlourish(false, ImageToggleState.State.Inactive);
				}
			}
			else if (this.selectedRecipe == keyValuePair.Value)
			{
				component2.ActivateFlourish(true, ImageToggleState.State.DisabledActive);
			}
			else
			{
				component2.ActivateFlourish(false, ImageToggleState.State.Disabled);
			}
			component.GetReference<LocText>("Label").color = (flag ? Color.black : new Color(0.22f, 0.22f, 0.22f, 1f));
		}
	}

	// Token: 0x06005DC9 RID: 24009 RVA: 0x00224C18 File Offset: 0x00222E18
	private Element[] GetRecipeElements(Recipe recipe)
	{
		Element[] array = new Element[recipe.Ingredients.Count];
		for (int i = 0; i < recipe.Ingredients.Count; i++)
		{
			Tag tag = recipe.Ingredients[i].tag;
			foreach (Element element in ElementLoader.elements)
			{
				if (GameTagExtensions.Create(element.id) == tag)
				{
					array[i] = element;
					break;
				}
			}
		}
		return array;
	}

	// Token: 0x0400400B RID: 16395
	[Header("Recipe List")]
	[SerializeField]
	private GameObject recipeGrid;

	// Token: 0x0400400C RID: 16396
	[Header("Recipe button variants")]
	[SerializeField]
	private GameObject recipeButton;

	// Token: 0x0400400D RID: 16397
	[SerializeField]
	private GameObject recipeButtonMultiple;

	// Token: 0x0400400E RID: 16398
	[SerializeField]
	private GameObject recipeButtonQueueHybrid;

	// Token: 0x0400400F RID: 16399
	[SerializeField]
	private GameObject recipeCategoryHeader;

	// Token: 0x04004010 RID: 16400
	[SerializeField]
	private Sprite buttonSelectedBG;

	// Token: 0x04004011 RID: 16401
	[SerializeField]
	private Sprite buttonNormalBG;

	// Token: 0x04004012 RID: 16402
	[SerializeField]
	private Sprite elementPlaceholderSpr;

	// Token: 0x04004013 RID: 16403
	[SerializeField]
	public Sprite radboltSprite;

	// Token: 0x04004014 RID: 16404
	private KToggle selectedToggle;

	// Token: 0x04004015 RID: 16405
	public LayoutElement buttonScrollContainer;

	// Token: 0x04004016 RID: 16406
	public RectTransform buttonContentContainer;

	// Token: 0x04004017 RID: 16407
	[SerializeField]
	private GameObject elementContainer;

	// Token: 0x04004018 RID: 16408
	[SerializeField]
	private LocText currentOrderLabel;

	// Token: 0x04004019 RID: 16409
	[SerializeField]
	private LocText nextOrderLabel;

	// Token: 0x0400401A RID: 16410
	private Dictionary<ComplexFabricator, int> selectedRecipeFabricatorMap = new Dictionary<ComplexFabricator, int>();

	// Token: 0x0400401B RID: 16411
	public EventReference createOrderSound;

	// Token: 0x0400401C RID: 16412
	[SerializeField]
	private RectTransform content;

	// Token: 0x0400401D RID: 16413
	[SerializeField]
	private LocText subtitleLabel;

	// Token: 0x0400401E RID: 16414
	[SerializeField]
	private LocText noRecipesDiscoveredLabel;

	// Token: 0x0400401F RID: 16415
	public TextStyleSetting styleTooltipHeader;

	// Token: 0x04004020 RID: 16416
	public TextStyleSetting styleTooltipBody;

	// Token: 0x04004021 RID: 16417
	private ComplexFabricator targetFab;

	// Token: 0x04004022 RID: 16418
	private ComplexRecipe selectedRecipe;

	// Token: 0x04004023 RID: 16419
	private Dictionary<GameObject, ComplexRecipe> recipeMap;

	// Token: 0x04004024 RID: 16420
	private Dictionary<string, GameObject> recipeCategories = new Dictionary<string, GameObject>();

	// Token: 0x04004025 RID: 16421
	private List<GameObject> recipeToggles = new List<GameObject>();

	// Token: 0x04004026 RID: 16422
	public SelectedRecipeQueueScreen recipeScreenPrefab;

	// Token: 0x04004027 RID: 16423
	private SelectedRecipeQueueScreen recipeScreen;

	// Token: 0x04004028 RID: 16424
	private int targetOrdersUpdatedSubHandle = -1;

	// Token: 0x02001A59 RID: 6745
	public enum StyleSetting
	{
		// Token: 0x0400774C RID: 30540
		GridResult,
		// Token: 0x0400774D RID: 30541
		ListResult,
		// Token: 0x0400774E RID: 30542
		GridInput,
		// Token: 0x0400774F RID: 30543
		ListInput,
		// Token: 0x04007750 RID: 30544
		ListInputOutput,
		// Token: 0x04007751 RID: 30545
		GridInputOutput,
		// Token: 0x04007752 RID: 30546
		ClassicFabricator,
		// Token: 0x04007753 RID: 30547
		ListQueueHybrid
	}
}
