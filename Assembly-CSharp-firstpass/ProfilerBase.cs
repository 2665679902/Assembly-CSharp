using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

// Token: 0x020000C9 RID: 201
public class ProfilerBase
{
	// Token: 0x060007AD RID: 1965 RVA: 0x0001F868 File Offset: 0x0001DA68
	public static void StartLine(StringBuilder sb, string category, string region_name, int tid, Stopwatch sw, string ph)
	{
		sb.Append("{\"cat\":\"").Append(category).Append("\"");
		sb.Append(",\"name\":\"").Append(region_name).Append("\"");
		sb.Append(",\"pid\":0");
		sb.Append(",\"tid\":").Append(tid);
		long elapsedTicks = sw.ElapsedTicks;
		long frequency = Stopwatch.Frequency;
		long num = elapsedTicks * 1000000L / frequency;
		sb.Append(",\"ts\":").Append(num);
		sb.Append(",\"ph\":\"").Append(ph).Append("\"");
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x0001F911 File Offset: 0x0001DB11
	public static void WriteLine(StringBuilder sb, string category, string region_name, int tid, Stopwatch sw, string ph, string suffix)
	{
		ProfilerBase.StartLine(sb, category, region_name, tid, sw, ph);
		sb.Append(suffix).Append("\n");
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x0001F933 File Offset: 0x0001DB33
	protected bool IsRecording()
	{
		return this.proFile != null;
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x0001F93E File Offset: 0x0001DB3E
	public ProfilerBase(string file_prefix)
	{
		this.filePrefix = file_prefix;
		this.threadInfos = new Dictionary<int, ProfilerBase.ThreadInfo>();
		this.sw = new Stopwatch();
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x0001F96E File Offset: 0x0001DB6E
	public void Init()
	{
		this.proFile = null;
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x0001F977 File Offset: 0x0001DB77
	public void Finalise()
	{
		if (this.IsRecording())
		{
			this.StopRecording();
		}
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x0001F987 File Offset: 0x0001DB87
	public void ToggleRecording(string category = "GAME")
	{
		this.category = "G";
		if (!this.initialised)
		{
			this.initialised = true;
			this.Init();
		}
		if (this.IsRecording())
		{
			this.StopRecording();
			return;
		}
		this.StartRecording();
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x0001F9C0 File Offset: 0x0001DBC0
	public virtual void StartRecording()
	{
		foreach (KeyValuePair<int, ProfilerBase.ThreadInfo> keyValuePair in this.threadInfos)
		{
			keyValuePair.Value.Reset();
		}
		this.proFile = new StreamWriter(this.filePrefix + this.idx.ToString() + ".json");
		this.idx++;
		if (this.proFile != null)
		{
			this.proFile.WriteLine("{\"traceEvents\":[");
		}
		this.sw.Start();
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x0001FA74 File Offset: 0x0001DC74
	public virtual void StopRecording()
	{
		this.sw.Stop();
		if (this.proFile == null)
		{
			return;
		}
		foreach (KeyValuePair<int, ProfilerBase.ThreadInfo> keyValuePair in this.threadInfos)
		{
			this.proFile.Write(keyValuePair.Value.sb.ToString());
			keyValuePair.Value.Reset();
		}
		ProfilerBase.ThreadInfo threadInfo = this.ManifestThreadInfo("Main");
		threadInfo.WriteLine(this.category, "end", this.sw, "B", "},");
		threadInfo.WriteLine(this.category, "end", this.sw, "E", "}]}");
		this.proFile.Write(threadInfo.sb.ToString());
		threadInfo.Reset();
		this.proFile.Close();
		this.proFile = null;
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x0001FB80 File Offset: 0x0001DD80
	public virtual void BeginThreadProfiling(string threadGroupName, string threadName)
	{
		this.ManifestThreadInfo(threadName);
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x0001FB8C File Offset: 0x0001DD8C
	public virtual void EndThreadProfiling()
	{
		if (this.proFile != null)
		{
			this.proFile.Write(this.ManifestThreadInfo(null).sb.ToString());
		}
		Dictionary<int, ProfilerBase.ThreadInfo> dictionary = this.threadInfos;
		lock (dictionary)
		{
			this.threadInfos.Remove(Thread.CurrentThread.ManagedThreadId);
		}
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x0001FC00 File Offset: 0x0001DE00
	protected ProfilerBase.ThreadInfo ManifestThreadInfo(string name = null)
	{
		ProfilerBase.ThreadInfo threadInfo;
		if (!this.threadInfos.TryGetValue(Thread.CurrentThread.ManagedThreadId, out threadInfo))
		{
			threadInfo = new ProfilerBase.ThreadInfo(Thread.CurrentThread.ManagedThreadId);
			if (name != null)
			{
				threadInfo.name = name;
			}
			global::Debug.LogFormat("ManifestThreadInfo: {0}, {1}", new object[]
			{
				name,
				Thread.CurrentThread.ManagedThreadId
			});
			Dictionary<int, ProfilerBase.ThreadInfo> dictionary = this.threadInfos;
			lock (dictionary)
			{
				this.threadInfos.Add(Thread.CurrentThread.ManagedThreadId, threadInfo);
			}
		}
		if (name != null && threadInfo.name != name)
		{
			global::Debug.LogFormat("ManifestThreadInfo: change name {0} to {1}, {2}", new object[]
			{
				name,
				threadInfo.name,
				Thread.CurrentThread.ManagedThreadId
			});
			threadInfo.name = name;
			Dictionary<int, ProfilerBase.ThreadInfo> dictionary = this.threadInfos;
			lock (dictionary)
			{
				this.threadInfos[threadInfo.id] = threadInfo;
			}
		}
		return threadInfo;
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x0001FD30 File Offset: 0x0001DF30
	[Conditional("KPROFILER_VALIDATE_REGION_NAME")]
	private void ValidateRegionName(string region_name)
	{
		DebugUtil.Assert(!region_name.Contains("\""));
		region_name = "InvalidRegionName";
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x0001FD4C File Offset: 0x0001DF4C
	protected void Push(string region_name, string file, uint line)
	{
		if (!this.IsRecording())
		{
			return;
		}
		ProfilerBase.ThreadInfo threadInfo = this.ManifestThreadInfo(null);
		threadInfo.regionStack.Push(region_name);
		threadInfo.WriteLine(this.category, region_name, this.sw, "B", "},");
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x0001FD94 File Offset: 0x0001DF94
	protected void Pop()
	{
		if (!this.IsRecording())
		{
			return;
		}
		ProfilerBase.ThreadInfo threadInfo = this.ManifestThreadInfo(null);
		if (threadInfo.regionStack.Count == 0)
		{
			return;
		}
		threadInfo.WriteLine(this.category, threadInfo.regionStack.Pop(), this.sw, "E", "},");
	}

	// Token: 0x04000609 RID: 1545
	private bool initialised;

	// Token: 0x0400060A RID: 1546
	private int idx;

	// Token: 0x0400060B RID: 1547
	protected StreamWriter proFile;

	// Token: 0x0400060C RID: 1548
	private string category = "GAME";

	// Token: 0x0400060D RID: 1549
	private string filePrefix;

	// Token: 0x0400060E RID: 1550
	protected Dictionary<int, ProfilerBase.ThreadInfo> threadInfos;

	// Token: 0x0400060F RID: 1551
	public Stopwatch sw;

	// Token: 0x020009ED RID: 2541
	protected struct ThreadInfo
	{
		// Token: 0x060053D1 RID: 21457 RVA: 0x0009C5DF File Offset: 0x0009A7DF
		public ThreadInfo(int id)
		{
			this.regionStack = new Stack<string>();
			this.sb = new StringBuilder();
			this.id = id;
			this.name = string.Empty;
		}

		// Token: 0x060053D2 RID: 21458 RVA: 0x0009C609 File Offset: 0x0009A809
		public void Reset()
		{
			this.regionStack.Clear();
			this.sb.Length = 0;
		}

		// Token: 0x060053D3 RID: 21459 RVA: 0x0009C622 File Offset: 0x0009A822
		public void WriteLine(string category, string region_name, Stopwatch sw, string ph, string suffix)
		{
			ProfilerBase.WriteLine(this.sb, category, region_name, this.id, sw, ph, suffix);
		}

		// Token: 0x060053D4 RID: 21460 RVA: 0x0009C63C File Offset: 0x0009A83C
		public void StartLine(string category, string region_name, Stopwatch sw, string ph)
		{
			ProfilerBase.StartLine(this.sb, category, region_name, this.id, sw, ph);
		}

		// Token: 0x04002235 RID: 8757
		public Stack<string> regionStack;

		// Token: 0x04002236 RID: 8758
		public StringBuilder sb;

		// Token: 0x04002237 RID: 8759
		public int id;

		// Token: 0x04002238 RID: 8760
		public string name;
	}
}
