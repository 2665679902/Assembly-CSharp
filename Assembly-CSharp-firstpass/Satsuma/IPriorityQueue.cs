using System;

namespace Satsuma
{
	// Token: 0x02000274 RID: 628
	public interface IPriorityQueue<TElement, TPriority> : IReadOnlyPriorityQueue<TElement, TPriority>, IClearable
	{
		// Token: 0x1700026E RID: 622
		TPriority this[TElement element] { get; set; }

		// Token: 0x06001333 RID: 4915
		bool Remove(TElement element);

		// Token: 0x06001334 RID: 4916
		bool Pop();
	}
}
