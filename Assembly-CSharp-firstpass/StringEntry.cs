using System;

// Token: 0x02000104 RID: 260
public class StringEntry
{
	// Token: 0x060008C6 RID: 2246 RVA: 0x0002335D File Offset: 0x0002155D
	public StringEntry(string str)
	{
		this.String = str;
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x0002336C File Offset: 0x0002156C
	public override string ToString()
	{
		return this.String;
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x00023374 File Offset: 0x00021574
	public static implicit operator string(StringEntry entry)
	{
		return entry.String;
	}

	// Token: 0x0400066C RID: 1644
	public string String;
}
