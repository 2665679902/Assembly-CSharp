using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020000CA RID: 202
public class LoadProfiler : ProfilerBase
{
	// Token: 0x060007BC RID: 1980 RVA: 0x0001FDE8 File Offset: 0x0001DFE8
	private LoadProfiler(string file_prefix)
		: base(file_prefix)
	{
	}

	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x060007BD RID: 1981 RVA: 0x0001FDF4 File Offset: 0x0001DFF4
	public static LoadProfiler Instance
	{
		get
		{
			if (LoadProfiler.instance == null)
			{
				LoadProfiler.instance = new LoadProfiler("load_stats_");
				if (!Stopwatch.IsHighResolution)
				{
					UnityEngine.Debug.LogWarning("Low resolution timer! [" + Stopwatch.Frequency.ToString() + "] ticks per second");
				}
			}
			return LoadProfiler.instance;
		}
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x0001FE44 File Offset: 0x0001E044
	private static void ProfilerSection(string region_name, string file = "unknown", uint line = 0U)
	{
		LoadProfiler.Instance.Push(region_name, file, line);
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x0001FE53 File Offset: 0x0001E053
	private static void EndProfilerSection()
	{
		LoadProfiler.Instance.Pop();
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x0001FE60 File Offset: 0x0001E060
	[Conditional("ENABLE_LOAD_STATS")]
	public static void AddEvent(string event_name, string file = "unknown", uint line = 0U)
	{
		if (!LoadProfiler.Instance.IsRecording() || LoadProfiler.Instance.proFile == null)
		{
			return;
		}
		LoadProfiler.Instance.ManifestThreadInfo(null).WriteLine("GAME", event_name, LoadProfiler.Instance.sw, "I", "},");
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x0001FEB3 File Offset: 0x0001E0B3
	[Conditional("ENABLE_LOAD_STATS")]
	public static void BeginSample(string region_name)
	{
		LoadProfiler.Instance.Push(region_name, "unknown", 0U);
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x0001FEC6 File Offset: 0x0001E0C6
	[Conditional("ENABLE_LOAD_STATS")]
	public static void EndSample()
	{
		LoadProfiler.Instance.Pop();
	}

	// Token: 0x04000610 RID: 1552
	private static LoadProfiler instance;
}
