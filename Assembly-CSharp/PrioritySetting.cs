using System;

// Token: 0x020003A2 RID: 930
public struct PrioritySetting : IComparable<PrioritySetting>
{
	// Token: 0x060012F4 RID: 4852 RVA: 0x00064BDC File Offset: 0x00062DDC
	public override int GetHashCode()
	{
		return ((int)((int)this.priority_class << 28)).GetHashCode() ^ this.priority_value.GetHashCode();
	}

	// Token: 0x060012F5 RID: 4853 RVA: 0x00064C06 File Offset: 0x00062E06
	public static bool operator ==(PrioritySetting lhs, PrioritySetting rhs)
	{
		return lhs.Equals(rhs);
	}

	// Token: 0x060012F6 RID: 4854 RVA: 0x00064C1B File Offset: 0x00062E1B
	public static bool operator !=(PrioritySetting lhs, PrioritySetting rhs)
	{
		return !lhs.Equals(rhs);
	}

	// Token: 0x060012F7 RID: 4855 RVA: 0x00064C33 File Offset: 0x00062E33
	public static bool operator <=(PrioritySetting lhs, PrioritySetting rhs)
	{
		return lhs.CompareTo(rhs) <= 0;
	}

	// Token: 0x060012F8 RID: 4856 RVA: 0x00064C43 File Offset: 0x00062E43
	public static bool operator >=(PrioritySetting lhs, PrioritySetting rhs)
	{
		return lhs.CompareTo(rhs) >= 0;
	}

	// Token: 0x060012F9 RID: 4857 RVA: 0x00064C53 File Offset: 0x00062E53
	public static bool operator <(PrioritySetting lhs, PrioritySetting rhs)
	{
		return lhs.CompareTo(rhs) < 0;
	}

	// Token: 0x060012FA RID: 4858 RVA: 0x00064C60 File Offset: 0x00062E60
	public static bool operator >(PrioritySetting lhs, PrioritySetting rhs)
	{
		return lhs.CompareTo(rhs) > 0;
	}

	// Token: 0x060012FB RID: 4859 RVA: 0x00064C6D File Offset: 0x00062E6D
	public override bool Equals(object obj)
	{
		return obj is PrioritySetting && ((PrioritySetting)obj).priority_class == this.priority_class && ((PrioritySetting)obj).priority_value == this.priority_value;
	}

	// Token: 0x060012FC RID: 4860 RVA: 0x00064CA4 File Offset: 0x00062EA4
	public int CompareTo(PrioritySetting other)
	{
		if (this.priority_class > other.priority_class)
		{
			return 1;
		}
		if (this.priority_class < other.priority_class)
		{
			return -1;
		}
		if (this.priority_value > other.priority_value)
		{
			return 1;
		}
		if (this.priority_value < other.priority_value)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x060012FD RID: 4861 RVA: 0x00064CF2 File Offset: 0x00062EF2
	public PrioritySetting(PriorityScreen.PriorityClass priority_class, int priority_value)
	{
		this.priority_class = priority_class;
		this.priority_value = priority_value;
	}

	// Token: 0x04000A37 RID: 2615
	public PriorityScreen.PriorityClass priority_class;

	// Token: 0x04000A38 RID: 2616
	public int priority_value;
}
