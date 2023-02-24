using System;
using UnityEngine;

// Token: 0x02000078 RID: 120
[Serializable]
public struct AABB3
{
	// Token: 0x060004C8 RID: 1224 RVA: 0x00017E17 File Offset: 0x00016017
	public AABB3(Vector3 pt)
	{
		this.min = pt;
		this.max = pt;
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x00017E27 File Offset: 0x00016027
	public AABB3(Vector3 min, Vector3 max)
	{
		this.min = min;
		this.max = max;
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x00017E37 File Offset: 0x00016037
	public bool IsValid()
	{
		return this.min.Min(this.max) == this.min;
	}

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x060004CB RID: 1227 RVA: 0x00017E55 File Offset: 0x00016055
	public Vector3 Center
	{
		get
		{
			return (this.min + this.max) * 0.5f;
		}
	}

	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x060004CC RID: 1228 RVA: 0x00017E72 File Offset: 0x00016072
	public Vector3 Range
	{
		get
		{
			return this.max - this.min;
		}
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x00017E88 File Offset: 0x00016088
	public void Expand(float amount)
	{
		Vector3 vector = new Vector3(amount * 0.5f, amount * 0.5f, amount * 0.5f);
		this.min -= vector;
		this.max += vector;
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x00017ED5 File Offset: 0x000160D5
	public void ExpandToFit(Vector3 pt)
	{
		this.min = this.min.Min(pt);
		this.max = this.max.Max(pt);
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x00017EFB File Offset: 0x000160FB
	public void ExpandToFit(AABB3 aabb)
	{
		this.min = this.min.Min(aabb.min);
		this.max = this.max.Max(aabb.max);
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x00017F2B File Offset: 0x0001612B
	public bool Contains(Vector3 pt)
	{
		return this.min.LessEqual(pt) && pt.Less(this.max);
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x00017F49 File Offset: 0x00016149
	public bool Contains(AABB3 aabb)
	{
		return this.Contains(aabb.min) && this.Contains(aabb.max);
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x00017F67 File Offset: 0x00016167
	public bool Intersects(AABB3 aabb)
	{
		return this.min.LessEqual(aabb.max) && aabb.min.Less(this.max);
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x00017F90 File Offset: 0x00016190
	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		AABB3 aabb = (AABB3)obj;
		return this.min == aabb.min && this.max == aabb.max;
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x00017FCF File Offset: 0x000161CF
	public override int GetHashCode()
	{
		return this.min.GetHashCode() ^ this.max.GetHashCode();
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x00017FF4 File Offset: 0x000161F4
	public unsafe void Transform(Matrix4x4 t)
	{
		Vector3* ptr;
		checked
		{
			ptr = stackalloc Vector3[unchecked((UIntPtr)8) * (UIntPtr)sizeof(Vector3)];
			*ptr = this.min;
		}
		ptr[1] = new Vector3(this.min.x, this.min.y, this.max.z);
		ptr[2] = new Vector3(this.min.x, this.max.y, this.min.z);
		ptr[3] = new Vector3(this.max.x, this.min.y, this.min.z);
		ptr[4] = new Vector3(this.min.x, this.max.y, this.max.z);
		ptr[5] = new Vector3(this.max.x, this.min.y, this.max.z);
		ptr[6] = new Vector3(this.max.x, this.max.y, this.min.z);
		ptr[7] = this.max;
		this.min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		this.max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
		for (int i = 0; i < 8; i++)
		{
			this.ExpandToFit(t * ptr[i]);
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x060004D6 RID: 1238 RVA: 0x000181D6 File Offset: 0x000163D6
	public float Width
	{
		get
		{
			return this.max.x - this.min.x;
		}
	}

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x060004D7 RID: 1239 RVA: 0x000181EF File Offset: 0x000163EF
	public float Height
	{
		get
		{
			return this.max.y - this.min.y;
		}
	}

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00018208 File Offset: 0x00016408
	public float Depth
	{
		get
		{
			return this.max.z - this.min.z;
		}
	}

	// Token: 0x04000505 RID: 1285
	public Vector3 min;

	// Token: 0x04000506 RID: 1286
	public Vector3 max;
}
