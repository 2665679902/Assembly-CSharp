using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AF4 RID: 2804
public class CrewListScreen<EntryType> : KScreen where EntryType : CrewListEntry
{
	// Token: 0x06005618 RID: 22040 RVA: 0x001F2690 File Offset: 0x001F0890
	protected override void OnActivate()
	{
		base.OnActivate();
		this.ClearEntries();
		this.SpawnEntries();
		this.PositionColumnTitles();
		if (this.autoColumn)
		{
			this.UpdateColumnTitles();
		}
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x06005619 RID: 22041 RVA: 0x001F26BF File Offset: 0x001F08BF
	protected override void OnCmpEnable()
	{
		if (this.autoColumn)
		{
			this.UpdateColumnTitles();
		}
		this.Reconstruct();
	}

	// Token: 0x0600561A RID: 22042 RVA: 0x001F26D8 File Offset: 0x001F08D8
	private void ClearEntries()
	{
		for (int i = this.EntryObjects.Count - 1; i > -1; i--)
		{
			Util.KDestroyGameObject(this.EntryObjects[i]);
		}
		this.EntryObjects.Clear();
	}

	// Token: 0x0600561B RID: 22043 RVA: 0x001F271E File Offset: 0x001F091E
	protected void RefreshCrewPortraitContent()
	{
		this.EntryObjects.ForEach(delegate(EntryType eo)
		{
			eo.RefreshCrewPortraitContent();
		});
	}

	// Token: 0x0600561C RID: 22044 RVA: 0x001F274A File Offset: 0x001F094A
	protected virtual void SpawnEntries()
	{
		if (this.EntryObjects.Count != 0)
		{
			this.ClearEntries();
		}
	}

	// Token: 0x0600561D RID: 22045 RVA: 0x001F2760 File Offset: 0x001F0960
	public override void ScreenUpdate(bool topLevel)
	{
		base.ScreenUpdate(topLevel);
		if (this.autoColumn)
		{
			this.UpdateColumnTitles();
		}
		bool flag = false;
		List<MinionIdentity> liveIdentities = new List<MinionIdentity>(Components.LiveMinionIdentities.Items);
		if (this.EntryObjects.Count != liveIdentities.Count || this.EntryObjects.FindAll((EntryType o) => liveIdentities.Contains(o.Identity)).Count != this.EntryObjects.Count)
		{
			flag = true;
		}
		if (flag)
		{
			this.Reconstruct();
		}
		this.UpdateScroll();
	}

	// Token: 0x0600561E RID: 22046 RVA: 0x001F27F1 File Offset: 0x001F09F1
	public void Reconstruct()
	{
		this.ClearEntries();
		this.SpawnEntries();
	}

	// Token: 0x0600561F RID: 22047 RVA: 0x001F2800 File Offset: 0x001F0A00
	private void UpdateScroll()
	{
		if (this.PanelScrollbar)
		{
			if (this.EntryObjects.Count <= this.maxEntriesBeforeScroll)
			{
				this.PanelScrollbar.value = Mathf.Lerp(this.PanelScrollbar.value, 1f, 10f);
				this.PanelScrollbar.gameObject.SetActive(false);
				return;
			}
			this.PanelScrollbar.gameObject.SetActive(true);
		}
	}

	// Token: 0x06005620 RID: 22048 RVA: 0x001F2878 File Offset: 0x001F0A78
	private void SetHeadersActive(bool state)
	{
		for (int i = 0; i < this.ColumnTitlesContainer.childCount; i++)
		{
			this.ColumnTitlesContainer.GetChild(i).gameObject.SetActive(state);
		}
	}

	// Token: 0x06005621 RID: 22049 RVA: 0x001F28B4 File Offset: 0x001F0AB4
	protected virtual void PositionColumnTitles()
	{
		if (this.ColumnTitlesContainer == null)
		{
			return;
		}
		if (this.EntryObjects.Count <= 0)
		{
			this.SetHeadersActive(false);
			return;
		}
		this.SetHeadersActive(true);
		int childCount = this.EntryObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			OverviewColumnIdentity component = this.EntryObjects[0].transform.GetChild(i).GetComponent<OverviewColumnIdentity>();
			if (component != null)
			{
				GameObject gameObject = Util.KInstantiate(this.Prefab_ColumnTitle, null, null);
				gameObject.name = component.Column_DisplayName;
				LocText componentInChildren = gameObject.GetComponentInChildren<LocText>();
				gameObject.transform.SetParent(this.ColumnTitlesContainer);
				componentInChildren.text = (component.StringLookup ? Strings.Get(component.Column_DisplayName) : component.Column_DisplayName);
				gameObject.GetComponent<ToolTip>().toolTip = string.Format(UI.TOOLTIPS.SORTCOLUMN, componentInChildren.text);
				gameObject.rectTransform().anchoredPosition = new Vector2(component.rectTransform().anchoredPosition.x, 0f);
				OverviewColumnIdentity overviewColumnIdentity = gameObject.GetComponent<OverviewColumnIdentity>();
				if (overviewColumnIdentity == null)
				{
					overviewColumnIdentity = gameObject.AddComponent<OverviewColumnIdentity>();
				}
				overviewColumnIdentity.Column_DisplayName = component.Column_DisplayName;
				overviewColumnIdentity.columnID = component.columnID;
				overviewColumnIdentity.xPivot = component.xPivot;
				overviewColumnIdentity.Sortable = component.Sortable;
				if (overviewColumnIdentity.Sortable)
				{
					overviewColumnIdentity.GetComponentInChildren<ImageToggleState>(true).gameObject.SetActive(true);
				}
			}
		}
		this.UpdateColumnTitles();
		this.sortToggleGroup = base.gameObject.AddComponent<ToggleGroup>();
		this.sortToggleGroup.allowSwitchOff = true;
	}

	// Token: 0x06005622 RID: 22050 RVA: 0x001F2A78 File Offset: 0x001F0C78
	protected void SortByName(bool reverse)
	{
		List<EntryType> list = new List<EntryType>(this.EntryObjects);
		list.Sort(delegate(EntryType a, EntryType b)
		{
			string text = a.Identity.GetProperName() + a.gameObject.GetInstanceID().ToString();
			string text2 = b.Identity.GetProperName() + b.gameObject.GetInstanceID().ToString();
			return text.CompareTo(text2);
		});
		this.ReorderEntries(list, reverse);
	}

	// Token: 0x06005623 RID: 22051 RVA: 0x001F2AC0 File Offset: 0x001F0CC0
	protected void UpdateColumnTitles()
	{
		if (this.EntryObjects.Count <= 0 || !this.EntryObjects[0].gameObject.activeSelf)
		{
			this.SetHeadersActive(false);
			return;
		}
		this.SetHeadersActive(true);
		for (int i = 0; i < this.ColumnTitlesContainer.childCount; i++)
		{
			RectTransform rectTransform = this.ColumnTitlesContainer.GetChild(i).rectTransform();
			for (int j = 0; j < this.EntryObjects[0].transform.childCount; j++)
			{
				OverviewColumnIdentity component = this.EntryObjects[0].transform.GetChild(j).GetComponent<OverviewColumnIdentity>();
				if (component != null && component.Column_DisplayName == rectTransform.name)
				{
					rectTransform.pivot = new Vector2(component.xPivot, rectTransform.pivot.y);
					rectTransform.anchoredPosition = new Vector2(component.rectTransform().anchoredPosition.x + this.columnTitleHorizontalOffset, 0f);
					rectTransform.sizeDelta = new Vector2(component.rectTransform().sizeDelta.x, rectTransform.sizeDelta.y);
					if (rectTransform.anchoredPosition.x == 0f)
					{
						rectTransform.gameObject.SetActive(false);
					}
					else
					{
						rectTransform.gameObject.SetActive(true);
					}
				}
			}
		}
	}

	// Token: 0x06005624 RID: 22052 RVA: 0x001F2C3C File Offset: 0x001F0E3C
	protected void ReorderEntries(List<EntryType> sortedEntries, bool reverse)
	{
		for (int i = 0; i < sortedEntries.Count; i++)
		{
			if (reverse)
			{
				sortedEntries[i].transform.SetSiblingIndex(sortedEntries.Count - 1 - i);
			}
			else
			{
				sortedEntries[i].transform.SetSiblingIndex(i);
			}
		}
	}

	// Token: 0x04003A93 RID: 14995
	public GameObject Prefab_CrewEntry;

	// Token: 0x04003A94 RID: 14996
	public List<EntryType> EntryObjects = new List<EntryType>();

	// Token: 0x04003A95 RID: 14997
	public Transform ScrollRectTransform;

	// Token: 0x04003A96 RID: 14998
	public Transform EntriesPanelTransform;

	// Token: 0x04003A97 RID: 14999
	protected Vector2 EntryRectSize = new Vector2(750f, 64f);

	// Token: 0x04003A98 RID: 15000
	public int maxEntriesBeforeScroll = 5;

	// Token: 0x04003A99 RID: 15001
	public Scrollbar PanelScrollbar;

	// Token: 0x04003A9A RID: 15002
	protected ToggleGroup sortToggleGroup;

	// Token: 0x04003A9B RID: 15003
	protected Toggle lastSortToggle;

	// Token: 0x04003A9C RID: 15004
	protected bool lastSortReversed;

	// Token: 0x04003A9D RID: 15005
	public GameObject Prefab_ColumnTitle;

	// Token: 0x04003A9E RID: 15006
	public Transform ColumnTitlesContainer;

	// Token: 0x04003A9F RID: 15007
	public bool autoColumn;

	// Token: 0x04003AA0 RID: 15008
	public float columnTitleHorizontalOffset;
}
