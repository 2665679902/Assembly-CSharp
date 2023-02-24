using System;

// Token: 0x020003C9 RID: 969
public class PathFinderQuery
{
	// Token: 0x06001413 RID: 5139 RVA: 0x0006A6AE File Offset: 0x000688AE
	public virtual bool IsMatch(int cell, int parent_cell, int cost)
	{
		return true;
	}

	// Token: 0x06001414 RID: 5140 RVA: 0x0006A6B1 File Offset: 0x000688B1
	public void SetResult(int cell, int cost, NavType nav_type)
	{
		this.resultCell = cell;
		this.resultNavType = nav_type;
	}

	// Token: 0x06001415 RID: 5141 RVA: 0x0006A6C1 File Offset: 0x000688C1
	public void ClearResult()
	{
		this.resultCell = -1;
	}

	// Token: 0x06001416 RID: 5142 RVA: 0x0006A6CA File Offset: 0x000688CA
	public virtual int GetResultCell()
	{
		return this.resultCell;
	}

	// Token: 0x06001417 RID: 5143 RVA: 0x0006A6D2 File Offset: 0x000688D2
	public NavType GetResultNavType()
	{
		return this.resultNavType;
	}

	// Token: 0x04000B1A RID: 2842
	protected int resultCell;

	// Token: 0x04000B1B RID: 2843
	private NavType resultNavType;
}
