using System;
using System.Collections.Generic;

// Token: 0x02000A5A RID: 2650
public class CategoryEntry : CodexEntry
{
	// Token: 0x170005E5 RID: 1509
	// (get) Token: 0x060050BB RID: 20667 RVA: 0x001CF196 File Offset: 0x001CD396
	// (set) Token: 0x060050BC RID: 20668 RVA: 0x001CF19E File Offset: 0x001CD39E
	public bool largeFormat { get; set; }

	// Token: 0x170005E6 RID: 1510
	// (get) Token: 0x060050BD RID: 20669 RVA: 0x001CF1A7 File Offset: 0x001CD3A7
	// (set) Token: 0x060050BE RID: 20670 RVA: 0x001CF1AF File Offset: 0x001CD3AF
	public bool sort { get; set; }

	// Token: 0x060050BF RID: 20671 RVA: 0x001CF1B8 File Offset: 0x001CD3B8
	public CategoryEntry(string category, List<ContentContainer> contentContainers, string name, List<CodexEntry> entriesInCategory, bool largeFormat, bool sort)
		: base(category, contentContainers, name)
	{
		this.entriesInCategory = entriesInCategory;
		this.largeFormat = largeFormat;
		this.sort = sort;
	}

	// Token: 0x04003646 RID: 13894
	public List<CodexEntry> entriesInCategory = new List<CodexEntry>();
}
