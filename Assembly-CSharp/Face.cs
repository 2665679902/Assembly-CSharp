using System;

// Token: 0x0200076E RID: 1902
public class Face : Resource
{
	// Token: 0x06003426 RID: 13350 RVA: 0x001187C5 File Offset: 0x001169C5
	public Face(string id, string headFXSymbol = null)
		: base(id, null, null)
	{
		this.hash = new HashedString(id);
		this.headFXHash = headFXSymbol;
	}

	// Token: 0x0400203B RID: 8251
	public HashedString hash;

	// Token: 0x0400203C RID: 8252
	public HashedString headFXHash;

	// Token: 0x0400203D RID: 8253
	private const string SYMBOL_PREFIX = "headfx_";
}
