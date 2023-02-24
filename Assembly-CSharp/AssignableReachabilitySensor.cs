using System;

// Token: 0x020003E8 RID: 1000
public class AssignableReachabilitySensor : Sensor
{
	// Token: 0x060014C5 RID: 5317 RVA: 0x0006CEE4 File Offset: 0x0006B0E4
	public AssignableReachabilitySensor(Sensors sensors)
		: base(sensors)
	{
		MinionAssignablesProxy minionAssignablesProxy = base.gameObject.GetComponent<MinionIdentity>().assignableProxy.Get();
		minionAssignablesProxy.ConfigureAssignableSlots();
		Assignables[] components = minionAssignablesProxy.GetComponents<Assignables>();
		if (components.Length == 0)
		{
			Debug.LogError(base.gameObject.GetProperName() + ": No 'Assignables' components found for AssignableReachabilitySensor");
		}
		int num = 0;
		foreach (Assignables assignables in components)
		{
			num += assignables.Slots.Count;
		}
		this.slots = new AssignableReachabilitySensor.SlotEntry[num];
		int num2 = 0;
		foreach (Assignables assignables2 in components)
		{
			for (int k = 0; k < assignables2.Slots.Count; k++)
			{
				this.slots[num2++].slot = assignables2.Slots[k];
			}
		}
		this.navigator = base.GetComponent<Navigator>();
	}

	// Token: 0x060014C6 RID: 5318 RVA: 0x0006CFCC File Offset: 0x0006B1CC
	public bool IsReachable(AssignableSlot slot)
	{
		for (int i = 0; i < this.slots.Length; i++)
		{
			if (this.slots[i].slot.slot == slot)
			{
				return this.slots[i].isReachable;
			}
		}
		Debug.LogError("Could not find slot: " + ((slot != null) ? slot.ToString() : null));
		return false;
	}

	// Token: 0x060014C7 RID: 5319 RVA: 0x0006D034 File Offset: 0x0006B234
	public override void Update()
	{
		for (int i = 0; i < this.slots.Length; i++)
		{
			AssignableReachabilitySensor.SlotEntry slotEntry = this.slots[i];
			AssignableSlotInstance slot = slotEntry.slot;
			if (slot.IsAssigned())
			{
				bool flag = slot.assignable.GetNavigationCost(this.navigator) != -1;
				Operational component = slot.assignable.GetComponent<Operational>();
				if (component != null)
				{
					flag = flag && component.IsOperational;
				}
				if (flag != slotEntry.isReachable)
				{
					slotEntry.isReachable = flag;
					this.slots[i] = slotEntry;
					base.Trigger(334784980, slotEntry);
				}
			}
			else if (slotEntry.isReachable)
			{
				slotEntry.isReachable = false;
				this.slots[i] = slotEntry;
				base.Trigger(334784980, slotEntry);
			}
		}
	}

	// Token: 0x04000B9E RID: 2974
	private AssignableReachabilitySensor.SlotEntry[] slots;

	// Token: 0x04000B9F RID: 2975
	private Navigator navigator;

	// Token: 0x02001002 RID: 4098
	private struct SlotEntry
	{
		// Token: 0x04005635 RID: 22069
		public AssignableSlotInstance slot;

		// Token: 0x04005636 RID: 22070
		public bool isReachable;
	}
}
