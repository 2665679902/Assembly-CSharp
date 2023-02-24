using System;

namespace rail
{
	// Token: 0x02000448 RID: 1096
	public class RailUsersRespondInvitation : EventBase
	{
		// Token: 0x04001089 RID: 4233
		public RailInviteOptions original_invite_option = new RailInviteOptions();

		// Token: 0x0400108A RID: 4234
		public EnumRailUsersInviteResponseType response;

		// Token: 0x0400108B RID: 4235
		public RailID inviter_id = new RailID();
	}
}
