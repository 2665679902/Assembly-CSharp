using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A64 RID: 2660
public class CodexTextWithTooltip : CodexWidget<CodexTextWithTooltip>
{
	// Token: 0x17000606 RID: 1542
	// (get) Token: 0x06005167 RID: 20839 RVA: 0x001D6EBA File Offset: 0x001D50BA
	// (set) Token: 0x06005168 RID: 20840 RVA: 0x001D6EC2 File Offset: 0x001D50C2
	public string text { get; set; }

	// Token: 0x17000607 RID: 1543
	// (get) Token: 0x06005169 RID: 20841 RVA: 0x001D6ECB File Offset: 0x001D50CB
	// (set) Token: 0x0600516A RID: 20842 RVA: 0x001D6ED3 File Offset: 0x001D50D3
	public string tooltip { get; set; }

	// Token: 0x17000608 RID: 1544
	// (get) Token: 0x0600516B RID: 20843 RVA: 0x001D6EDC File Offset: 0x001D50DC
	// (set) Token: 0x0600516C RID: 20844 RVA: 0x001D6EE4 File Offset: 0x001D50E4
	public CodexTextStyle style { get; set; }

	// Token: 0x17000609 RID: 1545
	// (get) Token: 0x0600516E RID: 20846 RVA: 0x001D6F00 File Offset: 0x001D5100
	// (set) Token: 0x0600516D RID: 20845 RVA: 0x001D6EED File Offset: 0x001D50ED
	public string stringKey
	{
		get
		{
			return "--> " + (this.text ?? "NULL");
		}
		set
		{
			this.text = Strings.Get(value);
		}
	}

	// Token: 0x0600516F RID: 20847 RVA: 0x001D6F1B File Offset: 0x001D511B
	public CodexTextWithTooltip()
	{
		this.style = CodexTextStyle.Body;
	}

	// Token: 0x06005170 RID: 20848 RVA: 0x001D6F2A File Offset: 0x001D512A
	public CodexTextWithTooltip(string text, string tooltip, CodexTextStyle style = CodexTextStyle.Body)
	{
		this.text = text;
		this.style = style;
		this.tooltip = tooltip;
	}

	// Token: 0x06005171 RID: 20849 RVA: 0x001D6F48 File Offset: 0x001D5148
	public void ConfigureLabel(LocText label, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		label.gameObject.SetActive(true);
		label.AllowLinks = this.style == CodexTextStyle.Body;
		label.textStyleSetting = textStyles[this.style];
		label.text = this.text;
		label.ApplySettings();
	}

	// Token: 0x06005172 RID: 20850 RVA: 0x001D6F94 File Offset: 0x001D5194
	public void ConfigureTooltip(ToolTip tooltip)
	{
		tooltip.SetSimpleTooltip(this.tooltip);
	}

	// Token: 0x06005173 RID: 20851 RVA: 0x001D6FA2 File Offset: 0x001D51A2
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		this.ConfigureLabel(contentGameObject.GetComponent<LocText>(), textStyles);
		this.ConfigureTooltip(contentGameObject.GetComponent<ToolTip>());
		base.ConfigurePreferredLayout(contentGameObject);
	}
}
