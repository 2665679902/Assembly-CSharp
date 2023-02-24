using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

// Token: 0x020000AC RID: 172
public class JobManager
{
	// Token: 0x170000BB RID: 187
	// (get) Token: 0x0600065F RID: 1631 RVA: 0x0001C9F3 File Offset: 0x0001ABF3
	// (set) Token: 0x06000660 RID: 1632 RVA: 0x0001C9FB File Offset: 0x0001ABFB
	public bool isShuttingDown { get; private set; }

	// Token: 0x06000662 RID: 1634 RVA: 0x0001CA2C File Offset: 0x0001AC2C
	private void Initialize()
	{
		this.semaphore = new Semaphore(0, CPUBudget.coreCount);
		for (int i = 0; i < CPUBudget.coreCount; i++)
		{
			this.threads.Add(new JobManager.WorkerThread(this.semaphore, this, string.Format("KWorker{0}", i)));
		}
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x0001CA84 File Offset: 0x0001AC84
	public bool DoNextWorkItem()
	{
		int num = Interlocked.Increment(ref this.nextWorkIndex);
		if (num < this.workItems.Count)
		{
			this.workItems.InternalDoWorkItem(num);
			return true;
		}
		return false;
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x0001CABC File Offset: 0x0001ACBC
	public void Cleanup()
	{
		this.isShuttingDown = true;
		this.semaphore.Release(this.threads.Count);
		foreach (JobManager.WorkerThread workerThread in this.threads)
		{
			workerThread.Cleanup();
		}
		this.threads.Clear();
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x0001CB38 File Offset: 0x0001AD38
	public void Run(IWorkItemCollection work_items)
	{
		if (this.semaphore == null)
		{
			this.Initialize();
		}
		if (JobManager.runSingleThreaded || this.threads.Count == 0)
		{
			for (int i = 0; i < work_items.Count; i++)
			{
				work_items.InternalDoWorkItem(i);
			}
			return;
		}
		this.workerThreadCount = this.threads.Count;
		this.nextWorkIndex = -1;
		this.workItems = work_items;
		Thread.MemoryBarrier();
		this.semaphore.Release(this.threads.Count);
		this.manualResetEvent.WaitOne();
		this.manualResetEvent.Reset();
		if (JobManager.errorOccured)
		{
			foreach (JobManager.WorkerThread workerThread in this.threads)
			{
				workerThread.PrintExceptions();
			}
		}
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x0001CC20 File Offset: 0x0001AE20
	public void DecrementActiveWorkerThreadCount()
	{
		if (Interlocked.Decrement(ref this.workerThreadCount) == 0)
		{
			this.manualResetEvent.Set();
		}
	}

	// Token: 0x040005AC RID: 1452
	public static bool errorOccured;

	// Token: 0x040005AD RID: 1453
	private List<JobManager.WorkerThread> threads = new List<JobManager.WorkerThread>();

	// Token: 0x040005AE RID: 1454
	private Semaphore semaphore;

	// Token: 0x040005AF RID: 1455
	private IWorkItemCollection workItems;

	// Token: 0x040005B0 RID: 1456
	private int nextWorkIndex = -1;

	// Token: 0x040005B1 RID: 1457
	private int workerThreadCount;

	// Token: 0x040005B2 RID: 1458
	private ManualResetEvent manualResetEvent = new ManualResetEvent(false);

	// Token: 0x040005B4 RID: 1460
	private static bool runSingleThreaded;

	// Token: 0x020009E4 RID: 2532
	private class WorkerThread
	{
		// Token: 0x060053B9 RID: 21433 RVA: 0x0009C388 File Offset: 0x0009A588
		public WorkerThread(Semaphore semaphore, JobManager job_manager, string name)
		{
			this.semaphore = semaphore;
			this.thread = new Thread(new ParameterizedThreadStart(JobManager.WorkerThread.ThreadMain), 131072);
			Util.ApplyInvariantCultureToThread(this.thread);
			this.thread.Priority = ThreadPriority.AboveNormal;
			this.thread.Name = name;
			this.jobManager = job_manager;
			this.exceptions = new List<Exception>();
			this.thread.Start(this);
		}

		// Token: 0x060053BA RID: 21434 RVA: 0x0009C400 File Offset: 0x0009A600
		public void Run()
		{
			for (;;)
			{
				this.semaphore.WaitOne();
				if (this.jobManager.isShuttingDown)
				{
					break;
				}
				try
				{
					bool flag = true;
					while (flag)
					{
						flag = this.jobManager.DoNextWorkItem();
					}
				}
				catch (Exception ex)
				{
					this.exceptions.Add(ex);
					JobManager.errorOccured = true;
					Debugger.Break();
				}
				this.jobManager.DecrementActiveWorkerThreadCount();
			}
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x0009C474 File Offset: 0x0009A674
		public void PrintExceptions()
		{
			foreach (Exception ex in this.exceptions)
			{
				global::Debug.LogError(ex);
			}
		}

		// Token: 0x060053BC RID: 21436 RVA: 0x0009C4C4 File Offset: 0x0009A6C4
		public void Cleanup()
		{
		}

		// Token: 0x060053BD RID: 21437 RVA: 0x0009C4C6 File Offset: 0x0009A6C6
		public static void ThreadMain(object data)
		{
			((JobManager.WorkerThread)data).Run();
		}

		// Token: 0x04002225 RID: 8741
		private Thread thread;

		// Token: 0x04002226 RID: 8742
		private Semaphore semaphore;

		// Token: 0x04002227 RID: 8743
		private JobManager jobManager;

		// Token: 0x04002228 RID: 8744
		private List<Exception> exceptions;
	}
}
