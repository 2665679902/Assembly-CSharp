using System;

// Token: 0x0200058E RID: 1422
public interface ISecondaryOutput
{
	// Token: 0x060022E7 RID: 8935
	bool HasSecondaryConduitType(ConduitType type);

	// Token: 0x060022E8 RID: 8936
	CellOffset GetSecondaryConduitOffset(ConduitType type);
}
