using System;

// Token: 0x02000782 RID: 1922
public enum GameHashes
{
	// Token: 0x0400213D RID: 8509
	Saved = 1589904519,
	// Token: 0x0400213E RID: 8510
	Loaded = -1594984443,
	// Token: 0x0400213F RID: 8511
	SetModel = 364584999,
	// Token: 0x04002140 RID: 8512
	SelectObject = -1503271301,
	// Token: 0x04002141 RID: 8513
	HighlightObject = -1201923725,
	// Token: 0x04002142 RID: 8514
	HighlightStatusItem = 2095258329,
	// Token: 0x04002143 RID: 8515
	ClickTile = -2043434986,
	// Token: 0x04002144 RID: 8516
	HoverObject = -1454059909,
	// Token: 0x04002145 RID: 8517
	UnhoverObject = -983359998,
	// Token: 0x04002146 RID: 8518
	ObjectReplaced = 1606648047,
	// Token: 0x04002147 RID: 8519
	DebugInsantBuildModeChanged = 1557339983,
	// Token: 0x04002148 RID: 8520
	ConnectWiring = -1950680436,
	// Token: 0x04002149 RID: 8521
	DisconnectWiring = 1903019870,
	// Token: 0x0400214A RID: 8522
	SetActivator = 315865555,
	// Token: 0x0400214B RID: 8523
	FocusLost = 553322396,
	// Token: 0x0400214C RID: 8524
	UIClear = 288942073,
	// Token: 0x0400214D RID: 8525
	UIRefresh = 1980521255,
	// Token: 0x0400214E RID: 8526
	UIRefreshData = -1514841199,
	// Token: 0x0400214F RID: 8527
	BuildToolDeactivated = -1190690038,
	// Token: 0x04002150 RID: 8528
	ActiveToolChanged = 1174281782,
	// Token: 0x04002151 RID: 8529
	OnStorageInteracted = -778359855,
	// Token: 0x04002152 RID: 8530
	OnStorageChange = -1697596308,
	// Token: 0x04002153 RID: 8531
	UpdateStorageInfo = -1197125120,
	// Token: 0x04002154 RID: 8532
	OnStore = 856640610,
	// Token: 0x04002155 RID: 8533
	OnStorageLockerSetupComplete = -1683615038,
	// Token: 0x04002156 RID: 8534
	Died = 1623392196,
	// Token: 0x04002157 RID: 8535
	DeathAnimComplete = -66249442,
	// Token: 0x04002158 RID: 8536
	Revived = -1117766961,
	// Token: 0x04002159 RID: 8537
	Defrosted = -1804024542,
	// Token: 0x0400215A RID: 8538
	VisualizerChanged = -2100764682,
	// Token: 0x0400215B RID: 8539
	BuildingStateChanged = -809948329,
	// Token: 0x0400215C RID: 8540
	OperationalChanged = -592767678,
	// Token: 0x0400215D RID: 8541
	FunctionalChanged = -1852328367,
	// Token: 0x0400215E RID: 8542
	OperationalFlagChanged = 187661686,
	// Token: 0x0400215F RID: 8543
	ActiveChanged = 824508782,
	// Token: 0x04002160 RID: 8544
	Open = 1677747338,
	// Token: 0x04002161 RID: 8545
	Close = 476357528,
	// Token: 0x04002162 RID: 8546
	DoorStateChanged = 1734268753,
	// Token: 0x04002163 RID: 8547
	DoorControlStateChanged = 279163026,
	// Token: 0x04002164 RID: 8548
	AccessControlChanged = -1525636549,
	// Token: 0x04002165 RID: 8549
	NewBuilding = -1661515756,
	// Token: 0x04002166 RID: 8550
	RefreshUserMenu = 493375141,
	// Token: 0x04002167 RID: 8551
	NewConstruction = 2121280625,
	// Token: 0x04002168 RID: 8552
	DroppedAsLoot = -375153990,
	// Token: 0x04002169 RID: 8553
	DroppedAll = -1957399615,
	// Token: 0x0400216A RID: 8554
	ResearchComplete = -107300940,
	// Token: 0x0400216B RID: 8555
	ActiveResearchChanged = -1914338957,
	// Token: 0x0400216C RID: 8556
	StudyComplete = -1436775550,
	// Token: 0x0400216D RID: 8557
	StudyCancel = 1488501379,
	// Token: 0x0400216E RID: 8558
	StatusChange = -111137758,
	// Token: 0x0400216F RID: 8559
	EquippedItemEquipper = -448952673,
	// Token: 0x04002170 RID: 8560
	UnequippedItemEquipper = -1285462312,
	// Token: 0x04002171 RID: 8561
	EquippedItemEquippable = -1617557748,
	// Token: 0x04002172 RID: 8562
	UnequippedItemEquippable = -170173755,
	// Token: 0x04002173 RID: 8563
	EnableOverlay = 1248612973,
	// Token: 0x04002174 RID: 8564
	DisableOverlay = 2015652040,
	// Token: 0x04002175 RID: 8565
	OverlayChanged = 1798162660,
	// Token: 0x04002176 RID: 8566
	SleepStarted = -1283701846,
	// Token: 0x04002177 RID: 8567
	SleepFail = 1338475637,
	// Token: 0x04002178 RID: 8568
	SleepDisturbedByNoise = 1621815900,
	// Token: 0x04002179 RID: 8569
	SleepDisturbedByLight = -1063113160,
	// Token: 0x0400217A RID: 8570
	SleepDisturbedByFearOfDark = -1307593733,
	// Token: 0x0400217B RID: 8571
	SleepDisturbedByMovement = -717201811,
	// Token: 0x0400217C RID: 8572
	SleepFinished = -2090444759,
	// Token: 0x0400217D RID: 8573
	EatStart = 1356255274,
	// Token: 0x0400217E RID: 8574
	EatFail = 1723868278,
	// Token: 0x0400217F RID: 8575
	EatCompleteEdible = -10536414,
	// Token: 0x04002180 RID: 8576
	EatStartEater = 1406130139,
	// Token: 0x04002181 RID: 8577
	EatCompleteEater = 1121894420,
	// Token: 0x04002182 RID: 8578
	EatSolidComplete = 1386391852,
	// Token: 0x04002183 RID: 8579
	CaloriesConsumed = -2038961714,
	// Token: 0x04002184 RID: 8580
	PrepareForExplosion = -979425869,
	// Token: 0x04002185 RID: 8581
	Cancel = 2127324410,
	// Token: 0x04002186 RID: 8582
	WorkChoreDisabled = 2108245096,
	// Token: 0x04002187 RID: 8583
	MarkForDeconstruct = -790448070,
	// Token: 0x04002188 RID: 8584
	MarkForRelocate = 923661281,
	// Token: 0x04002189 RID: 8585
	Prioritize = 1531330463,
	// Token: 0x0400218A RID: 8586
	Deprioritize = -195779040,
	// Token: 0x0400218B RID: 8587
	DeconstructStart = 1830962028,
	// Token: 0x0400218C RID: 8588
	DeconstructComplete = -702296337,
	// Token: 0x0400218D RID: 8589
	ItemReachable = 1261805274,
	// Token: 0x0400218E RID: 8590
	ItemUnreachable = -600999071,
	// Token: 0x0400218F RID: 8591
	CameraMove = 564128022,
	// Token: 0x04002190 RID: 8592
	PauseChanged = -1788536802,
	// Token: 0x04002191 RID: 8593
	BaseAlreadyCreated = -1992507039,
	// Token: 0x04002192 RID: 8594
	RegionChanged = -1601261024,
	// Token: 0x04002193 RID: 8595
	Rotated = -1643076535,
	// Token: 0x04002194 RID: 8596
	AnimQueueComplete = -1061186183,
	// Token: 0x04002195 RID: 8597
	HungerStatusChanged = -12937937,
	// Token: 0x04002196 RID: 8598
	TutorialOpened = 1634669191,
	// Token: 0x04002197 RID: 8599
	CreatureStatusChanged = -151109373,
	// Token: 0x04002198 RID: 8600
	LevelUp = -110704193,
	// Token: 0x04002199 RID: 8601
	NewDay = 631075836,
	// Token: 0x0400219A RID: 8602
	NewBlock = -1215042067,
	// Token: 0x0400219B RID: 8603
	ScheduleChanged = 467134493,
	// Token: 0x0400219C RID: 8604
	ScheduleBlocksTick = 1714332666,
	// Token: 0x0400219D RID: 8605
	ScheduleBlocksChanged = -894023145,
	// Token: 0x0400219E RID: 8606
	Craft = 748399584,
	// Token: 0x0400219F RID: 8607
	Harvest = 1272413801,
	// Token: 0x040021A0 RID: 8608
	HarvestComplete = 113170146,
	// Token: 0x040021A1 RID: 8609
	Absorb = -2064133523,
	// Token: 0x040021A2 RID: 8610
	AbsorbedBy = -1940207677,
	// Token: 0x040021A3 RID: 8611
	SplitFromChunk = 1335436905,
	// Token: 0x040021A4 RID: 8612
	Butcher = 395373363,
	// Token: 0x040021A5 RID: 8613
	WalkBy = -517744704,
	// Token: 0x040021A6 RID: 8614
	DoDamage = -184635526,
	// Token: 0x040021A7 RID: 8615
	NearMelting = -2009062694,
	// Token: 0x040021A8 RID: 8616
	MeltDown = 1930836866,
	// Token: 0x040021A9 RID: 8617
	Landed = 1188683690,
	// Token: 0x040021AA RID: 8618
	GeotunerChange = 1763323737,
	// Token: 0x040021AB RID: 8619
	GeyserEruption = -593169791,
	// Token: 0x040021AC RID: 8620
	Pacified = -1427155335,
	// Token: 0x040021AD RID: 8621
	Rescued = -638309935,
	// Token: 0x040021AE RID: 8622
	Released = 501672573,
	// Token: 0x040021AF RID: 8623
	FeedingStart = -1114035522,
	// Token: 0x040021B0 RID: 8624
	FeedingEnd = 581341623,
	// Token: 0x040021B1 RID: 8625
	FeedingComplete = 1270416669,
	// Token: 0x040021B2 RID: 8626
	PlanterStorage = 1309017699,
	// Token: 0x040021B3 RID: 8627
	GrowthStateMature = -2116516046,
	// Token: 0x040021B4 RID: 8628
	Fertilized = -1396791468,
	// Token: 0x040021B5 RID: 8629
	Unfertilized = -1073674739,
	// Token: 0x040021B6 RID: 8630
	CropDepleted = 591871899,
	// Token: 0x040021B7 RID: 8631
	BlightChanged = -1425542080,
	// Token: 0x040021B8 RID: 8632
	CropTended = 90606262,
	// Token: 0x040021B9 RID: 8633
	SeedProduced = 472291861,
	// Token: 0x040021BA RID: 8634
	SeedDropped = -1736624145,
	// Token: 0x040021BB RID: 8635
	DestinationReached = 387220196,
	// Token: 0x040021BC RID: 8636
	NavigationFailed = -766531887,
	// Token: 0x040021BD RID: 8637
	NavigationCellChanged = 915392638,
	// Token: 0x040021BE RID: 8638
	AssignablesChanged = -1585839766,
	// Token: 0x040021BF RID: 8639
	AssigneeChanged = 684616645,
	// Token: 0x040021C0 RID: 8640
	ObjectDestroyed = 1969584890,
	// Token: 0x040021C1 RID: 8641
	QueueDestroyObject = 1502190696,
	// Token: 0x040021C2 RID: 8642
	TargetLost = 2144432245,
	// Token: 0x040021C3 RID: 8643
	NewGameSpawn = 1119167081,
	// Token: 0x040021C4 RID: 8644
	OccupiableChanged = 2004582811,
	// Token: 0x040021C5 RID: 8645
	OccupantChanged = -731304873,
	// Token: 0x040021C6 RID: 8646
	OccupantValidChanged = -1820564715,
	// Token: 0x040021C7 RID: 8647
	EffectAdded = -1901442097,
	// Token: 0x040021C8 RID: 8648
	EffectRemoved = -1157678353,
	// Token: 0x040021C9 RID: 8649
	GameOver = 1719568262,
	// Token: 0x040021CA RID: 8650
	ConduitConnectionChanged = -2094018600,
	// Token: 0x040021CB RID: 8651
	PowerStatusChanged = 1088293757,
	// Token: 0x040021CC RID: 8652
	ConnectionsChanged = -1041684577,
	// Token: 0x040021CD RID: 8653
	ReachableChanged = -1432940121,
	// Token: 0x040021CE RID: 8654
	AddedFetchable = -1588644844,
	// Token: 0x040021CF RID: 8655
	RemovedFetchable = -1491270284,
	// Token: 0x040021D0 RID: 8656
	EnteredRedAlert = 1585324898,
	// Token: 0x040021D1 RID: 8657
	ExitedRedAlert = -1393151672,
	// Token: 0x040021D2 RID: 8658
	SafeCellDetected = 982561777,
	// Token: 0x040021D3 RID: 8659
	SafeCellLost = 506919987,
	// Token: 0x040021D4 RID: 8660
	CreatureMoveComplete = -1436222551,
	// Token: 0x040021D5 RID: 8661
	FishableDepleted = -1851044355,
	// Token: 0x040021D6 RID: 8662
	TooHotWarning = -1234705021,
	// Token: 0x040021D7 RID: 8663
	TooHotFatal = -55477301,
	// Token: 0x040021D8 RID: 8664
	TooColdWarning = -107174716,
	// Token: 0x040021D9 RID: 8665
	TooColdFatal = -1758196852,
	// Token: 0x040021DA RID: 8666
	TooColdSickness = 54654253,
	// Token: 0x040021DB RID: 8667
	TooHotSickness = -1174019026,
	// Token: 0x040021DC RID: 8668
	OptimalTemperatureAchieved = 115888613,
	// Token: 0x040021DD RID: 8669
	CreatureReproduce = 230069070,
	// Token: 0x040021DE RID: 8670
	ReadyToHatch = 657149762,
	// Token: 0x040021DF RID: 8671
	Hatch = 1922945024,
	// Token: 0x040021E0 RID: 8672
	IlluminationComfort = 1113102781,
	// Token: 0x040021E1 RID: 8673
	IlluminationDiscomfort = 1387626797,
	// Token: 0x040021E2 RID: 8674
	RadiationComfort = 874353739,
	// Token: 0x040021E3 RID: 8675
	RadiationDiscomfort = 1788072223,
	// Token: 0x040021E4 RID: 8676
	RadiationRecovery = 1556680150,
	// Token: 0x040021E5 RID: 8677
	UseSuccess = 58624316,
	// Token: 0x040021E6 RID: 8678
	UseFail = 1572098533,
	// Token: 0x040021E7 RID: 8679
	BodyOfWaterChanged = -263784810,
	// Token: 0x040021E8 RID: 8680
	BeginChore = -1988963660,
	// Token: 0x040021E9 RID: 8681
	EndChore = 1745615042,
	// Token: 0x040021EA RID: 8682
	AddUrge = -736698276,
	// Token: 0x040021EB RID: 8683
	RemoveUrge = 231622047,
	// Token: 0x040021EC RID: 8684
	ClosestEdibleChanged = 86328522,
	// Token: 0x040021ED RID: 8685
	CellChanged = 1088554450,
	// Token: 0x040021EE RID: 8686
	ObjectMovementStateChanged = 1027377649,
	// Token: 0x040021EF RID: 8687
	ObjectMovementWakeUp = -97592435,
	// Token: 0x040021F0 RID: 8688
	ObjectMovementSleep = -696472375,
	// Token: 0x040021F1 RID: 8689
	SicknessAdded = 1592732331,
	// Token: 0x040021F2 RID: 8690
	SicknessCured = 77635178,
	// Token: 0x040021F3 RID: 8691
	GermPresenceChanged = -1689370368,
	// Token: 0x040021F4 RID: 8692
	EnteredToxicArea = -1198182067,
	// Token: 0x040021F5 RID: 8693
	ExitedToxicArea = 369532135,
	// Token: 0x040021F6 RID: 8694
	EnteredBreathableArea = 99949694,
	// Token: 0x040021F7 RID: 8695
	ExitedBreathableArea = -1189351068,
	// Token: 0x040021F8 RID: 8696
	AreaElementSafeChanged = -2023773544,
	// Token: 0x040021F9 RID: 8697
	ColonyHasRationsChanged = -171255324,
	// Token: 0x040021FA RID: 8698
	AssignableReachabilityChanged = 334784980,
	// Token: 0x040021FB RID: 8699
	TimeOfDayChanged = 1791086652,
	// Token: 0x040021FC RID: 8700
	MessagesChanged = -599791736,
	// Token: 0x040021FD RID: 8701
	MessageAdded = 1558809273,
	// Token: 0x040021FE RID: 8702
	SaveGameReady = -1917495436,
	// Token: 0x040021FF RID: 8703
	StartGameUser = -838649377,
	// Token: 0x04002200 RID: 8704
	ToiletSensorChanged = -752545459,
	// Token: 0x04002201 RID: 8705
	ExposeToDisease = -283306403,
	// Token: 0x04002202 RID: 8706
	EntombedChanged = -1089732772,
	// Token: 0x04002203 RID: 8707
	HiddenChanged = -298960054,
	// Token: 0x04002204 RID: 8708
	EnteredFogOfWar = 40674218,
	// Token: 0x04002205 RID: 8709
	ExitedFogOfWar = 1357164944,
	// Token: 0x04002206 RID: 8710
	Nighttime = -722330267,
	// Token: 0x04002207 RID: 8711
	NewBaseCreated = -122303817,
	// Token: 0x04002208 RID: 8712
	ExposedToPutridOdour = -513254434,
	// Token: 0x04002209 RID: 8713
	FinishedActingOut = -1734580852,
	// Token: 0x0400220A RID: 8714
	Cringe = 508119890,
	// Token: 0x0400220B RID: 8715
	Uprooted = -216549700,
	// Token: 0x0400220C RID: 8716
	UprootTypeChanged = 755331037,
	// Token: 0x0400220D RID: 8717
	Drowning = 1949704522,
	// Token: 0x0400220E RID: 8718
	Drowned = -750750377,
	// Token: 0x0400220F RID: 8719
	DryingOut = -2057657673,
	// Token: 0x04002210 RID: 8720
	EnteredWetArea = 1555379996,
	// Token: 0x04002211 RID: 8721
	DriedOut = 1514431252,
	// Token: 0x04002212 RID: 8722
	CropReady = -1686619725,
	// Token: 0x04002213 RID: 8723
	CropPicked = -1072826864,
	// Token: 0x04002214 RID: 8724
	CropSpawned = 35625290,
	// Token: 0x04002215 RID: 8725
	LowPressureWarning = -1175525437,
	// Token: 0x04002216 RID: 8726
	LowPressureFatal = -593125877,
	// Token: 0x04002217 RID: 8727
	OptimalPressureAchieved = -907106982,
	// Token: 0x04002218 RID: 8728
	HighPressureWarning = 103243573,
	// Token: 0x04002219 RID: 8729
	HighPressureFatal = 646131325,
	// Token: 0x0400221A RID: 8730
	Wilt = -724860998,
	// Token: 0x0400221B RID: 8731
	WiltRecover = 712767498,
	// Token: 0x0400221C RID: 8732
	CropSleep = 655199271,
	// Token: 0x0400221D RID: 8733
	CropWakeUp = -1389112401,
	// Token: 0x0400221E RID: 8734
	Grow = -254803949,
	// Token: 0x0400221F RID: 8735
	BurstEmitDisease = 944091753,
	// Token: 0x04002220 RID: 8736
	LaserOn = -673283254,
	// Token: 0x04002221 RID: 8737
	LaserOff = -1559999068,
	// Token: 0x04002222 RID: 8738
	VentAnimatingChanged = -793429877,
	// Token: 0x04002223 RID: 8739
	VentClosed = -997182271,
	// Token: 0x04002224 RID: 8740
	VentOpen = 1531265279,
	// Token: 0x04002225 RID: 8741
	StartTransition = 1189352983,
	// Token: 0x04002226 RID: 8742
	Attacking = 1039067354,
	// Token: 0x04002227 RID: 8743
	StoppedAttacking = 379728621,
	// Token: 0x04002228 RID: 8744
	Attacked = -787691065,
	// Token: 0x04002229 RID: 8745
	HealthChanged = -1664904872,
	// Token: 0x0400222A RID: 8746
	BecameIncapacitated = -1506500077,
	// Token: 0x0400222B RID: 8747
	IncapacitationRecovery = -1256572400,
	// Token: 0x0400222C RID: 8748
	FullyHealed = -1491582671,
	// Token: 0x0400222D RID: 8749
	LayEgg = 1193600993,
	// Token: 0x0400222E RID: 8750
	DebugGoTo = 775300118,
	// Token: 0x0400222F RID: 8751
	Threatened = -96307134,
	// Token: 0x04002230 RID: 8752
	SafeFromThreats = -21431934,
	// Token: 0x04002231 RID: 8753
	BeginWalk = 1773898642,
	// Token: 0x04002232 RID: 8754
	EndWalk = 1597112836,
	// Token: 0x04002233 RID: 8755
	IsMovableChanged = -962627472,
	// Token: 0x04002234 RID: 8756
	CollectPriorityCommands = 809822742,
	// Token: 0x04002235 RID: 8757
	StartMining = -1762453998,
	// Token: 0x04002236 RID: 8758
	StopMining = 939543986,
	// Token: 0x04002237 RID: 8759
	OreSizeChanged = 1807976145,
	// Token: 0x04002238 RID: 8760
	UIScaleChange = -810220474,
	// Token: 0x04002239 RID: 8761
	NotStressed = -1175311898,
	// Token: 0x0400223A RID: 8762
	Stressed = 402754227,
	// Token: 0x0400223B RID: 8763
	StressedHadEnough = -115777784,
	// Token: 0x0400223C RID: 8764
	ResearchPointsChanged = -125623018,
	// Token: 0x0400223D RID: 8765
	ConduitContentsFrozen = -700727624,
	// Token: 0x0400223E RID: 8766
	ConduitContentsBoiling = -1152799878,
	// Token: 0x0400223F RID: 8767
	BuildingReceivedDamage = -1964935036,
	// Token: 0x04002240 RID: 8768
	BuildingBroken = 774203113,
	// Token: 0x04002241 RID: 8769
	BuildingPartiallyRepaired = -1699355994,
	// Token: 0x04002242 RID: 8770
	BuildingFullyRepaired = -1735440190,
	// Token: 0x04002243 RID: 8771
	DoBuildingDamage = -794517298,
	// Token: 0x04002244 RID: 8772
	UseBuilding = 1175726587,
	// Token: 0x04002245 RID: 8773
	Preserved = 751746776,
	// Token: 0x04002246 RID: 8774
	AcousticDisturbance = -527751701,
	// Token: 0x04002247 RID: 8775
	CopySettings = -905833192,
	// Token: 0x04002248 RID: 8776
	AttachFollowCam = -1506069671,
	// Token: 0x04002249 RID: 8777
	DetachFollowCam = -485480405,
	// Token: 0x0400224A RID: 8778
	EmitterBlocked = 1615168894,
	// Token: 0x0400224B RID: 8779
	EmitterUnblocked = -657992955,
	// Token: 0x0400224C RID: 8780
	EnterPeaceful = -649988633,
	// Token: 0x0400224D RID: 8781
	ExitPeaceful = -1215818035,
	// Token: 0x0400224E RID: 8782
	BuildingOverheated = 1832602615,
	// Token: 0x0400224F RID: 8783
	BuildingNoLongerOverheated = 171119937,
	// Token: 0x04002250 RID: 8784
	LiquidResourceEmpty = -370379773,
	// Token: 0x04002251 RID: 8785
	LiquidResourceRecieved = 207387507,
	// Token: 0x04002252 RID: 8786
	WrongAtmosphere = 221594799,
	// Token: 0x04002253 RID: 8787
	CorrectAtmosphere = 777259436,
	// Token: 0x04002254 RID: 8788
	ReceptacleOperational = 1628751838,
	// Token: 0x04002255 RID: 8789
	ReceptacleInoperational = 960378201,
	// Token: 0x04002256 RID: 8790
	PathAdvanced = 1347184327,
	// Token: 0x04002257 RID: 8791
	UpdateRoom = 144050788,
	// Token: 0x04002258 RID: 8792
	ChangeRoom = -832141045,
	// Token: 0x04002259 RID: 8793
	EquipmentChanged = -2146166042,
	// Token: 0x0400225A RID: 8794
	LogicEvent = -801688580,
	// Token: 0x0400225B RID: 8795
	BeginBreathRecovery = 961737054,
	// Token: 0x0400225C RID: 8796
	EndBreathRecovery = -2037519664,
	// Token: 0x0400225D RID: 8797
	FabricatorOrdersUpdated = 1721324763,
	// Token: 0x0400225E RID: 8798
	TagsChanged = -1582839653,
	// Token: 0x0400225F RID: 8799
	RolesUpdated = -1523247426,
	// Token: 0x04002260 RID: 8800
	AssignedRoleChanged = 540773776,
	// Token: 0x04002261 RID: 8801
	OnlyFetchMarkedItemsSettingChanged = 644822890,
	// Token: 0x04002262 RID: 8802
	OnlyFetchSpicedItemsSettingChanged = 1163645216,
	// Token: 0x04002263 RID: 8803
	BuildingCompleteDestroyed = -21016276,
	// Token: 0x04002264 RID: 8804
	BehaviourTagComplete = -739654666,
	// Token: 0x04002265 RID: 8805
	CreatureArrivedAtRanchStation = -1357116271,
	// Token: 0x04002266 RID: 8806
	RancherReadyAtRanchStation = 1084749845,
	// Token: 0x04002267 RID: 8807
	RanchingComplete = 1827504087,
	// Token: 0x04002268 RID: 8808
	CreatureAbandonedRanchStation = -364750427,
	// Token: 0x04002269 RID: 8809
	RanchStationNoLongerAvailable = 1689625967,
	// Token: 0x0400226A RID: 8810
	CreatureArrivedAtCapturePoint = -1992722293,
	// Token: 0x0400226B RID: 8811
	RancherReadyAtCapturePoint = 449143823,
	// Token: 0x0400226C RID: 8812
	FixedCaptureComplete = 643180843,
	// Token: 0x0400226D RID: 8813
	CreatureAbandonedCapturePoint = -1000356449,
	// Token: 0x0400226E RID: 8814
	CapturePointNoLongerAvailable = 1034952693,
	// Token: 0x0400226F RID: 8815
	ElementNoLongerAvailable = 801383139,
	// Token: 0x04002270 RID: 8816
	BreedingChancesChanged = 1059811075,
	// Token: 0x04002271 RID: 8817
	ToggleSandbox = -1948169901,
	// Token: 0x04002272 RID: 8818
	ConsumePlant = -1793167409,
	// Token: 0x04002273 RID: 8819
	AteFromStorage = -1452790913,
	// Token: 0x04002274 RID: 8820
	GrowIntoAdult = 2143203559,
	// Token: 0x04002275 RID: 8821
	SpawnedFrom = -2027483228,
	// Token: 0x04002276 RID: 8822
	UserSettingsChanged = -543130682,
	// Token: 0x04002277 RID: 8823
	StructureTemperatureRegistered = -1555603773,
	// Token: 0x04002278 RID: 8824
	TopicDiscovered = 937885943,
	// Token: 0x04002279 RID: 8825
	TopicDiscussed = 1102989392,
	// Token: 0x0400227A RID: 8826
	StartedTalking = -594200555,
	// Token: 0x0400227B RID: 8827
	StoppedTalking = 25860745,
	// Token: 0x0400227C RID: 8828
	MetaUnlockUnlocked = 1594320620,
	// Token: 0x0400227D RID: 8829
	UnstableGroundImpact = -975551167,
	// Token: 0x0400227E RID: 8830
	ReturnRocket = 1366341636,
	// Token: 0x0400227F RID: 8831
	DoLaunchRocket = 705820818,
	// Token: 0x04002280 RID: 8832
	DoReturnRocket = -1165815793,
	// Token: 0x04002281 RID: 8833
	RocketLanded = -887025858,
	// Token: 0x04002282 RID: 8834
	RocketLaunched = -1277991738,
	// Token: 0x04002283 RID: 8835
	RocketModuleChanged = 1512695988,
	// Token: 0x04002284 RID: 8836
	RequestRegisterLaunchCondition = 1711162550,
	// Token: 0x04002285 RID: 8837
	IgniteEngine = -1358394196,
	// Token: 0x04002286 RID: 8838
	RocketInteriorComplete = -71801987,
	// Token: 0x04002287 RID: 8839
	RocketRequestLaunch = 191901966,
	// Token: 0x04002288 RID: 8840
	RocketReadyToLaunch = -849456099,
	// Token: 0x04002289 RID: 8841
	UpdateRocketStatus = -688990705,
	// Token: 0x0400228A RID: 8842
	StartRocketLaunch = 546421097,
	// Token: 0x0400228B RID: 8843
	RocketTouchDown = -735346771,
	// Token: 0x0400228C RID: 8844
	RocketCreated = 374403796,
	// Token: 0x0400228D RID: 8845
	RocketRestrictionChanged = 1861523068,
	// Token: 0x0400228E RID: 8846
	StoragePriorityChanged = -1626373771,
	// Token: 0x0400228F RID: 8847
	StorageCapacityChanged = -945020481,
	// Token: 0x04002290 RID: 8848
	AttachmentNetworkChanged = 486707561,
	// Token: 0x04002291 RID: 8849
	LaunchConditionChanged = 1655598572,
	// Token: 0x04002292 RID: 8850
	ChoreInterrupt = 1485595942,
	// Token: 0x04002293 RID: 8851
	Flush = -350347868,
	// Token: 0x04002294 RID: 8852
	WorkerPlayPostAnim = -1142962013,
	// Token: 0x04002295 RID: 8853
	DeactivateResearchScreen = -1974454597,
	// Token: 0x04002296 RID: 8854
	StarmapDestinationChanged = 929158128,
	// Token: 0x04002297 RID: 8855
	DuplicantDied = 282337316,
	// Token: 0x04002298 RID: 8856
	DiscoveredSpace = -818188514,
	// Token: 0x04002299 RID: 8857
	StarmapAnalysisTargetChanged = 532901469,
	// Token: 0x0400229A RID: 8858
	BeginMeteorBombardment = -84771526,
	// Token: 0x0400229B RID: 8859
	PrefabInstanceIDRedirected = 17633999,
	// Token: 0x0400229C RID: 8860
	RootHealthChanged = 912965142,
	// Token: 0x0400229D RID: 8861
	HarvestDesignationChanged = -266953818,
	// Token: 0x0400229E RID: 8862
	TreeClimbComplete = -230295600,
	// Token: 0x0400229F RID: 8863
	EnteredYellowAlert = -741654735,
	// Token: 0x040022A0 RID: 8864
	ExitedYellowAlert = -2062778933,
	// Token: 0x040022A1 RID: 8865
	SkillPointAquired = 1505456302,
	// Token: 0x040022A2 RID: 8866
	MedCotMinimumThresholdUpdated = 875045922,
	// Token: 0x040022A3 RID: 8867
	GameOptionsUpdated = 75424175,
	// Token: 0x040022A4 RID: 8868
	CheckColonyAchievements = 395452326,
	// Token: 0x040022A5 RID: 8869
	StartWork = 1568504979,
	// Token: 0x040022A6 RID: 8870
	WorkableStartWork = 853695848,
	// Token: 0x040022A7 RID: 8871
	WorkableCompleteWork = -2011693419,
	// Token: 0x040022A8 RID: 8872
	WorkableStopWork = 679550494,
	// Token: 0x040022A9 RID: 8873
	StartReactable = -909573545,
	// Token: 0x040022AA RID: 8874
	EndReactable = 824899998,
	// Token: 0x040022AB RID: 8875
	TeleporterIDsChanged = -1266722732,
	// Token: 0x040022AC RID: 8876
	ClusterDestinationChanged = 543433792,
	// Token: 0x040022AD RID: 8877
	ClusterDestinationReached = 1796608350,
	// Token: 0x040022AE RID: 8878
	ClusterLocationChanged = -1298331547,
	// Token: 0x040022AF RID: 8879
	TemperatureUnitChanged = 999382396,
	// Token: 0x040022B0 RID: 8880
	GameplayEventStart = 1491341646,
	// Token: 0x040022B1 RID: 8881
	GameplayEventMonitorStart = -1660384580,
	// Token: 0x040022B2 RID: 8882
	GameplayEventMonitorChanged = -1122598290,
	// Token: 0x040022B3 RID: 8883
	GameplayEventMonitorEnd = -828272459,
	// Token: 0x040022B4 RID: 8884
	GameplayEventCommence = -2043101269,
	// Token: 0x040022B5 RID: 8885
	GameplayEventEnd = 1287635015,
	// Token: 0x040022B6 RID: 8886
	ClusterTelescopeTargetAdded = -1554423969,
	// Token: 0x040022B7 RID: 8887
	ClusterFogOfWarRevealed = -1991583975,
	// Token: 0x040022B8 RID: 8888
	ActiveWorldChanged = 1983128072,
	// Token: 0x040022B9 RID: 8889
	DiscoveredWorldsChanged = -521212405,
	// Token: 0x040022BA RID: 8890
	WorldAdded = -1280433810,
	// Token: 0x040022BB RID: 8891
	WorldRemoved = -1078710002,
	// Token: 0x040022BC RID: 8892
	WorldParentChanged = 880851192,
	// Token: 0x040022BD RID: 8893
	WorldRenamed = 1943181844,
	// Token: 0x040022BE RID: 8894
	MinionMigration = 586301400,
	// Token: 0x040022BF RID: 8895
	MinionStorageChanged = -392340561,
	// Token: 0x040022C0 RID: 8896
	NewWorldVisited = -434755240,
	// Token: 0x040022C1 RID: 8897
	MinionDelta = 2144209314,
	// Token: 0x040022C2 RID: 8898
	OnParticleStorageChanged = -1837862626,
	// Token: 0x040022C3 RID: 8899
	OnParticleStorageEmpty = 155636535,
	// Token: 0x040022C4 RID: 8900
	ParticleStorageCapacityChanged = -795826715,
	// Token: 0x040022C5 RID: 8901
	PoorAirQuality = -935848905,
	// Token: 0x040022C6 RID: 8902
	NameChanged = 1102426921,
	// Token: 0x040022C7 RID: 8903
	TrapTriggered = -358342870,
	// Token: 0x040022C8 RID: 8904
	BuildingActivated = -1909216579,
	// Token: 0x040022C9 RID: 8905
	EnterHome = -2099923209,
	// Token: 0x040022CA RID: 8906
	ExitHome = -1220248099,
	// Token: 0x040022CB RID: 8907
	JettisonedLander = 1792516731,
	// Token: 0x040022CC RID: 8908
	JettisonCargo = 218291192,
	// Token: 0x040022CD RID: 8909
	AssignmentGroupChanged = -1123234494,
	// Token: 0x040022CE RID: 8910
	SuitTankDelta = 608245985,
	// Token: 0x040022CF RID: 8911
	ChainedNetworkChanged = -1009905786,
	// Token: 0x040022D0 RID: 8912
	FoundationChanged = -1960061727,
	// Token: 0x040022D1 RID: 8913
	PlantSubspeciesProgress = -1531232972,
	// Token: 0x040022D2 RID: 8914
	PlantSubspeciesComplete = -98362560,
	// Token: 0x040022D3 RID: 8915
	LimitValveAmountChanged = -1722241721,
	// Token: 0x040022D4 RID: 8916
	LockerDroppedContents = -372600542,
	// Token: 0x040022D5 RID: 8917
	Happy = 1890751808,
	// Token: 0x040022D6 RID: 8918
	Unhappy = -647798969,
	// Token: 0x040022D7 RID: 8919
	DoorsLinked = -1118736034,
	// Token: 0x040022D8 RID: 8920
	RocketSelfDestructRequested = -1061799784,
	// Token: 0x040022D9 RID: 8921
	RocketExploded = -1311384361,
	// Token: 0x040022DA RID: 8922
	ModuleLanderLanded = 1591811118,
	// Token: 0x040022DB RID: 8923
	PartyLineJoined = 564760259,
	// Token: 0x040022DC RID: 8924
	ScreenResolutionChanged = 445618876,
	// Token: 0x040022DD RID: 8925
	GamepadUIModeChanged = -442024484,
	// Token: 0x040022DE RID: 8926
	CatchyTune = -1278274506,
	// Token: 0x040022DF RID: 8927
	MegaBrainTankCandidateDupesChanged = 374655100,
	// Token: 0x040022E0 RID: 8928
	DreamsOn = -1768884913,
	// Token: 0x040022E1 RID: 8929
	DreamsOff = 49503455
}
