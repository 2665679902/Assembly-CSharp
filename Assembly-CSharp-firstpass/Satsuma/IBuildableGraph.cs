using System;

namespace Satsuma
{
	// Token: 0x0200025F RID: 607
	public interface IBuildableGraph : IClearable
	{
		// Token: 0x0600126F RID: 4719
		Node AddNode();

		// Token: 0x06001270 RID: 4720
		Arc AddArc(Node u, Node v, Directedness directedness);
	}
}
