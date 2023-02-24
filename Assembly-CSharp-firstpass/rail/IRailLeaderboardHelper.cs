using System;

namespace rail
{
	// Token: 0x02000393 RID: 915
	public interface IRailLeaderboardHelper
	{
		// Token: 0x06002F13 RID: 12051
		IRailLeaderboard OpenLeaderboard(string leaderboard_name);

		// Token: 0x06002F14 RID: 12052
		IRailLeaderboard AsyncCreateLeaderboard(string leaderboard_name, LeaderboardSortType sort_type, LeaderboardDisplayType display_type, string user_data, out RailResult result);
	}
}
