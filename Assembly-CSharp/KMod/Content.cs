using System;

namespace KMod
{
	// Token: 0x02000D11 RID: 3345
	[Flags]
	public enum Content : byte
	{
		// Token: 0x04004BE4 RID: 19428
		LayerableFiles = 1,
		// Token: 0x04004BE5 RID: 19429
		Strings = 2,
		// Token: 0x04004BE6 RID: 19430
		DLL = 4,
		// Token: 0x04004BE7 RID: 19431
		Translation = 8,
		// Token: 0x04004BE8 RID: 19432
		Animation = 16
	}
}
