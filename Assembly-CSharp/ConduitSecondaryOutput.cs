using System;
using UnityEngine;

// Token: 0x02000594 RID: 1428
[AddComponentMenu("KMonoBehaviour/scripts/ConduitSecondaryOutput")]
public class ConduitSecondaryOutput : KMonoBehaviour, ISecondaryOutput
{
	// Token: 0x0600230F RID: 8975 RVA: 0x000BE140 File Offset: 0x000BC340
	public bool HasSecondaryConduitType(ConduitType type)
	{
		return this.portInfo.conduitType == type;
	}

	// Token: 0x06002310 RID: 8976 RVA: 0x000BE150 File Offset: 0x000BC350
	public CellOffset GetSecondaryConduitOffset(ConduitType type)
	{
		if (type == this.portInfo.conduitType)
		{
			return this.portInfo.offset;
		}
		return CellOffset.none;
	}

	// Token: 0x0400142E RID: 5166
	[SerializeField]
	public ConduitPortInfo portInfo;
}
