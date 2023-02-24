using System;

namespace rail
{
	// Token: 0x02000325 RID: 805
	public enum RAILEventID
	{
		// Token: 0x04000B4B RID: 2891
		kRailEventBegin,
		// Token: 0x04000B4C RID: 2892
		kRailEventFinalize,
		// Token: 0x04000B4D RID: 2893
		kRailEventSystemStateChanged,
		// Token: 0x04000B4E RID: 2894
		kRailPlatformNotifyEventJoinGameByGameServer = 100,
		// Token: 0x04000B4F RID: 2895
		kRailPlatformNotifyEventJoinGameByRoom,
		// Token: 0x04000B50 RID: 2896
		kRailPlatformNotifyEventJoinGameByUser,
		// Token: 0x04000B51 RID: 2897
		kRailEventStats = 2000,
		// Token: 0x04000B52 RID: 2898
		kRailEventStatsPlayerStatsReceived,
		// Token: 0x04000B53 RID: 2899
		kRailEventStatsPlayerStatsStored,
		// Token: 0x04000B54 RID: 2900
		kRailEventStatsNumOfPlayerReceived,
		// Token: 0x04000B55 RID: 2901
		kRailEventStatsGlobalStatsReceived,
		// Token: 0x04000B56 RID: 2902
		kRailEventAchievement = 2100,
		// Token: 0x04000B57 RID: 2903
		kRailEventAchievementPlayerAchievementReceived,
		// Token: 0x04000B58 RID: 2904
		kRailEventAchievementPlayerAchievementStored,
		// Token: 0x04000B59 RID: 2905
		kRailEventAchievementGlobalAchievementReceived,
		// Token: 0x04000B5A RID: 2906
		kRailEventLeaderboard = 2200,
		// Token: 0x04000B5B RID: 2907
		kRailEventLeaderboardReceived,
		// Token: 0x04000B5C RID: 2908
		kRailEventLeaderboardEntryReceived,
		// Token: 0x04000B5D RID: 2909
		kRailEventLeaderboardUploaded,
		// Token: 0x04000B5E RID: 2910
		kRailEventLeaderboardAttachSpaceWork,
		// Token: 0x04000B5F RID: 2911
		kRailEventLeaderboardAsyncCreated,
		// Token: 0x04000B60 RID: 2912
		kRailEventGameServer = 3000,
		// Token: 0x04000B61 RID: 2913
		kRailEventGameServerListResult,
		// Token: 0x04000B62 RID: 2914
		kRailEventGameServerCreated,
		// Token: 0x04000B63 RID: 2915
		kRailEventGameServerSetMetadataResult,
		// Token: 0x04000B64 RID: 2916
		kRailEventGameServerGetMetadataResult,
		// Token: 0x04000B65 RID: 2917
		kRailEventGameServerGetSessionTicket,
		// Token: 0x04000B66 RID: 2918
		kRailEventGameServerAuthSessionTicket,
		// Token: 0x04000B67 RID: 2919
		kRailEventGameServerPlayerListResult,
		// Token: 0x04000B68 RID: 2920
		kRailEventGameServerRegisterToServerListResult,
		// Token: 0x04000B69 RID: 2921
		kRailEventGameServerFavoriteGameServers,
		// Token: 0x04000B6A RID: 2922
		kRailEventGameServerAddFavoriteGameServer,
		// Token: 0x04000B6B RID: 2923
		kRailEventGameServerRemoveFavoriteGameServer,
		// Token: 0x04000B6C RID: 2924
		kRailEventUserSpace = 4000,
		// Token: 0x04000B6D RID: 2925
		kRailEventUserSpaceGetMySubscribedWorksResult,
		// Token: 0x04000B6E RID: 2926
		kRailEventUserSpaceGetMyFavoritesWorksResult,
		// Token: 0x04000B6F RID: 2927
		kRailEventUserSpaceQuerySpaceWorksResult,
		// Token: 0x04000B70 RID: 2928
		kRailEventUserSpaceUpdateMetadataResult,
		// Token: 0x04000B71 RID: 2929
		kRailEventUserSpaceSyncResult,
		// Token: 0x04000B72 RID: 2930
		kRailEventUserSpaceSubscribeResult,
		// Token: 0x04000B73 RID: 2931
		kRailEventUserSpaceModifyFavoritesWorksResult,
		// Token: 0x04000B74 RID: 2932
		kRailEventUserSpaceRemoveSpaceWorkResult,
		// Token: 0x04000B75 RID: 2933
		kRailEventUserSpaceVoteSpaceWorkResult,
		// Token: 0x04000B76 RID: 2934
		kRailEventUserSpaceSearchSpaceWorkResult,
		// Token: 0x04000B77 RID: 2935
		kRailEventUserSpaceQuerySpaceWorksResultV2,
		// Token: 0x04000B78 RID: 2936
		kRailEventUserSpaceDownloadProgress,
		// Token: 0x04000B79 RID: 2937
		kRailEventUserSpaceDownloadResult,
		// Token: 0x04000B7A RID: 2938
		kRailEventUserSpaceRateSpaceWorkResult,
		// Token: 0x04000B7B RID: 2939
		kRailEventUserSpaceQuerySpaceWorksInfoResult,
		// Token: 0x04000B7C RID: 2940
		kRailEventNetChannel = 5000,
		// Token: 0x04000B7D RID: 2941
		kRailEventNetChannelCreateChannelResult,
		// Token: 0x04000B7E RID: 2942
		kRailEventNetChannelInviteJoinChannelRequest,
		// Token: 0x04000B7F RID: 2943
		kRailEventNetChannelJoinChannelResult,
		// Token: 0x04000B80 RID: 2944
		kRailEventNetChannelChannelException,
		// Token: 0x04000B81 RID: 2945
		kRailEventNetChannelChannelNetDelay,
		// Token: 0x04000B82 RID: 2946
		kRailEventNetChannelInviteMemmberResult,
		// Token: 0x04000B83 RID: 2947
		kRailEventNetChannelMemberStateChanged,
		// Token: 0x04000B84 RID: 2948
		kRailEventStorageBegin = 6000,
		// Token: 0x04000B85 RID: 2949
		kRailEventStorageQueryQuotaResult,
		// Token: 0x04000B86 RID: 2950
		kRailEventStorageShareToSpaceWorkResult,
		// Token: 0x04000B87 RID: 2951
		kRailEventStorageAsyncReadFileResult,
		// Token: 0x04000B88 RID: 2952
		kRailEventStorageAsyncWriteFileResult,
		// Token: 0x04000B89 RID: 2953
		kRailEventStorageAsyncListStreamFileResult,
		// Token: 0x04000B8A RID: 2954
		kRailEventStorageAsyncRenameStreamFileResult,
		// Token: 0x04000B8B RID: 2955
		kRailEventStorageAsyncDeleteStreamFileResult,
		// Token: 0x04000B8C RID: 2956
		kRailEventStorageAsyncReadStreamFileResult,
		// Token: 0x04000B8D RID: 2957
		kRailEventStorageAsyncWriteStreamFileResult,
		// Token: 0x04000B8E RID: 2958
		kRailEventAssetsBegin = 7000,
		// Token: 0x04000B8F RID: 2959
		kRailEventAssetsRequestAllAssetsFinished,
		// Token: 0x04000B90 RID: 2960
		kRailEventAssetsCompleteConsumeByExchangeAssetsToFinished,
		// Token: 0x04000B91 RID: 2961
		kRailEventAssetsExchangeAssetsFinished,
		// Token: 0x04000B92 RID: 2962
		kRailEventAssetsExchangeAssetsToFinished,
		// Token: 0x04000B93 RID: 2963
		kRailEventAssetsDirectConsumeFinished,
		// Token: 0x04000B94 RID: 2964
		kRailEventAssetsStartConsumeFinished,
		// Token: 0x04000B95 RID: 2965
		kRailEventAssetsUpdateConsumeFinished,
		// Token: 0x04000B96 RID: 2966
		kRailEventAssetsCompleteConsumeFinished,
		// Token: 0x04000B97 RID: 2967
		kRailEventAssetsSplitFinished,
		// Token: 0x04000B98 RID: 2968
		kRailEventAssetsSplitToFinished,
		// Token: 0x04000B99 RID: 2969
		kRailEventAssetsMergeFinished,
		// Token: 0x04000B9A RID: 2970
		kRailEventAssetsMergeToFinished,
		// Token: 0x04000B9B RID: 2971
		kRailEventAssetsRequestAllProductFinished,
		// Token: 0x04000B9C RID: 2972
		kRailEventAssetsUpdateAssetPropertyFinished,
		// Token: 0x04000B9D RID: 2973
		kRailEventAssetsAssetsChanged,
		// Token: 0x04000B9E RID: 2974
		kRailEventUtilsBegin = 8000,
		// Token: 0x04000B9F RID: 2975
		kRailEventUtilsSignatureResult = 8002,
		// Token: 0x04000BA0 RID: 2976
		kRailEventUtilsGetImageDataResult,
		// Token: 0x04000BA1 RID: 2977
		kRailEventUtilsGameSettingMetadataChanged,
		// Token: 0x04000BA2 RID: 2978
		kRailEventInGamePurchaseBegin = 9000,
		// Token: 0x04000BA3 RID: 2979
		kRailEventInGamePurchaseAllProductsInfoReceived,
		// Token: 0x04000BA4 RID: 2980
		kRailEventInGamePurchaseAllPurchasableProductsInfoReceived,
		// Token: 0x04000BA5 RID: 2981
		kRailEventInGamePurchasePurchaseProductsResult,
		// Token: 0x04000BA6 RID: 2982
		kRailEventInGamePurchaseFinishOrderResult,
		// Token: 0x04000BA7 RID: 2983
		kRailEventInGamePurchasePurchaseProductsToAssetsResult,
		// Token: 0x04000BA8 RID: 2984
		kRailEventInGameStorePurchaseBegin = 9500,
		// Token: 0x04000BA9 RID: 2985
		kRailEventInGameStorePurchasePayWindowDisplayed,
		// Token: 0x04000BAA RID: 2986
		kRailEventInGameStorePurchasePayWindowClosed,
		// Token: 0x04000BAB RID: 2987
		kRailEventInGameStorePurchasePaymentResult,
		// Token: 0x04000BAC RID: 2988
		kRailEventRoom = 10000,
		// Token: 0x04000BAD RID: 2989
		kRailEventRoomGetRoomListResult = 10002,
		// Token: 0x04000BAE RID: 2990
		kRailEventRoomCreated,
		// Token: 0x04000BAF RID: 2991
		kRailEventRoomGetRoomMembersResult,
		// Token: 0x04000BB0 RID: 2992
		kRailEventRoomJoinRoomResult,
		// Token: 0x04000BB1 RID: 2993
		kRailEventRoomKickOffMemberResult,
		// Token: 0x04000BB2 RID: 2994
		kRailEventRoomSetRoomMetadataResult,
		// Token: 0x04000BB3 RID: 2995
		kRailEventRoomGetRoomMetadataResult,
		// Token: 0x04000BB4 RID: 2996
		kRailEventRoomGetMemberMetadataResult,
		// Token: 0x04000BB5 RID: 2997
		kRailEventRoomSetMemberMetadataResult,
		// Token: 0x04000BB6 RID: 2998
		kRailEventRoomLeaveRoomResult,
		// Token: 0x04000BB7 RID: 2999
		kRailEventRoomGetAllDataResult,
		// Token: 0x04000BB8 RID: 3000
		kRailEventRoomGetUserRoomListResult,
		// Token: 0x04000BB9 RID: 3001
		kRailEventRoomClearRoomMetadataResult,
		// Token: 0x04000BBA RID: 3002
		kRailEventRoomOpenRoomResult,
		// Token: 0x04000BBB RID: 3003
		kRailEventRoomSetRoomTagResult,
		// Token: 0x04000BBC RID: 3004
		kRailEventRoomGetRoomTagResult,
		// Token: 0x04000BBD RID: 3005
		kRailEventRoomSetNewRoomOwnerResult,
		// Token: 0x04000BBE RID: 3006
		kRailEventRoomSetRoomTypeResult,
		// Token: 0x04000BBF RID: 3007
		kRailEventRoomSetRoomMaxMemberResult,
		// Token: 0x04000BC0 RID: 3008
		kRailEventRoomNotify = 11000,
		// Token: 0x04000BC1 RID: 3009
		kRailEventRoomNotifyMetadataChanged,
		// Token: 0x04000BC2 RID: 3010
		kRailEventRoomNotifyMemberChanged,
		// Token: 0x04000BC3 RID: 3011
		kRailEventRoomNotifyMemberkicked,
		// Token: 0x04000BC4 RID: 3012
		kRailEventRoomNotifyRoomDestroyed,
		// Token: 0x04000BC5 RID: 3013
		kRailEventRoomNotifyRoomOwnerChanged,
		// Token: 0x04000BC6 RID: 3014
		kRailEventRoomNotifyRoomDataReceived,
		// Token: 0x04000BC7 RID: 3015
		kRailEventRoomNotifyRoomGameServerChanged,
		// Token: 0x04000BC8 RID: 3016
		kRailEventFriend = 12000,
		// Token: 0x04000BC9 RID: 3017
		kRailEventFriendsDialogShow,
		// Token: 0x04000BCA RID: 3018
		kRailEventFriendsSetMetadataResult,
		// Token: 0x04000BCB RID: 3019
		kRailEventFriendsGetMetadataResult,
		// Token: 0x04000BCC RID: 3020
		kRailEventFriendsClearMetadataResult,
		// Token: 0x04000BCD RID: 3021
		kRailEventFriendsGetInviteCommandLine,
		// Token: 0x04000BCE RID: 3022
		kRailEventFriendsReportPlayedWithUserListResult,
		// Token: 0x04000BCF RID: 3023
		kRailEventFriendsFriendsListChanged = 12010,
		// Token: 0x04000BD0 RID: 3024
		kRailEventFriendsGetFriendPlayedGamesResult,
		// Token: 0x04000BD1 RID: 3025
		kRailEventFriendsQueryPlayedWithFriendsListResult,
		// Token: 0x04000BD2 RID: 3026
		kRailEventFriendsQueryPlayedWithFriendsTimeResult,
		// Token: 0x04000BD3 RID: 3027
		kRailEventFriendsQueryPlayedWithFriendsGamesResult,
		// Token: 0x04000BD4 RID: 3028
		kRailEventFriendsAddFriendResult,
		// Token: 0x04000BD5 RID: 3029
		kRailEventFriendsOnlineStateChanged,
		// Token: 0x04000BD6 RID: 3030
		kRailEventFriendsMetadataChanged,
		// Token: 0x04000BD7 RID: 3031
		kRailEventSessionTicket = 13000,
		// Token: 0x04000BD8 RID: 3032
		kRailEventSessionTicketGetSessionTicket,
		// Token: 0x04000BD9 RID: 3033
		kRailEventSessionTicketAuthSessionTicket,
		// Token: 0x04000BDA RID: 3034
		kRailEventPlayerGetGamePurchaseKey,
		// Token: 0x04000BDB RID: 3035
		kRailEventQueryPlayerBannedStatus,
		// Token: 0x04000BDC RID: 3036
		kRailEventPlayerGetAuthenticateURL,
		// Token: 0x04000BDD RID: 3037
		kRailEventPlayerAntiAddictionGameOnlineTimeChanged,
		// Token: 0x04000BDE RID: 3038
		kRailEventPlayerGetEncryptedGameTicketResult,
		// Token: 0x04000BDF RID: 3039
		kRailEventPlayerGetPlayerMetadataResult,
		// Token: 0x04000BE0 RID: 3040
		kRailEventUsersGetUsersInfo = 13501,
		// Token: 0x04000BE1 RID: 3041
		kRailEventUsersNotifyInviter,
		// Token: 0x04000BE2 RID: 3042
		kRailEventUsersRespondInvitation,
		// Token: 0x04000BE3 RID: 3043
		kRailEventUsersInviteJoinGameResult,
		// Token: 0x04000BE4 RID: 3044
		kRailEventUsersInviteUsersResult,
		// Token: 0x04000BE5 RID: 3045
		kRailEventUsersGetInviteDetailResult,
		// Token: 0x04000BE6 RID: 3046
		kRailEventUsersCancelInviteResult,
		// Token: 0x04000BE7 RID: 3047
		kRailEventUsersGetUserLimitsResult,
		// Token: 0x04000BE8 RID: 3048
		kRailEventUsersShowChatWindowWithFriendResult,
		// Token: 0x04000BE9 RID: 3049
		kRailEventUsersShowUserHomepageWindowResult,
		// Token: 0x04000BEA RID: 3050
		kRailEventShowFloatingWindow = 14000,
		// Token: 0x04000BEB RID: 3051
		kRailEventShowFloatingNotifyWindow,
		// Token: 0x04000BEC RID: 3052
		kRailEventBrowser = 15000,
		// Token: 0x04000BED RID: 3053
		kRailEventBrowserCreateResult,
		// Token: 0x04000BEE RID: 3054
		kRailEventBrowserReloadResult,
		// Token: 0x04000BEF RID: 3055
		kRailEventBrowserCloseResult,
		// Token: 0x04000BF0 RID: 3056
		kRailEventBrowserJavascriptEvent,
		// Token: 0x04000BF1 RID: 3057
		kRailEventBrowserTryNavigateNewPageRequest,
		// Token: 0x04000BF2 RID: 3058
		kRailEventBrowserPaint,
		// Token: 0x04000BF3 RID: 3059
		kRailEventBrowserDamageRectPaint,
		// Token: 0x04000BF4 RID: 3060
		kRailEventBrowserNavigeteResult,
		// Token: 0x04000BF5 RID: 3061
		kRailEventBrowserStateChanged,
		// Token: 0x04000BF6 RID: 3062
		kRailEventBrowserTitleChanged,
		// Token: 0x04000BF7 RID: 3063
		kRailEventNetwork = 16000,
		// Token: 0x04000BF8 RID: 3064
		kRailEventNetworkCreateSessionRequest,
		// Token: 0x04000BF9 RID: 3065
		kRailEventNetworkCreateSessionFailed,
		// Token: 0x04000BFA RID: 3066
		kRailEventNetworkCreateRawSessionRequest,
		// Token: 0x04000BFB RID: 3067
		kRailEventNetworkCreateRawSessionFailed,
		// Token: 0x04000BFC RID: 3068
		kRailEventDlcBegin = 17000,
		// Token: 0x04000BFD RID: 3069
		kRailEventDlcInstallStart,
		// Token: 0x04000BFE RID: 3070
		kRailEventDlcInstallStartResult,
		// Token: 0x04000BFF RID: 3071
		kRailEventDlcInstallProgress,
		// Token: 0x04000C00 RID: 3072
		kRailEventDlcInstallFinished,
		// Token: 0x04000C01 RID: 3073
		kRailEventDlcUninstallFinished,
		// Token: 0x04000C02 RID: 3074
		kRailEventDlcCheckAllDlcsStateReadyResult,
		// Token: 0x04000C03 RID: 3075
		kRailEventDlcQueryIsOwnedDlcsResult,
		// Token: 0x04000C04 RID: 3076
		kRailEventDlcOwnershipChanged,
		// Token: 0x04000C05 RID: 3077
		kRailEventDlcRefundChanged,
		// Token: 0x04000C06 RID: 3078
		kRailEventScreenshot = 18000,
		// Token: 0x04000C07 RID: 3079
		kRailEventScreenshotTakeScreenshotFinished,
		// Token: 0x04000C08 RID: 3080
		kRailEventScreenshotTakeScreenshotRequest,
		// Token: 0x04000C09 RID: 3081
		kRailEventScreenshotPublishScreenshotFinished,
		// Token: 0x04000C0A RID: 3082
		kRailEventVoiceChannel = 19000,
		// Token: 0x04000C0B RID: 3083
		kRailEventVoiceChannelCreateResult,
		// Token: 0x04000C0C RID: 3084
		kRailEventVoiceChannelDataCaptured,
		// Token: 0x04000C0D RID: 3085
		kRailEventVoiceChannelJoinedResult,
		// Token: 0x04000C0E RID: 3086
		kRailEventVoiceChannelLeaveResult,
		// Token: 0x04000C0F RID: 3087
		kRailEventVoiceChannelAddUsersResult,
		// Token: 0x04000C10 RID: 3088
		kRailEventVoiceChannelRemoveUsersResult,
		// Token: 0x04000C11 RID: 3089
		kRailEventVoiceChannelInviteEvent,
		// Token: 0x04000C12 RID: 3090
		kRailEventVoiceChannelMemberChangedEvent,
		// Token: 0x04000C13 RID: 3091
		kRailEventVoiceChannelUsersSpeakingStateChangedEvent,
		// Token: 0x04000C14 RID: 3092
		kRailEventVoiceChannelPushToTalkKeyChangedEvent,
		// Token: 0x04000C15 RID: 3093
		kRailEventVoiceChannelSpeakingUsersChangedEvent,
		// Token: 0x04000C16 RID: 3094
		kRailEventAppBegin = 20000,
		// Token: 0x04000C17 RID: 3095
		kRailEventAppQuerySubscribeWishPlayStateResult,
		// Token: 0x04000C18 RID: 3096
		kRailEventTextInputBegin = 21000,
		// Token: 0x04000C19 RID: 3097
		kRailEventTextInputShowTextInputWindowResult,
		// Token: 0x04000C1A RID: 3098
		kRailEventIMEHelperTextInputBegin = 22000,
		// Token: 0x04000C1B RID: 3099
		kRailEventIMEHelperTextInputSelectedResult,
		// Token: 0x04000C1C RID: 3100
		kRailEventIMEHelperTextInputCompositionStateChanged,
		// Token: 0x04000C1D RID: 3101
		kRailEventHttpSessionBegin = 23000,
		// Token: 0x04000C1E RID: 3102
		kRailEventHttpSessionResponseResult,
		// Token: 0x04000C1F RID: 3103
		kRailEventSmallObjectServiceBegin = 24000,
		// Token: 0x04000C20 RID: 3104
		kRailEventSmallObjectServiceQueryObjectStateResult,
		// Token: 0x04000C21 RID: 3105
		kRailEventSmallObjectServiceDownloadResult,
		// Token: 0x04000C22 RID: 3106
		kRailEventZoneServerBegin = 25000,
		// Token: 0x04000C23 RID: 3107
		kRailEventZoneServerSwitchPlayerSelectedZoneResult,
		// Token: 0x04000C24 RID: 3108
		kRailEventGroupChatBegin = 26000,
		// Token: 0x04000C25 RID: 3109
		kRailEventGroupChatQueryGroupsInfoResult,
		// Token: 0x04000C26 RID: 3110
		kRailEventGroupChatOpenGroupChatResult,
		// Token: 0x04000C27 RID: 3111
		kRailEventInGameCoinBegin = 27000,
		// Token: 0x04000C28 RID: 3112
		kRailEventInGameCoinRequestCoinInfoResult,
		// Token: 0x04000C29 RID: 3113
		kRailEventInGameCoinPurchaseCoinsResult,
		// Token: 0x04000C2A RID: 3114
		kRailEventInGameActivityBegin = 28000,
		// Token: 0x04000C2B RID: 3115
		kRailEventInGameActivityQueryGameActivityResult,
		// Token: 0x04000C2C RID: 3116
		kRailEventInGameActivityOpenGameActivityWindowResult,
		// Token: 0x04000C2D RID: 3117
		kRailEventInGameActivityNotifyNewGameActivities,
		// Token: 0x04000C2E RID: 3118
		kRailEventInGameActivityGameActivityPlayerEvent,
		// Token: 0x04000C2F RID: 3119
		kRailEventAntiAddictionBegin = 29000,
		// Token: 0x04000C30 RID: 3120
		kRailEventAntiAddictionQueryGameOnlineTimeResult,
		// Token: 0x04000C31 RID: 3121
		kRailEventAntiAddictionCustomizeAntiAddictionActions,
		// Token: 0x04000C32 RID: 3122
		kRailThirdPartyAccountLoginBegin = 30000,
		// Token: 0x04000C33 RID: 3123
		kRailThirdPartyAccountLoginResult,
		// Token: 0x04000C34 RID: 3124
		kRailThirdPartyAccountLoginNotifyQrCodeInfo,
		// Token: 0x04000C35 RID: 3125
		kCustomEventBegin = 10000000
	}
}
