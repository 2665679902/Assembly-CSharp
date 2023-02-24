using System;
using System.Collections.Generic;

// Token: 0x02000132 RID: 306
public static class WorldGenUtil
{
	// Token: 0x06000A73 RID: 2675 RVA: 0x000283D4 File Offset: 0x000265D4
	public static void ShuffleSeeded<T>(this IList<T> list, KRandom rng)
	{
		int i = list.Count;
		while (i > 1)
		{
			i--;
			int num = rng.Next(i + 1);
			T t = list[num];
			list[num] = list[i];
			list[i] = t;
		}
	}
}
