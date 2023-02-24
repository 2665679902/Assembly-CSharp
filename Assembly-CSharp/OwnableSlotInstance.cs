using System;
using System.Diagnostics;

// Token: 0x02000882 RID: 2178
[DebuggerDisplay("{slot.Id}")]
public class OwnableSlotInstance : AssignableSlotInstance
{
	// Token: 0x06003E7A RID: 15994 RVA: 0x0015D80D File Offset: 0x0015BA0D
	public OwnableSlotInstance(Assignables assignables, OwnableSlot slot)
		: base(assignables, slot)
	{
	}
}
