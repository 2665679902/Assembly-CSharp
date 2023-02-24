using System;
using System.Diagnostics;

// Token: 0x020000CE RID: 206
public struct LoggerFSSF
{
	// Token: 0x060007D8 RID: 2008 RVA: 0x00020118 File Offset: 0x0001E318
	public LoggerFSSF(string name)
	{
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x0002011A File Offset: 0x0001E31A
	public string GetName()
	{
		return "";
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x00020121 File Offset: 0x0001E321
	public void SetName(string name)
	{
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x00020123 File Offset: 0x0001E323
	[Conditional("ENABLE_LOGGER")]
	public void Log(string evt, string param, float value)
	{
	}
}
