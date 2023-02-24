using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x02000270 RID: 624
	public interface IFlow<TCapacity>
	{
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06001305 RID: 4869
		IGraph Graph { get; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06001306 RID: 4870
		Func<Arc, TCapacity> Capacity { get; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06001307 RID: 4871
		Node Source { get; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06001308 RID: 4872
		Node Target { get; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06001309 RID: 4873
		TCapacity FlowSize { get; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600130A RID: 4874
		IEnumerable<KeyValuePair<Arc, TCapacity>> NonzeroArcs { get; }

		// Token: 0x0600130B RID: 4875
		TCapacity Flow(Arc arc);
	}
}
