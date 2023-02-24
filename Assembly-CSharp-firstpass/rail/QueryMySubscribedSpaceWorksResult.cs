using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200042B RID: 1067
	public class QueryMySubscribedSpaceWorksResult
	{
		// Token: 0x04001026 RID: 4134
		public uint total_available_works;

		// Token: 0x04001027 RID: 4135
		public EnumRailSpaceWorkType spacework_type;

		// Token: 0x04001028 RID: 4136
		public List<RailSpaceWorkDescriptor> spacework_descriptors = new List<RailSpaceWorkDescriptor>();
	}
}
