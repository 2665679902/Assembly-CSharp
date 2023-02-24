using System;
using System.Diagnostics;

// Token: 0x020000D5 RID: 213
public struct LoggerFSSSS
{
	// Token: 0x060007F4 RID: 2036 RVA: 0x00020173 File Offset: 0x0001E373
	public LoggerFSSSS(string name, int max_entries = 35)
	{
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x00020175 File Offset: 0x0001E375
	public string GetName()
	{
		return "";
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x0002017C File Offset: 0x0001E37C
	public void SetName(string name)
	{
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x0002017E File Offset: 0x0001E37E
	[Conditional("ENABLE_LOGGER")]
	public void Log(string evt, string param0 = "", string param1 = "", string param2 = "")
	{
	}
}
