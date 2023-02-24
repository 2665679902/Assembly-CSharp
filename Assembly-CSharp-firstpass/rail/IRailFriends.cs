using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000331 RID: 817
	public interface IRailFriends
	{
		// Token: 0x06002E3A RID: 11834
		RailResult AsyncGetPersonalInfo(List<RailID> rail_ids, string user_data);

		// Token: 0x06002E3B RID: 11835
		RailResult AsyncGetFriendMetadata(RailID rail_id, List<string> keys, string user_data);

		// Token: 0x06002E3C RID: 11836
		RailResult AsyncSetMyMetadata(List<RailKeyValue> key_values, string user_data);

		// Token: 0x06002E3D RID: 11837
		RailResult AsyncClearAllMyMetadata(string user_data);

		// Token: 0x06002E3E RID: 11838
		RailResult AsyncSetInviteCommandLine(string command_line, string user_data);

		// Token: 0x06002E3F RID: 11839
		RailResult AsyncGetInviteCommandLine(RailID rail_id, string user_data);

		// Token: 0x06002E40 RID: 11840
		RailResult AsyncReportPlayedWithUserList(List<RailUserPlayedWith> player_list, string user_data);

		// Token: 0x06002E41 RID: 11841
		RailResult GetFriendsList(List<RailFriendInfo> friends_list);

		// Token: 0x06002E42 RID: 11842
		RailResult AsyncQueryFriendPlayedGamesInfo(RailID rail_id, string user_data);

		// Token: 0x06002E43 RID: 11843
		RailResult AsyncQueryPlayedWithFriendsList(string user_data);

		// Token: 0x06002E44 RID: 11844
		RailResult AsyncQueryPlayedWithFriendsTime(List<RailID> rail_ids, string user_data);

		// Token: 0x06002E45 RID: 11845
		RailResult AsyncQueryPlayedWithFriendsGames(List<RailID> rail_ids, string user_data);

		// Token: 0x06002E46 RID: 11846
		RailResult AsyncAddFriend(RailFriendsAddFriendRequest request, string user_data);

		// Token: 0x06002E47 RID: 11847
		RailResult AsyncUpdateFriendsData(string user_data);
	}
}
