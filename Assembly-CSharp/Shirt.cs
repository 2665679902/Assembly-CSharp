using System;

// Token: 0x02000919 RID: 2329
public class Shirt : Resource
{
	// Token: 0x060043C6 RID: 17350 RVA: 0x0017E6F3 File Offset: 0x0017C8F3
	public Shirt(string id)
		: base(id, null, null)
	{
		this.hash = new HashedString(id);
	}

	// Token: 0x04002D3D RID: 11581
	public HashedString hash;
}
