using System;

// Token: 0x0200078F RID: 1935
public interface IEnergyProducer
{
	// Token: 0x170003EB RID: 1003
	// (get) Token: 0x06003610 RID: 13840
	float JoulesAvailable { get; }

	// Token: 0x06003611 RID: 13841
	void ConsumeEnergy(float joules);
}
