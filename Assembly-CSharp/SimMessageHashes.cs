using System;

// Token: 0x02000A27 RID: 2599
public enum SimMessageHashes
{
	// Token: 0x040034C0 RID: 13504
	Elements_CreateTable = 1108437482,
	// Token: 0x040034C1 RID: 13505
	Elements_CreateInteractions = -930289787,
	// Token: 0x040034C2 RID: 13506
	SetWorldZones = -457308393,
	// Token: 0x040034C3 RID: 13507
	ModifyCellWorldZone = -449718014,
	// Token: 0x040034C4 RID: 13508
	Disease_CreateTable = 825301935,
	// Token: 0x040034C5 RID: 13509
	Load = -672538170,
	// Token: 0x040034C6 RID: 13510
	Start = -931446686,
	// Token: 0x040034C7 RID: 13511
	AllocateCells = 1092408308,
	// Token: 0x040034C8 RID: 13512
	ClearUnoccupiedCells = -1836204275,
	// Token: 0x040034C9 RID: 13513
	DefineWorldOffsets = -895846551,
	// Token: 0x040034CA RID: 13514
	PrepareGameData = 1078620451,
	// Token: 0x040034CB RID: 13515
	SimData_InitializeFromCells = 2062421945,
	// Token: 0x040034CC RID: 13516
	SimData_ResizeAndInitializeVacuumCells = -752676153,
	// Token: 0x040034CD RID: 13517
	SimData_FreeCells = -1167792921,
	// Token: 0x040034CE RID: 13518
	SimFrameManager_NewGameFrame = -775326397,
	// Token: 0x040034CF RID: 13519
	Dig = 833038498,
	// Token: 0x040034D0 RID: 13520
	ModifyCell = -1252920804,
	// Token: 0x040034D1 RID: 13521
	ModifyCellEnergy = 818320644,
	// Token: 0x040034D2 RID: 13522
	SetInsulationValue = -898773121,
	// Token: 0x040034D3 RID: 13523
	SetStrengthValue = 1593243982,
	// Token: 0x040034D4 RID: 13524
	SetVisibleCells = -563057023,
	// Token: 0x040034D5 RID: 13525
	ChangeCellProperties = -469311643,
	// Token: 0x040034D6 RID: 13526
	AddBuildingHeatExchange = 1739021608,
	// Token: 0x040034D7 RID: 13527
	ModifyBuildingHeatExchange = 1818001569,
	// Token: 0x040034D8 RID: 13528
	ModifyBuildingEnergy = -1348791658,
	// Token: 0x040034D9 RID: 13529
	RemoveBuildingHeatExchange = -456116629,
	// Token: 0x040034DA RID: 13530
	AddBuildingToBuildingHeatExchange = -1338718217,
	// Token: 0x040034DB RID: 13531
	AddInContactBuildingToBuildingToBuildingHeatExchange = -1586724321,
	// Token: 0x040034DC RID: 13532
	RemoveBuildingInContactFromBuildingToBuildingHeatExchange = -1993857213,
	// Token: 0x040034DD RID: 13533
	RemoveBuildingToBuildingHeatExchange = 697100730,
	// Token: 0x040034DE RID: 13534
	SetDebugProperties = -1683118492,
	// Token: 0x040034DF RID: 13535
	MassConsumption = 1727657959,
	// Token: 0x040034E0 RID: 13536
	MassEmission = 797274363,
	// Token: 0x040034E1 RID: 13537
	AddElementConsumer = 2024405073,
	// Token: 0x040034E2 RID: 13538
	RemoveElementConsumer = 894417742,
	// Token: 0x040034E3 RID: 13539
	SetElementConsumerData = 1575539738,
	// Token: 0x040034E4 RID: 13540
	AddElementEmitter = -505471181,
	// Token: 0x040034E5 RID: 13541
	ModifyElementEmitter = 403589164,
	// Token: 0x040034E6 RID: 13542
	RemoveElementEmitter = -1524118282,
	// Token: 0x040034E7 RID: 13543
	AddElementChunk = 1445724082,
	// Token: 0x040034E8 RID: 13544
	RemoveElementChunk = -912908555,
	// Token: 0x040034E9 RID: 13545
	SetElementChunkData = -435115907,
	// Token: 0x040034EA RID: 13546
	MoveElementChunk = -374911358,
	// Token: 0x040034EB RID: 13547
	ModifyElementChunkEnergy = 1020555667,
	// Token: 0x040034EC RID: 13548
	ModifyChunkTemperatureAdjuster = -1387601379,
	// Token: 0x040034ED RID: 13549
	AddDiseaseEmitter = 1486783027,
	// Token: 0x040034EE RID: 13550
	ModifyDiseaseEmitter = -1899123924,
	// Token: 0x040034EF RID: 13551
	RemoveDiseaseEmitter = 468135926,
	// Token: 0x040034F0 RID: 13552
	AddDiseaseConsumer = 348345681,
	// Token: 0x040034F1 RID: 13553
	ModifyDiseaseConsumer = -1822987624,
	// Token: 0x040034F2 RID: 13554
	RemoveDiseaseConsumer = -781641650,
	// Token: 0x040034F3 RID: 13555
	ConsumeDisease = -1019841536,
	// Token: 0x040034F4 RID: 13556
	CellDiseaseModification = -1853671274,
	// Token: 0x040034F5 RID: 13557
	ToggleProfiler = -409964931,
	// Token: 0x040034F6 RID: 13558
	SetSavedOptions = 1154135737,
	// Token: 0x040034F7 RID: 13559
	CellRadiationModification = -1914877797,
	// Token: 0x040034F8 RID: 13560
	RadiationSickness = -727746602,
	// Token: 0x040034F9 RID: 13561
	AddRadiationEmitter = -1505895314,
	// Token: 0x040034FA RID: 13562
	ModifyRadiationEmitter = -503965465,
	// Token: 0x040034FB RID: 13563
	RemoveRadiationEmitter = -704259919,
	// Token: 0x040034FC RID: 13564
	RadiationParamsModification = 377112707
}
