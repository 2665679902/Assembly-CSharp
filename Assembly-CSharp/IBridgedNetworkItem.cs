using System;
using System.Collections.Generic;

// Token: 0x02000590 RID: 1424
public interface IBridgedNetworkItem
{
	// Token: 0x060022EB RID: 8939
	void AddNetworks(ICollection<UtilityNetwork> networks);

	// Token: 0x060022EC RID: 8940
	bool IsConnectedToNetworks(ICollection<UtilityNetwork> networks);

	// Token: 0x060022ED RID: 8941
	int GetNetworkCell();
}
