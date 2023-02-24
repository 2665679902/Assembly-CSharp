using System;
using UnityEngine;

// Token: 0x02000AFA RID: 2810
public class SuperCheckboxTableColumn : CheckboxTableColumn
{
	// Token: 0x0600564B RID: 22091 RVA: 0x001F3D28 File Offset: 0x001F1F28
	public SuperCheckboxTableColumn(CheckboxTableColumn[] columns_affected, Action<IAssignableIdentity, GameObject> on_load_action, Func<IAssignableIdentity, GameObject, TableScreen.ResultValues> get_value_action, Action<GameObject> on_press_action, Action<GameObject, TableScreen.ResultValues> set_value_action, Comparison<IAssignableIdentity> sort_comparison, Action<IAssignableIdentity, GameObject, ToolTip> on_tooltip)
		: base(on_load_action, get_value_action, on_press_action, set_value_action, sort_comparison, on_tooltip, null, null)
	{
		this.columns_affected = columns_affected;
	}

	// Token: 0x0600564C RID: 22092 RVA: 0x001F3D64 File Offset: 0x001F1F64
	public override GameObject GetDefaultWidget(GameObject parent)
	{
		GameObject widget_go = Util.KInstantiateUI(this.prefab_super_checkbox, parent, true);
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

	// Token: 0x0600564D RID: 22093 RVA: 0x001F3DF4 File Offset: 0x001F1FF4
	public override GameObject GetHeaderWidget(GameObject parent)
	{
		GameObject widget_go = Util.KInstantiateUI(this.prefab_super_checkbox, parent, true);
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

	// Token: 0x0600564E RID: 22094 RVA: 0x001F3E84 File Offset: 0x001F2084
	public override GameObject GetMinionWidget(GameObject parent)
	{
		GameObject widget_go = Util.KInstantiateUI(this.prefab_super_checkbox, parent, true);
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

	// Token: 0x04003AC1 RID: 15041
	public GameObject prefab_super_checkbox = Assets.UIPrefabs.TableScreenWidgets.SuperCheckbox_Horizontal;

	// Token: 0x04003AC2 RID: 15042
	public CheckboxTableColumn[] columns_affected;
}
