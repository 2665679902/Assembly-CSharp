using System;
using UnityEngine;

// Token: 0x0200044B RID: 1099
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Approachable")]
public class Approachable : KMonoBehaviour, IApproachable
{
	// Token: 0x060017B9 RID: 6073 RVA: 0x0007C34D File Offset: 0x0007A54D
	public CellOffset[] GetOffsets()
	{
		return OffsetGroups.Use;
	}

	// Token: 0x060017BA RID: 6074 RVA: 0x0007C354 File Offset: 0x0007A554
	public int GetCell()
	{
		return Grid.PosToCell(this);
	}
}
