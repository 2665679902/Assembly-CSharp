using System;

namespace rail
{
	// Token: 0x020003ED RID: 1005
	public interface IRailStatisticHelper
	{
		// Token: 0x06002FBD RID: 12221
		IRailPlayerStats CreatePlayerStats(RailID player);

		// Token: 0x06002FBE RID: 12222
		IRailGlobalStats GetGlobalStats();

		// Token: 0x06002FBF RID: 12223
		RailResult AsyncGetNumberOfPlayer(string user_data);
	}
}
