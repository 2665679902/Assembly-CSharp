using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000B0D RID: 2829
public class MaterialSelectionPanel : KScreen
{
	// Token: 0x06005727 RID: 22311 RVA: 0x001FB04F File Offset: 0x001F924F
	public static void ClearStatics()
	{
		MaterialSelectionPanel.elementsWithTag.Clear();
	}

	// Token: 0x1700064C RID: 1612
	// (get) Token: 0x06005728 RID: 22312 RVA: 0x001FB05B File Offset: 0x001F925B
	public Tag CurrentSelectedElement
	{
		get
		{
			return this.MaterialSelectors[0].CurrentSelectedElement;
		}
	}

	// Token: 0x1700064D RID: 1613
	// (get) Token: 0x06005729 RID: 22313 RVA: 0x001FB070 File Offset: 0x001F9270
	public IList<Tag> GetSelectedElementAsList
	{
		get
		{
			this.currentSelectedElements.Clear();
			foreach (MaterialSelector materialSelector in this.MaterialSelectors)
			{
				if (materialSelector.gameObject.activeSelf)
				{
					global::Debug.Assert(materialSelector.CurrentSelectedElement != null);
					this.currentSelectedElements.Add(materialSelector.CurrentSelectedElement);
				}
			}
			return this.currentSelectedElements;
		}
	}

	// Token: 0x1700064E RID: 1614
	// (get) Token: 0x0600572A RID: 22314 RVA: 0x001FB104 File Offset: 0x001F9304
	public PriorityScreen PriorityScreen
	{
		get
		{
			return this.priorityScreen;
		}
	}

	// Token: 0x0600572B RID: 22315 RVA: 0x001FB10C File Offset: 0x001F930C
	protected override void OnPrefabInit()
	{
		MaterialSelectionPanel.elementsWithTag.Clear();
		base.OnPrefabInit();
		base.ConsumeMouseScroll = true;
		for (int i = 0; i < 3; i++)
		{
			MaterialSelector materialSelector = Util.KInstantiateUI<MaterialSelector>(this.MaterialSelectorTemplate, base.gameObject, false);
			materialSelector.selectorIndex = i;
			this.MaterialSelectors.Add(materialSelector);
		}
		this.MaterialSelectors[0].gameObject.SetActive(true);
		this.MaterialSelectorTemplate.SetActive(false);
		this.ResearchRequired.SetActive(false);
		this.priorityScreen = Util.KInstantiateUI<PriorityScreen>(this.priorityScreenPrefab.gameObject, this.priorityScreenParent, false);
		this.priorityScreen.InstantiateButtons(new Action<PrioritySetting>(this.OnPriorityClicked), true);
		this.priorityScreenParent.transform.SetAsLastSibling();
		this.gameSubscriptionHandles.Add(Game.Instance.Subscribe(-107300940, delegate(object d)
		{
			this.RefreshSelectors();
		}));
	}

	// Token: 0x0600572C RID: 22316 RVA: 0x001FB1FC File Offset: 0x001F93FC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.activateOnSpawn = true;
	}

	// Token: 0x0600572D RID: 22317 RVA: 0x001FB20C File Offset: 0x001F940C
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		foreach (int num in this.gameSubscriptionHandles)
		{
			Game.Instance.Unsubscribe(num);
		}
		this.gameSubscriptionHandles.Clear();
	}

	// Token: 0x0600572E RID: 22318 RVA: 0x001FB274 File Offset: 0x001F9474
	public void AddSelectAction(MaterialSelector.SelectMaterialActions action)
	{
		this.MaterialSelectors.ForEach(delegate(MaterialSelector selector)
		{
			selector.selectMaterialActions = (MaterialSelector.SelectMaterialActions)Delegate.Combine(selector.selectMaterialActions, action);
		});
	}

	// Token: 0x0600572F RID: 22319 RVA: 0x001FB2A5 File Offset: 0x001F94A5
	public void ClearSelectActions()
	{
		this.MaterialSelectors.ForEach(delegate(MaterialSelector selector)
		{
			selector.selectMaterialActions = null;
		});
	}

	// Token: 0x06005730 RID: 22320 RVA: 0x001FB2D1 File Offset: 0x001F94D1
	public void ClearMaterialToggles()
	{
		this.MaterialSelectors.ForEach(delegate(MaterialSelector selector)
		{
			selector.ClearMaterialToggles();
		});
	}

	// Token: 0x06005731 RID: 22321 RVA: 0x001FB2FD File Offset: 0x001F94FD
	public void ConfigureScreen(Recipe recipe, MaterialSelectionPanel.GetBuildableStateDelegate buildableStateCB, MaterialSelectionPanel.GetBuildableTooltipDelegate buildableTooltipCB)
	{
		this.activeRecipe = recipe;
		this.GetBuildableState = buildableStateCB;
		this.GetBuildableTooltip = buildableTooltipCB;
		this.RefreshSelectors();
	}

	// Token: 0x06005732 RID: 22322 RVA: 0x001FB31C File Offset: 0x001F951C
	public bool AllSelectorsSelected()
	{
		bool flag = false;
		foreach (MaterialSelector materialSelector in this.MaterialSelectors)
		{
			flag = flag || materialSelector.gameObject.activeInHierarchy;
			if (materialSelector.gameObject.activeInHierarchy && materialSelector.CurrentSelectedElement == null)
			{
				return false;
			}
		}
		return flag;
	}

	// Token: 0x06005733 RID: 22323 RVA: 0x001FB3A4 File Offset: 0x001F95A4
	public void RefreshSelectors()
	{
		if (this.activeRecipe == null)
		{
			return;
		}
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.MaterialSelectors.ForEach(delegate(MaterialSelector selector)
		{
			selector.gameObject.SetActive(false);
		});
		BuildingDef buildingDef = this.activeRecipe.GetBuildingDef();
		bool flag = this.GetBuildableState(buildingDef);
		string text = this.GetBuildableTooltip(buildingDef);
		if (!flag)
		{
			this.ResearchRequired.SetActive(true);
			LocText[] componentsInChildren = this.ResearchRequired.GetComponentsInChildren<LocText>();
			componentsInChildren[0].text = "";
			componentsInChildren[1].text = text;
			componentsInChildren[1].color = Constants.NEGATIVE_COLOR;
			this.priorityScreen.gameObject.SetActive(false);
			this.buildToolRotateButton.gameObject.SetActive(false);
			return;
		}
		this.ResearchRequired.SetActive(false);
		for (int i = 0; i < this.activeRecipe.Ingredients.Count; i++)
		{
			this.MaterialSelectors[i].gameObject.SetActive(true);
			this.MaterialSelectors[i].ConfigureScreen(this.activeRecipe.Ingredients[i], this.activeRecipe);
		}
		this.priorityScreen.gameObject.SetActive(true);
		this.priorityScreen.transform.SetAsLastSibling();
		this.buildToolRotateButton.gameObject.SetActive(true);
		this.buildToolRotateButton.transform.SetAsLastSibling();
	}

	// Token: 0x06005734 RID: 22324 RVA: 0x001FB51D File Offset: 0x001F971D
	public void UpdateResourceToggleValues()
	{
		this.MaterialSelectors.ForEach(delegate(MaterialSelector selector)
		{
			if (selector.gameObject.activeSelf)
			{
				selector.RefreshToggleContents();
			}
		});
	}

	// Token: 0x06005735 RID: 22325 RVA: 0x001FB54C File Offset: 0x001F974C
	public bool AutoSelectAvailableMaterial()
	{
		bool flag = true;
		for (int i = 0; i < this.MaterialSelectors.Count; i++)
		{
			if (!this.MaterialSelectors[i].AutoSelectAvailableMaterial())
			{
				flag = false;
			}
		}
		return flag;
	}

	// Token: 0x06005736 RID: 22326 RVA: 0x001FB588 File Offset: 0x001F9788
	public void SelectSourcesMaterials(Building building)
	{
		Tag[] array = null;
		Deconstructable component = building.gameObject.GetComponent<Deconstructable>();
		if (component != null)
		{
			array = component.constructionElements;
		}
		Constructable component2 = building.GetComponent<Constructable>();
		if (component2 != null)
		{
			array = component2.SelectedElementsTags.ToArray<Tag>();
		}
		if (array != null)
		{
			for (int i = 0; i < Mathf.Min(array.Length, this.MaterialSelectors.Count); i++)
			{
				if (this.MaterialSelectors[i].ElementToggles.ContainsKey(array[i]))
				{
					this.MaterialSelectors[i].OnSelectMaterial(array[i], this.activeRecipe, false);
				}
			}
		}
	}

	// Token: 0x06005737 RID: 22327 RVA: 0x001FB630 File Offset: 0x001F9830
	public static MaterialSelectionPanel.SelectedElemInfo Filter(Tag materialCategoryTag)
	{
		MaterialSelectionPanel.SelectedElemInfo selectedElemInfo = default(MaterialSelectionPanel.SelectedElemInfo);
		selectedElemInfo.element = null;
		selectedElemInfo.kgAvailable = 0f;
		if (DiscoveredResources.Instance == null || ElementLoader.elements == null || ElementLoader.elements.Count == 0)
		{
			return selectedElemInfo;
		}
		List<Tag> list = null;
		if (!MaterialSelectionPanel.elementsWithTag.TryGetValue(materialCategoryTag, out list))
		{
			list = new List<Tag>();
			foreach (Element element in ElementLoader.elements)
			{
				if (element.tag == materialCategoryTag || element.HasTag(materialCategoryTag))
				{
					list.Add(element.tag);
				}
			}
			foreach (Tag tag in GameTags.MaterialBuildingElements)
			{
				if (tag == materialCategoryTag)
				{
					foreach (GameObject gameObject in Assets.GetPrefabsWithTag(tag))
					{
						KPrefabID component = gameObject.GetComponent<KPrefabID>();
						if (component != null && !list.Contains(component.PrefabTag))
						{
							list.Add(component.PrefabTag);
						}
					}
				}
			}
			MaterialSelectionPanel.elementsWithTag[materialCategoryTag] = list;
		}
		foreach (Tag tag2 in list)
		{
			float amount = ClusterManager.Instance.activeWorld.worldInventory.GetAmount(tag2, true);
			if (amount > selectedElemInfo.kgAvailable)
			{
				selectedElemInfo.kgAvailable = amount;
				selectedElemInfo.element = tag2;
			}
		}
		return selectedElemInfo;
	}

	// Token: 0x06005738 RID: 22328 RVA: 0x001FB824 File Offset: 0x001F9A24
	public void ToggleShowDescriptorPanels(bool show)
	{
		for (int i = 0; i < this.MaterialSelectors.Count; i++)
		{
			if (this.MaterialSelectors[i] != null)
			{
				this.MaterialSelectors[i].ToggleShowDescriptorsPanel(show);
			}
		}
	}

	// Token: 0x06005739 RID: 22329 RVA: 0x001FB86D File Offset: 0x001F9A6D
	private void OnPriorityClicked(PrioritySetting priority)
	{
		this.priorityScreen.SetScreenPriority(priority, false);
	}

	// Token: 0x04003B25 RID: 15141
	public Dictionary<KToggle, Tag> ElementToggles = new Dictionary<KToggle, Tag>();

	// Token: 0x04003B26 RID: 15142
	private List<MaterialSelector> MaterialSelectors = new List<MaterialSelector>();

	// Token: 0x04003B27 RID: 15143
	private List<Tag> currentSelectedElements = new List<Tag>();

	// Token: 0x04003B28 RID: 15144
	[SerializeField]
	protected PriorityScreen priorityScreenPrefab;

	// Token: 0x04003B29 RID: 15145
	[SerializeField]
	protected GameObject priorityScreenParent;

	// Token: 0x04003B2A RID: 15146
	[SerializeField]
	protected BuildToolRotateButtonUI buildToolRotateButton;

	// Token: 0x04003B2B RID: 15147
	private PriorityScreen priorityScreen;

	// Token: 0x04003B2C RID: 15148
	public GameObject MaterialSelectorTemplate;

	// Token: 0x04003B2D RID: 15149
	public GameObject ResearchRequired;

	// Token: 0x04003B2E RID: 15150
	private Recipe activeRecipe;

	// Token: 0x04003B2F RID: 15151
	private static Dictionary<Tag, List<Tag>> elementsWithTag = new Dictionary<Tag, List<Tag>>();

	// Token: 0x04003B30 RID: 15152
	private MaterialSelectionPanel.GetBuildableStateDelegate GetBuildableState;

	// Token: 0x04003B31 RID: 15153
	private MaterialSelectionPanel.GetBuildableTooltipDelegate GetBuildableTooltip;

	// Token: 0x04003B32 RID: 15154
	private List<int> gameSubscriptionHandles = new List<int>();

	// Token: 0x020019AC RID: 6572
	// (Invoke) Token: 0x060090DC RID: 37084
	public delegate bool GetBuildableStateDelegate(BuildingDef def);

	// Token: 0x020019AD RID: 6573
	// (Invoke) Token: 0x060090E0 RID: 37088
	public delegate string GetBuildableTooltipDelegate(BuildingDef def);

	// Token: 0x020019AE RID: 6574
	// (Invoke) Token: 0x060090E4 RID: 37092
	public delegate void SelectElement(Element element, float kgAvailable, float recipe_amount);

	// Token: 0x020019AF RID: 6575
	public struct SelectedElemInfo
	{
		// Token: 0x040074F9 RID: 29945
		public Tag element;

		// Token: 0x040074FA RID: 29946
		public float kgAvailable;
	}
}
