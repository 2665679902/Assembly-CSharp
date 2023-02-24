using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200042F RID: 1071
	public class RailSpaceWorkFilter
	{
		// Token: 0x0400103D RID: 4157
		public List<EnumRailWorkFileClass> classes = new List<EnumRailWorkFileClass>();

		// Token: 0x0400103E RID: 4158
		public List<EnumRailSpaceWorkType> type = new List<EnumRailSpaceWorkType>();

		// Token: 0x0400103F RID: 4159
		public List<RailID> collector_list = new List<RailID>();

		// Token: 0x04001040 RID: 4160
		public List<RailID> subscriber_list = new List<RailID>();

		// Token: 0x04001041 RID: 4161
		public List<RailID> creator_list = new List<RailID>();
	}
}
