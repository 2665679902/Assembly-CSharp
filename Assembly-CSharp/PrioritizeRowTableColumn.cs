using System;
using UnityEngine;

// Token: 0x02000B00 RID: 2816
public class PrioritizeRowTableColumn : TableColumn
{
	// Token: 0x06005665 RID: 22117 RVA: 0x001F4824 File Offset: 0x001F2A24
	public PrioritizeRowTableColumn(object user_data, Action<object, int> on_change_priority, Func<object, int, string> on_hover_widget)
		: base(null, null, null, null, null, false, "")
	{
		this.userData = user_data;
		this.onChangePriority = on_change_priority;
		this.onHoverWidget = on_hover_widget;
	}

	// Token: 0x06005666 RID: 22118 RVA: 0x001F484C File Offset: 0x001F2A4C
	public override GameObject GetMinionWidget(GameObject parent)
	{
		return this.GetWidget(parent);
	}

	// Token: 0x06005667 RID: 22119 RVA: 0x001F4855 File Offset: 0x001F2A55
	public override GameObject GetDefaultWidget(GameObject parent)
	{
		return this.GetWidget(parent);
	}

	// Token: 0x06005668 RID: 22120 RVA: 0x001F485E File Offset: 0x001F2A5E
	public override GameObject GetHeaderWidget(GameObject parent)
	{
		return Util.KInstantiateUI(Assets.UIPrefabs.TableScreenWidgets.PrioritizeRowHeaderWidget, parent, true);
	}

	// Token: 0x06005669 RID: 22121 RVA: 0x001F4878 File Offset: 0x001F2A78
	private GameObject GetWidget(GameObject parent)
	{
		GameObject gameObject = Util.KInstantiateUI(Assets.UIPrefabs.TableScreenWidgets.PrioritizeRowWidget, parent, true);
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		this.ConfigureButton(component, "UpButton", 1, gameObject);
		this.ConfigureButton(component, "DownButton", -1, gameObject);
		return gameObject;
	}

	// Token: 0x0600566A RID: 22122 RVA: 0x001F48C0 File Offset: 0x001F2AC0
	private void ConfigureButton(HierarchyReferences refs, string ref_id, int delta, GameObject widget_go)
	{
		KButton kbutton = refs.GetReference(ref_id) as KButton;
		kbutton.onClick += delegate
		{
			this.onChangePriority(widget_go, delta);
		};
		ToolTip component = kbutton.GetComponent<ToolTip>();
		if (component != null)
		{
			component.OnToolTip = () => this.onHoverWidget(widget_go, delta);
		}
	}

	// Token: 0x04003AD4 RID: 15060
	public object userData;

	// Token: 0x04003AD5 RID: 15061
	private Action<object, int> onChangePriority;

	// Token: 0x04003AD6 RID: 15062
	private Func<object, int, string> onHoverWidget;
}
