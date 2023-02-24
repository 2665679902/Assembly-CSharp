using System;
using STRINGS;
using UnityEngine;

// Token: 0x020009C5 RID: 2501
public class UtilityNetworkTubesManager : UtilityNetworkManager<TravelTubeNetwork, TravelTube>
{
	// Token: 0x06004A5A RID: 19034 RVA: 0x001A08A7 File Offset: 0x0019EAA7
	public UtilityNetworkTubesManager(int game_width, int game_height, int tile_layer)
		: base(game_width, game_height, tile_layer)
	{
	}

	// Token: 0x06004A5B RID: 19035 RVA: 0x001A08B2 File Offset: 0x0019EAB2
	public override bool CanAddConnection(UtilityConnections new_connection, int cell, bool is_physical_building, out string fail_reason)
	{
		return this.TestForUTurnLeft(cell, new_connection, is_physical_building, out fail_reason) && this.TestForUTurnRight(cell, new_connection, is_physical_building, out fail_reason) && this.TestForNoAdjacentBridge(cell, new_connection, out fail_reason);
	}

	// Token: 0x06004A5C RID: 19036 RVA: 0x001A08DA File Offset: 0x0019EADA
	public override void SetConnections(UtilityConnections connections, int cell, bool is_physical_building)
	{
		base.SetConnections(connections, cell, is_physical_building);
		Pathfinding.Instance.AddDirtyNavGridCell(cell);
	}

	// Token: 0x06004A5D RID: 19037 RVA: 0x001A08F0 File Offset: 0x0019EAF0
	private bool TestForUTurnLeft(int first_cell, UtilityConnections first_connection, bool is_physical_building, out string fail_reason)
	{
		int num = first_cell;
		UtilityConnections utilityConnections = first_connection;
		int num2 = 1;
		for (int i = 0; i < 3; i++)
		{
			int num3 = utilityConnections.CellInDirection(num);
			UtilityConnections utilityConnections2 = utilityConnections.LeftDirection();
			if (this.HasConnection(num3, utilityConnections2, is_physical_building))
			{
				num2++;
			}
			num = num3;
			utilityConnections = utilityConnections2;
		}
		fail_reason = UI.TOOLTIPS.HELP_TUBELOCATION_NO_UTURNS;
		return num2 <= 2;
	}

	// Token: 0x06004A5E RID: 19038 RVA: 0x001A094C File Offset: 0x0019EB4C
	private bool TestForUTurnRight(int first_cell, UtilityConnections first_connection, bool is_physical_building, out string fail_reason)
	{
		int num = first_cell;
		UtilityConnections utilityConnections = first_connection;
		int num2 = 1;
		for (int i = 0; i < 3; i++)
		{
			int num3 = utilityConnections.CellInDirection(num);
			UtilityConnections utilityConnections2 = utilityConnections.RightDirection();
			if (this.HasConnection(num3, utilityConnections2, is_physical_building))
			{
				num2++;
			}
			num = num3;
			utilityConnections = utilityConnections2;
		}
		fail_reason = UI.TOOLTIPS.HELP_TUBELOCATION_NO_UTURNS;
		return num2 <= 2;
	}

	// Token: 0x06004A5F RID: 19039 RVA: 0x001A09A8 File Offset: 0x0019EBA8
	private bool TestForNoAdjacentBridge(int cell, UtilityConnections connection, out string fail_reason)
	{
		UtilityConnections utilityConnections = connection.LeftDirection();
		UtilityConnections utilityConnections2 = connection.RightDirection();
		int num = utilityConnections.CellInDirection(cell);
		int num2 = utilityConnections2.CellInDirection(cell);
		GameObject gameObject = Grid.Objects[num, 9];
		GameObject gameObject2 = Grid.Objects[num2, 9];
		fail_reason = UI.TOOLTIPS.HELP_TUBELOCATION_STRAIGHT_BRIDGES;
		return (gameObject == null || gameObject.GetComponent<TravelTubeBridge>() == null) && (gameObject2 == null || gameObject2.GetComponent<TravelTubeBridge>() == null);
	}

	// Token: 0x06004A60 RID: 19040 RVA: 0x001A0A2C File Offset: 0x0019EC2C
	private bool HasConnection(int cell, UtilityConnections connection, bool is_physical_building)
	{
		return (base.GetConnections(cell, is_physical_building) & connection) > (UtilityConnections)0;
	}
}
