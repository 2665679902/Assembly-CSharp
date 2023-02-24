using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003DB RID: 987
public class StaterpillarCellQuery : PathFinderQuery
{
	// Token: 0x06001466 RID: 5222 RVA: 0x0006BC30 File Offset: 0x00069E30
	public StaterpillarCellQuery Reset(int max_results, GameObject tester, ObjectLayer conduitLayer)
	{
		this.max_results = max_results;
		this.tester = tester;
		this.result_cells.Clear();
		ObjectLayer objectLayer;
		if (conduitLayer <= ObjectLayer.LiquidConduit)
		{
			if (conduitLayer == ObjectLayer.GasConduit)
			{
				objectLayer = ObjectLayer.GasConduitConnection;
				goto IL_4A;
			}
			if (conduitLayer == ObjectLayer.LiquidConduit)
			{
				objectLayer = ObjectLayer.LiquidConduitConnection;
				goto IL_4A;
			}
		}
		else
		{
			if (conduitLayer == ObjectLayer.SolidConduit)
			{
				objectLayer = ObjectLayer.SolidConduitConnection;
				goto IL_4A;
			}
			if (conduitLayer == ObjectLayer.Wire)
			{
				objectLayer = ObjectLayer.WireConnectors;
				goto IL_4A;
			}
		}
		objectLayer = conduitLayer;
		IL_4A:
		this.connectorLayer = objectLayer;
		return this;
	}

	// Token: 0x06001467 RID: 5223 RVA: 0x0006BC8F File Offset: 0x00069E8F
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		if (!this.result_cells.Contains(cell) && this.CheckValidRoofCell(cell))
		{
			this.result_cells.Add(cell);
		}
		return this.result_cells.Count >= this.max_results;
	}

	// Token: 0x06001468 RID: 5224 RVA: 0x0006BCCC File Offset: 0x00069ECC
	private bool CheckValidRoofCell(int testCell)
	{
		if (!this.tester.GetComponent<Navigator>().NavGrid.NavTable.IsValid(testCell, NavType.Ceiling))
		{
			return false;
		}
		int cellInDirection = Grid.GetCellInDirection(testCell, Direction.Down);
		return !Grid.ObjectLayers[1].ContainsKey(testCell) && !Grid.ObjectLayers[1].ContainsKey(cellInDirection) && !Grid.Objects[cellInDirection, (int)this.connectorLayer] && Grid.IsValidBuildingCell(testCell) && Grid.IsValidCell(cellInDirection) && Grid.IsValidBuildingCell(cellInDirection) && !Grid.IsSolidCell(cellInDirection);
	}

	// Token: 0x04000B6C RID: 2924
	public List<int> result_cells = new List<int>();

	// Token: 0x04000B6D RID: 2925
	private int max_results;

	// Token: 0x04000B6E RID: 2926
	private GameObject tester;

	// Token: 0x04000B6F RID: 2927
	private ObjectLayer connectorLayer;
}
