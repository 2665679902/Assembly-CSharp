using System;

namespace rail
{
	// Token: 0x02000392 RID: 914
	public interface IRailLeaderboardEntries : IRailComponent
	{
		// Token: 0x06002F0D RID: 12045
		RailID GetRailID();

		// Token: 0x06002F0E RID: 12046
		string GetLeaderboardName();

		// Token: 0x06002F0F RID: 12047
		RailResult AsyncRequestLeaderboardEntries(RailID player, RequestLeaderboardEntryParam param, string user_data);

		// Token: 0x06002F10 RID: 12048
		RequestLeaderboardEntryParam GetEntriesParam();

		// Token: 0x06002F11 RID: 12049
		int GetEntriesCount();

		// Token: 0x06002F12 RID: 12050
		RailResult GetLeaderboardEntry(int index, LeaderboardEntry leaderboard_entry);
	}
}
