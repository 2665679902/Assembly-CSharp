using System;

// Token: 0x02000740 RID: 1856
public interface IEnergyConsumer : ICircuitConnected
{
	// Token: 0x170003AB RID: 939
	// (get) Token: 0x0600330A RID: 13066
	float WattsUsed { get; }

	// Token: 0x170003AC RID: 940
	// (get) Token: 0x0600330B RID: 13067
	float WattsNeededWhenActive { get; }

	// Token: 0x170003AD RID: 941
	// (get) Token: 0x0600330C RID: 13068
	int PowerSortOrder { get; }

	// Token: 0x0600330D RID: 13069
	void SetConnectionStatus(CircuitManager.ConnectionStatus status);

	// Token: 0x170003AE RID: 942
	// (get) Token: 0x0600330E RID: 13070
	string Name { get; }

	// Token: 0x170003AF RID: 943
	// (get) Token: 0x0600330F RID: 13071
	bool IsConnected { get; }

	// Token: 0x170003B0 RID: 944
	// (get) Token: 0x06003310 RID: 13072
	bool IsPowered { get; }
}
