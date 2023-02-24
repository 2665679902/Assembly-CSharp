using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000554 RID: 1364
[AddComponentMenu("KMonoBehaviour/scripts/Assignables")]
public class Assignables : KMonoBehaviour
{
	// Token: 0x1700018A RID: 394
	// (get) Token: 0x060020B3 RID: 8371 RVA: 0x000B2600 File Offset: 0x000B0800
	public List<AssignableSlotInstance> Slots
	{
		get
		{
			return this.slots;
		}
	}

	// Token: 0x060020B4 RID: 8372 RVA: 0x000B2608 File Offset: 0x000B0808
	protected IAssignableIdentity GetAssignableIdentity()
	{
		MinionIdentity component = base.GetComponent<MinionIdentity>();
		if (component != null)
		{
			return component.assignableProxy.Get();
		}
		return base.GetComponent<MinionAssignablesProxy>();
	}

	// Token: 0x060020B5 RID: 8373 RVA: 0x000B2637 File Offset: 0x000B0837
	protected override void OnSpawn()
	{
		base.OnSpawn();
		GameUtil.SubscribeToTags<Assignables>(this, Assignables.OnDeadTagAddedDelegate, true);
	}

	// Token: 0x060020B6 RID: 8374 RVA: 0x000B264C File Offset: 0x000B084C
	private void OnDeath(object data)
	{
		foreach (AssignableSlotInstance assignableSlotInstance in this.slots)
		{
			assignableSlotInstance.Unassign(true);
		}
	}

	// Token: 0x060020B7 RID: 8375 RVA: 0x000B26A0 File Offset: 0x000B08A0
	public void Add(AssignableSlotInstance slot_instance)
	{
		this.slots.Add(slot_instance);
	}

	// Token: 0x060020B8 RID: 8376 RVA: 0x000B26B0 File Offset: 0x000B08B0
	public Assignable GetAssignable(AssignableSlot slot)
	{
		AssignableSlotInstance slot2 = this.GetSlot(slot);
		if (slot2 == null)
		{
			return null;
		}
		return slot2.assignable;
	}

	// Token: 0x060020B9 RID: 8377 RVA: 0x000B26D0 File Offset: 0x000B08D0
	public AssignableSlotInstance GetSlot(AssignableSlot slot)
	{
		global::Debug.Assert(this.slots.Count > 0, "GetSlot called with no slots configured");
		if (slot == null)
		{
			return null;
		}
		foreach (AssignableSlotInstance assignableSlotInstance in this.slots)
		{
			if (assignableSlotInstance.slot == slot)
			{
				return assignableSlotInstance;
			}
		}
		return null;
	}

	// Token: 0x060020BA RID: 8378 RVA: 0x000B274C File Offset: 0x000B094C
	public Assignable AutoAssignSlot(AssignableSlot slot)
	{
		Assignable assignable = this.GetAssignable(slot);
		if (assignable != null)
		{
			return assignable;
		}
		GameObject targetGameObject = base.GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
		if (targetGameObject == null)
		{
			global::Debug.LogWarning("AutoAssignSlot failed, proxy game object was null.");
			return null;
		}
		Navigator component = targetGameObject.GetComponent<Navigator>();
		IAssignableIdentity assignableIdentity = this.GetAssignableIdentity();
		int num = int.MaxValue;
		foreach (Assignable assignable2 in Game.Instance.assignmentManager)
		{
			if (!(assignable2 == null) && !assignable2.IsAssigned() && assignable2.slot == slot && assignable2.CanAutoAssignTo(assignableIdentity))
			{
				int navigationCost = assignable2.GetNavigationCost(component);
				if (navigationCost != -1 && navigationCost < num)
				{
					num = navigationCost;
					assignable = assignable2;
				}
			}
		}
		if (assignable != null)
		{
			assignable.Assign(assignableIdentity);
		}
		return assignable;
	}

	// Token: 0x060020BB RID: 8379 RVA: 0x000B283C File Offset: 0x000B0A3C
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		foreach (AssignableSlotInstance assignableSlotInstance in this.slots)
		{
			assignableSlotInstance.Unassign(true);
		}
	}

	// Token: 0x040012E4 RID: 4836
	protected List<AssignableSlotInstance> slots = new List<AssignableSlotInstance>();

	// Token: 0x040012E5 RID: 4837
	private static readonly EventSystem.IntraObjectHandler<Assignables> OnDeadTagAddedDelegate = GameUtil.CreateHasTagHandler<Assignables>(GameTags.Dead, delegate(Assignables component, object data)
	{
		component.OnDeath(data);
	});
}
