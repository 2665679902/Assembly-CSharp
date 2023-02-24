using System;

// Token: 0x020000AB RID: 171
public static class GlobalJobManager
{
	// Token: 0x0600065D RID: 1629 RVA: 0x0001C9CD File Offset: 0x0001ABCD
	public static void Run(IWorkItemCollection work_items)
	{
		GlobalJobManager.jobManager.Run(work_items);
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x0001C9DA File Offset: 0x0001ABDA
	public static void Cleanup()
	{
		if (GlobalJobManager.jobManager != null)
		{
			GlobalJobManager.jobManager.Cleanup();
		}
		GlobalJobManager.jobManager = null;
	}

	// Token: 0x040005AB RID: 1451
	private static JobManager jobManager = new JobManager();
}
