using System;

namespace Satsuma
{
	// Token: 0x0200025A RID: 602
	public interface IDisjointSet<T> : IReadOnlyDisjointSet<T>, IClearable
	{
		// Token: 0x06001254 RID: 4692
		DisjointSetSet<T> Union(DisjointSetSet<T> a, DisjointSetSet<T> b);
	}
}
