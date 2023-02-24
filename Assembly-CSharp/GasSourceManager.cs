using System;
using UnityEngine;

// Token: 0x02000484 RID: 1156
[AddComponentMenu("KMonoBehaviour/scripts/GasSourceManager")]
public class GasSourceManager : KMonoBehaviour, IChunkManager
{
	// Token: 0x060019CF RID: 6607 RVA: 0x0008A860 File Offset: 0x00088A60
	protected override void OnPrefabInit()
	{
		GasSourceManager.Instance = this;
	}

	// Token: 0x060019D0 RID: 6608 RVA: 0x0008A868 File Offset: 0x00088A68
	public SubstanceChunk CreateChunk(SimHashes element_id, float mass, float temperature, byte diseaseIdx, int diseaseCount, Vector3 position)
	{
		return this.CreateChunk(ElementLoader.FindElementByHash(element_id), mass, temperature, diseaseIdx, diseaseCount, position);
	}

	// Token: 0x060019D1 RID: 6609 RVA: 0x0008A87E File Offset: 0x00088A7E
	public SubstanceChunk CreateChunk(Element element, float mass, float temperature, byte diseaseIdx, int diseaseCount, Vector3 position)
	{
		return GeneratedOre.CreateChunk(element, mass, temperature, diseaseIdx, diseaseCount, position);
	}

	// Token: 0x04000E7B RID: 3707
	public static GasSourceManager Instance;
}
