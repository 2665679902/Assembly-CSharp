using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200041D RID: 1053
	public class AsyncSubscribeSpaceWorksResult : EventBase
	{
		// Token: 0x04000FEB RID: 4075
		public List<SpaceWorkID> success_ids = new List<SpaceWorkID>();

		// Token: 0x04000FEC RID: 4076
		public List<SpaceWorkID> failure_ids = new List<SpaceWorkID>();

		// Token: 0x04000FED RID: 4077
		public bool subscribe;
	}
}
