using System;

// Token: 0x02000114 RID: 276
public class Tuple<T, U, V> : IEquatable<global::Tuple<T, U, V>>
{
	// Token: 0x06000947 RID: 2375 RVA: 0x00024DAE File Offset: 0x00022FAE
	public Tuple(T a, U b, V c)
	{
		this.first = a;
		this.second = b;
		this.third = c;
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x00024DCC File Offset: 0x00022FCC
	public bool Equals(global::Tuple<T, U, V> other)
	{
		return this.first.Equals(other.first) && this.second.Equals(other.second) && this.third.Equals(other.third);
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x00024E33 File Offset: 0x00023033
	public override int GetHashCode()
	{
		return this.first.GetHashCode() ^ this.second.GetHashCode() ^ this.third.GetHashCode();
	}

	// Token: 0x0400068C RID: 1676
	public T first;

	// Token: 0x0400068D RID: 1677
	public U second;

	// Token: 0x0400068E RID: 1678
	public V third;
}
