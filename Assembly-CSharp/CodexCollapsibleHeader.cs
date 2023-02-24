using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A71 RID: 2673
public class CodexCollapsibleHeader : CodexWidget<CodexCollapsibleHeader>
{
	// Token: 0x060051B6 RID: 20918 RVA: 0x001D871F File Offset: 0x001D691F
	public CodexCollapsibleHeader(string label, ContentContainer contents)
	{
		this.label = label;
		this.contents = contents;
	}

	// Token: 0x060051B7 RID: 20919 RVA: 0x001D8738 File Offset: 0x001D6938
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		HierarchyReferences component = contentGameObject.GetComponent<HierarchyReferences>();
		LocText reference = component.GetReference<LocText>("Label");
		reference.text = this.label;
		reference.textStyleSetting = textStyles[CodexTextStyle.Subtitle];
		reference.ApplySettings();
		MultiToggle reference2 = component.GetReference<MultiToggle>("ExpandToggle");
		reference2.ChangeState(1);
		reference2.onClick = delegate
		{
			this.ToggleCategoryOpen(contentGameObject, !this.contents.go.activeSelf);
		};
	}

	// Token: 0x060051B8 RID: 20920 RVA: 0x001D87AF File Offset: 0x001D69AF
	private void ToggleCategoryOpen(GameObject header, bool open)
	{
		header.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("ExpandToggle").ChangeState(open ? 1 : 0);
		this.contents.go.SetActive(open);
	}

	// Token: 0x040036CE RID: 14030
	private ContentContainer contents;

	// Token: 0x040036CF RID: 14031
	private string label;
}
