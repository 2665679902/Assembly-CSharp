using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000439 RID: 1081
	public class UserSpaceDownloadResult : EventBase
	{
		// Token: 0x04001064 RID: 4196
		public uint total_results;

		// Token: 0x04001065 RID: 4197
		public List<RailUserSpaceDownloadResult> results = new List<RailUserSpaceDownloadResult>();
	}
}
