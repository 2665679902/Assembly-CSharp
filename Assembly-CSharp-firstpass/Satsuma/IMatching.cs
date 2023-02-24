using System;

namespace Satsuma
{
	// Token: 0x02000266 RID: 614
	public interface IMatching : IGraph, IArcLookup
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06001284 RID: 4740
		IGraph Graph { get; }

		// Token: 0x06001285 RID: 4741
		Arc MatchedArc(Node node);
	}
}
