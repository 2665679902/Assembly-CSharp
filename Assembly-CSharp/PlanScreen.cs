using System;
using System.Collections.Generic;
using System.Linq;
using FMOD.Studio;
using STRINGS;
using TUNING;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000B59 RID: 2905
public class PlanScreen : KIconToggleMenu
{
	// Token: 0x17000666 RID: 1638
	// (get) Token: 0x06005A5B RID: 23131 RVA: 0x0020BC0C File Offset: 0x00209E0C
	// (set) Token: 0x06005A5C RID: 23132 RVA: 0x0020BC13 File Offset: 0x00209E13
	public static PlanScreen Instance { get; private set; }

	// Token: 0x06005A5D RID: 23133 RVA: 0x0020BC1B File Offset: 0x00209E1B
	public static void DestroyInstance()
	{
		PlanScreen.Instance = null;
	}

	// Token: 0x17000667 RID: 1639
	// (get) Token: 0x06005A5E RID: 23134 RVA: 0x0020BC23 File Offset: 0x00209E23
	public static Dictionary<HashedString, string> IconNameMap
	{
		get
		{
			return PlanScreen.iconNameMap;
		}
	}

	// Token: 0x06005A5F RID: 23135 RVA: 0x0020BC2A File Offset: 0x00209E2A
	private static HashedString CacheHashedString(string str)
	{
		return HashCache.Get().Add(str);
	}

	// Token: 0x17000668 RID: 1640
	// (get) Token: 0x06005A60 RID: 23136 RVA: 0x0020BC37 File Offset: 0x00209E37
	// (set) Token: 0x06005A61 RID: 23137 RVA: 0x0020BC3F File Offset: 0x00209E3F
	public ProductInfoScreen ProductInfoScreen { get; private set; }

	// Token: 0x17000669 RID: 1641
	// (get) Token: 0x06005A62 RID: 23138 RVA: 0x0020BC48 File Offset: 0x00209E48
	public KIconToggleMenu.ToggleInfo ActiveCategoryToggleInfo
	{
		get
		{
			return this.activeCategoryInfo;
		}
	}

	// Token: 0x1700066A RID: 1642
	// (get) Token: 0x06005A63 RID: 23139 RVA: 0x0020BC50 File Offset: 0x00209E50
	// (set) Token: 0x06005A64 RID: 23140 RVA: 0x0020BC58 File Offset: 0x00209E58
	public GameObject SelectedBuildingGameObject { get; private set; }

	// Token: 0x06005A65 RID: 23141 RVA: 0x0020BC61 File Offset: 0x00209E61
	public override float GetSortKey()
	{
		return 2f;
	}

	// Token: 0x06005A66 RID: 23142 RVA: 0x0020BC68 File Offset: 0x00209E68
	public PlanScreen.RequirementsState GetBuildableState(BuildingDef def)
	{
		if (def == null)
		{
			return PlanScreen.RequirementsState.Materials;
		}
		return this._buildableStatesByID[def.PrefabID];
	}

	// Token: 0x06005A67 RID: 23143 RVA: 0x0020BC88 File Offset: 0x00209E88
	private bool IsDefResearched(BuildingDef def)
	{
		bool flag = false;
		if (!this._researchedDefs.TryGetValue(def, out flag))
		{
			flag = this.UpdateDefResearched(def);
		}
		return flag;
	}

	// Token: 0x06005A68 RID: 23144 RVA: 0x0020BCB0 File Offset: 0x00209EB0
	private bool UpdateDefResearched(BuildingDef def)
	{
		return this._researchedDefs[def] = Db.Get().TechItems.IsTechItemComplete(def.PrefabID);
	}

	// Token: 0x06005A69 RID: 23145 RVA: 0x0020BCE4 File Offset: 0x00209EE4
	protected override void OnPrefabInit()
	{
		if (BuildMenu.UseHotkeyBuildMenu())
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			base.OnPrefabInit();
			PlanScreen.Instance = this;
			this.ProductInfoScreen = global::Util.KInstantiateUI<ProductInfoScreen>(this.productInfoScreenPrefab, this.recipeInfoScreenParent, false);
			this.ProductInfoScreen.rectTransform().pivot = new Vector2(0f, 0f);
			this.ProductInfoScreen.rectTransform().SetLocalPosition(new Vector3(326f, 0f, 0f));
			this.ProductInfoScreen.onElementsFullySelected = new System.Action(this.OnRecipeElementsFullySelected);
			KInputManager.InputChange.AddListener(new UnityAction(this.RefreshToolTip));
			this.planScreenScrollRect = base.transform.parent.GetComponentInParent<KScrollRect>();
			Game.Instance.Subscribe(-107300940, new Action<object>(this.OnResearchComplete));
			Game.Instance.Subscribe(1174281782, new Action<object>(this.OnActiveToolChanged));
			Game.Instance.Subscribe(1557339983, new Action<object>(this.ForceUpdateAllCategoryToggles));
		}
		this.buildingGroupsRoot.gameObject.SetActive(false);
	}

	// Token: 0x06005A6A RID: 23146 RVA: 0x0020BE1C File Offset: 0x0020A01C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.ConsumeMouseScroll = true;
		this.useSubCategoryLayout = KPlayerPrefs.GetInt("usePlanScreenListView") == 1;
		this.initTime = KTime.Instance.UnscaledGameTime;
		foreach (BuildingDef buildingDef in Assets.BuildingDefs)
		{
			this._buildableStatesByID.Add(buildingDef.PrefabID, PlanScreen.RequirementsState.Materials);
		}
		if (BuildMenu.UseHotkeyBuildMenu())
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			base.onSelect += this.OnClickCategory;
			this.Refresh();
			foreach (KToggle ktoggle in this.toggles)
			{
				ktoggle.group = base.GetComponent<ToggleGroup>();
			}
			this.RefreshBuildableStates(true);
			Game.Instance.Subscribe(288942073, new Action<object>(this.OnUIClear));
		}
		this.copyBuildingButton.GetComponent<MultiToggle>().onClick = delegate
		{
			this.OnClickCopyBuilding();
		};
		this.RefreshCopyBuildingButton(null);
		Game.Instance.Subscribe(-1503271301, new Action<object>(this.RefreshCopyBuildingButton));
		Game.Instance.Subscribe(1983128072, delegate(object data)
		{
			this.CloseRecipe(false);
		});
		this.pointerEnterActions = (KScreen.PointerEnterActions)Delegate.Combine(this.pointerEnterActions, new KScreen.PointerEnterActions(this.PointerEnter));
		this.pointerExitActions = (KScreen.PointerExitActions)Delegate.Combine(this.pointerExitActions, new KScreen.PointerExitActions(this.PointerExit));
		this.copyBuildingButton.GetComponent<ToolTip>().SetSimpleTooltip(GameUtil.ReplaceHotkeyString(UI.COPY_BUILDING_TOOLTIP, global::Action.CopyBuilding));
		this.RefreshScale(null);
		this.refreshScaleHandle = Game.Instance.Subscribe(-442024484, new Action<object>(this.RefreshScale));
		this.BuildButtonList();
		this.gridViewButton.onClick += this.OnClickGridView;
		this.listViewButton.onClick += this.OnClickListView;
	}

	// Token: 0x06005A6B RID: 23147 RVA: 0x0020C060 File Offset: 0x0020A260
	private void RefreshScale(object data = null)
	{
		base.GetComponent<GridLayoutGroup>().cellSize = (ScreenResolutionMonitor.UsingGamepadUIMode() ? new Vector2(54f, 50f) : new Vector2(45f, 45f));
		this.toggles.ForEach(delegate(KToggle to)
		{
			to.GetComponentInChildren<LocText>().fontSize = (float)(ScreenResolutionMonitor.UsingGamepadUIMode() ? PlanScreen.fontSizeBigMode : PlanScreen.fontSizeStandardMode);
		});
		LayoutElement component = this.copyBuildingButton.GetComponent<LayoutElement>();
		component.minWidth = (float)(ScreenResolutionMonitor.UsingGamepadUIMode() ? 58 : 54);
		component.minHeight = (float)(ScreenResolutionMonitor.UsingGamepadUIMode() ? 58 : 54);
		base.gameObject.rectTransform().anchoredPosition = new Vector2(0f, (float)(ScreenResolutionMonitor.UsingGamepadUIMode() ? (-68) : (-74)));
		this.adjacentPinnedButtons.GetComponent<HorizontalLayoutGroup>().padding.bottom = (ScreenResolutionMonitor.UsingGamepadUIMode() ? 14 : 6);
		Vector2 sizeDelta = this.buildingGroupsRoot.rectTransform().sizeDelta;
		Vector2 vector = (ScreenResolutionMonitor.UsingGamepadUIMode() ? new Vector2(320f, sizeDelta.y) : new Vector2(264f, sizeDelta.y));
		this.buildingGroupsRoot.rectTransform().sizeDelta = vector;
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.allSubCategoryObjects)
		{
			GridLayoutGroup componentInChildren = keyValuePair.Value.GetComponentInChildren<GridLayoutGroup>(true);
			if (this.useSubCategoryLayout)
			{
				componentInChildren.constraintCount = 1;
				componentInChildren.cellSize = new Vector2(vector.x - 24f, 36f);
			}
			else
			{
				componentInChildren.constraintCount = 3;
				componentInChildren.cellSize = (ScreenResolutionMonitor.UsingGamepadUIMode() ? PlanScreen.bigBuildingButtonSize : PlanScreen.standarduildingButtonSize);
			}
		}
		this.ProductInfoScreen.rectTransform().anchoredPosition = new Vector2(vector.x + 8f, this.ProductInfoScreen.rectTransform().anchoredPosition.y);
	}

	// Token: 0x06005A6C RID: 23148 RVA: 0x0020C268 File Offset: 0x0020A468
	protected override void OnForcedCleanUp()
	{
		KInputManager.InputChange.RemoveListener(new UnityAction(this.RefreshToolTip));
		base.OnForcedCleanUp();
	}

	// Token: 0x06005A6D RID: 23149 RVA: 0x0020C286 File Offset: 0x0020A486
	protected override void OnCleanUp()
	{
		if (Game.Instance != null)
		{
			Game.Instance.Unsubscribe(this.refreshScaleHandle);
		}
		base.OnCleanUp();
	}

	// Token: 0x06005A6E RID: 23150 RVA: 0x0020C2AC File Offset: 0x0020A4AC
	private void OnClickCopyBuilding()
	{
		if (!this.LastSelectedBuilding.IsNullOrDestroyed() && this.LastSelectedBuilding.gameObject.activeInHierarchy)
		{
			PlanScreen.Instance.CopyBuildingOrder(this.LastSelectedBuilding);
			return;
		}
		if (this.lastSelectedBuildingDef != null)
		{
			PlanScreen.Instance.CopyBuildingOrder(this.lastSelectedBuildingDef, this.LastSelectedBuildingFacade);
		}
	}

	// Token: 0x06005A6F RID: 23151 RVA: 0x0020C30D File Offset: 0x0020A50D
	private void OnClickListView()
	{
		this.useSubCategoryLayout = true;
		this.ForceRefreshAllBuildingToggles();
		this.BuildButtonList();
		this.ConfigurePanelSize(null);
		this.RefreshScale(null);
		KPlayerPrefs.SetInt("usePlanScreenListView", 1);
	}

	// Token: 0x06005A70 RID: 23152 RVA: 0x0020C33B File Offset: 0x0020A53B
	private void OnClickGridView()
	{
		this.useSubCategoryLayout = false;
		this.ForceRefreshAllBuildingToggles();
		this.BuildButtonList();
		this.ConfigurePanelSize(null);
		this.RefreshScale(null);
		KPlayerPrefs.SetInt("usePlanScreenListView", 0);
	}

	// Token: 0x1700066B RID: 1643
	// (get) Token: 0x06005A71 RID: 23153 RVA: 0x0020C369 File Offset: 0x0020A569
	// (set) Token: 0x06005A72 RID: 23154 RVA: 0x0020C374 File Offset: 0x0020A574
	private Building LastSelectedBuilding
	{
		get
		{
			return this.lastSelectedBuilding;
		}
		set
		{
			this.lastSelectedBuilding = value;
			if (this.lastSelectedBuilding != null)
			{
				this.lastSelectedBuildingDef = this.lastSelectedBuilding.Def;
				if (this.lastSelectedBuilding.gameObject.activeInHierarchy)
				{
					this.LastSelectedBuildingFacade = this.lastSelectedBuilding.GetComponent<BuildingFacade>().CurrentFacade;
				}
			}
		}
	}

	// Token: 0x1700066C RID: 1644
	// (get) Token: 0x06005A73 RID: 23155 RVA: 0x0020C3CF File Offset: 0x0020A5CF
	// (set) Token: 0x06005A74 RID: 23156 RVA: 0x0020C3D7 File Offset: 0x0020A5D7
	public string LastSelectedBuildingFacade
	{
		get
		{
			return this.lastSelectedBuildingFacade;
		}
		set
		{
			this.lastSelectedBuildingFacade = value;
		}
	}

	// Token: 0x06005A75 RID: 23157 RVA: 0x0020C3E0 File Offset: 0x0020A5E0
	public void RefreshCopyBuildingButton(object data = null)
	{
		this.adjacentPinnedButtons.rectTransform().anchoredPosition = new Vector2(Mathf.Min(base.gameObject.rectTransform().sizeDelta.x, base.transform.parent.rectTransform().rect.width), 0f);
		MultiToggle component = this.copyBuildingButton.GetComponent<MultiToggle>();
		if (SelectTool.Instance != null && SelectTool.Instance.selected != null)
		{
			Building component2 = SelectTool.Instance.selected.GetComponent<Building>();
			if (component2 != null && component2.Def.ShouldShowInBuildMenu() && component2.Def.IsAvailable())
			{
				this.LastSelectedBuilding = component2;
			}
		}
		if (this.lastSelectedBuildingDef != null)
		{
			component.gameObject.SetActive(PlanScreen.Instance.gameObject.activeInHierarchy);
			Sprite sprite = this.lastSelectedBuildingDef.GetUISprite("ui", false);
			if (this.LastSelectedBuildingFacade != null && this.LastSelectedBuildingFacade != "DEFAULT_FACADE")
			{
				sprite = Def.GetFacadeUISprite(this.LastSelectedBuildingFacade);
			}
			component.transform.Find("FG").GetComponent<Image>().sprite = sprite;
			component.transform.Find("FG").GetComponent<Image>().color = Color.white;
			component.ChangeState(1);
			return;
		}
		component.gameObject.SetActive(false);
		component.ChangeState(0);
	}

	// Token: 0x06005A76 RID: 23158 RVA: 0x0020C55C File Offset: 0x0020A75C
	public void RefreshToolTip()
	{
		for (int i = 0; i < TUNING.BUILDINGS.PLANORDER.Count; i++)
		{
			PlanScreen.PlanInfo planInfo = TUNING.BUILDINGS.PLANORDER[i];
			if (DlcManager.IsContentActive(planInfo.RequiredDlcId))
			{
				global::Action action = ((i < 14) ? (global::Action.Plan1 + i) : global::Action.NumActions);
				string text = HashCache.Get().Get(planInfo.category).ToUpper();
				this.toggleInfo[i].tooltip = GameUtil.ReplaceHotkeyString(Strings.Get("STRINGS.UI.BUILDCATEGORIES." + text + ".TOOLTIP"), action);
			}
		}
		this.copyBuildingButton.GetComponent<ToolTip>().SetSimpleTooltip(GameUtil.ReplaceHotkeyString(UI.COPY_BUILDING_TOOLTIP, global::Action.CopyBuilding));
	}

	// Token: 0x06005A77 RID: 23159 RVA: 0x0020C614 File Offset: 0x0020A814
	public void Refresh()
	{
		List<KIconToggleMenu.ToggleInfo> list = new List<KIconToggleMenu.ToggleInfo>();
		if (this.tagCategoryMap == null)
		{
			int num = 0;
			this.tagCategoryMap = new Dictionary<Tag, HashedString>();
			this.tagOrderMap = new Dictionary<Tag, int>();
			if (TUNING.BUILDINGS.PLANORDER.Count > 15)
			{
				DebugUtil.LogWarningArgs(new object[]
				{
					"Insufficient keys to cover root plan menu",
					"Max of 14 keys supported but TUNING.BUILDINGS.PLANORDER has " + TUNING.BUILDINGS.PLANORDER.Count.ToString()
				});
			}
			this.toggleEntries.Clear();
			for (int i = 0; i < TUNING.BUILDINGS.PLANORDER.Count; i++)
			{
				PlanScreen.PlanInfo planInfo = TUNING.BUILDINGS.PLANORDER[i];
				if (DlcManager.IsContentActive(planInfo.RequiredDlcId))
				{
					global::Action action = ((i < 15) ? (global::Action.Plan1 + i) : global::Action.NumActions);
					string text = PlanScreen.iconNameMap[planInfo.category];
					string text2 = HashCache.Get().Get(planInfo.category).ToUpper();
					KIconToggleMenu.ToggleInfo toggleInfo = new KIconToggleMenu.ToggleInfo(UI.StripLinkFormatting(Strings.Get("STRINGS.UI.BUILDCATEGORIES." + text2 + ".NAME")), text, planInfo.category, action, GameUtil.ReplaceHotkeyString(Strings.Get("STRINGS.UI.BUILDCATEGORIES." + text2 + ".TOOLTIP"), action), "");
					list.Add(toggleInfo);
					PlanScreen.PopulateOrderInfo(planInfo.category, planInfo.buildingAndSubcategoryData, this.tagCategoryMap, this.tagOrderMap, ref num);
					List<BuildingDef> list2 = new List<BuildingDef>();
					foreach (BuildingDef buildingDef in Assets.BuildingDefs)
					{
						HashedString hashedString;
						if (buildingDef.IsAvailable() && this.tagCategoryMap.TryGetValue(buildingDef.Tag, out hashedString) && !(hashedString != planInfo.category))
						{
							list2.Add(buildingDef);
						}
					}
					this.toggleEntries.Add(new PlanScreen.ToggleEntry(toggleInfo, planInfo.category, list2, planInfo.hideIfNotResearched));
				}
			}
			base.Setup(list);
			this.toggles.ForEach(delegate(KToggle to)
			{
				foreach (ImageToggleState imageToggleState in to.GetComponents<ImageToggleState>())
				{
					if (imageToggleState.TargetImage.sprite != null && imageToggleState.TargetImage.name == "FG" && !imageToggleState.useSprites)
					{
						imageToggleState.SetSprites(Assets.GetSprite(imageToggleState.TargetImage.sprite.name + "_disabled"), imageToggleState.TargetImage.sprite, imageToggleState.TargetImage.sprite, Assets.GetSprite(imageToggleState.TargetImage.sprite.name + "_disabled"));
					}
				}
				to.GetComponent<KToggle>().soundPlayer.Enabled = false;
				to.GetComponentInChildren<LocText>().fontSize = (float)(ScreenResolutionMonitor.UsingGamepadUIMode() ? PlanScreen.fontSizeBigMode : PlanScreen.fontSizeStandardMode);
			});
			for (int j = 0; j < this.toggleEntries.Count; j++)
			{
				PlanScreen.ToggleEntry toggleEntry = this.toggleEntries[j];
				toggleEntry.CollectToggleImages();
				this.toggleEntries[j] = toggleEntry;
			}
			this.ForceUpdateAllCategoryToggles(null);
		}
	}

	// Token: 0x06005A78 RID: 23160 RVA: 0x0020C8AC File Offset: 0x0020AAAC
	private void ForceUpdateAllCategoryToggles(object data = null)
	{
		this.forceUpdateAllCategoryToggles = true;
	}

	// Token: 0x06005A79 RID: 23161 RVA: 0x0020C8B5 File Offset: 0x0020AAB5
	public void ForceRefreshAllBuildingToggles()
	{
		this.forceRefreshAllBuildings = true;
	}

	// Token: 0x06005A7A RID: 23162 RVA: 0x0020C8C0 File Offset: 0x0020AAC0
	public void CopyBuildingOrder(BuildingDef buildingDef, string facadeID)
	{
		foreach (PlanScreen.PlanInfo planInfo in TUNING.BUILDINGS.PLANORDER)
		{
			foreach (KeyValuePair<string, string> keyValuePair in planInfo.buildingAndSubcategoryData)
			{
				if (buildingDef.PrefabID == keyValuePair.Key)
				{
					this.OpenCategoryByName(HashCache.Get().Get(planInfo.category));
					this.OnSelectBuilding(this.activeCategoryBuildingToggles[buildingDef].gameObject, buildingDef, facadeID);
					break;
				}
			}
		}
	}

	// Token: 0x06005A7B RID: 23163 RVA: 0x0020C990 File Offset: 0x0020AB90
	public void CopyBuildingOrder(Building building)
	{
		this.CopyBuildingOrder(building.Def, building.GetComponent<BuildingFacade>().CurrentFacade);
		if (this.ProductInfoScreen.materialSelectionPanel == null)
		{
			DebugUtil.DevLogError(building.Def.name + " def likely needs to be marked def.ShowInBuildMenu = false");
			return;
		}
		this.ProductInfoScreen.materialSelectionPanel.SelectSourcesMaterials(building);
		Rotatable component = building.GetComponent<Rotatable>();
		if (component != null)
		{
			BuildTool.Instance.SetToolOrientation(component.GetOrientation());
		}
	}

	// Token: 0x06005A7C RID: 23164 RVA: 0x0020CA14 File Offset: 0x0020AC14
	private static void PopulateOrderInfo(HashedString category, object data, Dictionary<Tag, HashedString> category_map, Dictionary<Tag, int> order_map, ref int building_index)
	{
		if (data.GetType() == typeof(PlanScreen.PlanInfo))
		{
			PlanScreen.PlanInfo planInfo = (PlanScreen.PlanInfo)data;
			PlanScreen.PopulateOrderInfo(planInfo.category, planInfo.buildingAndSubcategoryData, category_map, order_map, ref building_index);
			return;
		}
		foreach (KeyValuePair<string, string> keyValuePair in ((List<KeyValuePair<string, string>>)data))
		{
			Tag tag = new Tag(keyValuePair.Key);
			category_map[tag] = category;
			order_map[tag] = building_index;
			building_index++;
		}
	}

	// Token: 0x06005A7D RID: 23165 RVA: 0x0020CABC File Offset: 0x0020ACBC
	protected override void OnCmpEnable()
	{
		this.Refresh();
		this.RefreshCopyBuildingButton(null);
	}

	// Token: 0x06005A7E RID: 23166 RVA: 0x0020CACB File Offset: 0x0020ACCB
	protected override void OnCmpDisable()
	{
		this.ClearButtons();
	}

	// Token: 0x06005A7F RID: 23167 RVA: 0x0020CAD4 File Offset: 0x0020ACD4
	private void ClearButtons()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.allSubCategoryObjects)
		{
		}
		foreach (KeyValuePair<string, PlanBuildingToggle> keyValuePair2 in this.allBuildingToggles)
		{
			keyValuePair2.Value.gameObject.SetActive(false);
		}
		this.activeCategoryBuildingToggles.Clear();
		this.copyBuildingButton.gameObject.SetActive(false);
		this.copyBuildingButton.GetComponent<MultiToggle>().ChangeState(0);
	}

	// Token: 0x06005A80 RID: 23168 RVA: 0x0020CB9C File Offset: 0x0020AD9C
	public void OnSelectBuilding(GameObject button_go, BuildingDef def, string facadeID = null)
	{
		if (button_go == null)
		{
			global::Debug.Log("Button gameObject is null", base.gameObject);
			return;
		}
		if (button_go == this.SelectedBuildingGameObject)
		{
			this.CloseRecipe(true);
			return;
		}
		this.ignoreToolChangeMessages++;
		PlanBuildingToggle planBuildingToggle = null;
		if (this.currentlySelectedToggle != null)
		{
			planBuildingToggle = this.currentlySelectedToggle.GetComponent<PlanBuildingToggle>();
		}
		this.SelectedBuildingGameObject = button_go;
		this.currentlySelectedToggle = button_go.GetComponent<KToggle>();
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click", false));
		HashedString hashedString = this.tagCategoryMap[def.Tag];
		PlanScreen.ToggleEntry toggleEntry;
		if (this.GetToggleEntryForCategory(hashedString, out toggleEntry) && toggleEntry.pendingResearchAttentions.Contains(def.Tag))
		{
			toggleEntry.pendingResearchAttentions.Remove(def.Tag);
			button_go.GetComponent<PlanCategoryNotifications>().ToggleAttention(false);
			if (toggleEntry.pendingResearchAttentions.Count == 0)
			{
				toggleEntry.toggleInfo.toggle.GetComponent<PlanCategoryNotifications>().ToggleAttention(false);
			}
		}
		this.ProductInfoScreen.ClearProduct(false);
		if (planBuildingToggle != null)
		{
			planBuildingToggle.Refresh();
		}
		ToolMenu.Instance.ClearSelection();
		PrebuildTool.Instance.Activate(def, this.GetTooltipForBuildable(def));
		this.LastSelectedBuilding = def.BuildingComplete.GetComponent<Building>();
		this.RefreshCopyBuildingButton(null);
		this.ProductInfoScreen.Show(true);
		this.ProductInfoScreen.ConfigureScreen(def, facadeID);
		this.ignoreToolChangeMessages--;
	}

	// Token: 0x06005A81 RID: 23169 RVA: 0x0020CD10 File Offset: 0x0020AF10
	private void RefreshBuildableStates(bool force_update)
	{
		if (Assets.BuildingDefs == null || Assets.BuildingDefs.Count == 0)
		{
			return;
		}
		if (this.timeSinceNotificationPing < this.specialNotificationEmbellishDelay)
		{
			this.timeSinceNotificationPing += Time.unscaledDeltaTime;
		}
		if (this.timeSinceNotificationPing >= this.notificationPingExpire)
		{
			this.notificationPingCount = 0;
		}
		int num = 1;
		if (force_update)
		{
			num = Assets.BuildingDefs.Count;
			this.buildable_state_update_idx = 0;
		}
		ListPool<HashedString, PlanScreen>.PooledList pooledList = ListPool<HashedString, PlanScreen>.Allocate();
		for (int i = 0; i < ((this.activeCategoryInfo == null) ? num : Math.Max(num, 20)); i++)
		{
			this.buildable_state_update_idx = (this.buildable_state_update_idx + 1) % Assets.BuildingDefs.Count;
			BuildingDef buildingDef = Assets.BuildingDefs[this.buildable_state_update_idx];
			PlanScreen.RequirementsState buildableStateForDef = this.GetBuildableStateForDef(buildingDef);
			HashedString hashedString;
			if (this.tagCategoryMap.TryGetValue(buildingDef.Tag, out hashedString) && this._buildableStatesByID[buildingDef.PrefabID] != buildableStateForDef)
			{
				this._buildableStatesByID[buildingDef.PrefabID] = buildableStateForDef;
				if (this.ProductInfoScreen.currentDef == buildingDef)
				{
					this.ignoreToolChangeMessages++;
					this.ProductInfoScreen.ClearProduct(false);
					this.ProductInfoScreen.Show(true);
					this.ProductInfoScreen.ConfigureScreen(buildingDef);
					this.ignoreToolChangeMessages--;
				}
				if (buildableStateForDef == PlanScreen.RequirementsState.Complete)
				{
					foreach (KIconToggleMenu.ToggleInfo toggleInfo in this.toggleInfo)
					{
						if ((HashedString)toggleInfo.userData == hashedString)
						{
							Bouncer component = toggleInfo.toggle.GetComponent<Bouncer>();
							if (component != null && !component.IsBouncing() && !pooledList.Contains(hashedString))
							{
								pooledList.Add(hashedString);
								component.Bounce();
								if (KTime.Instance.UnscaledGameTime - this.initTime > 1.5f)
								{
									if (this.timeSinceNotificationPing >= this.specialNotificationEmbellishDelay)
									{
										string sound = GlobalAssets.GetSound("NewBuildable_Embellishment", false);
										if (sound != null)
										{
											SoundEvent.EndOneShot(SoundEvent.BeginOneShot(sound, SoundListenerController.Instance.transform.GetPosition(), 1f, false));
										}
									}
									string sound2 = GlobalAssets.GetSound("NewBuildable", false);
									if (sound2 != null)
									{
										EventInstance eventInstance = SoundEvent.BeginOneShot(sound2, SoundListenerController.Instance.transform.GetPosition(), 1f, false);
										eventInstance.setParameterByName("playCount", (float)this.notificationPingCount, false);
										SoundEvent.EndOneShot(eventInstance);
									}
								}
								this.timeSinceNotificationPing = 0f;
								this.notificationPingCount++;
							}
						}
					}
				}
			}
		}
		pooledList.Recycle();
	}

	// Token: 0x06005A82 RID: 23170 RVA: 0x0020CFEC File Offset: 0x0020B1EC
	private PlanScreen.RequirementsState GetBuildableStateForDef(BuildingDef def)
	{
		if (!def.IsAvailable())
		{
			return PlanScreen.RequirementsState.Invalid;
		}
		PlanScreen.RequirementsState requirementsState = PlanScreen.RequirementsState.Complete;
		if (!DebugHandler.InstantBuildMode && !Game.Instance.SandboxModeActive && !this.IsDefResearched(def))
		{
			requirementsState = PlanScreen.RequirementsState.Tech;
		}
		else if (def.BuildingComplete.HasTag(GameTags.Telepad) && ClusterUtil.ActiveWorldHasPrinter())
		{
			requirementsState = PlanScreen.RequirementsState.TelepadBuilt;
		}
		else if (def.BuildingComplete.HasTag(GameTags.RocketInteriorBuilding) && !ClusterUtil.ActiveWorldIsRocketInterior())
		{
			requirementsState = PlanScreen.RequirementsState.RocketInteriorOnly;
		}
		else if (def.BuildingComplete.HasTag(GameTags.NotRocketInteriorBuilding) && ClusterUtil.ActiveWorldIsRocketInterior())
		{
			requirementsState = PlanScreen.RequirementsState.RocketInteriorForbidden;
		}
		else if (def.BuildingComplete.HasTag(GameTags.UniquePerWorld) && BuildingInventory.Instance.BuildingCountForWorld_BAD_PERF(def.Tag, ClusterManager.Instance.activeWorldId) > 0)
		{
			requirementsState = PlanScreen.RequirementsState.UniquePerWorld;
		}
		else if (!DebugHandler.InstantBuildMode && !Game.Instance.SandboxModeActive && !ProductInfoScreen.MaterialsMet(def.CraftRecipe))
		{
			requirementsState = PlanScreen.RequirementsState.Materials;
		}
		return requirementsState;
	}

	// Token: 0x06005A83 RID: 23171 RVA: 0x0020D0D8 File Offset: 0x0020B2D8
	private void SetCategoryButtonState()
	{
		this.nextCategoryToUpdateIDX = (this.nextCategoryToUpdateIDX + 1) % this.toggleEntries.Count;
		for (int i = 0; i < this.toggleEntries.Count; i++)
		{
			if (this.forceUpdateAllCategoryToggles || i == this.nextCategoryToUpdateIDX)
			{
				PlanScreen.ToggleEntry toggleEntry = this.toggleEntries[i];
				KIconToggleMenu.ToggleInfo toggleInfo = toggleEntry.toggleInfo;
				toggleInfo.toggle.ActivateFlourish(this.activeCategoryInfo != null && toggleInfo.userData == this.activeCategoryInfo.userData);
				bool flag = false;
				bool flag2 = true;
				if (DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive)
				{
					flag = true;
					flag2 = false;
				}
				else
				{
					foreach (BuildingDef buildingDef in toggleEntry.buildingDefs)
					{
						if (this.GetBuildableState(buildingDef) == PlanScreen.RequirementsState.Complete)
						{
							flag = true;
							flag2 = false;
							break;
						}
					}
					if (flag2 && toggleEntry.AreAnyRequiredTechItemsAvailable())
					{
						flag2 = false;
					}
				}
				this.CategoryInteractive[toggleInfo] = !flag2;
				GameObject gameObject = toggleInfo.toggle.fgImage.transform.Find("ResearchIcon").gameObject;
				if (!flag)
				{
					if (flag2 && toggleEntry.hideIfNotResearched)
					{
						toggleInfo.toggle.gameObject.SetActive(false);
					}
					else if (flag2)
					{
						toggleInfo.toggle.gameObject.SetActive(true);
						gameObject.gameObject.SetActive(true);
					}
					else
					{
						toggleInfo.toggle.gameObject.SetActive(true);
						gameObject.gameObject.SetActive(false);
					}
					ImageToggleState.State state = ((this.activeCategoryInfo != null && toggleInfo.userData == this.activeCategoryInfo.userData) ? ImageToggleState.State.DisabledActive : ImageToggleState.State.Disabled);
					ImageToggleState[] array = toggleEntry.toggleImages;
					for (int j = 0; j < array.Length; j++)
					{
						array[j].SetState(state);
					}
				}
				else
				{
					toggleInfo.toggle.gameObject.SetActive(true);
					gameObject.gameObject.SetActive(false);
					ImageToggleState.State state2 = ((this.activeCategoryInfo == null || toggleInfo.userData != this.activeCategoryInfo.userData) ? ImageToggleState.State.Inactive : ImageToggleState.State.Active);
					ImageToggleState[] array = toggleEntry.toggleImages;
					for (int j = 0; j < array.Length; j++)
					{
						array[j].SetState(state2);
					}
				}
			}
		}
		this.RefreshCopyBuildingButton(null);
		this.forceUpdateAllCategoryToggles = false;
	}

	// Token: 0x06005A84 RID: 23172 RVA: 0x0020D344 File Offset: 0x0020B544
	private void DeactivateBuildTools()
	{
		InterfaceTool activeTool = PlayerController.Instance.ActiveTool;
		if (activeTool != null)
		{
			Type type = activeTool.GetType();
			if (type == typeof(BuildTool) || typeof(BaseUtilityBuildTool).IsAssignableFrom(type) || type == typeof(PrebuildTool))
			{
				activeTool.DeactivateTool(null);
				PlayerController.Instance.ActivateTool(SelectTool.Instance);
			}
		}
	}

	// Token: 0x06005A85 RID: 23173 RVA: 0x0020D3B8 File Offset: 0x0020B5B8
	public void CloseRecipe(bool playSound = false)
	{
		if (playSound)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Deselect", false));
		}
		if (PlayerController.Instance.ActiveTool is PrebuildTool || PlayerController.Instance.ActiveTool is BuildTool)
		{
			ToolMenu.Instance.ClearSelection();
		}
		this.DeactivateBuildTools();
		if (this.ProductInfoScreen != null)
		{
			this.ProductInfoScreen.ClearProduct(true);
		}
		if (this.activeCategoryInfo != null)
		{
			this.UpdateBuildingButtonList(this.activeCategoryInfo);
		}
		this.SelectedBuildingGameObject = null;
	}

	// Token: 0x06005A86 RID: 23174 RVA: 0x0020D440 File Offset: 0x0020B640
	public void SoftCloseRecipe()
	{
		this.ignoreToolChangeMessages++;
		if (PlayerController.Instance.ActiveTool is PrebuildTool || PlayerController.Instance.ActiveTool is BuildTool)
		{
			ToolMenu.Instance.ClearSelection();
		}
		this.DeactivateBuildTools();
		if (this.ProductInfoScreen != null)
		{
			this.ProductInfoScreen.ClearProduct(true);
		}
		this.currentlySelectedToggle = null;
		this.SelectedBuildingGameObject = null;
		this.ignoreToolChangeMessages--;
	}

	// Token: 0x06005A87 RID: 23175 RVA: 0x0020D4C4 File Offset: 0x0020B6C4
	public void CloseCategoryPanel(bool playSound = true)
	{
		this.activeCategoryInfo = null;
		if (playSound)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Close", false));
		}
		this.buildingGroupsRoot.GetComponent<ExpandRevealUIContent>().Collapse(delegate(object s)
		{
			this.ClearButtons();
			this.buildingGroupsRoot.gameObject.SetActive(false);
			this.ForceUpdateAllCategoryToggles(null);
		});
		this.PlanCategoryLabel.text = "";
		this.ForceUpdateAllCategoryToggles(null);
	}

	// Token: 0x06005A88 RID: 23176 RVA: 0x0020D520 File Offset: 0x0020B720
	private void OnClickCategory(KIconToggleMenu.ToggleInfo toggle_info)
	{
		this.CloseRecipe(false);
		if (!this.CategoryInteractive.ContainsKey(toggle_info) || !this.CategoryInteractive[toggle_info])
		{
			this.CloseCategoryPanel(false);
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Negative", false));
			return;
		}
		if (this.activeCategoryInfo == toggle_info)
		{
			this.CloseCategoryPanel(true);
		}
		else
		{
			this.OpenCategoryPanel(toggle_info, true);
		}
		this.ConfigurePanelSize(null);
		this.SetScrollPoint(0f);
	}

	// Token: 0x06005A89 RID: 23177 RVA: 0x0020D594 File Offset: 0x0020B794
	private void OpenCategoryPanel(KIconToggleMenu.ToggleInfo toggle_info, bool play_sound = true)
	{
		HashedString hashedString = (HashedString)toggle_info.userData;
		if (BuildingGroupScreen.Instance != null)
		{
			BuildingGroupScreen.Instance.ClearSearch();
		}
		this.ClearButtons();
		this.buildingGroupsRoot.gameObject.SetActive(true);
		this.activeCategoryInfo = toggle_info;
		if (play_sound)
		{
			UISounds.PlaySound(UISounds.Sound.ClickObject);
		}
		this.BuildButtonList();
		this.ForceRefreshAllBuildingToggles();
		this.UpdateBuildingButtonList(this.activeCategoryInfo);
		this.RefreshCategoryPanelTitle();
		this.ForceUpdateAllCategoryToggles(null);
		this.buildingGroupsRoot.GetComponent<ExpandRevealUIContent>().Expand(null);
	}

	// Token: 0x06005A8A RID: 23178 RVA: 0x0020D624 File Offset: 0x0020B824
	public void RefreshCategoryPanelTitle()
	{
		if (this.activeCategoryInfo != null)
		{
			this.PlanCategoryLabel.text = this.activeCategoryInfo.text.ToUpper();
		}
		if (!BuildingGroupScreen.SearchIsEmpty)
		{
			this.PlanCategoryLabel.text = UI.BUILDMENU.SEARCH_RESULTS_HEADER;
		}
	}

	// Token: 0x06005A8B RID: 23179 RVA: 0x0020D670 File Offset: 0x0020B870
	public void OpenCategoryByName(string category)
	{
		PlanScreen.ToggleEntry toggleEntry;
		if (this.GetToggleEntryForCategory(category, out toggleEntry))
		{
			this.OpenCategoryPanel(toggleEntry.toggleInfo, false);
			this.ConfigurePanelSize(null);
		}
	}

	// Token: 0x06005A8C RID: 23180 RVA: 0x0020D6A4 File Offset: 0x0020B8A4
	private void UpdateBuildingButtonList(KIconToggleMenu.ToggleInfo toggle_info)
	{
		KToggle ktoggle = toggle_info.toggle;
		if (ktoggle == null)
		{
			foreach (KIconToggleMenu.ToggleInfo toggleInfo in this.toggleInfo)
			{
				if (toggleInfo.userData == toggle_info.userData)
				{
					ktoggle = toggleInfo.toggle;
					break;
				}
			}
		}
		if (ktoggle != null && this.allBuildingToggles.Count != 0)
		{
			if (this.forceRefreshAllBuildings)
			{
				for (int i = 0; i < this.allBuildingToggles.Count; i++)
				{
					PlanBuildingToggle value = this.allBuildingToggles.ElementAt(i).Value;
					this.categoryPanelSizeNeedsRefresh = value.Refresh() || this.categoryPanelSizeNeedsRefresh;
					this.forceRefreshAllBuildings = false;
					value.SwitchViewMode(this.useSubCategoryLayout);
				}
			}
			else
			{
				for (int j = 0; j < this.maxToggleRefreshPerFrame; j++)
				{
					if (this.building_button_refresh_idx >= this.allBuildingToggles.Count)
					{
						this.building_button_refresh_idx = 0;
					}
					PlanBuildingToggle value2 = this.allBuildingToggles.ElementAt(this.building_button_refresh_idx).Value;
					this.categoryPanelSizeNeedsRefresh = value2.Refresh() || this.categoryPanelSizeNeedsRefresh;
					value2.SwitchViewMode(this.useSubCategoryLayout);
					this.building_button_refresh_idx++;
				}
			}
		}
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.allSubCategoryObjects)
		{
			GridLayoutGroup componentInChildren = keyValuePair.Value.GetComponentInChildren<GridLayoutGroup>(true);
			if (!(componentInChildren == null))
			{
				int num = 0;
				for (int k = 0; k < componentInChildren.transform.childCount; k++)
				{
					if (componentInChildren.transform.GetChild(k).gameObject.activeSelf)
					{
						num++;
					}
				}
				if (keyValuePair.Value.gameObject.activeSelf != num > 0)
				{
					keyValuePair.Value.gameObject.SetActive(num > 0);
				}
			}
		}
		if (this.categoryPanelSizeNeedsRefresh && this.building_button_refresh_idx >= this.activeCategoryBuildingToggles.Count)
		{
			this.categoryPanelSizeNeedsRefresh = false;
			this.ConfigurePanelSize(null);
		}
		if (this.ProductInfoScreen.gameObject.activeSelf)
		{
			this.ProductInfoScreen.materialSelectionPanel.UpdateResourceToggleValues();
		}
	}

	// Token: 0x06005A8D RID: 23181 RVA: 0x0020D924 File Offset: 0x0020BB24
	public override void ScreenUpdate(bool topLevel)
	{
		base.ScreenUpdate(topLevel);
		this.RefreshBuildableStates(false);
		this.SetCategoryButtonState();
		if (this.activeCategoryInfo != null)
		{
			this.UpdateBuildingButtonList(this.activeCategoryInfo);
		}
	}

	// Token: 0x06005A8E RID: 23182 RVA: 0x0020D950 File Offset: 0x0020BB50
	private void BuildButtonList()
	{
		this.activeCategoryBuildingToggles.Clear();
		Dictionary<string, HashedString> dictionary = new Dictionary<string, HashedString>();
		Dictionary<string, List<BuildingDef>> dictionary2 = new Dictionary<string, List<BuildingDef>>();
		if (!dictionary2.ContainsKey("default"))
		{
			dictionary2.Add("default", new List<BuildingDef>());
		}
		foreach (PlanScreen.PlanInfo planInfo in TUNING.BUILDINGS.PLANORDER)
		{
			foreach (KeyValuePair<string, string> keyValuePair in planInfo.buildingAndSubcategoryData)
			{
				BuildingDef buildingDef = Assets.GetBuildingDef(keyValuePair.Key);
				if (buildingDef.IsAvailable() && buildingDef.ShouldShowInBuildMenu())
				{
					dictionary.Add(buildingDef.PrefabID, planInfo.category);
					if (!dictionary2.ContainsKey(keyValuePair.Value))
					{
						dictionary2.Add(keyValuePair.Value, new List<BuildingDef>());
					}
					dictionary2[keyValuePair.Value].Add(buildingDef);
				}
			}
		}
		if (!this.allSubCategoryObjects.ContainsKey("default"))
		{
			this.allSubCategoryObjects.Add("default", global::Util.KInstantiateUI(this.subgroupPrefab, this.GroupsTransform.gameObject, true));
		}
		GameObject gameObject = this.allSubCategoryObjects["default"].GetComponent<HierarchyReferences>().GetReference<GridLayoutGroup>("Grid").gameObject;
		foreach (KeyValuePair<string, List<BuildingDef>> keyValuePair2 in dictionary2)
		{
			if (!this.allSubCategoryObjects.ContainsKey(keyValuePair2.Key))
			{
				this.allSubCategoryObjects.Add(keyValuePair2.Key, global::Util.KInstantiateUI(this.subgroupPrefab, this.GroupsTransform.gameObject, true));
			}
			if (keyValuePair2.Key == "default")
			{
				this.allSubCategoryObjects[keyValuePair2.Key].SetActive(this.useSubCategoryLayout);
			}
			HierarchyReferences component = this.allSubCategoryObjects[keyValuePair2.Key].GetComponent<HierarchyReferences>();
			GameObject gameObject2;
			if (this.useSubCategoryLayout)
			{
				component.GetReference<RectTransform>("Header").gameObject.SetActive(true);
				gameObject2 = this.allSubCategoryObjects[keyValuePair2.Key].GetComponent<HierarchyReferences>().GetReference<GridLayoutGroup>("Grid").gameObject;
				StringEntry stringEntry;
				if (Strings.TryGet("STRINGS.UI.NEWBUILDCATEGORIES." + keyValuePair2.Key.ToUpper() + ".BUILDMENUTITLE", out stringEntry))
				{
					component.GetReference<LocText>("HeaderLabel").SetText(stringEntry);
				}
			}
			else
			{
				component.GetReference<RectTransform>("Header").gameObject.SetActive(false);
				gameObject2 = gameObject;
			}
			foreach (BuildingDef buildingDef2 in keyValuePair2.Value)
			{
				HashedString hashedString = dictionary[buildingDef2.PrefabID];
				GameObject gameObject3 = this.CreateButton(buildingDef2, gameObject2, hashedString);
				PlanScreen.ToggleEntry toggleEntry = null;
				this.GetToggleEntryForCategory(hashedString, out toggleEntry);
				if (toggleEntry != null && toggleEntry.pendingResearchAttentions.Contains(buildingDef2.PrefabID))
				{
					gameObject3.GetComponent<PlanCategoryNotifications>().ToggleAttention(true);
				}
			}
		}
		this.RefreshScale(null);
	}

	// Token: 0x06005A8F RID: 23183 RVA: 0x0020DD0C File Offset: 0x0020BF0C
	public void ConfigurePanelSize(object data = null)
	{
		if (this.useSubCategoryLayout)
		{
			this.buildGrid_bg_rowHeight = 48f;
		}
		else
		{
			this.buildGrid_bg_rowHeight = (ScreenResolutionMonitor.UsingGamepadUIMode() ? PlanScreen.bigBuildingButtonSize.y : PlanScreen.standarduildingButtonSize.y);
		}
		GridLayoutGroup reference = this.subgroupPrefab.GetComponent<HierarchyReferences>().GetReference<GridLayoutGroup>("Grid");
		this.buildGrid_bg_rowHeight += reference.spacing.y;
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < this.GroupsTransform.childCount; i++)
		{
			int num3 = 0;
			HierarchyReferences component = this.GroupsTransform.GetChild(i).GetComponent<HierarchyReferences>();
			if (!(component == null))
			{
				GridLayoutGroup reference2 = component.GetReference<GridLayoutGroup>("Grid");
				if (!(reference2 == null))
				{
					bool flag = false;
					for (int j = 0; j < reference2.transform.childCount; j++)
					{
						if (reference2.transform.GetChild(j).gameObject.activeSelf)
						{
							flag = true;
							num3++;
						}
					}
					if (flag)
					{
						num2 += 24;
					}
					num += num3 / reference2.constraintCount;
					if (num3 % reference2.constraintCount != 0)
					{
						num++;
					}
				}
			}
		}
		num2 = Math.Min(72, num2);
		this.noResultMessage.SetActive(num == 0);
		int num4 = num;
		int num5 = Math.Max(1, Screen.height / (int)this.buildGrid_bg_rowHeight - 3);
		num5 = Math.Min(num5, this.useSubCategoryLayout ? 12 : 6);
		if (BuildingGroupScreen.IsEditing || !BuildingGroupScreen.SearchIsEmpty)
		{
			num4 = Mathf.Min(num5, this.useSubCategoryLayout ? 8 : 4);
		}
		this.BuildingGroupContentsRect.GetComponent<ScrollRect>().verticalScrollbar.gameObject.SetActive(num4 >= num5 - 1);
		float num6 = this.buildGrid_bg_borderHeight + (float)num2 + 36f + (float)Mathf.Clamp(num4, 0, num5) * this.buildGrid_bg_rowHeight;
		if (BuildingGroupScreen.IsEditing || !BuildingGroupScreen.SearchIsEmpty)
		{
			num6 = Mathf.Max(num6, this.buildingGroupsRoot.sizeDelta.y);
		}
		this.buildingGroupsRoot.sizeDelta = new Vector2(this.buildGrid_bg_width, num6);
		this.RefreshScale(null);
	}

	// Token: 0x06005A90 RID: 23184 RVA: 0x0020DF39 File Offset: 0x0020C139
	private void SetScrollPoint(float targetY)
	{
		this.BuildingGroupContentsRect.anchoredPosition = new Vector2(this.BuildingGroupContentsRect.anchoredPosition.x, targetY);
	}

	// Token: 0x06005A91 RID: 23185 RVA: 0x0020DF5C File Offset: 0x0020C15C
	private GameObject CreateButton(BuildingDef def, GameObject parent, HashedString plan_category)
	{
		GameObject gameObject;
		PlanBuildingToggle planBuildingToggle;
		if (this.allBuildingToggles.ContainsKey(def.PrefabID))
		{
			gameObject = this.allBuildingToggles[def.PrefabID].gameObject;
			planBuildingToggle = this.allBuildingToggles[def.PrefabID];
			planBuildingToggle.Refresh();
		}
		else
		{
			gameObject = global::Util.KInstantiateUI(this.planButtonPrefab, parent, false);
			gameObject.name = UI.StripLinkFormatting(def.name) + " Group:" + plan_category.ToString();
			planBuildingToggle = gameObject.GetComponentInChildren<PlanBuildingToggle>();
			planBuildingToggle.Config(def, this, plan_category);
			planBuildingToggle.soundPlayer.Enabled = false;
			planBuildingToggle.SwitchViewMode(this.useSubCategoryLayout);
			this.allBuildingToggles.Add(def.PrefabID, planBuildingToggle);
		}
		if (gameObject.transform.parent != parent)
		{
			gameObject.transform.SetParent(parent.transform);
		}
		this.activeCategoryBuildingToggles.Add(def, planBuildingToggle);
		return gameObject;
	}

	// Token: 0x06005A92 RID: 23186 RVA: 0x0020E054 File Offset: 0x0020C254
	public static bool TechRequirementsMet(TechItem techItem)
	{
		return DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive || techItem == null || techItem.IsComplete();
	}

	// Token: 0x06005A93 RID: 23187 RVA: 0x0020E074 File Offset: 0x0020C274
	private static bool TechRequirementsUpcoming(TechItem techItem)
	{
		return PlanScreen.TechRequirementsMet(techItem);
	}

	// Token: 0x06005A94 RID: 23188 RVA: 0x0020E07C File Offset: 0x0020C27C
	private bool GetToggleEntryForCategory(HashedString category, out PlanScreen.ToggleEntry toggleEntry)
	{
		toggleEntry = null;
		foreach (PlanScreen.ToggleEntry toggleEntry2 in this.toggleEntries)
		{
			if (toggleEntry2.planCategory == category)
			{
				toggleEntry = toggleEntry2;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06005A95 RID: 23189 RVA: 0x0020E0E4 File Offset: 0x0020C2E4
	public bool IsDefBuildable(BuildingDef def)
	{
		return this.GetBuildableState(def) == PlanScreen.RequirementsState.Complete;
	}

	// Token: 0x06005A96 RID: 23190 RVA: 0x0020E0F0 File Offset: 0x0020C2F0
	public string GetTooltipForBuildable(BuildingDef def)
	{
		PlanScreen.RequirementsState buildableState = this.GetBuildableState(def);
		return PlanScreen.GetTooltipForRequirementsState(def, buildableState);
	}

	// Token: 0x06005A97 RID: 23191 RVA: 0x0020E10C File Offset: 0x0020C30C
	public static string GetTooltipForRequirementsState(BuildingDef def, PlanScreen.RequirementsState state)
	{
		TechItem techItem = Db.Get().TechItems.TryGet(def.PrefabID);
		string text = null;
		if (Game.Instance.SandboxModeActive)
		{
			text = UIConstants.ColorPrefixYellow + UI.SANDBOXTOOLS.SETTINGS.INSTANT_BUILD.NAME + UIConstants.ColorSuffix;
		}
		else if (DebugHandler.InstantBuildMode)
		{
			text = UIConstants.ColorPrefixYellow + UI.DEBUG_TOOLS.DEBUG_ACTIVE + UIConstants.ColorSuffix;
		}
		else
		{
			switch (state)
			{
			case PlanScreen.RequirementsState.Tech:
				text = string.Format(UI.PRODUCTINFO_REQUIRESRESEARCHDESC, techItem.ParentTech.Name);
				break;
			case PlanScreen.RequirementsState.Materials:
				text = UI.PRODUCTINFO_MISSINGRESOURCES_HOVER;
				foreach (Recipe.Ingredient ingredient in def.CraftRecipe.Ingredients)
				{
					string text2 = string.Format("{0}{1}: {2}", "• ", ingredient.tag.ProperName(), GameUtil.GetFormattedMass(ingredient.amount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
					text = text + "\n" + text2;
				}
				break;
			case PlanScreen.RequirementsState.TelepadBuilt:
				text = UI.PRODUCTINFO_UNIQUE_PER_WORLD;
				break;
			case PlanScreen.RequirementsState.UniquePerWorld:
				text = UI.PRODUCTINFO_UNIQUE_PER_WORLD;
				break;
			case PlanScreen.RequirementsState.RocketInteriorOnly:
				text = UI.PRODUCTINFO_ROCKET_INTERIOR;
				break;
			case PlanScreen.RequirementsState.RocketInteriorForbidden:
				text = UI.PRODUCTINFO_ROCKET_NOT_INTERIOR;
				break;
			}
		}
		return text;
	}

	// Token: 0x06005A98 RID: 23192 RVA: 0x0020E298 File Offset: 0x0020C498
	private void PointerEnter(PointerEventData data)
	{
		this.planScreenScrollRect.mouseIsOver = true;
	}

	// Token: 0x06005A99 RID: 23193 RVA: 0x0020E2A6 File Offset: 0x0020C4A6
	private void PointerExit(PointerEventData data)
	{
		this.planScreenScrollRect.mouseIsOver = false;
	}

	// Token: 0x06005A9A RID: 23194 RVA: 0x0020E2B4 File Offset: 0x0020C4B4
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.Consumed)
		{
			return;
		}
		if (this.mouseOver && base.ConsumeMouseScroll)
		{
			if (KInputManager.currentControllerIsGamepad)
			{
				if (e.IsAction(global::Action.ZoomIn) || e.IsAction(global::Action.ZoomOut))
				{
					this.planScreenScrollRect.OnKeyDown(e);
				}
			}
			else if (!e.TryConsume(global::Action.ZoomIn))
			{
				e.TryConsume(global::Action.ZoomOut);
			}
		}
		if (e.IsAction(global::Action.CopyBuilding) && e.TryConsume(global::Action.CopyBuilding))
		{
			this.OnClickCopyBuilding();
		}
		if (this.toggles == null)
		{
			return;
		}
		if (!e.Consumed && this.activeCategoryInfo != null && e.TryConsume(global::Action.Escape))
		{
			this.OnClickCategory(this.activeCategoryInfo);
			SelectTool.Instance.Activate();
			this.ClearSelection();
			return;
		}
		if (!e.Consumed)
		{
			base.OnKeyDown(e);
		}
	}

	// Token: 0x06005A9B RID: 23195 RVA: 0x0020E37C File Offset: 0x0020C57C
	public override void OnKeyUp(KButtonEvent e)
	{
		if (this.mouseOver && base.ConsumeMouseScroll)
		{
			if (KInputManager.currentControllerIsGamepad)
			{
				if (e.IsAction(global::Action.ZoomIn) || e.IsAction(global::Action.ZoomOut))
				{
					this.planScreenScrollRect.OnKeyUp(e);
				}
			}
			else if (!e.TryConsume(global::Action.ZoomIn))
			{
				e.TryConsume(global::Action.ZoomOut);
			}
		}
		if (e.Consumed)
		{
			return;
		}
		if (this.SelectedBuildingGameObject != null && PlayerController.Instance.ConsumeIfNotDragging(e, global::Action.MouseRight))
		{
			this.CloseRecipe(false);
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Close", false));
		}
		else if (this.activeCategoryInfo != null && PlayerController.Instance.ConsumeIfNotDragging(e, global::Action.MouseRight))
		{
			this.OnUIClear(null);
		}
		if (!e.Consumed)
		{
			base.OnKeyUp(e);
		}
	}

	// Token: 0x06005A9C RID: 23196 RVA: 0x0020E43C File Offset: 0x0020C63C
	private void OnRecipeElementsFullySelected()
	{
		BuildingDef buildingDef = null;
		foreach (KeyValuePair<string, PlanBuildingToggle> keyValuePair in this.allBuildingToggles)
		{
			if (keyValuePair.Value == this.currentlySelectedToggle)
			{
				buildingDef = Assets.GetBuildingDef(keyValuePair.Key);
				break;
			}
		}
		DebugUtil.DevAssert(buildingDef, "def is null", null);
		if (buildingDef)
		{
			if (buildingDef.isKAnimTile && buildingDef.isUtility)
			{
				IList<Tag> getSelectedElementAsList = this.ProductInfoScreen.materialSelectionPanel.GetSelectedElementAsList;
				((buildingDef.BuildingComplete.GetComponent<Wire>() != null) ? WireBuildTool.Instance : UtilityBuildTool.Instance).Activate(buildingDef, getSelectedElementAsList);
				return;
			}
			BuildTool.Instance.Activate(buildingDef, this.ProductInfoScreen.materialSelectionPanel.GetSelectedElementAsList, this.ProductInfoScreen.FacadeSelectionPanel.SelectedFacade);
		}
	}

	// Token: 0x06005A9D RID: 23197 RVA: 0x0020E53C File Offset: 0x0020C73C
	public void OnResearchComplete(object tech)
	{
		foreach (TechItem techItem in ((Tech)tech).unlockedItems)
		{
			BuildingDef buildingDef = Assets.GetBuildingDef(techItem.Id);
			if (buildingDef != null)
			{
				this.UpdateDefResearched(buildingDef);
				if (this.tagCategoryMap.ContainsKey(buildingDef.Tag))
				{
					HashedString hashedString = this.tagCategoryMap[buildingDef.Tag];
					PlanScreen.ToggleEntry toggleEntry;
					if (this.GetToggleEntryForCategory(hashedString, out toggleEntry))
					{
						toggleEntry.pendingResearchAttentions.Add(buildingDef.Tag);
						toggleEntry.toggleInfo.toggle.GetComponent<PlanCategoryNotifications>().ToggleAttention(true);
						toggleEntry.Refresh();
					}
				}
			}
		}
	}

	// Token: 0x06005A9E RID: 23198 RVA: 0x0020E60C File Offset: 0x0020C80C
	private void OnUIClear(object data)
	{
		if (this.activeCategoryInfo != null)
		{
			this.selected = -1;
			this.OnClickCategory(this.activeCategoryInfo);
			SelectTool.Instance.Activate();
			PlayerController.Instance.ActivateTool(SelectTool.Instance);
			SelectTool.Instance.Select(null, true);
		}
	}

	// Token: 0x06005A9F RID: 23199 RVA: 0x0020E65C File Offset: 0x0020C85C
	private void OnActiveToolChanged(object data)
	{
		if (data == null)
		{
			return;
		}
		if (this.ignoreToolChangeMessages > 0)
		{
			return;
		}
		Type type = data.GetType();
		if (!typeof(BuildTool).IsAssignableFrom(type) && !typeof(PrebuildTool).IsAssignableFrom(type) && !typeof(BaseUtilityBuildTool).IsAssignableFrom(type))
		{
			this.CloseRecipe(false);
			this.CloseCategoryPanel(false);
		}
	}

	// Token: 0x06005AA0 RID: 23200 RVA: 0x0020E6C2 File Offset: 0x0020C8C2
	public PrioritySetting GetBuildingPriority()
	{
		return this.ProductInfoScreen.materialSelectionPanel.PriorityScreen.GetLastSelectedPriority();
	}

	// Token: 0x04003D16 RID: 15638
	[SerializeField]
	private GameObject planButtonPrefab;

	// Token: 0x04003D17 RID: 15639
	[SerializeField]
	private GameObject recipeInfoScreenParent;

	// Token: 0x04003D18 RID: 15640
	[SerializeField]
	private GameObject productInfoScreenPrefab;

	// Token: 0x04003D19 RID: 15641
	[SerializeField]
	private GameObject copyBuildingButton;

	// Token: 0x04003D1A RID: 15642
	[SerializeField]
	private KButton gridViewButton;

	// Token: 0x04003D1B RID: 15643
	[SerializeField]
	private KButton listViewButton;

	// Token: 0x04003D1C RID: 15644
	private bool useSubCategoryLayout;

	// Token: 0x04003D1D RID: 15645
	private int refreshScaleHandle = -1;

	// Token: 0x04003D1E RID: 15646
	[SerializeField]
	private GameObject adjacentPinnedButtons;

	// Token: 0x04003D1F RID: 15647
	private static Dictionary<HashedString, string> iconNameMap = new Dictionary<HashedString, string>
	{
		{
			PlanScreen.CacheHashedString("Base"),
			"icon_category_base"
		},
		{
			PlanScreen.CacheHashedString("Oxygen"),
			"icon_category_oxygen"
		},
		{
			PlanScreen.CacheHashedString("Power"),
			"icon_category_electrical"
		},
		{
			PlanScreen.CacheHashedString("Food"),
			"icon_category_food"
		},
		{
			PlanScreen.CacheHashedString("Plumbing"),
			"icon_category_plumbing"
		},
		{
			PlanScreen.CacheHashedString("HVAC"),
			"icon_category_ventilation"
		},
		{
			PlanScreen.CacheHashedString("Refining"),
			"icon_category_refinery"
		},
		{
			PlanScreen.CacheHashedString("Medical"),
			"icon_category_medical"
		},
		{
			PlanScreen.CacheHashedString("Furniture"),
			"icon_category_furniture"
		},
		{
			PlanScreen.CacheHashedString("Equipment"),
			"icon_category_misc"
		},
		{
			PlanScreen.CacheHashedString("Utilities"),
			"icon_category_utilities"
		},
		{
			PlanScreen.CacheHashedString("Automation"),
			"icon_category_automation"
		},
		{
			PlanScreen.CacheHashedString("Conveyance"),
			"icon_category_shipping"
		},
		{
			PlanScreen.CacheHashedString("Rocketry"),
			"icon_category_rocketry"
		},
		{
			PlanScreen.CacheHashedString("HEP"),
			"icon_category_radiation"
		}
	};

	// Token: 0x04003D20 RID: 15648
	private Dictionary<KIconToggleMenu.ToggleInfo, bool> CategoryInteractive = new Dictionary<KIconToggleMenu.ToggleInfo, bool>();

	// Token: 0x04003D22 RID: 15650
	[SerializeField]
	public PlanScreen.BuildingToolTipSettings buildingToolTipSettings;

	// Token: 0x04003D23 RID: 15651
	public PlanScreen.BuildingNameTextSetting buildingNameTextSettings;

	// Token: 0x04003D24 RID: 15652
	private KIconToggleMenu.ToggleInfo activeCategoryInfo;

	// Token: 0x04003D25 RID: 15653
	public Dictionary<BuildingDef, PlanBuildingToggle> activeCategoryBuildingToggles = new Dictionary<BuildingDef, PlanBuildingToggle>();

	// Token: 0x04003D26 RID: 15654
	private float timeSinceNotificationPing;

	// Token: 0x04003D27 RID: 15655
	private float notificationPingExpire = 0.5f;

	// Token: 0x04003D28 RID: 15656
	private float specialNotificationEmbellishDelay = 8f;

	// Token: 0x04003D29 RID: 15657
	private int notificationPingCount;

	// Token: 0x04003D2A RID: 15658
	public const string DEFAULT_SUBCATEGORY_KEY = "default";

	// Token: 0x04003D2B RID: 15659
	private Dictionary<string, GameObject> allSubCategoryObjects = new Dictionary<string, GameObject>();

	// Token: 0x04003D2C RID: 15660
	private Dictionary<string, PlanBuildingToggle> allBuildingToggles = new Dictionary<string, PlanBuildingToggle>();

	// Token: 0x04003D2D RID: 15661
	private static Vector2 bigBuildingButtonSize = new Vector2(98f, 123f);

	// Token: 0x04003D2E RID: 15662
	private static Vector2 standarduildingButtonSize = PlanScreen.bigBuildingButtonSize * 0.8f;

	// Token: 0x04003D2F RID: 15663
	public static int fontSizeBigMode = 16;

	// Token: 0x04003D30 RID: 15664
	public static int fontSizeStandardMode = 14;

	// Token: 0x04003D32 RID: 15666
	[SerializeField]
	private GameObject subgroupPrefab;

	// Token: 0x04003D33 RID: 15667
	public Transform GroupsTransform;

	// Token: 0x04003D34 RID: 15668
	public Sprite Overlay_NeedTech;

	// Token: 0x04003D35 RID: 15669
	public RectTransform buildingGroupsRoot;

	// Token: 0x04003D36 RID: 15670
	public RectTransform BuildButtonBGPanel;

	// Token: 0x04003D37 RID: 15671
	public RectTransform BuildingGroupContentsRect;

	// Token: 0x04003D38 RID: 15672
	public Sprite defaultBuildingIconSprite;

	// Token: 0x04003D39 RID: 15673
	private KScrollRect planScreenScrollRect;

	// Token: 0x04003D3A RID: 15674
	public Material defaultUIMaterial;

	// Token: 0x04003D3B RID: 15675
	public Material desaturatedUIMaterial;

	// Token: 0x04003D3C RID: 15676
	public LocText PlanCategoryLabel;

	// Token: 0x04003D3D RID: 15677
	public GameObject noResultMessage;

	// Token: 0x04003D3E RID: 15678
	private int nextCategoryToUpdateIDX = -1;

	// Token: 0x04003D3F RID: 15679
	private bool forceUpdateAllCategoryToggles;

	// Token: 0x04003D40 RID: 15680
	private bool forceRefreshAllBuildings = true;

	// Token: 0x04003D41 RID: 15681
	private List<PlanScreen.ToggleEntry> toggleEntries = new List<PlanScreen.ToggleEntry>();

	// Token: 0x04003D42 RID: 15682
	private int ignoreToolChangeMessages;

	// Token: 0x04003D43 RID: 15683
	private Dictionary<string, PlanScreen.RequirementsState> _buildableStatesByID = new Dictionary<string, PlanScreen.RequirementsState>();

	// Token: 0x04003D44 RID: 15684
	private Dictionary<Def, bool> _researchedDefs = new Dictionary<Def, bool>();

	// Token: 0x04003D45 RID: 15685
	[SerializeField]
	private TextStyleSetting[] CategoryLabelTextStyles;

	// Token: 0x04003D46 RID: 15686
	private float initTime;

	// Token: 0x04003D47 RID: 15687
	private Dictionary<Tag, HashedString> tagCategoryMap;

	// Token: 0x04003D48 RID: 15688
	private Dictionary<Tag, int> tagOrderMap;

	// Token: 0x04003D49 RID: 15689
	private BuildingDef lastSelectedBuildingDef;

	// Token: 0x04003D4A RID: 15690
	private Building lastSelectedBuilding;

	// Token: 0x04003D4B RID: 15691
	private string lastSelectedBuildingFacade = "DEFAULT_FACADE";

	// Token: 0x04003D4C RID: 15692
	private int buildable_state_update_idx;

	// Token: 0x04003D4D RID: 15693
	private int building_button_refresh_idx;

	// Token: 0x04003D4E RID: 15694
	private int maxToggleRefreshPerFrame = 10;

	// Token: 0x04003D4F RID: 15695
	private bool categoryPanelSizeNeedsRefresh;

	// Token: 0x04003D50 RID: 15696
	private float buildGrid_bg_width = 320f;

	// Token: 0x04003D51 RID: 15697
	private float buildGrid_bg_borderHeight = 48f;

	// Token: 0x04003D52 RID: 15698
	private const float BUILDGRID_SEARCHBAR_HEIGHT = 36f;

	// Token: 0x04003D53 RID: 15699
	private const int SUBCATEGORY_HEADER_HEIGHT = 24;

	// Token: 0x04003D54 RID: 15700
	private float buildGrid_bg_rowHeight;

	// Token: 0x02001A01 RID: 6657
	public struct PlanInfo
	{
		// Token: 0x060091E1 RID: 37345 RVA: 0x00315EDC File Offset: 0x003140DC
		public PlanInfo(HashedString category, bool hideIfNotResearched, List<string> listData, string RequiredDlcId = "")
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			foreach (string text in listData)
			{
				list.Add(new KeyValuePair<string, string>(text, TUNING.BUILDINGS.PLANSUBCATEGORYSORTING.ContainsKey(text) ? TUNING.BUILDINGS.PLANSUBCATEGORYSORTING[text] : "uncategorized"));
			}
			this.category = category;
			this.hideIfNotResearched = hideIfNotResearched;
			this.data = listData;
			this.buildingAndSubcategoryData = list;
			this.RequiredDlcId = RequiredDlcId;
		}

		// Token: 0x0400761C RID: 30236
		public HashedString category;

		// Token: 0x0400761D RID: 30237
		public bool hideIfNotResearched;

		// Token: 0x0400761E RID: 30238
		[Obsolete("Modders: Use ModUtil.AddBuildingToPlanScreen")]
		public List<string> data;

		// Token: 0x0400761F RID: 30239
		public List<KeyValuePair<string, string>> buildingAndSubcategoryData;

		// Token: 0x04007620 RID: 30240
		public string RequiredDlcId;
	}

	// Token: 0x02001A02 RID: 6658
	[Serializable]
	public struct BuildingToolTipSettings
	{
		// Token: 0x04007621 RID: 30241
		public TextStyleSetting BuildButtonName;

		// Token: 0x04007622 RID: 30242
		public TextStyleSetting BuildButtonDescription;

		// Token: 0x04007623 RID: 30243
		public TextStyleSetting MaterialRequirement;

		// Token: 0x04007624 RID: 30244
		public TextStyleSetting ResearchRequirement;
	}

	// Token: 0x02001A03 RID: 6659
	[Serializable]
	public struct BuildingNameTextSetting
	{
		// Token: 0x04007625 RID: 30245
		public TextStyleSetting ActiveSelected;

		// Token: 0x04007626 RID: 30246
		public TextStyleSetting ActiveDeselected;

		// Token: 0x04007627 RID: 30247
		public TextStyleSetting InactiveSelected;

		// Token: 0x04007628 RID: 30248
		public TextStyleSetting InactiveDeselected;
	}

	// Token: 0x02001A04 RID: 6660
	private class ToggleEntry
	{
		// Token: 0x060091E2 RID: 37346 RVA: 0x00315F78 File Offset: 0x00314178
		public ToggleEntry(KIconToggleMenu.ToggleInfo toggle_info, HashedString plan_category, List<BuildingDef> building_defs, bool hideIfNotResearched)
		{
			this.toggleInfo = toggle_info;
			this.planCategory = plan_category;
			this.buildingDefs = building_defs;
			this.hideIfNotResearched = hideIfNotResearched;
			this.pendingResearchAttentions = new List<Tag>();
			this.requiredTechItems = new List<TechItem>();
			this.toggleImages = null;
			foreach (BuildingDef buildingDef in building_defs)
			{
				TechItem techItem = Db.Get().TechItems.TryGet(buildingDef.PrefabID);
				if (techItem == null)
				{
					this.requiredTechItems.Clear();
					break;
				}
				if (!this.requiredTechItems.Contains(techItem))
				{
					this.requiredTechItems.Add(techItem);
				}
			}
			this._areAnyRequiredTechItemsAvailable = false;
			this.Refresh();
		}

		// Token: 0x060091E3 RID: 37347 RVA: 0x0031604C File Offset: 0x0031424C
		public bool AreAnyRequiredTechItemsAvailable()
		{
			return this._areAnyRequiredTechItemsAvailable;
		}

		// Token: 0x060091E4 RID: 37348 RVA: 0x00316054 File Offset: 0x00314254
		public void Refresh()
		{
			if (this._areAnyRequiredTechItemsAvailable)
			{
				return;
			}
			if (this.requiredTechItems.Count == 0)
			{
				this._areAnyRequiredTechItemsAvailable = true;
				return;
			}
			using (List<TechItem>.Enumerator enumerator = this.requiredTechItems.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (PlanScreen.TechRequirementsUpcoming(enumerator.Current))
					{
						this._areAnyRequiredTechItemsAvailable = true;
						break;
					}
				}
			}
		}

		// Token: 0x060091E5 RID: 37349 RVA: 0x003160D0 File Offset: 0x003142D0
		public void CollectToggleImages()
		{
			this.toggleImages = this.toggleInfo.toggle.gameObject.GetComponents<ImageToggleState>();
		}

		// Token: 0x04007629 RID: 30249
		public KIconToggleMenu.ToggleInfo toggleInfo;

		// Token: 0x0400762A RID: 30250
		public HashedString planCategory;

		// Token: 0x0400762B RID: 30251
		public List<BuildingDef> buildingDefs;

		// Token: 0x0400762C RID: 30252
		public List<Tag> pendingResearchAttentions;

		// Token: 0x0400762D RID: 30253
		private List<TechItem> requiredTechItems;

		// Token: 0x0400762E RID: 30254
		public ImageToggleState[] toggleImages;

		// Token: 0x0400762F RID: 30255
		public bool hideIfNotResearched;

		// Token: 0x04007630 RID: 30256
		private bool _areAnyRequiredTechItemsAvailable;
	}

	// Token: 0x02001A05 RID: 6661
	public enum RequirementsState
	{
		// Token: 0x04007632 RID: 30258
		Invalid,
		// Token: 0x04007633 RID: 30259
		Tech,
		// Token: 0x04007634 RID: 30260
		Materials,
		// Token: 0x04007635 RID: 30261
		Complete,
		// Token: 0x04007636 RID: 30262
		TelepadBuilt,
		// Token: 0x04007637 RID: 30263
		UniquePerWorld,
		// Token: 0x04007638 RID: 30264
		RocketInteriorOnly,
		// Token: 0x04007639 RID: 30265
		RocketInteriorForbidden
	}
}
