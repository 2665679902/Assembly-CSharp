using System;

namespace KMod
{
	// Token: 0x02000D16 RID: 3350
	public enum EventType
	{
		// Token: 0x04004C1E RID: 19486
		LoadError,
		// Token: 0x04004C1F RID: 19487
		NotFound,
		// Token: 0x04004C20 RID: 19488
		InstallInfoInaccessible,
		// Token: 0x04004C21 RID: 19489
		OutOfOrder,
		// Token: 0x04004C22 RID: 19490
		ExpectedActive,
		// Token: 0x04004C23 RID: 19491
		ExpectedInactive,
		// Token: 0x04004C24 RID: 19492
		ActiveDuringCrash,
		// Token: 0x04004C25 RID: 19493
		InstallFailed,
		// Token: 0x04004C26 RID: 19494
		Installed,
		// Token: 0x04004C27 RID: 19495
		Uninstalled,
		// Token: 0x04004C28 RID: 19496
		VersionUpdate,
		// Token: 0x04004C29 RID: 19497
		AvailableContentChanged,
		// Token: 0x04004C2A RID: 19498
		RestartRequested,
		// Token: 0x04004C2B RID: 19499
		BadWorldGen,
		// Token: 0x04004C2C RID: 19500
		Deactivated,
		// Token: 0x04004C2D RID: 19501
		DisabledEarlyAccess
	}
}
