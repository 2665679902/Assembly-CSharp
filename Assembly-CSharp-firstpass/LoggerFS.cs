using System;
using System.Diagnostics;

// Token: 0x020000D1 RID: 209
public struct LoggerFS
{
	// Token: 0x060007E4 RID: 2020 RVA: 0x0002013F File Offset: 0x0001E33F
	public LoggerFS(string name, int max_entries = 35)
	{
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x00020141 File Offset: 0x0001E341
	public string GetName()
	{
		return "";
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x00020148 File Offset: 0x0001E348
	public void SetName(string name)
	{
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x0002014A File Offset: 0x0001E34A
	[Conditional("ENABLE_LOGGER")]
	public void Log(string evt)
	{
	}
}
