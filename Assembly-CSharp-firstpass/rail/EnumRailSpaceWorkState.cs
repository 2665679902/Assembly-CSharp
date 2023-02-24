using System;

namespace rail
{
	// Token: 0x02000425 RID: 1061
	public enum EnumRailSpaceWorkState
	{
		// Token: 0x04001009 RID: 4105
		kRailSpaceWorkStateNone,
		// Token: 0x0400100A RID: 4106
		kRailSpaceWorkStateDownloaded,
		// Token: 0x0400100B RID: 4107
		kRailSpaceWorkStateNeedsSync,
		// Token: 0x0400100C RID: 4108
		kRailSpaceWorkStateDownloading = 4,
		// Token: 0x0400100D RID: 4109
		kRailSpaceWorkStateUploading = 8
	}
}
