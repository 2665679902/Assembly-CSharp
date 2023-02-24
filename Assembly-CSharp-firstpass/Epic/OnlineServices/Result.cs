using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000537 RID: 1335
	public enum Result
	{
		// Token: 0x04001474 RID: 5236
		Success,
		// Token: 0x04001475 RID: 5237
		NoConnection,
		// Token: 0x04001476 RID: 5238
		InvalidCredentials,
		// Token: 0x04001477 RID: 5239
		InvalidUser,
		// Token: 0x04001478 RID: 5240
		InvalidAuth,
		// Token: 0x04001479 RID: 5241
		AccessDenied,
		// Token: 0x0400147A RID: 5242
		MissingPermissions,
		// Token: 0x0400147B RID: 5243
		TokenNotAccount,
		// Token: 0x0400147C RID: 5244
		TooManyRequests,
		// Token: 0x0400147D RID: 5245
		AlreadyPending,
		// Token: 0x0400147E RID: 5246
		InvalidParameters,
		// Token: 0x0400147F RID: 5247
		InvalidRequest,
		// Token: 0x04001480 RID: 5248
		UnrecognizedResponse,
		// Token: 0x04001481 RID: 5249
		IncompatibleVersion,
		// Token: 0x04001482 RID: 5250
		NotConfigured,
		// Token: 0x04001483 RID: 5251
		AlreadyConfigured,
		// Token: 0x04001484 RID: 5252
		NotImplemented,
		// Token: 0x04001485 RID: 5253
		Canceled,
		// Token: 0x04001486 RID: 5254
		NotFound,
		// Token: 0x04001487 RID: 5255
		OperationWillRetry,
		// Token: 0x04001488 RID: 5256
		NoChange,
		// Token: 0x04001489 RID: 5257
		VersionMismatch,
		// Token: 0x0400148A RID: 5258
		LimitExceeded,
		// Token: 0x0400148B RID: 5259
		Disabled,
		// Token: 0x0400148C RID: 5260
		DuplicateNotAllowed,
		// Token: 0x0400148D RID: 5261
		MissingParametersDEPRECATED,
		// Token: 0x0400148E RID: 5262
		InvalidSandboxId,
		// Token: 0x0400148F RID: 5263
		TimedOut,
		// Token: 0x04001490 RID: 5264
		PartialResult,
		// Token: 0x04001491 RID: 5265
		MissingRole,
		// Token: 0x04001492 RID: 5266
		MissingFeature,
		// Token: 0x04001493 RID: 5267
		InvalidSandbox,
		// Token: 0x04001494 RID: 5268
		InvalidDeployment,
		// Token: 0x04001495 RID: 5269
		InvalidProduct,
		// Token: 0x04001496 RID: 5270
		InvalidProductUserID,
		// Token: 0x04001497 RID: 5271
		ServiceFailure,
		// Token: 0x04001498 RID: 5272
		CacheDirectoryMissing,
		// Token: 0x04001499 RID: 5273
		CacheDirectoryInvalid,
		// Token: 0x0400149A RID: 5274
		InvalidState,
		// Token: 0x0400149B RID: 5275
		RequestInProgress,
		// Token: 0x0400149C RID: 5276
		AuthAccountLocked = 1001,
		// Token: 0x0400149D RID: 5277
		AuthAccountLockedForUpdate,
		// Token: 0x0400149E RID: 5278
		AuthInvalidRefreshToken,
		// Token: 0x0400149F RID: 5279
		AuthInvalidToken,
		// Token: 0x040014A0 RID: 5280
		AuthAuthenticationFailure,
		// Token: 0x040014A1 RID: 5281
		AuthInvalidPlatformToken,
		// Token: 0x040014A2 RID: 5282
		AuthWrongAccount,
		// Token: 0x040014A3 RID: 5283
		AuthWrongClient,
		// Token: 0x040014A4 RID: 5284
		AuthFullAccountRequired,
		// Token: 0x040014A5 RID: 5285
		AuthHeadlessAccountRequired,
		// Token: 0x040014A6 RID: 5286
		AuthPasswordResetRequired,
		// Token: 0x040014A7 RID: 5287
		AuthPasswordCannotBeReused,
		// Token: 0x040014A8 RID: 5288
		AuthExpired,
		// Token: 0x040014A9 RID: 5289
		AuthScopeConsentRequired,
		// Token: 0x040014AA RID: 5290
		AuthApplicationNotFound,
		// Token: 0x040014AB RID: 5291
		AuthScopeNotFound,
		// Token: 0x040014AC RID: 5292
		AuthAccountFeatureRestricted,
		// Token: 0x040014AD RID: 5293
		AuthPinGrantCode = 1020,
		// Token: 0x040014AE RID: 5294
		AuthPinGrantExpired,
		// Token: 0x040014AF RID: 5295
		AuthPinGrantPending,
		// Token: 0x040014B0 RID: 5296
		AuthExternalAuthNotLinked = 1030,
		// Token: 0x040014B1 RID: 5297
		AuthExternalAuthRevoked = 1032,
		// Token: 0x040014B2 RID: 5298
		AuthExternalAuthInvalid,
		// Token: 0x040014B3 RID: 5299
		AuthExternalAuthRestricted,
		// Token: 0x040014B4 RID: 5300
		AuthExternalAuthCannotLogin,
		// Token: 0x040014B5 RID: 5301
		AuthExternalAuthExpired,
		// Token: 0x040014B6 RID: 5302
		AuthExternalAuthIsLastLoginType,
		// Token: 0x040014B7 RID: 5303
		AuthExchangeCodeNotFound = 1040,
		// Token: 0x040014B8 RID: 5304
		AuthOriginatingExchangeCodeSessionExpired,
		// Token: 0x040014B9 RID: 5305
		AuthPersistentAuthAccountNotActive = 1050,
		// Token: 0x040014BA RID: 5306
		AuthMFARequired = 1060,
		// Token: 0x040014BB RID: 5307
		AuthParentalControls = 1070,
		// Token: 0x040014BC RID: 5308
		AuthNoRealId = 1080,
		// Token: 0x040014BD RID: 5309
		FriendsInviteAwaitingAcceptance = 2000,
		// Token: 0x040014BE RID: 5310
		FriendsNoInvitation,
		// Token: 0x040014BF RID: 5311
		FriendsAlreadyFriends = 2003,
		// Token: 0x040014C0 RID: 5312
		FriendsNotFriends,
		// Token: 0x040014C1 RID: 5313
		FriendsTargetUserTooManyInvites,
		// Token: 0x040014C2 RID: 5314
		FriendsLocalUserTooManyInvites,
		// Token: 0x040014C3 RID: 5315
		FriendsTargetUserFriendLimitExceeded,
		// Token: 0x040014C4 RID: 5316
		FriendsLocalUserFriendLimitExceeded,
		// Token: 0x040014C5 RID: 5317
		PresenceDataInvalid = 3000,
		// Token: 0x040014C6 RID: 5318
		PresenceDataLengthInvalid,
		// Token: 0x040014C7 RID: 5319
		PresenceDataKeyInvalid,
		// Token: 0x040014C8 RID: 5320
		PresenceDataKeyLengthInvalid,
		// Token: 0x040014C9 RID: 5321
		PresenceDataValueInvalid,
		// Token: 0x040014CA RID: 5322
		PresenceDataValueLengthInvalid,
		// Token: 0x040014CB RID: 5323
		PresenceRichTextInvalid,
		// Token: 0x040014CC RID: 5324
		PresenceRichTextLengthInvalid,
		// Token: 0x040014CD RID: 5325
		PresenceStatusInvalid,
		// Token: 0x040014CE RID: 5326
		EcomEntitlementStale = 4000,
		// Token: 0x040014CF RID: 5327
		EcomCatalogOfferStale,
		// Token: 0x040014D0 RID: 5328
		EcomCatalogItemStale,
		// Token: 0x040014D1 RID: 5329
		EcomCatalogOfferPriceInvalid,
		// Token: 0x040014D2 RID: 5330
		EcomCheckoutLoadError,
		// Token: 0x040014D3 RID: 5331
		SessionsSessionInProgress = 5000,
		// Token: 0x040014D4 RID: 5332
		SessionsTooManyPlayers,
		// Token: 0x040014D5 RID: 5333
		SessionsNoPermission,
		// Token: 0x040014D6 RID: 5334
		SessionsSessionAlreadyExists,
		// Token: 0x040014D7 RID: 5335
		SessionsInvalidLock,
		// Token: 0x040014D8 RID: 5336
		SessionsInvalidSession,
		// Token: 0x040014D9 RID: 5337
		SessionsSandboxNotAllowed,
		// Token: 0x040014DA RID: 5338
		SessionsInviteFailed,
		// Token: 0x040014DB RID: 5339
		SessionsInviteNotFound,
		// Token: 0x040014DC RID: 5340
		SessionsUpsertNotAllowed,
		// Token: 0x040014DD RID: 5341
		SessionsAggregationFailed,
		// Token: 0x040014DE RID: 5342
		SessionsHostAtCapacity,
		// Token: 0x040014DF RID: 5343
		SessionsSandboxAtCapacity,
		// Token: 0x040014E0 RID: 5344
		SessionsSessionNotAnonymous,
		// Token: 0x040014E1 RID: 5345
		SessionsOutOfSync,
		// Token: 0x040014E2 RID: 5346
		SessionsTooManyInvites,
		// Token: 0x040014E3 RID: 5347
		SessionsPresenceSessionExists,
		// Token: 0x040014E4 RID: 5348
		SessionsDeploymentAtCapacity,
		// Token: 0x040014E5 RID: 5349
		SessionsNotAllowed,
		// Token: 0x040014E6 RID: 5350
		PlayerDataStorageFilenameInvalid = 6000,
		// Token: 0x040014E7 RID: 5351
		PlayerDataStorageFilenameLengthInvalid,
		// Token: 0x040014E8 RID: 5352
		PlayerDataStorageFilenameInvalidChars,
		// Token: 0x040014E9 RID: 5353
		PlayerDataStorageFileSizeTooLarge,
		// Token: 0x040014EA RID: 5354
		PlayerDataStorageFileSizeInvalid,
		// Token: 0x040014EB RID: 5355
		PlayerDataStorageFileHandleInvalid,
		// Token: 0x040014EC RID: 5356
		PlayerDataStorageDataInvalid,
		// Token: 0x040014ED RID: 5357
		PlayerDataStorageDataLengthInvalid,
		// Token: 0x040014EE RID: 5358
		PlayerDataStorageStartIndexInvalid,
		// Token: 0x040014EF RID: 5359
		PlayerDataStorageRequestInProgress,
		// Token: 0x040014F0 RID: 5360
		PlayerDataStorageUserThrottled,
		// Token: 0x040014F1 RID: 5361
		PlayerDataStorageEncryptionKeyNotSet,
		// Token: 0x040014F2 RID: 5362
		PlayerDataStorageUserErrorFromDataCallback,
		// Token: 0x040014F3 RID: 5363
		PlayerDataStorageFileHeaderHasNewerVersion,
		// Token: 0x040014F4 RID: 5364
		PlayerDataStorageFileCorrupted,
		// Token: 0x040014F5 RID: 5365
		ConnectExternalTokenValidationFailed = 7000,
		// Token: 0x040014F6 RID: 5366
		ConnectUserAlreadyExists,
		// Token: 0x040014F7 RID: 5367
		ConnectAuthExpired,
		// Token: 0x040014F8 RID: 5368
		ConnectInvalidToken,
		// Token: 0x040014F9 RID: 5369
		ConnectUnsupportedTokenType,
		// Token: 0x040014FA RID: 5370
		ConnectLinkAccountFailed,
		// Token: 0x040014FB RID: 5371
		ConnectExternalServiceUnavailable,
		// Token: 0x040014FC RID: 5372
		ConnectExternalServiceConfigurationFailure,
		// Token: 0x040014FD RID: 5373
		ConnectLinkAccountFailedMissingNintendoIdAccount,
		// Token: 0x040014FE RID: 5374
		SocialOverlayLoadError = 8000,
		// Token: 0x040014FF RID: 5375
		LobbyNotOwner = 9000,
		// Token: 0x04001500 RID: 5376
		LobbyInvalidLock,
		// Token: 0x04001501 RID: 5377
		LobbyLobbyAlreadyExists,
		// Token: 0x04001502 RID: 5378
		LobbySessionInProgress,
		// Token: 0x04001503 RID: 5379
		LobbyTooManyPlayers,
		// Token: 0x04001504 RID: 5380
		LobbyNoPermission,
		// Token: 0x04001505 RID: 5381
		LobbyInvalidSession,
		// Token: 0x04001506 RID: 5382
		LobbySandboxNotAllowed,
		// Token: 0x04001507 RID: 5383
		LobbyInviteFailed,
		// Token: 0x04001508 RID: 5384
		LobbyInviteNotFound,
		// Token: 0x04001509 RID: 5385
		LobbyUpsertNotAllowed,
		// Token: 0x0400150A RID: 5386
		LobbyAggregationFailed,
		// Token: 0x0400150B RID: 5387
		LobbyHostAtCapacity,
		// Token: 0x0400150C RID: 5388
		LobbySandboxAtCapacity,
		// Token: 0x0400150D RID: 5389
		LobbyTooManyInvites,
		// Token: 0x0400150E RID: 5390
		LobbyDeploymentAtCapacity,
		// Token: 0x0400150F RID: 5391
		LobbyNotAllowed,
		// Token: 0x04001510 RID: 5392
		LobbyMemberUpdateOnly,
		// Token: 0x04001511 RID: 5393
		LobbyPresenceLobbyExists,
		// Token: 0x04001512 RID: 5394
		TitleStorageUserErrorFromDataCallback = 10000,
		// Token: 0x04001513 RID: 5395
		TitleStorageEncryptionKeyNotSet,
		// Token: 0x04001514 RID: 5396
		TitleStorageFileCorrupted,
		// Token: 0x04001515 RID: 5397
		TitleStorageFileHeaderHasNewerVersion,
		// Token: 0x04001516 RID: 5398
		UnexpectedError = 2147483647
	}
}
