using System;
using System.Collections.Generic;

namespace Satsuma
{
	// Token: 0x02000264 RID: 612
	public interface IGraph : IArcLookup
	{
		// Token: 0x06001279 RID: 4729
		IEnumerable<Node> Nodes();

		// Token: 0x0600127A RID: 4730
		IEnumerable<Arc> Arcs(ArcFilter filter = ArcFilter.All);

		// Token: 0x0600127B RID: 4731
		IEnumerable<Arc> Arcs(Node u, ArcFilter filter = ArcFilter.All);

		// Token: 0x0600127C RID: 4732
		IEnumerable<Arc> Arcs(Node u, Node v, ArcFilter filter = ArcFilter.All);

		// Token: 0x0600127D RID: 4733
		int NodeCount();

		// Token: 0x0600127E RID: 4734
		int ArcCount(ArcFilter filter = ArcFilter.All);

		// Token: 0x0600127F RID: 4735
		int ArcCount(Node u, ArcFilter filter = ArcFilter.All);

		// Token: 0x06001280 RID: 4736
		int ArcCount(Node u, Node v, ArcFilter filter = ArcFilter.All);

		// Token: 0x06001281 RID: 4737
		bool HasNode(Node node);

		// Token: 0x06001282 RID: 4738
		bool HasArc(Arc arc);
	}
}
