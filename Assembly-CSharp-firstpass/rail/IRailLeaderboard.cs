using System;

namespace rail
{
	// Token: 0x02000391 RID: 913
	public interface IRailLeaderboard : IRailComponent
	{
		// Token: 0x06002F03 RID: 12035
		string GetLeaderboardName();

		// Token: 0x06002F04 RID: 12036
		string GetLeaderboardDisplayName();

		// Token: 0x06002F05 RID: 12037
		int GetTotalEntriesCount();

		// Token: 0x06002F06 RID: 12038
		RailResult AsyncGetLeaderboard(string user_data);

		// Token: 0x06002F07 RID: 12039
		RailResult GetLeaderboardParameters(LeaderboardParameters param);

		// Token: 0x06002F08 RID: 12040
		IRailLeaderboardEntries CreateLeaderboardEntries();

		// Token: 0x06002F09 RID: 12041
		RailResult AsyncUploadLeaderboard(UploadLeaderboardParam update_param, string user_data);

		// Token: 0x06002F0A RID: 12042
		RailResult GetLeaderboardSortType(out int sort_type);

		// Token: 0x06002F0B RID: 12043
		RailResult GetLeaderboardDisplayType(out int display_type);

		// Token: 0x06002F0C RID: 12044
		RailResult AsyncAttachSpaceWork(SpaceWorkID spacework_id, string user_data);
	}
}
