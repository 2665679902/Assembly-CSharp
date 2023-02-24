using System;
using System.Collections.Generic;
using System.Diagnostics;
using KSerialization;
using UnityEngine;

// Token: 0x02000080 RID: 128
[DebuggerDisplay("{r}, {q}")]
[SerializationConfig(MemberSerialization.OptIn)]
[Serializable]
public struct AxialI : IEquatable<AxialI>
{
	// Token: 0x170000AA RID: 170
	// (get) Token: 0x06000504 RID: 1284 RVA: 0x00018A11 File Offset: 0x00016C11
	// (set) Token: 0x06000505 RID: 1285 RVA: 0x00018A19 File Offset: 0x00016C19
	public int R
	{
		get
		{
			return this.r;
		}
		set
		{
			this.r = value;
		}
	}

	// Token: 0x170000AB RID: 171
	// (get) Token: 0x06000506 RID: 1286 RVA: 0x00018A22 File Offset: 0x00016C22
	// (set) Token: 0x06000507 RID: 1287 RVA: 0x00018A2A File Offset: 0x00016C2A
	public int Q
	{
		get
		{
			return this.q;
		}
		set
		{
			this.q = value;
		}
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x00018A33 File Offset: 0x00016C33
	public AxialI(int a, int b)
	{
		this.r = a;
		this.q = b;
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x00018A44 File Offset: 0x00016C44
	public Vector3I ToCube()
	{
		int num = this.q;
		int num2 = this.r;
		int num3 = -num - num2;
		return new Vector3I(num, num3, num2);
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x00018A6A File Offset: 0x00016C6A
	public Vector3 ToWorld()
	{
		return AxialUtil.AxialToWorld((float)this.r, (float)this.q);
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00018A80 File Offset: 0x00016C80
	public Vector2 ToWorld2D()
	{
		Vector3 vector = this.ToWorld();
		return new Vector2(vector.x, vector.y);
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x00018AA5 File Offset: 0x00016CA5
	public static AxialI operator +(AxialI u, AxialI v)
	{
		return new AxialI(u.r + v.r, u.q + v.q);
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x00018AC6 File Offset: 0x00016CC6
	public static AxialI operator -(AxialI u, AxialI v)
	{
		return new AxialI(u.r - v.r, u.q - v.q);
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x00018AE7 File Offset: 0x00016CE7
	public static AxialI operator +(AxialI u, int scalar)
	{
		return new AxialI(u.r + scalar, u.q + scalar);
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x00018AFE File Offset: 0x00016CFE
	public static AxialI operator -(AxialI u, int scalar)
	{
		return new AxialI(u.r - scalar, u.q - scalar);
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x00018B15 File Offset: 0x00016D15
	public static AxialI operator *(AxialI v, int s)
	{
		return new AxialI(v.r * s, v.q * s);
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x00018B2C File Offset: 0x00016D2C
	public static AxialI operator /(AxialI v, int s)
	{
		return new AxialI(v.r / s, v.q / s);
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x00018B43 File Offset: 0x00016D43
	public static bool operator ==(AxialI u, AxialI v)
	{
		return u.r == v.r && u.q == v.q;
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x00018B63 File Offset: 0x00016D63
	public static bool operator !=(AxialI u, AxialI v)
	{
		return u.r != v.r || u.q != v.q;
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x00018B86 File Offset: 0x00016D86
	public static bool operator <(AxialI u, AxialI v)
	{
		return u.r < v.r && u.q < v.q;
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x00018BA6 File Offset: 0x00016DA6
	public static bool operator >(AxialI u, AxialI v)
	{
		return u.r > v.r && u.q > v.q;
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x00018BC6 File Offset: 0x00016DC6
	public static bool operator <=(AxialI u, AxialI v)
	{
		return u.r <= v.r && u.q <= v.q;
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x00018BE9 File Offset: 0x00016DE9
	public static bool operator >=(AxialI u, AxialI v)
	{
		return u.r >= v.r && u.q >= v.q;
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x00018C0C File Offset: 0x00016E0C
	public override bool Equals(object obj)
	{
		bool flag;
		try
		{
			AxialI axialI = (AxialI)obj;
			flag = axialI.r == this.r && axialI.q == this.q;
		}
		catch
		{
			flag = false;
		}
		return flag;
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x00018C58 File Offset: 0x00016E58
	public bool Equals(AxialI v)
	{
		return v.r == this.r && v.q == this.q;
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x00018C78 File Offset: 0x00016E78
	public override int GetHashCode()
	{
		return this.r ^ this.q;
	}

	// Token: 0x0400051B RID: 1307
	public static readonly AxialI ZERO = new AxialI(0, 0);

	// Token: 0x0400051C RID: 1308
	public static readonly AxialI NORTHWEST = new AxialI(0, -1);

	// Token: 0x0400051D RID: 1309
	public static readonly AxialI NORTHEAST = new AxialI(1, -1);

	// Token: 0x0400051E RID: 1310
	public static readonly AxialI EAST = new AxialI(1, 0);

	// Token: 0x0400051F RID: 1311
	public static readonly AxialI SOUTHEAST = new AxialI(0, 1);

	// Token: 0x04000520 RID: 1312
	public static readonly AxialI SOUTHWEST = new AxialI(-1, 1);

	// Token: 0x04000521 RID: 1313
	public static readonly AxialI WEST = new AxialI(-1, 0);

	// Token: 0x04000522 RID: 1314
	public static readonly List<AxialI> DIRECTIONS = new List<AxialI>
	{
		AxialI.NORTHWEST,
		AxialI.NORTHEAST,
		AxialI.EAST,
		AxialI.SOUTHEAST,
		AxialI.SOUTHWEST,
		AxialI.WEST
	};

	// Token: 0x04000523 RID: 1315
	public static readonly List<AxialI> CLOCKWISE = new List<AxialI>
	{
		AxialI.EAST,
		AxialI.SOUTHEAST,
		AxialI.SOUTHWEST,
		AxialI.WEST,
		AxialI.NORTHWEST,
		AxialI.NORTHEAST
	};

	// Token: 0x04000524 RID: 1316
	[Serialize]
	public int r;

	// Token: 0x04000525 RID: 1317
	[Serialize]
	public int q;
}
