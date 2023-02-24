using System;

namespace rail
{
	// Token: 0x020002DD RID: 733
	public interface IRailApps
	{
		// Token: 0x06002D8D RID: 11661
		bool IsGameInstalled(RailGameID game_id);

		// Token: 0x06002D8E RID: 11662
		RailResult AsyncQuerySubscribeWishPlayState(RailGameID game_id, string user_data);
	}
}
