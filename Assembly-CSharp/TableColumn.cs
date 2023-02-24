using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B07 RID: 2823
public class TableColumn : IRender1000ms
{
	// Token: 0x1700064A RID: 1610
	// (get) Token: 0x060056DB RID: 22235 RVA: 0x001F8C2E File Offset: 0x001F6E2E
	public bool isRevealed
	{
		get
		{
			return this.revealed == null || this.revealed();
		}
	}

	// Token: 0x060056DC RID: 22236 RVA: 0x001F8C48 File Offset: 0x001F6E48
	public TableColumn(Action<IAssignableIdentity, GameObject> on_load_action, Comparison<IAssignableIdentity> sort_comparison, Action<IAssignableIdentity, GameObject, ToolTip> on_tooltip = null, Action<IAssignableIdentity, GameObject, ToolTip> on_sort_tooltip = null, Func<bool> revealed = null, bool should_refresh_columns = false, string scrollerID = "")
	{
		this.on_load_action = on_load_action;
		this.sort_comparer = sort_comparison;
		this.on_tooltip = on_tooltip;
		this.on_sort_tooltip = on_sort_tooltip;
		this.revealed = revealed;
		this.scrollerID = scrollerID;
		if (should_refresh_columns)
		{
			SimAndRenderScheduler.instance.Add(this, false);
		}
	}

	// Token: 0x060056DD RID: 22237 RVA: 0x001F8CA4 File Offset: 0x001F6EA4
	protected string GetTooltip(ToolTip tool_tip_instance)
	{
		GameObject gameObject = tool_tip_instance.gameObject;
		HierarchyReferences component = tool_tip_instance.GetComponent<HierarchyReferences>();
		if (component != null && component.HasReference("Widget"))
		{
			gameObject = component.GetReference("Widget").gameObject;
		}
		TableRow tableRow = null;
		foreach (KeyValuePair<TableRow, GameObject> keyValuePair in this.widgets_by_row)
		{
			if (keyValuePair.Value == gameObject)
			{
				tableRow = keyValuePair.Key;
				break;
			}
		}
		if (tableRow != null && this.on_tooltip != null)
		{
			this.on_tooltip(tableRow.GetIdentity(), gameObject, tool_tip_instance);
		}
		return "";
	}

	// Token: 0x060056DE RID: 22238 RVA: 0x001F8D6C File Offset: 0x001F6F6C
	protected string GetSortTooltip(ToolTip sort_tooltip_instance)
	{
		GameObject gameObject = sort_tooltip_instance.transform.parent.gameObject;
		TableRow tableRow = null;
		foreach (KeyValuePair<TableRow, GameObject> keyValuePair in this.widgets_by_row)
		{
			if (keyValuePair.Value == gameObject)
			{
				tableRow = keyValuePair.Key;
				break;
			}
		}
		if (tableRow != null && this.on_sort_tooltip != null)
		{
			this.on_sort_tooltip(tableRow.GetIdentity(), gameObject, sort_tooltip_instance);
		}
		return "";
	}

	// Token: 0x1700064B RID: 1611
	// (get) Token: 0x060056DF RID: 22239 RVA: 0x001F8E10 File Offset: 0x001F7010
	public bool isDirty
	{
		get
		{
			return this.dirty;
		}
	}

	// Token: 0x060056E0 RID: 22240 RVA: 0x001F8E18 File Offset: 0x001F7018
	public bool ContainsWidget(GameObject widget)
	{
		return this.widgets_by_row.ContainsValue(widget);
	}

	// Token: 0x060056E1 RID: 22241 RVA: 0x001F8E26 File Offset: 0x001F7026
	public virtual GameObject GetMinionWidget(GameObject parent)
	{
		global::Debug.LogError("Table Column has no Widget prefab");
		return null;
	}

	// Token: 0x060056E2 RID: 22242 RVA: 0x001F8E33 File Offset: 0x001F7033
	public virtual GameObject GetHeaderWidget(GameObject parent)
	{
		global::Debug.LogError("Table Column has no Widget prefab");
		return null;
	}

	// Token: 0x060056E3 RID: 22243 RVA: 0x001F8E40 File Offset: 0x001F7040
	public virtual GameObject GetDefaultWidget(GameObject parent)
	{
		global::Debug.LogError("Table Column has no Widget prefab");
		return null;
	}

	// Token: 0x060056E4 RID: 22244 RVA: 0x001F8E4D File Offset: 0x001F704D
	public void Render1000ms(float dt)
	{
		this.MarkDirty(null, TableScreen.ResultValues.False);
	}

	// Token: 0x060056E5 RID: 22245 RVA: 0x001F8E57 File Offset: 0x001F7057
	public void MarkDirty(GameObject triggering_obj = null, TableScreen.ResultValues triggering_object_state = TableScreen.ResultValues.False)
	{
		this.dirty = true;
	}

	// Token: 0x060056E6 RID: 22246 RVA: 0x001F8E60 File Offset: 0x001F7060
	public void MarkClean()
	{
		this.dirty = false;
	}

	// Token: 0x04003AEA RID: 15082
	public Action<IAssignableIdentity, GameObject> on_load_action;

	// Token: 0x04003AEB RID: 15083
	public Action<IAssignableIdentity, GameObject, ToolTip> on_tooltip;

	// Token: 0x04003AEC RID: 15084
	public Action<IAssignableIdentity, GameObject, ToolTip> on_sort_tooltip;

	// Token: 0x04003AED RID: 15085
	public Comparison<IAssignableIdentity> sort_comparer;

	// Token: 0x04003AEE RID: 15086
	public Dictionary<TableRow, GameObject> widgets_by_row = new Dictionary<TableRow, GameObject>();

	// Token: 0x04003AEF RID: 15087
	public string scrollerID;

	// Token: 0x04003AF0 RID: 15088
	public TableScreen screen;

	// Token: 0x04003AF1 RID: 15089
	public MultiToggle column_sort_toggle;

	// Token: 0x04003AF2 RID: 15090
	private Func<bool> revealed;

	// Token: 0x04003AF3 RID: 15091
	protected bool dirty;
}
