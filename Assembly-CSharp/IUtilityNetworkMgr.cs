using System;
using System.Collections.Generic;

// Token: 0x020009C1 RID: 2497
public interface IUtilityNetworkMgr
{
	// Token: 0x06004A1B RID: 18971
	bool CanAddConnection(UtilityConnections new_connection, int cell, bool is_physical_building, out string fail_reason);

	// Token: 0x06004A1C RID: 18972
	void AddConnection(UtilityConnections new_connection, int cell, bool is_physical_building);

	// Token: 0x06004A1D RID: 18973
	void StashVisualGrids();

	// Token: 0x06004A1E RID: 18974
	void UnstashVisualGrids();

	// Token: 0x06004A1F RID: 18975
	string GetVisualizerString(int cell);

	// Token: 0x06004A20 RID: 18976
	string GetVisualizerString(UtilityConnections connections);

	// Token: 0x06004A21 RID: 18977
	UtilityConnections GetConnections(int cell, bool is_physical_building);

	// Token: 0x06004A22 RID: 18978
	UtilityConnections GetDisplayConnections(int cell);

	// Token: 0x06004A23 RID: 18979
	void SetConnections(UtilityConnections connections, int cell, bool is_physical_building);

	// Token: 0x06004A24 RID: 18980
	void ClearCell(int cell, bool is_physical_building);

	// Token: 0x06004A25 RID: 18981
	void ForceRebuildNetworks();

	// Token: 0x06004A26 RID: 18982
	void AddToNetworks(int cell, object item, bool is_endpoint);

	// Token: 0x06004A27 RID: 18983
	void RemoveFromNetworks(int cell, object vent, bool is_endpoint);

	// Token: 0x06004A28 RID: 18984
	object GetEndpoint(int cell);

	// Token: 0x06004A29 RID: 18985
	UtilityNetwork GetNetworkForDirection(int cell, Direction direction);

	// Token: 0x06004A2A RID: 18986
	UtilityNetwork GetNetworkForCell(int cell);

	// Token: 0x06004A2B RID: 18987
	void AddNetworksRebuiltListener(Action<IList<UtilityNetwork>, ICollection<int>> listener);

	// Token: 0x06004A2C RID: 18988
	void RemoveNetworksRebuiltListener(Action<IList<UtilityNetwork>, ICollection<int>> listener);

	// Token: 0x06004A2D RID: 18989
	IList<UtilityNetwork> GetNetworks();
}
