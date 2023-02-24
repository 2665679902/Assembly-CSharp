﻿using System;
using System.Collections.Generic;

// Token: 0x02000873 RID: 2163
public static class OffsetGroups
{
	// Token: 0x06003E31 RID: 15921 RVA: 0x0015AD74 File Offset: 0x00158F74
	public static CellOffset[] InitGrid(int x0, int x1, int y0, int y1)
	{
		List<CellOffset> list = new List<CellOffset>();
		for (int i = y0; i <= y1; i++)
		{
			for (int j = x0; j <= x1; j++)
			{
				list.Add(new CellOffset(j, i));
			}
		}
		CellOffset[] array = list.ToArray();
		Array.Sort<CellOffset>(array, 0, array.Length, new OffsetGroups.CellOffsetComparer());
		return array;
	}

	// Token: 0x06003E32 RID: 15922 RVA: 0x0015ADC4 File Offset: 0x00158FC4
	public static CellOffset[][] BuildReachabilityTable(CellOffset[] area_offsets, CellOffset[][] table, CellOffset[] filter)
	{
		Dictionary<CellOffset[][], Dictionary<CellOffset[], CellOffset[][]>> dictionary = null;
		Dictionary<CellOffset[], CellOffset[][]> dictionary2 = null;
		CellOffset[][] array = null;
		if (OffsetGroups.reachabilityTableCache.TryGetValue(area_offsets, out dictionary) && dictionary.TryGetValue(table, out dictionary2) && dictionary2.TryGetValue((filter == null) ? OffsetGroups.nullFilter : filter, out array))
		{
			return array;
		}
		HashSet<CellOffset> hashSet = new HashSet<CellOffset>();
		foreach (CellOffset cellOffset in area_offsets)
		{
			foreach (CellOffset[] array2 in table)
			{
				if (filter == null || Array.IndexOf<CellOffset>(filter, array2[0]) == -1)
				{
					CellOffset cellOffset2 = cellOffset + array2[0];
					hashSet.Add(cellOffset2);
				}
			}
		}
		List<CellOffset[]> list = new List<CellOffset[]>();
		foreach (CellOffset cellOffset3 in hashSet)
		{
			CellOffset cellOffset4 = area_offsets[0];
			foreach (CellOffset cellOffset5 in area_offsets)
			{
				if ((cellOffset3 - cellOffset4).GetOffsetDistance() > (cellOffset3 - cellOffset5).GetOffsetDistance())
				{
					cellOffset4 = cellOffset5;
				}
			}
			foreach (CellOffset[] array3 in table)
			{
				if ((filter == null || Array.IndexOf<CellOffset>(filter, array3[0]) == -1) && array3[0] + cellOffset4 == cellOffset3)
				{
					CellOffset[] array4 = new CellOffset[array3.Length];
					for (int k = 0; k < array3.Length; k++)
					{
						array4[k] = array3[k] + cellOffset4;
					}
					list.Add(array4);
				}
			}
		}
		array = list.ToArray();
		Array.Sort<CellOffset[]>(array, (CellOffset[] x, CellOffset[] y) => x[0].GetOffsetDistance().CompareTo(y[0].GetOffsetDistance()));
		if (dictionary == null)
		{
			dictionary = new Dictionary<CellOffset[][], Dictionary<CellOffset[], CellOffset[][]>>();
			OffsetGroups.reachabilityTableCache.Add(area_offsets, dictionary);
		}
		if (dictionary2 == null)
		{
			dictionary2 = new Dictionary<CellOffset[], CellOffset[][]>();
			dictionary.Add(table, dictionary2);
		}
		dictionary2.Add((filter == null) ? OffsetGroups.nullFilter : filter, array);
		return array;
	}

	// Token: 0x040028AF RID: 10415
	public static CellOffset[] Use = new CellOffset[1];

	// Token: 0x040028B0 RID: 10416
	public static CellOffset[] Chat = new CellOffset[]
	{
		new CellOffset(1, 0),
		new CellOffset(-1, 0),
		new CellOffset(1, 1),
		new CellOffset(1, -1),
		new CellOffset(-1, 1),
		new CellOffset(-1, -1)
	};

	// Token: 0x040028B1 RID: 10417
	public static CellOffset[] LeftOnly = new CellOffset[]
	{
		new CellOffset(-1, 0)
	};

	// Token: 0x040028B2 RID: 10418
	public static CellOffset[] RightOnly = new CellOffset[]
	{
		new CellOffset(1, 0)
	};

	// Token: 0x040028B3 RID: 10419
	public static CellOffset[] LeftOrRight = new CellOffset[]
	{
		new CellOffset(-1, 0),
		new CellOffset(1, 0)
	};

	// Token: 0x040028B4 RID: 10420
	public static CellOffset[] Standard = OffsetGroups.InitGrid(-2, 2, -3, 3);

	// Token: 0x040028B5 RID: 10421
	public static CellOffset[] LiquidSource = new CellOffset[]
	{
		new CellOffset(0, 0),
		new CellOffset(1, 0),
		new CellOffset(-1, 0),
		new CellOffset(0, 1),
		new CellOffset(0, -1),
		new CellOffset(1, 1),
		new CellOffset(1, -1),
		new CellOffset(-1, 1),
		new CellOffset(-1, -1),
		new CellOffset(2, 0),
		new CellOffset(-2, 0)
	};

	// Token: 0x040028B6 RID: 10422
	public static CellOffset[][] InvertedStandardTable = OffsetTable.Mirror(new CellOffset[][]
	{
		new CellOffset[]
		{
			new CellOffset(0, 0)
		},
		new CellOffset[]
		{
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(0, 2),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(0, 3),
			new CellOffset(0, 2),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(0, -1)
		},
		new CellOffset[]
		{
			new CellOffset(0, -2)
		},
		new CellOffset[]
		{
			new CellOffset(0, -3),
			new CellOffset(0, -2),
			new CellOffset(0, -1)
		},
		new CellOffset[]
		{
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(1, 1),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(1, 1),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(1, 2),
			new CellOffset(1, 1),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(1, 2),
			new CellOffset(0, 2),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(1, 3),
			new CellOffset(1, 2),
			new CellOffset(1, 1),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(1, 3),
			new CellOffset(0, 3),
			new CellOffset(0, 2),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(1, -1)
		},
		new CellOffset[]
		{
			new CellOffset(1, -2),
			new CellOffset(1, -1),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(1, -2),
			new CellOffset(1, -1),
			new CellOffset(0, -1)
		},
		new CellOffset[]
		{
			new CellOffset(1, -3),
			new CellOffset(1, -2),
			new CellOffset(1, -1),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(1, -3),
			new CellOffset(1, -2),
			new CellOffset(0, -2),
			new CellOffset(0, -1)
		},
		new CellOffset[]
		{
			new CellOffset(2, 0),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(2, 1),
			new CellOffset(1, 1),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(2, 1),
			new CellOffset(1, 1),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(2, 2),
			new CellOffset(1, 2),
			new CellOffset(1, 1),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(2, 2),
			new CellOffset(1, 2),
			new CellOffset(1, 1),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(2, 3),
			new CellOffset(1, 3),
			new CellOffset(1, 2),
			new CellOffset(1, 1),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(2, -1),
			new CellOffset(2, 0),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(2, -2),
			new CellOffset(2, -1),
			new CellOffset(1, -1),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(2, -3),
			new CellOffset(1, -2),
			new CellOffset(1, -1),
			new CellOffset(1, 0)
		}
	});

	// Token: 0x040028B7 RID: 10423
	public static CellOffset[][] InvertedStandardTableWithCorners = OffsetTable.Mirror(new CellOffset[][]
	{
		new CellOffset[]
		{
			new CellOffset(0, 0)
		},
		new CellOffset[]
		{
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(0, 2),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(0, 3),
			new CellOffset(0, 2),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(0, -1)
		},
		new CellOffset[]
		{
			new CellOffset(0, -2)
		},
		new CellOffset[]
		{
			new CellOffset(0, -3),
			new CellOffset(0, -2),
			new CellOffset(0, -1)
		},
		new CellOffset[]
		{
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(1, 1)
		},
		new CellOffset[]
		{
			new CellOffset(1, 2),
			new CellOffset(1, 1)
		},
		new CellOffset[]
		{
			new CellOffset(1, 2),
			new CellOffset(0, 2),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(1, 3),
			new CellOffset(1, 2),
			new CellOffset(1, 1)
		},
		new CellOffset[]
		{
			new CellOffset(1, 3),
			new CellOffset(0, 3),
			new CellOffset(0, 2),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(1, -1)
		},
		new CellOffset[]
		{
			new CellOffset(1, -2),
			new CellOffset(1, -1)
		},
		new CellOffset[]
		{
			new CellOffset(1, -3),
			new CellOffset(1, -2),
			new CellOffset(0, -2),
			new CellOffset(0, -1)
		},
		new CellOffset[]
		{
			new CellOffset(1, -3),
			new CellOffset(1, -2),
			new CellOffset(1, -1)
		},
		new CellOffset[]
		{
			new CellOffset(2, 0),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(2, 1),
			new CellOffset(1, 1)
		},
		new CellOffset[]
		{
			new CellOffset(2, 2),
			new CellOffset(1, 2),
			new CellOffset(1, 1)
		},
		new CellOffset[]
		{
			new CellOffset(2, 3),
			new CellOffset(1, 3),
			new CellOffset(1, 2),
			new CellOffset(1, 1)
		},
		new CellOffset[]
		{
			new CellOffset(2, -1),
			new CellOffset(2, 0),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(2, -2),
			new CellOffset(2, -1),
			new CellOffset(1, -1)
		},
		new CellOffset[]
		{
			new CellOffset(2, -3),
			new CellOffset(1, -2),
			new CellOffset(1, -1)
		}
	});

	// Token: 0x040028B8 RID: 10424
	public static CellOffset[][] InvertedWideTable = OffsetTable.Mirror(new CellOffset[][]
	{
		new CellOffset[]
		{
			new CellOffset(0, 0)
		},
		new CellOffset[]
		{
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(0, 2),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(0, 3),
			new CellOffset(0, 2),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(0, -1)
		},
		new CellOffset[]
		{
			new CellOffset(0, -2)
		},
		new CellOffset[]
		{
			new CellOffset(0, -3),
			new CellOffset(0, -2),
			new CellOffset(0, -1)
		},
		new CellOffset[]
		{
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(1, 1),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(1, 1),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(1, 2),
			new CellOffset(1, 1),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(1, 2),
			new CellOffset(0, 2),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(1, 3),
			new CellOffset(1, 2),
			new CellOffset(1, 1),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(1, 3),
			new CellOffset(0, 3),
			new CellOffset(0, 2),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(1, -1)
		},
		new CellOffset[]
		{
			new CellOffset(1, -2),
			new CellOffset(1, -1),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(1, -2),
			new CellOffset(1, -1),
			new CellOffset(0, -1)
		},
		new CellOffset[]
		{
			new CellOffset(1, -3),
			new CellOffset(1, -2),
			new CellOffset(1, -1),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(1, -3),
			new CellOffset(1, -2),
			new CellOffset(0, -2),
			new CellOffset(0, -1)
		},
		new CellOffset[]
		{
			new CellOffset(2, 0),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(2, 1),
			new CellOffset(1, 1),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(2, 1),
			new CellOffset(1, 1),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(2, 2),
			new CellOffset(1, 2),
			new CellOffset(1, 1),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(2, 2),
			new CellOffset(1, 2),
			new CellOffset(1, 1),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(2, 3),
			new CellOffset(1, 3),
			new CellOffset(1, 2),
			new CellOffset(1, 1),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(2, -1),
			new CellOffset(2, 0),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(2, -2),
			new CellOffset(2, -1),
			new CellOffset(1, -1),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(2, -3),
			new CellOffset(1, -2),
			new CellOffset(1, -1),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(3, 0),
			new CellOffset(2, 0),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(3, 1),
			new CellOffset(2, 1),
			new CellOffset(1, 1),
			new CellOffset(0, 1)
		},
		new CellOffset[]
		{
			new CellOffset(3, 1),
			new CellOffset(2, 1),
			new CellOffset(1, 1),
			new CellOffset(1, 0)
		},
		new CellOffset[]
		{
			new CellOffset(3, -1),
			new CellOffset(2, -1),
			new CellOffset(1, -1),
			new CellOffset(0, -1)
		},
		new CellOffset[]
		{
			new CellOffset(3, -1),
			new CellOffset(2, -1),
			new CellOffset(1, -1),
			new CellOffset(1, 0)
		}
	});

	// Token: 0x040028B9 RID: 10425
	private static Dictionary<CellOffset[], Dictionary<CellOffset[][], Dictionary<CellOffset[], CellOffset[][]>>> reachabilityTableCache = new Dictionary<CellOffset[], Dictionary<CellOffset[][], Dictionary<CellOffset[], CellOffset[][]>>>();

	// Token: 0x040028BA RID: 10426
	private static readonly CellOffset[] nullFilter = new CellOffset[0];

	// Token: 0x02001639 RID: 5689
	private class CellOffsetComparer : IComparer<CellOffset>
	{
		// Token: 0x06008714 RID: 34580 RVA: 0x002F0DDC File Offset: 0x002EEFDC
		public int Compare(CellOffset a, CellOffset b)
		{
			int num = Math.Abs(a.x) + Math.Abs(a.y);
			int num2 = Math.Abs(b.x) + Math.Abs(b.y);
			return num.CompareTo(num2);
		}
	}
}
