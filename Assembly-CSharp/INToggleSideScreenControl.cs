using System;
using System.Collections.Generic;

// Token: 0x02000BC9 RID: 3017
public interface INToggleSideScreenControl
{
	// Token: 0x1700069E RID: 1694
	// (get) Token: 0x06005EDD RID: 24285
	string SidescreenTitleKey { get; }

	// Token: 0x1700069F RID: 1695
	// (get) Token: 0x06005EDE RID: 24286
	List<LocString> Options { get; }

	// Token: 0x170006A0 RID: 1696
	// (get) Token: 0x06005EDF RID: 24287
	List<LocString> Tooltips { get; }

	// Token: 0x170006A1 RID: 1697
	// (get) Token: 0x06005EE0 RID: 24288
	string Description { get; }

	// Token: 0x170006A2 RID: 1698
	// (get) Token: 0x06005EE1 RID: 24289
	int SelectedOption { get; }

	// Token: 0x170006A3 RID: 1699
	// (get) Token: 0x06005EE2 RID: 24290
	int QueuedOption { get; }

	// Token: 0x06005EE3 RID: 24291
	void QueueSelectedOption(int option);
}
