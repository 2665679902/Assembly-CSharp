using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000419 RID: 1049
	public class AsyncQuerySpaceWorksResult : EventBase
	{
		// Token: 0x04000FE5 RID: 4069
		public uint total_available_works;

		// Token: 0x04000FE6 RID: 4070
		public List<RailSpaceWorkDescriptor> spacework_descriptors = new List<RailSpaceWorkDescriptor>();
	}
}
