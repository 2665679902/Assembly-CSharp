using System;

// Token: 0x02000025 RID: 37
public enum Action
{
	// Token: 0x040000D3 RID: 211
	Invalid,
	// Token: 0x040000D4 RID: 212
	Escape,
	// Token: 0x040000D5 RID: 213
	Help,
	// Token: 0x040000D6 RID: 214
	MouseLeft,
	// Token: 0x040000D7 RID: 215
	ShiftMouseLeft,
	// Token: 0x040000D8 RID: 216
	MouseRight,
	// Token: 0x040000D9 RID: 217
	MouseMiddle,
	// Token: 0x040000DA RID: 218
	ZoomIn,
	// Token: 0x040000DB RID: 219
	ZoomOut,
	// Token: 0x040000DC RID: 220
	SpeedUp,
	// Token: 0x040000DD RID: 221
	SlowDown,
	// Token: 0x040000DE RID: 222
	TogglePause,
	// Token: 0x040000DF RID: 223
	CycleSpeed,
	// Token: 0x040000E0 RID: 224
	AlternateView,
	// Token: 0x040000E1 RID: 225
	DragStraight,
	// Token: 0x040000E2 RID: 226
	SetUserNav1,
	// Token: 0x040000E3 RID: 227
	SetUserNav2,
	// Token: 0x040000E4 RID: 228
	SetUserNav3,
	// Token: 0x040000E5 RID: 229
	SetUserNav4,
	// Token: 0x040000E6 RID: 230
	SetUserNav5,
	// Token: 0x040000E7 RID: 231
	SetUserNav6,
	// Token: 0x040000E8 RID: 232
	SetUserNav7,
	// Token: 0x040000E9 RID: 233
	SetUserNav8,
	// Token: 0x040000EA RID: 234
	SetUserNav9,
	// Token: 0x040000EB RID: 235
	SetUserNav10,
	// Token: 0x040000EC RID: 236
	GotoUserNav1,
	// Token: 0x040000ED RID: 237
	GotoUserNav2,
	// Token: 0x040000EE RID: 238
	GotoUserNav3,
	// Token: 0x040000EF RID: 239
	GotoUserNav4,
	// Token: 0x040000F0 RID: 240
	GotoUserNav5,
	// Token: 0x040000F1 RID: 241
	GotoUserNav6,
	// Token: 0x040000F2 RID: 242
	GotoUserNav7,
	// Token: 0x040000F3 RID: 243
	GotoUserNav8,
	// Token: 0x040000F4 RID: 244
	GotoUserNav9,
	// Token: 0x040000F5 RID: 245
	GotoUserNav10,
	// Token: 0x040000F6 RID: 246
	BUILD_MENU_START_INTERCEPT,
	// Token: 0x040000F7 RID: 247
	Plan1,
	// Token: 0x040000F8 RID: 248
	Plan2,
	// Token: 0x040000F9 RID: 249
	Plan3,
	// Token: 0x040000FA RID: 250
	Plan4,
	// Token: 0x040000FB RID: 251
	Plan5,
	// Token: 0x040000FC RID: 252
	Plan6,
	// Token: 0x040000FD RID: 253
	Plan7,
	// Token: 0x040000FE RID: 254
	Plan8,
	// Token: 0x040000FF RID: 255
	Plan9,
	// Token: 0x04000100 RID: 256
	Plan10,
	// Token: 0x04000101 RID: 257
	Plan11,
	// Token: 0x04000102 RID: 258
	Plan12,
	// Token: 0x04000103 RID: 259
	Plan13,
	// Token: 0x04000104 RID: 260
	Plan14,
	// Token: 0x04000105 RID: 261
	Plan15,
	// Token: 0x04000106 RID: 262
	CopyBuilding,
	// Token: 0x04000107 RID: 263
	BuildCategoryLadders,
	// Token: 0x04000108 RID: 264
	BuildCategoryTiles,
	// Token: 0x04000109 RID: 265
	BuildCategoryDoors,
	// Token: 0x0400010A RID: 266
	BuildCategoryStorage,
	// Token: 0x0400010B RID: 267
	BuildCategoryGenerators,
	// Token: 0x0400010C RID: 268
	BuildCategoryWires,
	// Token: 0x0400010D RID: 269
	BuildCategoryPowerControl,
	// Token: 0x0400010E RID: 270
	BuildCategoryPlumbingStructures,
	// Token: 0x0400010F RID: 271
	BuildCategoryPipes,
	// Token: 0x04000110 RID: 272
	BuildCategoryVentilationStructures,
	// Token: 0x04000111 RID: 273
	BuildCategoryTubes,
	// Token: 0x04000112 RID: 274
	BuildCategoryTravelTubes,
	// Token: 0x04000113 RID: 275
	BuildCategoryConveyance,
	// Token: 0x04000114 RID: 276
	BuildCategoryLogicWiring,
	// Token: 0x04000115 RID: 277
	BuildCategoryLogicGates,
	// Token: 0x04000116 RID: 278
	BuildCategoryLogicSwitches,
	// Token: 0x04000117 RID: 279
	BuildCategoryLogicConduits,
	// Token: 0x04000118 RID: 280
	BuildCategoryCooking,
	// Token: 0x04000119 RID: 281
	BuildCategoryFarming,
	// Token: 0x0400011A RID: 282
	BuildCategoryRanching,
	// Token: 0x0400011B RID: 283
	BuildCategoryResearch,
	// Token: 0x0400011C RID: 284
	BuildCategoryHygiene,
	// Token: 0x0400011D RID: 285
	BuildCategoryMedical,
	// Token: 0x0400011E RID: 286
	BuildCategoryRecreation,
	// Token: 0x0400011F RID: 287
	BuildCategoryFurniture,
	// Token: 0x04000120 RID: 288
	BuildCategoryDecor,
	// Token: 0x04000121 RID: 289
	BuildCategoryOxygen,
	// Token: 0x04000122 RID: 290
	BuildCategoryUtilities,
	// Token: 0x04000123 RID: 291
	BuildCategoryRefining,
	// Token: 0x04000124 RID: 292
	BuildCategoryEquipment,
	// Token: 0x04000125 RID: 293
	BuildCategoryRocketry,
	// Token: 0x04000126 RID: 294
	BuildMenuKeyA,
	// Token: 0x04000127 RID: 295
	BuildMenuKeyB,
	// Token: 0x04000128 RID: 296
	BuildMenuKeyC,
	// Token: 0x04000129 RID: 297
	BuildMenuKeyD,
	// Token: 0x0400012A RID: 298
	BuildMenuKeyE,
	// Token: 0x0400012B RID: 299
	BuildMenuKeyF,
	// Token: 0x0400012C RID: 300
	BuildMenuKeyG,
	// Token: 0x0400012D RID: 301
	BuildMenuKeyH,
	// Token: 0x0400012E RID: 302
	BuildMenuKeyI,
	// Token: 0x0400012F RID: 303
	BuildMenuKeyJ,
	// Token: 0x04000130 RID: 304
	BuildMenuKeyK,
	// Token: 0x04000131 RID: 305
	BuildMenuKeyL,
	// Token: 0x04000132 RID: 306
	BuildMenuKeyM,
	// Token: 0x04000133 RID: 307
	BuildMenuKeyN,
	// Token: 0x04000134 RID: 308
	BuildMenuKeyO,
	// Token: 0x04000135 RID: 309
	BuildMenuKeyP,
	// Token: 0x04000136 RID: 310
	BuildMenuKeyQ,
	// Token: 0x04000137 RID: 311
	BuildMenuKeyR,
	// Token: 0x04000138 RID: 312
	BuildMenuKeyS,
	// Token: 0x04000139 RID: 313
	BuildMenuKeyT,
	// Token: 0x0400013A RID: 314
	BuildMenuKeyU,
	// Token: 0x0400013B RID: 315
	BuildMenuKeyV,
	// Token: 0x0400013C RID: 316
	BuildMenuKeyW,
	// Token: 0x0400013D RID: 317
	BuildMenuKeyX,
	// Token: 0x0400013E RID: 318
	BuildMenuKeyY,
	// Token: 0x0400013F RID: 319
	BuildMenuKeyZ,
	// Token: 0x04000140 RID: 320
	ManagePriorities,
	// Token: 0x04000141 RID: 321
	ManageConsumables,
	// Token: 0x04000142 RID: 322
	ManageVitals,
	// Token: 0x04000143 RID: 323
	ManageResources,
	// Token: 0x04000144 RID: 324
	ManageResearch,
	// Token: 0x04000145 RID: 325
	ManageSchedule,
	// Token: 0x04000146 RID: 326
	ManageReport,
	// Token: 0x04000147 RID: 327
	ManageDatabase,
	// Token: 0x04000148 RID: 328
	ManageSkills,
	// Token: 0x04000149 RID: 329
	ManageStarmap,
	// Token: 0x0400014A RID: 330
	ManageDiagnostics,
	// Token: 0x0400014B RID: 331
	Overlay1,
	// Token: 0x0400014C RID: 332
	Overlay2,
	// Token: 0x0400014D RID: 333
	Overlay3,
	// Token: 0x0400014E RID: 334
	Overlay4,
	// Token: 0x0400014F RID: 335
	Overlay5,
	// Token: 0x04000150 RID: 336
	Overlay6,
	// Token: 0x04000151 RID: 337
	Overlay7,
	// Token: 0x04000152 RID: 338
	Overlay8,
	// Token: 0x04000153 RID: 339
	Overlay9,
	// Token: 0x04000154 RID: 340
	Overlay10,
	// Token: 0x04000155 RID: 341
	Overlay11,
	// Token: 0x04000156 RID: 342
	Overlay12,
	// Token: 0x04000157 RID: 343
	Overlay13,
	// Token: 0x04000158 RID: 344
	Overlay14,
	// Token: 0x04000159 RID: 345
	Overlay15,
	// Token: 0x0400015A RID: 346
	PanUp,
	// Token: 0x0400015B RID: 347
	PanDown,
	// Token: 0x0400015C RID: 348
	PanLeft,
	// Token: 0x0400015D RID: 349
	PanRight,
	// Token: 0x0400015E RID: 350
	CameraHome,
	// Token: 0x0400015F RID: 351
	BuildingUtility1,
	// Token: 0x04000160 RID: 352
	BuildingUtility2,
	// Token: 0x04000161 RID: 353
	BuildingUtility3,
	// Token: 0x04000162 RID: 354
	BuildingDeconstruct,
	// Token: 0x04000163 RID: 355
	BuildingCancel,
	// Token: 0x04000164 RID: 356
	Dig,
	// Token: 0x04000165 RID: 357
	Attack,
	// Token: 0x04000166 RID: 358
	Capture,
	// Token: 0x04000167 RID: 359
	Harvest,
	// Token: 0x04000168 RID: 360
	EmptyPipe,
	// Token: 0x04000169 RID: 361
	AccessCleanUpCollection,
	// Token: 0x0400016A RID: 362
	Mop,
	// Token: 0x0400016B RID: 363
	Clear,
	// Token: 0x0400016C RID: 364
	Disinfect,
	// Token: 0x0400016D RID: 365
	AccessPrioritizeCollection,
	// Token: 0x0400016E RID: 366
	Prioritize,
	// Token: 0x0400016F RID: 367
	Deprioritize,
	// Token: 0x04000170 RID: 368
	AccessRegionCollection,
	// Token: 0x04000171 RID: 369
	SelectRegion,
	// Token: 0x04000172 RID: 370
	EraseRegion,
	// Token: 0x04000173 RID: 371
	CreateRoomRegion,
	// Token: 0x04000174 RID: 372
	CreateToiletRegion,
	// Token: 0x04000175 RID: 373
	CreateMedicalRegion,
	// Token: 0x04000176 RID: 374
	CreateMessHallRegion,
	// Token: 0x04000177 RID: 375
	CreateDisposalRegion,
	// Token: 0x04000178 RID: 376
	CreateRecreationRegion,
	// Token: 0x04000179 RID: 377
	CreateExosuitRegion,
	// Token: 0x0400017A RID: 378
	ToggleEnabled,
	// Token: 0x0400017B RID: 379
	ToggleOpen,
	// Token: 0x0400017C RID: 380
	ToggleScreenshotMode,
	// Token: 0x0400017D RID: 381
	SreenShot1x,
	// Token: 0x0400017E RID: 382
	SreenShot2x,
	// Token: 0x0400017F RID: 383
	SreenShot8x,
	// Token: 0x04000180 RID: 384
	SreenShot32x,
	// Token: 0x04000181 RID: 385
	DebugToggle,
	// Token: 0x04000182 RID: 386
	DebugSpawnMinion,
	// Token: 0x04000183 RID: 387
	DebugSpawnStressTest,
	// Token: 0x04000184 RID: 388
	DebugSuperTestMode,
	// Token: 0x04000185 RID: 389
	DebugUltraTestMode,
	// Token: 0x04000186 RID: 390
	DebugSlowTestMode,
	// Token: 0x04000187 RID: 391
	DebugInstantBuildMode,
	// Token: 0x04000188 RID: 392
	DebugToggleFastWorkers,
	// Token: 0x04000189 RID: 393
	DebugExplosion,
	// Token: 0x0400018A RID: 394
	DebugDiscoverAllElements,
	// Token: 0x0400018B RID: 395
	DebugTriggerException,
	// Token: 0x0400018C RID: 396
	DebugTriggerError,
	// Token: 0x0400018D RID: 397
	DebugTogglePersonalPriorityComparison,
	// Token: 0x0400018E RID: 398
	DebugCheerEmote,
	// Token: 0x0400018F RID: 399
	DebugDig,
	// Token: 0x04000190 RID: 400
	DebugToggleUI,
	// Token: 0x04000191 RID: 401
	DebugCollectGarbage,
	// Token: 0x04000192 RID: 402
	DebugInvincible,
	// Token: 0x04000193 RID: 403
	DebugRefreshNavCell,
	// Token: 0x04000194 RID: 404
	DebugToggleClusterFX,
	// Token: 0x04000195 RID: 405
	DebugApplyHighAudioReverb,
	// Token: 0x04000196 RID: 406
	DebugApplyLowAudioReverb,
	// Token: 0x04000197 RID: 407
	DebugForceLightEverywhere,
	// Token: 0x04000198 RID: 408
	DebugPlace,
	// Token: 0x04000199 RID: 409
	DebugVisualTest,
	// Token: 0x0400019A RID: 410
	DebugGameplayTest,
	// Token: 0x0400019B RID: 411
	DebugElementTest,
	// Token: 0x0400019C RID: 412
	DebugRiverTest,
	// Token: 0x0400019D RID: 413
	DebugTileTest,
	// Token: 0x0400019E RID: 414
	DebugGotoTarget,
	// Token: 0x0400019F RID: 415
	DebugTeleport,
	// Token: 0x040001A0 RID: 416
	DebugSelectMaterial,
	// Token: 0x040001A1 RID: 417
	DebugToggleMusic,
	// Token: 0x040001A2 RID: 418
	DebugToggleSelectInEditor,
	// Token: 0x040001A3 RID: 419
	DebugPathFinding,
	// Token: 0x040001A4 RID: 420
	DebugSuperSpeed,
	// Token: 0x040001A5 RID: 421
	DebugGameStep,
	// Token: 0x040001A6 RID: 422
	DebugSimStep,
	// Token: 0x040001A7 RID: 423
	DebugNotification,
	// Token: 0x040001A8 RID: 424
	DebugNotificationMessage,
	// Token: 0x040001A9 RID: 425
	DebugReloadLevel,
	// Token: 0x040001AA RID: 426
	DebugReloadMods,
	// Token: 0x040001AB RID: 427
	ToggleProfiler,
	// Token: 0x040001AC RID: 428
	ToggleChromeProfiler,
	// Token: 0x040001AD RID: 429
	RotateBuilding,
	// Token: 0x040001AE RID: 430
	DebugReportBug,
	// Token: 0x040001AF RID: 431
	DebugFocus,
	// Token: 0x040001B0 RID: 432
	DebugCellInfo,
	// Token: 0x040001B1 RID: 433
	DebugDumpGCRoots,
	// Token: 0x040001B2 RID: 434
	DebugDumpGarbageReferences,
	// Token: 0x040001B3 RID: 435
	DebugDumpEventData,
	// Token: 0x040001B4 RID: 436
	DebugDumpSceneParitionerLeakData,
	// Token: 0x040001B5 RID: 437
	DebugCrashSim,
	// Token: 0x040001B6 RID: 438
	DebugNextCall,
	// Token: 0x040001B7 RID: 439
	DebugLockCursor,
	// Token: 0x040001B8 RID: 440
	DialogSubmit,
	// Token: 0x040001B9 RID: 441
	SandboxBrush,
	// Token: 0x040001BA RID: 442
	SandboxSprinkle,
	// Token: 0x040001BB RID: 443
	SandboxFlood,
	// Token: 0x040001BC RID: 444
	SandboxMarquee,
	// Token: 0x040001BD RID: 445
	SandboxSample,
	// Token: 0x040001BE RID: 446
	SandboxHeatGun,
	// Token: 0x040001BF RID: 447
	SandboxClearFloor,
	// Token: 0x040001C0 RID: 448
	SandboxDestroy,
	// Token: 0x040001C1 RID: 449
	SandboxSpawnEntity,
	// Token: 0x040001C2 RID: 450
	ToggleSandboxTools,
	// Token: 0x040001C3 RID: 451
	SandboxReveal,
	// Token: 0x040001C4 RID: 452
	SandboxRadsTool,
	// Token: 0x040001C5 RID: 453
	SandboxCritterTool,
	// Token: 0x040001C6 RID: 454
	SandboxCopyElement,
	// Token: 0x040001C7 RID: 455
	SandboxStressTool,
	// Token: 0x040001C8 RID: 456
	CinemaCamEnable,
	// Token: 0x040001C9 RID: 457
	CinemaPanLeft,
	// Token: 0x040001CA RID: 458
	CinemaPanRight,
	// Token: 0x040001CB RID: 459
	CinemaPanUp,
	// Token: 0x040001CC RID: 460
	CinemaPanDown,
	// Token: 0x040001CD RID: 461
	CinemaZoomIn,
	// Token: 0x040001CE RID: 462
	CinemaZoomOut,
	// Token: 0x040001CF RID: 463
	CinemaPanSpeedMinus,
	// Token: 0x040001D0 RID: 464
	CinemaPanSpeedPlus,
	// Token: 0x040001D1 RID: 465
	CinemaZoomSpeedMinus,
	// Token: 0x040001D2 RID: 466
	CinemaZoomSpeedPlus,
	// Token: 0x040001D3 RID: 467
	CinemaToggleLock,
	// Token: 0x040001D4 RID: 468
	CinemaToggleEasing,
	// Token: 0x040001D5 RID: 469
	CinemaUnpauseOnMove,
	// Token: 0x040001D6 RID: 470
	SwitchActiveWorld1,
	// Token: 0x040001D7 RID: 471
	SwitchActiveWorld2,
	// Token: 0x040001D8 RID: 472
	SwitchActiveWorld3,
	// Token: 0x040001D9 RID: 473
	SwitchActiveWorld4,
	// Token: 0x040001DA RID: 474
	SwitchActiveWorld5,
	// Token: 0x040001DB RID: 475
	SwitchActiveWorld6,
	// Token: 0x040001DC RID: 476
	SwitchActiveWorld7,
	// Token: 0x040001DD RID: 477
	SwitchActiveWorld8,
	// Token: 0x040001DE RID: 478
	SwitchActiveWorld9,
	// Token: 0x040001DF RID: 479
	SwitchActiveWorld10,
	// Token: 0x040001E0 RID: 480
	DebugSpawnMinionAtmoSuit,
	// Token: 0x040001E1 RID: 481
	BuildMenuUp,
	// Token: 0x040001E2 RID: 482
	BuildMenuDown,
	// Token: 0x040001E3 RID: 483
	BuildMenuLeft,
	// Token: 0x040001E4 RID: 484
	BuildMenuRight,
	// Token: 0x040001E5 RID: 485
	AnalogCamera,
	// Token: 0x040001E6 RID: 486
	AnalogCursor,
	// Token: 0x040001E7 RID: 487
	Disconnect,
	// Token: 0x040001E8 RID: 488
	NumActions
}
