using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200041C RID: 1052
	public class AsyncSearchSpaceWorksResult : EventBase
	{
		// Token: 0x04000FE9 RID: 4073
		public uint total_available_works;

		// Token: 0x04000FEA RID: 4074
		public List<RailSpaceWorkDescriptor> spacework_descriptors = new List<RailSpaceWorkDescriptor>();
	}
}
