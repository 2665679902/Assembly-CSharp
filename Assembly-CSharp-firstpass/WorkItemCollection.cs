using System;
using System.Collections.Generic;

// Token: 0x020000AA RID: 170
public class WorkItemCollection<WorkItemType, SharedDataType> : IWorkItemCollection where WorkItemType : IWorkItem<SharedDataType>
{
	// Token: 0x170000BA RID: 186
	// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001C935 File Offset: 0x0001AB35
	public int Count
	{
		get
		{
			return this.items.Count;
		}
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x0001C942 File Offset: 0x0001AB42
	public WorkItemType GetWorkItem(int idx)
	{
		return this.items[idx];
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x0001C950 File Offset: 0x0001AB50
	public void Add(WorkItemType work_item)
	{
		this.items.Add(work_item);
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x0001C960 File Offset: 0x0001AB60
	public void InternalDoWorkItem(int work_item_idx)
	{
		WorkItemType workItemType = this.items[work_item_idx];
		workItemType.Run(this.sharedData);
		this.items[work_item_idx] = workItemType;
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x0001C99A File Offset: 0x0001AB9A
	public void Reset(SharedDataType shared_data)
	{
		this.sharedData = shared_data;
		this.items.Clear();
	}

	// Token: 0x040005A9 RID: 1449
	private List<WorkItemType> items = new List<WorkItemType>();

	// Token: 0x040005AA RID: 1450
	private SharedDataType sharedData;
}
