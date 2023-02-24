using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A6B RID: 2667
public class CodexLabelWithIcon : CodexWidget<CodexLabelWithIcon>
{
	// Token: 0x17000610 RID: 1552
	// (get) Token: 0x06005193 RID: 20883 RVA: 0x001D7292 File Offset: 0x001D5492
	// (set) Token: 0x06005194 RID: 20884 RVA: 0x001D729A File Offset: 0x001D549A
	public CodexImage icon { get; set; }

	// Token: 0x17000611 RID: 1553
	// (get) Token: 0x06005195 RID: 20885 RVA: 0x001D72A3 File Offset: 0x001D54A3
	// (set) Token: 0x06005196 RID: 20886 RVA: 0x001D72AB File Offset: 0x001D54AB
	public CodexText label { get; set; }

	// Token: 0x06005197 RID: 20887 RVA: 0x001D72B4 File Offset: 0x001D54B4
	public CodexLabelWithIcon()
	{
	}

	// Token: 0x06005198 RID: 20888 RVA: 0x001D72BC File Offset: 0x001D54BC
	public CodexLabelWithIcon(string text, CodexTextStyle style, global::Tuple<Sprite, Color> coloredSprite)
	{
		this.icon = new CodexImage(coloredSprite);
		this.label = new CodexText(text, style, null);
	}

	// Token: 0x06005199 RID: 20889 RVA: 0x001D72DE File Offset: 0x001D54DE
	public CodexLabelWithIcon(string text, CodexTextStyle style, global::Tuple<Sprite, Color> coloredSprite, int iconWidth, int iconHeight)
	{
		this.icon = new CodexImage(iconWidth, iconHeight, coloredSprite);
		this.label = new CodexText(text, style, null);
	}

	// Token: 0x0600519A RID: 20890 RVA: 0x001D7304 File Offset: 0x001D5504
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		this.icon.ConfigureImage(contentGameObject.GetComponentInChildren<Image>());
		if (this.icon.preferredWidth != -1 && this.icon.preferredHeight != -1)
		{
			LayoutElement component = contentGameObject.GetComponentInChildren<Image>().GetComponent<LayoutElement>();
			component.minWidth = (float)this.icon.preferredHeight;
			component.minHeight = (float)this.icon.preferredWidth;
			component.preferredHeight = (float)this.icon.preferredHeight;
			component.preferredWidth = (float)this.icon.preferredWidth;
		}
		this.label.ConfigureLabel(contentGameObject.GetComponentInChildren<LocText>(), textStyles);
	}
}
