using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000417 RID: 1047
	public class AsyncModifyFavoritesWorksResult : EventBase
	{
		// Token: 0x04000FE1 RID: 4065
		public List<SpaceWorkID> success_ids = new List<SpaceWorkID>();

		// Token: 0x04000FE2 RID: 4066
		public List<SpaceWorkID> failure_ids = new List<SpaceWorkID>();

		// Token: 0x04000FE3 RID: 4067
		public EnumRailModifyFavoritesSpaceWorkType modify_flag;
	}
}
