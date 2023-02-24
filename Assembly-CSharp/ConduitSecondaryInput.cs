using System;
using UnityEngine;

// Token: 0x02000593 RID: 1427
[AddComponentMenu("KMonoBehaviour/scripts/ConduitSecondaryInput")]
public class ConduitSecondaryInput : KMonoBehaviour, ISecondaryInput
{
	// Token: 0x0600230C RID: 8972 RVA: 0x000BE107 File Offset: 0x000BC307
	public bool HasSecondaryConduitType(ConduitType type)
	{
		return this.portInfo.conduitType == type;
	}

	// Token: 0x0600230D RID: 8973 RVA: 0x000BE117 File Offset: 0x000BC317
	public CellOffset GetSecondaryConduitOffset(ConduitType type)
	{
		if (this.portInfo.conduitType == type)
		{
			return this.portInfo.offset;
		}
		return CellOffset.none;
	}

	// Token: 0x0400142D RID: 5165
	[SerializeField]
	public ConduitPortInfo portInfo;
}
