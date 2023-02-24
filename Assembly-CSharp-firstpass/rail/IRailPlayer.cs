using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020003A9 RID: 937
	public interface IRailPlayer
	{
		// Token: 0x06002F38 RID: 12088
		bool AlreadyLoggedIn();

		// Token: 0x06002F39 RID: 12089
		RailID GetRailID();

		// Token: 0x06002F3A RID: 12090
		RailResult GetPlayerDataPath(out string path);

		// Token: 0x06002F3B RID: 12091
		RailResult AsyncAcquireSessionTicket(string user_data);

		// Token: 0x06002F3C RID: 12092
		RailResult AsyncStartSessionWithPlayer(RailSessionTicket player_ticket, RailID player_rail_id, string user_data);

		// Token: 0x06002F3D RID: 12093
		void TerminateSessionOfPlayer(RailID player_rail_id);

		// Token: 0x06002F3E RID: 12094
		void AbandonSessionTicket(RailSessionTicket session_ticket);

		// Token: 0x06002F3F RID: 12095
		RailResult GetPlayerName(out string name);

		// Token: 0x06002F40 RID: 12096
		EnumRailPlayerOwnershipType GetPlayerOwnershipType();

		// Token: 0x06002F41 RID: 12097
		RailResult AsyncGetGamePurchaseKey(string user_data);

		// Token: 0x06002F42 RID: 12098
		bool IsGameRevenueLimited();

		// Token: 0x06002F43 RID: 12099
		float GetRateOfGameRevenue();

		// Token: 0x06002F44 RID: 12100
		RailResult AsyncQueryPlayerBannedStatus(string user_data);

		// Token: 0x06002F45 RID: 12101
		RailResult AsyncGetAuthenticateURL(RailGetAuthenticateURLOptions options, string user_data);

		// Token: 0x06002F46 RID: 12102
		RailResult AsyncGetPlayerMetadata(List<string> keys, string user_data);

		// Token: 0x06002F47 RID: 12103
		RailResult AsyncGetEncryptedGameTicket(string set_metadata, string user_data);

		// Token: 0x06002F48 RID: 12104
		RailPlayerAccountType GetPlayerAccountType();
	}
}
