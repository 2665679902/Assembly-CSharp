using System;
using UnityEngine;

// Token: 0x02000AF9 RID: 2809
public class CheckboxTableColumn : TableColumn
{
	// Token: 0x06005647 RID: 22087 RVA: 0x001F3A68 File Offset: 0x001F1C68
	public CheckboxTableColumn(Action<IAssignableIdentity, GameObject> on_load_action, Func<IAssignableIdentity, GameObject, TableScreen.ResultValues> get_value_action, Action<GameObject> on_press_action, Action<GameObject, TableScreen.ResultValues> set_value_action, Comparison<IAssignableIdentity> sort_comparer, Action<IAssignableIdentity, GameObject, ToolTip> on_tooltip, Action<IAssignableIdentity, GameObject, ToolTip> on_sort_tooltip, Func<bool> revealed = null)
		: base(on_load_action, sort_comparer, on_tooltip, on_sort_tooltip, revealed, false, "")
	{
		this.get_value_action = get_value_action;
		this.on_press_action = on_press_action;
		this.on_set_action = set_value_action;
	}

	// Token: 0x06005648 RID: 22088 RVA: 0x001F3ACC File Offset: 0x001F1CCC
	public override GameObject GetMinionWidget(GameObject parent)
	{
		GameObject widget_go = Util.KInstantiateUI(this.prefab_checkbox, parent, true);
		if (widget_go.GetComponent<ToolTip>() != null)
		{
			widget_go.GetComponent<ToolTip>().OnToolTip = () => this.GetTooltip(widget_go.GetComponent<ToolTip>());
		}
		MultiToggle component = widget_go.GetComponent<MultiToggle>();
		component.onClick = (System.Action)Delegate.Combine(component.onClick, new System.Action(delegate
		{
			this.on_press_action(widget_go);
		}));
		return widget_go;
	}

	// Token: 0x06005649 RID: 22089 RVA: 0x001F3B5C File Offset: 0x001F1D5C
	public override GameObject GetDefaultWidget(GameObject parent)
	{
		GameObject widget_go = Util.KInstantiateUI(this.prefab_checkbox, parent, true);
		if (widget_go.GetComponent<ToolTip>() != null)
		{
			widget_go.GetComponent<ToolTip>().OnToolTip = () => this.GetTooltip(widget_go.GetComponent<ToolTip>());
		}
		MultiToggle component = widget_go.GetComponent<MultiToggle>();
		component.onClick = (System.Action)Delegate.Combine(component.onClick, new System.Action(delegate
		{
			this.on_press_action(widget_go);
		}));
		return widget_go;
	}

	// Token: 0x0600564A RID: 22090 RVA: 0x001F3BEC File Offset: 0x001F1DEC
	public override GameObject GetHeaderWidget(GameObject parent)
	{
		ToolTip tooltip = null;
		GameObject widget_go = Util.KInstantiateUI(this.prefab_header_portrait_checkbox, parent, true);
		tooltip = widget_go.GetComponent<ToolTip>();
		HierarchyReferences component = widget_go.GetComponent<HierarchyReferences>();
		if (tooltip == null && component != null && component.HasReference("ToolTip"))
		{
			tooltip = component.GetReference("ToolTip") as ToolTip;
		}
		tooltip.OnToolTip = () => this.GetTooltip(tooltip);
		MultiToggle multiToggle = component.GetReference("Toggle") as MultiToggle;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
		{
			this.on_press_action(widget_go);
		}));
		MultiToggle componentInChildren = widget_go.GetComponentInChildren<MultiToggle>();
		MultiToggle multiToggle2 = componentInChildren;
		multiToggle2.onClick = (System.Action)Delegate.Combine(multiToggle2.onClick, new System.Action(delegate
		{
			this.screen.SetSortComparison(this.sort_comparer, this);
			this.screen.SortRows();
		}));
		ToolTip sort_tooltip = componentInChildren.GetComponent<ToolTip>();
		sort_tooltip.OnToolTip = () => this.GetSortTooltip(sort_tooltip);
		this.column_sort_toggle = widget_go.GetComponentInChildren<MultiToggle>();
		return widget_go;
	}

	// Token: 0x04003ABC RID: 15036
	public GameObject prefab_header_portrait_checkbox = Assets.UIPrefabs.TableScreenWidgets.TogglePortrait;

	// Token: 0x04003ABD RID: 15037
	public GameObject prefab_checkbox = Assets.UIPrefabs.TableScreenWidgets.Checkbox;

	// Token: 0x04003ABE RID: 15038
	public Action<GameObject> on_press_action;

	// Token: 0x04003ABF RID: 15039
	public Action<GameObject, TableScreen.ResultValues> on_set_action;

	// Token: 0x04003AC0 RID: 15040
	public Func<IAssignableIdentity, GameObject, TableScreen.ResultValues> get_value_action;
}
