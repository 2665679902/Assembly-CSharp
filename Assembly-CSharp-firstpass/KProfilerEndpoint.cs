using System;
using System.Diagnostics;

// Token: 0x020000C5 RID: 197
public class KProfilerEndpoint
{
	// Token: 0x06000781 RID: 1921 RVA: 0x0001F6DC File Offset: 0x0001D8DC
	[Conditional("ENABLE_KPROFILER")]
	public virtual void Begin(string name, string group)
	{
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x0001F6DE File Offset: 0x0001D8DE
	[Conditional("ENABLE_KPROFILER")]
	public virtual void End(string name)
	{
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x0001F6E0 File Offset: 0x0001D8E0
	[Conditional("ENABLE_KPROFILER")]
	public virtual void BeginFrame()
	{
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x0001F6E2 File Offset: 0x0001D8E2
	[Conditional("ENABLE_KPROFILER")]
	public virtual void Ping(string display, string group, double value)
	{
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x0001F6E4 File Offset: 0x0001D8E4
	[Conditional("ENABLE_KPROFILER")]
	public virtual void BeginAsync(string display, string group)
	{
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x0001F6E6 File Offset: 0x0001D8E6
	[Conditional("ENABLE_KPROFILER")]
	public virtual void EndAsync(string display)
	{
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x0001F6E8 File Offset: 0x0001D8E8
	[Conditional("ENABLE_KPROFILER")]
	public virtual void EndFrame()
	{
	}
}
