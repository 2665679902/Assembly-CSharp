using System;
using System.Collections.Generic;
using System.Linq;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000B8A RID: 2954
public class AccessControlSideScreen : SideScreenContent
{
	// Token: 0x06005CC2 RID: 23746 RVA: 0x0021EC9A File Offset: 0x0021CE9A
	public override string GetTitle()
	{
		if (this.target != null)
		{
			return string.Format(base.GetTitle(), this.target.GetProperName());
		}
		return base.GetTitle();
	}

	// Token: 0x06005CC3 RID: 23747 RVA: 0x0021ECC8 File Offset: 0x0021CEC8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.sortByNameToggle.onValueChanged.AddListener(delegate(bool reverse_sort)
		{
			this.SortEntries(reverse_sort, new Comparison<MinionAssignablesProxy>(AccessControlSideScreen.MinionIdentitySort.CompareByName));
		});
		this.sortByRoleToggle.onValueChanged.AddListener(delegate(bool reverse_sort)
		{
			this.SortEntries(reverse_sort, new Comparison<MinionAssignablesProxy>(AccessControlSideScreen.MinionIdentitySort.CompareByRole));
		});
		this.sortByPermissionToggle.onValueChanged.AddListener(new UnityAction<bool>(this.SortByPermission));
	}

	// Token: 0x06005CC4 RID: 23748 RVA: 0x0021ED2F File Offset: 0x0021CF2F
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<AccessControl>() != null && target.GetComponent<AccessControl>().controlEnabled;
	}

	// Token: 0x06005CC5 RID: 23749 RVA: 0x0021ED4C File Offset: 0x0021CF4C
	public override void SetTarget(GameObject target)
	{
		if (this.target != null)
		{
			this.ClearTarget();
		}
		this.target = target.GetComponent<AccessControl>();
		this.doorTarget = target.GetComponent<Door>();
		if (this.target == null)
		{
			return;
		}
		target.Subscribe(1734268753, new Action<object>(this.OnDoorStateChanged));
		target.Subscribe(-1525636549, new Action<object>(this.OnAccessControlChanged));
		if (this.rowPool == null)
		{
			this.rowPool = new UIPool<AccessControlSideScreenRow>(this.rowPrefab);
		}
		base.gameObject.SetActive(true);
		this.identityList = new List<MinionAssignablesProxy>(Components.MinionAssignablesProxy.Items);
		this.Refresh(this.identityList, true);
	}

	// Token: 0x06005CC6 RID: 23750 RVA: 0x0021EE0C File Offset: 0x0021D00C
	public override void ClearTarget()
	{
		base.ClearTarget();
		if (this.target != null)
		{
			this.target.Unsubscribe(1734268753, new Action<object>(this.OnDoorStateChanged));
			this.target.Unsubscribe(-1525636549, new Action<object>(this.OnAccessControlChanged));
		}
	}

	// Token: 0x06005CC7 RID: 23751 RVA: 0x0021EE68 File Offset: 0x0021D068
	private void Refresh(List<MinionAssignablesProxy> identities, bool rebuild)
	{
		Rotatable component = this.target.GetComponent<Rotatable>();
		bool flag = component != null && component.IsRotated;
		this.defaultsRow.SetRotated(flag);
		this.defaultsRow.SetContent(this.target.DefaultPermission, new Action<MinionAssignablesProxy, AccessControl.Permission>(this.OnDefaultPermissionChanged));
		if (rebuild)
		{
			this.ClearContent();
		}
		foreach (MinionAssignablesProxy minionAssignablesProxy in identities)
		{
			AccessControlSideScreenRow accessControlSideScreenRow;
			if (rebuild)
			{
				accessControlSideScreenRow = this.rowPool.GetFreeElement(this.rowGroup, true);
				this.identityRowMap.Add(minionAssignablesProxy, accessControlSideScreenRow);
			}
			else
			{
				accessControlSideScreenRow = this.identityRowMap[minionAssignablesProxy];
			}
			AccessControl.Permission setPermission = this.target.GetSetPermission(minionAssignablesProxy);
			bool flag2 = this.target.IsDefaultPermission(minionAssignablesProxy);
			accessControlSideScreenRow.SetRotated(flag);
			accessControlSideScreenRow.SetMinionContent(minionAssignablesProxy, setPermission, flag2, new Action<MinionAssignablesProxy, AccessControl.Permission>(this.OnPermissionChanged), new Action<MinionAssignablesProxy, bool>(this.OnPermissionDefault));
		}
		this.RefreshOnline();
		this.ContentContainer.SetActive(this.target.controlEnabled);
	}

	// Token: 0x06005CC8 RID: 23752 RVA: 0x0021EFA4 File Offset: 0x0021D1A4
	private void RefreshOnline()
	{
		bool flag = this.target.Online && (this.doorTarget == null || this.doorTarget.CurrentState == Door.ControlState.Auto);
		this.disabledOverlay.SetActive(!flag);
		this.headerBG.ColorState = (flag ? KImage.ColorSelector.Active : KImage.ColorSelector.Inactive);
	}

	// Token: 0x06005CC9 RID: 23753 RVA: 0x0021F002 File Offset: 0x0021D202
	private void SortByPermission(bool state)
	{
		this.ExecuteSort<int>(this.sortByPermissionToggle, state, delegate(MinionAssignablesProxy identity)
		{
			if (!this.target.IsDefaultPermission(identity))
			{
				return (int)this.target.GetSetPermission(identity);
			}
			return -1;
		}, false);
	}

	// Token: 0x06005CCA RID: 23754 RVA: 0x0021F020 File Offset: 0x0021D220
	private void ExecuteSort<T>(Toggle toggle, bool state, Func<MinionAssignablesProxy, T> sortFunction, bool refresh = false)
	{
		toggle.GetComponent<ImageToggleState>().SetActiveState(state);
		if (!state)
		{
			return;
		}
		this.identityList = (state ? this.identityList.OrderBy(sortFunction).ToList<MinionAssignablesProxy>() : this.identityList.OrderByDescending(sortFunction).ToList<MinionAssignablesProxy>());
		if (refresh)
		{
			this.Refresh(this.identityList, false);
			return;
		}
		for (int i = 0; i < this.identityList.Count; i++)
		{
			if (this.identityRowMap.ContainsKey(this.identityList[i]))
			{
				this.identityRowMap[this.identityList[i]].transform.SetSiblingIndex(i);
			}
		}
	}

	// Token: 0x06005CCB RID: 23755 RVA: 0x0021F0D0 File Offset: 0x0021D2D0
	private void SortEntries(bool reverse_sort, Comparison<MinionAssignablesProxy> compare)
	{
		this.identityList.Sort(compare);
		if (reverse_sort)
		{
			this.identityList.Reverse();
		}
		for (int i = 0; i < this.identityList.Count; i++)
		{
			if (this.identityRowMap.ContainsKey(this.identityList[i]))
			{
				this.identityRowMap[this.identityList[i]].transform.SetSiblingIndex(i);
			}
		}
	}

	// Token: 0x06005CCC RID: 23756 RVA: 0x0021F148 File Offset: 0x0021D348
	private void ClearContent()
	{
		if (this.rowPool != null)
		{
			this.rowPool.ClearAll();
		}
		this.identityRowMap.Clear();
	}

	// Token: 0x06005CCD RID: 23757 RVA: 0x0021F168 File Offset: 0x0021D368
	private void OnDefaultPermissionChanged(MinionAssignablesProxy identity, AccessControl.Permission permission)
	{
		this.target.DefaultPermission = permission;
		this.Refresh(this.identityList, false);
		foreach (MinionAssignablesProxy minionAssignablesProxy in this.identityList)
		{
			if (this.target.IsDefaultPermission(minionAssignablesProxy))
			{
				this.target.ClearPermission(minionAssignablesProxy);
			}
		}
	}

	// Token: 0x06005CCE RID: 23758 RVA: 0x0021F1E8 File Offset: 0x0021D3E8
	private void OnPermissionChanged(MinionAssignablesProxy identity, AccessControl.Permission permission)
	{
		this.target.SetPermission(identity, permission);
	}

	// Token: 0x06005CCF RID: 23759 RVA: 0x0021F1F7 File Offset: 0x0021D3F7
	private void OnPermissionDefault(MinionAssignablesProxy identity, bool isDefault)
	{
		if (isDefault)
		{
			this.target.ClearPermission(identity);
		}
		else
		{
			this.target.SetPermission(identity, this.target.DefaultPermission);
		}
		this.Refresh(this.identityList, false);
	}

	// Token: 0x06005CD0 RID: 23760 RVA: 0x0021F22E File Offset: 0x0021D42E
	private void OnAccessControlChanged(object data)
	{
		this.RefreshOnline();
	}

	// Token: 0x06005CD1 RID: 23761 RVA: 0x0021F236 File Offset: 0x0021D436
	private void OnDoorStateChanged(object data)
	{
		this.RefreshOnline();
	}

	// Token: 0x06005CD2 RID: 23762 RVA: 0x0021F240 File Offset: 0x0021D440
	private void OnSelectSortFunc(IListableOption role, object data)
	{
		if (role != null)
		{
			foreach (AccessControlSideScreen.MinionIdentitySort.SortInfo sortInfo in AccessControlSideScreen.MinionIdentitySort.SortInfos)
			{
				if (sortInfo.name == role.GetProperName())
				{
					this.sortInfo = sortInfo;
					this.identityList.Sort(this.sortInfo.compare);
					for (int j = 0; j < this.identityList.Count; j++)
					{
						if (this.identityRowMap.ContainsKey(this.identityList[j]))
						{
							this.identityRowMap[this.identityList[j]].transform.SetSiblingIndex(j);
						}
					}
					return;
				}
			}
		}
	}

	// Token: 0x04003F6E RID: 16238
	[SerializeField]
	private AccessControlSideScreenRow rowPrefab;

	// Token: 0x04003F6F RID: 16239
	[SerializeField]
	private GameObject rowGroup;

	// Token: 0x04003F70 RID: 16240
	[SerializeField]
	private AccessControlSideScreenDoor defaultsRow;

	// Token: 0x04003F71 RID: 16241
	[SerializeField]
	private Toggle sortByNameToggle;

	// Token: 0x04003F72 RID: 16242
	[SerializeField]
	private Toggle sortByPermissionToggle;

	// Token: 0x04003F73 RID: 16243
	[SerializeField]
	private Toggle sortByRoleToggle;

	// Token: 0x04003F74 RID: 16244
	[SerializeField]
	private GameObject disabledOverlay;

	// Token: 0x04003F75 RID: 16245
	[SerializeField]
	private KImage headerBG;

	// Token: 0x04003F76 RID: 16246
	private AccessControl target;

	// Token: 0x04003F77 RID: 16247
	private Door doorTarget;

	// Token: 0x04003F78 RID: 16248
	private UIPool<AccessControlSideScreenRow> rowPool;

	// Token: 0x04003F79 RID: 16249
	private AccessControlSideScreen.MinionIdentitySort.SortInfo sortInfo = AccessControlSideScreen.MinionIdentitySort.SortInfos[0];

	// Token: 0x04003F7A RID: 16250
	private Dictionary<MinionAssignablesProxy, AccessControlSideScreenRow> identityRowMap = new Dictionary<MinionAssignablesProxy, AccessControlSideScreenRow>();

	// Token: 0x04003F7B RID: 16251
	private List<MinionAssignablesProxy> identityList = new List<MinionAssignablesProxy>();

	// Token: 0x02001A46 RID: 6726
	private static class MinionIdentitySort
	{
		// Token: 0x060092AC RID: 37548 RVA: 0x00318202 File Offset: 0x00316402
		public static int CompareByName(MinionAssignablesProxy a, MinionAssignablesProxy b)
		{
			return a.GetProperName().CompareTo(b.GetProperName());
		}

		// Token: 0x060092AD RID: 37549 RVA: 0x00318218 File Offset: 0x00316418
		public static int CompareByRole(MinionAssignablesProxy a, MinionAssignablesProxy b)
		{
			global::Debug.Assert(a, "a was null");
			global::Debug.Assert(b, "b was null");
			GameObject targetGameObject = a.GetTargetGameObject();
			GameObject targetGameObject2 = b.GetTargetGameObject();
			MinionResume minionResume = (targetGameObject ? targetGameObject.GetComponent<MinionResume>() : null);
			MinionResume minionResume2 = (targetGameObject2 ? targetGameObject2.GetComponent<MinionResume>() : null);
			if (minionResume2 == null)
			{
				return 1;
			}
			if (minionResume == null)
			{
				return -1;
			}
			int num = minionResume.CurrentRole.CompareTo(minionResume2.CurrentRole);
			if (num != 0)
			{
				return num;
			}
			return AccessControlSideScreen.MinionIdentitySort.CompareByName(a, b);
		}

		// Token: 0x0400771C RID: 30492
		public static readonly AccessControlSideScreen.MinionIdentitySort.SortInfo[] SortInfos = new AccessControlSideScreen.MinionIdentitySort.SortInfo[]
		{
			new AccessControlSideScreen.MinionIdentitySort.SortInfo
			{
				name = UI.MINION_IDENTITY_SORT.NAME,
				compare = new Comparison<MinionAssignablesProxy>(AccessControlSideScreen.MinionIdentitySort.CompareByName)
			},
			new AccessControlSideScreen.MinionIdentitySort.SortInfo
			{
				name = UI.MINION_IDENTITY_SORT.ROLE,
				compare = new Comparison<MinionAssignablesProxy>(AccessControlSideScreen.MinionIdentitySort.CompareByRole)
			}
		};

		// Token: 0x02002108 RID: 8456
		public class SortInfo : IListableOption
		{
			// Token: 0x0600A5DD RID: 42461 RVA: 0x0034B06F File Offset: 0x0034926F
			public string GetProperName()
			{
				return this.name;
			}

			// Token: 0x040092E6 RID: 37606
			public LocString name;

			// Token: 0x040092E7 RID: 37607
			public Comparison<MinionAssignablesProxy> compare;
		}
	}
}
