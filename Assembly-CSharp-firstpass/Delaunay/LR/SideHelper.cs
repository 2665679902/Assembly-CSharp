using System;

namespace Delaunay.LR
{
	// Token: 0x02000156 RID: 342
	public class SideHelper
	{
		// Token: 0x06000B92 RID: 2962 RVA: 0x0002E290 File Offset: 0x0002C490
		public static Side Other(Side leftRight)
		{
			if (leftRight != Side.LEFT)
			{
				return Side.LEFT;
			}
			return Side.RIGHT;
		}
	}
}
