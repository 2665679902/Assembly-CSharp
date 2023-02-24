using System;
using UnityEngine;

// Token: 0x0200044A RID: 1098
public interface IApproachable
{
	// Token: 0x060017B6 RID: 6070
	CellOffset[] GetOffsets();

	// Token: 0x060017B7 RID: 6071
	int GetCell();

	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x060017B8 RID: 6072
	Transform transform { get; }
}
