using System;
using System.Collections.Generic;

namespace ClipperLib
{
	// Token: 0x02000166 RID: 358
	public class MyIntersectNodeSort : IComparer<IntersectNode>
	{
		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002E908 File Offset: 0x0002CB08
		public int Compare(IntersectNode node1, IntersectNode node2)
		{
			long num = node2.Pt.Y - node1.Pt.Y;
			if (num > 0L)
			{
				return 1;
			}
			if (num < 0L)
			{
				return -1;
			}
			return 0;
		}
	}
}
