using System;
using UnityEngine;

// Token: 0x02000AE5 RID: 2789
public readonly struct Alignment
{
	// Token: 0x06005556 RID: 21846 RVA: 0x001EDE49 File Offset: 0x001EC049
	public Alignment(float x, float y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x06005557 RID: 21847 RVA: 0x001EDE59 File Offset: 0x001EC059
	public static Alignment Custom(float x, float y)
	{
		return new Alignment(x, y);
	}

	// Token: 0x06005558 RID: 21848 RVA: 0x001EDE62 File Offset: 0x001EC062
	public static Alignment TopLeft()
	{
		return new Alignment(0f, 1f);
	}

	// Token: 0x06005559 RID: 21849 RVA: 0x001EDE73 File Offset: 0x001EC073
	public static Alignment Top()
	{
		return new Alignment(0.5f, 1f);
	}

	// Token: 0x0600555A RID: 21850 RVA: 0x001EDE84 File Offset: 0x001EC084
	public static Alignment TopRight()
	{
		return new Alignment(1f, 1f);
	}

	// Token: 0x0600555B RID: 21851 RVA: 0x001EDE95 File Offset: 0x001EC095
	public static Alignment Left()
	{
		return new Alignment(0f, 0.5f);
	}

	// Token: 0x0600555C RID: 21852 RVA: 0x001EDEA6 File Offset: 0x001EC0A6
	public static Alignment Center()
	{
		return new Alignment(0.5f, 0.5f);
	}

	// Token: 0x0600555D RID: 21853 RVA: 0x001EDEB7 File Offset: 0x001EC0B7
	public static Alignment Right()
	{
		return new Alignment(1f, 0.5f);
	}

	// Token: 0x0600555E RID: 21854 RVA: 0x001EDEC8 File Offset: 0x001EC0C8
	public static Alignment BottomLeft()
	{
		return new Alignment(0f, 0f);
	}

	// Token: 0x0600555F RID: 21855 RVA: 0x001EDED9 File Offset: 0x001EC0D9
	public static Alignment Bottom()
	{
		return new Alignment(0.5f, 0f);
	}

	// Token: 0x06005560 RID: 21856 RVA: 0x001EDEEA File Offset: 0x001EC0EA
	public static Alignment BottomRight()
	{
		return new Alignment(1f, 0f);
	}

	// Token: 0x06005561 RID: 21857 RVA: 0x001EDEFB File Offset: 0x001EC0FB
	public static implicit operator Vector2(Alignment a)
	{
		return new Vector2(a.x, a.y);
	}

	// Token: 0x06005562 RID: 21858 RVA: 0x001EDF0E File Offset: 0x001EC10E
	public static implicit operator Alignment(Vector2 v)
	{
		return new Alignment(v.x, v.y);
	}

	// Token: 0x040039FD RID: 14845
	public readonly float x;

	// Token: 0x040039FE RID: 14846
	public readonly float y;
}
