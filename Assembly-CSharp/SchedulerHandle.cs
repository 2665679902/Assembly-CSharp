using System;

// Token: 0x020003E7 RID: 999
public struct SchedulerHandle
{
	// Token: 0x060014C0 RID: 5312 RVA: 0x0006CE6A File Offset: 0x0006B06A
	public SchedulerHandle(Scheduler scheduler, SchedulerEntry entry)
	{
		this.entry = entry;
		this.scheduler = scheduler;
	}

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x060014C1 RID: 5313 RVA: 0x0006CE7A File Offset: 0x0006B07A
	public float TimeRemaining
	{
		get
		{
			if (!this.IsValid)
			{
				return -1f;
			}
			return this.entry.time - this.scheduler.GetTime();
		}
	}

	// Token: 0x060014C2 RID: 5314 RVA: 0x0006CEA1 File Offset: 0x0006B0A1
	public void FreeResources()
	{
		this.entry.FreeResources();
		this.scheduler = null;
	}

	// Token: 0x060014C3 RID: 5315 RVA: 0x0006CEB5 File Offset: 0x0006B0B5
	public void ClearScheduler()
	{
		if (this.scheduler == null)
		{
			return;
		}
		this.scheduler.Clear(this);
		this.scheduler = null;
	}

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x060014C4 RID: 5316 RVA: 0x0006CED8 File Offset: 0x0006B0D8
	public bool IsValid
	{
		get
		{
			return this.scheduler != null;
		}
	}

	// Token: 0x04000B9C RID: 2972
	public SchedulerEntry entry;

	// Token: 0x04000B9D RID: 2973
	private Scheduler scheduler;
}
