using System;

namespace rail
{
	// Token: 0x02000442 RID: 1090
	public class RailUsersGetInviteDetailResult : EventBase
	{
		// Token: 0x0400107E RID: 4222
		public string command_line;

		// Token: 0x0400107F RID: 4223
		public EnumRailUsersInviteType invite_type;

		// Token: 0x04001080 RID: 4224
		public RailID inviter_id = new RailID();
	}
}
