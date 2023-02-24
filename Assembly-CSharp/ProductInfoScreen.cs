using System;
using System.Collections.Generic;
using Database;
using Klei.AI;
using STRINGS;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000B61 RID: 2913
public class ProductInfoScreen : KScreen
{
	// Token: 0x1700066E RID: 1646
	// (get) Token: 0x06005ACE RID: 23246 RVA: 0x0020F460 File Offset: 0x0020D660
	public FacadeSelectionPanel FacadeSelectionPanel
	{
		get
		{
			return this.facadeSelectionPanel;
		}
	}

	// Token: 0x06005ACF RID: 23247 RVA: 0x0020F468 File Offset: 0x0020D668
	private void RefreshScreen()
	{
		if (this.currentDef != null)
		{
			this.SetTitle(this.currentDef);
			return;
		}
		this.ClearProduct(true);
	}

	// Token: 0x06005AD0 RID: 23248 RVA: 0x0020F48C File Offset: 0x0020D68C
	public void ClearProduct(bool deactivateTool = true)
	{
		if (this.materialSelectionPanel == null)
		{
			return;
		}
		this.currentDef = null;
		this.materialSelectionPanel.ClearMaterialToggles();
		if (PlayerController.Instance.ActiveTool == BuildTool.Instance && deactivateTool)
		{
			BuildTool.Instance.Deactivate();
		}
		if (PlayerController.Instance.ActiveTool == UtilityBuildTool.Instance || PlayerController.Instance.ActiveTool == WireBuildTool.Instance)
		{
			ToolMenu.Instance.ClearSelection();
		}
		this.ClearLabels();
		this.Show(false);
	}

	// Token: 0x06005AD1 RID: 23249 RVA: 0x0020F520 File Offset: 0x0020D720
	public new void Awake()
	{
		base.Awake();
		this.facadeSelectionPanel = Util.KInstantiateUI<FacadeSelectionPanel>(this.facadeSelectionPanelPrefab.gameObject, base.gameObject, false);
		FacadeSelectionPanel facadeSelectionPanel = this.facadeSelectionPanel;
		facadeSelectionPanel.OnFacadeSelectionChanged = (System.Action)Delegate.Combine(facadeSelectionPanel.OnFacadeSelectionChanged, new System.Action(this.OnFacadeSelectionChanged));
		this.materialSelectionPanel = Util.KInstantiateUI<MaterialSelectionPanel>(this.materialSelectionPanelPrefab.gameObject, base.gameObject, false);
	}

	// Token: 0x06005AD2 RID: 23250 RVA: 0x0020F594 File Offset: 0x0020D794
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (BuildingGroupScreen.Instance != null)
		{
			BuildingGroupScreen instance = BuildingGroupScreen.Instance;
			instance.pointerEnterActions = (KScreen.PointerEnterActions)Delegate.Combine(instance.pointerEnterActions, new KScreen.PointerEnterActions(this.CheckMouseOver));
			BuildingGroupScreen instance2 = BuildingGroupScreen.Instance;
			instance2.pointerExitActions = (KScreen.PointerExitActions)Delegate.Combine(instance2.pointerExitActions, new KScreen.PointerExitActions(this.CheckMouseOver));
		}
		if (PlanScreen.Instance != null)
		{
			PlanScreen instance3 = PlanScreen.Instance;
			instance3.pointerEnterActions = (KScreen.PointerEnterActions)Delegate.Combine(instance3.pointerEnterActions, new KScreen.PointerEnterActions(this.CheckMouseOver));
			PlanScreen instance4 = PlanScreen.Instance;
			instance4.pointerExitActions = (KScreen.PointerExitActions)Delegate.Combine(instance4.pointerExitActions, new KScreen.PointerExitActions(this.CheckMouseOver));
		}
		if (BuildMenu.Instance != null)
		{
			BuildMenu instance5 = BuildMenu.Instance;
			instance5.pointerEnterActions = (KScreen.PointerEnterActions)Delegate.Combine(instance5.pointerEnterActions, new KScreen.PointerEnterActions(this.CheckMouseOver));
			BuildMenu instance6 = BuildMenu.Instance;
			instance6.pointerExitActions = (KScreen.PointerExitActions)Delegate.Combine(instance6.pointerExitActions, new KScreen.PointerExitActions(this.CheckMouseOver));
		}
		this.pointerEnterActions = (KScreen.PointerEnterActions)Delegate.Combine(this.pointerEnterActions, new KScreen.PointerEnterActions(this.CheckMouseOver));
		this.pointerExitActions = (KScreen.PointerExitActions)Delegate.Combine(this.pointerExitActions, new KScreen.PointerExitActions(this.CheckMouseOver));
		base.ConsumeMouseScroll = true;
		this.sandboxInstantBuildToggle.ChangeState(SandboxToolParameterMenu.instance.settings.InstantBuild ? 1 : 0);
		MultiToggle multiToggle = this.sandboxInstantBuildToggle;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
		{
			SandboxToolParameterMenu.instance.settings.InstantBuild = !SandboxToolParameterMenu.instance.settings.InstantBuild;
			this.sandboxInstantBuildToggle.ChangeState(SandboxToolParameterMenu.instance.settings.InstantBuild ? 1 : 0);
		}));
		this.sandboxInstantBuildToggle.gameObject.SetActive(Game.Instance.SandboxModeActive);
		Game.Instance.Subscribe(-1948169901, delegate(object data)
		{
			this.sandboxInstantBuildToggle.gameObject.SetActive(Game.Instance.SandboxModeActive);
		});
	}

	// Token: 0x06005AD3 RID: 23251 RVA: 0x0020F77A File Offset: 0x0020D97A
	public void ConfigureScreen(BuildingDef def)
	{
		this.ConfigureScreen(def, this.FacadeSelectionPanel.SelectedFacade);
	}

	// Token: 0x06005AD4 RID: 23252 RVA: 0x0020F790 File Offset: 0x0020D990
	public void ConfigureScreen(BuildingDef def, string facadeID)
	{
		this.configuring = true;
		this.currentDef = def;
		this.SetTitle(def);
		this.SetDescription(def);
		this.SetEffects(def);
		this.facadeSelectionPanel.SetBuildingDef(def.PrefabID);
		BuildingFacadeResource buildingFacadeResource = null;
		if ("DEFAULT_FACADE" != facadeID)
		{
			buildingFacadeResource = Db.GetBuildingFacades().TryGet(facadeID);
		}
		if (buildingFacadeResource != null && buildingFacadeResource.PrefabID == def.PrefabID && Db.Get().Permits.Get(buildingFacadeResource.PermitId).IsUnlocked())
		{
			this.facadeSelectionPanel.SelectedFacade = facadeID;
		}
		else
		{
			this.facadeSelectionPanel.SelectedFacade = "DEFAULT_FACADE";
		}
		this.SetMaterials(def);
		this.configuring = false;
	}

	// Token: 0x06005AD5 RID: 23253 RVA: 0x0020F84A File Offset: 0x0020DA4A
	private void ExpandInfo(PointerEventData data)
	{
		this.ToggleExpandedInfo(true);
	}

	// Token: 0x06005AD6 RID: 23254 RVA: 0x0020F853 File Offset: 0x0020DA53
	private void CollapseInfo(PointerEventData data)
	{
		this.ToggleExpandedInfo(false);
	}

	// Token: 0x06005AD7 RID: 23255 RVA: 0x0020F85C File Offset: 0x0020DA5C
	public void ToggleExpandedInfo(bool state)
	{
		this.expandedInfo = state;
		if (this.ProductDescriptionPane != null)
		{
			this.ProductDescriptionPane.SetActive(this.expandedInfo);
		}
		if (this.ProductRequirementsPane != null)
		{
			this.ProductRequirementsPane.gameObject.SetActive(this.expandedInfo && this.ProductRequirementsPane.HasDescriptors());
		}
		if (this.ProductEffectsPane != null)
		{
			this.ProductEffectsPane.gameObject.SetActive(this.expandedInfo && this.ProductEffectsPane.HasDescriptors());
		}
		if (this.ProductFlavourPane != null)
		{
			this.ProductFlavourPane.SetActive(this.expandedInfo);
		}
		if (this.materialSelectionPanel != null && this.materialSelectionPanel.CurrentSelectedElement != null)
		{
			this.materialSelectionPanel.ToggleShowDescriptorPanels(this.expandedInfo);
		}
	}

	// Token: 0x06005AD8 RID: 23256 RVA: 0x0020F950 File Offset: 0x0020DB50
	private void CheckMouseOver(PointerEventData data)
	{
		bool flag = base.GetMouseOver || (PlanScreen.Instance != null && ((PlanScreen.Instance.isActiveAndEnabled && PlanScreen.Instance.GetMouseOver) || BuildingGroupScreen.Instance.GetMouseOver)) || (BuildMenu.Instance != null && BuildMenu.Instance.isActiveAndEnabled && BuildMenu.Instance.GetMouseOver);
		this.ToggleExpandedInfo(flag);
	}

	// Token: 0x06005AD9 RID: 23257 RVA: 0x0020F9C8 File Offset: 0x0020DBC8
	private void Update()
	{
		if (!DebugHandler.InstantBuildMode && !Game.Instance.SandboxModeActive && this.currentDef != null && this.materialSelectionPanel.CurrentSelectedElement != null && !MaterialSelector.AllowInsufficientMaterialBuild() && this.currentDef.Mass[0] > ClusterManager.Instance.activeWorld.worldInventory.GetAmount(this.materialSelectionPanel.CurrentSelectedElement, true))
		{
			this.materialSelectionPanel.AutoSelectAvailableMaterial();
		}
	}

	// Token: 0x06005ADA RID: 23258 RVA: 0x0020FA50 File Offset: 0x0020DC50
	private void SetTitle(BuildingDef def)
	{
		this.titleBar.SetTitle(def.Name);
		bool flag = (PlanScreen.Instance != null && PlanScreen.Instance.isActiveAndEnabled && PlanScreen.Instance.IsDefBuildable(def)) || (BuildMenu.Instance != null && BuildMenu.Instance.isActiveAndEnabled && BuildMenu.Instance.BuildableState(def) == PlanScreen.RequirementsState.Complete);
		this.titleBar.GetComponentInChildren<KImage>().ColorState = (flag ? KImage.ColorSelector.Active : KImage.ColorSelector.Disabled);
	}

	// Token: 0x06005ADB RID: 23259 RVA: 0x0020FADC File Offset: 0x0020DCDC
	private void SetDescription(BuildingDef def)
	{
		if (def == null)
		{
			return;
		}
		if (this.productFlavourText == null)
		{
			return;
		}
		string text = "";
		KPrefabID component = def.BuildingComplete.GetComponent<KPrefabID>();
		string text2 = "";
		foreach (Tag tag in component.Tags)
		{
			string text3;
			if (CodexEntryGenerator.room_constraint_to_building_label_dict.TryGetValue(tag, out text3))
			{
				text2 = text2 + "\n    • " + text3;
			}
		}
		if (!string.IsNullOrWhiteSpace(text2))
		{
			text += string.Format("<b>{0}</b>: {1}\n\n", CODEX.HEADERS.BUILDINGTYPE, text2);
		}
		text += def.Desc;
		Dictionary<Klei.AI.Attribute, float> dictionary = new Dictionary<Klei.AI.Attribute, float>();
		Dictionary<Klei.AI.Attribute, float> dictionary2 = new Dictionary<Klei.AI.Attribute, float>();
		foreach (Klei.AI.Attribute attribute in def.attributes)
		{
			if (!dictionary.ContainsKey(attribute))
			{
				dictionary[attribute] = 0f;
			}
		}
		foreach (AttributeModifier attributeModifier in def.attributeModifiers)
		{
			float num = 0f;
			Klei.AI.Attribute attribute2 = Db.Get().BuildingAttributes.Get(attributeModifier.AttributeId);
			dictionary.TryGetValue(attribute2, out num);
			num += attributeModifier.Value;
			dictionary[attribute2] = num;
		}
		if (this.materialSelectionPanel.CurrentSelectedElement != null)
		{
			Element element = ElementLoader.GetElement(this.materialSelectionPanel.CurrentSelectedElement);
			if (element != null)
			{
				using (List<AttributeModifier>.Enumerator enumerator3 = element.attributeModifiers.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						AttributeModifier attributeModifier2 = enumerator3.Current;
						float num2 = 0f;
						Klei.AI.Attribute attribute3 = Db.Get().BuildingAttributes.Get(attributeModifier2.AttributeId);
						dictionary2.TryGetValue(attribute3, out num2);
						num2 += attributeModifier2.Value;
						dictionary2[attribute3] = num2;
					}
					goto IL_2A8;
				}
			}
			PrefabAttributeModifiers component2 = Assets.TryGetPrefab(this.materialSelectionPanel.CurrentSelectedElement).GetComponent<PrefabAttributeModifiers>();
			if (component2 != null)
			{
				foreach (AttributeModifier attributeModifier3 in component2.descriptors)
				{
					float num3 = 0f;
					Klei.AI.Attribute attribute4 = Db.Get().BuildingAttributes.Get(attributeModifier3.AttributeId);
					dictionary2.TryGetValue(attribute4, out num3);
					num3 += attributeModifier3.Value;
					dictionary2[attribute4] = num3;
				}
			}
		}
		IL_2A8:
		if (dictionary.Count > 0)
		{
			text += "\n\n";
			foreach (KeyValuePair<Klei.AI.Attribute, float> keyValuePair in dictionary)
			{
				float num4 = 0f;
				dictionary.TryGetValue(keyValuePair.Key, out num4);
				float num5 = 0f;
				string text4 = "";
				if (dictionary2.TryGetValue(keyValuePair.Key, out num5))
				{
					num5 = Mathf.Abs(num4 * num5);
					text4 = "(+" + num5.ToString() + ")";
				}
				text = string.Concat(new string[]
				{
					text,
					"\n",
					keyValuePair.Key.Name,
					": ",
					(num4 + num5).ToString(),
					text4
				});
			}
		}
		this.productFlavourText.text = text;
	}

	// Token: 0x06005ADC RID: 23260 RVA: 0x0020FED4 File Offset: 0x0020E0D4
	private void SetEffects(BuildingDef def)
	{
		if (this.productDescriptionText.text != null)
		{
			this.productDescriptionText.text = string.Format("{0}", def.Effect);
		}
		List<Descriptor> allDescriptors = GameUtil.GetAllDescriptors(def.BuildingComplete, false);
		List<Descriptor> requirementDescriptors = GameUtil.GetRequirementDescriptors(allDescriptors);
		if (requirementDescriptors.Count > 0)
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(UI.BUILDINGEFFECTS.OPERATIONREQUIREMENTS, UI.BUILDINGEFFECTS.TOOLTIPS.OPERATIONREQUIREMENTS, Descriptor.DescriptorType.Effect);
			requirementDescriptors.Insert(0, descriptor);
			this.ProductRequirementsPane.gameObject.SetActive(true);
		}
		else
		{
			this.ProductRequirementsPane.gameObject.SetActive(false);
		}
		this.ProductRequirementsPane.SetDescriptors(requirementDescriptors);
		List<Descriptor> effectDescriptors = GameUtil.GetEffectDescriptors(allDescriptors);
		if (effectDescriptors.Count > 0)
		{
			Descriptor descriptor2 = default(Descriptor);
			descriptor2.SetupDescriptor(UI.BUILDINGEFFECTS.OPERATIONEFFECTS, UI.BUILDINGEFFECTS.TOOLTIPS.OPERATIONEFFECTS, Descriptor.DescriptorType.Effect);
			effectDescriptors.Insert(0, descriptor2);
			this.ProductEffectsPane.gameObject.SetActive(true);
		}
		else
		{
			this.ProductEffectsPane.gameObject.SetActive(false);
		}
		this.ProductEffectsPane.SetDescriptors(effectDescriptors);
	}

	// Token: 0x06005ADD RID: 23261 RVA: 0x0020FFEC File Offset: 0x0020E1EC
	public void ClearLabels()
	{
		List<string> list = new List<string>(this.descLabels.Keys);
		if (list.Count > 0)
		{
			foreach (string text in list)
			{
				GameObject gameObject = this.descLabels[text];
				if (gameObject != null)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
				this.descLabels.Remove(text);
			}
		}
	}

	// Token: 0x06005ADE RID: 23262 RVA: 0x00210078 File Offset: 0x0020E278
	public void SetMaterials(BuildingDef def)
	{
		this.materialSelectionPanel.gameObject.SetActive(true);
		Recipe craftRecipe = def.CraftRecipe;
		this.materialSelectionPanel.ClearSelectActions();
		this.materialSelectionPanel.ConfigureScreen(craftRecipe, new MaterialSelectionPanel.GetBuildableStateDelegate(PlanScreen.Instance.IsDefBuildable), new MaterialSelectionPanel.GetBuildableTooltipDelegate(PlanScreen.Instance.GetTooltipForBuildable));
		this.materialSelectionPanel.ToggleShowDescriptorPanels(false);
		this.materialSelectionPanel.AddSelectAction(new MaterialSelector.SelectMaterialActions(this.RefreshScreen));
		this.materialSelectionPanel.AddSelectAction(new MaterialSelector.SelectMaterialActions(this.onMenuMaterialChanged));
		this.materialSelectionPanel.AutoSelectAvailableMaterial();
		this.ActivateAppropriateTool(def);
	}

	// Token: 0x06005ADF RID: 23263 RVA: 0x00210121 File Offset: 0x0020E321
	private void OnFacadeSelectionChanged()
	{
		if (this.currentDef == null)
		{
			return;
		}
		this.ActivateAppropriateTool(this.currentDef);
	}

	// Token: 0x06005AE0 RID: 23264 RVA: 0x0021013E File Offset: 0x0020E33E
	private void onMenuMaterialChanged()
	{
		if (this.currentDef == null)
		{
			return;
		}
		this.ActivateAppropriateTool(this.currentDef);
		this.SetDescription(this.currentDef);
	}

	// Token: 0x06005AE1 RID: 23265 RVA: 0x00210168 File Offset: 0x0020E368
	private void ActivateAppropriateTool(BuildingDef def)
	{
		global::Debug.Assert(def != null, "def was null");
		if (((PlanScreen.Instance != null) ? PlanScreen.Instance.IsDefBuildable(def) : (BuildMenu.Instance != null && BuildMenu.Instance.BuildableState(def) == PlanScreen.RequirementsState.Complete)) && this.materialSelectionPanel.AllSelectorsSelected() && this.facadeSelectionPanel.SelectedFacade != null)
		{
			this.onElementsFullySelected.Signal();
			return;
		}
		if (!MaterialSelector.AllowInsufficientMaterialBuild() && !DebugHandler.InstantBuildMode)
		{
			if (PlayerController.Instance.ActiveTool == BuildTool.Instance)
			{
				BuildTool.Instance.Deactivate();
			}
			PrebuildTool.Instance.Activate(def, PlanScreen.Instance.GetTooltipForBuildable(def));
		}
	}

	// Token: 0x06005AE2 RID: 23266 RVA: 0x0021022C File Offset: 0x0020E42C
	public static bool MaterialsMet(Recipe recipe)
	{
		if (recipe == null)
		{
			global::Debug.LogError("Trying to verify the materials on a null recipe!");
			return false;
		}
		if (recipe.Ingredients == null || recipe.Ingredients.Count == 0)
		{
			global::Debug.LogError("Trying to verify the materials on a recipe with no MaterialCategoryTags!");
			return false;
		}
		bool flag = true;
		for (int i = 0; i < recipe.Ingredients.Count; i++)
		{
			if (MaterialSelectionPanel.Filter(recipe.Ingredients[i].tag).kgAvailable < recipe.Ingredients[i].amount)
			{
				flag = false;
				break;
			}
		}
		return flag;
	}

	// Token: 0x06005AE3 RID: 23267 RVA: 0x002102B4 File Offset: 0x0020E4B4
	public void Close()
	{
		if (this.configuring)
		{
			return;
		}
		this.ClearProduct(true);
		this.Show(false);
	}

	// Token: 0x04003D8A RID: 15754
	public TitleBar titleBar;

	// Token: 0x04003D8B RID: 15755
	public GameObject ProductDescriptionPane;

	// Token: 0x04003D8C RID: 15756
	public LocText productDescriptionText;

	// Token: 0x04003D8D RID: 15757
	public DescriptorPanel ProductRequirementsPane;

	// Token: 0x04003D8E RID: 15758
	public DescriptorPanel ProductEffectsPane;

	// Token: 0x04003D8F RID: 15759
	public GameObject ProductFlavourPane;

	// Token: 0x04003D90 RID: 15760
	public LocText productFlavourText;

	// Token: 0x04003D91 RID: 15761
	public RectTransform BGPanel;

	// Token: 0x04003D92 RID: 15762
	public MaterialSelectionPanel materialSelectionPanelPrefab;

	// Token: 0x04003D93 RID: 15763
	public FacadeSelectionPanel facadeSelectionPanelPrefab;

	// Token: 0x04003D94 RID: 15764
	private Dictionary<string, GameObject> descLabels = new Dictionary<string, GameObject>();

	// Token: 0x04003D95 RID: 15765
	public MultiToggle sandboxInstantBuildToggle;

	// Token: 0x04003D96 RID: 15766
	[NonSerialized]
	public MaterialSelectionPanel materialSelectionPanel;

	// Token: 0x04003D97 RID: 15767
	[SerializeField]
	private FacadeSelectionPanel facadeSelectionPanel;

	// Token: 0x04003D98 RID: 15768
	[NonSerialized]
	public BuildingDef currentDef;

	// Token: 0x04003D99 RID: 15769
	public System.Action onElementsFullySelected;

	// Token: 0x04003D9A RID: 15770
	private bool expandedInfo = true;

	// Token: 0x04003D9B RID: 15771
	private bool configuring;
}
