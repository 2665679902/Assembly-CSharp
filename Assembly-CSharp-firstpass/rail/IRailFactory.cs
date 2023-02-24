using System;

namespace rail
{
	// Token: 0x02000326 RID: 806
	public interface IRailFactory
	{
		// Token: 0x06002E0E RID: 11790
		IRailPlayer RailPlayer();

		// Token: 0x06002E0F RID: 11791
		IRailUsersHelper RailUsersHelper();

		// Token: 0x06002E10 RID: 11792
		IRailFriends RailFriends();

		// Token: 0x06002E11 RID: 11793
		IRailFloatingWindow RailFloatingWindow();

		// Token: 0x06002E12 RID: 11794
		IRailBrowserHelper RailBrowserHelper();

		// Token: 0x06002E13 RID: 11795
		IRailInGamePurchase RailInGamePurchase();

		// Token: 0x06002E14 RID: 11796
		IRailInGameCoin RailInGameCoin();

		// Token: 0x06002E15 RID: 11797
		IRailRoomHelper RailRoomHelper();

		// Token: 0x06002E16 RID: 11798
		IRailGameServerHelper RailGameServerHelper();

		// Token: 0x06002E17 RID: 11799
		IRailStorageHelper RailStorageHelper();

		// Token: 0x06002E18 RID: 11800
		IRailUserSpaceHelper RailUserSpaceHelper();

		// Token: 0x06002E19 RID: 11801
		IRailStatisticHelper RailStatisticHelper();

		// Token: 0x06002E1A RID: 11802
		IRailLeaderboardHelper RailLeaderboardHelper();

		// Token: 0x06002E1B RID: 11803
		IRailAchievementHelper RailAchievementHelper();

		// Token: 0x06002E1C RID: 11804
		IRailNetwork RailNetworkHelper();

		// Token: 0x06002E1D RID: 11805
		IRailApps RailApps();

		// Token: 0x06002E1E RID: 11806
		IRailGame RailGame();

		// Token: 0x06002E1F RID: 11807
		IRailUtils RailUtils();

		// Token: 0x06002E20 RID: 11808
		IRailAssetsHelper RailAssetsHelper();

		// Token: 0x06002E21 RID: 11809
		IRailDlcHelper RailDlcHelper();

		// Token: 0x06002E22 RID: 11810
		IRailScreenshotHelper RailScreenshotHelper();

		// Token: 0x06002E23 RID: 11811
		IRailVoiceHelper RailVoiceHelper();

		// Token: 0x06002E24 RID: 11812
		IRailSystemHelper RailSystemHelper();

		// Token: 0x06002E25 RID: 11813
		IRailTextInputHelper RailTextInputHelper();

		// Token: 0x06002E26 RID: 11814
		IRailIMEHelper RailIMETextInputHelper();

		// Token: 0x06002E27 RID: 11815
		IRailHttpSessionHelper RailHttpSessionHelper();

		// Token: 0x06002E28 RID: 11816
		IRailSmallObjectServiceHelper RailSmallObjectServiceHelper();

		// Token: 0x06002E29 RID: 11817
		IRailZoneServerHelper RailZoneServerHelper();

		// Token: 0x06002E2A RID: 11818
		IRailGroupChatHelper RailGroupChatHelper();

		// Token: 0x06002E2B RID: 11819
		IRailInGameStorePurchaseHelper RailInGameStorePurchaseHelper();

		// Token: 0x06002E2C RID: 11820
		IRailInGameActivityHelper RailInGameActivityHelper();

		// Token: 0x06002E2D RID: 11821
		IRailAntiAddictionHelper RailAntiAddictionHelper();

		// Token: 0x06002E2E RID: 11822
		IRailThirdPartyAccountLoginHelper RailThirdPartyAccountLoginHelper();
	}
}
