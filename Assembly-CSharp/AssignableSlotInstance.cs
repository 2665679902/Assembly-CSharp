using System;
using UnityEngine;

// Token: 0x02000553 RID: 1363
public abstract class AssignableSlotInstance
{
	// Token: 0x17000188 RID: 392
	// (get) Token: 0x060020AB RID: 8363 RVA: 0x000B2532 File Offset: 0x000B0732
	// (set) Token: 0x060020AC RID: 8364 RVA: 0x000B253A File Offset: 0x000B073A
	public Assignables assignables { get; private set; }

	// Token: 0x17000189 RID: 393
	// (get) Token: 0x060020AD RID: 8365 RVA: 0x000B2543 File Offset: 0x000B0743
	public GameObject gameObject
	{
		get
		{
			return this.assignables.gameObject;
		}
	}

	// Token: 0x060020AE RID: 8366 RVA: 0x000B2550 File Offset: 0x000B0750
	public AssignableSlotInstance(Assignables assignables, AssignableSlot slot)
	{
		this.slot = slot;
		this.assignables = assignables;
	}

	// Token: 0x060020AF RID: 8367 RVA: 0x000B2566 File Offset: 0x000B0766
	public void Assign(Assignable assignable)
	{
		if (this.assignable == assignable)
		{
			return;
		}
		this.Unassign(false);
		this.assignable = assignable;
		this.assignables.Trigger(-1585839766, this);
	}

	// Token: 0x060020B0 RID: 8368 RVA: 0x000B2598 File Offset: 0x000B0798
	public virtual void Unassign(bool trigger_event = true)
	{
		if (this.unassigning)
		{
			return;
		}
		if (this.IsAssigned())
		{
			this.unassigning = true;
			this.assignable.Unassign();
			if (trigger_event)
			{
				this.assignables.Trigger(-1585839766, this);
			}
			this.assignable = null;
			this.unassigning = false;
		}
	}

	// Token: 0x060020B1 RID: 8369 RVA: 0x000B25EA File Offset: 0x000B07EA
	public bool IsAssigned()
	{
		return this.assignable != null;
	}

	// Token: 0x060020B2 RID: 8370 RVA: 0x000B25F8 File Offset: 0x000B07F8
	public bool IsUnassigning()
	{
		return this.unassigning;
	}

	// Token: 0x040012E0 RID: 4832
	public AssignableSlot slot;

	// Token: 0x040012E1 RID: 4833
	public Assignable assignable;

	// Token: 0x040012E3 RID: 4835
	private bool unassigning;
}
