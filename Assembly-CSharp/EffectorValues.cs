using System;

// Token: 0x02000731 RID: 1841
[Serializable]
public struct EffectorValues
{
	// Token: 0x06003278 RID: 12920 RVA: 0x00110B84 File Offset: 0x0010ED84
	public EffectorValues(int amt, int rad)
	{
		this.amount = amt;
		this.radius = rad;
	}

	// Token: 0x06003279 RID: 12921 RVA: 0x00110B94 File Offset: 0x0010ED94
	public override bool Equals(object obj)
	{
		return obj is EffectorValues && this.Equals((EffectorValues)obj);
	}

	// Token: 0x0600327A RID: 12922 RVA: 0x00110BAC File Offset: 0x0010EDAC
	public bool Equals(EffectorValues p)
	{
		return p != null && (this == p || (!(base.GetType() != p.GetType()) && this.amount == p.amount && this.radius == p.radius));
	}

	// Token: 0x0600327B RID: 12923 RVA: 0x00110C1A File Offset: 0x0010EE1A
	public override int GetHashCode()
	{
		return this.amount ^ this.radius;
	}

	// Token: 0x0600327C RID: 12924 RVA: 0x00110C29 File Offset: 0x0010EE29
	public static bool operator ==(EffectorValues lhs, EffectorValues rhs)
	{
		if (lhs == null)
		{
			return rhs == null;
		}
		return lhs.Equals(rhs);
	}

	// Token: 0x0600327D RID: 12925 RVA: 0x00110C47 File Offset: 0x0010EE47
	public static bool operator !=(EffectorValues lhs, EffectorValues rhs)
	{
		return !(lhs == rhs);
	}

	// Token: 0x04001EBF RID: 7871
	public int amount;

	// Token: 0x04001EC0 RID: 7872
	public int radius;
}
