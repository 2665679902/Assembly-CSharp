using System;
using System.Runtime.InteropServices;

// Token: 0x0200012C RID: 300
[ComVisible(true)]
[Serializable]
public class KRandom
{
	// Token: 0x06000A4E RID: 2638 RVA: 0x0002745F File Offset: 0x0002565F
	public KRandom()
		: this(Environment.TickCount)
	{
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x0002746C File Offset: 0x0002566C
	public KRandom(int Seed)
	{
		int num = ((Seed == int.MinValue) ? int.MaxValue : Math.Abs(Seed));
		int num2 = 161803398 - num;
		this.SeedArray[55] = num2;
		int num3 = 1;
		for (int i = 1; i < 55; i++)
		{
			int num4 = 21 * i % 55;
			this.SeedArray[num4] = num3;
			num3 = num2 - num3;
			if (num3 < 0)
			{
				num3 += int.MaxValue;
			}
			num2 = this.SeedArray[num4];
		}
		for (int j = 1; j < 5; j++)
		{
			for (int k = 1; k < 56; k++)
			{
				this.SeedArray[k] -= this.SeedArray[1 + (k + 30) % 55];
				if (this.SeedArray[k] < 0)
				{
					this.SeedArray[k] += int.MaxValue;
				}
			}
		}
		this.inext = 0;
		this.inextp = 21;
		Seed = 1;
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x00027569 File Offset: 0x00025769
	protected virtual double Sample()
	{
		return (double)this.InternalSample() * 4.656612875245797E-10;
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x0002757C File Offset: 0x0002577C
	private int InternalSample()
	{
		int num = this.inext;
		int num2 = this.inextp;
		if (++num >= 56)
		{
			num = 1;
		}
		if (++num2 >= 56)
		{
			num2 = 1;
		}
		int num3 = this.SeedArray[num] - this.SeedArray[num2];
		if (num3 == 2147483647)
		{
			num3--;
		}
		if (num3 < 0)
		{
			num3 += int.MaxValue;
		}
		this.SeedArray[num] = num3;
		this.inext = num;
		this.inextp = num2;
		return num3;
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x000275EF File Offset: 0x000257EF
	public virtual int Next()
	{
		return this.InternalSample();
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x000275F8 File Offset: 0x000257F8
	private double GetSampleForLargeRange()
	{
		int num = this.InternalSample();
		if (this.InternalSample() % 2 == 0)
		{
			num = -num;
		}
		return ((double)num + 2147483646.0) / 4294967293.0;
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x00027638 File Offset: 0x00025838
	public virtual int Next(int minValue, int maxValue)
	{
		long num = (long)maxValue - (long)minValue;
		if (num <= 2147483647L)
		{
			return (int)(this.Sample() * (double)num) + minValue;
		}
		return (int)((long)(this.GetSampleForLargeRange() * (double)num) + (long)minValue);
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x00027673 File Offset: 0x00025873
	public virtual int Next(int maxValue)
	{
		return (int)(this.Sample() * (double)maxValue);
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x00027683 File Offset: 0x00025883
	public virtual double NextDouble()
	{
		return this.Sample();
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x0002768C File Offset: 0x0002588C
	public virtual void NextBytes(byte[] buffer)
	{
		if (buffer == null)
		{
			throw new ArgumentNullException("buffer");
		}
		for (int i = 0; i < buffer.Length; i++)
		{
			buffer[i] = (byte)(this.InternalSample() % 256);
		}
	}

	// Token: 0x040006CB RID: 1739
	private const int MBIG = 2147483647;

	// Token: 0x040006CC RID: 1740
	private const int MSEED = 161803398;

	// Token: 0x040006CD RID: 1741
	private const int MZ = 0;

	// Token: 0x040006CE RID: 1742
	private int inext;

	// Token: 0x040006CF RID: 1743
	private int inextp;

	// Token: 0x040006D0 RID: 1744
	private int[] SeedArray = new int[56];
}
