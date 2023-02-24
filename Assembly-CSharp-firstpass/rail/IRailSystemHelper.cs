using System;

namespace rail
{
	// Token: 0x02000404 RID: 1028
	public interface IRailSystemHelper
	{
		// Token: 0x06002FF7 RID: 12279
		RailResult SetTerminationTimeoutOwnershipExpired(int timeout_seconds);

		// Token: 0x06002FF8 RID: 12280
		RailSystemState GetPlatformSystemState();
	}
}
