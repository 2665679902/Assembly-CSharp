using System;
using UnityEngine;

// Token: 0x0200079E RID: 1950
public struct GridArea
{
	// Token: 0x170003FB RID: 1019
	// (get) Token: 0x06003701 RID: 14081 RVA: 0x00132515 File Offset: 0x00130715
	public Vector2I Min
	{
		get
		{
			return this.min;
		}
	}

	// Token: 0x170003FC RID: 1020
	// (get) Token: 0x06003702 RID: 14082 RVA: 0x0013251D File Offset: 0x0013071D
	public Vector2I Max
	{
		get
		{
			return this.max;
		}
	}

	// Token: 0x06003703 RID: 14083 RVA: 0x00132528 File Offset: 0x00130728
	public void SetArea(int cell, int width, int height)
	{
		Vector2I vector2I = Grid.CellToXY(cell);
		Vector2I vector2I2 = new Vector2I(vector2I.x + width, vector2I.y + height);
		this.SetExtents(vector2I.x, vector2I.y, vector2I2.x, vector2I2.y);
	}

	// Token: 0x06003704 RID: 14084 RVA: 0x00132574 File Offset: 0x00130774
	public void SetExtents(int min_x, int min_y, int max_x, int max_y)
	{
		this.min.x = Math.Max(min_x, 0);
		this.min.y = Math.Max(min_y, 0);
		this.max.x = Math.Min(max_x, Grid.WidthInCells);
		this.max.y = Math.Min(max_y, Grid.HeightInCells);
		this.MinCell = Grid.XYToCell(this.min.x, this.min.y);
		this.MaxCell = Grid.XYToCell(this.max.x, this.max.y);
	}

	// Token: 0x06003705 RID: 14085 RVA: 0x00132614 File Offset: 0x00130814
	public bool Contains(int cell)
	{
		if (cell >= this.MinCell && cell < this.MaxCell)
		{
			int num = cell % Grid.WidthInCells;
			return num >= this.Min.x && num < this.Max.x;
		}
		return false;
	}

	// Token: 0x06003706 RID: 14086 RVA: 0x0013265B File Offset: 0x0013085B
	public bool Contains(int x, int y)
	{
		return x >= this.min.x && x < this.max.x && y >= this.min.y && y < this.max.y;
	}

	// Token: 0x06003707 RID: 14087 RVA: 0x00132698 File Offset: 0x00130898
	public bool Contains(Vector3 pos)
	{
		return (float)this.min.x <= pos.x && pos.x < (float)this.max.x && (float)this.min.y <= pos.y && pos.y <= (float)this.max.y;
	}

	// Token: 0x06003708 RID: 14088 RVA: 0x001326FA File Offset: 0x001308FA
	public void RunIfInside(int cell, Action<int> action)
	{
		if (this.Contains(cell))
		{
			action(cell);
		}
	}

	// Token: 0x06003709 RID: 14089 RVA: 0x0013270C File Offset: 0x0013090C
	public void Run(Action<int> action)
	{
		for (int i = this.min.y; i < this.max.y; i++)
		{
			for (int j = this.min.x; j < this.max.x; j++)
			{
				int num = Grid.XYToCell(j, i);
				action(num);
			}
		}
	}

	// Token: 0x0600370A RID: 14090 RVA: 0x00132768 File Offset: 0x00130968
	public void RunOnDifference(GridArea subtract_area, Action<int> action)
	{
		for (int i = this.min.y; i < this.max.y; i++)
		{
			for (int j = this.min.x; j < this.max.x; j++)
			{
				if (!subtract_area.Contains(j, i))
				{
					int num = Grid.XYToCell(j, i);
					action(num);
				}
			}
		}
	}

	// Token: 0x0600370B RID: 14091 RVA: 0x001327CF File Offset: 0x001309CF
	public int GetCellCount()
	{
		return (this.max.x - this.min.x) * (this.max.y - this.min.y);
	}

	// Token: 0x040024EF RID: 9455
	private Vector2I min;

	// Token: 0x040024F0 RID: 9456
	private Vector2I max;

	// Token: 0x040024F1 RID: 9457
	private int MinCell;

	// Token: 0x040024F2 RID: 9458
	private int MaxCell;
}
