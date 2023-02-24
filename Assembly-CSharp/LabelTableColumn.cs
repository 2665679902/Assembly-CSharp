using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AFB RID: 2811
public class LabelTableColumn : TableColumn
{
	// Token: 0x0600564F RID: 22095 RVA: 0x001F3F12 File Offset: 0x001F2112
	public LabelTableColumn(Action<IAssignableIdentity, GameObject> on_load_action, Func<IAssignableIdentity, GameObject, string> get_value_action, Comparison<IAssignableIdentity> sort_comparison, Action<IAssignableIdentity, GameObject, ToolTip> on_tooltip, Action<IAssignableIdentity, GameObject, ToolTip> on_sort_tooltip, int widget_width = 128, bool should_refresh_columns = false)
		: base(on_load_action, sort_comparison, on_tooltip, on_sort_tooltip, null, should_refresh_columns, "")
	{
		this.get_value_action = get_value_action;
		this.widget_width = widget_width;
	}

	// Token: 0x06005650 RID: 22096 RVA: 0x001F3F44 File Offset: 0x001F2144
	public override GameObject GetDefaultWidget(GameObject parent)
	{
		GameObject gameObject = Util.KInstantiateUI(Assets.UIPrefabs.TableScreenWidgets.Label, parent, true);
		LayoutElement component = gameObject.GetComponentInChildren<LocText>().GetComponent<LayoutElement>();
		component.preferredWidth = (component.minWidth = (float)this.widget_width);
		return gameObject;
	}

	// Token: 0x06005651 RID: 22097 RVA: 0x001F3F88 File Offset: 0x001F2188
	public override GameObject GetMinionWidget(GameObject parent)
	{
		GameObject gameObject = Util.KInstantiateUI(Assets.UIPrefabs.TableScreenWidgets.Label, parent, true);
		ToolTip tt = gameObject.GetComponent<ToolTip>();
		tt.OnToolTip = () => this.GetTooltip(tt);
		LayoutElement component = gameObject.GetComponentInChildren<LocText>().GetComponent<LayoutElement>();
		component.preferredWidth = (component.minWidth = (float)this.widget_width);
		return gameObject;
	}

	// Token: 0x06005652 RID: 22098 RVA: 0x001F4000 File Offset: 0x001F2200
	public override GameObject GetHeaderWidget(GameObject parent)
	{
		GameObject widget_go = null;
		widget_go = Util.KInstantiateUI(Assets.UIPrefabs.TableScreenWidgets.LabelHeader, parent, true);
		MultiToggle componentInChildren = widget_go.GetComponentInChildren<MultiToggle>(true);
		this.column_sort_toggle = componentInChildren;
		MultiToggle multiToggle = componentInChildren;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
		{
			this.screen.SetSortComparison(this.sort_comparer, this);
			this.screen.SortRows();
		}));
		ToolTip tt = widget_go.GetComponent<ToolTip>();
		tt.OnToolTip = delegate
		{
			this.on_tooltip(null, widget_go, tt);
			return "";
		};
		tt = widget_go.GetComponentInChildren<MultiToggle>().GetComponent<ToolTip>();
		tt.OnToolTip = delegate
		{
			this.on_sort_tooltip(null, widget_go, tt);
			return "";
		};
		LayoutElement component = widget_go.GetComponentInChildren<LocText>().GetComponent<LayoutElement>();
		component.preferredWidth = (component.minWidth = (float)this.widget_width);
		return widget_go;
	}

	// Token: 0x04003AC3 RID: 15043
	public Func<IAssignableIdentity, GameObject, string> get_value_action;

	// Token: 0x04003AC4 RID: 15044
	private int widget_width = 128;
}
