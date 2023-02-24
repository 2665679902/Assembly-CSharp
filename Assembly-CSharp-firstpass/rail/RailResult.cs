using System;

namespace rail
{
	// Token: 0x020003B8 RID: 952
	public enum RailResult
	{
		// Token: 0x04000D99 RID: 3481
		kSuccess,
		// Token: 0x04000D9A RID: 3482
		kFailure = 1010001,
		// Token: 0x04000D9B RID: 3483
		kErrorInvalidParam,
		// Token: 0x04000D9C RID: 3484
		kErrorImageNotFound,
		// Token: 0x04000D9D RID: 3485
		kErrorBufferTooSmall,
		// Token: 0x04000D9E RID: 3486
		kErrorNetworkError,
		// Token: 0x04000D9F RID: 3487
		kErrorUnimplemented,
		// Token: 0x04000DA0 RID: 3488
		kErrorInvalidCustomKey,
		// Token: 0x04000DA1 RID: 3489
		kErrorClientInOfflineMode,
		// Token: 0x04000DA2 RID: 3490
		kErrorParameterLengthTooLong,
		// Token: 0x04000DA3 RID: 3491
		kErrorWebApiKeyNoAccessOnThisGame,
		// Token: 0x04000DA4 RID: 3492
		kErrorOperationTimeout,
		// Token: 0x04000DA5 RID: 3493
		kErrorServerResponseInvalid,
		// Token: 0x04000DA6 RID: 3494
		kErrorServerInternalError,
		// Token: 0x04000DA7 RID: 3495
		kErrorFileNotFound = 1011000,
		// Token: 0x04000DA8 RID: 3496
		kErrorAccessDenied,
		// Token: 0x04000DA9 RID: 3497
		kErrorOpenFileFailed,
		// Token: 0x04000DAA RID: 3498
		kErrorCreateFileFailed,
		// Token: 0x04000DAB RID: 3499
		kErrorReadFailed,
		// Token: 0x04000DAC RID: 3500
		kErrorWriteFailed,
		// Token: 0x04000DAD RID: 3501
		kErrorFileDestroyed,
		// Token: 0x04000DAE RID: 3502
		kErrorFileDelete,
		// Token: 0x04000DAF RID: 3503
		kErrorFileQueryIndexOutofRange,
		// Token: 0x04000DB0 RID: 3504
		kErrorFileAvaiableQuotaMoreThanTotal,
		// Token: 0x04000DB1 RID: 3505
		kErrorFileGetRemotePathError,
		// Token: 0x04000DB2 RID: 3506
		kErrorFileIllegal,
		// Token: 0x04000DB3 RID: 3507
		kErrorStreamFileWriteParamError,
		// Token: 0x04000DB4 RID: 3508
		kErrorStreamFileReadParamError,
		// Token: 0x04000DB5 RID: 3509
		kErrorStreamCloseErrorIOWritting,
		// Token: 0x04000DB6 RID: 3510
		kErrorStreamCloseErrorIOReading,
		// Token: 0x04000DB7 RID: 3511
		kErrorStreamDeleteFileOpenFileError,
		// Token: 0x04000DB8 RID: 3512
		kErrorStreamRenameFileOpenFileError,
		// Token: 0x04000DB9 RID: 3513
		kErrorStreamReadOnlyError,
		// Token: 0x04000DBA RID: 3514
		kErrorStreamCreateFileRemoveOldFileError,
		// Token: 0x04000DBB RID: 3515
		kErrorStreamCreateFileNameNotAvailable,
		// Token: 0x04000DBC RID: 3516
		kErrorStreamOpenFileErrorCloudStorageDisabledByPlatform,
		// Token: 0x04000DBD RID: 3517
		kErrorStreamOpenFileErrorCloudStorageDisabledByPlayer,
		// Token: 0x04000DBE RID: 3518
		kErrorStoragePathNotFound,
		// Token: 0x04000DBF RID: 3519
		kErrorStorageFileCantOpen,
		// Token: 0x04000DC0 RID: 3520
		kErrorStorageFileRefuseAccess,
		// Token: 0x04000DC1 RID: 3521
		kErrorStorageFileInvalidHandle,
		// Token: 0x04000DC2 RID: 3522
		kErrorStorageFileInUsingByAnotherProcess,
		// Token: 0x04000DC3 RID: 3523
		kErrorStorageFileLockedByAnotherProcess,
		// Token: 0x04000DC4 RID: 3524
		kErrorStorageFileWriteDiskNotEnough,
		// Token: 0x04000DC5 RID: 3525
		kErrorStorageFileCantCreateFileOrPath,
		// Token: 0x04000DC6 RID: 3526
		kErrorRoomCreateFailed = 1012000,
		// Token: 0x04000DC7 RID: 3527
		kErrorKickOffFailed,
		// Token: 0x04000DC8 RID: 3528
		kErrorKickOffPlayerNotInRoom,
		// Token: 0x04000DC9 RID: 3529
		kErrorKickOffNotRoomOwner,
		// Token: 0x04000DCA RID: 3530
		kErrorKickOffPlayingGame,
		// Token: 0x04000DCB RID: 3531
		kErrorRoomServerRequestInvalidData,
		// Token: 0x04000DCC RID: 3532
		kErrorRoomServerConnectTcaplusFail,
		// Token: 0x04000DCD RID: 3533
		kErrorRoomServerConnectTcaplusTimeOut,
		// Token: 0x04000DCE RID: 3534
		kErrorRoomServerWrongDataInTcaplus,
		// Token: 0x04000DCF RID: 3535
		kErrorRoomServerNoDataInTcaplus,
		// Token: 0x04000DD0 RID: 3536
		kErrorRoomServerAllocateRoomIdFail,
		// Token: 0x04000DD1 RID: 3537
		kErrorRoomServerCreateGroupInImCloudFail,
		// Token: 0x04000DD2 RID: 3538
		kErrorRoomServerUserAlreadyInGame,
		// Token: 0x04000DD3 RID: 3539
		kErrorRoomServerQueryResultEmpty,
		// Token: 0x04000DD4 RID: 3540
		kErrorRoomServerRoomFull,
		// Token: 0x04000DD5 RID: 3541
		kErrorRoomServerRoomNotExist,
		// Token: 0x04000DD6 RID: 3542
		kErrorRoomServerUserAlreadyInRoom,
		// Token: 0x04000DD7 RID: 3543
		kErrorRoomServerQueryRailIdServiceFail = 1012018,
		// Token: 0x04000DD8 RID: 3544
		kErrorRoomServerImCloudFail,
		// Token: 0x04000DD9 RID: 3545
		kErrorRoomServerPbSerializeFail,
		// Token: 0x04000DDA RID: 3546
		kErrorRoomServerDirtyWord,
		// Token: 0x04000DDB RID: 3547
		kErrorRoomServerNoPermission,
		// Token: 0x04000DDC RID: 3548
		kErrorRoomServerLeaveUserNotInRoom,
		// Token: 0x04000DDD RID: 3549
		kErrorRoomServerDestroiedRoomNotExist,
		// Token: 0x04000DDE RID: 3550
		kErrorRoomServerUserIsNotRoomMember,
		// Token: 0x04000DDF RID: 3551
		kErrorRoomServerLockFailed,
		// Token: 0x04000DE0 RID: 3552
		kErrorRoomServerRouteMiss,
		// Token: 0x04000DE1 RID: 3553
		kErrorRoomServerRetry,
		// Token: 0x04000DE2 RID: 3554
		kErrorRoomSendDataNotImplemented,
		// Token: 0x04000DE3 RID: 3555
		kErrorRoomInvokeFailed,
		// Token: 0x04000DE4 RID: 3556
		kErrorRoomServerPasswordIncorrect,
		// Token: 0x04000DE5 RID: 3557
		kErrorRoomServerRoomIsNotJoinable,
		// Token: 0x04000DE6 RID: 3558
		kErrorStats = 1013000,
		// Token: 0x04000DE7 RID: 3559
		kErrorStatsDontSetOtherPlayerStat,
		// Token: 0x04000DE8 RID: 3560
		kErrorAchievement = 1014000,
		// Token: 0x04000DE9 RID: 3561
		kErrorAchievementOutofRange,
		// Token: 0x04000DEA RID: 3562
		kErrorAchievementNotMyAchievement,
		// Token: 0x04000DEB RID: 3563
		kErrorLeaderboard = 1015000,
		// Token: 0x04000DEC RID: 3564
		kErrorLeaderboardNotExists,
		// Token: 0x04000DED RID: 3565
		kErrorLeaderboardNotBePrepared,
		// Token: 0x04000DEE RID: 3566
		kErrorLeaderboardCreattionNotSupport,
		// Token: 0x04000DEF RID: 3567
		kErrorAssets = 1016000,
		// Token: 0x04000DF0 RID: 3568
		kErrorAssetsPending,
		// Token: 0x04000DF1 RID: 3569
		kErrorAssetsOK,
		// Token: 0x04000DF2 RID: 3570
		kErrorAssetsExpired,
		// Token: 0x04000DF3 RID: 3571
		kErrorAssetsInvalidParam,
		// Token: 0x04000DF4 RID: 3572
		kErrorAssetsServiceUnavailable,
		// Token: 0x04000DF5 RID: 3573
		kErrorAssetsLimitExceeded,
		// Token: 0x04000DF6 RID: 3574
		kErrorAssetsFailed,
		// Token: 0x04000DF7 RID: 3575
		kErrorAssetsRailIdInvalid,
		// Token: 0x04000DF8 RID: 3576
		kErrorAssetsGameIdInvalid,
		// Token: 0x04000DF9 RID: 3577
		kErrorAssetsRequestInvokeFailed,
		// Token: 0x04000DFA RID: 3578
		kErrorAssetsUpdateConsumeProgressNull,
		// Token: 0x04000DFB RID: 3579
		kErrorAssetsCanNotFindAssetId,
		// Token: 0x04000DFC RID: 3580
		kErrorAssetInvalidRequest,
		// Token: 0x04000DFD RID: 3581
		kErrorAssetDBFail,
		// Token: 0x04000DFE RID: 3582
		kErrorAssetDataTooOld,
		// Token: 0x04000DFF RID: 3583
		kErrorAssetInConsume,
		// Token: 0x04000E00 RID: 3584
		kErrorAssetNotExist,
		// Token: 0x04000E01 RID: 3585
		kErrorAssetExchangNotMatch,
		// Token: 0x04000E02 RID: 3586
		kErrorAssetSystemError,
		// Token: 0x04000E03 RID: 3587
		kErrorAssetBadJasonData,
		// Token: 0x04000E04 RID: 3588
		kErrorAssetStateNotConsuing,
		// Token: 0x04000E05 RID: 3589
		kErrorAssetStateConsuing,
		// Token: 0x04000E06 RID: 3590
		kErrorAssetDifferentProductId,
		// Token: 0x04000E07 RID: 3591
		kErrorAssetConsumeQuantityTooBig,
		// Token: 0x04000E08 RID: 3592
		kErrorAssetMissMatchRailId,
		// Token: 0x04000E09 RID: 3593
		kErrorAssetProductInfoNotReady,
		// Token: 0x04000E0A RID: 3594
		kErrorInGamePurchase = 1017000,
		// Token: 0x04000E0B RID: 3595
		kErrorInGamePurchaseProductInfoExpired,
		// Token: 0x04000E0C RID: 3596
		kErrorInGamePurchaseAcquireSessionTicketFailed,
		// Token: 0x04000E0D RID: 3597
		kErrorInGamePurchaseParseWebContentFaild,
		// Token: 0x04000E0E RID: 3598
		kErrorInGamePurchaseProductIsNotExist,
		// Token: 0x04000E0F RID: 3599
		kErrorInGamePurchaseOrderIDIsNotExist,
		// Token: 0x04000E10 RID: 3600
		kErrorInGamePurchasePreparePaymentRequestTimeout,
		// Token: 0x04000E11 RID: 3601
		kErrorInGamePurchaseCreateOrderFailed,
		// Token: 0x04000E12 RID: 3602
		kErrorInGamePurchaseQueryOrderFailed,
		// Token: 0x04000E13 RID: 3603
		kErrorInGamePurchaseFinishOrderFailed,
		// Token: 0x04000E14 RID: 3604
		kErrorInGamePurchasePaymentFailed,
		// Token: 0x04000E15 RID: 3605
		kErrorInGamePurchasePaymentCancle,
		// Token: 0x04000E16 RID: 3606
		kErrorInGamePurchaseCreatePaymentBrowserFailed,
		// Token: 0x04000E17 RID: 3607
		kErrorInGamePurchaseExceededProductPurchaseLimit,
		// Token: 0x04000E18 RID: 3608
		kErrorInGamePurchaseExceededPurchaseCountLimit,
		// Token: 0x04000E19 RID: 3609
		kErrorInGameStorePurchase = 1017500,
		// Token: 0x04000E1A RID: 3610
		kErrorInGameStorePurchasePaymentSuccess,
		// Token: 0x04000E1B RID: 3611
		kErrorInGameStorePurchasePaymentFailure,
		// Token: 0x04000E1C RID: 3612
		kErrorInGameStorePurchasePaymentCancle,
		// Token: 0x04000E1D RID: 3613
		kErrorPlayer = 1018000,
		// Token: 0x04000E1E RID: 3614
		kErrorPlayerUserFolderCreateFailed,
		// Token: 0x04000E1F RID: 3615
		kErrorPlayerUserFolderCanntFind,
		// Token: 0x04000E20 RID: 3616
		kErrorPlayerUserNotFriend,
		// Token: 0x04000E21 RID: 3617
		kErrorPlayerGameNotSupportPurchaseKey,
		// Token: 0x04000E22 RID: 3618
		kErrorPlayerGetAuthenticateURLFailed,
		// Token: 0x04000E23 RID: 3619
		kErrorPlayerGetAuthenticateURLServerError,
		// Token: 0x04000E24 RID: 3620
		kErrorPlayerGetAuthenticateURLInvalidURL,
		// Token: 0x04000E25 RID: 3621
		kErrorFriends = 1019000,
		// Token: 0x04000E26 RID: 3622
		kErrorFriendsKeyFrontUseRail,
		// Token: 0x04000E27 RID: 3623
		kErrorFriendsMetadataSizeInvalid,
		// Token: 0x04000E28 RID: 3624
		kErrorFriendsMetadataKeyLenInvalid,
		// Token: 0x04000E29 RID: 3625
		kErrorFriendsMetadataValueLenInvalid,
		// Token: 0x04000E2A RID: 3626
		kErrorFriendsMetadataKeyInvalid,
		// Token: 0x04000E2B RID: 3627
		kErrorFriendsGetMetadataFailed,
		// Token: 0x04000E2C RID: 3628
		kErrorFriendsSetPlayTogetherSizeZero,
		// Token: 0x04000E2D RID: 3629
		kErrorFriendsSetPlayTogetherContentSizeInvalid,
		// Token: 0x04000E2E RID: 3630
		kErrorFriendsInviteResponseTypeInvalid,
		// Token: 0x04000E2F RID: 3631
		kErrorFriendsListUpdateFailed,
		// Token: 0x04000E30 RID: 3632
		kErrorFriendsAddFriendInvalidID,
		// Token: 0x04000E31 RID: 3633
		kErrorFriendsAddFriendNetworkError,
		// Token: 0x04000E32 RID: 3634
		kErrorFriendsServerBusy,
		// Token: 0x04000E33 RID: 3635
		kErrorFriendsUpdateFriendsListTooFrequent,
		// Token: 0x04000E34 RID: 3636
		kErrorSessionTicket = 1020000,
		// Token: 0x04000E35 RID: 3637
		kErrorSessionTicketGetTicketFailed,
		// Token: 0x04000E36 RID: 3638
		kErrorSessionTicketAuthFailed,
		// Token: 0x04000E37 RID: 3639
		kErrorSessionTicketAuthTicketAbandoned,
		// Token: 0x04000E38 RID: 3640
		kErrorSessionTicketAuthTicketExpire,
		// Token: 0x04000E39 RID: 3641
		kErrorSessionTicketAuthTicketInvalid,
		// Token: 0x04000E3A RID: 3642
		kErrorSessionTicketInvalidParameter = 1020500,
		// Token: 0x04000E3B RID: 3643
		kErrorSessionTicketInvalidTicket,
		// Token: 0x04000E3C RID: 3644
		kErrorSessionTicketIncorrectTicketOwner,
		// Token: 0x04000E3D RID: 3645
		kErrorSessionTicketHasBeenCanceledByTicketOwner,
		// Token: 0x04000E3E RID: 3646
		kErrorSessionTicketExpired,
		// Token: 0x04000E3F RID: 3647
		kErrorFloatWindow = 1021000,
		// Token: 0x04000E40 RID: 3648
		kErrorFloatWindowInitFailed,
		// Token: 0x04000E41 RID: 3649
		kErrorFloatWindowShowStoreInvalidPara,
		// Token: 0x04000E42 RID: 3650
		kErrorFloatWindowShowStoreCreateBrowserFailed,
		// Token: 0x04000E43 RID: 3651
		kErrorUserSpace = 1022000,
		// Token: 0x04000E44 RID: 3652
		kErrorUserSpaceGetWorkDetailFailed,
		// Token: 0x04000E45 RID: 3653
		kErrorUserSpaceDownloadError,
		// Token: 0x04000E46 RID: 3654
		kErrorUserSpaceDescFileInvalid,
		// Token: 0x04000E47 RID: 3655
		kErrorUserSpaceReplaceOldFileFailed,
		// Token: 0x04000E48 RID: 3656
		kErrorUserSpaceUserCancelSync,
		// Token: 0x04000E49 RID: 3657
		kErrorUserSpaceIDorUserdataPathInvalid,
		// Token: 0x04000E4A RID: 3658
		kErrorUserSpaceNoData,
		// Token: 0x04000E4B RID: 3659
		kErrorUserSpaceSpaceWorkIDInvalid,
		// Token: 0x04000E4C RID: 3660
		kErrorUserSpaceNoSyncingNow,
		// Token: 0x04000E4D RID: 3661
		kErrorUserSpaceSpaceWorkAlreadySyncing,
		// Token: 0x04000E4E RID: 3662
		kErrorUserSpaceSubscribePartialSuccess,
		// Token: 0x04000E4F RID: 3663
		kErrorUserSpaceNoVersionField,
		// Token: 0x04000E50 RID: 3664
		kErrorUserSpaceUpdateFailedWhenUploading,
		// Token: 0x04000E51 RID: 3665
		kErrorUserSpaceGetTicketFailed,
		// Token: 0x04000E52 RID: 3666
		kErrorUserSpaceVersionOccupied,
		// Token: 0x04000E53 RID: 3667
		kErrorUserSpaceCallCreateMethodFailed,
		// Token: 0x04000E54 RID: 3668
		kErrorUserSpaceCreateMethodRspFailed,
		// Token: 0x04000E55 RID: 3669
		kErrorUserSpaceNoEditablePermission = 1022020,
		// Token: 0x04000E56 RID: 3670
		kErrorUserSpaceCallEditMethodFailed,
		// Token: 0x04000E57 RID: 3671
		kErrorUserSpaceEditMethodRspFailed,
		// Token: 0x04000E58 RID: 3672
		kErrorUserSpaceMetadataHasInvalidKey,
		// Token: 0x04000E59 RID: 3673
		kErrorUserSpaceModifyFavoritePartialSuccess,
		// Token: 0x04000E5A RID: 3674
		kErrorUserSpaceFilePathTooLong,
		// Token: 0x04000E5B RID: 3675
		kErrorUserSpaceInvalidContentFolder,
		// Token: 0x04000E5C RID: 3676
		kErrorUserSpaceInvalidFilePath,
		// Token: 0x04000E5D RID: 3677
		kErrorUserSpaceUploadFileNotMeetLimit,
		// Token: 0x04000E5E RID: 3678
		kErrorUserSpaceCannotReadFileToBeUploaded,
		// Token: 0x04000E5F RID: 3679
		kErrorUserSpaceUploadSpaceWorkHasNoVersionField,
		// Token: 0x04000E60 RID: 3680
		kErrorUserSpaceDownloadCurrentDescFileFailed,
		// Token: 0x04000E61 RID: 3681
		kErrorUserSpaceCannotGetSpaceWorkDownloadUrl,
		// Token: 0x04000E62 RID: 3682
		kErrorUserSpaceCannotGetSpaceWorkUploadUrl,
		// Token: 0x04000E63 RID: 3683
		kErrorUserSpaceCannotReadFileWhenUploading,
		// Token: 0x04000E64 RID: 3684
		kErrorUserSpaceUploadFileTooLarge,
		// Token: 0x04000E65 RID: 3685
		kErrorUserSpaceUploadRequestTimeout,
		// Token: 0x04000E66 RID: 3686
		kErrorUserSpaceUploadRequestFailed,
		// Token: 0x04000E67 RID: 3687
		kErrorUserSpaceUploadInternalError,
		// Token: 0x04000E68 RID: 3688
		kErrorUserSpaceUploadCloudServerError,
		// Token: 0x04000E69 RID: 3689
		kErrorUserSpaceUploadCloudServerRspInvalid,
		// Token: 0x04000E6A RID: 3690
		kErrorUserSpaceUploadCopyNoExistCloudFile,
		// Token: 0x04000E6B RID: 3691
		kErrorUserSpaceShareLevelNotSatisfied,
		// Token: 0x04000E6C RID: 3692
		kErrorUserSpaceHasntBeenApproved,
		// Token: 0x04000E6D RID: 3693
		kErrorUserSpaceCanNotLoadCacheFromLocalFile,
		// Token: 0x04000E6E RID: 3694
		kErrorUserSpaceV2NoDownloadURL,
		// Token: 0x04000E6F RID: 3695
		kErrorUserSpaceV2DownloadDescriptionFilePerformFailed,
		// Token: 0x04000E70 RID: 3696
		kErrorUserSpaceV2DescriptionFileParseResponseNotJson,
		// Token: 0x04000E71 RID: 3697
		kErrorUserSpaceV2DescriptionFileParseJsonError,
		// Token: 0x04000E72 RID: 3698
		kErrorUserSpaceV2CheckDownloadContentFailed,
		// Token: 0x04000E73 RID: 3699
		kErrorUserSpaceV2OfflineMode,
		// Token: 0x04000E74 RID: 3700
		kErrorUserSpaceV2CannotAccessUserDataPath,
		// Token: 0x04000E75 RID: 3701
		kErrorUserSpaceV2CleanInvalidFilesFailed,
		// Token: 0x04000E76 RID: 3702
		kErrorUserSpaceV2GeneratePreviewPathFailed,
		// Token: 0x04000E77 RID: 3703
		kErrorUserSpaceV2LocalFilesAreLatestVersion,
		// Token: 0x04000E78 RID: 3704
		kErrorUserSpaceV2DownloadFilesFailed,
		// Token: 0x04000E79 RID: 3705
		kErrorUserSpaceV2HelperInvokeFailed,
		// Token: 0x04000E7A RID: 3706
		kErrorUserSpaceV2HelperInvokeRetFailed,
		// Token: 0x04000E7B RID: 3707
		kErrorUserSpaceV2HelperNoCache,
		// Token: 0x04000E7C RID: 3708
		kErrorUserSpaceV2HelperQueryDetailInfoFailed,
		// Token: 0x04000E7D RID: 3709
		kErrorUserSpaceV2HelperNoFilesNeedDownload,
		// Token: 0x04000E7E RID: 3710
		kErrorUserSpaceV2WriteDescriptionFileFailed,
		// Token: 0x04000E7F RID: 3711
		kErrorUserSpaceV2FilePathTooLong,
		// Token: 0x04000E80 RID: 3712
		kErrorUserSpaceV2ResumeDownloadFromBreakpointFailed,
		// Token: 0x04000E81 RID: 3713
		kErrorUserSpaceV2CannotFindTaskIdInResumeDownloadList,
		// Token: 0x04000E82 RID: 3714
		kErrorUserSpaceV2CancelAllDownloadByUser,
		// Token: 0x04000E83 RID: 3715
		kErrorUserSpaceV2AlreadyInDownloading,
		// Token: 0x04000E84 RID: 3716
		kErrorUserSpaceV2TenioDLError,
		// Token: 0x04000E85 RID: 3717
		kErrorUserSpaceV2EnumEnd,
		// Token: 0x04000E86 RID: 3718
		kErrorUserSpaceV2QueriedSpaceWorksAmountExceedLimit,
		// Token: 0x04000E87 RID: 3719
		kErrorGameServer = 1023000,
		// Token: 0x04000E88 RID: 3720
		kErrorGameServerCreateFailed,
		// Token: 0x04000E89 RID: 3721
		kErrorGameServerDisconnectedServerlist,
		// Token: 0x04000E8A RID: 3722
		kErrorGameServerConnectServerlistFailure,
		// Token: 0x04000E8B RID: 3723
		kErrorGameServerSetMetadataFailed,
		// Token: 0x04000E8C RID: 3724
		kErrorGameServerGetMetadataFailed,
		// Token: 0x04000E8D RID: 3725
		kErrorGameServerGetServerListFailed,
		// Token: 0x04000E8E RID: 3726
		kErrorGameServerGetPlayerListFailed,
		// Token: 0x04000E8F RID: 3727
		kErrorGameServerPlayerNotJoinGameserver,
		// Token: 0x04000E90 RID: 3728
		kErrorGameServerNeedGetFovariteFirst,
		// Token: 0x04000E91 RID: 3729
		kErrorGameServerAddFovariteFailed,
		// Token: 0x04000E92 RID: 3730
		kErrorGameServerRemoveFovariteFailed,
		// Token: 0x04000E93 RID: 3731
		kErrorNetwork = 1024000,
		// Token: 0x04000E94 RID: 3732
		kErrorNetworkInitializeFailed,
		// Token: 0x04000E95 RID: 3733
		kErrorNetworkSessionIsNotExist,
		// Token: 0x04000E96 RID: 3734
		kErrorNetworkNoAvailableDataToRead,
		// Token: 0x04000E97 RID: 3735
		kErrorNetworkUnReachable,
		// Token: 0x04000E98 RID: 3736
		kErrorNetworkRemotePeerOffline,
		// Token: 0x04000E99 RID: 3737
		kErrorNetworkServerUnavailabe,
		// Token: 0x04000E9A RID: 3738
		kErrorNetworkConnectionDenied,
		// Token: 0x04000E9B RID: 3739
		kErrorNetworkConnectionClosed,
		// Token: 0x04000E9C RID: 3740
		kErrorNetworkConnectionReset,
		// Token: 0x04000E9D RID: 3741
		kErrorNetworkSendDataSizeTooLarge,
		// Token: 0x04000E9E RID: 3742
		kErrorNetworkSessioNotRegistered,
		// Token: 0x04000E9F RID: 3743
		kErrorNetworkSessionTimeout,
		// Token: 0x04000EA0 RID: 3744
		kErrorDlc = 1025000,
		// Token: 0x04000EA1 RID: 3745
		kErrorDlcInstallFailed,
		// Token: 0x04000EA2 RID: 3746
		kErrorDlcUninstallFailed,
		// Token: 0x04000EA3 RID: 3747
		kErrorDlcGetDlcListTimeout,
		// Token: 0x04000EA4 RID: 3748
		kErrorDlcRequestInvokeFailed,
		// Token: 0x04000EA5 RID: 3749
		kErrorDlcRequestToofrequently,
		// Token: 0x04000EA6 RID: 3750
		kErrorUtils = 1026000,
		// Token: 0x04000EA7 RID: 3751
		kErrorUtilsImagePathNull,
		// Token: 0x04000EA8 RID: 3752
		kErrorUtilsImagePathInvalid,
		// Token: 0x04000EA9 RID: 3753
		kErrorUtilsImageDownloadFail,
		// Token: 0x04000EAA RID: 3754
		kErrorUtilsImageOpenLocalFail,
		// Token: 0x04000EAB RID: 3755
		kErrorUtilsImageBufferAllocateFail,
		// Token: 0x04000EAC RID: 3756
		kErrorUtilsImageReadLocalFail,
		// Token: 0x04000EAD RID: 3757
		kErrorUtilsImageParseFail,
		// Token: 0x04000EAE RID: 3758
		kErrorUtilsImageScaleFail,
		// Token: 0x04000EAF RID: 3759
		kErrorUtilsImageUnknownFormat,
		// Token: 0x04000EB0 RID: 3760
		kErrorUtilsImageNotNeedResize,
		// Token: 0x04000EB1 RID: 3761
		kErrorUtilsImageResizeParameterInvalid,
		// Token: 0x04000EB2 RID: 3762
		kErrorUtilsImageSaveFileFail,
		// Token: 0x04000EB3 RID: 3763
		kErrorUtilsDirtyWordsFilterTooManyInput,
		// Token: 0x04000EB4 RID: 3764
		kErrorUtilsDirtyWordsHasInvalidString,
		// Token: 0x04000EB5 RID: 3765
		kErrorUtilsDirtyWordsNotReady,
		// Token: 0x04000EB6 RID: 3766
		kErrorUtilsDirtyWordsDllUnloaded,
		// Token: 0x04000EB7 RID: 3767
		kErrorUtilsCrashAllocateFailed,
		// Token: 0x04000EB8 RID: 3768
		kErrorUtilsCrashCallbackSwitchOff,
		// Token: 0x04000EB9 RID: 3769
		kErrorUsers = 1027000,
		// Token: 0x04000EBA RID: 3770
		kErrorUsersInvalidInviteCommandLine,
		// Token: 0x04000EBB RID: 3771
		kErrorUsersSetCommandLineFailed,
		// Token: 0x04000EBC RID: 3772
		kErrorUsersInviteListEmpty,
		// Token: 0x04000EBD RID: 3773
		kErrorUsersGenerateRequestFail,
		// Token: 0x04000EBE RID: 3774
		kErrorUsersUnknownInviteType,
		// Token: 0x04000EBF RID: 3775
		kErrorUsersInvalidInviteUsersSize,
		// Token: 0x04000EC0 RID: 3776
		kErrorScreenshot = 1028000,
		// Token: 0x04000EC1 RID: 3777
		kErrorScreenshotWorkNotExist,
		// Token: 0x04000EC2 RID: 3778
		kErrorScreenshotCantConvertPng,
		// Token: 0x04000EC3 RID: 3779
		kErrorScreenshotCopyFileFailed,
		// Token: 0x04000EC4 RID: 3780
		kErrorScreenshotCantCreateThumbnail,
		// Token: 0x04000EC5 RID: 3781
		kErrorVoiceCapture = 1029000,
		// Token: 0x04000EC6 RID: 3782
		kErrorVoiceCaptureInitializeFailed,
		// Token: 0x04000EC7 RID: 3783
		kErrorVoiceCaptureDeviceLost,
		// Token: 0x04000EC8 RID: 3784
		kErrorVoiceCaptureIsRecording,
		// Token: 0x04000EC9 RID: 3785
		kErrorVoiceCaptureNotRecording,
		// Token: 0x04000ECA RID: 3786
		kErrorVoiceCaptureNoData,
		// Token: 0x04000ECB RID: 3787
		kErrorVoiceCaptureMoreData,
		// Token: 0x04000ECC RID: 3788
		kErrorVoiceCaptureDataCorrupted,
		// Token: 0x04000ECD RID: 3789
		kErrorVoiceCapturekUnsupportedCodec,
		// Token: 0x04000ECE RID: 3790
		kErrorVoiceChannelHelperNotReady,
		// Token: 0x04000ECF RID: 3791
		kErrorVoiceChannelIsBusy,
		// Token: 0x04000ED0 RID: 3792
		kErrorVoiceChannelNotJoinedChannel,
		// Token: 0x04000ED1 RID: 3793
		kErrorVoiceChannelLostConnection,
		// Token: 0x04000ED2 RID: 3794
		kErrorVoiceChannelAlreadyJoinedAnotherChannel,
		// Token: 0x04000ED3 RID: 3795
		kErrorVoiceChannelPartialSuccess,
		// Token: 0x04000ED4 RID: 3796
		kErrorVoiceChannelNotTheChannelOwner,
		// Token: 0x04000ED5 RID: 3797
		kErrorTextInputTextInputSendMessageToPlatformFailed = 1040000,
		// Token: 0x04000ED6 RID: 3798
		kErrorTextInputTextInputSendMessageToOverlayFailed,
		// Token: 0x04000ED7 RID: 3799
		kErrorTextInputTextInputUserCanceled,
		// Token: 0x04000ED8 RID: 3800
		kErrorTextInputTextInputEnableChineseFailed,
		// Token: 0x04000ED9 RID: 3801
		kErrorTextInputTextInputShowFailed,
		// Token: 0x04000EDA RID: 3802
		kErrorTextInputEnableIMEHelperTextInputWindowFailed,
		// Token: 0x04000EDB RID: 3803
		kErrorApps = 1041000,
		// Token: 0x04000EDC RID: 3804
		kErrorAppsCountingKeyExists,
		// Token: 0x04000EDD RID: 3805
		kErrorAppsCountingKeyDoesNotExist,
		// Token: 0x04000EDE RID: 3806
		kErrorHttpSession = 1042000,
		// Token: 0x04000EDF RID: 3807
		kErrorHttpSessionPostBodyContentConflictWithPostParameter,
		// Token: 0x04000EE0 RID: 3808
		kErrorHttpSessionRequestMehotdNotPost,
		// Token: 0x04000EE1 RID: 3809
		kErrorSmallObjectService = 1043000,
		// Token: 0x04000EE2 RID: 3810
		kErrorSmallObjectServiceObjectNotExist,
		// Token: 0x04000EE3 RID: 3811
		kErrorSmallObjectServiceFailedToRequestDownload,
		// Token: 0x04000EE4 RID: 3812
		kErrorSmallObjectServiceDownloadFailed,
		// Token: 0x04000EE5 RID: 3813
		kErrorSmallObjectServiceFailedToWriteDisk,
		// Token: 0x04000EE6 RID: 3814
		kErrorSmallObjectServiceFailedToUpdateObject,
		// Token: 0x04000EE7 RID: 3815
		kErrorSmallObjectServicePartialDownloadSuccess,
		// Token: 0x04000EE8 RID: 3816
		kErrorSmallObjectServiceObjectNetworkIssue,
		// Token: 0x04000EE9 RID: 3817
		kErrorSmallObjectServiceObjectServerError,
		// Token: 0x04000EEA RID: 3818
		kErrorSmallObjectServiceInvalidBranch,
		// Token: 0x04000EEB RID: 3819
		kErrorZoneServer = 1044000,
		// Token: 0x04000EEC RID: 3820
		kErrorZoneServerValueDataIsNotExist,
		// Token: 0x04000EED RID: 3821
		kErrorInGameCoin = 1045000,
		// Token: 0x04000EEE RID: 3822
		kErrorInGameCoinCreatePaymentBrowserFailed,
		// Token: 0x04000EEF RID: 3823
		kErrorInGameCoinOperationTimeout,
		// Token: 0x04000EF0 RID: 3824
		kErrorInGameCoinPaymentFailed,
		// Token: 0x04000EF1 RID: 3825
		kErrorInGameCoinPaymentCanceled,
		// Token: 0x04000EF2 RID: 3826
		kErrorInGameCoinConfigurationNotAvailable,
		// Token: 0x04000EF3 RID: 3827
		kErrorInGameActivity = 1046000,
		// Token: 0x04000EF4 RID: 3828
		kErrorInGameActivityNoConfigurationInfo,
		// Token: 0x04000EF5 RID: 3829
		kErrorInGameActivityInvalidConfigurationInfo,
		// Token: 0x04000EF6 RID: 3830
		kErrorInGameActivityNoDefaultActivityWindow,
		// Token: 0x04000EF7 RID: 3831
		kErrorInGameActivityNoSuchActivityID,
		// Token: 0x04000EF8 RID: 3832
		kErrorInGameActivityOpenGameActivityWindowFailed,
		// Token: 0x04000EF9 RID: 3833
		kErrorInGameActivityOpenGameActivityWindowTimeout,
		// Token: 0x04000EFA RID: 3834
		kErrorThirdPartyAccountLogin = 1047000,
		// Token: 0x04000EFB RID: 3835
		kErrorThirdPartyAccountLoginAutoLoginNotSupport,
		// Token: 0x04000EFC RID: 3836
		kErrorThirdPartyAccountLoginAccountTypeNotSupportAutoLogin,
		// Token: 0x04000EFD RID: 3837
		kRailErrorServerBegin = 2000000,
		// Token: 0x04000EFE RID: 3838
		kErrorPaymentSystem = 2080001,
		// Token: 0x04000EFF RID: 3839
		kErrorPaymentParameterIlleage = 2080008,
		// Token: 0x04000F00 RID: 3840
		kErrorPaymentOrderIlleage = 2080011,
		// Token: 0x04000F01 RID: 3841
		kErrorAssetsInvalidParameter = 2230001,
		// Token: 0x04000F02 RID: 3842
		kErrorAssetsSystemError = 2230007,
		// Token: 0x04000F03 RID: 3843
		kErrorDirtyWordsFilterNoPermission = 2290028,
		// Token: 0x04000F04 RID: 3844
		kErrorDirtyWordsFilterCheckFailed,
		// Token: 0x04000F05 RID: 3845
		kErrorDirtyWordsFilterSystemBusy,
		// Token: 0x04000F06 RID: 3846
		kRailErrorInnerServerBegin = 2500000,
		// Token: 0x04000F07 RID: 3847
		kErrorGameGrayCheckSnowError,
		// Token: 0x04000F08 RID: 3848
		kErrorGameGrayParameterIlleage,
		// Token: 0x04000F09 RID: 3849
		kErrorGameGraySystemError,
		// Token: 0x04000F0A RID: 3850
		kErrorGameGrayQQToWegameidError,
		// Token: 0x04000F0B RID: 3851
		kRailErrorInnerServerEnd = 2699999,
		// Token: 0x04000F0C RID: 3852
		kRailErrorServerEnd = 2999999
	}
}
