using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B09 RID: 2825
[AddComponentMenu("KMonoBehaviour/scripts/TableRow")]
public class TableRow : KMonoBehaviour
{
	// Token: 0x060056EB RID: 22251 RVA: 0x001F8EEC File Offset: 0x001F70EC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (this.selectMinionButton != null)
		{
			this.selectMinionButton.onClick += this.SelectMinion;
			this.selectMinionButton.onDoubleClick += this.SelectAndFocusMinion;
		}
	}

	// Token: 0x060056EC RID: 22252 RVA: 0x001F8F3B File Offset: 0x001F713B
	public GameObject GetScroller(string scrollerID)
	{
		return this.scrollers[scrollerID];
	}

	// Token: 0x060056ED RID: 22253 RVA: 0x001F8F49 File Offset: 0x001F7149
	public GameObject GetScrollerBorder(string scrolledID)
	{
		return this.scrollerBorders[scrolledID];
	}

	// Token: 0x060056EE RID: 22254 RVA: 0x001F8F58 File Offset: 0x001F7158
	public void SelectMinion()
	{
		MinionIdentity minionIdentity = this.minion as MinionIdentity;
		if (minionIdentity == null)
		{
			return;
		}
		SelectTool.Instance.Select(minionIdentity.GetComponent<KSelectable>(), false);
	}

	// Token: 0x060056EF RID: 22255 RVA: 0x001F8F8C File Offset: 0x001F718C
	public void SelectAndFocusMinion()
	{
		MinionIdentity minionIdentity = this.minion as MinionIdentity;
		if (minionIdentity == null)
		{
			return;
		}
		SelectTool.Instance.SelectAndFocus(minionIdentity.transform.GetPosition(), minionIdentity.GetComponent<KSelectable>(), new Vector3(8f, 0f, 0f));
	}

	// Token: 0x060056F0 RID: 22256 RVA: 0x001F8FE0 File Offset: 0x001F71E0
	public void ConfigureAsWorldDivider(Dictionary<string, TableColumn> columns, TableScreen screen)
	{
		ScrollRect scroll_rect = base.gameObject.GetComponentInChildren<ScrollRect>();
		this.rowType = TableRow.RowType.WorldDivider;
		foreach (KeyValuePair<string, TableColumn> keyValuePair in columns)
		{
			if (keyValuePair.Value.scrollerID != "")
			{
				TableColumn value = keyValuePair.Value;
				break;
			}
		}
		scroll_rect.onValueChanged.AddListener(delegate
		{
			if (screen.CheckScrollersDirty())
			{
				return;
			}
			screen.SetScrollersDirty(scroll_rect.horizontalNormalizedPosition);
		});
	}

	// Token: 0x060056F1 RID: 22257 RVA: 0x001F908C File Offset: 0x001F728C
	public void ConfigureContent(IAssignableIdentity minion, Dictionary<string, TableColumn> columns, TableScreen screen)
	{
		this.minion = minion;
		KImage componentInChildren = base.GetComponentInChildren<KImage>(true);
		componentInChildren.colorStyleSetting = ((minion == null) ? this.style_setting_default : this.style_setting_minion);
		componentInChildren.ColorState = KImage.ColorSelector.Inactive;
		CanvasGroup component = base.GetComponent<CanvasGroup>();
		if (component != null && minion as StoredMinionIdentity != null)
		{
			component.alpha = 0.6f;
		}
		foreach (KeyValuePair<string, TableColumn> keyValuePair in columns)
		{
			GameObject gameObject;
			if (minion == null)
			{
				if (this.isDefault)
				{
					gameObject = keyValuePair.Value.GetDefaultWidget(base.gameObject);
				}
				else
				{
					gameObject = keyValuePair.Value.GetHeaderWidget(base.gameObject);
				}
			}
			else
			{
				gameObject = keyValuePair.Value.GetMinionWidget(base.gameObject);
			}
			this.widgets.Add(keyValuePair.Value, gameObject);
			keyValuePair.Value.widgets_by_row.Add(this, gameObject);
			if (keyValuePair.Value.scrollerID != "")
			{
				foreach (string text in keyValuePair.Value.screen.column_scrollers)
				{
					if (!(text != keyValuePair.Value.scrollerID))
					{
						if (!this.scrollers.ContainsKey(text))
						{
							GameObject gameObject2 = Util.KInstantiateUI(this.scrollerPrefab, base.gameObject, true);
							ScrollRect scroll_rect = gameObject2.GetComponent<ScrollRect>();
							scroll_rect.onValueChanged.AddListener(delegate
							{
								if (screen.CheckScrollersDirty())
								{
									return;
								}
								screen.SetScrollersDirty(scroll_rect.horizontalNormalizedPosition);
							});
							this.scrollers.Add(text, scroll_rect.content.gameObject);
							if (scroll_rect.content.transform.parent.Find("Border") != null)
							{
								this.scrollerBorders.Add(text, scroll_rect.content.transform.parent.Find("Border").gameObject);
							}
						}
						gameObject.transform.SetParent(this.scrollers[text].transform);
						this.scrollers[text].transform.parent.GetComponent<ScrollRect>().horizontalNormalizedPosition = 0f;
					}
				}
			}
		}
		this.RefreshColumns(columns);
		if (minion != null)
		{
			base.gameObject.name = minion.GetProperName();
		}
		else if (this.isDefault)
		{
			base.gameObject.name = "defaultRow";
		}
		if (this.selectMinionButton)
		{
			this.selectMinionButton.transform.SetAsLastSibling();
		}
		foreach (KeyValuePair<string, GameObject> keyValuePair2 in this.scrollerBorders)
		{
			RectTransform rectTransform = keyValuePair2.Value.rectTransform();
			float width = rectTransform.rect.width;
			keyValuePair2.Value.transform.SetParent(base.gameObject.transform);
			rectTransform.anchorMin = (rectTransform.anchorMax = new Vector2(0f, 1f));
			rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
			RectTransform rectTransform2 = this.scrollers[keyValuePair2.Key].transform.parent.rectTransform();
			Vector3 vector = this.scrollers[keyValuePair2.Key].transform.parent.rectTransform().GetLocalPosition() - new Vector3(rectTransform2.sizeDelta.x / 2f, -1f * (rectTransform2.sizeDelta.y / 2f), 0f);
			vector.y = 0f;
			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 374f);
			rectTransform.SetLocalPosition(vector + Vector3.up * rectTransform.GetLocalPosition().y + Vector3.up * -rectTransform.anchoredPosition.y);
		}
	}

	// Token: 0x060056F2 RID: 22258 RVA: 0x001F9570 File Offset: 0x001F7770
	public void RefreshColumns(Dictionary<string, TableColumn> columns)
	{
		foreach (KeyValuePair<string, TableColumn> keyValuePair in columns)
		{
			if (keyValuePair.Value.on_load_action != null)
			{
				keyValuePair.Value.on_load_action(this.minion, keyValuePair.Value.widgets_by_row[this]);
			}
		}
	}

	// Token: 0x060056F3 RID: 22259 RVA: 0x001F95F0 File Offset: 0x001F77F0
	public void RefreshScrollers()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.scrollers)
		{
			ScrollRect component = keyValuePair.Value.transform.parent.GetComponent<ScrollRect>();
			component.GetComponent<LayoutElement>().minWidth = Mathf.Min(768f, component.content.sizeDelta.x);
		}
		foreach (KeyValuePair<string, GameObject> keyValuePair2 in this.scrollerBorders)
		{
			RectTransform rectTransform = keyValuePair2.Value.rectTransform();
			rectTransform.sizeDelta = new Vector2(this.scrollers[keyValuePair2.Key].transform.parent.GetComponent<LayoutElement>().minWidth, rectTransform.sizeDelta.y);
		}
	}

	// Token: 0x060056F4 RID: 22260 RVA: 0x001F9700 File Offset: 0x001F7900
	public GameObject GetWidget(TableColumn column)
	{
		if (this.widgets.ContainsKey(column) && this.widgets[column] != null)
		{
			return this.widgets[column];
		}
		global::Debug.LogWarning("Widget is null or row does not contain widget for column " + ((column != null) ? column.ToString() : null));
		return null;
	}

	// Token: 0x060056F5 RID: 22261 RVA: 0x001F9759 File Offset: 0x001F7959
	public IAssignableIdentity GetIdentity()
	{
		return this.minion;
	}

	// Token: 0x060056F6 RID: 22262 RVA: 0x001F9761 File Offset: 0x001F7961
	public bool ContainsWidget(GameObject widget)
	{
		return this.widgets.ContainsValue(widget);
	}

	// Token: 0x060056F7 RID: 22263 RVA: 0x001F9770 File Offset: 0x001F7970
	public void Clear()
	{
		foreach (KeyValuePair<TableColumn, GameObject> keyValuePair in this.widgets)
		{
			keyValuePair.Key.widgets_by_row.Remove(this);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04003AF4 RID: 15092
	public TableRow.RowType rowType;

	// Token: 0x04003AF5 RID: 15093
	private IAssignableIdentity minion;

	// Token: 0x04003AF6 RID: 15094
	private Dictionary<TableColumn, GameObject> widgets = new Dictionary<TableColumn, GameObject>();

	// Token: 0x04003AF7 RID: 15095
	private Dictionary<string, GameObject> scrollers = new Dictionary<string, GameObject>();

	// Token: 0x04003AF8 RID: 15096
	private Dictionary<string, GameObject> scrollerBorders = new Dictionary<string, GameObject>();

	// Token: 0x04003AF9 RID: 15097
	public bool isDefault;

	// Token: 0x04003AFA RID: 15098
	public KButton selectMinionButton;

	// Token: 0x04003AFB RID: 15099
	[SerializeField]
	private ColorStyleSetting style_setting_default;

	// Token: 0x04003AFC RID: 15100
	[SerializeField]
	private ColorStyleSetting style_setting_minion;

	// Token: 0x04003AFD RID: 15101
	[SerializeField]
	private GameObject scrollerPrefab;

	// Token: 0x04003AFE RID: 15102
	[SerializeField]
	private GameObject scrollbarPrefab;

	// Token: 0x020019A2 RID: 6562
	public enum RowType
	{
		// Token: 0x040074D5 RID: 29909
		Header,
		// Token: 0x040074D6 RID: 29910
		Default,
		// Token: 0x040074D7 RID: 29911
		Minion,
		// Token: 0x040074D8 RID: 29912
		StoredMinon,
		// Token: 0x040074D9 RID: 29913
		WorldDivider
	}
}
