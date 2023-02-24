using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x02000273 RID: 627
	public interface IReadOnlyPriorityQueue<TElement, TPriority>
	{
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600132B RID: 4907
		int Count { get; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600132C RID: 4908
		IEnumerable<KeyValuePair<TElement, TPriority>> Items { get; }

		// Token: 0x0600132D RID: 4909
		bool Contains(TElement element);

		// Token: 0x0600132E RID: 4910
		bool TryGetPriority(TElement element, out TPriority priority);

		// Token: 0x0600132F RID: 4911
		TElement Peek();

		// Token: 0x06001330 RID: 4912
		TElement Peek(out TPriority priority);
	}
}
