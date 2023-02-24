using System;
using System.Diagnostics;

// Token: 0x020000CF RID: 207
public struct LoggerSSF
{
	// Token: 0x060007DC RID: 2012 RVA: 0x00020125 File Offset: 0x0001E325
	public LoggerSSF(string name, int max_entries = 35)
	{
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x00020127 File Offset: 0x0001E327
	public string GetName()
	{
		return "";
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x0002012E File Offset: 0x0001E32E
	public void SetName(string name)
	{
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x00020130 File Offset: 0x0001E330
	[Conditional("ENABLE_LOGGER")]
	public void Log(string evt, string param, float value)
	{
	}
}
