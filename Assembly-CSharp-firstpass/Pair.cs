using System;

// Token: 0x020000E1 RID: 225
[Serializable]
public struct Pair<T, U> : IEquatable<Pair<T, U>>
{
	// Token: 0x06000838 RID: 2104 RVA: 0x000214A4 File Offset: 0x0001F6A4
	public Pair(T a, U b)
	{
		this.first = a;
		this.second = b;
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x000214B4 File Offset: 0x0001F6B4
	public bool Equals(Pair<T, U> other)
	{
		return this.first.Equals(other.first) && this.second.Equals(other.second);
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x000214F2 File Offset: 0x0001F6F2
	public override int GetHashCode()
	{
		return this.first.GetHashCode() ^ this.second.GetHashCode();
	}

	// Token: 0x0400062B RID: 1579
	public T first;

	// Token: 0x0400062C RID: 1580
	public U second;
}
