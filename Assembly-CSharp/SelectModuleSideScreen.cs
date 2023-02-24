using System;
using System.Collections;
using System.Collections.Generic;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BDB RID: 3035
public class SelectModuleSideScreen : KScreen
{
	// Token: 0x06005F8D RID: 24461 RVA: 0x0022F122 File Offset: 0x0022D322
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (!show)
		{
			DetailsScreen.Instance.ClearSecondarySideScreen();
		}
	}

	// Token: 0x06005F8E RID: 24462 RVA: 0x0022F138 File Offset: 0x0022D338
	protected override void OnSpawn()
	{
		base.OnSpawn();
		SelectModuleSideScreen.Instance = this;
		this.SpawnButtons(null);
		this.buildSelectedModuleButton.onClick += this.OnClickBuildSelectedModule;
	}

	// Token: 0x06005F8F RID: 24463 RVA: 0x0022F164 File Offset: 0x0022D364
	protected override void OnForcedCleanUp()
	{
		SelectModuleSideScreen.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x06005F90 RID: 24464 RVA: 0x0022F172 File Offset: 0x0022D372
	protected override void OnCmpDisable()
	{
		this.ClearSubscriptionHandles();
		this.module = null;
		base.OnCmpDisable();
	}

	// Token: 0x06005F91 RID: 24465 RVA: 0x0022F188 File Offset: 0x0022D388
	private void ClearSubscriptionHandles()
	{
		foreach (int num in this.gameSubscriptionHandles)
		{
			Game.Instance.Unsubscribe(num);
		}
		this.gameSubscriptionHandles.Clear();
	}

	// Token: 0x06005F92 RID: 24466 RVA: 0x0022F1EC File Offset: 0x0022D3EC
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		this.ClearSubscriptionHandles();
		this.gameSubscriptionHandles.Add(Game.Instance.Subscribe(-107300940, new Action<object>(this.UpdateBuildableStates)));
		this.gameSubscriptionHandles.Add(Game.Instance.Subscribe(-1948169901, new Action<object>(this.UpdateBuildableStates)));
	}

	// Token: 0x06005F93 RID: 24467 RVA: 0x0022F254 File Offset: 0x0022D454
	protected override void OnCleanUp()
	{
		foreach (int num in this.gameSubscriptionHandles)
		{
			Game.Instance.Unsubscribe(num);
		}
		this.gameSubscriptionHandles.Clear();
		base.OnCleanUp();
	}

	// Token: 0x06005F94 RID: 24468 RVA: 0x0022F2BC File Offset: 0x0022D4BC
	public void SetLaunchPad(LaunchPad pad)
	{
		this.launchPad = pad;
		this.module = null;
		this.UpdateBuildableStates(null);
		foreach (KeyValuePair<BuildingDef, GameObject> keyValuePair in this.buttons)
		{
			this.SetupBuildingTooltip(keyValuePair.Value.GetComponent<ToolTip>(), keyValuePair.Key);
		}
	}

	// Token: 0x06005F95 RID: 24469 RVA: 0x0022F338 File Offset: 0x0022D538
	public void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.module = new_target.GetComponent<RocketModuleCluster>();
		if (this.module == null)
		{
			global::Debug.LogError("The gameObject received does not contain a RocketModuleCluster component");
			return;
		}
		this.launchPad = null;
		foreach (KeyValuePair<BuildingDef, GameObject> keyValuePair in this.buttons)
		{
			this.SetupBuildingTooltip(keyValuePair.Value.GetComponent<ToolTip>(), keyValuePair.Key);
		}
		this.UpdateBuildableStates(null);
		this.buildSelectedModuleButton.isInteractable = false;
		if (this.selectedModuleDef != null)
		{
			this.SelectModule(this.selectedModuleDef);
		}
	}

	// Token: 0x06005F96 RID: 24470 RVA: 0x0022F40C File Offset: 0x0022D60C
	private void UpdateBuildableStates(object data = null)
	{
		foreach (KeyValuePair<BuildingDef, GameObject> keyValuePair in this.buttons)
		{
			if (!this.moduleBuildableState.ContainsKey(keyValuePair.Key))
			{
				this.moduleBuildableState.Add(keyValuePair.Key, false);
			}
			TechItem techItem = Db.Get().TechItems.TryGet(keyValuePair.Key.PrefabID);
			if (techItem != null)
			{
				bool flag = DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive || techItem.IsComplete();
				keyValuePair.Value.SetActive(flag);
			}
			else
			{
				keyValuePair.Value.SetActive(true);
			}
			this.moduleBuildableState[keyValuePair.Key] = this.TestBuildable(keyValuePair.Key);
		}
		if (this.selectedModuleDef != null)
		{
			this.ConfigureMaterialSelector();
		}
		this.SetButtonColors();
	}

	// Token: 0x06005F97 RID: 24471 RVA: 0x0022F518 File Offset: 0x0022D718
	private void OnClickBuildSelectedModule()
	{
		if (this.selectedModuleDef != null)
		{
			this.OrderBuildSelectedModule();
		}
	}

	// Token: 0x06005F98 RID: 24472 RVA: 0x0022F530 File Offset: 0x0022D730
	private void ConfigureMaterialSelector()
	{
		this.buildSelectedModuleButton.isInteractable = false;
		if (this.materialSelectionPanel == null)
		{
			this.materialSelectionPanel = Util.KInstantiateUI<MaterialSelectionPanel>(this.materialSelectionPanelPrefab.gameObject, base.gameObject, true);
			this.materialSelectionPanel.transform.SetSiblingIndex(this.buildSelectedModuleButton.transform.GetSiblingIndex());
		}
		this.materialSelectionPanel.ClearSelectActions();
		this.materialSelectionPanel.ConfigureScreen(this.selectedModuleDef.CraftRecipe, new MaterialSelectionPanel.GetBuildableStateDelegate(this.IsDefBuildable), new MaterialSelectionPanel.GetBuildableTooltipDelegate(this.GetErrorTooltips));
		this.materialSelectionPanel.ToggleShowDescriptorPanels(false);
		this.materialSelectionPanel.AddSelectAction(new MaterialSelector.SelectMaterialActions(this.UpdateBuildButton));
		this.materialSelectionPanel.AutoSelectAvailableMaterial();
	}

	// Token: 0x06005F99 RID: 24473 RVA: 0x0022F5FC File Offset: 0x0022D7FC
	private bool IsDefBuildable(BuildingDef def)
	{
		return this.moduleBuildableState.ContainsKey(def) && this.moduleBuildableState[def];
	}

	// Token: 0x06005F9A RID: 24474 RVA: 0x0022F61C File Offset: 0x0022D81C
	private void UpdateBuildButton()
	{
		this.buildSelectedModuleButton.isInteractable = this.materialSelectionPanel != null && this.materialSelectionPanel.AllSelectorsSelected() && this.selectedModuleDef != null && this.moduleBuildableState[this.selectedModuleDef];
	}

	// Token: 0x06005F9B RID: 24475 RVA: 0x0022F674 File Offset: 0x0022D874
	public void SetButtonColors()
	{
		foreach (KeyValuePair<BuildingDef, GameObject> keyValuePair in this.buttons)
		{
			MultiToggle component = keyValuePair.Value.GetComponent<MultiToggle>();
			HierarchyReferences component2 = keyValuePair.Value.GetComponent<HierarchyReferences>();
			if (!this.moduleBuildableState[keyValuePair.Key])
			{
				component2.GetReference<Image>("FG").material = PlanScreen.Instance.desaturatedUIMaterial;
				if (keyValuePair.Key == this.selectedModuleDef)
				{
					component.ChangeState(1);
				}
				else
				{
					component.ChangeState(0);
				}
			}
			else
			{
				component2.GetReference<Image>("FG").material = PlanScreen.Instance.defaultUIMaterial;
				if (keyValuePair.Key == this.selectedModuleDef)
				{
					component.ChangeState(3);
				}
				else
				{
					component.ChangeState(2);
				}
			}
		}
		this.UpdateBuildButton();
	}

	// Token: 0x06005F9C RID: 24476 RVA: 0x0022F778 File Offset: 0x0022D978
	private bool TestBuildable(BuildingDef def)
	{
		GameObject buildingComplete = def.BuildingComplete;
		SelectModuleCondition.SelectionContext selectionContext = this.GetSelectionContext(def);
		if (selectionContext == SelectModuleCondition.SelectionContext.AddModuleAbove && this.module != null)
		{
			BuildingAttachPoint component = this.module.GetComponent<BuildingAttachPoint>();
			if (component != null && component.points[0].attachedBuilding != null && !component.points[0].attachedBuilding.GetComponent<ReorderableBuilding>().CanMoveVertically(def.HeightInCells, null))
			{
				return false;
			}
		}
		if (selectionContext == SelectModuleCondition.SelectionContext.AddModuleBelow && !this.module.GetComponent<ReorderableBuilding>().CanMoveVertically(def.HeightInCells, null))
		{
			return false;
		}
		if (selectionContext == SelectModuleCondition.SelectionContext.ReplaceModule && this.module != null && def != null && this.module.GetComponent<Building>().Def == def)
		{
			return false;
		}
		foreach (SelectModuleCondition selectModuleCondition in buildingComplete.GetComponent<ReorderableBuilding>().buildConditions)
		{
			if ((!selectModuleCondition.IgnoreInSanboxMode() || (!DebugHandler.InstantBuildMode && !Game.Instance.SandboxModeActive)) && !selectModuleCondition.EvaluateCondition((this.module == null) ? this.launchPad.gameObject : this.module.gameObject, def, selectionContext))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06005F9D RID: 24477 RVA: 0x0022F8E8 File Offset: 0x0022DAE8
	private void ClearButtons()
	{
		foreach (KeyValuePair<BuildingDef, GameObject> keyValuePair in this.buttons)
		{
			Util.KDestroyGameObject(keyValuePair.Value);
		}
		for (int i = this.categories.Count - 1; i >= 0; i--)
		{
			Util.KDestroyGameObject(this.categories[i]);
		}
		this.categories.Clear();
		this.buttons.Clear();
	}

	// Token: 0x06005F9E RID: 24478 RVA: 0x0022F980 File Offset: 0x0022DB80
	public void SpawnButtons(object data = null)
	{
		this.ClearButtons();
		GameObject gameObject = Util.KInstantiateUI(this.categoryPrefab, this.categoryContent, true);
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		this.categories.Add(gameObject);
		component.GetReference<LocText>("label");
		Transform reference = component.GetReference<Transform>("content");
		List<GameObject> prefabsWithComponent = Assets.GetPrefabsWithComponent<RocketModuleCluster>();
		using (List<string>.Enumerator enumerator = SelectModuleSideScreen.moduleButtonSortOrder.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				SelectModuleSideScreen.<>c__DisplayClass39_0 CS$<>8__locals1 = new SelectModuleSideScreen.<>c__DisplayClass39_0();
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.id = enumerator.Current;
				GameObject part = prefabsWithComponent.Find((GameObject p) => p.PrefabID().Name == CS$<>8__locals1.id);
				if (part == null)
				{
					global::Debug.LogWarning("Found an id [" + CS$<>8__locals1.id + "] in moduleButtonSortOrder in SelectModuleSideScreen.cs that doesn't have a corresponding rocket part!");
				}
				else
				{
					GameObject gameObject2 = Util.KInstantiateUI(this.moduleButtonPrefab, reference.gameObject, true);
					gameObject2.GetComponentsInChildren<Image>()[1].sprite = Def.GetUISprite(part, "ui", false).first;
					LocText componentInChildren = gameObject2.GetComponentInChildren<LocText>();
					componentInChildren.text = part.GetProperName();
					componentInChildren.alignment = TextAlignmentOptions.Bottom;
					componentInChildren.enableWordWrapping = true;
					MultiToggle component2 = gameObject2.GetComponent<MultiToggle>();
					component2.onClick = (System.Action)Delegate.Combine(component2.onClick, new System.Action(delegate
					{
						CS$<>8__locals1.<>4__this.SelectModule(part.GetComponent<Building>().Def);
					}));
					this.buttons.Add(part.GetComponent<Building>().Def, gameObject2);
					if (this.selectedModuleDef != null)
					{
						this.SelectModule(this.selectedModuleDef);
					}
				}
			}
		}
		this.UpdateBuildableStates(null);
	}

	// Token: 0x06005F9F RID: 24479 RVA: 0x0022FB68 File Offset: 0x0022DD68
	private void SetupBuildingTooltip(ToolTip tooltip, BuildingDef def)
	{
		tooltip.ClearMultiStringTooltip();
		string name = def.Name;
		string text = def.Effect;
		RocketModuleCluster component = def.BuildingComplete.GetComponent<RocketModuleCluster>();
		BuildingDef buildingDef = ((this.GetSelectionContext(def) == SelectModuleCondition.SelectionContext.ReplaceModule) ? this.module.GetComponent<Building>().Def : null);
		if (component != null)
		{
			text = text + "\n\n" + UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.MODULESTATCHANGE.TITLE;
			float burden = component.performanceStats.burden;
			float fuelKilogramPerDistance = component.performanceStats.FuelKilogramPerDistance;
			float enginePower = component.performanceStats.enginePower;
			int heightInCells = component.GetComponent<Building>().Def.HeightInCells;
			CraftModuleInterface craftModuleInterface = null;
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			int num4 = 0;
			if (base.GetComponentInParent<DetailsScreen>() != null && base.GetComponentInParent<DetailsScreen>().target.GetComponent<RocketModuleCluster>() != null)
			{
				craftModuleInterface = base.GetComponentInParent<DetailsScreen>().target.GetComponent<RocketModuleCluster>().CraftInterface;
			}
			int num5 = -1;
			if (craftModuleInterface != null)
			{
				num5 = craftModuleInterface.MaxHeight;
			}
			RocketEngineCluster component2 = component.GetComponent<RocketEngineCluster>();
			if (component2 != null)
			{
				num5 = component2.maxHeight;
			}
			float num6;
			float num7;
			if (craftModuleInterface == null)
			{
				num = burden;
				num2 = fuelKilogramPerDistance;
				num3 = enginePower;
				num6 = num3 / num;
				num7 = num6;
				num4 = heightInCells;
			}
			else
			{
				if (buildingDef != null)
				{
					RocketModulePerformance performanceStats = this.module.GetComponent<RocketModuleCluster>().performanceStats;
					num -= performanceStats.burden;
					num2 -= performanceStats.fuelKilogramPerDistance;
					num3 -= performanceStats.enginePower;
					num4 -= buildingDef.HeightInCells;
				}
				num = burden + craftModuleInterface.TotalBurden;
				num2 = fuelKilogramPerDistance + craftModuleInterface.Range;
				num3 = component.performanceStats.enginePower + craftModuleInterface.EnginePower;
				num6 = (component.performanceStats.enginePower + craftModuleInterface.EnginePower) / num;
				num7 = num6 - craftModuleInterface.EnginePower / craftModuleInterface.TotalBurden;
				num4 = craftModuleInterface.RocketHeight + heightInCells;
			}
			string text2 = ((burden >= 0f) ? GameUtil.AddPositiveSign(string.Format(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.MODULESTATCHANGE.NEGATIVEDELTA, burden), true) : string.Format(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.MODULESTATCHANGE.POSITIVEDELTA, burden));
			string text3 = ((fuelKilogramPerDistance >= 0f) ? GameUtil.AddPositiveSign(string.Format(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.MODULESTATCHANGE.POSITIVEDELTA, Math.Round((double)fuelKilogramPerDistance, 2)), true) : string.Format(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.MODULESTATCHANGE.NEGATIVEDELTA, Math.Round((double)fuelKilogramPerDistance, 2)));
			string text4 = ((enginePower >= 0f) ? GameUtil.AddPositiveSign(string.Format(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.MODULESTATCHANGE.POSITIVEDELTA, enginePower), true) : string.Format(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.MODULESTATCHANGE.NEGATIVEDELTA, enginePower));
			string text5 = ((num7 >= num6) ? GameUtil.AddPositiveSign(string.Format(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.MODULESTATCHANGE.POSITIVEDELTA, Math.Round((double)num7, 3)), true) : string.Format(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.MODULESTATCHANGE.NEGATIVEDELTA, Math.Round((double)num7, 2)));
			string text6 = ((heightInCells >= 0) ? GameUtil.AddPositiveSign(string.Format(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.MODULESTATCHANGE.POSITIVEDELTA, heightInCells), true) : string.Format(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.MODULESTATCHANGE.NEGATIVEDELTA, heightInCells));
			if (num5 != -1)
			{
				text = text + "\n" + string.Format(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.MODULESTATCHANGE.HEIGHT, num4, text6, num5);
			}
			else
			{
				text = text + "\n" + string.Format(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.MODULESTATCHANGE.HEIGHT_NOMAX, num4, text6);
			}
			text = text + "\n" + string.Format(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.MODULESTATCHANGE.BURDEN, num, text2);
			text = text + "\n" + string.Format(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.MODULESTATCHANGE.RANGE, Math.Round((double)num2, 2), text3);
			text = text + "\n" + string.Format(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.MODULESTATCHANGE.ENGINEPOWER, num3, text4);
			text = text + "\n" + string.Format(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.MODULESTATCHANGE.SPEED, Math.Round((double)num6, 3), text5);
			if (component.GetComponent<RocketEngineCluster>() != null)
			{
				text = text + "\n\n" + string.Format(UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.ENGINE_MAX_HEIGHT, num5);
			}
		}
		tooltip.AddMultiStringTooltip(name, PlanScreen.Instance.buildingToolTipSettings.BuildButtonName);
		tooltip.AddMultiStringTooltip(text, PlanScreen.Instance.buildingToolTipSettings.BuildButtonDescription);
		this.AddErrorTooltips(tooltip, def, false);
	}

	// Token: 0x06005FA0 RID: 24480 RVA: 0x00230020 File Offset: 0x0022E220
	private SelectModuleCondition.SelectionContext GetSelectionContext(BuildingDef def)
	{
		SelectModuleCondition.SelectionContext selectionContext = SelectModuleCondition.SelectionContext.AddModuleAbove;
		if (this.launchPad == null)
		{
			if (!this.addingNewModule)
			{
				selectionContext = SelectModuleCondition.SelectionContext.ReplaceModule;
			}
			else
			{
				List<SelectModuleCondition> buildConditions = Assets.GetPrefab(this.module.GetComponent<KPrefabID>().PrefabID()).GetComponent<ReorderableBuilding>().buildConditions;
				ReorderableBuilding component = def.BuildingComplete.GetComponent<ReorderableBuilding>();
				if (buildConditions.Find((SelectModuleCondition match) => match is TopOnly) == null)
				{
					if (component.buildConditions.Find((SelectModuleCondition match) => match is EngineOnBottom) == null)
					{
						return selectionContext;
					}
				}
				selectionContext = SelectModuleCondition.SelectionContext.AddModuleBelow;
			}
		}
		return selectionContext;
	}

	// Token: 0x06005FA1 RID: 24481 RVA: 0x002300CC File Offset: 0x0022E2CC
	private string GetErrorTooltips(BuildingDef def)
	{
		List<SelectModuleCondition> buildConditions = def.BuildingComplete.GetComponent<ReorderableBuilding>().buildConditions;
		SelectModuleCondition.SelectionContext selectionContext = this.GetSelectionContext(def);
		string text = "";
		for (int i = 0; i < buildConditions.Count; i++)
		{
			if (!buildConditions[i].IgnoreInSanboxMode() || (!DebugHandler.InstantBuildMode && !Game.Instance.SandboxModeActive))
			{
				GameObject gameObject = ((this.module == null) ? this.launchPad.gameObject : this.module.gameObject);
				if (!buildConditions[i].EvaluateCondition(gameObject, def, selectionContext))
				{
					if (!string.IsNullOrEmpty(text))
					{
						text += "\n";
					}
					text += buildConditions[i].GetStatusTooltip(false, gameObject, def);
				}
			}
		}
		return text;
	}

	// Token: 0x06005FA2 RID: 24482 RVA: 0x00230198 File Offset: 0x0022E398
	private void AddErrorTooltips(ToolTip tooltip, BuildingDef def, bool clearFirst = false)
	{
		if (clearFirst)
		{
			tooltip.ClearMultiStringTooltip();
		}
		if (!clearFirst)
		{
			tooltip.AddMultiStringTooltip("\n", PlanScreen.Instance.buildingToolTipSettings.MaterialRequirement);
		}
		tooltip.AddMultiStringTooltip(this.GetErrorTooltips(def), PlanScreen.Instance.buildingToolTipSettings.MaterialRequirement);
	}

	// Token: 0x06005FA3 RID: 24483 RVA: 0x002301EA File Offset: 0x0022E3EA
	public void SelectModule(BuildingDef def)
	{
		this.selectedModuleDef = def;
		this.ConfigureMaterialSelector();
		this.SetButtonColors();
		this.UpdateBuildButton();
		this.AddErrorTooltips(this.buildSelectedModuleButton.GetComponent<ToolTip>(), this.selectedModuleDef, true);
	}

	// Token: 0x06005FA4 RID: 24484 RVA: 0x00230220 File Offset: 0x0022E420
	private void OrderBuildSelectedModule()
	{
		BuildingDef buildingDef = this.selectedModuleDef;
		GameObject gameObject2;
		if (this.module != null)
		{
			GameObject gameObject = this.module.gameObject;
			if (this.addingNewModule)
			{
				gameObject2 = this.module.GetComponent<ReorderableBuilding>().AddModule(this.selectedModuleDef, this.materialSelectionPanel.GetSelectedElementAsList);
			}
			else
			{
				gameObject2 = this.module.GetComponent<ReorderableBuilding>().ConvertModule(this.selectedModuleDef, this.materialSelectionPanel.GetSelectedElementAsList);
			}
		}
		else
		{
			gameObject2 = this.launchPad.AddBaseModule(this.selectedModuleDef, this.materialSelectionPanel.GetSelectedElementAsList);
		}
		if (gameObject2 != null)
		{
			Vector2 anchoredPosition = this.mainContents.GetComponent<KScrollRect>().content.anchoredPosition;
			SelectTool.Instance.StartCoroutine(this.SelectNextFrame(gameObject2.GetComponent<KSelectable>(), buildingDef, anchoredPosition.y));
		}
	}

	// Token: 0x06005FA5 RID: 24485 RVA: 0x002302FA File Offset: 0x0022E4FA
	private IEnumerator SelectNextFrame(KSelectable selectable, BuildingDef previousSelectedDef, float scrollPosition)
	{
		yield return 0;
		SelectTool.Instance.Select(selectable, false);
		RocketModuleSideScreen.instance.ClickAddNew(scrollPosition, previousSelectedDef);
		yield break;
	}

	// Token: 0x04004171 RID: 16753
	public RocketModule module;

	// Token: 0x04004172 RID: 16754
	private LaunchPad launchPad;

	// Token: 0x04004173 RID: 16755
	public GameObject mainContents;

	// Token: 0x04004174 RID: 16756
	[Header("Category")]
	public GameObject categoryPrefab;

	// Token: 0x04004175 RID: 16757
	public GameObject moduleButtonPrefab;

	// Token: 0x04004176 RID: 16758
	public GameObject categoryContent;

	// Token: 0x04004177 RID: 16759
	private BuildingDef selectedModuleDef;

	// Token: 0x04004178 RID: 16760
	public List<GameObject> categories = new List<GameObject>();

	// Token: 0x04004179 RID: 16761
	public Dictionary<BuildingDef, GameObject> buttons = new Dictionary<BuildingDef, GameObject>();

	// Token: 0x0400417A RID: 16762
	private Dictionary<BuildingDef, bool> moduleBuildableState = new Dictionary<BuildingDef, bool>();

	// Token: 0x0400417B RID: 16763
	public static SelectModuleSideScreen Instance;

	// Token: 0x0400417C RID: 16764
	public bool addingNewModule;

	// Token: 0x0400417D RID: 16765
	public GameObject materialSelectionPanelPrefab;

	// Token: 0x0400417E RID: 16766
	private MaterialSelectionPanel materialSelectionPanel;

	// Token: 0x0400417F RID: 16767
	public KButton buildSelectedModuleButton;

	// Token: 0x04004180 RID: 16768
	public ColorStyleSetting colorStyleButton;

	// Token: 0x04004181 RID: 16769
	public ColorStyleSetting colorStyleButtonSelected;

	// Token: 0x04004182 RID: 16770
	public ColorStyleSetting colorStyleButtonInactive;

	// Token: 0x04004183 RID: 16771
	public ColorStyleSetting colorStyleButtonInactiveSelected;

	// Token: 0x04004184 RID: 16772
	private List<int> gameSubscriptionHandles = new List<int>();

	// Token: 0x04004185 RID: 16773
	public static List<string> moduleButtonSortOrder = new List<string>
	{
		"CO2Engine", "SugarEngine", "SteamEngineCluster", "KeroseneEngineClusterSmall", "KeroseneEngineCluster", "HEPEngine", "HydrogenEngineCluster", "HabitatModuleSmall", "HabitatModuleMedium", "NoseconeBasic",
		"NoseconeHarvest", "OrbitalCargoModule", "ScoutModule", "PioneerModule", "LiquidFuelTankCluster", "SmallOxidizerTank", "OxidizerTankCluster", "OxidizerTankLiquidCluster", "SolidCargoBaySmall", "LiquidCargoBaySmall",
		"GasCargoBaySmall", "CargoBayCluster", "LiquidCargoBayCluster", "GasCargoBayCluster", "BatteryModule", "SolarPanelModule", "ArtifactCargoBay", "ScannerModule"
	};
}
