using System;

// Token: 0x02000361 RID: 865
public readonly struct Padding
{
	// Token: 0x17000052 RID: 82
	// (get) Token: 0x06001181 RID: 4481 RVA: 0x0005D8B9 File Offset: 0x0005BAB9
	public float Width
	{
		get
		{
			return this.left + this.right;
		}
	}

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x06001182 RID: 4482 RVA: 0x0005D8C8 File Offset: 0x0005BAC8
	public float Height
	{
		get
		{
			return this.top + this.bottom;
		}
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x0005D8D7 File Offset: 0x0005BAD7
	public Padding(float left, float right, float top, float bottom)
	{
		this.top = top;
		this.bottom = bottom;
		this.left = left;
		this.right = right;
	}

	// Token: 0x06001184 RID: 4484 RVA: 0x0005D8F6 File Offset: 0x0005BAF6
	public static Padding All(float padding)
	{
		return new Padding(padding, padding, padding, padding);
	}

	// Token: 0x06001185 RID: 4485 RVA: 0x0005D901 File Offset: 0x0005BB01
	public static Padding Symmetric(float horizontal, float vertical)
	{
		return new Padding(horizontal, horizontal, vertical, vertical);
	}

	// Token: 0x06001186 RID: 4486 RVA: 0x0005D90C File Offset: 0x0005BB0C
	public static Padding Only(float left = 0f, float right = 0f, float top = 0f, float bottom = 0f)
	{
		return new Padding(left, right, top, bottom);
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x0005D917 File Offset: 0x0005BB17
	public static Padding Vertical(float vertical)
	{
		return new Padding(0f, 0f, vertical, vertical);
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x0005D92A File Offset: 0x0005BB2A
	public static Padding Horizontal(float horizontal)
	{
		return new Padding(horizontal, horizontal, 0f, 0f);
	}

	// Token: 0x06001189 RID: 4489 RVA: 0x0005D93D File Offset: 0x0005BB3D
	public static Padding Top(float amount)
	{
		return new Padding(0f, 0f, amount, 0f);
	}

	// Token: 0x0600118A RID: 4490 RVA: 0x0005D954 File Offset: 0x0005BB54
	public static Padding Left(float amount)
	{
		return new Padding(amount, 0f, 0f, 0f);
	}

	// Token: 0x0600118B RID: 4491 RVA: 0x0005D96B File Offset: 0x0005BB6B
	public static Padding Bottom(float amount)
	{
		return new Padding(0f, 0f, 0f, amount);
	}

	// Token: 0x0600118C RID: 4492 RVA: 0x0005D982 File Offset: 0x0005BB82
	public static Padding Right(float amount)
	{
		return new Padding(0f, amount, 0f, 0f);
	}

	// Token: 0x0600118D RID: 4493 RVA: 0x0005D999 File Offset: 0x0005BB99
	public static Padding operator +(Padding a, Padding b)
	{
		return new Padding(a.left + b.left, a.right + b.right, a.top + b.top, a.bottom + b.bottom);
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x0005D9D4 File Offset: 0x0005BBD4
	public static Padding operator -(Padding a, Padding b)
	{
		return new Padding(a.left - b.left, a.right - b.right, a.top - b.top, a.bottom - b.bottom);
	}

	// Token: 0x0600118F RID: 4495 RVA: 0x0005DA0F File Offset: 0x0005BC0F
	public static Padding operator *(float f, Padding p)
	{
		return p * f;
	}

	// Token: 0x06001190 RID: 4496 RVA: 0x0005DA18 File Offset: 0x0005BC18
	public static Padding operator *(Padding p, float f)
	{
		return new Padding(p.left * f, p.right * f, p.top * f, p.bottom * f);
	}

	// Token: 0x06001191 RID: 4497 RVA: 0x0005DA3F File Offset: 0x0005BC3F
	public static Padding operator /(Padding p, float f)
	{
		return new Padding(p.left / f, p.right / f, p.top / f, p.bottom / f);
	}

	// Token: 0x04000981 RID: 2433
	public readonly float top;

	// Token: 0x04000982 RID: 2434
	public readonly float bottom;

	// Token: 0x04000983 RID: 2435
	public readonly float left;

	// Token: 0x04000984 RID: 2436
	public readonly float right;
}
