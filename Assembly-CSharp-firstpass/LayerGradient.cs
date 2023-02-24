using System;
using System.Collections.Generic;

// Token: 0x02000129 RID: 297
public class LayerGradient : Gradient<List<string>>
{
	// Token: 0x06000A3E RID: 2622 RVA: 0x0002733E File Offset: 0x0002553E
	public LayerGradient()
		: base(null, 0f)
	{
	}

	// Token: 0x06000A3F RID: 2623 RVA: 0x0002734C File Offset: 0x0002554C
	public LayerGradient(List<string> content, float bandSize)
		: base(content, bandSize)
	{
	}
}
