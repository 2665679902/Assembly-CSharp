using System;

// Token: 0x020007F4 RID: 2036
public class LiquidFetchMask
{
	// Token: 0x06003AB3 RID: 15027 RVA: 0x00145094 File Offset: 0x00143294
	public LiquidFetchMask(CellOffset[][] offset_table)
	{
		for (int i = 0; i < offset_table.Length; i++)
		{
			for (int j = 0; j < offset_table[i].Length; j++)
			{
				this.maxOffset.x = Math.Max(this.maxOffset.x, Math.Abs(offset_table[i][j].x));
				this.maxOffset.y = Math.Max(this.maxOffset.y, Math.Abs(offset_table[i][j].y));
			}
		}
		this.isLiquidAvailable = new bool[Grid.CellCount];
		for (int k = 0; k < Grid.CellCount; k++)
		{
			this.RefreshCell(k);
		}
	}

	// Token: 0x06003AB4 RID: 15028 RVA: 0x00145148 File Offset: 0x00143348
	private void RefreshCell(int cell)
	{
		CellOffset offset = Grid.GetOffset(cell);
		int num = Math.Max(0, offset.y - this.maxOffset.y);
		while (num < Grid.HeightInCells && num < offset.y + this.maxOffset.y)
		{
			int num2 = Math.Max(0, offset.x - this.maxOffset.x);
			while (num2 < Grid.WidthInCells && num2 < offset.x + this.maxOffset.x)
			{
				if (Grid.Element[Grid.XYToCell(num2, num)].IsLiquid)
				{
					this.isLiquidAvailable[cell] = true;
					return;
				}
				num2++;
			}
			num++;
		}
		this.isLiquidAvailable[cell] = false;
	}

	// Token: 0x06003AB5 RID: 15029 RVA: 0x001451FB File Offset: 0x001433FB
	public void MarkDirty(int cell)
	{
		this.RefreshCell(cell);
	}

	// Token: 0x06003AB6 RID: 15030 RVA: 0x00145204 File Offset: 0x00143404
	public bool IsLiquidAvailable(int cell)
	{
		return this.isLiquidAvailable[cell];
	}

	// Token: 0x06003AB7 RID: 15031 RVA: 0x0014520E File Offset: 0x0014340E
	public void Destroy()
	{
		this.isLiquidAvailable = null;
	}

	// Token: 0x04002684 RID: 9860
	private bool[] isLiquidAvailable;

	// Token: 0x04002685 RID: 9861
	private CellOffset maxOffset;
}
