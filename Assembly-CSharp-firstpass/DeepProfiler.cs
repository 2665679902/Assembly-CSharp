using System;
using System.Diagnostics;

// Token: 0x02000093 RID: 147
public class DeepProfiler
{
	// Token: 0x060005A4 RID: 1444 RVA: 0x0001AAEE File Offset: 0x00018CEE
	public DeepProfiler(bool enable_profiling)
	{
		this.enableProfiling = enable_profiling;
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x0001AAFD File Offset: 0x00018CFD
	[Conditional("DEEP_PROFILE")]
	public void BeginSample(string message)
	{
		bool flag = this.enableProfiling;
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x0001AB06 File Offset: 0x00018D06
	[Conditional("DEEP_PROFILE")]
	public void EndSample()
	{
		bool flag = this.enableProfiling;
	}

	// Token: 0x04000577 RID: 1399
	private bool enableProfiling;
}
