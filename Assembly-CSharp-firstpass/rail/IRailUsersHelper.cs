using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200043A RID: 1082
	public interface IRailUsersHelper
	{
		// Token: 0x06003070 RID: 12400
		RailResult AsyncGetUsersInfo(List<RailID> rail_ids, string user_data);

		// Token: 0x06003071 RID: 12401
		RailResult AsyncInviteUsers(string command_line, List<RailID> users, RailInviteOptions options, string user_data);

		// Token: 0x06003072 RID: 12402
		RailResult AsyncGetInviteDetail(RailID inviter, EnumRailUsersInviteType invite_type, string user_data);

		// Token: 0x06003073 RID: 12403
		RailResult AsyncCancelInvite(EnumRailUsersInviteType invite_type, string user_data);

		// Token: 0x06003074 RID: 12404
		RailResult AsyncCancelAllInvites(string user_data);

		// Token: 0x06003075 RID: 12405
		RailResult AsyncGetUserLimits(RailID user_id, string user_data);

		// Token: 0x06003076 RID: 12406
		RailResult AsyncShowChatWindowWithFriend(RailID rail_id, string user_data);

		// Token: 0x06003077 RID: 12407
		RailResult AsyncShowUserHomepageWindow(RailID rail_id, string user_data);
	}
}
