using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x0200027D RID: 637
	public interface ITsp<TNode>
	{
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060013A6 RID: 5030
		IEnumerable<TNode> Tour { get; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060013A7 RID: 5031
		double TourCost { get; }
	}
}
