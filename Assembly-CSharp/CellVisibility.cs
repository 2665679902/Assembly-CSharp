using System;

// Token: 0x0200067E RID: 1662
public class CellVisibility
{
	// Token: 0x06002CD0 RID: 11472 RVA: 0x000EB25C File Offset: 0x000E945C
	public CellVisibility()
	{
		Grid.GetVisibleExtents(out this.MinX, out this.MinY, out this.MaxX, out this.MaxY);
	}

	// Token: 0x06002CD1 RID: 11473 RVA: 0x000EB284 File Offset: 0x000E9484
	public bool IsVisible(int cell)
	{
		int num = Grid.CellColumn(cell);
		if (num < this.MinX || num > this.MaxX)
		{
			return false;
		}
		int num2 = Grid.CellRow(cell);
		return num2 >= this.MinY && num2 <= this.MaxY;
	}

	// Token: 0x04001ABF RID: 6847
	private int MinX;

	// Token: 0x04001AC0 RID: 6848
	private int MinY;

	// Token: 0x04001AC1 RID: 6849
	private int MaxX;

	// Token: 0x04001AC2 RID: 6850
	private int MaxY;
}
