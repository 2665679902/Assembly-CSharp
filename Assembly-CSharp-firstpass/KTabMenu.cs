using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000067 RID: 103
public class KTabMenu : KScreen
{
	// Token: 0x17000094 RID: 148
	// (get) Token: 0x06000439 RID: 1081 RVA: 0x00015620 File Offset: 0x00013820
	public int PreviousActiveTab
	{
		get
		{
			return this.previouslyActiveTab;
		}
	}

	// Token: 0x1400001C RID: 28
	// (add) Token: 0x0600043A RID: 1082 RVA: 0x00015628 File Offset: 0x00013828
	// (remove) Token: 0x0600043B RID: 1083 RVA: 0x00015660 File Offset: 0x00013860
	public event KTabMenu.TabActivated onTabActivated;

	// Token: 0x0600043C RID: 1084 RVA: 0x00015698 File Offset: 0x00013898
	public int AddTab(string tabName, KScreen contents)
	{
		int count = this.tabs.Count;
		this.header.Add(tabName, new KTabMenuHeader.OnClick(this.ActivateTab), this.tabs.Count);
		this.header.SetTabEnabled(count, true);
		this.tabs.Add(contents);
		return count;
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x000156F0 File Offset: 0x000138F0
	public int AddTab(Sprite icon, string tabName, KScreen contents, string tooltip = "")
	{
		int count = this.tabs.Count;
		this.header.Add(icon, tabName, new KTabMenuHeader.OnClick(this.ActivateTab), this.tabs.Count, tooltip);
		this.header.SetTabEnabled(count, true);
		this.tabs.Add(contents);
		return count;
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x0001574C File Offset: 0x0001394C
	public virtual void ActivateTab(int tabIdx)
	{
		this.header.Activate(tabIdx, this.previouslyActiveTab);
		for (int i = 0; i < this.tabs.Count; i++)
		{
			this.tabs[i].gameObject.SetActive(i == tabIdx);
		}
		ScrollRect component = this.body.GetComponent<ScrollRect>();
		if (component != null && tabIdx < this.tabs.Count)
		{
			component.content = this.tabs[tabIdx].GetComponent<RectTransform>();
		}
		if (this.onTabActivated != null)
		{
			this.onTabActivated(tabIdx, this.previouslyActiveTab);
		}
		this.previouslyActiveTab = tabIdx;
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x000157F8 File Offset: 0x000139F8
	protected override void OnDeactivate()
	{
		foreach (KScreen kscreen in this.tabs)
		{
			kscreen.Deactivate();
		}
		base.OnDeactivate();
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x00015850 File Offset: 0x00013A50
	public void SetTabEnabled(int tabIdx, bool enabled)
	{
		this.header.SetTabEnabled(tabIdx, enabled);
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x00015860 File Offset: 0x00013A60
	protected int CountTabs()
	{
		int num = 0;
		for (int i = 0; i < this.header.transform.childCount; i++)
		{
			if (this.header.transform.GetChild(i).gameObject.activeSelf)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x040004A1 RID: 1185
	[SerializeField]
	protected KTabMenuHeader header;

	// Token: 0x040004A2 RID: 1186
	[SerializeField]
	protected RectTransform body;

	// Token: 0x040004A3 RID: 1187
	protected List<KScreen> tabs = new List<KScreen>();

	// Token: 0x040004A4 RID: 1188
	protected int previouslyActiveTab = -1;

	// Token: 0x020009B9 RID: 2489
	// (Invoke) Token: 0x0600535C RID: 21340
	public delegate void TabActivated(int tabIdx, int previouslyActiveTabIdx);
}
