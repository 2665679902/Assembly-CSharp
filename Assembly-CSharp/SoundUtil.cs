using System;
using UnityEngine;

// Token: 0x02000931 RID: 2353
public static class SoundUtil
{
	// Token: 0x060044F3 RID: 17651 RVA: 0x00185158 File Offset: 0x00183358
	public static float GetLiquidDepth(int cell)
	{
		float num = 0f;
		num += Grid.Mass[cell] * (Grid.Element[cell].IsLiquid ? 1f : 0f);
		int num2 = Grid.CellBelow(cell);
		if (Grid.IsValidCell(num2))
		{
			num += Grid.Mass[num2] * (Grid.Element[num2].IsLiquid ? 1f : 0f);
		}
		return Mathf.Min(num / 1000f, 1f);
	}

	// Token: 0x060044F4 RID: 17652 RVA: 0x001851DD File Offset: 0x001833DD
	public static float GetLiquidVolume(float mass)
	{
		return Mathf.Min(mass / 100f, 1f);
	}
}
