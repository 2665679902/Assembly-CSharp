using System;
using System.Diagnostics;

// Token: 0x020000D4 RID: 212
public struct LoggerFSSS
{
	// Token: 0x060007F0 RID: 2032 RVA: 0x00020166 File Offset: 0x0001E366
	public LoggerFSSS(string name, int max_entries = 35)
	{
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x00020168 File Offset: 0x0001E368
	public string GetName()
	{
		return "";
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x0002016F File Offset: 0x0001E36F
	public void SetName(string name)
	{
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x00020171 File Offset: 0x0001E371
	[Conditional("ENABLE_LOGGER")]
	public void Log(string evt, string param0, string param1)
	{
	}
}
