using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000438 RID: 1080
	public class UserSpaceDownloadProgress : EventBase
	{
		// Token: 0x04001062 RID: 4194
		public List<RailUserSpaceDownloadProgress> progress = new List<RailUserSpaceDownloadProgress>();

		// Token: 0x04001063 RID: 4195
		public uint total_progress;
	}
}
