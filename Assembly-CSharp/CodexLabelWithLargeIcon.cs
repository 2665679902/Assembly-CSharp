using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A6C RID: 2668
public class CodexLabelWithLargeIcon : CodexLabelWithIcon
{
	// Token: 0x17000612 RID: 1554
	// (get) Token: 0x0600519B RID: 20891 RVA: 0x001D73A2 File Offset: 0x001D55A2
	// (set) Token: 0x0600519C RID: 20892 RVA: 0x001D73AA File Offset: 0x001D55AA
	public string linkID { get; set; }

	// Token: 0x0600519D RID: 20893 RVA: 0x001D73B3 File Offset: 0x001D55B3
	public CodexLabelWithLargeIcon()
	{
	}

	// Token: 0x0600519E RID: 20894 RVA: 0x001D73BC File Offset: 0x001D55BC
	public CodexLabelWithLargeIcon(string text, CodexTextStyle style, global::Tuple<Sprite, Color> coloredSprite, string targetEntrylinkID)
		: base(text, style, coloredSprite, 128, 128)
	{
		base.icon = new CodexImage(128, 128, coloredSprite);
		base.label = new CodexText(text, style, null);
		this.linkID = targetEntrylinkID;
	}

	// Token: 0x0600519F RID: 20895 RVA: 0x001D7408 File Offset: 0x001D5608
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		base.icon.ConfigureImage(contentGameObject.GetComponentsInChildren<Image>()[1]);
		if (base.icon.preferredWidth != -1 && base.icon.preferredHeight != -1)
		{
			LayoutElement component = contentGameObject.GetComponentsInChildren<Image>()[1].GetComponent<LayoutElement>();
			component.minWidth = (float)base.icon.preferredHeight;
			component.minHeight = (float)base.icon.preferredWidth;
			component.preferredHeight = (float)base.icon.preferredHeight;
			component.preferredWidth = (float)base.icon.preferredWidth;
		}
		base.label.text = UI.StripLinkFormatting(base.label.text);
		base.label.ConfigureLabel(contentGameObject.GetComponentInChildren<LocText>(), textStyles);
		contentGameObject.GetComponent<KButton>().ClearOnClick();
		contentGameObject.GetComponent<KButton>().onClick += delegate
		{
			ManagementMenu.Instance.codexScreen.ChangeArticle(this.linkID, false, default(Vector3), CodexScreen.HistoryDirection.NewArticle);
		};
	}
}
