using System;

namespace Satsuma
{
	// Token: 0x02000261 RID: 609
	public interface IArcLookup
	{
		// Token: 0x06001273 RID: 4723
		Node U(Arc arc);

		// Token: 0x06001274 RID: 4724
		Node V(Arc arc);

		// Token: 0x06001275 RID: 4725
		bool IsEdge(Arc arc);
	}
}
