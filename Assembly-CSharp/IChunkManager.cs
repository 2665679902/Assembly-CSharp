using System;
using UnityEngine;

// Token: 0x020004DA RID: 1242
public interface IChunkManager
{
	// Token: 0x06001D73 RID: 7539
	SubstanceChunk CreateChunk(Element element, float mass, float temperature, byte diseaseIdx, int diseaseCount, Vector3 position);

	// Token: 0x06001D74 RID: 7540
	SubstanceChunk CreateChunk(SimHashes element_id, float mass, float temperature, byte diseaseIdx, int diseaseCount, Vector3 position);
}
