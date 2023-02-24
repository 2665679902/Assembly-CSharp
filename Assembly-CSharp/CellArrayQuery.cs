using System;

// Token: 0x020003CE RID: 974
public class CellArrayQuery : PathFinderQuery
{
	// Token: 0x06001436 RID: 5174 RVA: 0x0006AF25 File Offset: 0x00069125
	public CellArrayQuery Reset(int[] target_cells)
	{
		this.targetCells = target_cells;
		return this;
	}

	// Token: 0x06001437 RID: 5175 RVA: 0x0006AF30 File Offset: 0x00069130
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		for (int i = 0; i < this.targetCells.Length; i++)
		{
			if (this.targetCells[i] == cell)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000B3A RID: 2874
	private int[] targetCells;
}
