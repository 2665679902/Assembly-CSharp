using System;
using System.Diagnostics;

// Token: 0x020000D2 RID: 210
public struct LoggerFSFSF
{
	// Token: 0x060007E8 RID: 2024 RVA: 0x0002014C File Offset: 0x0001E34C
	public LoggerFSFSF(string name, int max_entries = 35)
	{
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x0002014E File Offset: 0x0001E34E
	public string GetName()
	{
		return "";
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x00020155 File Offset: 0x0001E355
	public void SetName(string name)
	{
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x00020157 File Offset: 0x0001E357
	[Conditional("ENABLE_LOGGER")]
	public void Log(string name1, float val1, string name2, float val2)
	{
	}
}
