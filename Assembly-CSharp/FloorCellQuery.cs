using System;
using System.Collections.Generic;

// Token: 0x020003D3 RID: 979
public class FloorCellQuery : PathFinderQuery
{
	// Token: 0x06001446 RID: 5190 RVA: 0x0006B08A File Offset: 0x0006928A
	public FloorCellQuery Reset(int max_results, int adjacent_cells_buffer = 0)
	{
		this.max_results = max_results;
		this.adjacent_cells_buffer = adjacent_cells_buffer;
		this.result_cells.Clear();
		return this;
	}

	// Token: 0x06001447 RID: 5191 RVA: 0x0006B0A6 File Offset: 0x000692A6
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		if (!this.result_cells.Contains(cell) && this.CheckValidFloorCell(cell))
		{
			this.result_cells.Add(cell);
		}
		return this.result_cells.Count >= this.max_results;
	}

	// Token: 0x06001448 RID: 5192 RVA: 0x0006B0E4 File Offset: 0x000692E4
	private bool CheckValidFloorCell(int testCell)
	{
		if (!Grid.IsValidCell(testCell) || Grid.IsSolidCell(testCell))
		{
			return false;
		}
		int cellInDirection = Grid.GetCellInDirection(testCell, Direction.Up);
		int cellInDirection2 = Grid.GetCellInDirection(testCell, Direction.Down);
		if (!Grid.ObjectLayers[1].ContainsKey(testCell) && Grid.IsValidCell(cellInDirection2) && Grid.IsSolidCell(cellInDirection2) && Grid.IsValidCell(cellInDirection) && !Grid.IsSolidCell(cellInDirection))
		{
			int num = testCell;
			int num2 = testCell;
			for (int i = 0; i < this.adjacent_cells_buffer; i++)
			{
				num = Grid.CellLeft(num);
				num2 = Grid.CellRight(num2);
				if (!Grid.IsValidCell(num) || Grid.IsSolidCell(num))
				{
					return false;
				}
				if (!Grid.IsValidCell(num2) || Grid.IsSolidCell(num2))
				{
					return false;
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x04000B3F RID: 2879
	public List<int> result_cells = new List<int>();

	// Token: 0x04000B40 RID: 2880
	private int max_results;

	// Token: 0x04000B41 RID: 2881
	private int adjacent_cells_buffer;
}
