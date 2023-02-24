using System;

// Token: 0x020004F5 RID: 1269
public struct DataPoint
{
	// Token: 0x06001DE7 RID: 7655 RVA: 0x0009F487 File Offset: 0x0009D687
	public DataPoint(float start, float end, float value)
	{
		this.periodStart = start;
		this.periodEnd = end;
		this.periodValue = value;
	}

	// Token: 0x040010BD RID: 4285
	public float periodStart;

	// Token: 0x040010BE RID: 4286
	public float periodEnd;

	// Token: 0x040010BF RID: 4287
	public float periodValue;
}
