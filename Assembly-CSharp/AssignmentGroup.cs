using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// Token: 0x02000486 RID: 1158
public class AssignmentGroup : IAssignableIdentity
{
	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x060019D9 RID: 6617 RVA: 0x0008A896 File Offset: 0x00088A96
	// (set) Token: 0x060019DA RID: 6618 RVA: 0x0008A89E File Offset: 0x00088A9E
	public string id { get; private set; }

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x060019DB RID: 6619 RVA: 0x0008A8A7 File Offset: 0x00088AA7
	// (set) Token: 0x060019DC RID: 6620 RVA: 0x0008A8AF File Offset: 0x00088AAF
	public string name { get; private set; }

	// Token: 0x060019DD RID: 6621 RVA: 0x0008A8B8 File Offset: 0x00088AB8
	public AssignmentGroup(string id, IAssignableIdentity[] members, string name)
	{
		this.id = id;
		this.name = name;
		foreach (IAssignableIdentity assignableIdentity in members)
		{
			this.members.Add(assignableIdentity);
		}
		if (Game.Instance != null)
		{
			Game.Instance.assignmentManager.assignment_groups.Add(id, this);
			Game.Instance.Trigger(-1123234494, this);
		}
	}

	// Token: 0x060019DE RID: 6622 RVA: 0x0008A942 File Offset: 0x00088B42
	public void AddMember(IAssignableIdentity member)
	{
		if (!this.members.Contains(member))
		{
			this.members.Add(member);
		}
		Game.Instance.Trigger(-1123234494, this);
	}

	// Token: 0x060019DF RID: 6623 RVA: 0x0008A96E File Offset: 0x00088B6E
	public void RemoveMember(IAssignableIdentity member)
	{
		this.members.Remove(member);
		Game.Instance.Trigger(-1123234494, this);
	}

	// Token: 0x060019E0 RID: 6624 RVA: 0x0008A98D File Offset: 0x00088B8D
	public string GetProperName()
	{
		return this.name;
	}

	// Token: 0x060019E1 RID: 6625 RVA: 0x0008A995 File Offset: 0x00088B95
	public bool HasMember(IAssignableIdentity member)
	{
		return this.members.Contains(member);
	}

	// Token: 0x060019E2 RID: 6626 RVA: 0x0008A9A3 File Offset: 0x00088BA3
	public bool IsNull()
	{
		return false;
	}

	// Token: 0x060019E3 RID: 6627 RVA: 0x0008A9A6 File Offset: 0x00088BA6
	public ReadOnlyCollection<IAssignableIdentity> GetMembers()
	{
		return this.members.AsReadOnly();
	}

	// Token: 0x060019E4 RID: 6628 RVA: 0x0008A9B4 File Offset: 0x00088BB4
	public List<Ownables> GetOwners()
	{
		this.current_owners.Clear();
		foreach (IAssignableIdentity assignableIdentity in this.members)
		{
			this.current_owners.AddRange(assignableIdentity.GetOwners());
		}
		return this.current_owners;
	}

	// Token: 0x060019E5 RID: 6629 RVA: 0x0008AA24 File Offset: 0x00088C24
	public Ownables GetSoleOwner()
	{
		if (this.members.Count == 1)
		{
			return this.members[0] as Ownables;
		}
		Debug.LogWarningFormat("GetSoleOwner called on AssignmentGroup with {0} members", new object[] { this.members.Count });
		return null;
	}

	// Token: 0x060019E6 RID: 6630 RVA: 0x0008AA78 File Offset: 0x00088C78
	public bool HasOwner(Assignables owner)
	{
		using (List<IAssignableIdentity>.Enumerator enumerator = this.members.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.HasOwner(owner))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060019E7 RID: 6631 RVA: 0x0008AAD4 File Offset: 0x00088CD4
	public int NumOwners()
	{
		int num = 0;
		foreach (IAssignableIdentity assignableIdentity in this.members)
		{
			num += assignableIdentity.NumOwners();
		}
		return num;
	}

	// Token: 0x04000E7E RID: 3710
	private List<IAssignableIdentity> members = new List<IAssignableIdentity>();

	// Token: 0x04000E7F RID: 3711
	public List<Ownables> current_owners = new List<Ownables>();
}
