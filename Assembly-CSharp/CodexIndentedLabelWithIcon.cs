using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A68 RID: 2664
public class CodexIndentedLabelWithIcon : CodexWidget<CodexIndentedLabelWithIcon>
{
	// Token: 0x1700060E RID: 1550
	// (get) Token: 0x06005187 RID: 20871 RVA: 0x001D7168 File Offset: 0x001D5368
	// (set) Token: 0x06005188 RID: 20872 RVA: 0x001D7170 File Offset: 0x001D5370
	public CodexImage icon { get; set; }

	// Token: 0x1700060F RID: 1551
	// (get) Token: 0x06005189 RID: 20873 RVA: 0x001D7179 File Offset: 0x001D5379
	// (set) Token: 0x0600518A RID: 20874 RVA: 0x001D7181 File Offset: 0x001D5381
	public CodexText label { get; set; }

	// Token: 0x0600518B RID: 20875 RVA: 0x001D718A File Offset: 0x001D538A
	public CodexIndentedLabelWithIcon()
	{
	}

	// Token: 0x0600518C RID: 20876 RVA: 0x001D7192 File Offset: 0x001D5392
	public CodexIndentedLabelWithIcon(string text, CodexTextStyle style, global::Tuple<Sprite, Color> coloredSprite)
	{
		this.icon = new CodexImage(coloredSprite);
		this.label = new CodexText(text, style, null);
	}

	// Token: 0x0600518D RID: 20877 RVA: 0x001D71B4 File Offset: 0x001D53B4
	public CodexIndentedLabelWithIcon(string text, CodexTextStyle style, global::Tuple<Sprite, Color> coloredSprite, int iconWidth, int iconHeight)
	{
		this.icon = new CodexImage(iconWidth, iconHeight, coloredSprite);
		this.label = new CodexText(text, style, null);
	}

	// Token: 0x0600518E RID: 20878 RVA: 0x001D71DC File Offset: 0x001D53DC
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		Image componentInChildren = contentGameObject.GetComponentInChildren<Image>();
		this.icon.ConfigureImage(componentInChildren);
		this.label.ConfigureLabel(contentGameObject.GetComponentInChildren<LocText>(), textStyles);
		if (this.icon.preferredWidth != -1 && this.icon.preferredHeight != -1)
		{
			LayoutElement component = componentInChildren.GetComponent<LayoutElement>();
			component.minWidth = (float)this.icon.preferredHeight;
			component.minHeight = (float)this.icon.preferredWidth;
			component.preferredHeight = (float)this.icon.preferredHeight;
			component.preferredWidth = (float)this.icon.preferredWidth;
		}
	}
}
