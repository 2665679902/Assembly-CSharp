using System;

// Token: 0x02000A6F RID: 2671
public class ElementUsage
{
	// Token: 0x060051AE RID: 20910 RVA: 0x001D7FF0 File Offset: 0x001D61F0
	public ElementUsage(Tag tag, float amount, bool continuous)
	{
		this.tag = tag;
		this.amount = amount;
		this.continuous = continuous;
	}

	// Token: 0x040036BF RID: 14015
	public Tag tag;

	// Token: 0x040036C0 RID: 14016
	public float amount;

	// Token: 0x040036C1 RID: 14017
	public bool continuous;
}
