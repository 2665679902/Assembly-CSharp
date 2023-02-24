using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AFF RID: 2815
public class PrioritizationGroupTableColumn : TableColumn
{
	// Token: 0x06005660 RID: 22112 RVA: 0x001F45E0 File Offset: 0x001F27E0
	public PrioritizationGroupTableColumn(object user_data, Action<IAssignableIdentity, GameObject> on_load_action, Action<object, int> on_change_priority, Func<object, string> on_hover_widget, Action<object, int> on_change_header_priority, Func<object, string> on_hover_header_option_selector, Action<object> on_sort_clicked, Func<object, string> on_sort_hovered)
		: base(on_load_action, null, null, null, null, false, "")
	{
		this.userData = user_data;
		this.onChangePriority = on_change_priority;
		this.onHoverWidget = on_hover_widget;
		this.onHoverHeaderOptionSelector = on_hover_header_option_selector;
		this.onSortClicked = on_sort_clicked;
		this.onSortHovered = on_sort_hovered;
	}

	// Token: 0x06005661 RID: 22113 RVA: 0x001F462C File Offset: 0x001F282C
	public override GameObject GetMinionWidget(GameObject parent)
	{
		return this.GetWidget(parent);
	}

	// Token: 0x06005662 RID: 22114 RVA: 0x001F4635 File Offset: 0x001F2835
	public override GameObject GetDefaultWidget(GameObject parent)
	{
		return this.GetWidget(parent);
	}

	// Token: 0x06005663 RID: 22115 RVA: 0x001F4640 File Offset: 0x001F2840
	private GameObject GetWidget(GameObject parent)
	{
		GameObject widget_go = Util.KInstantiateUI(Assets.UIPrefabs.TableScreenWidgets.PriorityGroupSelector, parent, true);
		OptionSelector component = widget_go.GetComponent<OptionSelector>();
		component.Initialize(widget_go);
		component.OnChangePriority = delegate(object widget, int delta)
		{
			this.onChangePriority(widget, delta);
		};
		ToolTip[] componentsInChildren = widget_go.transform.GetComponentsInChildren<ToolTip>();
		if (componentsInChildren != null)
		{
			Func<string> <>9__1;
			foreach (ToolTip toolTip in componentsInChildren)
			{
				Func<string> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = () => this.onHoverWidget(widget_go));
				}
				toolTip.OnToolTip = func;
			}
		}
		return widget_go;
	}

	// Token: 0x06005664 RID: 22116 RVA: 0x001F46F4 File Offset: 0x001F28F4
	public override GameObject GetHeaderWidget(GameObject parent)
	{
		GameObject widget_go = Util.KInstantiateUI(Assets.UIPrefabs.TableScreenWidgets.PriorityGroupSelectorHeader, parent, true);
		HierarchyReferences component = widget_go.GetComponent<HierarchyReferences>();
		LayoutElement component2 = widget_go.GetComponentInChildren<LocText>().GetComponent<LayoutElement>();
		component2.preferredWidth = (component2.minWidth = 63f);
		Component reference = component.GetReference("Label");
		reference.GetComponent<LocText>().raycastTarget = true;
		ToolTip component3 = reference.GetComponent<ToolTip>();
		if (component3 != null)
		{
			component3.OnToolTip = () => this.onHoverWidget(widget_go);
		}
		MultiToggle componentInChildren = widget_go.GetComponentInChildren<MultiToggle>(true);
		this.column_sort_toggle = componentInChildren;
		MultiToggle multiToggle = componentInChildren;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
		{
			this.onSortClicked(widget_go);
		}));
		ToolTip component4 = componentInChildren.GetComponent<ToolTip>();
		if (component4 != null)
		{
			component4.OnToolTip = () => this.onSortHovered(widget_go);
		}
		ToolTip component5 = (component.GetReference("PrioritizeButton") as KButton).GetComponent<ToolTip>();
		if (component5 != null)
		{
			component5.OnToolTip = () => this.onHoverHeaderOptionSelector(widget_go);
		}
		return widget_go;
	}

	// Token: 0x04003ACE RID: 15054
	public object userData;

	// Token: 0x04003ACF RID: 15055
	private Action<object, int> onChangePriority;

	// Token: 0x04003AD0 RID: 15056
	private Func<object, string> onHoverWidget;

	// Token: 0x04003AD1 RID: 15057
	private Func<object, string> onHoverHeaderOptionSelector;

	// Token: 0x04003AD2 RID: 15058
	private Action<object> onSortClicked;

	// Token: 0x04003AD3 RID: 15059
	private Func<object, string> onSortHovered;
}
