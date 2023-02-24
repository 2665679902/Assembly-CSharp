using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

// Token: 0x020000C8 RID: 200
public static class KProfiler
{
	// Token: 0x06000793 RID: 1939 RVA: 0x0001F719 File Offset: 0x0001D919
	public static bool IsEnabled()
	{
		return KProfiler.enabled;
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x0001F720 File Offset: 0x0001D920
	public static void Enable()
	{
		KProfiler.enabled = true;
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x0001F728 File Offset: 0x0001D928
	public static void Disable()
	{
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x0001F72A File Offset: 0x0001D92A
	public static void BeginThread(string name, string group)
	{
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x0001F72C File Offset: 0x0001D92C
	public static void BeginFrame()
	{
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x0001F72E File Offset: 0x0001D92E
	public static void EndFrame()
	{
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x0001F730 File Offset: 0x0001D930
	public static int BeginSampleI(string region_name, string group = "Game")
	{
		int num = KProfiler.counter;
		KProfiler.counter++;
		return num;
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x0001F743 File Offset: 0x0001D943
	public static int EndSampleI(string region_name = null)
	{
		KProfiler.counter--;
		return KProfiler.counter;
	}

	// Token: 0x0600079B RID: 1947 RVA: 0x0001F756 File Offset: 0x0001D956
	public static string SanitizeName(string name)
	{
		return KProfiler.re.Replace(name, "${1}");
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x0001F768 File Offset: 0x0001D968
	[Conditional("ENABLE_KPROFILER")]
	public static void Ping(string display, string group, double value)
	{
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x0001F76A File Offset: 0x0001D96A
	[Conditional("ENABLE_KPROFILER")]
	public static void BeginAsync(string display, string group = "Game")
	{
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x0001F76C File Offset: 0x0001D96C
	[Conditional("ENABLE_KPROFILER")]
	public static void EndAsync(string display)
	{
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x0001F76E File Offset: 0x0001D96E
	[Conditional("ENABLE_KPROFILER")]
	public static void BeginSample(string region_name, string group = "Game")
	{
		KProfiler.BeginSampleI(region_name, group);
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x0001F778 File Offset: 0x0001D978
	[Conditional("ENABLE_KPROFILER")]
	public static void EndSample(string region_name = null)
	{
		KProfiler.EndSampleI(region_name);
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x0001F781 File Offset: 0x0001D981
	[Conditional("ENABLE_KPROFILER")]
	public static void EndSample(string region_name, int count)
	{
		KProfiler.EndSampleI(region_name);
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x0001F78A File Offset: 0x0001D98A
	[Conditional("ENABLE_KPROFILER")]
	public static void BeginSample(string region_name, int count)
	{
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x0001F78C File Offset: 0x0001D98C
	[Conditional("ENABLE_KPROFILER")]
	public static void BeginSample(string region_name, string group, int count)
	{
		KProfiler.BeginSampleI(region_name, group);
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x0001F796 File Offset: 0x0001D996
	public static int BeginSampleI(string region_name, UnityEngine.Object profiler_obj)
	{
		int num = KProfiler.counter;
		KProfiler.counter++;
		return num;
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x0001F7A9 File Offset: 0x0001D9A9
	[Conditional("ENABLE_KPROFILER")]
	public static void BeginSample(string region_name, UnityEngine.Object profiler_obj)
	{
		KProfiler.BeginSampleI(region_name, profiler_obj);
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x0001F7B3 File Offset: 0x0001D9B3
	[Conditional("ENABLE_KPROFILER")]
	public static void AddEvent(string event_name)
	{
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x0001F7B8 File Offset: 0x0001D9B8
	[Conditional("ENABLE_KPROFILER")]
	public static void AddCounter(string event_name, List<KeyValuePair<string, int>> series_name_counts)
	{
		foreach (KeyValuePair<string, int> keyValuePair in series_name_counts)
		{
			KProfiler.EndSampleI(null);
		}
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x0001F808 File Offset: 0x0001DA08
	[Conditional("ENABLE_KPROFILER")]
	public static void AddCounter(string event_name, string series_name, int count)
	{
		KProfiler.EndSampleI(series_name);
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x0001F811 File Offset: 0x0001DA11
	[Conditional("ENABLE_KPROFILER")]
	public static void AddCounter(string event_name, int count)
	{
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x0001F813 File Offset: 0x0001DA13
	[Conditional("ENABLE_KPROFILER")]
	public static void BeginThreadProfiling(string threadGroupName, string threadName)
	{
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x0001F815 File Offset: 0x0001DA15
	[Conditional("ENABLE_KPROFILER")]
	public static void EndThreadProfiling()
	{
	}

	// Token: 0x04000601 RID: 1537
	private static bool enabled = false;

	// Token: 0x04000602 RID: 1538
	public static int counter = 0;

	// Token: 0x04000603 RID: 1539
	public static Thread main_thread;

	// Token: 0x04000604 RID: 1540
	public static KProfilerEndpoint AppEndpoint = new KProfilerPluginEndpoint();

	// Token: 0x04000605 RID: 1541
	public static KProfilerEndpoint UnityEndpoint = new KProfilerEndpoint();

	// Token: 0x04000606 RID: 1542
	public static KProfilerEndpoint ChromeEndpoint = new KProfilerEndpoint();

	// Token: 0x04000607 RID: 1543
	private static string pattern = "<link=\"(.+)\">(.+)<\\/link>";

	// Token: 0x04000608 RID: 1544
	private static Regex re = new Regex(KProfiler.pattern);

	// Token: 0x020009EC RID: 2540
	public struct Region : IDisposable
	{
		// Token: 0x060053CF RID: 21455 RVA: 0x0009C5D4 File Offset: 0x0009A7D4
		public Region(string region_name, UnityEngine.Object profiler_obj = null)
		{
			this.regionName = region_name;
		}

		// Token: 0x060053D0 RID: 21456 RVA: 0x0009C5DD File Offset: 0x0009A7DD
		public void Dispose()
		{
		}

		// Token: 0x04002234 RID: 8756
		private string regionName;
	}
}
