using System;

namespace rail
{
	// Token: 0x02000445 RID: 1093
	public class RailUsersInviteJoinGameResult : EventBase
	{
		// Token: 0x04001084 RID: 4228
		public EnumRailUsersInviteResponseType response_value;

		// Token: 0x04001085 RID: 4229
		public RailID invitee_id = new RailID();

		// Token: 0x04001086 RID: 4230
		public EnumRailUsersInviteType invite_type;
	}
}
