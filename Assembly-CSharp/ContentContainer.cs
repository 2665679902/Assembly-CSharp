using System;
using System.Collections.Generic;
using KSerialization.Converters;
using UnityEngine;

// Token: 0x02000A62 RID: 2658
public class ContentContainer
{
	// Token: 0x06005151 RID: 20817 RVA: 0x001D6D58 File Offset: 0x001D4F58
	public ContentContainer()
	{
		this.content = new List<ICodexWidget>();
	}

	// Token: 0x06005152 RID: 20818 RVA: 0x001D6D6B File Offset: 0x001D4F6B
	public ContentContainer(List<ICodexWidget> content, ContentContainer.ContentLayout contentLayout)
	{
		this.content = content;
		this.contentLayout = contentLayout;
	}

	// Token: 0x170005FE RID: 1534
	// (get) Token: 0x06005153 RID: 20819 RVA: 0x001D6D81 File Offset: 0x001D4F81
	// (set) Token: 0x06005154 RID: 20820 RVA: 0x001D6D89 File Offset: 0x001D4F89
	public List<ICodexWidget> content { get; set; }

	// Token: 0x170005FF RID: 1535
	// (get) Token: 0x06005155 RID: 20821 RVA: 0x001D6D92 File Offset: 0x001D4F92
	// (set) Token: 0x06005156 RID: 20822 RVA: 0x001D6D9A File Offset: 0x001D4F9A
	public string lockID { get; set; }

	// Token: 0x17000600 RID: 1536
	// (get) Token: 0x06005157 RID: 20823 RVA: 0x001D6DA3 File Offset: 0x001D4FA3
	// (set) Token: 0x06005158 RID: 20824 RVA: 0x001D6DAB File Offset: 0x001D4FAB
	[StringEnumConverter]
	public ContentContainer.ContentLayout contentLayout { get; set; }

	// Token: 0x17000601 RID: 1537
	// (get) Token: 0x06005159 RID: 20825 RVA: 0x001D6DB4 File Offset: 0x001D4FB4
	// (set) Token: 0x0600515A RID: 20826 RVA: 0x001D6DBC File Offset: 0x001D4FBC
	public bool showBeforeGeneratedContent { get; set; }

	// Token: 0x0400369E RID: 13982
	public GameObject go;

	// Token: 0x020018F9 RID: 6393
	public enum ContentLayout
	{
		// Token: 0x040072FA RID: 29434
		Vertical,
		// Token: 0x040072FB RID: 29435
		Horizontal,
		// Token: 0x040072FC RID: 29436
		Grid,
		// Token: 0x040072FD RID: 29437
		GridTwoColumn,
		// Token: 0x040072FE RID: 29438
		GridTwoColumnTall
	}
}
