using System;

// Token: 0x020003AB RID: 939
public class Urge : Resource
{
	// Token: 0x0600135E RID: 4958 RVA: 0x00066D40 File Offset: 0x00064F40
	public Urge(string id)
		: base(id, null, null)
	{
	}

	// Token: 0x0600135F RID: 4959 RVA: 0x00066D4B File Offset: 0x00064F4B
	public override string ToString()
	{
		return this.Id;
	}
}
