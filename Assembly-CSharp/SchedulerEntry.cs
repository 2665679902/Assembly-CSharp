using System;
using UnityEngine;

// Token: 0x020003E5 RID: 997
public struct SchedulerEntry
{
	// Token: 0x1700008B RID: 139
	// (get) Token: 0x060014B1 RID: 5297 RVA: 0x0006CD1C File Offset: 0x0006AF1C
	// (set) Token: 0x060014B2 RID: 5298 RVA: 0x0006CD24 File Offset: 0x0006AF24
	public SchedulerEntry.Details details { readonly get; private set; }

	// Token: 0x060014B3 RID: 5299 RVA: 0x0006CD2D File Offset: 0x0006AF2D
	public SchedulerEntry(string name, float time, float time_interval, Action<object> callback, object callback_data, GameObject profiler_obj)
	{
		this.time = time;
		this.details = new SchedulerEntry.Details(name, callback, callback_data, time_interval, profiler_obj);
	}

	// Token: 0x060014B4 RID: 5300 RVA: 0x0006CD49 File Offset: 0x0006AF49
	public void FreeResources()
	{
		this.details = null;
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x060014B5 RID: 5301 RVA: 0x0006CD52 File Offset: 0x0006AF52
	public Action<object> callback
	{
		get
		{
			return this.details.callback;
		}
	}

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x060014B6 RID: 5302 RVA: 0x0006CD5F File Offset: 0x0006AF5F
	public object callbackData
	{
		get
		{
			return this.details.callbackData;
		}
	}

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x060014B7 RID: 5303 RVA: 0x0006CD6C File Offset: 0x0006AF6C
	public float timeInterval
	{
		get
		{
			return this.details.timeInterval;
		}
	}

	// Token: 0x060014B8 RID: 5304 RVA: 0x0006CD79 File Offset: 0x0006AF79
	public override string ToString()
	{
		return this.time.ToString();
	}

	// Token: 0x060014B9 RID: 5305 RVA: 0x0006CD86 File Offset: 0x0006AF86
	public void Clear()
	{
		this.details.callback = null;
	}

	// Token: 0x04000B98 RID: 2968
	public float time;

	// Token: 0x02001001 RID: 4097
	public class Details
	{
		// Token: 0x0600712D RID: 28973 RVA: 0x002A8A0B File Offset: 0x002A6C0B
		public Details(string name, Action<object> callback, object callback_data, float time_interval, GameObject profiler_obj)
		{
			this.timeInterval = time_interval;
			this.callback = callback;
			this.callbackData = callback_data;
		}

		// Token: 0x04005632 RID: 22066
		public Action<object> callback;

		// Token: 0x04005633 RID: 22067
		public object callbackData;

		// Token: 0x04005634 RID: 22068
		public float timeInterval;
	}
}
