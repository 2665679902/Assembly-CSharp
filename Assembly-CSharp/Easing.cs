using System;
using UnityEngine;

// Token: 0x0200035A RID: 858
public class Easing
{
	// Token: 0x04000955 RID: 2389
	public const Easing.EasingFn PARAM_DEFAULT = null;

	// Token: 0x04000956 RID: 2390
	public static readonly Easing.EasingFn Linear = (float x) => x;

	// Token: 0x04000957 RID: 2391
	public static readonly Easing.EasingFn SmoothStep = (float x) => Mathf.SmoothStep(0f, 1f, x);

	// Token: 0x04000958 RID: 2392
	public static readonly Easing.EasingFn QuadIn = (float x) => x * x;

	// Token: 0x04000959 RID: 2393
	public static readonly Easing.EasingFn QuadOut = (float x) => 1f - (1f - x) * (1f - x);

	// Token: 0x0400095A RID: 2394
	public static readonly Easing.EasingFn QuadInOut = delegate(float x)
	{
		if ((double)x >= 0.5)
		{
			return 1f - Mathf.Pow(-2f * x + 2f, 2f) / 2f;
		}
		return 2f * x * x;
	};

	// Token: 0x0400095B RID: 2395
	public static readonly Easing.EasingFn CubicIn = (float x) => x * x * x;

	// Token: 0x0400095C RID: 2396
	public static readonly Easing.EasingFn CubicOut = (float x) => 1f - Mathf.Pow(1f - x, 3f);

	// Token: 0x0400095D RID: 2397
	public static readonly Easing.EasingFn CubicInOut = delegate(float x)
	{
		if ((double)x >= 0.5)
		{
			return 1f - Mathf.Pow(-2f * x + 2f, 3f) / 2f;
		}
		return 4f * x * x * x;
	};

	// Token: 0x0400095E RID: 2398
	public static readonly Easing.EasingFn QuartIn = (float x) => x * x * x * x;

	// Token: 0x0400095F RID: 2399
	public static readonly Easing.EasingFn QuartOut = (float x) => 1f - Mathf.Pow(1f - x, 4f);

	// Token: 0x04000960 RID: 2400
	public static readonly Easing.EasingFn QuartInOut = delegate(float x)
	{
		if ((double)x >= 0.5)
		{
			return 1f - Mathf.Pow(-2f * x + 2f, 4f) / 2f;
		}
		return 8f * x * x * x * x;
	};

	// Token: 0x04000961 RID: 2401
	public static readonly Easing.EasingFn QuintIn = (float x) => x * x * x * x * x;

	// Token: 0x04000962 RID: 2402
	public static readonly Easing.EasingFn QuintOut = (float x) => 1f - Mathf.Pow(1f - x, 5f);

	// Token: 0x04000963 RID: 2403
	public static readonly Easing.EasingFn QuintInOut = delegate(float x)
	{
		if ((double)x >= 0.5)
		{
			return 1f - Mathf.Pow(-2f * x + 2f, 5f) / 2f;
		}
		return 16f * x * x * x * x * x;
	};

	// Token: 0x04000964 RID: 2404
	public static readonly Easing.EasingFn ExpoIn = delegate(float x)
	{
		if (x != 0f)
		{
			return Mathf.Pow(2f, 10f * x - 10f);
		}
		return 0f;
	};

	// Token: 0x04000965 RID: 2405
	public static readonly Easing.EasingFn ExpoOut = delegate(float x)
	{
		if (x != 1f)
		{
			return 1f - Mathf.Pow(2f, -10f * x);
		}
		return 1f;
	};

	// Token: 0x04000966 RID: 2406
	public static readonly Easing.EasingFn ExpoInOut = delegate(float x)
	{
		if (x == 0f)
		{
			return 0f;
		}
		if (x == 1f)
		{
			return 1f;
		}
		if ((double)x >= 0.5)
		{
			return (2f - Mathf.Pow(2f, -20f * x + 10f)) / 2f;
		}
		return Mathf.Pow(2f, 20f * x - 10f) / 2f;
	};

	// Token: 0x04000967 RID: 2407
	public static readonly Easing.EasingFn SineIn = (float x) => 1f - Mathf.Cos(x * 3.1415927f / 2f);

	// Token: 0x04000968 RID: 2408
	public static readonly Easing.EasingFn SineOut = (float x) => Mathf.Sin(x * 3.1415927f / 2f);

	// Token: 0x04000969 RID: 2409
	public static readonly Easing.EasingFn SineInOut = (float x) => -(Mathf.Cos(3.1415927f * x) - 1f) / 2f;

	// Token: 0x0400096A RID: 2410
	public static readonly Easing.EasingFn CircIn = (float x) => 1f - Mathf.Sqrt(1f - Mathf.Pow(x, 2f));

	// Token: 0x0400096B RID: 2411
	public static readonly Easing.EasingFn CircOut = (float x) => Mathf.Sqrt(1f - Mathf.Pow(x - 1f, 2f));

	// Token: 0x0400096C RID: 2412
	public static readonly Easing.EasingFn CircInOut = delegate(float x)
	{
		if ((double)x >= 0.5)
		{
			return (Mathf.Sqrt(1f - Mathf.Pow(-2f * x + 2f, 2f)) + 1f) / 2f;
		}
		return (1f - Mathf.Sqrt(1f - Mathf.Pow(2f * x, 2f))) / 2f;
	};

	// Token: 0x0400096D RID: 2413
	public static readonly Easing.EasingFn ElasticIn = delegate(float x)
	{
		if (x == 0f)
		{
			return 0f;
		}
		if (x != 1f)
		{
			return -Mathf.Pow(2f, 10f * x - 10f) * Mathf.Sin((x * 10f - 10.75f) * 2.0943952f);
		}
		return 1f;
	};

	// Token: 0x0400096E RID: 2414
	public static readonly Easing.EasingFn ElasticOut = delegate(float x)
	{
		if (x == 0f)
		{
			return 0f;
		}
		if (x != 1f)
		{
			return Mathf.Pow(2f, -10f * x) * Mathf.Sin((x * 10f - 0.75f) * 2.0943952f) + 1f;
		}
		return 1f;
	};

	// Token: 0x0400096F RID: 2415
	public static readonly Easing.EasingFn ElasticInOut = delegate(float x)
	{
		if (x == 0f)
		{
			return 0f;
		}
		if (x == 1f)
		{
			return 1f;
		}
		if ((double)x >= 0.5)
		{
			return Mathf.Pow(2f, -20f * x + 10f) * Mathf.Sin((20f * x - 11.125f) * 1.3962635f) / 2f + 1f;
		}
		return -(Mathf.Pow(2f, 20f * x - 10f) * Mathf.Sin((20f * x - 11.125f) * 1.3962635f)) / 2f;
	};

	// Token: 0x04000970 RID: 2416
	public static readonly Easing.EasingFn BackIn = (float x) => 2.70158f * x * x * x - 1.70158f * x * x;

	// Token: 0x04000971 RID: 2417
	public static readonly Easing.EasingFn BackOut = (float x) => 1f + 2.70158f * Mathf.Pow(x - 1f, 3f) + 1.70158f * Mathf.Pow(x - 1f, 2f);

	// Token: 0x04000972 RID: 2418
	public static readonly Easing.EasingFn BackInOut = delegate(float x)
	{
		if ((double)x >= 0.5)
		{
			return (Mathf.Pow(2f * x - 2f, 2f) * (3.5949094f * (x * 2f - 2f) + 2.5949094f) + 2f) / 2f;
		}
		return Mathf.Pow(2f * x, 2f) * (7.189819f * x - 2.5949094f) / 2f;
	};

	// Token: 0x04000973 RID: 2419
	public static readonly Easing.EasingFn BounceIn = (float x) => 1f - Easing.BounceOut(1f - x);

	// Token: 0x04000974 RID: 2420
	public static readonly Easing.EasingFn BounceOut = delegate(float x)
	{
		if (x < 0.36363637f)
		{
			return 7.5625f * x * x;
		}
		if (x < 0.72727275f)
		{
			return 7.5625f * (x -= 0.54545456f) * x + 0.75f;
		}
		if ((double)x < 0.9090909090909091)
		{
			return 7.5625f * (x -= 0.8181818f) * x + 0.9375f;
		}
		return 7.5625f * (x -= 0.95454544f) * x + 0.984375f;
	};

	// Token: 0x04000975 RID: 2421
	public static readonly Easing.EasingFn BounceInOut = delegate(float x)
	{
		if ((double)x >= 0.5)
		{
			return (1f + Easing.BounceOut(2f * x - 1f)) / 2f;
		}
		return (1f - Easing.BounceOut(1f - 2f * x)) / 2f;
	};

	// Token: 0x02000F20 RID: 3872
	// (Invoke) Token: 0x06006E03 RID: 28163
	public delegate float EasingFn(float f);
}
