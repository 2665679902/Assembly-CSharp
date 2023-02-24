using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200034B RID: 843
	public interface IRailGame
	{
		// Token: 0x06002E5E RID: 11870
		RailGameID GetGameID();

		// Token: 0x06002E5F RID: 11871
		RailResult ReportGameContentDamaged(EnumRailGameContentDamageFlag flag);

		// Token: 0x06002E60 RID: 11872
		RailResult GetGameInstallPath(out string app_path);

		// Token: 0x06002E61 RID: 11873
		RailResult AsyncQuerySubscribeWishPlayState(string user_data);

		// Token: 0x06002E62 RID: 11874
		RailResult GetPlayerSelectedLanguageCode(out string language_code);

		// Token: 0x06002E63 RID: 11875
		RailResult GetGameSupportedLanguageCodes(List<string> language_codes);

		// Token: 0x06002E64 RID: 11876
		RailResult SetGameState(EnumRailGamePlayingState game_state);

		// Token: 0x06002E65 RID: 11877
		RailResult GetGameState(out EnumRailGamePlayingState game_state);

		// Token: 0x06002E66 RID: 11878
		RailResult RegisterGameDefineGamePlayingState(List<RailGameDefineGamePlayingState> game_playing_states);

		// Token: 0x06002E67 RID: 11879
		RailResult SetGameDefineGamePlayingState(uint game_playing_state);

		// Token: 0x06002E68 RID: 11880
		RailResult GetGameDefineGamePlayingState(out uint game_playing_state);

		// Token: 0x06002E69 RID: 11881
		RailResult GetBranchBuildNumber(out string build_number);

		// Token: 0x06002E6A RID: 11882
		RailResult GetCurrentBranchInfo(RailBranchInfo branch_info);

		// Token: 0x06002E6B RID: 11883
		RailResult StartGameTimeCounting(string counting_key);

		// Token: 0x06002E6C RID: 11884
		RailResult EndGameTimeCounting(string counting_key);

		// Token: 0x06002E6D RID: 11885
		RailID GetGamePurchasePlayerRailID();

		// Token: 0x06002E6E RID: 11886
		uint GetGameEarliestPurchaseTime();

		// Token: 0x06002E6F RID: 11887
		uint GetTimeCountSinceGameActivated();

		// Token: 0x06002E70 RID: 11888
		uint GetTimeCountSinceLastMouseMoved();
	}
}
