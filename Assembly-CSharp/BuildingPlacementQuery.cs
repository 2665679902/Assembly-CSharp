using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003CD RID: 973
public class BuildingPlacementQuery : PathFinderQuery
{
	// Token: 0x06001432 RID: 5170 RVA: 0x0006ADCC File Offset: 0x00068FCC
	public BuildingPlacementQuery Reset(int max_results, GameObject toPlace)
	{
		this.max_results = max_results;
		this.toPlace = toPlace;
		this.cellOffsets = toPlace.GetComponent<OccupyArea>().OccupiedCellsOffsets;
		this.result_cells.Clear();
		return this;
	}

	// Token: 0x06001433 RID: 5171 RVA: 0x0006ADF9 File Offset: 0x00068FF9
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		if (!this.result_cells.Contains(cell) && this.CheckValidPlaceCell(cell))
		{
			this.result_cells.Add(cell);
		}
		return this.result_cells.Count >= this.max_results;
	}

	// Token: 0x06001434 RID: 5172 RVA: 0x0006AE34 File Offset: 0x00069034
	private bool CheckValidPlaceCell(int testCell)
	{
		if (!Grid.IsValidCell(testCell) || Grid.IsSolidCell(testCell) || Grid.ObjectLayers[1].ContainsKey(testCell))
		{
			return false;
		}
		bool flag = true;
		int widthInCells = this.toPlace.GetComponent<OccupyArea>().GetWidthInCells();
		int num = testCell;
		for (int i = 0; i < widthInCells; i++)
		{
			int cellInDirection = Grid.GetCellInDirection(num, Direction.Down);
			if (!Grid.IsValidCell(cellInDirection) || !Grid.IsSolidCell(cellInDirection))
			{
				flag = false;
				break;
			}
			num = Grid.GetCellInDirection(num, Direction.Right);
		}
		if (flag)
		{
			for (int j = 0; j < this.cellOffsets.Length; j++)
			{
				CellOffset cellOffset = this.cellOffsets[j];
				int num2 = Grid.OffsetCell(testCell, cellOffset);
				if (!Grid.IsValidCell(num2) || Grid.IsSolidCell(num2) || !Grid.IsValidBuildingCell(num2) || Grid.ObjectLayers[1].ContainsKey(num2))
				{
					flag = false;
					break;
				}
			}
		}
		return flag;
	}

	// Token: 0x04000B36 RID: 2870
	public List<int> result_cells = new List<int>();

	// Token: 0x04000B37 RID: 2871
	private int max_results;

	// Token: 0x04000B38 RID: 2872
	private GameObject toPlace;

	// Token: 0x04000B39 RID: 2873
	private CellOffset[] cellOffsets;
}
