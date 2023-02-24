using System;
using System.Collections.Generic;
using FMODUnity;
using KSerialization;
using UnityEngine;

// Token: 0x02000956 RID: 2390
public class PassengerRocketModule : KMonoBehaviour
{
	// Token: 0x17000540 RID: 1344
	// (get) Token: 0x0600469C RID: 18076 RVA: 0x0018D67F File Offset: 0x0018B87F
	public PassengerRocketModule.RequestCrewState PassengersRequested
	{
		get
		{
			return this.passengersRequested;
		}
	}

	// Token: 0x0600469D RID: 18077 RVA: 0x0018D688 File Offset: 0x0018B888
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Game.Instance.Subscribe(-1123234494, new Action<object>(this.OnAssignmentGroupChanged));
		GameUtil.SubscribeToTags<PassengerRocketModule>(this, PassengerRocketModule.OnRocketOnGroundTagDelegate, false);
		base.Subscribe<PassengerRocketModule>(1655598572, PassengerRocketModule.RefreshDelegate);
		base.Subscribe<PassengerRocketModule>(191901966, PassengerRocketModule.RefreshDelegate);
		base.Subscribe<PassengerRocketModule>(-71801987, PassengerRocketModule.RefreshDelegate);
		base.Subscribe<PassengerRocketModule>(-1277991738, PassengerRocketModule.OnLaunchDelegate);
		base.Subscribe<PassengerRocketModule>(-1432940121, PassengerRocketModule.OnReachableChangedDelegate);
		new ReachabilityMonitor.Instance(base.GetComponent<Workable>()).StartSM();
	}

	// Token: 0x0600469E RID: 18078 RVA: 0x0018D728 File Offset: 0x0018B928
	protected override void OnCleanUp()
	{
		Game.Instance.Unsubscribe(-1123234494, new Action<object>(this.OnAssignmentGroupChanged));
		base.OnCleanUp();
	}

	// Token: 0x0600469F RID: 18079 RVA: 0x0018D74B File Offset: 0x0018B94B
	private void OnAssignmentGroupChanged(object data)
	{
		this.RefreshOrders();
	}

	// Token: 0x060046A0 RID: 18080 RVA: 0x0018D754 File Offset: 0x0018B954
	private void OnReachableChanged(object data)
	{
		bool flag = (bool)data;
		KSelectable component = base.GetComponent<KSelectable>();
		if (flag)
		{
			component.RemoveStatusItem(Db.Get().BuildingStatusItems.PassengerModuleUnreachable, false);
			return;
		}
		component.AddStatusItem(Db.Get().BuildingStatusItems.PassengerModuleUnreachable, this);
	}

	// Token: 0x060046A1 RID: 18081 RVA: 0x0018D79F File Offset: 0x0018B99F
	public void RequestCrewBoard(PassengerRocketModule.RequestCrewState requestBoard)
	{
		this.passengersRequested = requestBoard;
		this.RefreshOrders();
	}

	// Token: 0x060046A2 RID: 18082 RVA: 0x0018D7B0 File Offset: 0x0018B9B0
	public bool ShouldCrewGetIn()
	{
		CraftModuleInterface craftInterface = base.GetComponent<RocketModuleCluster>().CraftInterface;
		return this.passengersRequested == PassengerRocketModule.RequestCrewState.Request || (craftInterface.IsLaunchRequested() && craftInterface.CheckPreppedForLaunch());
	}

	// Token: 0x060046A3 RID: 18083 RVA: 0x0018D7E4 File Offset: 0x0018B9E4
	private void RefreshOrders()
	{
		if (!this.HasTag(GameTags.RocketOnGround) || !base.GetComponent<ClustercraftExteriorDoor>().HasTargetWorld())
		{
			return;
		}
		int cell = base.GetComponent<NavTeleporter>().GetCell();
		int num = base.GetComponent<ClustercraftExteriorDoor>().TargetCell();
		bool flag = this.ShouldCrewGetIn();
		if (flag)
		{
			using (List<MinionIdentity>.Enumerator enumerator = Components.LiveMinionIdentities.Items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MinionIdentity minionIdentity = enumerator.Current;
					bool flag2 = Game.Instance.assignmentManager.assignment_groups[base.GetComponent<AssignmentGroupController>().AssignmentGroupID].HasMember(minionIdentity.assignableProxy.Get());
					bool flag3 = minionIdentity.GetMyWorldId() == (int)Grid.WorldIdx[num];
					if (!flag3 && flag2)
					{
						minionIdentity.GetSMI<RocketPassengerMonitor.Instance>().SetMoveTarget(num);
					}
					else if (flag3 && !flag2)
					{
						minionIdentity.GetSMI<RocketPassengerMonitor.Instance>().SetMoveTarget(cell);
					}
					else
					{
						minionIdentity.GetSMI<RocketPassengerMonitor.Instance>().ClearMoveTarget(num);
					}
				}
				goto IL_144;
			}
		}
		foreach (MinionIdentity minionIdentity2 in Components.LiveMinionIdentities.Items)
		{
			minionIdentity2.GetSMI<RocketPassengerMonitor.Instance>().ClearMoveTarget(cell);
			minionIdentity2.GetSMI<RocketPassengerMonitor.Instance>().ClearMoveTarget(num);
		}
		IL_144:
		for (int i = 0; i < Components.LiveMinionIdentities.Count; i++)
		{
			this.RefreshAccessStatus(Components.LiveMinionIdentities[i], flag);
		}
	}

	// Token: 0x060046A4 RID: 18084 RVA: 0x0018D980 File Offset: 0x0018BB80
	private void RefreshAccessStatus(MinionIdentity minion, bool restrict)
	{
		Component interiorDoor = base.GetComponent<ClustercraftExteriorDoor>().GetInteriorDoor();
		AccessControl component = base.GetComponent<AccessControl>();
		AccessControl component2 = interiorDoor.GetComponent<AccessControl>();
		if (!restrict)
		{
			component.SetPermission(minion.assignableProxy.Get(), AccessControl.Permission.Both);
			component2.SetPermission(minion.assignableProxy.Get(), AccessControl.Permission.Both);
			return;
		}
		if (Game.Instance.assignmentManager.assignment_groups[base.GetComponent<AssignmentGroupController>().AssignmentGroupID].HasMember(minion.assignableProxy.Get()))
		{
			component.SetPermission(minion.assignableProxy.Get(), AccessControl.Permission.Both);
			component2.SetPermission(minion.assignableProxy.Get(), AccessControl.Permission.Neither);
			return;
		}
		component.SetPermission(minion.assignableProxy.Get(), AccessControl.Permission.Neither);
		component2.SetPermission(minion.assignableProxy.Get(), AccessControl.Permission.Both);
	}

	// Token: 0x060046A5 RID: 18085 RVA: 0x0018DA48 File Offset: 0x0018BC48
	public bool CheckPilotBoarded()
	{
		ICollection<IAssignableIdentity> members = base.GetComponent<AssignmentGroupController>().GetMembers();
		if (members.Count == 0)
		{
			return false;
		}
		List<IAssignableIdentity> list = new List<IAssignableIdentity>();
		foreach (IAssignableIdentity assignableIdentity in members)
		{
			MinionAssignablesProxy minionAssignablesProxy = (MinionAssignablesProxy)assignableIdentity;
			if (minionAssignablesProxy != null)
			{
				MinionResume component = minionAssignablesProxy.GetTargetGameObject().GetComponent<MinionResume>();
				if (component != null && component.HasPerk(Db.Get().SkillPerks.CanUseRocketControlStation))
				{
					list.Add(assignableIdentity);
				}
			}
		}
		if (list.Count == 0)
		{
			return false;
		}
		using (List<IAssignableIdentity>.Enumerator enumerator2 = list.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				if (((MinionAssignablesProxy)enumerator2.Current).GetTargetGameObject().GetMyWorldId() == (int)Grid.WorldIdx[base.GetComponent<ClustercraftExteriorDoor>().TargetCell()])
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060046A6 RID: 18086 RVA: 0x0018DB5C File Offset: 0x0018BD5C
	public global::Tuple<int, int> GetCrewBoardedFraction()
	{
		ICollection<IAssignableIdentity> members = base.GetComponent<AssignmentGroupController>().GetMembers();
		if (members.Count == 0)
		{
			return new global::Tuple<int, int>(0, 0);
		}
		int num = 0;
		using (IEnumerator<IAssignableIdentity> enumerator = members.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((MinionAssignablesProxy)enumerator.Current).GetTargetGameObject().GetMyWorldId() != (int)Grid.WorldIdx[base.GetComponent<ClustercraftExteriorDoor>().TargetCell()])
				{
					num++;
				}
			}
		}
		return new global::Tuple<int, int>(members.Count - num, members.Count);
	}

	// Token: 0x060046A7 RID: 18087 RVA: 0x0018DBF4 File Offset: 0x0018BDF4
	public bool CheckPassengersBoarded()
	{
		ICollection<IAssignableIdentity> members = base.GetComponent<AssignmentGroupController>().GetMembers();
		if (members.Count == 0)
		{
			return false;
		}
		bool flag = false;
		foreach (IAssignableIdentity assignableIdentity in members)
		{
			MinionAssignablesProxy minionAssignablesProxy = (MinionAssignablesProxy)assignableIdentity;
			if (minionAssignablesProxy != null)
			{
				MinionResume component = minionAssignablesProxy.GetTargetGameObject().GetComponent<MinionResume>();
				if (component != null && component.HasPerk(Db.Get().SkillPerks.CanUseRocketControlStation))
				{
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			return false;
		}
		using (IEnumerator<IAssignableIdentity> enumerator = members.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (((MinionAssignablesProxy)enumerator.Current).GetTargetGameObject().GetMyWorldId() != (int)Grid.WorldIdx[base.GetComponent<ClustercraftExteriorDoor>().TargetCell()])
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x060046A8 RID: 18088 RVA: 0x0018DCF0 File Offset: 0x0018BEF0
	public bool CheckExtraPassengers()
	{
		ClustercraftExteriorDoor component = base.GetComponent<ClustercraftExteriorDoor>();
		if (component.HasTargetWorld())
		{
			byte b = Grid.WorldIdx[component.TargetCell()];
			List<MinionIdentity> worldItems = Components.LiveMinionIdentities.GetWorldItems((int)b, false);
			for (int i = 0; i < worldItems.Count; i++)
			{
				if (!Game.Instance.assignmentManager.assignment_groups[base.GetComponent<AssignmentGroupController>().AssignmentGroupID].HasMember(worldItems[i].assignableProxy.Get()))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060046A9 RID: 18089 RVA: 0x0018DD74 File Offset: 0x0018BF74
	public void RemoveRocketPassenger(MinionIdentity minion)
	{
		if (minion != null)
		{
			string assignmentGroupID = base.GetComponent<AssignmentGroupController>().AssignmentGroupID;
			MinionAssignablesProxy minionAssignablesProxy = minion.assignableProxy.Get();
			if (Game.Instance.assignmentManager.assignment_groups[assignmentGroupID].HasMember(minionAssignablesProxy))
			{
				Game.Instance.assignmentManager.assignment_groups[assignmentGroupID].RemoveMember(minionAssignablesProxy);
			}
			this.RefreshOrders();
		}
	}

	// Token: 0x060046AA RID: 18090 RVA: 0x0018DDE0 File Offset: 0x0018BFE0
	public void ClearMinionAssignments(object data)
	{
		string assignmentGroupID = base.GetComponent<AssignmentGroupController>().AssignmentGroupID;
		foreach (IAssignableIdentity assignableIdentity in Game.Instance.assignmentManager.assignment_groups[assignmentGroupID].GetMembers())
		{
			Game.Instance.assignmentManager.RemoveFromWorld(assignableIdentity, this.GetMyWorldId());
		}
	}

	// Token: 0x04002EB5 RID: 11957
	public EventReference interiorReverbSnapshot;

	// Token: 0x04002EB6 RID: 11958
	[Serialize]
	private PassengerRocketModule.RequestCrewState passengersRequested;

	// Token: 0x04002EB7 RID: 11959
	private static readonly EventSystem.IntraObjectHandler<PassengerRocketModule> OnRocketOnGroundTagDelegate = GameUtil.CreateHasTagHandler<PassengerRocketModule>(GameTags.RocketOnGround, delegate(PassengerRocketModule component, object data)
	{
		component.RequestCrewBoard(PassengerRocketModule.RequestCrewState.Release);
	});

	// Token: 0x04002EB8 RID: 11960
	private static EventSystem.IntraObjectHandler<PassengerRocketModule> RefreshDelegate = new EventSystem.IntraObjectHandler<PassengerRocketModule>(delegate(PassengerRocketModule cmp, object data)
	{
		cmp.RefreshOrders();
	});

	// Token: 0x04002EB9 RID: 11961
	private static EventSystem.IntraObjectHandler<PassengerRocketModule> OnLaunchDelegate = new EventSystem.IntraObjectHandler<PassengerRocketModule>(delegate(PassengerRocketModule component, object data)
	{
		component.ClearMinionAssignments(data);
	});

	// Token: 0x04002EBA RID: 11962
	private static readonly EventSystem.IntraObjectHandler<PassengerRocketModule> OnReachableChangedDelegate = new EventSystem.IntraObjectHandler<PassengerRocketModule>(delegate(PassengerRocketModule component, object data)
	{
		component.OnReachableChanged(data);
	});

	// Token: 0x02001751 RID: 5969
	public enum RequestCrewState
	{
		// Token: 0x04006CBB RID: 27835
		Release,
		// Token: 0x04006CBC RID: 27836
		Request
	}
}
