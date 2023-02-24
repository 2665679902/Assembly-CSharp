using System;

// Token: 0x0200091D RID: 2333
public class SimData
{
	// Token: 0x04002D50 RID: 11600
	public unsafe Sim.EmittedMassInfo* emittedMassEntries;

	// Token: 0x04002D51 RID: 11601
	public unsafe Sim.ElementChunkInfo* elementChunks;

	// Token: 0x04002D52 RID: 11602
	public unsafe Sim.BuildingTemperatureInfo* buildingTemperatures;

	// Token: 0x04002D53 RID: 11603
	public unsafe Sim.DiseaseEmittedInfo* diseaseEmittedInfos;

	// Token: 0x04002D54 RID: 11604
	public unsafe Sim.DiseaseConsumedInfo* diseaseConsumedInfos;
}
