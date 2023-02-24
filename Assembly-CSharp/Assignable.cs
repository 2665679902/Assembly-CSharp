using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

// Token: 0x02000551 RID: 1361
public abstract class Assignable : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x17000186 RID: 390
	// (get) Token: 0x06002093 RID: 8339 RVA: 0x000B1E49 File Offset: 0x000B0049
	public AssignableSlot slot
	{
		get
		{
			if (this._slot == null)
			{
				this._slot = Db.Get().AssignableSlots.Get(this.slotID);
			}
			return this._slot;
		}
	}

	// Token: 0x17000187 RID: 391
	// (get) Token: 0x06002094 RID: 8340 RVA: 0x000B1E74 File Offset: 0x000B0074
	public bool CanBeAssigned
	{
		get
		{
			return this.canBeAssigned;
		}
	}

	// Token: 0x14000010 RID: 16
	// (add) Token: 0x06002095 RID: 8341 RVA: 0x000B1E7C File Offset: 0x000B007C
	// (remove) Token: 0x06002096 RID: 8342 RVA: 0x000B1EB4 File Offset: 0x000B00B4
	public event Action<IAssignableIdentity> OnAssign;

	// Token: 0x06002097 RID: 8343 RVA: 0x000B1EE9 File Offset: 0x000B00E9
	[OnDeserialized]
	internal void OnDeserialized()
	{
	}

	// Token: 0x06002098 RID: 8344 RVA: 0x000B1EEC File Offset: 0x000B00EC
	private void RestoreAssignee()
	{
		IAssignableIdentity savedAssignee = this.GetSavedAssignee();
		if (savedAssignee != null)
		{
			this.Assign(savedAssignee);
		}
	}

	// Token: 0x06002099 RID: 8345 RVA: 0x000B1F0C File Offset: 0x000B010C
	private IAssignableIdentity GetSavedAssignee()
	{
		if (this.assignee_identityRef.Get() != null)
		{
			return this.assignee_identityRef.Get().GetComponent<IAssignableIdentity>();
		}
		if (!string.IsNullOrEmpty(this.assignee_groupID))
		{
			return Game.Instance.assignmentManager.assignment_groups[this.assignee_groupID];
		}
		return null;
	}

	// Token: 0x0600209A RID: 8346 RVA: 0x000B1F68 File Offset: 0x000B0168
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.RestoreAssignee();
		Game.Instance.assignmentManager.Add(this);
		if (this.assignee == null && this.canBePublic)
		{
			this.Assign(Game.Instance.assignmentManager.assignment_groups["public"]);
		}
		this.assignmentPreconditions.Add(delegate(MinionAssignablesProxy proxy)
		{
			GameObject targetGameObject = proxy.GetTargetGameObject();
			return targetGameObject.GetComponent<KMonoBehaviour>().GetMyWorldId() == this.GetMyWorldId() || targetGameObject.IsMyParentWorld(base.gameObject);
		});
		this.autoassignmentPreconditions.Add(delegate(MinionAssignablesProxy proxy)
		{
			Operational component = base.GetComponent<Operational>();
			return !(component != null) || component.IsOperational;
		});
	}

	// Token: 0x0600209B RID: 8347 RVA: 0x000B1FEE File Offset: 0x000B01EE
	protected override void OnCleanUp()
	{
		this.Unassign();
		Game.Instance.assignmentManager.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x0600209C RID: 8348 RVA: 0x000B200C File Offset: 0x000B020C
	public bool CanAutoAssignTo(IAssignableIdentity identity)
	{
		MinionAssignablesProxy minionAssignablesProxy = identity as MinionAssignablesProxy;
		if (minionAssignablesProxy == null)
		{
			return true;
		}
		if (!this.CanAssignTo(minionAssignablesProxy))
		{
			return false;
		}
		using (List<Func<MinionAssignablesProxy, bool>>.Enumerator enumerator = this.autoassignmentPreconditions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current(minionAssignablesProxy))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x0600209D RID: 8349 RVA: 0x000B2084 File Offset: 0x000B0284
	public bool CanAssignTo(IAssignableIdentity identity)
	{
		MinionAssignablesProxy minionAssignablesProxy = identity as MinionAssignablesProxy;
		if (minionAssignablesProxy == null)
		{
			return true;
		}
		using (List<Func<MinionAssignablesProxy, bool>>.Enumerator enumerator = this.assignmentPreconditions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current(minionAssignablesProxy))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x0600209E RID: 8350 RVA: 0x000B20F0 File Offset: 0x000B02F0
	public bool IsAssigned()
	{
		return this.assignee != null;
	}

	// Token: 0x0600209F RID: 8351 RVA: 0x000B20FC File Offset: 0x000B02FC
	public bool IsAssignedTo(IAssignableIdentity identity)
	{
		global::Debug.Assert(identity != null, "IsAssignedTo identity is null");
		Ownables soleOwner = identity.GetSoleOwner();
		global::Debug.Assert(soleOwner != null, "IsAssignedTo identity sole owner is null");
		if (this.assignee != null)
		{
			foreach (Ownables ownables in this.assignee.GetOwners())
			{
				global::Debug.Assert(ownables, "Assignable owners list contained null");
				if (ownables.gameObject == soleOwner.gameObject)
				{
					return true;
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x060020A0 RID: 8352 RVA: 0x000B21A4 File Offset: 0x000B03A4
	public virtual void Assign(IAssignableIdentity new_assignee)
	{
		if (new_assignee == this.assignee)
		{
			return;
		}
		if (new_assignee is KMonoBehaviour)
		{
			if (!this.CanAssignTo(new_assignee))
			{
				return;
			}
			this.assignee_identityRef.Set((KMonoBehaviour)new_assignee);
			this.assignee_groupID = "";
		}
		else if (new_assignee is AssignmentGroup)
		{
			this.assignee_identityRef.Set(null);
			this.assignee_groupID = ((AssignmentGroup)new_assignee).id;
		}
		base.GetComponent<KPrefabID>().AddTag(GameTags.Assigned, false);
		this.assignee = new_assignee;
		if (this.slot != null && (new_assignee is MinionIdentity || new_assignee is StoredMinionIdentity || new_assignee is MinionAssignablesProxy))
		{
			Ownables soleOwner = new_assignee.GetSoleOwner();
			if (soleOwner != null)
			{
				AssignableSlotInstance slot = soleOwner.GetSlot(this.slot);
				if (slot != null)
				{
					slot.Assign(this);
				}
			}
			Equipment component = soleOwner.GetComponent<Equipment>();
			if (component != null)
			{
				AssignableSlotInstance slot2 = component.GetSlot(this.slot);
				if (slot2 != null)
				{
					slot2.Assign(this);
				}
			}
		}
		if (this.OnAssign != null)
		{
			this.OnAssign(new_assignee);
		}
		base.Trigger(684616645, new_assignee);
	}

	// Token: 0x060020A1 RID: 8353 RVA: 0x000B22B8 File Offset: 0x000B04B8
	public virtual void Unassign()
	{
		if (this.assignee == null)
		{
			return;
		}
		base.GetComponent<KPrefabID>().RemoveTag(GameTags.Assigned);
		if (this.slot != null)
		{
			Ownables soleOwner = this.assignee.GetSoleOwner();
			if (soleOwner)
			{
				AssignableSlotInstance assignableSlotInstance = soleOwner.GetSlot(this.slot);
				if (assignableSlotInstance != null)
				{
					assignableSlotInstance.Unassign(true);
				}
				Equipment component = soleOwner.GetComponent<Equipment>();
				if (component != null)
				{
					assignableSlotInstance = component.GetSlot(this.slot);
					if (assignableSlotInstance != null)
					{
						assignableSlotInstance.Unassign(true);
					}
				}
			}
		}
		this.assignee = null;
		if (this.canBePublic)
		{
			this.Assign(Game.Instance.assignmentManager.assignment_groups["public"]);
		}
		this.assignee_identityRef.Set(null);
		this.assignee_groupID = "";
		if (this.OnAssign != null)
		{
			this.OnAssign(null);
		}
		base.Trigger(684616645, null);
	}

	// Token: 0x060020A2 RID: 8354 RVA: 0x000B239D File Offset: 0x000B059D
	public void SetCanBeAssigned(bool state)
	{
		this.canBeAssigned = state;
	}

	// Token: 0x060020A3 RID: 8355 RVA: 0x000B23A6 File Offset: 0x000B05A6
	public void AddAssignPrecondition(Func<MinionAssignablesProxy, bool> precondition)
	{
		this.assignmentPreconditions.Add(precondition);
	}

	// Token: 0x060020A4 RID: 8356 RVA: 0x000B23B4 File Offset: 0x000B05B4
	public void AddAutoassignPrecondition(Func<MinionAssignablesProxy, bool> precondition)
	{
		this.autoassignmentPreconditions.Add(precondition);
	}

	// Token: 0x060020A5 RID: 8357 RVA: 0x000B23C4 File Offset: 0x000B05C4
	public int GetNavigationCost(Navigator navigator)
	{
		int num = -1;
		int num2 = Grid.PosToCell(this);
		IApproachable component = base.GetComponent<IApproachable>();
		CellOffset[] array = ((component != null) ? component.GetOffsets() : new CellOffset[1]);
		DebugUtil.DevAssert(navigator != null, "Navigator is mysteriously null", null);
		if (navigator == null)
		{
			return -1;
		}
		foreach (CellOffset cellOffset in array)
		{
			int num3 = Grid.OffsetCell(num2, cellOffset);
			int navigationCost = navigator.GetNavigationCost(num3);
			if (navigationCost != -1 && (num == -1 || navigationCost < num))
			{
				num = navigationCost;
			}
		}
		return num;
	}

	// Token: 0x040012D4 RID: 4820
	public string slotID;

	// Token: 0x040012D5 RID: 4821
	private AssignableSlot _slot;

	// Token: 0x040012D6 RID: 4822
	public IAssignableIdentity assignee;

	// Token: 0x040012D7 RID: 4823
	[Serialize]
	protected Ref<KMonoBehaviour> assignee_identityRef = new Ref<KMonoBehaviour>();

	// Token: 0x040012D8 RID: 4824
	[Serialize]
	private string assignee_groupID = "";

	// Token: 0x040012D9 RID: 4825
	public AssignableSlot[] subSlots;

	// Token: 0x040012DA RID: 4826
	public bool canBePublic;

	// Token: 0x040012DB RID: 4827
	[Serialize]
	private bool canBeAssigned = true;

	// Token: 0x040012DC RID: 4828
	private List<Func<MinionAssignablesProxy, bool>> autoassignmentPreconditions = new List<Func<MinionAssignablesProxy, bool>>();

	// Token: 0x040012DD RID: 4829
	private List<Func<MinionAssignablesProxy, bool>> assignmentPreconditions = new List<Func<MinionAssignablesProxy, bool>>();
}
