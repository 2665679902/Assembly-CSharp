using System;

// Token: 0x020009B9 RID: 2489
public interface IUtilityItem
{
	// Token: 0x1700057A RID: 1402
	// (get) Token: 0x060049F0 RID: 18928
	// (set) Token: 0x060049F1 RID: 18929
	UtilityConnections Connections { get; set; }

	// Token: 0x060049F2 RID: 18930
	void UpdateConnections(UtilityConnections Connections);

	// Token: 0x060049F3 RID: 18931
	int GetNetworkID();

	// Token: 0x060049F4 RID: 18932
	UtilityNetwork GetNetworkForDirection(Direction d);
}
