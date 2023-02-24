using System;
using UnityEngine;

// Token: 0x02000AFC RID: 2812
public class ButtonLabelColumn : LabelTableColumn
{
	// Token: 0x06005653 RID: 22099 RVA: 0x001F40F3 File Offset: 0x001F22F3
	public ButtonLabelColumn(Action<IAssignableIdentity, GameObject> on_load_action, Func<IAssignableIdentity, GameObject, string> get_value_action, Action<GameObject> on_click_action, Action<GameObject> on_double_click_action, Comparison<IAssignableIdentity> sort_comparison, Action<IAssignableIdentity, GameObject, ToolTip> on_tooltip, Action<IAssignableIdentity, GameObject, ToolTip> on_sort_tooltip, bool whiteText = false)
		: base(on_load_action, get_value_action, sort_comparison, on_tooltip, on_sort_tooltip, 128, false)
	{
		this.on_click_action = on_click_action;
		this.on_double_click_action = on_double_click_action;
		this.whiteText = whiteText;
	}

	// Token: 0x06005654 RID: 22100 RVA: 0x001F4120 File Offset: 0x001F2320
	public override GameObject GetDefaultWidget(GameObject parent)
	{
		GameObject widget_go = Util.KInstantiateUI(this.whiteText ? Assets.UIPrefabs.TableScreenWidgets.ButtonLabelWhite : Assets.UIPrefabs.TableScreenWidgets.ButtonLabel, parent, true);
		if (this.on_click_action != null)
		{
			widget_go.GetComponent<KButton>().onClick += delegate
			{
				this.on_click_action(widget_go);
			};
		}
		if (this.on_double_click_action != null)
		{
			widget_go.GetComponent<KButton>().onDoubleClick += delegate
			{
				this.on_double_click_action(widget_go);
			};
		}
		return widget_go;
	}

	// Token: 0x06005655 RID: 22101 RVA: 0x001F41BD File Offset: 0x001F23BD
	public override GameObject GetHeaderWidget(GameObject parent)
	{
		return base.GetHeaderWidget(parent);
	}

	// Token: 0x06005656 RID: 22102 RVA: 0x001F41C8 File Offset: 0x001F23C8
	public override GameObject GetMinionWidget(GameObject parent)
	{
		GameObject widget_go = Util.KInstantiateUI(this.whiteText ? Assets.UIPrefabs.TableScreenWidgets.ButtonLabelWhite : Assets.UIPrefabs.TableScreenWidgets.ButtonLabel, parent, true);
		ToolTip tt = widget_go.GetComponent<ToolTip>();
		tt.OnToolTip = () => this.GetTooltip(tt);
		if (this.on_click_action != null)
		{
			widget_go.GetComponent<KButton>().onClick += delegate
			{
				this.on_click_action(widget_go);
			};
		}
		if (this.on_double_click_action != null)
		{
			widget_go.GetComponent<KButton>().onDoubleClick += delegate
			{
				this.on_double_click_action(widget_go);
			};
		}
		return widget_go;
	}

	// Token: 0x04003AC5 RID: 15045
	private Action<GameObject> on_click_action;

	// Token: 0x04003AC6 RID: 15046
	private Action<GameObject> on_double_click_action;

	// Token: 0x04003AC7 RID: 15047
	private bool whiteText;
}
