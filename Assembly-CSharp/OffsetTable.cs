using System;
using System.Collections.Generic;

// Token: 0x02000874 RID: 2164
public static class OffsetTable
{
	// Token: 0x06003E34 RID: 15924 RVA: 0x0015C1DC File Offset: 0x0015A3DC
	public static CellOffset[][] Mirror(CellOffset[][] table)
	{
		List<CellOffset[]> list = new List<CellOffset[]>();
		foreach (CellOffset[] array in table)
		{
			list.Add(array);
			if (array[0].x != 0)
			{
				CellOffset[] array2 = new CellOffset[array.Length];
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = array[j];
					array2[j].x = -array2[j].x;
				}
				list.Add(array2);
			}
		}
		return list.ToArray();
	}
}
