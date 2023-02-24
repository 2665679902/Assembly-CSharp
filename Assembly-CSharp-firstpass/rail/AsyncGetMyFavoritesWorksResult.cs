using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000415 RID: 1045
	public class AsyncGetMyFavoritesWorksResult : EventBase
	{
		// Token: 0x04000FDD RID: 4061
		public uint total_available_works;

		// Token: 0x04000FDE RID: 4062
		public List<RailSpaceWorkDescriptor> spacework_descriptors = new List<RailSpaceWorkDescriptor>();
	}
}
