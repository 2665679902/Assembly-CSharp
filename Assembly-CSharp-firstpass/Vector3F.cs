using System;
using System.Diagnostics;
using UnityEngine;
using YamlDotNet.Serialization;

// Token: 0x02000121 RID: 289
[DebuggerDisplay("{x}, {y}, {z}")]
public struct Vector3F
{
	// Token: 0x17000106 RID: 262
	// (get) Token: 0x06000A10 RID: 2576 RVA: 0x00026D7C File Offset: 0x00024F7C
	[YamlIgnore]
	public float sqrMagnitude
	{
		get
		{
			return Mathf.Pow(this.x, 2f) + Mathf.Pow(this.y, 2f) + Mathf.Pow(this.z, 2f);
		}
	}

	// Token: 0x17000107 RID: 263
	// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00026DB0 File Offset: 0x00024FB0
	[YamlIgnore]
	public float magnitude
	{
		get
		{
			return Mathf.Sqrt(this.sqrMagnitude);
		}
	}

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x06000A12 RID: 2578 RVA: 0x00026DBD File Offset: 0x00024FBD
	[YamlIgnore]
	public Vector3F normalized
	{
		get
		{
			return this / this.magnitude;
		}
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x00026DD0 File Offset: 0x00024FD0
	public Vector3F(float _x, float _y, float _z)
	{
		this.x = _x;
		this.y = _y;
		this.z = _z;
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x00026DE7 File Offset: 0x00024FE7
	public static Vector3F operator -(Vector3F v1, Vector3F v2)
	{
		return new Vector3F(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x00026E15 File Offset: 0x00025015
	public static Vector3F operator +(Vector3F v1, Vector3F v2)
	{
		return new Vector3F(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x00026E43 File Offset: 0x00025043
	public static Vector3F operator *(Vector3F v, float scalar)
	{
		return new Vector3F(v.x * scalar, v.y * scalar, v.z * scalar);
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x00026E62 File Offset: 0x00025062
	public static Vector3F operator *(float scalar, Vector3F v)
	{
		return new Vector3F(v.x * scalar, v.y * scalar, v.z * scalar);
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x00026E81 File Offset: 0x00025081
	public static Vector3F operator /(Vector3F v, float scalar)
	{
		return new Vector3F(v.x / scalar, v.y / scalar, v.z / scalar);
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x00026EA0 File Offset: 0x000250A0
	public static Vector3F operator /(float scalar, Vector3F v)
	{
		return new Vector3F(v.x / scalar, v.y / scalar, v.z / scalar);
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x00026EBF File Offset: 0x000250BF
	public static bool operator <(Vector3F v1, Vector3F v2)
	{
		return v1.x < v2.x && v1.y < v2.y && v1.z < v2.z;
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x00026EED File Offset: 0x000250ED
	public static bool operator >(Vector3F v1, Vector3F v2)
	{
		return v1.x > v2.x && v1.y > v2.y && v1.z > v2.z;
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x00026F1B File Offset: 0x0002511B
	public static bool operator <=(Vector3F v1, Vector3F v2)
	{
		return v1.x <= v2.x && v1.y <= v2.y && v1.z <= v2.z;
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x00026F4C File Offset: 0x0002514C
	public static bool operator >=(Vector3F v1, Vector3F v2)
	{
		return v1.x >= v2.x && v1.y >= v2.y && v1.z >= v2.z;
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x00026F7D File Offset: 0x0002517D
	public static bool operator ==(Vector3F v1, Vector3F v2)
	{
		return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x00026FAB File Offset: 0x000251AB
	public static bool operator !=(Vector3F v1, Vector3F v2)
	{
		return !(v1 == v2);
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x00026FB7 File Offset: 0x000251B7
	public override bool Equals(object o)
	{
		return base.Equals(o);
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x00026FCA File Offset: 0x000251CA
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x00026FDC File Offset: 0x000251DC
	public override string ToString()
	{
		return string.Format("{0}, {1}, {2}", this.x, this.y, this.z);
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x00027009 File Offset: 0x00025209
	public static float Dot(Vector3F v1, Vector3F v2)
	{
		return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x00027034 File Offset: 0x00025234
	public static implicit operator Vector3(Vector3F v)
	{
		return new Vector3(v.x, v.y, v.z);
	}

	// Token: 0x040006B8 RID: 1720
	public float x;

	// Token: 0x040006B9 RID: 1721
	public float y;

	// Token: 0x040006BA RID: 1722
	public float z;
}
