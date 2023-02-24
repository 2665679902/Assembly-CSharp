using System;

// Token: 0x020007B2 RID: 1970
public interface IFuelTank
{
	// Token: 0x17000414 RID: 1044
	// (get) Token: 0x060037C1 RID: 14273
	IStorage Storage { get; }

	// Token: 0x17000415 RID: 1045
	// (get) Token: 0x060037C2 RID: 14274
	bool ConsumeFuelOnLand { get; }

	// Token: 0x060037C3 RID: 14275
	void DEBUG_FillTank();
}
