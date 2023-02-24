using System;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public static class MathUtil
{
	// Token: 0x06000800 RID: 2048 RVA: 0x0002052C File Offset: 0x0001E72C
	public static float Clamp(float min, float max, float val)
	{
		return Mathf.Max(min, Mathf.Min(max, val));
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x0002053B File Offset: 0x0001E73B
	public static int Clamp(int min, int max, int val)
	{
		return Mathf.Max(min, Mathf.Min(max, val));
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x0002054A File Offset: 0x0001E74A
	public static float ReRange(float val, float in_a, float in_b, float out_a, float out_b)
	{
		return (val - in_a) / (in_b - in_a) * (out_b - out_a) + out_a;
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x0002055A File Offset: 0x0001E75A
	public static float Wrap(float min, float max, float val)
	{
		while (val < min)
		{
			val += max - min;
		}
		while (val > max)
		{
			val -= max - min;
		}
		return val;
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x00020578 File Offset: 0x0001E778
	public static float ApproachConstant(float target, float current, float speed)
	{
		float num = target - current;
		if (num > speed)
		{
			return current + speed;
		}
		if (num < -speed)
		{
			return current - speed;
		}
		return target;
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x0002059C File Offset: 0x0001E79C
	public static Vector3 ApproachConstant(Vector3 target, Vector3 current, float speed)
	{
		Vector3 vector = target - current;
		if (vector.magnitude > speed)
		{
			return current + vector.normalized * speed;
		}
		return target;
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x000205D0 File Offset: 0x0001E7D0
	public static Vector3 Round(this Vector3 v)
	{
		return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x000205F8 File Offset: 0x0001E7F8
	public static Vector3 Min(this Vector3 a, Vector3 b)
	{
		return new Vector3(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y), Mathf.Min(a.z, b.z));
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x00020632 File Offset: 0x0001E832
	public static Vector3 Max(this Vector3 a, Vector3 b)
	{
		return new Vector3(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y), Mathf.Max(a.z, b.z));
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x0002066C File Offset: 0x0001E86C
	public static Vector3[] RaySphereIntersection(Ray ray, Vector3 sphereCenter, float sphereRadius)
	{
		ray.direction.Normalize();
		Vector3 vector = sphereCenter - ray.origin;
		float num = Vector3.Dot(ray.direction, vector);
		float num2 = Vector3.Dot(vector, vector);
		float num3 = num * num - num2 + sphereRadius * sphereRadius;
		if (num3 < 0f)
		{
			return new Vector3[0];
		}
		if (num3 == 0f)
		{
			Vector3 vector2 = num * ray.direction + ray.origin;
			return new Vector3[] { vector2 };
		}
		Vector3 vector3 = (num - Mathf.Sqrt(num3)) * ray.direction + ray.origin;
		Vector3 vector4 = (num + Mathf.Sqrt(num3)) * ray.direction + ray.origin;
		return new Vector3[] { vector3, vector4 };
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x00020755 File Offset: 0x0001E955
	public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
	{
		return Mathf.Atan2(Vector3.Dot(n, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * 57.29578f;
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x00020778 File Offset: 0x0001E978
	public static float GetClosestPointBetweenPointAndLineSegment(MathUtil.Pair<Vector2, Vector2> segment, Vector2 point, ref float closest_point)
	{
		float num = (segment.Second.x - segment.First.x) * (segment.Second.x - segment.First.x) + (segment.Second.y - segment.First.y) * (segment.Second.y - segment.First.y);
		if (num <= 0f)
		{
			closest_point = 0f;
			return Vector2.Distance(segment.First, point);
		}
		float num2 = (point.x - segment.First.x) * (segment.Second.x - segment.First.x) + (point.y - segment.First.y) * (segment.Second.y - segment.First.y);
		closest_point = Mathf.Max(0f, Mathf.Min(1f, num2 / num));
		return Vector2.Distance(segment.First + (segment.Second - segment.First) * closest_point, point);
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x00020899 File Offset: 0x0001EA99
	public static bool IsPowerOfTwo(int x)
	{
		DebugUtil.DevAssert(x > 0, "Invalid input", null);
		return (x & (x - 1)) == 0;
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x000208B4 File Offset: 0x0001EAB4
	public static int RoundToNextPowerOfTwo(int x)
	{
		if (MathUtil.IsPowerOfTwo(x))
		{
			return x;
		}
		int num = 0;
		for (int i = 0; i < 32; i++)
		{
			if ((x & (1 << i)) != 0)
			{
				num = i;
			}
		}
		return 1 << num + 1;
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x000208F0 File Offset: 0x0001EAF0
	public static Vector2I PowerOfTwoToMaskAndShift(int x)
	{
		DebugUtil.DevAssert(x > 0 && MathUtil.IsPowerOfTwo(x), "Invalid input", null);
		int num = 0;
		for (int i = 0; i < 32; i++)
		{
			if ((x & (1 << i)) != 0)
			{
				num = i;
				break;
			}
		}
		Vector2I vector2I;
		vector2I.x = x - 1;
		vector2I.y = num;
		return vector2I;
	}

	// Token: 0x020009EE RID: 2542
	public struct MinMax
	{
		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x060053D5 RID: 21461 RVA: 0x0009C654 File Offset: 0x0009A854
		// (set) Token: 0x060053D6 RID: 21462 RVA: 0x0009C65C File Offset: 0x0009A85C
		public float min { readonly get; private set; }

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x060053D7 RID: 21463 RVA: 0x0009C665 File Offset: 0x0009A865
		// (set) Token: 0x060053D8 RID: 21464 RVA: 0x0009C66D File Offset: 0x0009A86D
		public float max { readonly get; private set; }

		// Token: 0x060053D9 RID: 21465 RVA: 0x0009C676 File Offset: 0x0009A876
		public MinMax(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x060053DA RID: 21466 RVA: 0x0009C686 File Offset: 0x0009A886
		public float Get(SeededRandom rnd)
		{
			return rnd.RandomRange(this.min, this.max);
		}

		// Token: 0x060053DB RID: 21467 RVA: 0x0009C69A File Offset: 0x0009A89A
		public float Get()
		{
			return UnityEngine.Random.Range(this.min, this.max);
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x0009C6AD File Offset: 0x0009A8AD
		public float Lerp(float t)
		{
			return Mathf.Lerp(this.min, this.max, t);
		}

		// Token: 0x060053DD RID: 21469 RVA: 0x0009C6C1 File Offset: 0x0009A8C1
		public override string ToString()
		{
			return string.Format("[{0}:{1}]", this.min, this.max);
		}
	}

	// Token: 0x020009EF RID: 2543
	public class Pair<T, U>
	{
		// Token: 0x060053DE RID: 21470 RVA: 0x0009C6E3 File Offset: 0x0009A8E3
		public Pair()
		{
		}

		// Token: 0x060053DF RID: 21471 RVA: 0x0009C6EB File Offset: 0x0009A8EB
		public Pair(T first, U second)
		{
			this.First = first;
			this.Second = second;
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x060053E0 RID: 21472 RVA: 0x0009C701 File Offset: 0x0009A901
		// (set) Token: 0x060053E1 RID: 21473 RVA: 0x0009C709 File Offset: 0x0009A909
		public T First { get; set; }

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x060053E2 RID: 21474 RVA: 0x0009C712 File Offset: 0x0009A912
		// (set) Token: 0x060053E3 RID: 21475 RVA: 0x0009C71A File Offset: 0x0009A91A
		public U Second { get; set; }
	}
}
