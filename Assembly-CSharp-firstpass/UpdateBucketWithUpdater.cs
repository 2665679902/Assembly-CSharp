using System;
using System.Collections.Generic;
using System.Diagnostics;

// Token: 0x020000FF RID: 255
[DebuggerDisplay("{name}")]
public class UpdateBucketWithUpdater<DataType> : StateMachineUpdater.BaseUpdateBucket
{
	// Token: 0x170000EE RID: 238
	// (get) Token: 0x060008AC RID: 2220 RVA: 0x00022D50 File Offset: 0x00020F50
	public override int count
	{
		get
		{
			return this.entries.Count;
		}
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x00022D5D File Offset: 0x00020F5D
	public UpdateBucketWithUpdater(string name)
		: base(name)
	{
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x00022D80 File Offset: 0x00020F80
	public HandleVector<int>.Handle Add(DataType data, float last_update_time, UpdateBucketWithUpdater<DataType>.IUpdater updater)
	{
		UpdateBucketWithUpdater<DataType>.Entry entry = default(UpdateBucketWithUpdater<DataType>.Entry);
		entry.data = data;
		entry.lastUpdateTime = last_update_time;
		entry.updater = updater;
		HandleVector<int>.Handle handle = this.entries.Allocate(entry);
		this.entries.SetData(handle, entry);
		return handle;
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x00022DC8 File Offset: 0x00020FC8
	public override void Remove(HandleVector<int>.Handle handle)
	{
		this.pendingRemovals.Add(handle);
		UpdateBucketWithUpdater<DataType>.Entry data = this.entries.GetData(handle);
		data.updater = null;
		this.entries.SetData(handle, data);
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x00022E04 File Offset: 0x00021004
	public override void Update(float dt)
	{
		if (KMonoBehaviour.isLoadingScene)
		{
			return;
		}
		List<UpdateBucketWithUpdater<DataType>.Entry> dataList = this.entries.GetDataList();
		foreach (HandleVector<int>.Handle handle in this.pendingRemovals)
		{
			this.entries.Free(handle);
		}
		this.pendingRemovals.Clear();
		if (this.batch_update_delegate != null)
		{
			this.batch_update_delegate(dataList, dt);
			return;
		}
		int count = dataList.Count;
		for (int i = 0; i < count; i++)
		{
			UpdateBucketWithUpdater<DataType>.Entry entry = dataList[i];
			if (entry.updater != null)
			{
				entry.updater.Update(entry.data, dt - entry.lastUpdateTime);
				entry.lastUpdateTime = 0f;
				dataList[i] = entry;
			}
		}
	}

	// Token: 0x0400065F RID: 1631
	private KCompactedVector<UpdateBucketWithUpdater<DataType>.Entry> entries = new KCompactedVector<UpdateBucketWithUpdater<DataType>.Entry>(0);

	// Token: 0x04000660 RID: 1632
	private List<HandleVector<int>.Handle> pendingRemovals = new List<HandleVector<int>.Handle>();

	// Token: 0x04000661 RID: 1633
	public UpdateBucketWithUpdater<DataType>.BatchUpdateDelegate batch_update_delegate;

	// Token: 0x020009FF RID: 2559
	public struct Entry
	{
		// Token: 0x0400224E RID: 8782
		public DataType data;

		// Token: 0x0400224F RID: 8783
		public float lastUpdateTime;

		// Token: 0x04002250 RID: 8784
		public UpdateBucketWithUpdater<DataType>.IUpdater updater;
	}

	// Token: 0x02000A00 RID: 2560
	public interface IUpdater
	{
		// Token: 0x0600540E RID: 21518
		void Update(DataType smi, float dt);
	}

	// Token: 0x02000A01 RID: 2561
	// (Invoke) Token: 0x06005410 RID: 21520
	public delegate void BatchUpdateDelegate(List<UpdateBucketWithUpdater<DataType>.Entry> items, float time_delta);
}
