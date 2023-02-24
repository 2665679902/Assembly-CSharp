using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000555 RID: 1365
[AddComponentMenu("KMonoBehaviour/scripts/AssignmentManager")]
public class AssignmentManager : KMonoBehaviour
{
	// Token: 0x060020BE RID: 8382 RVA: 0x000B28C8 File Offset: 0x000B0AC8
	public IEnumerator<Assignable> GetEnumerator()
	{
		return this.assignables.GetEnumerator();
	}

	// Token: 0x060020BF RID: 8383 RVA: 0x000B28DA File Offset: 0x000B0ADA
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Game.Instance.Subscribe<AssignmentManager>(586301400, AssignmentManager.MinionMigrationDelegate);
	}

	// Token: 0x060020C0 RID: 8384 RVA: 0x000B28F8 File Offset: 0x000B0AF8
	protected void MinionMigration(object data)
	{
		MinionMigrationEventArgs minionMigrationEventArgs = data as MinionMigrationEventArgs;
		foreach (Assignable assignable in this.assignables)
		{
			if (assignable.assignee != null)
			{
				Ownables soleOwner = assignable.assignee.GetSoleOwner();
				if (soleOwner != null && soleOwner.GetComponent<MinionAssignablesProxy>() != null && assignable.assignee.GetSoleOwner().GetComponent<MinionAssignablesProxy>().GetTargetGameObject() == minionMigrationEventArgs.minionId.gameObject)
				{
					assignable.Unassign();
				}
			}
		}
	}

	// Token: 0x060020C1 RID: 8385 RVA: 0x000B29A4 File Offset: 0x000B0BA4
	public void Add(Assignable assignable)
	{
		this.assignables.Add(assignable);
	}

	// Token: 0x060020C2 RID: 8386 RVA: 0x000B29B2 File Offset: 0x000B0BB2
	public void Remove(Assignable assignable)
	{
		this.assignables.Remove(assignable);
	}

	// Token: 0x060020C3 RID: 8387 RVA: 0x000B29C1 File Offset: 0x000B0BC1
	public AssignmentGroup TryCreateAssignmentGroup(string id, IAssignableIdentity[] members, string name)
	{
		if (this.assignment_groups.ContainsKey(id))
		{
			return this.assignment_groups[id];
		}
		return new AssignmentGroup(id, members, name);
	}

	// Token: 0x060020C4 RID: 8388 RVA: 0x000B29E6 File Offset: 0x000B0BE6
	public void RemoveAssignmentGroup(string id)
	{
		if (!this.assignment_groups.ContainsKey(id))
		{
			global::Debug.LogError("Assignment group with id " + id + " doesn't exists");
			return;
		}
		this.assignment_groups.Remove(id);
	}

	// Token: 0x060020C5 RID: 8389 RVA: 0x000B2A19 File Offset: 0x000B0C19
	public void AddToAssignmentGroup(string group_id, IAssignableIdentity member)
	{
		global::Debug.Assert(this.assignment_groups.ContainsKey(group_id));
		this.assignment_groups[group_id].AddMember(member);
	}

	// Token: 0x060020C6 RID: 8390 RVA: 0x000B2A3E File Offset: 0x000B0C3E
	public void RemoveFromAssignmentGroup(string group_id, IAssignableIdentity member)
	{
		global::Debug.Assert(this.assignment_groups.ContainsKey(group_id));
		this.assignment_groups[group_id].RemoveMember(member);
	}

	// Token: 0x060020C7 RID: 8391 RVA: 0x000B2A64 File Offset: 0x000B0C64
	public void RemoveFromAllGroups(IAssignableIdentity member)
	{
		foreach (Assignable assignable in this.assignables)
		{
			if (assignable.assignee == member)
			{
				assignable.Unassign();
			}
		}
		foreach (KeyValuePair<string, AssignmentGroup> keyValuePair in this.assignment_groups)
		{
			if (keyValuePair.Value.HasMember(member))
			{
				keyValuePair.Value.RemoveMember(member);
			}
		}
	}

	// Token: 0x060020C8 RID: 8392 RVA: 0x000B2B18 File Offset: 0x000B0D18
	public void RemoveFromWorld(IAssignableIdentity minionIdentity, int world_id)
	{
		foreach (Assignable assignable in this.assignables)
		{
			if (assignable.assignee != null && assignable.assignee.GetOwners().Count == 1)
			{
				Ownables soleOwner = assignable.assignee.GetSoleOwner();
				if (soleOwner != null && soleOwner.GetComponent<MinionAssignablesProxy>() != null && assignable.assignee == minionIdentity && assignable.GetMyWorldId() == world_id)
				{
					assignable.Unassign();
				}
			}
		}
	}

	// Token: 0x060020C9 RID: 8393 RVA: 0x000B2BBC File Offset: 0x000B0DBC
	public List<Assignable> GetPreferredAssignables(Assignables owner, AssignableSlot slot)
	{
		this.PreferredAssignableResults.Clear();
		int num = int.MaxValue;
		foreach (Assignable assignable in this.assignables)
		{
			if (assignable.slot == slot && assignable.assignee != null && assignable.assignee.HasOwner(owner))
			{
				Room room = assignable.assignee as Room;
				if (room != null && room.roomType.priority_building_use)
				{
					this.PreferredAssignableResults.Clear();
					this.PreferredAssignableResults.Add(assignable);
					return this.PreferredAssignableResults;
				}
				int num2 = assignable.assignee.NumOwners();
				if (num2 == num)
				{
					this.PreferredAssignableResults.Add(assignable);
				}
				else if (num2 < num)
				{
					num = num2;
					this.PreferredAssignableResults.Clear();
					this.PreferredAssignableResults.Add(assignable);
				}
			}
		}
		return this.PreferredAssignableResults;
	}

	// Token: 0x060020CA RID: 8394 RVA: 0x000B2CCC File Offset: 0x000B0ECC
	public bool IsPreferredAssignable(Assignables owner, Assignable candidate)
	{
		IAssignableIdentity assignee = candidate.assignee;
		if (assignee == null || !assignee.HasOwner(owner))
		{
			return false;
		}
		int num = assignee.NumOwners();
		Room room = assignee as Room;
		if (room != null && room.roomType.priority_building_use)
		{
			return true;
		}
		foreach (Assignable assignable in this.assignables)
		{
			if (assignable.slot == candidate.slot && assignable.assignee != assignee)
			{
				Room room2 = assignable.assignee as Room;
				if (room2 != null && room2.roomType.priority_building_use && assignable.assignee.HasOwner(owner))
				{
					return false;
				}
				if (assignable.assignee.NumOwners() < num && assignable.assignee.HasOwner(owner))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x040012E6 RID: 4838
	private List<Assignable> assignables = new List<Assignable>();

	// Token: 0x040012E7 RID: 4839
	public Dictionary<string, AssignmentGroup> assignment_groups = new Dictionary<string, AssignmentGroup> { 
	{
		"public",
		new AssignmentGroup("public", new IAssignableIdentity[0], UI.UISIDESCREENS.ASSIGNABLESIDESCREEN.PUBLIC)
	} };

	// Token: 0x040012E8 RID: 4840
	private static readonly EventSystem.IntraObjectHandler<AssignmentManager> MinionMigrationDelegate = new EventSystem.IntraObjectHandler<AssignmentManager>(delegate(AssignmentManager component, object data)
	{
		component.MinionMigration(data);
	});

	// Token: 0x040012E9 RID: 4841
	private List<Assignable> PreferredAssignableResults = new List<Assignable>();
}
