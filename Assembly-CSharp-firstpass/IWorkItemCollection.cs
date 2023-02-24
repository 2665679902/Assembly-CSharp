using System;

// Token: 0x020000A9 RID: 169
public interface IWorkItemCollection
{
	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x06000654 RID: 1620
	int Count { get; }

	// Token: 0x06000655 RID: 1621
	void InternalDoWorkItem(int work_item_idx);
}
