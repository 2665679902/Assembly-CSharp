using System;

// Token: 0x02000683 RID: 1667
public interface ICircuitConnected
{
	// Token: 0x17000320 RID: 800
	// (get) Token: 0x06002CE2 RID: 11490
	bool IsVirtual { get; }

	// Token: 0x17000321 RID: 801
	// (get) Token: 0x06002CE3 RID: 11491
	int PowerCell { get; }

	// Token: 0x17000322 RID: 802
	// (get) Token: 0x06002CE4 RID: 11492
	object VirtualCircuitKey { get; }
}
