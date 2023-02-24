using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x02000259 RID: 601
	public interface IReadOnlyDisjointSet<T>
	{
		// Token: 0x06001252 RID: 4690
		DisjointSetSet<T> WhereIs(T element);

		// Token: 0x06001253 RID: 4691
		IEnumerable<T> Elements(DisjointSetSet<T> aSet);
	}
}
