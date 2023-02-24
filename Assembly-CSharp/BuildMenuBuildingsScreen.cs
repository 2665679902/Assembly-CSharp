using System;
using System.Collections;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020009F1 RID: 2545
public class BuildMenuBuildingsScreen : KIconToggleMenu
{
	// Token: 0x06004C15 RID: 19477 RVA: 0x001AAA1D File Offset: 0x001A8C1D
	public override float GetSortKey()
	{
		return 8f;
	}

	// Token: 0x06004C16 RID: 19478 RVA: 0x001AAA24 File Offset: 0x001A8C24
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.UpdateBuildableStates();
		Game.Instance.Subscribe(-107300940, new Action<object>(this.OnResearchComplete));
		base.onSelect += this.OnClickBuilding;
		Game.Instance.Subscribe(-1190690038, new Action<object>(this.OnBuildToolDeactivated));
	}

	// Token: 0x06004C17 RID: 19479 RVA: 0x001AAA88 File Offset: 0x001A8C88
	public void Configure(HashedString category, IList<BuildMenu.BuildingInfo> building_infos)
	{
		this.ClearButtons();
		this.SetHasFocus(true);
		List<KIconToggleMenu.ToggleInfo> list = new List<KIconToggleMenu.ToggleInfo>();
		string text = HashCache.Get().Get(category).ToUpper();
		text = text.Replace(" ", "");
		this.titleLabel.text = Strings.Get("STRINGS.UI.NEWBUILDCATEGORIES." + text + ".BUILDMENUTITLE");
		foreach (BuildMenu.BuildingInfo buildingInfo in building_infos)
		{
			BuildingDef def = Assets.GetBuildingDef(buildingInfo.id);
			if (def.ShouldShowInBuildMenu() && def.IsAvailable())
			{
				KIconToggleMenu.ToggleInfo toggleInfo = new KIconToggleMenu.ToggleInfo(def.Name, new BuildMenuBuildingsScreen.UserData(def, PlanScreen.RequirementsState.Tech), def.HotKey, () => def.GetUISprite("ui", false));
				list.Add(toggleInfo);
			}
		}
		base.Setup(list);
		for (int i = 0; i < this.toggleInfo.Count; i++)
		{
			this.RefreshToggle(this.toggleInfo[i]);
		}
		int num = 0;
		using (IEnumerator enumerator2 = this.gridSizer.transform.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				if (((Transform)enumerator2.Current).gameObject.activeSelf)
				{
					num++;
				}
			}
		}
		this.gridSizer.constraintCount = Mathf.Min(num, 3);
		int num2 = Mathf.Min(num, this.gridSizer.constraintCount);
		int num3 = (num + this.gridSizer.constraintCount - 1) / this.gridSizer.constraintCount;
		int num4 = num2 - 1;
		int num5 = num3 - 1;
		Vector2 vector = new Vector2((float)num2 * this.gridSizer.cellSize.x + (float)num4 * this.gridSizer.spacing.x + (float)this.gridSizer.padding.left + (float)this.gridSizer.padding.right, (float)num3 * this.gridSizer.cellSize.y + (float)num5 * this.gridSizer.spacing.y + (float)this.gridSizer.padding.top + (float)this.gridSizer.padding.bottom);
		this.contentSizeLayout.minWidth = vector.x;
		this.contentSizeLayout.minHeight = vector.y;
	}

	// Token: 0x06004C18 RID: 19480 RVA: 0x001AAD48 File Offset: 0x001A8F48
	private void ConfigureToolTip(ToolTip tooltip, BuildingDef def)
	{
		tooltip.ClearMultiStringTooltip();
		tooltip.AddMultiStringTooltip(def.Name, this.buildingToolTipSettings.BuildButtonName);
		tooltip.AddMultiStringTooltip(def.Effect, this.buildingToolTipSettings.BuildButtonDescription);
	}

	// Token: 0x06004C19 RID: 19481 RVA: 0x001AAD80 File Offset: 0x001A8F80
	public void CloseRecipe(bool playSound = false)
	{
		if (playSound)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Deselect", false));
		}
		ToolMenu.Instance.ClearSelection();
		this.DeactivateBuildTools();
		if (PlayerController.Instance.ActiveTool == PrebuildTool.Instance)
		{
			SelectTool.Instance.Activate();
		}
		this.selectedBuilding = null;
		this.onBuildingSelected(this.selectedBuilding);
	}

	// Token: 0x06004C1A RID: 19482 RVA: 0x001AADE8 File Offset: 0x001A8FE8
	private void RefreshToggle(KIconToggleMenu.ToggleInfo info)
	{
		if (info == null || info.toggle == null)
		{
			return;
		}
		BuildingDef def = (info.userData as BuildMenuBuildingsScreen.UserData).def;
		TechItem techItem = Db.Get().TechItems.TryGet(def.PrefabID);
		bool flag = DebugHandler.InstantBuildMode || techItem == null || techItem.IsComplete();
		bool flag2 = flag || techItem == null || techItem.ParentTech.ArePrerequisitesComplete();
		KToggle toggle = info.toggle;
		if (toggle.gameObject.activeSelf != flag2)
		{
			toggle.gameObject.SetActive(flag2);
		}
		if (toggle.bgImage == null)
		{
			return;
		}
		Image image = toggle.bgImage.GetComponentsInChildren<Image>()[1];
		Sprite uisprite = def.GetUISprite("ui", false);
		image.sprite = uisprite;
		image.SetNativeSize();
		image.rectTransform().sizeDelta /= 4f;
		ToolTip component = toggle.gameObject.GetComponent<ToolTip>();
		component.ClearMultiStringTooltip();
		string text = def.Name;
		string effect = def.Effect;
		if (def.HotKey != global::Action.NumActions)
		{
			text += GameUtil.GetHotkeyString(def.HotKey);
		}
		component.AddMultiStringTooltip(text, this.buildingToolTipSettings.BuildButtonName);
		component.AddMultiStringTooltip(effect, this.buildingToolTipSettings.BuildButtonDescription);
		LocText componentInChildren = toggle.GetComponentInChildren<LocText>();
		if (componentInChildren != null)
		{
			componentInChildren.text = def.Name;
		}
		PlanScreen.RequirementsState requirementsState = BuildMenu.Instance.BuildableState(def);
		int num = ((requirementsState == PlanScreen.RequirementsState.Complete) ? 1 : 0);
		ImageToggleState.State state;
		if (def == this.selectedBuilding && (requirementsState == PlanScreen.RequirementsState.Complete || DebugHandler.InstantBuildMode))
		{
			state = ImageToggleState.State.Active;
		}
		else
		{
			state = ((requirementsState == PlanScreen.RequirementsState.Complete || DebugHandler.InstantBuildMode) ? ImageToggleState.State.Inactive : ImageToggleState.State.Disabled);
		}
		if (def == this.selectedBuilding && state == ImageToggleState.State.Disabled)
		{
			state = ImageToggleState.State.DisabledActive;
		}
		else if (state == ImageToggleState.State.Disabled)
		{
			state = ImageToggleState.State.Disabled;
		}
		toggle.GetComponent<ImageToggleState>().SetState(state);
		Material material;
		Color color;
		if (requirementsState == PlanScreen.RequirementsState.Complete || DebugHandler.InstantBuildMode)
		{
			material = this.defaultUIMaterial;
			color = Color.white;
		}
		else
		{
			material = this.desaturatedUIMaterial;
			Color color3;
			if (!flag)
			{
				Graphic graphic = image;
				Color color2 = new Color(1f, 1f, 1f, 0.15f);
				graphic.color = color2;
				color3 = color2;
			}
			else
			{
				color3 = new Color(1f, 1f, 1f, 0.6f);
			}
			color = color3;
		}
		if (image.material != material)
		{
			image.material = material;
			image.color = color;
		}
		Image fgImage = toggle.gameObject.GetComponent<KToggle>().fgImage;
		fgImage.gameObject.SetActive(false);
		if (!flag)
		{
			fgImage.sprite = this.Overlay_NeedTech;
			fgImage.gameObject.SetActive(true);
			string text2 = string.Format(UI.PRODUCTINFO_REQUIRESRESEARCHDESC, techItem.ParentTech.Name);
			component.AddMultiStringTooltip("\n", this.buildingToolTipSettings.ResearchRequirement);
			component.AddMultiStringTooltip(text2, this.buildingToolTipSettings.ResearchRequirement);
			return;
		}
		if (requirementsState != PlanScreen.RequirementsState.Complete)
		{
			fgImage.gameObject.SetActive(false);
			component.AddMultiStringTooltip("\n", this.buildingToolTipSettings.ResearchRequirement);
			string text3 = UI.PRODUCTINFO_MISSINGRESOURCES_HOVER;
			component.AddMultiStringTooltip(text3, this.buildingToolTipSettings.ResearchRequirement);
			foreach (Recipe.Ingredient ingredient in def.CraftRecipe.Ingredients)
			{
				string text4 = string.Format("{0}{1}: {2}", "• ", ingredient.tag.ProperName(), GameUtil.GetFormattedMass(ingredient.amount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
				component.AddMultiStringTooltip(text4, this.buildingToolTipSettings.ResearchRequirement);
			}
			component.AddMultiStringTooltip("", this.buildingToolTipSettings.ResearchRequirement);
		}
	}

	// Token: 0x06004C1B RID: 19483 RVA: 0x001AB1D8 File Offset: 0x001A93D8
	public void ClearUI()
	{
		this.Show(false);
		this.ClearButtons();
	}

	// Token: 0x06004C1C RID: 19484 RVA: 0x001AB1E8 File Offset: 0x001A93E8
	private void ClearButtons()
	{
		foreach (KToggle ktoggle in this.toggles)
		{
			ktoggle.gameObject.SetActive(false);
			ktoggle.gameObject.transform.SetParent(null);
			UnityEngine.Object.DestroyImmediate(ktoggle.gameObject);
		}
		if (this.toggles != null)
		{
			this.toggles.Clear();
		}
		if (this.toggleInfo != null)
		{
			this.toggleInfo.Clear();
		}
	}

	// Token: 0x06004C1D RID: 19485 RVA: 0x001AB280 File Offset: 0x001A9480
	private void OnClickBuilding(KIconToggleMenu.ToggleInfo toggle_info)
	{
		BuildMenuBuildingsScreen.UserData userData = toggle_info.userData as BuildMenuBuildingsScreen.UserData;
		this.OnSelectBuilding(userData.def);
	}

	// Token: 0x06004C1E RID: 19486 RVA: 0x001AB2A8 File Offset: 0x001A94A8
	private void OnSelectBuilding(BuildingDef def)
	{
		PlanScreen.RequirementsState requirementsState = BuildMenu.Instance.BuildableState(def);
		if (requirementsState - PlanScreen.RequirementsState.Materials <= 1)
		{
			if (def != this.selectedBuilding)
			{
				this.selectedBuilding = def;
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click", false));
			}
			else
			{
				this.selectedBuilding = null;
				this.ClearSelection();
				this.CloseRecipe(true);
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Deselect", false));
			}
		}
		else
		{
			this.selectedBuilding = null;
			this.ClearSelection();
			this.CloseRecipe(true);
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Negative", false));
		}
		this.onBuildingSelected(this.selectedBuilding);
	}

	// Token: 0x06004C1F RID: 19487 RVA: 0x001AB34C File Offset: 0x001A954C
	public void UpdateBuildableStates()
	{
		if (this.toggleInfo == null || this.toggleInfo.Count <= 0)
		{
			return;
		}
		BuildingDef buildingDef = null;
		foreach (KIconToggleMenu.ToggleInfo toggleInfo in this.toggleInfo)
		{
			this.RefreshToggle(toggleInfo);
			BuildMenuBuildingsScreen.UserData userData = toggleInfo.userData as BuildMenuBuildingsScreen.UserData;
			BuildingDef def = userData.def;
			if (def.IsAvailable())
			{
				PlanScreen.RequirementsState requirementsState = BuildMenu.Instance.BuildableState(def);
				if (requirementsState != userData.requirementsState)
				{
					if (def == BuildMenu.Instance.SelectedBuildingDef)
					{
						buildingDef = def;
					}
					this.RefreshToggle(toggleInfo);
					userData.requirementsState = requirementsState;
				}
			}
		}
		if (buildingDef != null)
		{
			BuildMenu.Instance.RefreshProductInfoScreen(buildingDef);
		}
	}

	// Token: 0x06004C20 RID: 19488 RVA: 0x001AB420 File Offset: 0x001A9620
	private void OnResearchComplete(object data)
	{
		this.UpdateBuildableStates();
	}

	// Token: 0x06004C21 RID: 19489 RVA: 0x001AB428 File Offset: 0x001A9628
	private void DeactivateBuildTools()
	{
		InterfaceTool activeTool = PlayerController.Instance.ActiveTool;
		if (activeTool != null)
		{
			Type type = activeTool.GetType();
			if (type == typeof(BuildTool) || typeof(BaseUtilityBuildTool).IsAssignableFrom(type) || typeof(PrebuildTool).IsAssignableFrom(type))
			{
				activeTool.DeactivateTool(null);
			}
		}
	}

	// Token: 0x06004C22 RID: 19490 RVA: 0x001AB490 File Offset: 0x001A9690
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.mouseOver && base.ConsumeMouseScroll && !e.TryConsume(global::Action.ZoomIn))
		{
			e.TryConsume(global::Action.ZoomOut);
		}
		if (!this.HasFocus)
		{
			return;
		}
		if (e.TryConsume(global::Action.Escape))
		{
			Game.Instance.Trigger(288942073, null);
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Close", false));
			return;
		}
		base.OnKeyDown(e);
		if (!e.Consumed)
		{
			global::Action action = e.GetAction();
			if (action >= global::Action.BUILD_MENU_START_INTERCEPT)
			{
				e.TryConsume(action);
			}
		}
	}

	// Token: 0x06004C23 RID: 19491 RVA: 0x001AB514 File Offset: 0x001A9714
	public override void OnKeyUp(KButtonEvent e)
	{
		if (!this.HasFocus)
		{
			return;
		}
		if (this.selectedBuilding != null && PlayerController.Instance.ConsumeIfNotDragging(e, global::Action.MouseRight))
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Close", false));
			Game.Instance.Trigger(288942073, null);
			return;
		}
		base.OnKeyUp(e);
		if (!e.Consumed)
		{
			global::Action action = e.GetAction();
			if (action >= global::Action.BUILD_MENU_START_INTERCEPT)
			{
				e.TryConsume(action);
			}
		}
	}

	// Token: 0x06004C24 RID: 19492 RVA: 0x001AB58C File Offset: 0x001A978C
	public override void Close()
	{
		ToolMenu.Instance.ClearSelection();
		this.DeactivateBuildTools();
		if (PlayerController.Instance.ActiveTool == PrebuildTool.Instance)
		{
			SelectTool.Instance.Activate();
		}
		this.selectedBuilding = null;
		this.ClearButtons();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06004C25 RID: 19493 RVA: 0x001AB5E2 File Offset: 0x001A97E2
	public override void SetHasFocus(bool has_focus)
	{
		base.SetHasFocus(has_focus);
		if (this.focusIndicator != null)
		{
			this.focusIndicator.color = (has_focus ? this.focusedColour : this.unfocusedColour);
		}
	}

	// Token: 0x06004C26 RID: 19494 RVA: 0x001AB61A File Offset: 0x001A981A
	private void OnBuildToolDeactivated(object data)
	{
		this.CloseRecipe(false);
	}

	// Token: 0x04003217 RID: 12823
	[SerializeField]
	private Image focusIndicator;

	// Token: 0x04003218 RID: 12824
	[SerializeField]
	private Color32 focusedColour;

	// Token: 0x04003219 RID: 12825
	[SerializeField]
	private Color32 unfocusedColour;

	// Token: 0x0400321A RID: 12826
	public Action<BuildingDef> onBuildingSelected;

	// Token: 0x0400321B RID: 12827
	[SerializeField]
	private LocText titleLabel;

	// Token: 0x0400321C RID: 12828
	[SerializeField]
	private BuildMenuBuildingsScreen.BuildingToolTipSettings buildingToolTipSettings;

	// Token: 0x0400321D RID: 12829
	[SerializeField]
	private LayoutElement contentSizeLayout;

	// Token: 0x0400321E RID: 12830
	[SerializeField]
	private GridLayoutGroup gridSizer;

	// Token: 0x0400321F RID: 12831
	[SerializeField]
	private Sprite Overlay_NeedTech;

	// Token: 0x04003220 RID: 12832
	[SerializeField]
	private Material defaultUIMaterial;

	// Token: 0x04003221 RID: 12833
	[SerializeField]
	private Material desaturatedUIMaterial;

	// Token: 0x04003222 RID: 12834
	private BuildingDef selectedBuilding;

	// Token: 0x020017F8 RID: 6136
	[Serializable]
	public struct BuildingToolTipSettings
	{
		// Token: 0x04006E90 RID: 28304
		public TextStyleSetting BuildButtonName;

		// Token: 0x04006E91 RID: 28305
		public TextStyleSetting BuildButtonDescription;

		// Token: 0x04006E92 RID: 28306
		public TextStyleSetting MaterialRequirement;

		// Token: 0x04006E93 RID: 28307
		public TextStyleSetting ResearchRequirement;
	}

	// Token: 0x020017F9 RID: 6137
	[Serializable]
	public struct BuildingNameTextSetting
	{
		// Token: 0x04006E94 RID: 28308
		public TextStyleSetting ActiveSelected;

		// Token: 0x04006E95 RID: 28309
		public TextStyleSetting ActiveDeselected;

		// Token: 0x04006E96 RID: 28310
		public TextStyleSetting InactiveSelected;

		// Token: 0x04006E97 RID: 28311
		public TextStyleSetting InactiveDeselected;
	}

	// Token: 0x020017FA RID: 6138
	private class UserData
	{
		// Token: 0x06008C7F RID: 35967 RVA: 0x00302A87 File Offset: 0x00300C87
		public UserData(BuildingDef def, PlanScreen.RequirementsState state)
		{
			this.def = def;
			this.requirementsState = state;
		}

		// Token: 0x04006E98 RID: 28312
		public BuildingDef def;

		// Token: 0x04006E99 RID: 28313
		public PlanScreen.RequirementsState requirementsState;
	}
}
