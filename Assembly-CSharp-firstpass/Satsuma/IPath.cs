using System;

namespace Satsuma
{
	// Token: 0x0200026C RID: 620
	public interface IPath : IGraph, IArcLookup
	{
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060012CC RID: 4812
		Node FirstNode { get; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060012CD RID: 4813
		Node LastNode { get; }

		// Token: 0x060012CE RID: 4814
		Arc NextArc(Node node);

		// Token: 0x060012CF RID: 4815
		Arc PrevArc(Node node);
	}
}
