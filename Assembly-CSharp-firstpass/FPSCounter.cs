using System;
using UnityEngine;

// Token: 0x02000134 RID: 308
public class FPSCounter
{
	// Token: 0x06000A75 RID: 2677 RVA: 0x00028422 File Offset: 0x00026622
	public static void Create()
	{
		if (FPSCounter.instance == null)
		{
			FPSCounter.instance = new FPSCounter();
			FPSCounter.instance.FPSNextPeriod = Time.realtimeSinceStartup + FPSCounter.FPSMeasurePeriod;
		}
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x0002844C File Offset: 0x0002664C
	public static void Update()
	{
		FPSCounter.Create();
		FPSCounter.instance.FPSAccumulator++;
		if (Time.realtimeSinceStartup > FPSCounter.instance.FPSNextPeriod)
		{
			FPSCounter.currentFPS = (int)((float)FPSCounter.instance.FPSAccumulator / FPSCounter.FPSMeasurePeriod);
			FPSCounter.instance.FPSAccumulator = 0;
			FPSCounter.instance.FPSNextPeriod += FPSCounter.FPSMeasurePeriod;
		}
	}

	// Token: 0x040006DA RID: 1754
	public static float FPSMeasurePeriod = 0.1f;

	// Token: 0x040006DB RID: 1755
	private int FPSAccumulator;

	// Token: 0x040006DC RID: 1756
	private float FPSNextPeriod;

	// Token: 0x040006DD RID: 1757
	public static int currentFPS;

	// Token: 0x040006DE RID: 1758
	private static FPSCounter instance;
}
