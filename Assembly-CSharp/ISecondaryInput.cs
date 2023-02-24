using System;

// Token: 0x0200058F RID: 1423
public interface ISecondaryInput
{
	// Token: 0x060022E9 RID: 8937
	bool HasSecondaryConduitType(ConduitType type);

	// Token: 0x060022EA RID: 8938
	CellOffset GetSecondaryConduitOffset(ConduitType type);
}
