using System;

// Token: 0x020003D0 RID: 976
public class CellOffsetQuery : CellArrayQuery
{
	// Token: 0x0600143E RID: 5182 RVA: 0x0006AFB8 File Offset: 0x000691B8
	public CellArrayQuery Reset(int cell, CellOffset[] offsets)
	{
		int[] array = new int[offsets.Length];
		for (int i = 0; i < offsets.Length; i++)
		{
			array[i] = Grid.OffsetCell(cell, offsets[i]);
		}
		base.Reset(array);
		return this;
	}
}
