using System;
using System.Diagnostics;

// Token: 0x020000D3 RID: 211
public struct LoggerFSS
{
	// Token: 0x060007EC RID: 2028 RVA: 0x00020159 File Offset: 0x0001E359
	public LoggerFSS(string name, int max_entries = 35)
	{
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x0002015B File Offset: 0x0001E35B
	public string GetName()
	{
		return "";
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x00020162 File Offset: 0x0001E362
	public void SetName(string name)
	{
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x00020164 File Offset: 0x0001E364
	[Conditional("ENABLE_LOGGER")]
	public void Log(string evt, string param = "")
	{
	}
}
