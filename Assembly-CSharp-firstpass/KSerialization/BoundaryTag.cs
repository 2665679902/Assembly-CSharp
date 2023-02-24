using System;

namespace KSerialization
{
	// Token: 0x020004FF RID: 1279
	public enum BoundaryTag : uint
	{
		// Token: 0x040013D4 RID: 5076
		DirectoryStart = 3235774464U,
		// Token: 0x040013D5 RID: 5077
		DirectoryEnd,
		// Token: 0x040013D6 RID: 5078
		TemplateStart = 3235774467U,
		// Token: 0x040013D7 RID: 5079
		TemplateEnd,
		// Token: 0x040013D8 RID: 5080
		FieldStart,
		// Token: 0x040013D9 RID: 5081
		FieldEnd,
		// Token: 0x040013DA RID: 5082
		PropertyStart,
		// Token: 0x040013DB RID: 5083
		PropertyEnd
	}
}
