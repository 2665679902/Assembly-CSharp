using System;
using System.Diagnostics;
using KSerialization;
using UnityEngine;
using YamlDotNet.Serialization;

// Token: 0x0200011F RID: 287
[DebuggerDisplay("{x}, {y}")]
public struct Vector2f
{
	// Token: 0x170000FE RID: 254
	// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00026870 File Offset: 0x00024A70
	// (set) Token: 0x060009E4 RID: 2532 RVA: 0x00026878 File Offset: 0x00024A78
	public float X
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

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00026881 File Offset: 0x00024A81
	// (set) Token: 0x060009E6 RID: 2534 RVA: 0x00026889 File Offset: 0x00024A89
	public float Y
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

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00026892 File Offset: 0x00024A92
	[YamlIgnore]
	public float sqrMagnitude
	{
		get
		{
			return Mathf.Pow(this.x, 2f) + Mathf.Pow(this.y, 2f);
		}
	}

	// Token: 0x17000101 RID: 257
	// (get) Token: 0x060009E8 RID: 2536 RVA: 0x000268B5 File Offset: 0x00024AB5
	[YamlIgnore]
	public float magnitude
	{
		get
		{
			return Mathf.Sqrt(this.sqrMagnitude);
		}
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x060009E9 RID: 2537 RVA: 0x000268C2 File Offset: 0x00024AC2
	[YamlIgnore]
	public Vector2f normalized
	{
		get
		{
			return this / this.magnitude;
		}
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x000268D5 File Offset: 0x00024AD5
	public Vector2f(int a, int b)
	{
		this.x = (float)a;
		this.y = (float)b;
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x000268E7 File Offset: 0x00024AE7
	public Vector2f(float a, float b)
	{
		this.x = a;
		this.y = b;
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x000268F7 File Offset: 0x00024AF7
	public Vector2f(Vector2 src)
	{
		this.x = src.x;
		this.y = src.y;
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x00026911 File Offset: 0x00024B11
	public static Vector2 operator +(Vector2f u, Vector2 v)
	{
		return new Vector2(u.x + v.x, u.y + v.y);
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x00026932 File Offset: 0x00024B32
	public static Vector2 operator +(Vector2 u, Vector2f v)
	{
		return new Vector2(u.x + v.x, u.y + v.y);
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x00026953 File Offset: 0x00024B53
	public static Vector2f operator +(Vector2f u, Vector2f v)
	{
		return new Vector2f(u.x + v.x, u.y + v.y);
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x00026974 File Offset: 0x00024B74
	public static Vector2f operator -(Vector2f u, Vector2f v)
	{
		return new Vector2f(u.x - v.x, u.y - v.y);
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x00026995 File Offset: 0x00024B95
	public static Vector2f operator *(Vector2f u, Vector2f v)
	{
		return new Vector2f(u.x * v.x, u.y * v.y);
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x000269B6 File Offset: 0x00024BB6
	public static Vector2f operator /(Vector2f u, Vector2f v)
	{
		return new Vector2f(u.x / v.x, u.y / v.y);
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x000269D7 File Offset: 0x00024BD7
	public static Vector2f operator *(Vector2f v, float s)
	{
		return new Vector2f(v.x * s, v.y * s);
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x000269EE File Offset: 0x00024BEE
	public static Vector2f operator /(Vector2f v, float s)
	{
		return new Vector2f(v.x / s, v.y / s);
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x00026A05 File Offset: 0x00024C05
	public static Vector2f operator +(Vector2f u, float scalar)
	{
		return new Vector2f(u.x + scalar, u.y + scalar);
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x00026A1C File Offset: 0x00024C1C
	public static Vector2f operator -(Vector2f u, float scalar)
	{
		return new Vector2f(u.x - scalar, u.y - scalar);
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x00026A33 File Offset: 0x00024C33
	public static bool operator ==(Vector2f u, Vector2f v)
	{
		return u.x == v.x && u.y == v.y;
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x00026A53 File Offset: 0x00024C53
	public static bool operator !=(Vector2f u, Vector2f v)
	{
		return u.x != v.x || u.y != v.y;
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x00026A76 File Offset: 0x00024C76
	public static implicit operator Vector2(Vector2f v)
	{
		return new Vector2(v.x, v.y);
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x00026A89 File Offset: 0x00024C89
	public static implicit operator Vector2f(Vector2 v)
	{
		return new Vector2f(v.x, v.y);
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x00026A9C File Offset: 0x00024C9C
	public bool Equals(Vector2 v)
	{
		return v.x == this.x && v.y == this.y;
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x00026ABC File Offset: 0x00024CBC
	public override bool Equals(object obj)
	{
		bool flag;
		try
		{
			Vector2f vector2f = (Vector2f)obj;
			flag = vector2f.x == this.x && vector2f.y == this.y;
		}
		catch
		{
			flag = false;
		}
		return flag;
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x00026B08 File Offset: 0x00024D08
	public override int GetHashCode()
	{
		return this.x.GetHashCode() ^ this.y.GetHashCode();
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x00026B21 File Offset: 0x00024D21
	public override string ToString()
	{
		return string.Format("{0}, {1}", this.x, this.y);
	}

	// Token: 0x040006B3 RID: 1715
	[Serialize]
	public float x;

	// Token: 0x040006B4 RID: 1716
	[Serialize]
	public float y;
}
