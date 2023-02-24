using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AFD RID: 2813
public class NumericDropDownTableColumn : TableColumn
{
	// Token: 0x06005657 RID: 22103 RVA: 0x001F428D File Offset: 0x001F248D
	public NumericDropDownTableColumn(object user_data, List<TMP_Dropdown.OptionData> options, Action<IAssignableIdentity, GameObject> on_load_action, Action<GameObject, int> set_value_action, Comparison<IAssignableIdentity> sort_comparer, NumericDropDownTableColumn.ToolTipCallbacks callbacks, Func<bool> revealed = null)
		: base(on_load_action, sort_comparer, callbacks.headerTooltip, callbacks.headerSortTooltip, revealed, false, "")
	{
		this.userData = user_data;
		this.set_value_action = set_value_action;
		this.options = options;
		this.callbacks = callbacks;
	}

	// Token: 0x06005658 RID: 22104 RVA: 0x001F42CC File Offset: 0x001F24CC
	public override GameObject GetMinionWidget(GameObject parent)
	{
		return this.GetWidget(parent);
	}

	// Token: 0x06005659 RID: 22105 RVA: 0x001F42D5 File Offset: 0x001F24D5
	public override GameObject GetDefaultWidget(GameObject parent)
	{
		return this.GetWidget(parent);
	}

	// Token: 0x0600565A RID: 22106 RVA: 0x001F42E0 File Offset: 0x001F24E0
	private GameObject GetWidget(GameObject parent)
	{
		GameObject widget_go = Util.KInstantiateUI(Assets.UIPrefabs.TableScreenWidgets.NumericDropDown, parent, true);
		TMP_Dropdown componentInChildren = widget_go.transform.GetComponentInChildren<TMP_Dropdown>();
		componentInChildren.options = this.options;
		componentInChildren.onValueChanged.AddListener(delegate(int new_value)
		{
			this.set_value_action(widget_go, new_value);
		});
		ToolTip tt = widget_go.transform.GetComponentInChildren<ToolTip>();
		if (tt != null)
		{
			tt.OnToolTip = () => this.GetTooltip(tt);
		}
		return widget_go;
	}

	// Token: 0x0600565B RID: 22107 RVA: 0x001F438C File Offset: 0x001F258C
	public override GameObject GetHeaderWidget(GameObject parent)
	{
		NumericDropDownTableColumn.<>c__DisplayClass9_0 CS$<>8__locals1 = new NumericDropDownTableColumn.<>c__DisplayClass9_0();
		CS$<>8__locals1.<>4__this = this;
		CS$<>8__locals1.widget_go = Util.KInstantiateUI(Assets.UIPrefabs.TableScreenWidgets.DropDownHeader, parent, true);
		HierarchyReferences component = CS$<>8__locals1.widget_go.GetComponent<HierarchyReferences>();
		Component reference = component.GetReference("Label");
		MultiToggle componentInChildren = reference.GetComponentInChildren<MultiToggle>(true);
		this.column_sort_toggle = componentInChildren;
		MultiToggle multiToggle = componentInChildren;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
		{
			CS$<>8__locals1.<>4__this.screen.SetSortComparison(CS$<>8__locals1.<>4__this.sort_comparer, CS$<>8__locals1.<>4__this);
			CS$<>8__locals1.<>4__this.screen.SortRows();
		}));
		ToolTip tt2 = reference.GetComponent<ToolTip>();
		tt2.enabled = true;
		tt2.OnToolTip = delegate
		{
			CS$<>8__locals1.<>4__this.callbacks.headerTooltip(null, CS$<>8__locals1.widget_go, tt2);
			return "";
		};
		ToolTip tt3 = componentInChildren.transform.GetComponent<ToolTip>();
		tt3.OnToolTip = delegate
		{
			CS$<>8__locals1.<>4__this.callbacks.headerSortTooltip(null, CS$<>8__locals1.widget_go, tt3);
			return "";
		};
		Component reference2 = component.GetReference("DropDown");
		TMP_Dropdown componentInChildren2 = reference2.GetComponentInChildren<TMP_Dropdown>();
		componentInChildren2.options = this.options;
		componentInChildren2.onValueChanged.AddListener(delegate(int new_value)
		{
			CS$<>8__locals1.<>4__this.set_value_action(CS$<>8__locals1.widget_go, new_value);
		});
		ToolTip tt = reference2.GetComponent<ToolTip>();
		tt.OnToolTip = delegate
		{
			CS$<>8__locals1.<>4__this.callbacks.headerDropdownTooltip(null, CS$<>8__locals1.widget_go, tt);
			return "";
		};
		LayoutElement component2 = CS$<>8__locals1.widget_go.GetComponentInChildren<LocText>().GetComponent<LayoutElement>();
		component2.preferredWidth = (component2.minWidth = 83f);
		return CS$<>8__locals1.widget_go;
	}

	// Token: 0x04003AC8 RID: 15048
	public object userData;

	// Token: 0x04003AC9 RID: 15049
	private NumericDropDownTableColumn.ToolTipCallbacks callbacks;

	// Token: 0x04003ACA RID: 15050
	private Action<GameObject, int> set_value_action;

	// Token: 0x04003ACB RID: 15051
	private List<TMP_Dropdown.OptionData> options;

	// Token: 0x0200198B RID: 6539
	public class ToolTipCallbacks
	{
		// Token: 0x0400749F RID: 29855
		public Action<IAssignableIdentity, GameObject, ToolTip> headerTooltip;

		// Token: 0x040074A0 RID: 29856
		public Action<IAssignableIdentity, GameObject, ToolTip> headerSortTooltip;

		// Token: 0x040074A1 RID: 29857
		public Action<IAssignableIdentity, GameObject, ToolTip> headerDropdownTooltip;
	}
}
