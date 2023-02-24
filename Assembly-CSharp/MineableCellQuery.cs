using System;
using System.Collections.Generic;

// Token: 0x020003D5 RID: 981
public class MineableCellQuery : PathFinderQuery
{
	// Token: 0x0600144E RID: 5198 RVA: 0x0006B219 File Offset: 0x00069419
	public MineableCellQuery Reset(Tag element, int max_results)
	{
		this.element = element;
		this.max_results = max_results;
		this.result_cells.Clear();
		return this;
	}

	// Token: 0x0600144F RID: 5199 RVA: 0x0006B238 File Offset: 0x00069438
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		if (!this.result_cells.Contains(cell) && this.CheckValidMineCell(this.element, cell))
		{
			this.result_cells.Add(cell);
		}
		return this.result_cells.Count >= this.max_results;
	}

	// Token: 0x06001450 RID: 5200 RVA: 0x0006B284 File Offset: 0x00069484
	private bool CheckValidMineCell(Tag element, int testCell)
	{
		if (!Grid.IsValidCell(testCell))
		{
			return false;
		}
		foreach (Direction direction in MineableCellQuery.DIRECTION_CHECKS)
		{
			int cellInDirection = Grid.GetCellInDirection(testCell, direction);
			if (Grid.IsValidCell(cellInDirection) && Grid.IsSolidCell(cellInDirection) && !Grid.Foundation[cellInDirection] && Grid.Element[cellInDirection].tag == element)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000B45 RID: 2885
	public List<int> result_cells = new List<int>();

	// Token: 0x04000B46 RID: 2886
	private Tag element;

	// Token: 0x04000B47 RID: 2887
	private int max_results;

	// Token: 0x04000B48 RID: 2888
	public static List<Direction> DIRECTION_CHECKS = new List<Direction>
	{
		Direction.Down,
		Direction.Right,
		Direction.Left,
		Direction.Up
	};
}
