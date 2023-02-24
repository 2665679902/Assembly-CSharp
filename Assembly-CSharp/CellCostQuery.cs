using System;

// Token: 0x020003CF RID: 975
public class CellCostQuery : PathFinderQuery
{
	// Token: 0x17000085 RID: 133
	// (get) Token: 0x06001439 RID: 5177 RVA: 0x0006AF66 File Offset: 0x00069166
	// (set) Token: 0x0600143A RID: 5178 RVA: 0x0006AF6E File Offset: 0x0006916E
	public int resultCost { get; private set; }

	// Token: 0x0600143B RID: 5179 RVA: 0x0006AF77 File Offset: 0x00069177
	public void Reset(int target_cell, int max_cost)
	{
		this.targetCell = target_cell;
		this.maxCost = max_cost;
		this.resultCost = -1;
	}

	// Token: 0x0600143C RID: 5180 RVA: 0x0006AF8E File Offset: 0x0006918E
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		if (cost > this.maxCost)
		{
			return true;
		}
		if (cell == this.targetCell)
		{
			this.resultCost = cost;
			return true;
		}
		return false;
	}

	// Token: 0x04000B3B RID: 2875
	private int targetCell;

	// Token: 0x04000B3C RID: 2876
	private int maxCost;
}
