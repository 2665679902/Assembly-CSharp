using System;

// Token: 0x020003D1 RID: 977
public class CellQuery : PathFinderQuery
{
	// Token: 0x06001440 RID: 5184 RVA: 0x0006AFFD File Offset: 0x000691FD
	public CellQuery Reset(int target_cell)
	{
		this.targetCell = target_cell;
		return this;
	}

	// Token: 0x06001441 RID: 5185 RVA: 0x0006B007 File Offset: 0x00069207
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		return cell == this.targetCell;
	}

	// Token: 0x04000B3E RID: 2878
	private int targetCell;
}
