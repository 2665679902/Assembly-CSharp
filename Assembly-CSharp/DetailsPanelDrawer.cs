using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A2C RID: 2604
public class DetailsPanelDrawer
{
	// Token: 0x06004F06 RID: 20230 RVA: 0x001C1826 File Offset: 0x001BFA26
	public DetailsPanelDrawer(GameObject label_prefab, GameObject parent)
	{
		this.parent = parent;
		this.labelPrefab = label_prefab;
		this.stringformatter = new UIStringFormatter();
		this.floatFormatter = new UIFloatFormatter();
	}

	// Token: 0x06004F07 RID: 20231 RVA: 0x001C1860 File Offset: 0x001BFA60
	public DetailsPanelDrawer NewLabel(string text)
	{
		DetailsPanelDrawer.Label label = default(DetailsPanelDrawer.Label);
		if (this.activeLabelCount >= this.labels.Count)
		{
			label.text = Util.KInstantiate(this.labelPrefab, this.parent, null).GetComponent<LocText>();
			label.tooltip = label.text.GetComponent<ToolTip>();
			label.text.transform.localScale = new Vector3(1f, 1f, 1f);
			this.labels.Add(label);
		}
		else
		{
			label = this.labels[this.activeLabelCount];
		}
		this.activeLabelCount++;
		label.text.text = text;
		label.tooltip.toolTip = "";
		label.tooltip.OnToolTip = null;
		label.text.gameObject.SetActive(true);
		return this;
	}

	// Token: 0x06004F08 RID: 20232 RVA: 0x001C1944 File Offset: 0x001BFB44
	public DetailsPanelDrawer Tooltip(string tooltip_text)
	{
		this.labels[this.activeLabelCount - 1].tooltip.toolTip = tooltip_text;
		return this;
	}

	// Token: 0x06004F09 RID: 20233 RVA: 0x001C1965 File Offset: 0x001BFB65
	public DetailsPanelDrawer Tooltip(Func<string> tooltip_cb)
	{
		this.labels[this.activeLabelCount - 1].tooltip.OnToolTip = tooltip_cb;
		return this;
	}

	// Token: 0x06004F0A RID: 20234 RVA: 0x001C1986 File Offset: 0x001BFB86
	public string Format(string format, float value)
	{
		return this.floatFormatter.Format(format, value);
	}

	// Token: 0x06004F0B RID: 20235 RVA: 0x001C1995 File Offset: 0x001BFB95
	public string Format(string format, string s0)
	{
		return this.stringformatter.Format(format, s0);
	}

	// Token: 0x06004F0C RID: 20236 RVA: 0x001C19A4 File Offset: 0x001BFBA4
	public string Format(string format, string s0, string s1)
	{
		return this.stringformatter.Format(format, s0, s1);
	}

	// Token: 0x06004F0D RID: 20237 RVA: 0x001C19B4 File Offset: 0x001BFBB4
	public DetailsPanelDrawer BeginDrawing()
	{
		this.activeLabelCount = 0;
		this.stringformatter.BeginDrawing();
		this.floatFormatter.BeginDrawing();
		return this;
	}

	// Token: 0x06004F0E RID: 20238 RVA: 0x001C19D4 File Offset: 0x001BFBD4
	public DetailsPanelDrawer EndDrawing()
	{
		this.floatFormatter.EndDrawing();
		this.stringformatter.EndDrawing();
		for (int i = this.activeLabelCount; i < this.labels.Count; i++)
		{
			if (this.labels[i].text.gameObject.activeSelf)
			{
				this.labels[i].text.gameObject.SetActive(false);
			}
		}
		return this;
	}

	// Token: 0x0400351E RID: 13598
	private List<DetailsPanelDrawer.Label> labels = new List<DetailsPanelDrawer.Label>();

	// Token: 0x0400351F RID: 13599
	private int activeLabelCount;

	// Token: 0x04003520 RID: 13600
	private UIStringFormatter stringformatter;

	// Token: 0x04003521 RID: 13601
	private UIFloatFormatter floatFormatter;

	// Token: 0x04003522 RID: 13602
	private GameObject parent;

	// Token: 0x04003523 RID: 13603
	private GameObject labelPrefab;

	// Token: 0x020018BD RID: 6333
	private struct Label
	{
		// Token: 0x0400723E RID: 29246
		public LocText text;

		// Token: 0x0400723F RID: 29247
		public ToolTip tooltip;
	}
}
