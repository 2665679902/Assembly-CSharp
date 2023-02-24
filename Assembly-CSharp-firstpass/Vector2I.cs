using System;
using System.Diagnostics;
using KSerialization;
using UnityEngine;
using YamlDotNet.Serialization;

// Token: 0x0200011E RID: 286
[DebuggerDisplay("{x}, {y}")]
[SerializationConfig(MemberSerialization.OptIn)]
[Serializable]
public struct Vector2I : IComparable<Vector2I>, IEquatable<Vector2I>
{
	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x060009BF RID: 2495 RVA: 0x00026408 File Offset: 0x00024608
	// (set) Token: 0x060009C0 RID: 2496 RVA: 0x00026410 File Offset: 0x00024610
	public int X
	{
		get
		{
			return this.x;
		}
		set
		{
			this.x = value;
		}
	}

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00026419 File Offset: 0x00024619
	// (set) Token: 0x060009C2 RID: 2498 RVA: 0x00026421 File Offset: 0x00024621
	public int Y
	{
		get
		{
			return this.y;
		}
		set
		{
			this.y = value;
		}
	}

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0002642A File Offset: 0x0002462A
	[YamlIgnore]
	public int sqrMagnitude
	{
		get
		{
			return Mathf.FloorToInt(Mathf.Pow((float)this.x, 2f) + Mathf.Pow((float)this.y, 2f));
		}
	}

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00026454 File Offset: 0x00024654
	[YamlIgnore]
	public int magnitude
	{
		get
		{
			return Mathf.FloorToInt(Mathf.Sqrt((float)this.sqrMagnitude));
		}
	}

	// Token: 0x170000FC RID: 252
	// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00026467 File Offset: 0x00024667
	[YamlIgnore]
	public Vector2I normalized
	{
		get
		{
			return this / this.magnitude;
		}
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x0002647A File Offset: 0x0002467A
	public Vector2I(int a, int b)
	{
		this.x = a;
		this.y = b;
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x0002648A File Offset: 0x0002468A
	public static Vector2I operator +(Vector2I u, Vector2I v)
	{
		return new Vector2I(u.x + v.x, u.y + v.y);
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x000264AB File Offset: 0x000246AB
	public static Vector2I operator -(Vector2I u, Vector2I v)
	{
		return new Vector2I(u.x - v.x, u.y - v.y);
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x000264CC File Offset: 0x000246CC
	public static Vector2I operator *(Vector2I u, Vector2I v)
	{
		return new Vector2I(u.x * v.x, u.y * v.y);
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x000264ED File Offset: 0x000246ED
	public static Vector2I operator /(Vector2I u, Vector2I v)
	{
		return new Vector2I(u.x / v.x, u.y / v.y);
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x0002650E File Offset: 0x0002470E
	public static Vector2I operator *(Vector2I v, int s)
	{
		return new Vector2I(v.x * s, v.y * s);
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x00026525 File Offset: 0x00024725
	public static Vector2I operator /(Vector2I v, int s)
	{
		return new Vector2I(v.x / s, v.y / s);
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x0002653C File Offset: 0x0002473C
	public static Vector2I operator +(Vector2I u, int scalar)
	{
		return new Vector2I(u.x + scalar, u.y + scalar);
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x00026553 File Offset: 0x00024753
	public static Vector2I operator -(Vector2I u, int scalar)
	{
		return new Vector2I(u.x - scalar, u.y - scalar);
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x0002656A File Offset: 0x0002476A
	public static bool operator ==(Vector2I u, Vector2I v)
	{
		return u.x == v.x && u.y == v.y;
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x0002658A File Offset: 0x0002478A
	public static bool operator !=(Vector2I u, Vector2I v)
	{
		return u.x != v.x || u.y != v.y;
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x000265AD File Offset: 0x000247AD
	public static Vector2I Min(Vector2I v, Vector2I w)
	{
		return new Vector2I((v.x < w.x) ? v.x : w.x, (v.y < w.y) ? v.y : w.y);
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x000265EC File Offset: 0x000247EC
	public static Vector2I Max(Vector2I v, Vector2I w)
	{
		return new Vector2I((v.x > w.x) ? v.x : w.x, (v.y > w.y) ? v.y : w.y);
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x0002662B File Offset: 0x0002482B
	public static bool operator <(Vector2I u, Vector2I v)
	{
		return u.x < v.x && u.y < v.y;
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x0002664B File Offset: 0x0002484B
	public static bool operator >(Vector2I u, Vector2I v)
	{
		return u.x > v.x && u.y > v.y;
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x0002666B File Offset: 0x0002486B
	public static bool operator <=(Vector2I u, Vector2I v)
	{
		return u.x <= v.x && u.y <= v.y;
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x0002668E File Offset: 0x0002488E
	public static bool operator >=(Vector2I u, Vector2I v)
	{
		return u.x >= v.x && u.y >= v.y;
	}

	// Token: 0x170000FD RID: 253
	// (get) Token: 0x060009D7 RID: 2519 RVA: 0x000266B1 File Offset: 0x000248B1
	public int magnitudeSqr
	{
		get
		{
			return this.x * this.x + this.y * this.y;
		}
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x000266CE File Offset: 0x000248CE
	public static implicit operator Vector2(Vector2I v)
	{
		return new Vector2((float)v.x, (float)v.y);
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x000266E4 File Offset: 0x000248E4
	public override bool Equals(object obj)
	{
		bool flag;
		try
		{
			Vector2I vector2I = (Vector2I)obj;
			flag = vector2I.x == this.x && vector2I.y == this.y;
		}
		catch
		{
			flag = false;
		}
		return flag;
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x00026730 File Offset: 0x00024930
	public bool Equals(Vector2I v)
	{
		return v.x == this.x && v.y == this.y;
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x00026750 File Offset: 0x00024950
	public override int GetHashCode()
	{
		return this.x ^ this.y;
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x0002675F File Offset: 0x0002495F
	public static bool operator <=(Vector2I u, Vector2 v)
	{
		return (float)u.x <= v.x && (float)u.y <= v.y;
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x00026784 File Offset: 0x00024984
	public static bool operator >=(Vector2I u, Vector2 v)
	{
		return (float)u.x >= v.x && (float)u.y >= v.y;
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x000267A9 File Offset: 0x000249A9
	public static bool operator <=(Vector2 u, Vector2I v)
	{
		return u.x <= (float)v.x && u.y <= (float)v.y;
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x000267CE File Offset: 0x000249CE
	public static bool operator >=(Vector2 u, Vector2I v)
	{
		return u.x >= (float)v.x && u.y >= (float)v.y;
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x000267F3 File Offset: 0x000249F3
	public override string ToString()
	{
		return string.Format("{0}, {1}", this.x, this.y);
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x00026818 File Offset: 0x00024A18
	public int CompareTo(Vector2I other)
	{
		int num = this.y - other.y;
		if (other.y == 0)
		{
			return this.x - other.x;
		}
		return num;
	}

	// Token: 0x040006AE RID: 1710
	public static readonly Vector2I zero = new Vector2I(0, 0);

	// Token: 0x040006AF RID: 1711
	public static readonly Vector2I one = new Vector2I(1, 1);

	// Token: 0x040006B0 RID: 1712
	public static readonly Vector2I minusone = new Vector2I(-1, -1);

	// Token: 0x040006B1 RID: 1713
	[Serialize]
	public int x;

	// Token: 0x040006B2 RID: 1714
	[Serialize]
	public int y;
}
