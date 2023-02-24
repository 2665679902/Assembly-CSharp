using System;
using System.Diagnostics;

// Token: 0x020000D6 RID: 214
public struct LoggerTimeString
{
	// Token: 0x060007F8 RID: 2040 RVA: 0x00020180 File Offset: 0x0001E380
	public LoggerTimeString(string name, int max_entries = 35)
	{
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x00020182 File Offset: 0x0001E382
	public string GetName()
	{
		return "";
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x00020189 File Offset: 0x0001E389
	public void SetName(string name)
	{
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x0002018B File Offset: 0x0001E38B
	[Conditional("ENABLE_LOGGER")]
	public void Log(string evt)
	{
	}
}
