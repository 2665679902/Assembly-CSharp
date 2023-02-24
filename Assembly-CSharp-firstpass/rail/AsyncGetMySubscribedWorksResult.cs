using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000416 RID: 1046
	public class AsyncGetMySubscribedWorksResult : EventBase
	{
		// Token: 0x04000FDF RID: 4063
		public uint total_available_works;

		// Token: 0x04000FE0 RID: 4064
		public List<RailSpaceWorkDescriptor> spacework_descriptors = new List<RailSpaceWorkDescriptor>();
	}
}
