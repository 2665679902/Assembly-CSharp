using System;

// Token: 0x020006A9 RID: 1705
public interface IConduitDispenser
{
	// Token: 0x17000344 RID: 836
	// (get) Token: 0x06002E45 RID: 11845
	Storage Storage { get; }

	// Token: 0x17000345 RID: 837
	// (get) Token: 0x06002E46 RID: 11846
	ConduitType ConduitType { get; }
}
