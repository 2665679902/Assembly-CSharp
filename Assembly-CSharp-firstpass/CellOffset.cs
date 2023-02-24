using System;
using UnityEngine;

// Token: 0x02000088 RID: 136
[Serializable]
public struct CellOffset : IEquatable<CellOffset>
{
	// Token: 0x170000AE RID: 174
	// (get) Token: 0x06000553 RID: 1363 RVA: 0x0001A286 File Offset: 0x00018486
	public static CellOffset none
	{
		get
		{
			return new CellOffset(0, 0);
		}
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x0001A28F File Offset: 0x0001848F
	public CellOffset(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x0001A29F File Offset: 0x0001849F
	public CellOffset(Vector2 offset)
	{
		this.x = Mathf.RoundToInt(offset.x);
		this.y = Mathf.RoundToInt(offset.y);
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x0001A2C3 File Offset: 0x000184C3
	public Vector2I ToVector2I()
	{
		return new Vector2I(this.x, this.y);
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x0001A2D6 File Offset: 0x000184D6
	public Vector3 ToVector3()
	{
		return new Vector3((float)this.x, (float)this.y, 0f);
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x0001A2F0 File Offset: 0x000184F0
	public CellOffset Offset(CellOffset offset)
	{
		return new CellOffset(this.x + offset.x, this.y + offset.y);
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x0001A311 File Offset: 0x00018511
	public int GetOffsetDistance()
	{
		return Math.Abs(this.x) + Math.Abs(this.y);
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x0001A32A File Offset: 0x0001852A
	public static CellOffset operator +(CellOffset a, CellOffset b)
	{
		return new CellOffset(a.x + b.x, a.y + b.y);
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x0001A34B File Offset: 0x0001854B
	public static CellOffset operator -(CellOffset a, CellOffset b)
	{
		return new CellOffset(a.x - b.x, a.y - b.y);
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x0001A36C File Offset: 0x0001856C
	public static CellOffset operator *(CellOffset offset, int value)
	{
		return new CellOffset(offset.x * value, offset.y * value);
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x0001A383 File Offset: 0x00018583
	public static CellOffset operator *(int value, CellOffset offset)
	{
		return new CellOffset(offset.x * value, offset.y * value);
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x0001A39C File Offset: 0x0001859C
	public override bool Equals(object obj)
	{
		CellOffset cellOffset = (CellOffset)obj;
		return this.x == cellOffset.x && this.y == cellOffset.y;
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x0001A3CE File Offset: 0x000185CE
	public bool Equals(CellOffset offset)
	{
		return this.x == offset.x && this.y == offset.y;
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x0001A3EE File Offset: 0x000185EE
	public override int GetHashCode()
	{
		return this.x + this.y * 8192;
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x0001A403 File Offset: 0x00018603
	public static bool operator ==(CellOffset a, CellOffset b)
	{
		return a.x == b.x && a.y == b.y;
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x0001A423 File Offset: 0x00018623
	public static bool operator !=(CellOffset a, CellOffset b)
	{
		return a.x != b.x || a.y != b.y;
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x0001A448 File Offset: 0x00018648
	public override string ToString()
	{
		return string.Concat(new string[]
		{
			"(",
			this.x.ToString(),
			",",
			this.y.ToString(),
			")"
		});
	}

	// Token: 0x0400053D RID: 1341
	public int x;

	// Token: 0x0400053E RID: 1342
	public int y;
}
