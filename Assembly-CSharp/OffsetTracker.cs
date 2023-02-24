using System;
using UnityEngine;

// Token: 0x02000876 RID: 2166
public class OffsetTracker
{
	// Token: 0x06003E3E RID: 15934 RVA: 0x0015C514 File Offset: 0x0015A714
	public virtual CellOffset[] GetOffsets(int current_cell)
	{
		if (current_cell != this.previousCell)
		{
			global::Debug.Assert(!OffsetTracker.isExecutingWithinJob, "OffsetTracker.GetOffsets() is making a mutating call but is currently executing within a job");
			this.UpdateCell(this.previousCell, current_cell);
			this.previousCell = current_cell;
		}
		if (this.offsets == null)
		{
			global::Debug.Assert(!OffsetTracker.isExecutingWithinJob, "OffsetTracker.GetOffsets() is making a mutating call but is currently executing within a job");
			this.UpdateOffsets(this.previousCell);
		}
		return this.offsets;
	}

	// Token: 0x06003E3F RID: 15935 RVA: 0x0015C57C File Offset: 0x0015A77C
	public void ForceRefresh()
	{
		int num = this.previousCell;
		this.previousCell = Grid.InvalidCell;
		this.Refresh(num);
	}

	// Token: 0x06003E40 RID: 15936 RVA: 0x0015C5A2 File Offset: 0x0015A7A2
	public void Refresh(int cell)
	{
		this.GetOffsets(cell);
	}

	// Token: 0x06003E41 RID: 15937 RVA: 0x0015C5AC File Offset: 0x0015A7AC
	protected virtual void UpdateCell(int previous_cell, int current_cell)
	{
	}

	// Token: 0x06003E42 RID: 15938 RVA: 0x0015C5AE File Offset: 0x0015A7AE
	protected virtual void UpdateOffsets(int current_cell)
	{
	}

	// Token: 0x06003E43 RID: 15939 RVA: 0x0015C5B0 File Offset: 0x0015A7B0
	public virtual void Clear()
	{
	}

	// Token: 0x06003E44 RID: 15940 RVA: 0x0015C5B2 File Offset: 0x0015A7B2
	public virtual void DebugDrawExtents()
	{
	}

	// Token: 0x06003E45 RID: 15941 RVA: 0x0015C5B4 File Offset: 0x0015A7B4
	public virtual void DebugDrawEditor()
	{
	}

	// Token: 0x06003E46 RID: 15942 RVA: 0x0015C5B8 File Offset: 0x0015A7B8
	public virtual void DebugDrawOffsets(int cell)
	{
		foreach (CellOffset cellOffset in this.GetOffsets(cell))
		{
			int num = Grid.OffsetCell(cell, cellOffset);
			Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
			Gizmos.DrawWireCube(Grid.CellToPosCCC(num, Grid.SceneLayer.Move), new Vector3(0.95f, 0.95f, 0.95f));
		}
	}

	// Token: 0x040028C1 RID: 10433
	public static bool isExecutingWithinJob;

	// Token: 0x040028C2 RID: 10434
	protected CellOffset[] offsets;

	// Token: 0x040028C3 RID: 10435
	protected int previousCell = Grid.InvalidCell;
}
