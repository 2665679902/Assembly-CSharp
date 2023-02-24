using System;

// Token: 0x020006A6 RID: 1702
public interface IConduitConsumer
{
	// Token: 0x17000336 RID: 822
	// (get) Token: 0x06002E25 RID: 11813
	Storage Storage { get; }

	// Token: 0x17000337 RID: 823
	// (get) Token: 0x06002E26 RID: 11814
	ConduitType ConduitType { get; }
}
