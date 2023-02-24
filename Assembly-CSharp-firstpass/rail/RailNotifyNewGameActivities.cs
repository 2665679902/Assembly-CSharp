using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000378 RID: 888
	public class RailNotifyNewGameActivities : EventBase
	{
		// Token: 0x04000CF6 RID: 3318
		public List<RailGameActivityInfo> game_activities = new List<RailGameActivityInfo>();
	}
}
