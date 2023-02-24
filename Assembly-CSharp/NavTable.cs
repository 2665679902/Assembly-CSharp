using System;

// Token: 0x020003C2 RID: 962
public class NavTable
{
	// Token: 0x060013F2 RID: 5106 RVA: 0x00069AF4 File Offset: 0x00067CF4
	public NavTable(int cell_count)
	{
		this.ValidCells = new short[cell_count];
		this.NavTypeMasks = new short[11];
		for (short num = 0; num < 11; num += 1)
		{
			this.NavTypeMasks[(int)num] = (short)(1 << (int)num);
		}
	}

	// Token: 0x060013F3 RID: 5107 RVA: 0x00069B3D File Offset: 0x00067D3D
	public bool IsValid(int cell, NavType nav_type = NavType.Floor)
	{
		return Grid.IsValidCell(cell) && (this.NavTypeMasks[(int)nav_type] & this.ValidCells[cell]) != 0;
	}

	// Token: 0x060013F4 RID: 5108 RVA: 0x00069B60 File Offset: 0x00067D60
	public void SetValid(int cell, NavType nav_type, bool is_valid)
	{
		short num = this.NavTypeMasks[(int)nav_type];
		short num2 = this.ValidCells[cell];
		if ((num2 & num) != 0 != is_valid)
		{
			if (is_valid)
			{
				this.ValidCells[cell] = num | num2;
			}
			else
			{
				this.ValidCells[cell] = ~num & num2;
			}
			if (this.OnValidCellChanged != null)
			{
				this.OnValidCellChanged(cell, nav_type);
			}
		}
	}

	// Token: 0x04000AF5 RID: 2805
	public Action<int, NavType> OnValidCellChanged;

	// Token: 0x04000AF6 RID: 2806
	private short[] NavTypeMasks;

	// Token: 0x04000AF7 RID: 2807
	private short[] ValidCells;
}
