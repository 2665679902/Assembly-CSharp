using System;
using System.Collections.Generic;

// Token: 0x020003E6 RID: 998
public class SchedulerGroup
{
	// Token: 0x1700008F RID: 143
	// (get) Token: 0x060014BA RID: 5306 RVA: 0x0006CD94 File Offset: 0x0006AF94
	// (set) Token: 0x060014BB RID: 5307 RVA: 0x0006CD9C File Offset: 0x0006AF9C
	public Scheduler scheduler { get; private set; }

	// Token: 0x060014BC RID: 5308 RVA: 0x0006CDA5 File Offset: 0x0006AFA5
	public SchedulerGroup(Scheduler scheduler)
	{
		this.scheduler = scheduler;
		this.Reset();
	}

	// Token: 0x060014BD RID: 5309 RVA: 0x0006CDC5 File Offset: 0x0006AFC5
	public void FreeResources()
	{
		if (this.scheduler != null)
		{
			this.scheduler.FreeResources();
		}
		this.scheduler = null;
		if (this.handles != null)
		{
			this.handles.Clear();
		}
		this.handles = null;
	}

	// Token: 0x060014BE RID: 5310 RVA: 0x0006CDFC File Offset: 0x0006AFFC
	public void Reset()
	{
		foreach (SchedulerHandle schedulerHandle in this.handles)
		{
			schedulerHandle.ClearScheduler();
		}
		this.handles.Clear();
	}

	// Token: 0x060014BF RID: 5311 RVA: 0x0006CE5C File Offset: 0x0006B05C
	public void Add(SchedulerHandle handle)
	{
		this.handles.Add(handle);
	}

	// Token: 0x04000B9B RID: 2971
	private List<SchedulerHandle> handles = new List<SchedulerHandle>();
}
