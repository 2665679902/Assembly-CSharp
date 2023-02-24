using System;
using System.Collections;
using System.Collections.Generic;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B0A RID: 2826
public class TableScreen : ShowOptimizedKScreen
{
	// Token: 0x060056F9 RID: 22265 RVA: 0x001F9805 File Offset: 0x001F7A05
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.removeWorldHandle = ClusterManager.Instance.Subscribe(-1078710002, new Action<object>(this.RemoveWorldDivider));
	}

	// Token: 0x060056FA RID: 22266 RVA: 0x001F9830 File Offset: 0x001F7A30
	protected override void OnActivate()
	{
		base.OnActivate();
		this.title_bar.text = this.title;
		base.ConsumeMouseScroll = true;
		this.CloseButton.onClick += delegate
		{
			ManagementMenu.Instance.CloseAll();
		};
		this.incubating = true;
		base.transform.rectTransform().localScale = Vector3.zero;
		Components.LiveMinionIdentities.OnAdd += delegate(MinionIdentity param)
		{
			this.MarkRowsDirty();
		};
		Components.LiveMinionIdentities.OnRemove += delegate(MinionIdentity param)
		{
			this.MarkRowsDirty();
		};
	}

	// Token: 0x060056FB RID: 22267 RVA: 0x001F98CD File Offset: 0x001F7ACD
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.removeWorldHandle != -1)
		{
			ClusterManager.Instance.Unsubscribe(this.removeWorldHandle);
		}
	}

	// Token: 0x060056FC RID: 22268 RVA: 0x001F98EE File Offset: 0x001F7AEE
	protected override void OnShow(bool show)
	{
		if (!show)
		{
			this.active_cascade_coroutine_count = 0;
			base.StopAllCoroutines();
			this.StopLoopingCascadeSound();
		}
		this.ZeroScrollers();
		base.OnShow(show);
		if (show)
		{
			this.MarkRowsDirty();
		}
	}

	// Token: 0x060056FD RID: 22269 RVA: 0x001F991C File Offset: 0x001F7B1C
	private void ZeroScrollers()
	{
		if (this.rows.Count > 0)
		{
			foreach (string text in this.column_scrollers)
			{
				foreach (TableRow tableRow in this.rows)
				{
					tableRow.GetScroller(text).transform.parent.GetComponent<ScrollRect>().horizontalNormalizedPosition = 0f;
				}
			}
			foreach (KeyValuePair<int, GameObject> keyValuePair in this.worldDividers)
			{
				ScrollRect componentInChildren = keyValuePair.Value.GetComponentInChildren<ScrollRect>();
				if (componentInChildren != null)
				{
					componentInChildren.horizontalNormalizedPosition = 0f;
				}
			}
		}
	}

	// Token: 0x060056FE RID: 22270 RVA: 0x001F9A34 File Offset: 0x001F7C34
	public bool CheckScrollersDirty()
	{
		return this.scrollersDirty;
	}

	// Token: 0x060056FF RID: 22271 RVA: 0x001F9A3C File Offset: 0x001F7C3C
	public void SetScrollersDirty(float position)
	{
		this.targetScrollerPosition = position;
		this.scrollersDirty = true;
		this.PositionScrollers();
	}

	// Token: 0x06005700 RID: 22272 RVA: 0x001F9A54 File Offset: 0x001F7C54
	public void PositionScrollers()
	{
		foreach (TableRow tableRow in this.rows)
		{
			ScrollRect componentInChildren = tableRow.GetComponentInChildren<ScrollRect>();
			if (componentInChildren != null)
			{
				componentInChildren.horizontalNormalizedPosition = this.targetScrollerPosition;
			}
		}
		foreach (KeyValuePair<int, GameObject> keyValuePair in this.worldDividers)
		{
			if (keyValuePair.Value.activeInHierarchy)
			{
				ScrollRect componentInChildren2 = keyValuePair.Value.GetComponentInChildren<ScrollRect>();
				if (componentInChildren2 != null)
				{
					componentInChildren2.horizontalNormalizedPosition = this.targetScrollerPosition;
				}
			}
		}
		this.scrollersDirty = false;
	}

	// Token: 0x06005701 RID: 22273 RVA: 0x001F9B30 File Offset: 0x001F7D30
	public override void ScreenUpdate(bool topLevel)
	{
		if (this.isHiddenButActive)
		{
			return;
		}
		base.ScreenUpdate(topLevel);
		if (this.incubating)
		{
			this.ZeroScrollers();
			base.transform.rectTransform().localScale = Vector3.one;
			this.incubating = false;
		}
		if (this.rows_dirty)
		{
			this.RefreshRows();
		}
		foreach (TableRow tableRow in this.rows)
		{
			tableRow.RefreshScrollers();
		}
		foreach (TableColumn tableColumn in this.columns.Values)
		{
			if (tableColumn.isDirty)
			{
				foreach (KeyValuePair<TableRow, GameObject> keyValuePair in tableColumn.widgets_by_row)
				{
					tableColumn.on_load_action(keyValuePair.Key.GetIdentity(), keyValuePair.Value);
					tableColumn.MarkClean();
				}
			}
		}
	}

	// Token: 0x06005702 RID: 22274 RVA: 0x001F9C70 File Offset: 0x001F7E70
	protected void MarkRowsDirty()
	{
		this.rows_dirty = true;
	}

	// Token: 0x06005703 RID: 22275 RVA: 0x001F9C7C File Offset: 0x001F7E7C
	protected virtual void RefreshRows()
	{
		this.ObsoleteRows();
		this.AddRow(null);
		if (this.has_default_duplicant_row)
		{
			this.AddDefaultRow();
		}
		for (int i = 0; i < Components.LiveMinionIdentities.Count; i++)
		{
			if (Components.LiveMinionIdentities[i] != null)
			{
				this.AddRow(Components.LiveMinionIdentities[i]);
			}
		}
		foreach (MinionStorage minionStorage in Components.MinionStorages.Items)
		{
			foreach (MinionStorage.Info info in minionStorage.GetStoredMinionInfo())
			{
				if (info.serializedMinion != null)
				{
					StoredMinionIdentity storedMinionIdentity = info.serializedMinion.Get<StoredMinionIdentity>();
					this.AddRow(storedMinionIdentity);
				}
			}
		}
		foreach (int num in ClusterManager.Instance.GetWorldIDsSorted())
		{
			this.AddWorldDivider(num);
		}
		foreach (KeyValuePair<int, bool> keyValuePair in this.obsoleteWorldDividerStatus)
		{
			if (keyValuePair.Value)
			{
				this.RemoveWorldDivider(keyValuePair.Key);
			}
		}
		this.obsoleteWorldDividerStatus.Clear();
		foreach (KeyValuePair<int, GameObject> keyValuePair2 in this.worldDividers)
		{
			Component reference = keyValuePair2.Value.GetComponent<HierarchyReferences>().GetReference("NobodyRow");
			reference.gameObject.SetActive(true);
			foreach (object obj in Components.MinionAssignablesProxy)
			{
				MinionAssignablesProxy minionAssignablesProxy = (MinionAssignablesProxy)obj;
				if (minionAssignablesProxy != null && minionAssignablesProxy.GetTargetGameObject() != null && minionAssignablesProxy.GetTargetGameObject().GetMyWorld().id == keyValuePair2.Key)
				{
					reference.gameObject.SetActive(false);
					break;
				}
			}
			keyValuePair2.Value.SetActive(ClusterManager.Instance.GetWorld(keyValuePair2.Key).IsDiscovered && DlcManager.FeatureClusterSpaceEnabled());
		}
		using (Dictionary<IAssignableIdentity, bool>.Enumerator enumerator7 = this.obsoleteMinionRowStatus.GetEnumerator())
		{
			while (enumerator7.MoveNext())
			{
				KeyValuePair<IAssignableIdentity, bool> kvp = enumerator7.Current;
				if (kvp.Value)
				{
					int num2 = this.rows.FindIndex((TableRow match) => match.GetIdentity() == kvp.Key);
					TableRow tableRow = this.rows[num2];
					this.rows[num2].Clear();
					this.rows.RemoveAt(num2);
					this.all_sortable_rows.Remove(tableRow);
				}
			}
		}
		this.obsoleteMinionRowStatus.Clear();
		this.SortRows();
		this.rows_dirty = false;
	}

	// Token: 0x06005704 RID: 22276 RVA: 0x001FA008 File Offset: 0x001F8208
	public virtual void SetSortComparison(Comparison<IAssignableIdentity> comparison, TableColumn sort_column)
	{
		if (comparison == null)
		{
			return;
		}
		if (this.active_sort_column != sort_column)
		{
			this.active_sort_column = sort_column;
			this.active_sort_method = comparison;
			this.sort_is_reversed = false;
			return;
		}
		if (this.sort_is_reversed)
		{
			this.sort_is_reversed = false;
			this.active_sort_method = null;
			this.active_sort_column = null;
			return;
		}
		this.sort_is_reversed = true;
	}

	// Token: 0x06005705 RID: 22277 RVA: 0x001FA060 File Offset: 0x001F8260
	public void SortRows()
	{
		foreach (TableColumn tableColumn in this.columns.Values)
		{
			if (!(tableColumn.column_sort_toggle == null))
			{
				if (tableColumn == this.active_sort_column)
				{
					if (this.sort_is_reversed)
					{
						tableColumn.column_sort_toggle.ChangeState(2);
					}
					else
					{
						tableColumn.column_sort_toggle.ChangeState(1);
					}
				}
				else
				{
					tableColumn.column_sort_toggle.ChangeState(0);
				}
			}
		}
		Dictionary<IAssignableIdentity, TableRow> dictionary = new Dictionary<IAssignableIdentity, TableRow>();
		foreach (TableRow tableRow in this.all_sortable_rows)
		{
			dictionary.Add(tableRow.GetIdentity(), tableRow);
		}
		Dictionary<int, List<IAssignableIdentity>> dictionary2 = new Dictionary<int, List<IAssignableIdentity>>();
		foreach (KeyValuePair<IAssignableIdentity, TableRow> keyValuePair in dictionary)
		{
			int id = keyValuePair.Key.GetSoleOwner().GetComponent<MinionAssignablesProxy>().GetTargetGameObject()
				.GetComponent<KMonoBehaviour>()
				.GetMyWorld()
				.id;
			if (!dictionary2.ContainsKey(id))
			{
				dictionary2.Add(id, new List<IAssignableIdentity>());
			}
			dictionary2[id].Add(keyValuePair.Key);
		}
		this.all_sortable_rows.Clear();
		Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
		int num = 0;
		int num2 = 0;
		foreach (KeyValuePair<int, List<IAssignableIdentity>> keyValuePair2 in dictionary2)
		{
			dictionary3.Add(keyValuePair2.Key, num);
			num++;
			List<IAssignableIdentity> list = new List<IAssignableIdentity>();
			foreach (IAssignableIdentity assignableIdentity in keyValuePair2.Value)
			{
				list.Add(assignableIdentity);
			}
			if (this.active_sort_method != null)
			{
				list.Sort(this.active_sort_method);
				if (this.sort_is_reversed)
				{
					list.Reverse();
				}
			}
			num += list.Count;
			num2 += list.Count;
			for (int i = 0; i < list.Count; i++)
			{
				this.all_sortable_rows.Add(dictionary[list[i]]);
			}
		}
		for (int j = 0; j < this.all_sortable_rows.Count; j++)
		{
			this.all_sortable_rows[j].gameObject.transform.SetSiblingIndex(j);
		}
		foreach (KeyValuePair<int, int> keyValuePair3 in dictionary3)
		{
			this.worldDividers[keyValuePair3.Key].transform.SetSiblingIndex(keyValuePair3.Value);
		}
		if (this.has_default_duplicant_row)
		{
			this.default_row.transform.SetAsFirstSibling();
		}
	}

	// Token: 0x06005706 RID: 22278 RVA: 0x001FA3AC File Offset: 0x001F85AC
	protected int compare_rows_alphabetical(IAssignableIdentity a, IAssignableIdentity b)
	{
		if (a == null && b == null)
		{
			return 0;
		}
		if (a == null)
		{
			return -1;
		}
		if (b == null)
		{
			return 1;
		}
		return a.GetProperName().CompareTo(b.GetProperName());
	}

	// Token: 0x06005707 RID: 22279 RVA: 0x001FA3D1 File Offset: 0x001F85D1
	protected int default_sort(TableRow a, TableRow b)
	{
		return 0;
	}

	// Token: 0x06005708 RID: 22280 RVA: 0x001FA3D4 File Offset: 0x001F85D4
	protected void ObsoleteRows()
	{
		for (int i = this.rows.Count - 1; i >= 0; i--)
		{
			IAssignableIdentity identity = this.rows[i].GetIdentity();
			if (identity != null)
			{
				this.obsoleteMinionRowStatus.Add(identity, true);
			}
		}
		foreach (KeyValuePair<int, GameObject> keyValuePair in this.worldDividers)
		{
			this.obsoleteWorldDividerStatus.Add(keyValuePair.Key, true);
		}
	}

	// Token: 0x06005709 RID: 22281 RVA: 0x001FA470 File Offset: 0x001F8670
	protected void AddRow(IAssignableIdentity minion)
	{
		bool flag = minion == null;
		if (!flag && this.obsoleteMinionRowStatus.ContainsKey(minion))
		{
			this.obsoleteMinionRowStatus[minion] = false;
			this.rows.Find((TableRow match) => match.GetIdentity() == minion).RefreshColumns(this.columns);
			return;
		}
		if (flag && this.header_row != null)
		{
			this.header_row.GetComponent<TableRow>().RefreshColumns(this.columns);
			return;
		}
		GameObject gameObject = Util.KInstantiateUI(flag ? this.prefab_row_header : this.prefab_row_empty, (minion == null) ? this.header_content_transform.gameObject : this.scroll_content_transform.gameObject, true);
		TableRow component = gameObject.GetComponent<TableRow>();
		component.rowType = (flag ? TableRow.RowType.Header : ((minion as MinionIdentity != null) ? TableRow.RowType.Minion : TableRow.RowType.StoredMinon));
		this.rows.Add(component);
		component.ConfigureContent(minion, this.columns, this);
		if (!flag)
		{
			this.all_sortable_rows.Add(component);
			return;
		}
		this.header_row = gameObject;
	}

	// Token: 0x0600570A RID: 22282 RVA: 0x001FA5A0 File Offset: 0x001F87A0
	protected void AddDefaultRow()
	{
		if (this.default_row != null)
		{
			this.default_row.GetComponent<TableRow>().RefreshColumns(this.columns);
			return;
		}
		GameObject gameObject = Util.KInstantiateUI(this.prefab_row_empty, this.scroll_content_transform.gameObject, true);
		this.default_row = gameObject;
		TableRow component = gameObject.GetComponent<TableRow>();
		component.rowType = TableRow.RowType.Default;
		component.isDefault = true;
		this.rows.Add(component);
		component.ConfigureContent(null, this.columns, this);
	}

	// Token: 0x0600570B RID: 22283 RVA: 0x001FA620 File Offset: 0x001F8820
	protected void AddWorldDivider(int worldId)
	{
		if (this.obsoleteWorldDividerStatus.ContainsKey(worldId) && this.obsoleteWorldDividerStatus[worldId])
		{
			this.obsoleteWorldDividerStatus[worldId] = false;
			return;
		}
		GameObject gameObject = Util.KInstantiateUI(this.prefab_world_divider, this.scroll_content_transform.gameObject, true);
		gameObject.GetComponentInChildren<Image>().color = ClusterManager.worldColors[worldId % ClusterManager.worldColors.Length];
		RectTransform component = gameObject.GetComponentInChildren<LocText>().GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(150f, component.sizeDelta.y);
		ClusterGridEntity component2 = ClusterManager.Instance.GetWorld(worldId).GetComponent<ClusterGridEntity>();
		string text = ((component2 is Clustercraft) ? NAMEGEN.WORLD.SPACECRAFT_PREFIX : NAMEGEN.WORLD.PLANETOID_PREFIX);
		gameObject.GetComponentInChildren<LocText>().SetText(text + component2.Name);
		gameObject.GetComponentInChildren<ToolTip>().SetSimpleTooltip(string.Format(NAMEGEN.WORLD.WORLDDIVIDER_TOOLTIP, component2.Name));
		gameObject.GetComponent<HierarchyReferences>().GetReference<Image>("Icon").sprite = component2.GetUISprite();
		this.worldDividers.Add(worldId, gameObject);
		gameObject.GetComponent<TableRow>().ConfigureAsWorldDivider(this.columns, this);
	}

	// Token: 0x0600570C RID: 22284 RVA: 0x001FA750 File Offset: 0x001F8950
	protected void RemoveWorldDivider(object worldId)
	{
		if (this.worldDividers.ContainsKey((int)worldId))
		{
			this.rows.Remove(this.worldDividers[(int)worldId].GetComponent<TableRow>());
			Util.KDestroyGameObject(this.worldDividers[(int)worldId]);
			this.worldDividers.Remove((int)worldId);
		}
	}

	// Token: 0x0600570D RID: 22285 RVA: 0x001FA7BC File Offset: 0x001F89BC
	protected TableRow GetWidgetRow(GameObject widget_go)
	{
		if (widget_go == null)
		{
			global::Debug.LogWarning("Widget is null");
			return null;
		}
		if (this.known_widget_rows.ContainsKey(widget_go))
		{
			return this.known_widget_rows[widget_go];
		}
		foreach (TableRow tableRow in this.rows)
		{
			if (tableRow.rowType != TableRow.RowType.WorldDivider && tableRow.ContainsWidget(widget_go))
			{
				this.known_widget_rows.Add(widget_go, tableRow);
				return tableRow;
			}
		}
		global::Debug.LogWarning("Row is null for widget: " + widget_go.name + " parent is " + widget_go.transform.parent.name);
		return null;
	}

	// Token: 0x0600570E RID: 22286 RVA: 0x001FA888 File Offset: 0x001F8A88
	protected void StartScrollableContent(string scrollablePanelID)
	{
		if (!this.column_scrollers.Contains(scrollablePanelID))
		{
			DividerColumn dividerColumn = new DividerColumn(() => true, "");
			this.RegisterColumn("scroller_spacer_" + scrollablePanelID, dividerColumn);
			this.column_scrollers.Add(scrollablePanelID);
		}
	}

	// Token: 0x0600570F RID: 22287 RVA: 0x001FA8EC File Offset: 0x001F8AEC
	protected PortraitTableColumn AddPortraitColumn(string id, Action<IAssignableIdentity, GameObject> on_load_action, Comparison<IAssignableIdentity> sort_comparison, bool double_click_to_target = true)
	{
		PortraitTableColumn portraitTableColumn = new PortraitTableColumn(on_load_action, sort_comparison, double_click_to_target);
		if (this.RegisterColumn(id, portraitTableColumn))
		{
			return portraitTableColumn;
		}
		return null;
	}

	// Token: 0x06005710 RID: 22288 RVA: 0x001FA910 File Offset: 0x001F8B10
	protected ButtonLabelColumn AddButtonLabelColumn(string id, Action<IAssignableIdentity, GameObject> on_load_action, Func<IAssignableIdentity, GameObject, string> get_value_action, Action<GameObject> on_click_action, Action<GameObject> on_double_click_action, Comparison<IAssignableIdentity> sort_comparison, Action<IAssignableIdentity, GameObject, ToolTip> on_tooltip, Action<IAssignableIdentity, GameObject, ToolTip> on_sort_tooltip, bool whiteText = false)
	{
		ButtonLabelColumn buttonLabelColumn = new ButtonLabelColumn(on_load_action, get_value_action, on_click_action, on_double_click_action, sort_comparison, on_tooltip, on_sort_tooltip, whiteText);
		if (this.RegisterColumn(id, buttonLabelColumn))
		{
			return buttonLabelColumn;
		}
		return null;
	}

	// Token: 0x06005711 RID: 22289 RVA: 0x001FA940 File Offset: 0x001F8B40
	protected LabelTableColumn AddLabelColumn(string id, Action<IAssignableIdentity, GameObject> on_load_action, Func<IAssignableIdentity, GameObject, string> get_value_action, Comparison<IAssignableIdentity> sort_comparison, Action<IAssignableIdentity, GameObject, ToolTip> on_tooltip, Action<IAssignableIdentity, GameObject, ToolTip> on_sort_tooltip, int widget_width = 128, bool should_refresh_columns = false)
	{
		LabelTableColumn labelTableColumn = new LabelTableColumn(on_load_action, get_value_action, sort_comparison, on_tooltip, on_sort_tooltip, widget_width, should_refresh_columns);
		if (this.RegisterColumn(id, labelTableColumn))
		{
			return labelTableColumn;
		}
		return null;
	}

	// Token: 0x06005712 RID: 22290 RVA: 0x001FA96C File Offset: 0x001F8B6C
	protected CheckboxTableColumn AddCheckboxColumn(string id, Action<IAssignableIdentity, GameObject> on_load_action, Func<IAssignableIdentity, GameObject, TableScreen.ResultValues> get_value_action, Action<GameObject> on_press_action, Action<GameObject, TableScreen.ResultValues> set_value_function, Comparison<IAssignableIdentity> sort_comparison, Action<IAssignableIdentity, GameObject, ToolTip> on_tooltip, Action<IAssignableIdentity, GameObject, ToolTip> on_sort_tooltip)
	{
		CheckboxTableColumn checkboxTableColumn = new CheckboxTableColumn(on_load_action, get_value_action, on_press_action, set_value_function, sort_comparison, on_tooltip, on_sort_tooltip, null);
		if (this.RegisterColumn(id, checkboxTableColumn))
		{
			return checkboxTableColumn;
		}
		return null;
	}

	// Token: 0x06005713 RID: 22291 RVA: 0x001FA99C File Offset: 0x001F8B9C
	protected SuperCheckboxTableColumn AddSuperCheckboxColumn(string id, CheckboxTableColumn[] columns_affected, Action<IAssignableIdentity, GameObject> on_load_action, Func<IAssignableIdentity, GameObject, TableScreen.ResultValues> get_value_action, Action<GameObject> on_press_action, Action<GameObject, TableScreen.ResultValues> set_value_action, Comparison<IAssignableIdentity> sort_comparison, Action<IAssignableIdentity, GameObject, ToolTip> on_tooltip)
	{
		SuperCheckboxTableColumn superCheckboxTableColumn = new SuperCheckboxTableColumn(columns_affected, on_load_action, get_value_action, on_press_action, set_value_action, sort_comparison, on_tooltip);
		if (this.RegisterColumn(id, superCheckboxTableColumn))
		{
			foreach (CheckboxTableColumn checkboxTableColumn in columns_affected)
			{
				checkboxTableColumn.on_set_action = (Action<GameObject, TableScreen.ResultValues>)Delegate.Combine(checkboxTableColumn.on_set_action, new Action<GameObject, TableScreen.ResultValues>(superCheckboxTableColumn.MarkDirty));
			}
			superCheckboxTableColumn.MarkDirty(null, TableScreen.ResultValues.False);
			return superCheckboxTableColumn;
		}
		global::Debug.LogWarning("SuperCheckbox column registration failed");
		return null;
	}

	// Token: 0x06005714 RID: 22292 RVA: 0x001FAA10 File Offset: 0x001F8C10
	protected NumericDropDownTableColumn AddNumericDropDownColumn(string id, object user_data, List<TMP_Dropdown.OptionData> options, Action<IAssignableIdentity, GameObject> on_load_action, Action<GameObject, int> set_value_action, Comparison<IAssignableIdentity> sort_comparison, NumericDropDownTableColumn.ToolTipCallbacks tooltip_callbacks)
	{
		NumericDropDownTableColumn numericDropDownTableColumn = new NumericDropDownTableColumn(user_data, options, on_load_action, set_value_action, sort_comparison, tooltip_callbacks, null);
		if (this.RegisterColumn(id, numericDropDownTableColumn))
		{
			return numericDropDownTableColumn;
		}
		return null;
	}

	// Token: 0x06005715 RID: 22293 RVA: 0x001FAA3B File Offset: 0x001F8C3B
	protected bool RegisterColumn(string id, TableColumn new_column)
	{
		if (this.columns.ContainsKey(id))
		{
			global::Debug.LogWarning(string.Format("Column with id {0} already in dictionary", id));
			return false;
		}
		new_column.screen = this;
		this.columns.Add(id, new_column);
		this.MarkRowsDirty();
		return true;
	}

	// Token: 0x06005716 RID: 22294 RVA: 0x001FAA78 File Offset: 0x001F8C78
	protected TableColumn GetWidgetColumn(GameObject widget_go)
	{
		if (this.known_widget_columns.ContainsKey(widget_go))
		{
			return this.known_widget_columns[widget_go];
		}
		foreach (KeyValuePair<string, TableColumn> keyValuePair in this.columns)
		{
			if (keyValuePair.Value.ContainsWidget(widget_go))
			{
				this.known_widget_columns.Add(widget_go, keyValuePair.Value);
				return keyValuePair.Value;
			}
		}
		global::Debug.LogWarning("No column found for widget gameobject " + widget_go.name);
		return null;
	}

	// Token: 0x06005717 RID: 22295 RVA: 0x001FAB24 File Offset: 0x001F8D24
	protected void on_load_portrait(IAssignableIdentity minion, GameObject widget_go)
	{
		TableRow widgetRow = this.GetWidgetRow(widget_go);
		CrewPortrait component = widget_go.GetComponent<CrewPortrait>();
		if (minion != null)
		{
			component.SetIdentityObject(minion, false);
			component.ForceRefresh();
			return;
		}
		component.targetImage.enabled = widgetRow.rowType == TableRow.RowType.Default;
	}

	// Token: 0x06005718 RID: 22296 RVA: 0x001FAB68 File Offset: 0x001F8D68
	protected void on_load_name_label(IAssignableIdentity minion, GameObject widget_go)
	{
		TableRow widgetRow = this.GetWidgetRow(widget_go);
		LocText locText = null;
		HierarchyReferences component = widget_go.GetComponent<HierarchyReferences>();
		LocText locText2 = component.GetReference("Label") as LocText;
		if (component.HasReference("SubLabel"))
		{
			locText = component.GetReference("SubLabel") as LocText;
		}
		if (minion != null)
		{
			locText2.text = (this.GetWidgetColumn(widget_go) as LabelTableColumn).get_value_action(minion, widget_go);
			if (locText != null)
			{
				MinionIdentity minionIdentity = minion as MinionIdentity;
				if (minionIdentity != null)
				{
					locText.text = minionIdentity.gameObject.GetComponent<MinionResume>().GetSkillsSubtitle();
				}
				else
				{
					locText.text = "";
				}
				locText.enableWordWrapping = false;
				return;
			}
		}
		else
		{
			if (widgetRow.isDefault)
			{
				locText2.text = UI.JOBSCREEN_DEFAULT;
				if (locText != null)
				{
					locText.gameObject.SetActive(false);
				}
			}
			else
			{
				locText2.text = UI.JOBSCREEN_EVERYONE;
			}
			if (locText != null)
			{
				locText.text = "";
			}
		}
	}

	// Token: 0x06005719 RID: 22297 RVA: 0x001FAC75 File Offset: 0x001F8E75
	protected string get_value_name_label(IAssignableIdentity minion, GameObject widget_go)
	{
		return minion.GetProperName();
	}

	// Token: 0x0600571A RID: 22298 RVA: 0x001FAC80 File Offset: 0x001F8E80
	protected void on_load_value_checkbox_column_super(IAssignableIdentity minion, GameObject widget_go)
	{
		MultiToggle component = widget_go.GetComponent<MultiToggle>();
		TableRow.RowType rowType = this.GetWidgetRow(widget_go).rowType;
		if (rowType <= TableRow.RowType.Minion)
		{
			component.ChangeState((int)this.get_value_checkbox_column_super(minion, widget_go));
		}
	}

	// Token: 0x0600571B RID: 22299 RVA: 0x001FACB8 File Offset: 0x001F8EB8
	public virtual TableScreen.ResultValues get_value_checkbox_column_super(IAssignableIdentity minion, GameObject widget_go)
	{
		SuperCheckboxTableColumn superCheckboxTableColumn = this.GetWidgetColumn(widget_go) as SuperCheckboxTableColumn;
		TableRow widgetRow = this.GetWidgetRow(widget_go);
		bool flag = true;
		bool flag2 = true;
		bool flag3 = false;
		bool flag4 = false;
		foreach (CheckboxTableColumn checkboxTableColumn in superCheckboxTableColumn.columns_affected)
		{
			if (checkboxTableColumn.isRevealed)
			{
				switch (checkboxTableColumn.get_value_action(widgetRow.GetIdentity(), widgetRow.GetWidget(checkboxTableColumn)))
				{
				case TableScreen.ResultValues.False:
					flag2 = false;
					if (!flag)
					{
					}
					break;
				case TableScreen.ResultValues.Partial:
					flag4 = true;
					break;
				case TableScreen.ResultValues.True:
					flag4 = true;
					flag = false;
					if (!flag2)
					{
					}
					break;
				case TableScreen.ResultValues.ConditionalGroup:
					flag3 = true;
					flag2 = false;
					flag = false;
					break;
				}
			}
		}
		TableScreen.ResultValues resultValues = TableScreen.ResultValues.Partial;
		if (flag3 && !flag4 && !flag2 && !flag)
		{
			resultValues = TableScreen.ResultValues.ConditionalGroup;
		}
		else if (flag2)
		{
			resultValues = TableScreen.ResultValues.True;
		}
		else if (flag)
		{
			resultValues = TableScreen.ResultValues.False;
		}
		else if (flag4)
		{
			resultValues = TableScreen.ResultValues.Partial;
		}
		return resultValues;
	}

	// Token: 0x0600571C RID: 22300 RVA: 0x001FAD9C File Offset: 0x001F8F9C
	protected void set_value_checkbox_column_super(GameObject widget_go, TableScreen.ResultValues new_value)
	{
		SuperCheckboxTableColumn superCheckboxTableColumn = this.GetWidgetColumn(widget_go) as SuperCheckboxTableColumn;
		TableRow widgetRow = this.GetWidgetRow(widget_go);
		switch (widgetRow.rowType)
		{
		case TableRow.RowType.Header:
			base.StartCoroutine(this.CascadeSetRowCheckBoxes(superCheckboxTableColumn.columns_affected, this.default_row.GetComponent<TableRow>(), new_value, widget_go));
			base.StartCoroutine(this.CascadeSetColumnCheckBoxes(this.all_sortable_rows, superCheckboxTableColumn, new_value, widget_go));
			return;
		case TableRow.RowType.Default:
			base.StartCoroutine(this.CascadeSetRowCheckBoxes(superCheckboxTableColumn.columns_affected, widgetRow, new_value, widget_go));
			return;
		case TableRow.RowType.Minion:
			base.StartCoroutine(this.CascadeSetRowCheckBoxes(superCheckboxTableColumn.columns_affected, widgetRow, new_value, widget_go));
			return;
		default:
			return;
		}
	}

	// Token: 0x0600571D RID: 22301 RVA: 0x001FAE3C File Offset: 0x001F903C
	protected IEnumerator CascadeSetRowCheckBoxes(CheckboxTableColumn[] checkBoxToggleColumns, TableRow row, TableScreen.ResultValues state, GameObject ignore_widget = null)
	{
		if (this.active_cascade_coroutine_count == 0)
		{
			this.current_looping_sound = LoopingSoundManager.StartSound(this.cascade_sound_path, Vector3.zero, false, false);
		}
		this.active_cascade_coroutine_count++;
		int num;
		for (int i = 0; i < checkBoxToggleColumns.Length; i = num + 1)
		{
			if (checkBoxToggleColumns[i].widgets_by_row.ContainsKey(row))
			{
				GameObject gameObject = checkBoxToggleColumns[i].widgets_by_row[row];
				if (!(gameObject == ignore_widget) && checkBoxToggleColumns[i].isRevealed)
				{
					bool flag = false;
					switch ((this.GetWidgetColumn(gameObject) as CheckboxTableColumn).get_value_action(row.GetIdentity(), gameObject))
					{
					case TableScreen.ResultValues.False:
						flag = state != TableScreen.ResultValues.False;
						break;
					case TableScreen.ResultValues.Partial:
					case TableScreen.ResultValues.ConditionalGroup:
						flag = true;
						break;
					case TableScreen.ResultValues.True:
						flag = state != TableScreen.ResultValues.True;
						break;
					}
					if (flag)
					{
						(this.GetWidgetColumn(gameObject) as CheckboxTableColumn).on_set_action(gameObject, state);
						yield return null;
					}
				}
			}
			num = i;
		}
		this.active_cascade_coroutine_count--;
		if (this.active_cascade_coroutine_count <= 0)
		{
			this.StopLoopingCascadeSound();
		}
		yield break;
	}

	// Token: 0x0600571E RID: 22302 RVA: 0x001FAE68 File Offset: 0x001F9068
	protected IEnumerator CascadeSetColumnCheckBoxes(List<TableRow> rows, CheckboxTableColumn checkBoxToggleColumn, TableScreen.ResultValues state, GameObject header_widget_go = null)
	{
		if (this.active_cascade_coroutine_count == 0)
		{
			this.current_looping_sound = LoopingSoundManager.StartSound(this.cascade_sound_path, Vector3.zero, false, true);
		}
		this.active_cascade_coroutine_count++;
		int num;
		for (int i = 0; i < rows.Count; i = num + 1)
		{
			GameObject widget = rows[i].GetWidget(checkBoxToggleColumn);
			if (!(widget == header_widget_go))
			{
				bool flag = false;
				switch ((this.GetWidgetColumn(widget) as CheckboxTableColumn).get_value_action(rows[i].GetIdentity(), widget))
				{
				case TableScreen.ResultValues.False:
					flag = state != TableScreen.ResultValues.False;
					break;
				case TableScreen.ResultValues.Partial:
				case TableScreen.ResultValues.ConditionalGroup:
					flag = true;
					break;
				case TableScreen.ResultValues.True:
					flag = state != TableScreen.ResultValues.True;
					break;
				}
				if (flag)
				{
					(this.GetWidgetColumn(widget) as CheckboxTableColumn).on_set_action(widget, state);
					yield return null;
				}
			}
			num = i;
		}
		if (header_widget_go != null)
		{
			(this.GetWidgetColumn(header_widget_go) as CheckboxTableColumn).on_load_action(null, header_widget_go);
		}
		this.active_cascade_coroutine_count--;
		if (this.active_cascade_coroutine_count <= 0)
		{
			this.StopLoopingCascadeSound();
		}
		yield break;
	}

	// Token: 0x0600571F RID: 22303 RVA: 0x001FAE94 File Offset: 0x001F9094
	private void StopLoopingCascadeSound()
	{
		if (this.current_looping_sound.IsValid())
		{
			LoopingSoundManager.StopSound(this.current_looping_sound);
			this.current_looping_sound.Clear();
		}
	}

	// Token: 0x06005720 RID: 22304 RVA: 0x001FAEBC File Offset: 0x001F90BC
	protected void on_press_checkbox_column_super(GameObject widget_go)
	{
		SuperCheckboxTableColumn superCheckboxTableColumn = this.GetWidgetColumn(widget_go) as SuperCheckboxTableColumn;
		TableRow widgetRow = this.GetWidgetRow(widget_go);
		switch (this.get_value_checkbox_column_super(widgetRow.GetIdentity(), widget_go))
		{
		case TableScreen.ResultValues.False:
			superCheckboxTableColumn.on_set_action(widget_go, TableScreen.ResultValues.True);
			break;
		case TableScreen.ResultValues.Partial:
		case TableScreen.ResultValues.ConditionalGroup:
			superCheckboxTableColumn.on_set_action(widget_go, TableScreen.ResultValues.True);
			break;
		case TableScreen.ResultValues.True:
			superCheckboxTableColumn.on_set_action(widget_go, TableScreen.ResultValues.False);
			break;
		}
		superCheckboxTableColumn.on_load_action(widgetRow.GetIdentity(), widget_go);
	}

	// Token: 0x06005721 RID: 22305 RVA: 0x001FAF44 File Offset: 0x001F9144
	protected void on_tooltip_sort_alphabetically(IAssignableIdentity minion, GameObject widget_go, ToolTip tooltip)
	{
		tooltip.ClearMultiStringTooltip();
		switch (this.GetWidgetRow(widget_go).rowType)
		{
		case TableRow.RowType.Header:
			tooltip.AddMultiStringTooltip(UI.TABLESCREENS.COLUMN_SORT_BY_NAME, null);
			break;
		case TableRow.RowType.Default:
		case TableRow.RowType.Minion:
			break;
		default:
			return;
		}
	}

	// Token: 0x04003AFF RID: 15103
	protected string title;

	// Token: 0x04003B00 RID: 15104
	protected bool has_default_duplicant_row = true;

	// Token: 0x04003B01 RID: 15105
	protected bool useWorldDividers = true;

	// Token: 0x04003B02 RID: 15106
	private bool rows_dirty;

	// Token: 0x04003B03 RID: 15107
	protected Comparison<IAssignableIdentity> active_sort_method;

	// Token: 0x04003B04 RID: 15108
	protected TableColumn active_sort_column;

	// Token: 0x04003B05 RID: 15109
	protected bool sort_is_reversed;

	// Token: 0x04003B06 RID: 15110
	private int active_cascade_coroutine_count;

	// Token: 0x04003B07 RID: 15111
	private HandleVector<int>.Handle current_looping_sound = HandleVector<int>.InvalidHandle;

	// Token: 0x04003B08 RID: 15112
	private bool incubating;

	// Token: 0x04003B09 RID: 15113
	private int removeWorldHandle = -1;

	// Token: 0x04003B0A RID: 15114
	protected Dictionary<string, TableColumn> columns = new Dictionary<string, TableColumn>();

	// Token: 0x04003B0B RID: 15115
	public List<TableRow> rows = new List<TableRow>();

	// Token: 0x04003B0C RID: 15116
	public List<TableRow> all_sortable_rows = new List<TableRow>();

	// Token: 0x04003B0D RID: 15117
	public List<string> column_scrollers = new List<string>();

	// Token: 0x04003B0E RID: 15118
	private Dictionary<GameObject, TableRow> known_widget_rows = new Dictionary<GameObject, TableRow>();

	// Token: 0x04003B0F RID: 15119
	private Dictionary<GameObject, TableColumn> known_widget_columns = new Dictionary<GameObject, TableColumn>();

	// Token: 0x04003B10 RID: 15120
	public GameObject prefab_row_empty;

	// Token: 0x04003B11 RID: 15121
	public GameObject prefab_row_header;

	// Token: 0x04003B12 RID: 15122
	public GameObject prefab_world_divider;

	// Token: 0x04003B13 RID: 15123
	public GameObject prefab_scroller_border;

	// Token: 0x04003B14 RID: 15124
	private string cascade_sound_path = GlobalAssets.GetSound("Placers_Unfurl_LP", false);

	// Token: 0x04003B15 RID: 15125
	public KButton CloseButton;

	// Token: 0x04003B16 RID: 15126
	[MyCmpGet]
	private VerticalLayoutGroup VLG;

	// Token: 0x04003B17 RID: 15127
	protected GameObject header_row;

	// Token: 0x04003B18 RID: 15128
	protected GameObject default_row;

	// Token: 0x04003B19 RID: 15129
	public LocText title_bar;

	// Token: 0x04003B1A RID: 15130
	public Transform header_content_transform;

	// Token: 0x04003B1B RID: 15131
	public Transform scroll_content_transform;

	// Token: 0x04003B1C RID: 15132
	public Transform scroller_borders_transform;

	// Token: 0x04003B1D RID: 15133
	public Dictionary<int, GameObject> worldDividers = new Dictionary<int, GameObject>();

	// Token: 0x04003B1E RID: 15134
	private bool scrollersDirty;

	// Token: 0x04003B1F RID: 15135
	private float targetScrollerPosition;

	// Token: 0x04003B20 RID: 15136
	private Dictionary<IAssignableIdentity, bool> obsoleteMinionRowStatus = new Dictionary<IAssignableIdentity, bool>();

	// Token: 0x04003B21 RID: 15137
	private Dictionary<int, bool> obsoleteWorldDividerStatus = new Dictionary<int, bool>();

	// Token: 0x020019A6 RID: 6566
	public enum ResultValues
	{
		// Token: 0x040074E0 RID: 29920
		False,
		// Token: 0x040074E1 RID: 29921
		Partial,
		// Token: 0x040074E2 RID: 29922
		True,
		// Token: 0x040074E3 RID: 29923
		ConditionalGroup
	}
}
