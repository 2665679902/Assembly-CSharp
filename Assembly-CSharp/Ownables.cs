using System;
using KSerialization;

// Token: 0x02000883 RID: 2179
[SerializationConfig(MemberSerialization.OptIn)]
public class Ownables : Assignables
{
	// Token: 0x06003E7B RID: 15995 RVA: 0x0015D817 File Offset: 0x0015BA17
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06003E7C RID: 15996 RVA: 0x0015D820 File Offset: 0x0015BA20
	public void UnassignAll()
	{
		foreach (AssignableSlotInstance assignableSlotInstance in this.slots)
		{
			if (assignableSlotInstance.assignable != null)
			{
				assignableSlotInstance.assignable.Unassign();
			}
		}
	}
}
