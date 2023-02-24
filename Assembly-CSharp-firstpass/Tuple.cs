using System;

// Token: 0x02000113 RID: 275
public class Tuple<T, U> : IEquatable<global::Tuple<T, U>>
{
	// Token: 0x06000944 RID: 2372 RVA: 0x00024D35 File Offset: 0x00022F35
	public Tuple(T a, U b)
	{
		this.first = a;
		this.second = b;
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x00024D4B File Offset: 0x00022F4B
	public bool Equals(global::Tuple<T, U> other)
	{
		return this.first.Equals(other.first) && this.second.Equals(other.second);
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x00024D89 File Offset: 0x00022F89
	public override int GetHashCode()
	{
		return this.first.GetHashCode() ^ this.second.GetHashCode();
	}

	// Token: 0x0400068A RID: 1674
	public T first;

	// Token: 0x0400068B RID: 1675
	public U second;
}
