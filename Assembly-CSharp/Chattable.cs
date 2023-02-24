using System;
using UnityEngine;

// Token: 0x02000459 RID: 1113
[AddComponentMenu("KMonoBehaviour/scripts/Chattable")]
public class Chattable : KMonoBehaviour, IApproachable
{
	// Token: 0x06001872 RID: 6258 RVA: 0x0008197B File Offset: 0x0007FB7B
	public CellOffset[] GetOffsets()
	{
		return OffsetGroups.Chat;
	}

	// Token: 0x06001873 RID: 6259 RVA: 0x00081982 File Offset: 0x0007FB82
	public int GetCell()
	{
		return Grid.PosToCell(this);
	}
}
