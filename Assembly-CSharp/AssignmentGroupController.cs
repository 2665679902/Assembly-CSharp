using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using KSerialization;

// Token: 0x02000937 RID: 2359
public class AssignmentGroupController : KMonoBehaviour
{
	// Token: 0x170004EF RID: 1263
	// (get) Token: 0x0600450B RID: 17675 RVA: 0x00185655 File Offset: 0x00183855
	// (set) Token: 0x0600450C RID: 17676 RVA: 0x0018565D File Offset: 0x0018385D
	public string AssignmentGroupID
	{
		get
		{
			return this._assignmentGroupID;
		}
		private set
		{
			this._assignmentGroupID = value;
		}
	}

	// Token: 0x0600450D RID: 17677 RVA: 0x00185666 File Offset: 0x00183866
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x0600450E RID: 17678 RVA: 0x0018566E File Offset: 0x0018386E
	[OnDeserialized]
	protected void CreateOrRestoreGroupID()
	{
		if (string.IsNullOrEmpty(this.AssignmentGroupID))
		{
			this.GenerateGroupID();
			return;
		}
		Game.Instance.assignmentManager.TryCreateAssignmentGroup(this.AssignmentGroupID, new IAssignableIdentity[0], base.gameObject.GetProperName());
	}

	// Token: 0x0600450F RID: 17679 RVA: 0x001856AB File Offset: 0x001838AB
	public void SetGroupID(string id)
	{
		DebugUtil.DevAssert(!string.IsNullOrEmpty(id), "Trying to set Assignment group on " + base.gameObject.name + " to null or empty.", null);
		if (!string.IsNullOrEmpty(id))
		{
			this.AssignmentGroupID = id;
		}
	}

	// Token: 0x06004510 RID: 17680 RVA: 0x001856E5 File Offset: 0x001838E5
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.RestoreGroupAssignees();
	}

	// Token: 0x06004511 RID: 17681 RVA: 0x001856F4 File Offset: 0x001838F4
	private void GenerateGroupID()
	{
		if (!this.generateGroupOnStart)
		{
			return;
		}
		if (!string.IsNullOrEmpty(this.AssignmentGroupID))
		{
			return;
		}
		this.SetGroupID(base.GetComponent<KPrefabID>().PrefabID().ToString() + "_" + base.GetComponent<KPrefabID>().InstanceID.ToString() + "_assignmentGroup");
		Game.Instance.assignmentManager.TryCreateAssignmentGroup(this.AssignmentGroupID, new IAssignableIdentity[0], base.gameObject.GetProperName());
	}

	// Token: 0x06004512 RID: 17682 RVA: 0x00185780 File Offset: 0x00183980
	private void RestoreGroupAssignees()
	{
		if (!this.generateGroupOnStart)
		{
			return;
		}
		this.CreateOrRestoreGroupID();
		if (this.minionsInGroupAtLoad == null)
		{
			this.minionsInGroupAtLoad = new Ref<MinionAssignablesProxy>[0];
		}
		for (int i = 0; i < this.minionsInGroupAtLoad.Length; i++)
		{
			Game.Instance.assignmentManager.AddToAssignmentGroup(this.AssignmentGroupID, this.minionsInGroupAtLoad[i].Get());
		}
		Ownable component = base.GetComponent<Ownable>();
		if (component != null)
		{
			component.Assign(Game.Instance.assignmentManager.assignment_groups[this.AssignmentGroupID]);
			component.SetCanBeAssigned(false);
		}
	}

	// Token: 0x06004513 RID: 17683 RVA: 0x0018581C File Offset: 0x00183A1C
	public bool CheckMinionIsMember(MinionAssignablesProxy minion)
	{
		if (string.IsNullOrEmpty(this.AssignmentGroupID))
		{
			this.GenerateGroupID();
		}
		return Game.Instance.assignmentManager.assignment_groups[this.AssignmentGroupID].HasMember(minion);
	}

	// Token: 0x06004514 RID: 17684 RVA: 0x00185854 File Offset: 0x00183A54
	public void SetMember(MinionAssignablesProxy minion, bool isAllowed)
	{
		Debug.Assert(DlcManager.IsExpansion1Active());
		if (!isAllowed)
		{
			Game.Instance.assignmentManager.RemoveFromAssignmentGroup(this.AssignmentGroupID, minion);
			return;
		}
		if (!this.CheckMinionIsMember(minion))
		{
			Game.Instance.assignmentManager.AddToAssignmentGroup(this.AssignmentGroupID, minion);
		}
	}

	// Token: 0x06004515 RID: 17685 RVA: 0x001858A4 File Offset: 0x00183AA4
	protected override void OnCleanUp()
	{
		if (this.generateGroupOnStart)
		{
			Game.Instance.assignmentManager.RemoveAssignmentGroup(this.AssignmentGroupID);
		}
		base.OnCleanUp();
	}

	// Token: 0x06004516 RID: 17686 RVA: 0x001858CC File Offset: 0x00183ACC
	[OnSerializing]
	private void OnSerialize()
	{
		Debug.Assert(!string.IsNullOrEmpty(this.AssignmentGroupID), "Assignment group on " + base.gameObject.name + " has null or empty ID");
		ReadOnlyCollection<IAssignableIdentity> members = Game.Instance.assignmentManager.assignment_groups[this.AssignmentGroupID].GetMembers();
		this.minionsInGroupAtLoad = new Ref<MinionAssignablesProxy>[members.Count];
		for (int i = 0; i < members.Count; i++)
		{
			this.minionsInGroupAtLoad[i] = new Ref<MinionAssignablesProxy>((MinionAssignablesProxy)members[i]);
		}
	}

	// Token: 0x06004517 RID: 17687 RVA: 0x00185961 File Offset: 0x00183B61
	public ReadOnlyCollection<IAssignableIdentity> GetMembers()
	{
		return Game.Instance.assignmentManager.assignment_groups[this.AssignmentGroupID].GetMembers();
	}

	// Token: 0x04002E13 RID: 11795
	public bool generateGroupOnStart;

	// Token: 0x04002E14 RID: 11796
	[Serialize]
	private string _assignmentGroupID;

	// Token: 0x04002E15 RID: 11797
	[Serialize]
	private Ref<MinionAssignablesProxy>[] minionsInGroupAtLoad;
}
