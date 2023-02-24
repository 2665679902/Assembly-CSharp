using System;
using System.Diagnostics;

// Token: 0x020000D0 RID: 208
public struct LoggerFIO
{
	// Token: 0x060007E0 RID: 2016 RVA: 0x00020132 File Offset: 0x0001E332
	public LoggerFIO(string name, int max_entries = 35)
	{
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x00020134 File Offset: 0x0001E334
	public string GetName()
	{
		return "";
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x0002013B File Offset: 0x0001E33B
	public void SetName(string name)
	{
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x0002013D File Offset: 0x0001E33D
	[Conditional("ENABLE_LOGGER")]
	public void Log(int evt, object obj)
	{
	}
}
