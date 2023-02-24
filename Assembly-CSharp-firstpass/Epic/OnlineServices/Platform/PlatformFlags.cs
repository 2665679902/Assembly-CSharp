using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020006DA RID: 1754
	[Flags]
	public enum PlatformFlags : ulong
	{
		// Token: 0x0400199D RID: 6557
		None = 0UL,
		// Token: 0x0400199E RID: 6558
		LoadingInEditor = 1UL,
		// Token: 0x0400199F RID: 6559
		DisableOverlay = 2UL,
		// Token: 0x040019A0 RID: 6560
		DisableSocialOverlay = 4UL
	}
}
