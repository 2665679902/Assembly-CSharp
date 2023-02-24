using System;
using System.Diagnostics;
using UnityEngine;
using YamlDotNet.Serialization;

// Token: 0x02000120 RID: 288
[DebuggerDisplay("{x}, {y}, {z}")]
public struct Vector3I
{
	// Token: 0x17000103 RID: 259
	// (get) Token: 0x060009FF RID: 2559 RVA: 0x00026B43 File Offset: 0x00024D43
	[YamlIgnore]
	public int sqrMagnitude
	{
		get
		{
			return Mathf.FloorToInt(Mathf.Pow((float)this.x, 2f) + Mathf.Pow((float)this.y, 2f) + Mathf.Pow((float)this.z, 2f));
		}
	}

	// Token: 0x17000104 RID: 260
	// (get) Token: 0x06000A00 RID: 2560 RVA: 0x00026B7F File Offset: 0x00024D7F
	[YamlIgnore]
	public int magnitude
	{
		get
		{
			return Mathf.FloorToInt(Mathf.Sqrt((float)this.sqrMagnitude));
		}
	}

	// Token: 0x17000105 RID: 261
	// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00026B92 File Offset: 0x00024D92
	[YamlIgnore]
	public Vector3I normalized
	{
		get
		{
			return this / this.magnitude;
		}
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x00026BA5 File Offset: 0x00024DA5
	public Vector3I(int a, int b, int c)
	{
		this.x = a;
		this.y = b;
		this.z = c;
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x00026BBC File Offset: 0x00024DBC
	public static Vector3I operator +(Vector3I u, Vector3I v)
	{
		return new Vector3I(u.x + v.x, u.y + v.y, u.z + v.z);
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x00026BEA File Offset: 0x00024DEA
	public static Vector3I operator -(Vector3I u, Vector3I v)
	{
		return new Vector3I(u.x - v.x, u.y - v.y, u.z - v.z);
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x00026C18 File Offset: 0x00024E18
	public static Vector3I operator *(Vector3I u, Vector3I v)
	{
		return new Vector3I(u.x * v.x, u.y * v.y, u.z * v.z);
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x00026C46 File Offset: 0x00024E46
	public static Vector3I operator /(Vector3I u, Vector3I v)
	{
		return new Vector3I(u.x / v.x, u.y / v.y, u.z / v.z);
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x00026C74 File Offset: 0x00024E74
	public static Vector3I operator *(Vector3I v, int s)
	{
		return new Vector3I(v.x * s, v.y * s, v.z * s);
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x00026C93 File Offset: 0x00024E93
	public static Vector3I operator /(Vector3I v, int s)
	{
		return new Vector3I(v.x / s, v.y / s, v.z / s);
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x00026CB2 File Offset: 0x00024EB2
	public static Vector3I operator +(Vector3I u, int scalar)
	{
		return new Vector3I(u.x + scalar, u.y + scalar, u.z + scalar);
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x00026CD1 File Offset: 0x00024ED1
	public static Vector3I operator -(Vector3I u, int scalar)
	{
		return new Vector3I(u.x - scalar, u.y - scalar, u.z - scalar);
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x00026CF0 File Offset: 0x00024EF0
	public static bool operator ==(Vector3I v1, Vector3I v2)
	{
		return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x00026D1E File Offset: 0x00024F1E
	public static bool operator !=(Vector3I v1, Vector3I v2)
	{
		return !(v1 == v2);
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x00026D2A File Offset: 0x00024F2A
	public override bool Equals(object o)
	{
		return base.Equals(o);
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x00026D3D File Offset: 0x00024F3D
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x00026D4F File Offset: 0x00024F4F
	public override string ToString()
	{
		return string.Format("{0}, {1}, {2}", this.x, this.y, this.z);
	}

	// Token: 0x040006B5 RID: 1717
	public int x;

	// Token: 0x040006B6 RID: 1718
	public int y;

	// Token: 0x040006B7 RID: 1719
	public int z;
}
