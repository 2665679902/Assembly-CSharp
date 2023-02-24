using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B57 RID: 2903
public class PlanBuildingToggle : KToggle
{
	// Token: 0x06005A4C RID: 23116 RVA: 0x0020B4B8 File Offset: 0x002096B8
	public void Config(BuildingDef def, PlanScreen planScreen, HashedString buildingCategory)
	{
		this.def = def;
		this.planScreen = planScreen;
		this.buildingCategory = buildingCategory;
		this.techItem = Db.Get().TechItems.TryGet(def.PrefabID);
		this.gameSubscriptions.Add(Game.Instance.Subscribe(-107300940, new Action<object>(this.CheckResearch)));
		this.gameSubscriptions.Add(Game.Instance.Subscribe(-1948169901, new Action<object>(this.CheckResearch)));
		this.gameSubscriptions.Add(Game.Instance.Subscribe(1557339983, new Action<object>(this.CheckResearch)));
		this.sprite = def.GetUISprite("ui", false);
		base.onClick += delegate
		{
			PlanScreen.Instance.OnSelectBuilding(this.gameObject, def, null);
			this.RefreshDisplay();
		};
		if (TUNING.BUILDINGS.PLANSUBCATEGORYSORTING.ContainsKey(def.PrefabID))
		{
			Strings.TryGet("STRINGS.UI.NEWBUILDCATEGORIES." + TUNING.BUILDINGS.PLANSUBCATEGORYSORTING[def.PrefabID].ToUpper() + ".NAME", out this.subcategoryName);
		}
		else
		{
			global::Debug.LogWarning("Building " + def.PrefabID + " has not been added to plan screen subcategory organization in BuildingTuning.cs");
		}
		this.CheckResearch(null);
		this.Refresh();
	}

	// Token: 0x06005A4D RID: 23117 RVA: 0x0020B62C File Offset: 0x0020982C
	protected override void OnDestroy()
	{
		if (Game.Instance != null)
		{
			foreach (int num in this.gameSubscriptions)
			{
				Game.Instance.Unsubscribe(num);
			}
		}
		this.gameSubscriptions.Clear();
		base.OnDestroy();
	}

	// Token: 0x06005A4E RID: 23118 RVA: 0x0020B6A4 File Offset: 0x002098A4
	private void CheckResearch(object data = null)
	{
		this.researchComplete = PlanScreen.TechRequirementsMet(this.techItem);
	}

	// Token: 0x06005A4F RID: 23119 RVA: 0x0020B6B8 File Offset: 0x002098B8
	public bool CheckBuildingPassesSearchFilter(Def building)
	{
		if (BuildingGroupScreen.SearchIsEmpty)
		{
			return this.StandardDisplayFilter();
		}
		string text = BuildingGroupScreen.Instance.inputField.text;
		string text2 = UI.StripLinkFormatting(building.Name).ToLower();
		text = text.ToUpper();
		return text2.ToUpper().Contains(text) || (this.subcategoryName != null && this.subcategoryName.String.ToUpper().Contains(text));
	}

	// Token: 0x06005A50 RID: 23120 RVA: 0x0020B72C File Offset: 0x0020992C
	private bool StandardDisplayFilter()
	{
		return (this.researchComplete || DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive) && (this.planScreen.ActiveCategoryToggleInfo == null || this.buildingCategory == (HashedString)this.planScreen.ActiveCategoryToggleInfo.userData);
	}

	// Token: 0x06005A51 RID: 23121 RVA: 0x0020B788 File Offset: 0x00209988
	public bool Refresh()
	{
		bool flag;
		if (BuildingGroupScreen.SearchIsEmpty)
		{
			flag = this.StandardDisplayFilter();
		}
		else
		{
			flag = this.CheckBuildingPassesSearchFilter(this.def);
		}
		bool flag2 = false;
		if (base.gameObject.activeSelf != flag)
		{
			base.gameObject.SetActive(flag);
			flag2 = true;
		}
		if (!base.gameObject.activeSelf)
		{
			return flag2;
		}
		this.PositionTooltip();
		this.RefreshLabel();
		this.RefreshDisplay();
		return flag2;
	}

	// Token: 0x06005A52 RID: 23122 RVA: 0x0020B7F4 File Offset: 0x002099F4
	public void SwitchViewMode(bool listView)
	{
		this.text.gameObject.SetActive(!listView);
		this.text_listView.gameObject.SetActive(listView);
		this.buildingIcon.gameObject.SetActive(!listView);
		this.buildingIcon_listView.gameObject.SetActive(listView);
	}

	// Token: 0x06005A53 RID: 23123 RVA: 0x0020B84C File Offset: 0x00209A4C
	private void RefreshLabel()
	{
		if (this.text != null)
		{
			this.text.fontSize = (float)(ScreenResolutionMonitor.UsingGamepadUIMode() ? PlanScreen.fontSizeBigMode : PlanScreen.fontSizeStandardMode);
			this.text_listView.fontSize = (float)(ScreenResolutionMonitor.UsingGamepadUIMode() ? PlanScreen.fontSizeBigMode : PlanScreen.fontSizeStandardMode);
			this.text.text = this.def.Name;
			this.text_listView.text = this.def.Name;
		}
	}

	// Token: 0x06005A54 RID: 23124 RVA: 0x0020B8D4 File Offset: 0x00209AD4
	private void RefreshDisplay()
	{
		PlanScreen.RequirementsState buildableState = PlanScreen.Instance.GetBuildableState(this.def);
		bool flag = buildableState == PlanScreen.RequirementsState.Complete || DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive;
		bool flag2 = base.gameObject == PlanScreen.Instance.SelectedBuildingGameObject;
		if (flag2 && flag)
		{
			this.toggle.ChangeState(1);
		}
		else if (!flag2 && flag)
		{
			this.toggle.ChangeState(0);
		}
		else if (flag2 && !flag)
		{
			this.toggle.ChangeState(3);
		}
		else if (!flag2 && !flag)
		{
			this.toggle.ChangeState(2);
		}
		this.RefreshBuildingButtonIconAndColors(flag);
		this.RefreshFG(buildableState);
	}

	// Token: 0x06005A55 RID: 23125 RVA: 0x0020B984 File Offset: 0x00209B84
	private void PositionTooltip()
	{
		this.tooltip.overrideParentObject = (PlanScreen.Instance.ProductInfoScreen.gameObject.activeSelf ? PlanScreen.Instance.ProductInfoScreen.rectTransform() : PlanScreen.Instance.buildingGroupsRoot);
		this.tooltip.tooltipPivot = Vector2.zero;
		this.tooltip.parentPositionAnchor = new Vector2(1f, 0f);
		this.tooltip.tooltipPositionOffset = new Vector2(4f, 0f);
		this.tooltip.ClearMultiStringTooltip();
		string name = this.def.Name;
		string effect = this.def.Effect;
		this.tooltip.AddMultiStringTooltip(name, PlanScreen.Instance.buildingToolTipSettings.BuildButtonName);
		this.tooltip.AddMultiStringTooltip(effect, PlanScreen.Instance.buildingToolTipSettings.BuildButtonDescription);
	}

	// Token: 0x06005A56 RID: 23126 RVA: 0x0020BA6C File Offset: 0x00209C6C
	private void RefreshBuildingButtonIconAndColors(bool buttonAvailable)
	{
		if (this.sprite == null)
		{
			this.sprite = PlanScreen.Instance.defaultBuildingIconSprite;
		}
		this.buildingIcon.sprite = this.sprite;
		this.buildingIcon.SetNativeSize();
		this.buildingIcon_listView.sprite = this.sprite;
		float num = (ScreenResolutionMonitor.UsingGamepadUIMode() ? 3.25f : 4f);
		this.buildingIcon.rectTransform().sizeDelta /= num;
		Material material = (buttonAvailable ? PlanScreen.Instance.defaultUIMaterial : PlanScreen.Instance.desaturatedUIMaterial);
		if (this.buildingIcon.material != material)
		{
			this.buildingIcon.material = material;
			this.buildingIcon_listView.material = material;
		}
	}

	// Token: 0x06005A57 RID: 23127 RVA: 0x0020BB3C File Offset: 0x00209D3C
	private void RefreshFG(PlanScreen.RequirementsState requirementsState)
	{
		if (requirementsState == PlanScreen.RequirementsState.Tech)
		{
			this.fgImage.sprite = PlanScreen.Instance.Overlay_NeedTech;
			this.fgImage.gameObject.SetActive(true);
		}
		else
		{
			this.fgImage.gameObject.SetActive(false);
		}
		string tooltipForRequirementsState = PlanScreen.GetTooltipForRequirementsState(this.def, requirementsState);
		if (tooltipForRequirementsState != null)
		{
			this.tooltip.AddMultiStringTooltip("\n", PlanScreen.Instance.buildingToolTipSettings.ResearchRequirement);
			this.tooltip.AddMultiStringTooltip(tooltipForRequirementsState, PlanScreen.Instance.buildingToolTipSettings.ResearchRequirement);
		}
	}

	// Token: 0x04003D05 RID: 15621
	private BuildingDef def;

	// Token: 0x04003D06 RID: 15622
	private HashedString buildingCategory;

	// Token: 0x04003D07 RID: 15623
	private TechItem techItem;

	// Token: 0x04003D08 RID: 15624
	private List<int> gameSubscriptions = new List<int>();

	// Token: 0x04003D09 RID: 15625
	private bool researchComplete;

	// Token: 0x04003D0A RID: 15626
	private Sprite sprite;

	// Token: 0x04003D0B RID: 15627
	[SerializeField]
	private MultiToggle toggle;

	// Token: 0x04003D0C RID: 15628
	[SerializeField]
	private ToolTip tooltip;

	// Token: 0x04003D0D RID: 15629
	[SerializeField]
	private LocText text;

	// Token: 0x04003D0E RID: 15630
	[SerializeField]
	private LocText text_listView;

	// Token: 0x04003D0F RID: 15631
	[SerializeField]
	private Image buildingIcon;

	// Token: 0x04003D10 RID: 15632
	[SerializeField]
	private Image buildingIcon_listView;

	// Token: 0x04003D11 RID: 15633
	[SerializeField]
	private Image fgIcon;

	// Token: 0x04003D12 RID: 15634
	[SerializeField]
	private PlanScreen planScreen;

	// Token: 0x04003D13 RID: 15635
	private StringEntry subcategoryName;
}
