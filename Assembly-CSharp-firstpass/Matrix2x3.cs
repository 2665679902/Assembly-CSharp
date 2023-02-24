using System;
using UnityEngine;

// Token: 0x020000D9 RID: 217
public struct Matrix2x3
{
	// Token: 0x0600080F RID: 2063 RVA: 0x00020945 File Offset: 0x0001EB45
	public Matrix2x3(float e00, float e01, float e02, float e10, float e11, float e12)
	{
		this.m00 = e00;
		this.m01 = e01;
		this.m02 = e02;
		this.m10 = e10;
		this.m11 = e11;
		this.m12 = e12;
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x00020974 File Offset: 0x0001EB74
	public override bool Equals(object obj)
	{
		Matrix2x3 matrix2x = (Matrix2x3)obj;
		return this == matrix2x;
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x00020994 File Offset: 0x0001EB94
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x000209A8 File Offset: 0x0001EBA8
	public static Vector3 operator *(Matrix2x3 m, Vector3 v)
	{
		return new Vector3(v.x * m.m00 + v.y * m.m01 + m.m02, v.x * m.m10 + v.y * m.m11 + m.m12, v.z);
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00020A04 File Offset: 0x0001EC04
	public static Matrix2x3 operator *(Matrix2x3 m, Matrix2x3 n)
	{
		return new Matrix2x3(m.m00 * n.m00 + m.m01 * n.m10, m.m00 * n.m01 + m.m01 * n.m11, m.m00 * n.m02 + m.m01 * n.m12 + m.m02 * 1f, m.m10 * n.m00 + m.m11 * n.m10, m.m10 * n.m01 + m.m11 * n.m11, m.m10 * n.m02 + m.m11 * n.m12 + m.m12 * 1f);
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00020AD4 File Offset: 0x0001ECD4
	public static bool operator ==(Matrix2x3 m, Matrix2x3 n)
	{
		return m.m00 == n.m00 && m.m01 == n.m01 && m.m02 == n.m02 && m.m10 == n.m10 && m.m11 == n.m11 && m.m12 == n.m12;
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x00020B37 File Offset: 0x0001ED37
	public static bool operator !=(Matrix2x3 m, Matrix2x3 n)
	{
		return !(m == n);
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x00020B44 File Offset: 0x0001ED44
	public Vector3 MultiplyPoint(Vector3 v)
	{
		return new Vector3(v.x * this.m00 + v.y * this.m01 + this.m02, v.x * this.m10 + v.y * this.m11 + this.m12, v.z);
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x00020BA0 File Offset: 0x0001EDA0
	public Vector3 MultiplyVector(Vector3 v)
	{
		return new Vector3(v.x * this.m00 + v.y * this.m01, v.x * this.m10 + v.y * this.m11, v.z);
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x00020BF0 File Offset: 0x0001EDF0
	public static implicit operator Matrix4x4(Matrix2x3 m)
	{
		Matrix4x4 matrix4x = Matrix4x4.identity;
		matrix4x.m00 = m.m00;
		matrix4x.m01 = m.m01;
		matrix4x.m03 = m.m02;
		matrix4x.m10 = m.m10;
		matrix4x.m11 = m.m11;
		matrix4x.m13 = m.m12;
		return matrix4x;
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x00020C54 File Offset: 0x0001EE54
	public static Matrix2x3 Scale(Vector2 scale)
	{
		Matrix2x3 matrix2x = Matrix2x3.identity;
		matrix2x.m00 = scale.x;
		matrix2x.m11 = scale.y;
		return matrix2x;
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x00020C84 File Offset: 0x0001EE84
	public static Matrix2x3 Translate(Vector2 translation)
	{
		Matrix2x3 matrix2x = Matrix2x3.identity;
		matrix2x.m02 = translation.x;
		matrix2x.m12 = translation.y;
		return matrix2x;
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x00020CB4 File Offset: 0x0001EEB4
	public static Matrix2x3 Rotate(float angle_in_radians)
	{
		Matrix2x3 matrix2x = Matrix2x3.identity;
		float num = Mathf.Cos(angle_in_radians);
		float num2 = Mathf.Sin(angle_in_radians);
		matrix2x.m00 = num;
		matrix2x.m01 = -num2;
		matrix2x.m10 = num2;
		matrix2x.m11 = num;
		return matrix2x;
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x00020CF8 File Offset: 0x0001EEF8
	public static Matrix2x3 Rotate(Quaternion quaternion)
	{
		Matrix2x3 matrix2x = Matrix2x3.identity;
		float num = quaternion.x * quaternion.x;
		float num2 = quaternion.y * quaternion.y;
		float num3 = quaternion.z * quaternion.z;
		float num4 = quaternion.x * quaternion.y;
		float num5 = quaternion.x * quaternion.z;
		float num6 = quaternion.y * quaternion.z;
		float num7 = quaternion.w * quaternion.x;
		float num8 = quaternion.w * quaternion.y;
		float num9 = quaternion.w * quaternion.z;
		matrix2x.m00 = 1f - 2f * (num2 + num3);
		matrix2x.m01 = 2f * (num4 - num9);
		matrix2x.m02 = 2f * (num5 + num8);
		matrix2x.m10 = 2f * (num4 + num9);
		matrix2x.m11 = 1f - 2f * (num + num3);
		matrix2x.m12 = 2f * (num6 - num7);
		return matrix2x;
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x00020E04 File Offset: 0x0001F004
	public static Matrix2x3 TRS(Vector2 translation, Quaternion quaternion, Vector2 scale)
	{
		Matrix2x3 matrix2x = Matrix2x3.Rotate(quaternion);
		matrix2x.m00 *= scale.x;
		matrix2x.m11 *= scale.y;
		matrix2x.m02 = translation.x;
		matrix2x.m12 = translation.y;
		return matrix2x;
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x00020E5C File Offset: 0x0001F05C
	public static Matrix2x3 TRS(Vector2 translation, float angle_in_radians, Vector2 scale)
	{
		Matrix2x3 matrix2x = Matrix2x3.Rotate(angle_in_radians);
		matrix2x.m00 *= scale.x;
		matrix2x.m11 *= scale.y;
		matrix2x.m02 = translation.x;
		matrix2x.m12 = translation.y;
		return matrix2x;
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x00020EB4 File Offset: 0x0001F0B4
	public override string ToString()
	{
		return string.Format("[{0}, {1}, {2}]  [{3}, {4}, {5}]", new object[] { this.m00, this.m01, this.m02, this.m10, this.m11, this.m12 });
	}

	// Token: 0x0400061F RID: 1567
	public float m00;

	// Token: 0x04000620 RID: 1568
	public float m01;

	// Token: 0x04000621 RID: 1569
	public float m02;

	// Token: 0x04000622 RID: 1570
	public float m10;

	// Token: 0x04000623 RID: 1571
	public float m11;

	// Token: 0x04000624 RID: 1572
	public float m12;

	// Token: 0x04000625 RID: 1573
	public static readonly Matrix2x3 identity = new Matrix2x3(1f, 0f, 0f, 0f, 1f, 0f);
}
