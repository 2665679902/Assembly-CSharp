using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000443 RID: 1091
	public class RailUsersGetUserLimitsResult : EventBase
	{
		// Token: 0x04001081 RID: 4225
		public RailID user_id = new RailID();

		// Token: 0x04001082 RID: 4226
		public List<EnumRailUsersLimits> user_limits = new List<EnumRailUsersLimits>();
	}
}
