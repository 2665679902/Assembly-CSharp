using System;
using System.Collections.Generic;
using Klei;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B0E RID: 2830
public class MaterialSelector : KScreen
{
	// Token: 0x0600573D RID: 22333 RVA: 0x001FB8C4 File Offset: 0x001F9AC4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.toggleGroup = base.GetComponent<ToggleGroup>();
	}

	// Token: 0x0600573E RID: 22334 RVA: 0x001FB8D8 File Offset: 0x001F9AD8
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.Consumed)
		{
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x0600573F RID: 22335 RVA: 0x001FB8EC File Offset: 0x001F9AEC
	public void ClearMaterialToggles()
	{
		this.CurrentSelectedElement = null;
		this.NoMaterialDiscovered.gameObject.SetActive(false);
		foreach (KeyValuePair<Tag, KToggle> keyValuePair in this.ElementToggles)
		{
			keyValuePair.Value.gameObject.SetActive(false);
			Util.KDestroyGameObject(keyValuePair.Value.gameObject);
		}
		this.ElementToggles.Clear();
	}

	// Token: 0x06005740 RID: 22336 RVA: 0x001FB984 File Offset: 0x001F9B84
	public static List<Tag> GetValidMaterials(Tag materialTypeTag)
	{
		List<Tag> list = new List<Tag>();
		foreach (Element element in ElementLoader.elements)
		{
			if (element.IsSolid && (element.tag == materialTypeTag || element.HasTag(materialTypeTag)))
			{
				list.Add(element.tag);
			}
		}
		foreach (Tag tag in GameTags.MaterialBuildingElements)
		{
			if (tag == materialTypeTag)
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
		return list;
	}

	// Token: 0x06005741 RID: 22337 RVA: 0x001FBAAC File Offset: 0x001F9CAC
	public void ConfigureScreen(Recipe.Ingredient ingredient, Recipe recipe)
	{
		this.ClearMaterialToggles();
		this.activeIngredient = ingredient;
		this.activeRecipe = recipe;
		this.activeMass = ingredient.amount;
		foreach (Tag tag in MaterialSelector.GetValidMaterials(ingredient.tag))
		{
			if (!this.ElementToggles.ContainsKey(tag))
			{
				GameObject gameObject = Util.KInstantiate(this.TogglePrefab, this.LayoutContainer, "MaterialSelection_" + tag.ProperName());
				gameObject.transform.localScale = Vector3.one;
				gameObject.SetActive(true);
				KToggle component = gameObject.GetComponent<KToggle>();
				this.ElementToggles.Add(tag, component);
				component.group = this.toggleGroup;
				gameObject.gameObject.GetComponent<ToolTip>().toolTip = tag.ProperName();
			}
		}
		this.ConfigureMaterialTooltips();
		this.RefreshToggleContents();
	}

	// Token: 0x06005742 RID: 22338 RVA: 0x001FBBAC File Offset: 0x001F9DAC
	private void SetToggleBGImage(KToggle toggle, Tag elem)
	{
		if (toggle == this.selectedToggle)
		{
			toggle.GetComponentsInChildren<Image>()[1].material = GlobalResources.Instance().AnimUIMaterial;
			toggle.GetComponent<ImageToggleState>().SetActive();
			return;
		}
		if (ClusterManager.Instance.activeWorld.worldInventory.GetAmount(elem, true) >= this.activeMass || DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive)
		{
			toggle.GetComponentsInChildren<Image>()[1].material = GlobalResources.Instance().AnimUIMaterial;
			toggle.GetComponentsInChildren<Image>()[1].color = Color.white;
			toggle.GetComponent<ImageToggleState>().SetInactive();
			return;
		}
		toggle.GetComponentsInChildren<Image>()[1].material = GlobalResources.Instance().AnimMaterialUIDesaturated;
		toggle.GetComponentsInChildren<Image>()[1].color = new Color(1f, 1f, 1f, 0.6f);
		if (!MaterialSelector.AllowInsufficientMaterialBuild())
		{
			toggle.GetComponent<ImageToggleState>().SetDisabled();
		}
	}

	// Token: 0x06005743 RID: 22339 RVA: 0x001FBCA0 File Offset: 0x001F9EA0
	public void OnSelectMaterial(Tag elem, Recipe recipe, bool focusScrollRect = false)
	{
		KToggle ktoggle = this.ElementToggles[elem];
		if (ktoggle != this.selectedToggle)
		{
			this.selectedToggle = ktoggle;
			if (recipe != null)
			{
				SaveGame.Instance.materialSelectorSerializer.SetSelectedElement(ClusterManager.Instance.activeWorldId, this.selectorIndex, recipe.Result, elem);
			}
			this.CurrentSelectedElement = elem;
			if (this.selectMaterialActions != null)
			{
				this.selectMaterialActions();
			}
			this.UpdateHeader();
			this.SetDescription(elem);
			this.SetEffects(elem);
			if (!this.MaterialDescriptionPane.gameObject.activeSelf && !this.MaterialEffectsPane.gameObject.activeSelf)
			{
				this.DescriptorsPanel.SetActive(false);
			}
			else
			{
				this.DescriptorsPanel.SetActive(true);
			}
		}
		if (focusScrollRect && this.ElementToggles.Count > 1)
		{
			List<Tag> list = new List<Tag>();
			foreach (KeyValuePair<Tag, KToggle> keyValuePair in this.ElementToggles)
			{
				list.Add(keyValuePair.Key);
			}
			list.Sort(new Comparison<Tag>(this.ElementSorter));
			float num = (float)list.IndexOf(elem);
			int constraintCount = this.LayoutContainer.GetComponent<GridLayoutGroup>().constraintCount;
			float num2 = num / (float)constraintCount / (float)Math.Max((list.Count - 1) / constraintCount, 1);
			this.ScrollRect.normalizedPosition = new Vector2(0f, 1f - num2);
		}
		this.RefreshToggleContents();
	}

	// Token: 0x06005744 RID: 22340 RVA: 0x001FBE38 File Offset: 0x001FA038
	public void RefreshToggleContents()
	{
		foreach (KeyValuePair<Tag, KToggle> keyValuePair in this.ElementToggles)
		{
			KToggle value = keyValuePair.Value;
			Tag elem = keyValuePair.Key;
			GameObject gameObject = value.gameObject;
			bool flag = DiscoveredResources.Instance.IsDiscovered(elem) || DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive;
			if (gameObject.activeSelf != flag)
			{
				gameObject.SetActive(flag);
			}
			if (flag)
			{
				LocText[] componentsInChildren = gameObject.GetComponentsInChildren<LocText>();
				LocText locText = componentsInChildren[0];
				TMP_Text tmp_Text = componentsInChildren[1];
				Image image = gameObject.GetComponentsInChildren<Image>()[1];
				tmp_Text.text = Util.FormatWholeNumber(ClusterManager.Instance.activeWorld.worldInventory.GetAmount(elem, true));
				locText.text = Util.FormatWholeNumber(this.activeMass);
				GameObject gameObject2 = Assets.TryGetPrefab(keyValuePair.Key);
				if (gameObject2 != null)
				{
					KBatchedAnimController component = gameObject2.GetComponent<KBatchedAnimController>();
					image.sprite = Def.GetUISpriteFromMultiObjectAnim(component.AnimFiles[0], "ui", false, "");
				}
				this.SetToggleBGImage(keyValuePair.Value, keyValuePair.Key);
				value.soundPlayer.AcceptClickCondition = () => this.IsEnoughMass(elem);
				value.ClearOnClick();
				if (this.IsEnoughMass(elem))
				{
					value.onClick += delegate
					{
						this.OnSelectMaterial(elem, this.activeRecipe, false);
					};
				}
			}
		}
		this.SortElementToggles();
		this.UpdateHeader();
	}

	// Token: 0x06005745 RID: 22341 RVA: 0x001FBFF4 File Offset: 0x001FA1F4
	private bool IsEnoughMass(Tag t)
	{
		return ClusterManager.Instance.activeWorld.worldInventory.GetAmount(t, true) >= this.activeMass || DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive || MaterialSelector.AllowInsufficientMaterialBuild();
	}

	// Token: 0x06005746 RID: 22342 RVA: 0x001FC030 File Offset: 0x001FA230
	public bool AutoSelectAvailableMaterial()
	{
		if (this.activeRecipe == null || this.ElementToggles.Count == 0)
		{
			return false;
		}
		Tag previousElement = SaveGame.Instance.materialSelectorSerializer.GetPreviousElement(ClusterManager.Instance.activeWorldId, this.selectorIndex, this.activeRecipe.Result);
		if (previousElement != null)
		{
			KToggle ktoggle;
			this.ElementToggles.TryGetValue(previousElement, out ktoggle);
			if (ktoggle != null && (DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive || ClusterManager.Instance.activeWorld.worldInventory.GetAmount(previousElement, true) >= this.activeMass))
			{
				this.OnSelectMaterial(previousElement, this.activeRecipe, true);
				return true;
			}
		}
		float num = -1f;
		List<Tag> list = new List<Tag>();
		foreach (KeyValuePair<Tag, KToggle> keyValuePair in this.ElementToggles)
		{
			list.Add(keyValuePair.Key);
		}
		list.Sort(new Comparison<Tag>(this.ElementSorter));
		if (DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive)
		{
			this.OnSelectMaterial(list[0], this.activeRecipe, true);
			return true;
		}
		Tag tag = null;
		foreach (Tag tag2 in list)
		{
			float amount = ClusterManager.Instance.activeWorld.worldInventory.GetAmount(tag2, true);
			if (amount >= this.activeMass && amount > num)
			{
				num = amount;
				tag = tag2;
			}
		}
		if (tag != null)
		{
			this.OnSelectMaterial(tag, this.activeRecipe, true);
			return true;
		}
		return false;
	}

	// Token: 0x06005747 RID: 22343 RVA: 0x001FC208 File Offset: 0x001FA408
	private void SortElementToggles()
	{
		bool flag = false;
		int num = -1;
		this.elementsToSort.Clear();
		foreach (KeyValuePair<Tag, KToggle> keyValuePair in this.ElementToggles)
		{
			if (keyValuePair.Value.gameObject.activeSelf)
			{
				this.elementsToSort.Add(keyValuePair.Key);
			}
		}
		this.elementsToSort.Sort(new Comparison<Tag>(this.ElementSorter));
		for (int i = 0; i < this.elementsToSort.Count; i++)
		{
			int siblingIndex = this.ElementToggles[this.elementsToSort[i]].transform.GetSiblingIndex();
			if (siblingIndex <= num)
			{
				flag = true;
				break;
			}
			num = siblingIndex;
		}
		if (flag)
		{
			foreach (Tag tag in this.elementsToSort)
			{
				this.ElementToggles[tag].transform.SetAsLastSibling();
			}
		}
		this.UpdateScrollBar();
	}

	// Token: 0x06005748 RID: 22344 RVA: 0x001FC348 File Offset: 0x001FA548
	private void ConfigureMaterialTooltips()
	{
		foreach (KeyValuePair<Tag, KToggle> keyValuePair in this.ElementToggles)
		{
			ToolTip component = keyValuePair.Value.gameObject.GetComponent<ToolTip>();
			if (component != null)
			{
				component.toolTip = GameUtil.GetMaterialTooltips(keyValuePair.Key);
			}
		}
	}

	// Token: 0x06005749 RID: 22345 RVA: 0x001FC3C4 File Offset: 0x001FA5C4
	private void UpdateScrollBar()
	{
		int num = 0;
		foreach (KeyValuePair<Tag, KToggle> keyValuePair in this.ElementToggles)
		{
			if (keyValuePair.Value.gameObject.activeSelf)
			{
				num++;
			}
		}
		if (this.Scrollbar.activeSelf != num > 5)
		{
			this.Scrollbar.SetActive(num > 5);
		}
		this.ScrollRect.GetComponent<LayoutElement>().minHeight = (float)(74 * ((num <= 5) ? 1 : 2));
	}

	// Token: 0x0600574A RID: 22346 RVA: 0x001FC468 File Offset: 0x001FA668
	private void UpdateHeader()
	{
		if (this.activeIngredient == null)
		{
			return;
		}
		int num = 0;
		foreach (KeyValuePair<Tag, KToggle> keyValuePair in this.ElementToggles)
		{
			if (keyValuePair.Value.gameObject.activeSelf)
			{
				num++;
			}
		}
		LocText componentInChildren = this.Headerbar.GetComponentInChildren<LocText>();
		if (num == 0)
		{
			componentInChildren.text = string.Format(UI.PRODUCTINFO_MISSINGRESOURCES_TITLE, this.activeIngredient.tag.ProperName(), GameUtil.GetFormattedMass(this.activeIngredient.amount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
			string text = string.Format(UI.PRODUCTINFO_MISSINGRESOURCES_DESC, this.activeIngredient.tag.ProperName());
			this.NoMaterialDiscovered.text = text;
			this.NoMaterialDiscovered.gameObject.SetActive(true);
			this.NoMaterialDiscovered.color = Constants.NEGATIVE_COLOR;
			this.BadBG.SetActive(true);
			this.Scrollbar.SetActive(false);
			this.LayoutContainer.SetActive(false);
			return;
		}
		componentInChildren.text = string.Format(UI.PRODUCTINFO_SELECTMATERIAL, this.activeIngredient.tag.ProperName());
		this.NoMaterialDiscovered.gameObject.SetActive(false);
		this.BadBG.SetActive(false);
		this.LayoutContainer.SetActive(true);
		this.UpdateScrollBar();
	}

	// Token: 0x0600574B RID: 22347 RVA: 0x001FC5F0 File Offset: 0x001FA7F0
	public void ToggleShowDescriptorsPanel(bool show)
	{
		this.DescriptorsPanel.gameObject.SetActive(show);
	}

	// Token: 0x0600574C RID: 22348 RVA: 0x001FC604 File Offset: 0x001FA804
	private void SetDescription(Tag element)
	{
		StringEntry stringEntry = null;
		if (Strings.TryGet(new StringKey("STRINGS.ELEMENTS." + element.ToString().ToUpper() + ".BUILD_DESC"), out stringEntry))
		{
			this.MaterialDescriptionText.text = stringEntry.ToString();
			this.MaterialDescriptionPane.SetActive(true);
			return;
		}
		this.MaterialDescriptionPane.SetActive(false);
	}

	// Token: 0x0600574D RID: 22349 RVA: 0x001FC66C File Offset: 0x001FA86C
	private void SetEffects(Tag element)
	{
		List<Descriptor> materialDescriptors = GameUtil.GetMaterialDescriptors(element);
		if (materialDescriptors.Count > 0)
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(ELEMENTS.MATERIAL_MODIFIERS.EFFECTS_HEADER, ELEMENTS.MATERIAL_MODIFIERS.TOOLTIP.EFFECTS_HEADER, Descriptor.DescriptorType.Effect);
			materialDescriptors.Insert(0, descriptor);
			this.MaterialEffectsPane.gameObject.SetActive(true);
			this.MaterialEffectsPane.SetDescriptors(materialDescriptors);
			return;
		}
		this.MaterialEffectsPane.gameObject.SetActive(false);
	}

	// Token: 0x0600574E RID: 22350 RVA: 0x001FC6E4 File Offset: 0x001FA8E4
	public static bool AllowInsufficientMaterialBuild()
	{
		return GenericGameSettings.instance.allowInsufficientMaterialBuild;
	}

	// Token: 0x0600574F RID: 22351 RVA: 0x001FC6F0 File Offset: 0x001FA8F0
	private int ElementSorter(Tag at, Tag bt)
	{
		GameObject gameObject = Assets.TryGetPrefab(at);
		IHasSortOrder hasSortOrder = ((gameObject != null) ? gameObject.GetComponent<IHasSortOrder>() : null);
		GameObject gameObject2 = Assets.TryGetPrefab(bt);
		IHasSortOrder hasSortOrder2 = ((gameObject2 != null) ? gameObject2.GetComponent<IHasSortOrder>() : null);
		if (hasSortOrder == null || hasSortOrder2 == null)
		{
			return 0;
		}
		Element element = ElementLoader.GetElement(at);
		Element element2 = ElementLoader.GetElement(bt);
		if (element != null && element2 != null && element.buildMenuSort == element2.buildMenuSort)
		{
			return element.idx.CompareTo(element2.idx);
		}
		return hasSortOrder.sortOrder.CompareTo(hasSortOrder2.sortOrder);
	}

	// Token: 0x04003B33 RID: 15155
	public Tag CurrentSelectedElement;

	// Token: 0x04003B34 RID: 15156
	public Dictionary<Tag, KToggle> ElementToggles = new Dictionary<Tag, KToggle>();

	// Token: 0x04003B35 RID: 15157
	public int selectorIndex;

	// Token: 0x04003B36 RID: 15158
	public MaterialSelector.SelectMaterialActions selectMaterialActions;

	// Token: 0x04003B37 RID: 15159
	public MaterialSelector.SelectMaterialActions deselectMaterialActions;

	// Token: 0x04003B38 RID: 15160
	private ToggleGroup toggleGroup;

	// Token: 0x04003B39 RID: 15161
	public GameObject TogglePrefab;

	// Token: 0x04003B3A RID: 15162
	public GameObject LayoutContainer;

	// Token: 0x04003B3B RID: 15163
	public KScrollRect ScrollRect;

	// Token: 0x04003B3C RID: 15164
	public GameObject Scrollbar;

	// Token: 0x04003B3D RID: 15165
	public GameObject Headerbar;

	// Token: 0x04003B3E RID: 15166
	public GameObject BadBG;

	// Token: 0x04003B3F RID: 15167
	public LocText NoMaterialDiscovered;

	// Token: 0x04003B40 RID: 15168
	public GameObject MaterialDescriptionPane;

	// Token: 0x04003B41 RID: 15169
	public LocText MaterialDescriptionText;

	// Token: 0x04003B42 RID: 15170
	public DescriptorPanel MaterialEffectsPane;

	// Token: 0x04003B43 RID: 15171
	public GameObject DescriptorsPanel;

	// Token: 0x04003B44 RID: 15172
	private KToggle selectedToggle;

	// Token: 0x04003B45 RID: 15173
	private Recipe.Ingredient activeIngredient;

	// Token: 0x04003B46 RID: 15174
	private Recipe activeRecipe;

	// Token: 0x04003B47 RID: 15175
	private float activeMass;

	// Token: 0x04003B48 RID: 15176
	private List<Tag> elementsToSort = new List<Tag>();

	// Token: 0x020019B2 RID: 6578
	// (Invoke) Token: 0x060090F0 RID: 37104
	public delegate void SelectMaterialActions();
}
