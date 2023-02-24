using System;
using UnityEngine;

// Token: 0x02000496 RID: 1174
[AddComponentMenu("KMonoBehaviour/scripts/LiquidSourceManager")]
public class LiquidSourceManager : KMonoBehaviour, IChunkManager
{
	// Token: 0x06001A56 RID: 6742 RVA: 0x0008C41F File Offset: 0x0008A61F
	protected override void OnPrefabInit()
	{
		LiquidSourceManager.Instance = this;
	}

	// Token: 0x06001A57 RID: 6743 RVA: 0x0008C427 File Offset: 0x0008A627
	public SubstanceChunk CreateChunk(SimHashes element_id, float mass, float temperature, byte diseaseIdx, int diseaseCount, Vector3 position)
	{
		return this.CreateChunk(ElementLoader.FindElementByHash(element_id), mass, temperature, diseaseIdx, diseaseCount, position);
	}

	// Token: 0x06001A58 RID: 6744 RVA: 0x0008C43D File Offset: 0x0008A63D
	public SubstanceChunk CreateChunk(Element element, float mass, float temperature, byte diseaseIdx, int diseaseCount, Vector3 position)
	{
		return GeneratedOre.CreateChunk(element, mass, temperature, diseaseIdx, diseaseCount, position);
	}

	// Token: 0x04000EA7 RID: 3751
	public static LiquidSourceManager Instance;
}
