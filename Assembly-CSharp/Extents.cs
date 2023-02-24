using System;

// Token: 0x02000903 RID: 2307
public struct Extents
{
	// Token: 0x06004316 RID: 17174 RVA: 0x0017B404 File Offset: 0x00179604
	public static Extents OneCell(int cell)
	{
		int num;
		int num2;
		Grid.CellToXY(cell, out num, out num2);
		return new Extents(num, num2, 1, 1);
	}

	// Token: 0x06004317 RID: 17175 RVA: 0x0017B424 File Offset: 0x00179624
	public Extents(int x, int y, int width, int height)
	{
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}

	// Token: 0x06004318 RID: 17176 RVA: 0x0017B444 File Offset: 0x00179644
	public Extents(int cell, int radius)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		this.x = num - radius;
		this.y = num2 - radius;
		this.width = radius * 2 + 1;
		this.height = radius * 2 + 1;
	}

	// Token: 0x06004319 RID: 17177 RVA: 0x0017B487 File Offset: 0x00179687
	public Extents(int center_x, int center_y, int radius)
	{
		this.x = center_x - radius;
		this.y = center_y - radius;
		this.width = radius * 2 + 1;
		this.height = radius * 2 + 1;
	}

	// Token: 0x0600431A RID: 17178 RVA: 0x0017B4B4 File Offset: 0x001796B4
	public Extents(int cell, CellOffset[] offsets)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		int num3 = num;
		int num4 = num2;
		foreach (CellOffset cellOffset in offsets)
		{
			int num5 = 0;
			int num6 = 0;
			Grid.CellToXY(Grid.OffsetCell(cell, cellOffset), out num5, out num6);
			num = Math.Min(num, num5);
			num2 = Math.Min(num2, num6);
			num3 = Math.Max(num3, num5);
			num4 = Math.Max(num4, num6);
		}
		this.x = num;
		this.y = num2;
		this.width = num3 - num + 1;
		this.height = num4 - num2 + 1;
	}

	// Token: 0x0600431B RID: 17179 RVA: 0x0017B550 File Offset: 0x00179750
	public Extents(int cell, CellOffset[] offsets, Orientation orientation)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		int num3 = num;
		int num4 = num2;
		for (int i = 0; i < offsets.Length; i++)
		{
			CellOffset rotatedCellOffset = Rotatable.GetRotatedCellOffset(offsets[i], orientation);
			int num5 = 0;
			int num6 = 0;
			Grid.CellToXY(Grid.OffsetCell(cell, rotatedCellOffset), out num5, out num6);
			num = Math.Min(num, num5);
			num2 = Math.Min(num2, num6);
			num3 = Math.Max(num3, num5);
			num4 = Math.Max(num4, num6);
		}
		this.x = num;
		this.y = num2;
		this.width = num3 - num + 1;
		this.height = num4 - num2 + 1;
	}

	// Token: 0x0600431C RID: 17180 RVA: 0x0017B5F0 File Offset: 0x001797F0
	public Extents(int cell, CellOffset[][] offset_table)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		int num3 = num;
		int num4 = num2;
		foreach (CellOffset[] array in offset_table)
		{
			int num5 = 0;
			int num6 = 0;
			Grid.CellToXY(Grid.OffsetCell(cell, array[0]), out num5, out num6);
			num = Math.Min(num, num5);
			num2 = Math.Min(num2, num6);
			num3 = Math.Max(num3, num5);
			num4 = Math.Max(num4, num6);
		}
		this.x = num;
		this.y = num2;
		this.width = num3 - num + 1;
		this.height = num4 - num2 + 1;
	}

	// Token: 0x0600431D RID: 17181 RVA: 0x0017B68C File Offset: 0x0017988C
	public bool Contains(Vector2I pos)
	{
		return this.x <= pos.x && pos.x < this.x + this.width && this.y <= pos.y && pos.y < this.y + this.height;
	}

	// Token: 0x04002CBB RID: 11451
	public int x;

	// Token: 0x04002CBC RID: 11452
	public int y;

	// Token: 0x04002CBD RID: 11453
	public int width;

	// Token: 0x04002CBE RID: 11454
	public int height;
}
