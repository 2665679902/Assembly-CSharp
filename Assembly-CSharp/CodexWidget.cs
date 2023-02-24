using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A61 RID: 2657
public abstract class CodexWidget<SubClass> : ICodexWidget
{
	// Token: 0x170005FC RID: 1532
	// (get) Token: 0x06005149 RID: 20809 RVA: 0x001D6CE9 File Offset: 0x001D4EE9
	// (set) Token: 0x0600514A RID: 20810 RVA: 0x001D6CF1 File Offset: 0x001D4EF1
	public int preferredWidth { get; set; }

	// Token: 0x170005FD RID: 1533
	// (get) Token: 0x0600514B RID: 20811 RVA: 0x001D6CFA File Offset: 0x001D4EFA
	// (set) Token: 0x0600514C RID: 20812 RVA: 0x001D6D02 File Offset: 0x001D4F02
	public int preferredHeight { get; set; }

	// Token: 0x0600514D RID: 20813 RVA: 0x001D6D0B File Offset: 0x001D4F0B
	protected CodexWidget()
	{
		this.preferredWidth = -1;
		this.preferredHeight = -1;
	}

	// Token: 0x0600514E RID: 20814 RVA: 0x001D6D21 File Offset: 0x001D4F21
	protected CodexWidget(int preferredWidth, int preferredHeight)
	{
		this.preferredWidth = preferredWidth;
		this.preferredHeight = preferredHeight;
	}

	// Token: 0x0600514F RID: 20815
	public abstract void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles);

	// Token: 0x06005150 RID: 20816 RVA: 0x001D6D37 File Offset: 0x001D4F37
	protected void ConfigurePreferredLayout(GameObject contentGameObject)
	{
		LayoutElement componentInChildren = contentGameObject.GetComponentInChildren<LayoutElement>();
		componentInChildren.preferredHeight = (float)this.preferredHeight;
		componentInChildren.preferredWidth = (float)this.preferredWidth;
	}
}
