using System;

// Token: 0x02000023 RID: 35
public class SimplexNoise
{
	// Token: 0x060001E9 RID: 489 RVA: 0x0000AD20 File Offset: 0x00008F20
	public static float noise(float x, float y, float z)
	{
		SimplexNoise.s = (x + y + z) * 0.33333334f;
		SimplexNoise.i = SimplexNoise.fastfloor(x + SimplexNoise.s);
		SimplexNoise.j = SimplexNoise.fastfloor(y + SimplexNoise.s);
		SimplexNoise.k = SimplexNoise.fastfloor(z + SimplexNoise.s);
		SimplexNoise.s = (float)(SimplexNoise.i + SimplexNoise.j + SimplexNoise.k) * 0.16666667f;
		SimplexNoise.u = x - (float)SimplexNoise.i + SimplexNoise.s;
		SimplexNoise.v = y - (float)SimplexNoise.j + SimplexNoise.s;
		SimplexNoise.w = z - (float)SimplexNoise.k + SimplexNoise.s;
		SimplexNoise.A[0] = (SimplexNoise.A[1] = (SimplexNoise.A[2] = 0));
		int num = ((SimplexNoise.u >= SimplexNoise.w) ? ((SimplexNoise.u >= SimplexNoise.v) ? 0 : 1) : ((SimplexNoise.v >= SimplexNoise.w) ? 1 : 2));
		int num2 = ((SimplexNoise.u < SimplexNoise.w) ? ((SimplexNoise.u < SimplexNoise.v) ? 0 : 1) : ((SimplexNoise.v < SimplexNoise.w) ? 1 : 2));
		return SimplexNoise.K(num) + SimplexNoise.K(3 - num - num2) + SimplexNoise.K(num2) + SimplexNoise.K(0);
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000AE5F File Offset: 0x0000905F
	private static int fastfloor(float n)
	{
		if (n <= 0f)
		{
			return (int)n - 1;
		}
		return (int)n;
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0000AE70 File Offset: 0x00009070
	private static float K(int a)
	{
		SimplexNoise.s = (float)(SimplexNoise.A[0] + SimplexNoise.A[1] + SimplexNoise.A[2]) * 0.16666667f;
		float num = SimplexNoise.u - (float)SimplexNoise.A[0] + SimplexNoise.s;
		float num2 = SimplexNoise.v - (float)SimplexNoise.A[1] + SimplexNoise.s;
		float num3 = SimplexNoise.w - (float)SimplexNoise.A[2] + SimplexNoise.s;
		float num4 = 0.6f - num * num - num2 * num2 - num3 * num3;
		int num5 = SimplexNoise.shuffle(SimplexNoise.i + SimplexNoise.A[0], SimplexNoise.j + SimplexNoise.A[1], SimplexNoise.k + SimplexNoise.A[2]);
		SimplexNoise.A[a]++;
		if (num4 < 0f)
		{
			return 0f;
		}
		int num6 = (num5 >> 5) & 1;
		int num7 = (num5 >> 4) & 1;
		int num8 = (num5 >> 3) & 1;
		int num9 = (num5 >> 2) & 1;
		int num10 = num5 & 3;
		float num11 = ((num10 == 1) ? num : ((num10 == 2) ? num2 : num3));
		float num12 = ((num10 == 1) ? num2 : ((num10 == 2) ? num3 : num));
		float num13 = ((num10 == 1) ? num3 : ((num10 == 2) ? num : num2));
		num11 = ((num6 == num8) ? (-num11) : num11);
		num12 = ((num6 == num7) ? (-num12) : num12);
		num13 = ((num6 != (num7 ^ num8)) ? (-num13) : num13);
		num4 *= num4;
		return 8f * num4 * num4 * (num11 + ((num10 == 0) ? (num12 + num13) : ((num9 == 0) ? num12 : num13)));
	}

	// Token: 0x060001EC RID: 492 RVA: 0x0000AFF4 File Offset: 0x000091F4
	private static int shuffle(int i, int j, int k)
	{
		return SimplexNoise.b(i, j, k, 0) + SimplexNoise.b(j, k, i, 1) + SimplexNoise.b(k, i, j, 2) + SimplexNoise.b(i, j, k, 3) + SimplexNoise.b(j, k, i, 4) + SimplexNoise.b(k, i, j, 5) + SimplexNoise.b(i, j, k, 6) + SimplexNoise.b(j, k, i, 7);
	}

	// Token: 0x060001ED RID: 493 RVA: 0x0000B050 File Offset: 0x00009250
	private static int b(int i, int j, int k, int B)
	{
		return SimplexNoise.T[(SimplexNoise.b(i, B) << 2) | (SimplexNoise.b(j, B) << 1) | SimplexNoise.b(k, B)];
	}

	// Token: 0x060001EE RID: 494 RVA: 0x0000B073 File Offset: 0x00009273
	private static int b(int N, int B)
	{
		return (N >> B) & 1;
	}

	// Token: 0x040000C4 RID: 196
	private static int i;

	// Token: 0x040000C5 RID: 197
	private static int j;

	// Token: 0x040000C6 RID: 198
	private static int k;

	// Token: 0x040000C7 RID: 199
	private static int[] A = new int[3];

	// Token: 0x040000C8 RID: 200
	private static float u;

	// Token: 0x040000C9 RID: 201
	private static float v;

	// Token: 0x040000CA RID: 202
	private static float w;

	// Token: 0x040000CB RID: 203
	private static float s;

	// Token: 0x040000CC RID: 204
	private const float onethird = 0.33333334f;

	// Token: 0x040000CD RID: 205
	private const float onesixth = 0.16666667f;

	// Token: 0x040000CE RID: 206
	private static int[] T = new int[] { 21, 56, 50, 44, 13, 19, 7, 42 };
}
