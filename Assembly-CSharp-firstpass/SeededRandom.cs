using System;

// Token: 0x0200012E RID: 302
public class SeededRandom
{
	// Token: 0x17000111 RID: 273
	// (get) Token: 0x06000A5F RID: 2655 RVA: 0x00027DA5 File Offset: 0x00025FA5
	// (set) Token: 0x06000A60 RID: 2656 RVA: 0x00027DAD File Offset: 0x00025FAD
	public int seed { get; private set; }

	// Token: 0x06000A61 RID: 2657 RVA: 0x00027DB6 File Offset: 0x00025FB6
	public SeededRandom(int seed)
	{
		if (seed == -2147483648)
		{
			seed = 0;
		}
		this.seed = seed;
		this.rnd = new KRandom(seed);
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x00027DDC File Offset: 0x00025FDC
	public KRandom RandomSource()
	{
		return this.rnd;
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x00027DE4 File Offset: 0x00025FE4
	public float RandomValue()
	{
		return (float)this.rnd.NextDouble();
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x00027DF2 File Offset: 0x00025FF2
	public double NextDouble()
	{
		return this.rnd.NextDouble();
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x00027E00 File Offset: 0x00026000
	public float RandomRange(float rangeLow, float rangeHigh)
	{
		float num = rangeHigh - rangeLow;
		return rangeLow + (float)(this.rnd.NextDouble() * (double)num);
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x00027E24 File Offset: 0x00026024
	public int RandomRange(int rangeLow, int rangeHigh)
	{
		int num = rangeHigh - rangeLow;
		return rangeLow + (int)(this.rnd.NextDouble() * (double)num);
	}

	// Token: 0x040006D1 RID: 1745
	private KRandom rnd;
}
