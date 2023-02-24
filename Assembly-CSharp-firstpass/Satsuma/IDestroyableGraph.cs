using System;

namespace Satsuma
{
	// Token: 0x02000260 RID: 608
	public interface IDestroyableGraph : IClearable
	{
		// Token: 0x06001271 RID: 4721
		bool DeleteNode(Node node);

		// Token: 0x06001272 RID: 4722
		bool DeleteArc(Arc arc);
	}
}
