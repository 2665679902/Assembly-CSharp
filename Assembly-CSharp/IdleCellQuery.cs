using System;

// Token: 0x020003D4 RID: 980
public class IdleCellQuery : PathFinderQuery
{
	// Token: 0x0600144A RID: 5194 RVA: 0x0006B1A3 File Offset: 0x000693A3
	public IdleCellQuery Reset(MinionBrain brain, int max_cost)
	{
		this.brain = brain;
		this.maxCost = max_cost;
		this.targetCell = Grid.InvalidCell;
		return this;
	}

	// Token: 0x0600144B RID: 5195 RVA: 0x0006B1C0 File Offset: 0x000693C0
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		SafeCellQuery.SafeFlags flags = SafeCellQuery.GetFlags(cell, this.brain, false);
		if ((flags & SafeCellQuery.SafeFlags.IsClear) != (SafeCellQuery.SafeFlags)0 && (flags & SafeCellQuery.SafeFlags.IsNotLadder) != (SafeCellQuery.SafeFlags)0 && (flags & SafeCellQuery.SafeFlags.IsNotTube) != (SafeCellQuery.SafeFlags)0 && (flags & SafeCellQuery.SafeFlags.IsBreathable) != (SafeCellQuery.SafeFlags)0 && (flags & SafeCellQuery.SafeFlags.IsNotLiquid) != (SafeCellQuery.SafeFlags)0)
		{
			this.targetCell = cell;
		}
		return cost > this.maxCost;
	}

	// Token: 0x0600144C RID: 5196 RVA: 0x0006B209 File Offset: 0x00069409
	public override int GetResultCell()
	{
		return this.targetCell;
	}

	// Token: 0x04000B42 RID: 2882
	private MinionBrain brain;

	// Token: 0x04000B43 RID: 2883
	private int targetCell;

	// Token: 0x04000B44 RID: 2884
	private int maxCost;
}
