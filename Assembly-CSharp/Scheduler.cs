using System;
using UnityEngine;

// Token: 0x020003E3 RID: 995
public class Scheduler : IScheduler
{
	// Token: 0x1700008A RID: 138
	// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0006CB25 File Offset: 0x0006AD25
	public int Count
	{
		get
		{
			return this.entries.Count;
		}
	}

	// Token: 0x060014A7 RID: 5287 RVA: 0x0006CB32 File Offset: 0x0006AD32
	public Scheduler(SchedulerClock clock)
	{
		this.clock = clock;
	}

	// Token: 0x060014A8 RID: 5288 RVA: 0x0006CB57 File Offset: 0x0006AD57
	public float GetTime()
	{
		return this.clock.GetTime();
	}

	// Token: 0x060014A9 RID: 5289 RVA: 0x0006CB64 File Offset: 0x0006AD64
	private SchedulerHandle Schedule(SchedulerEntry entry)
	{
		this.entries.Enqueue(entry.time, entry);
		return new SchedulerHandle(this, entry);
	}

	// Token: 0x060014AA RID: 5290 RVA: 0x0006CB80 File Offset: 0x0006AD80
	private SchedulerHandle Schedule(string name, float time, float time_interval, Action<object> callback, object callback_data, GameObject profiler_obj)
	{
		SchedulerEntry schedulerEntry = new SchedulerEntry(name, time + this.clock.GetTime(), time_interval, callback, callback_data, profiler_obj);
		return this.Schedule(schedulerEntry);
	}

	// Token: 0x060014AB RID: 5291 RVA: 0x0006CBB0 File Offset: 0x0006ADB0
	public void FreeResources()
	{
		this.clock = null;
		if (this.entries != null)
		{
			while (this.entries.Count > 0)
			{
				this.entries.Dequeue().Value.FreeResources();
			}
		}
		this.entries = null;
	}

	// Token: 0x060014AC RID: 5292 RVA: 0x0006CC00 File Offset: 0x0006AE00
	public SchedulerHandle Schedule(string name, float time, Action<object> callback, object callback_data = null, SchedulerGroup group = null)
	{
		if (group != null && group.scheduler != this)
		{
			global::Debug.LogError("Scheduler group mismatch!");
		}
		SchedulerHandle schedulerHandle = this.Schedule(name, time, -1f, callback, callback_data, null);
		if (group != null)
		{
			group.Add(schedulerHandle);
		}
		return schedulerHandle;
	}

	// Token: 0x060014AD RID: 5293 RVA: 0x0006CC44 File Offset: 0x0006AE44
	public void Clear(SchedulerHandle handle)
	{
		handle.entry.Clear();
	}

	// Token: 0x060014AE RID: 5294 RVA: 0x0006CC54 File Offset: 0x0006AE54
	public void Update()
	{
		if (this.Count == 0)
		{
			return;
		}
		int count = this.Count;
		int num = 0;
		using (new KProfiler.Region("Scheduler.Update", null))
		{
			float time = this.clock.GetTime();
			if (this.previousTime != time)
			{
				this.previousTime = time;
				while (num < count && time >= this.entries.Peek().Key)
				{
					SchedulerEntry value = this.entries.Dequeue().Value;
					if (value.callback != null)
					{
						value.callback(value.callbackData);
					}
					num++;
				}
			}
		}
	}

	// Token: 0x04000B95 RID: 2965
	public FloatHOTQueue<SchedulerEntry> entries = new FloatHOTQueue<SchedulerEntry>();

	// Token: 0x04000B96 RID: 2966
	private SchedulerClock clock;

	// Token: 0x04000B97 RID: 2967
	private float previousTime = float.NegativeInfinity;
}
