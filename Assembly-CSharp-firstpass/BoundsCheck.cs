using System;
using UnityEngine;

// Token: 0x0200013C RID: 316
internal static class BoundsCheck
{
	// Token: 0x06000A98 RID: 2712 RVA: 0x00028728 File Offset: 0x00026928
	public static int Check(Vector2 point, Rect bounds)
	{
		int num = 0;
		if (point.x == bounds.xMin)
		{
			num |= BoundsCheck.LEFT;
		}
		if (point.x == bounds.xMax)
		{
			num |= BoundsCheck.RIGHT;
		}
		if (point.y == bounds.yMin)
		{
			num |= BoundsCheck.TOP;
		}
		if (point.y == bounds.yMax)
		{
			num |= BoundsCheck.BOTTOM;
		}
		return num;
	}

	// Token: 0x040006E5 RID: 1765
	public static readonly int TOP = 1;

	// Token: 0x040006E6 RID: 1766
	public static readonly int BOTTOM = 2;

	// Token: 0x040006E7 RID: 1767
	public static readonly int LEFT = 4;

	// Token: 0x040006E8 RID: 1768
	public static readonly int RIGHT = 8;
}
