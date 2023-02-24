using System;

// Token: 0x020003D9 RID: 985
public class SafetyQuery : PathFinderQuery
{
	// Token: 0x0600145D RID: 5213 RVA: 0x0006B927 File Offset: 0x00069B27
	public SafetyQuery(SafetyChecker checker, KMonoBehaviour cmp, int max_cost)
	{
		this.checker = checker;
		this.cmp = cmp;
		this.maxCost = max_cost;
	}

	// Token: 0x0600145E RID: 5214 RVA: 0x0006B944 File Offset: 0x00069B44
	public void Reset()
	{
		this.targetCell = PathFinder.InvalidCell;
		this.targetCost = int.MaxValue;
		this.targetConditions = 0;
		this.context = new SafetyChecker.Context(this.cmp);
	}

	// Token: 0x0600145F RID: 5215 RVA: 0x0006B974 File Offset: 0x00069B74
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		bool flag = false;
		int safetyConditions = this.checker.GetSafetyConditions(cell, cost, this.context, out flag);
		if (safetyConditions != 0 && (safetyConditions > this.targetConditions || (safetyConditions == this.targetConditions && cost < this.targetCost)))
		{
			this.targetCell = cell;
			this.targetConditions = safetyConditions;
			this.targetCost = cost;
			if (flag)
			{
				return true;
			}
		}
		return cost >= this.maxCost;
	}

	// Token: 0x06001460 RID: 5216 RVA: 0x0006B9DD File Offset: 0x00069BDD
	public override int GetResultCell()
	{
		return this.targetCell;
	}

	// Token: 0x04000B60 RID: 2912
	private int targetCell;

	// Token: 0x04000B61 RID: 2913
	private int targetCost;

	// Token: 0x04000B62 RID: 2914
	private int targetConditions;

	// Token: 0x04000B63 RID: 2915
	private int maxCost;

	// Token: 0x04000B64 RID: 2916
	private SafetyChecker checker;

	// Token: 0x04000B65 RID: 2917
	private KMonoBehaviour cmp;

	// Token: 0x04000B66 RID: 2918
	private SafetyChecker.Context context;
}
