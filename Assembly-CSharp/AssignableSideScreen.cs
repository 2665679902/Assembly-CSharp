using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000B92 RID: 2962
public class AssignableSideScreen : SideScreenContent
{
	// Token: 0x17000681 RID: 1665
	// (get) Token: 0x06005D18 RID: 23832 RVA: 0x0022084A File Offset: 0x0021EA4A
	// (set) Token: 0x06005D19 RID: 23833 RVA: 0x00220852 File Offset: 0x0021EA52
	public Assignable targetAssignable { get; private set; }

	// Token: 0x06005D1A RID: 23834 RVA: 0x0022085B File Offset: 0x0021EA5B
	public override string GetTitle()
	{
		if (this.targetAssignable != null)
		{
			return string.Format(base.GetTitle(), this.targetAssignable.GetProperName());
		}
		return base.GetTitle();
	}

	// Token: 0x06005D1B RID: 23835 RVA: 0x00220888 File Offset: 0x0021EA88
	protected override void OnSpawn()
	{
		base.OnSpawn();
		MultiToggle multiToggle = this.dupeSortingToggle;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
		{
			this.SortByName(true);
		}));
		MultiToggle multiToggle2 = this.generalSortingToggle;
		multiToggle2.onClick = (System.Action)Delegate.Combine(multiToggle2.onClick, new System.Action(delegate
		{
			this.SortByAssignment(true);
		}));
		base.Subscribe(Game.Instance.gameObject, 875045922, new Action<object>(this.OnRefreshData));
	}

	// Token: 0x06005D1C RID: 23836 RVA: 0x0022090B File Offset: 0x0021EB0B
	private void OnRefreshData(object obj)
	{
		this.SetTarget(this.targetAssignable.gameObject);
	}

	// Token: 0x06005D1D RID: 23837 RVA: 0x00220920 File Offset: 0x0021EB20
	public override void ClearTarget()
	{
		if (this.targetAssignableSubscriptionHandle != -1 && this.targetAssignable != null)
		{
			this.targetAssignable.Unsubscribe(this.targetAssignableSubscriptionHandle);
			this.targetAssignableSubscriptionHandle = -1;
		}
		this.targetAssignable = null;
		Components.LiveMinionIdentities.OnAdd -= this.OnMinionIdentitiesChanged;
		Components.LiveMinionIdentities.OnRemove -= this.OnMinionIdentitiesChanged;
		base.ClearTarget();
	}

	// Token: 0x06005D1E RID: 23838 RVA: 0x00220995 File Offset: 0x0021EB95
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<Assignable>() != null && target.GetComponent<Assignable>().CanBeAssigned && target.GetComponent<AssignmentGroupController>() == null;
	}

	// Token: 0x06005D1F RID: 23839 RVA: 0x002209C0 File Offset: 0x0021EBC0
	public override void SetTarget(GameObject target)
	{
		Components.LiveMinionIdentities.OnAdd += this.OnMinionIdentitiesChanged;
		Components.LiveMinionIdentities.OnRemove += this.OnMinionIdentitiesChanged;
		if (this.targetAssignableSubscriptionHandle != -1 && this.targetAssignable != null)
		{
			this.targetAssignable.Unsubscribe(this.targetAssignableSubscriptionHandle);
		}
		this.targetAssignable = target.GetComponent<Assignable>();
		if (this.targetAssignable == null)
		{
			global::Debug.LogError(string.Format("{0} selected has no Assignable component.", target.GetProperName()));
			return;
		}
		if (this.rowPool == null)
		{
			this.rowPool = new UIPool<AssignableSideScreenRow>(this.rowPrefab);
		}
		base.gameObject.SetActive(true);
		this.identityList = new List<MinionAssignablesProxy>(Components.MinionAssignablesProxy.Items);
		this.dupeSortingToggle.ChangeState(0);
		this.generalSortingToggle.ChangeState(0);
		this.activeSortToggle = null;
		this.activeSortFunction = null;
		if (!this.targetAssignable.CanBeAssigned)
		{
			this.HideScreen(true);
		}
		else
		{
			this.HideScreen(false);
		}
		this.targetAssignableSubscriptionHandle = this.targetAssignable.Subscribe(684616645, new Action<object>(this.OnAssigneeChanged));
		this.Refresh(this.identityList);
		this.SortByAssignment(false);
	}

	// Token: 0x06005D20 RID: 23840 RVA: 0x00220B03 File Offset: 0x0021ED03
	private void OnMinionIdentitiesChanged(MinionIdentity change)
	{
		this.identityList = new List<MinionAssignablesProxy>(Components.MinionAssignablesProxy.Items);
		this.Refresh(this.identityList);
	}

	// Token: 0x06005D21 RID: 23841 RVA: 0x00220B28 File Offset: 0x0021ED28
	private void OnAssigneeChanged(object data = null)
	{
		foreach (KeyValuePair<IAssignableIdentity, AssignableSideScreenRow> keyValuePair in this.identityRowMap)
		{
			keyValuePair.Value.Refresh(null);
		}
	}

	// Token: 0x06005D22 RID: 23842 RVA: 0x00220B84 File Offset: 0x0021ED84
	private void Refresh(List<MinionAssignablesProxy> identities)
	{
		this.ClearContent();
		this.currentOwnerText.text = string.Format(UI.UISIDESCREENS.ASSIGNABLESIDESCREEN.UNASSIGNED, Array.Empty<object>());
		if (this.targetAssignable == null)
		{
			return;
		}
		if (this.targetAssignable.GetComponent<Equippable>() == null && !this.targetAssignable.HasTag(GameTags.NotRoomAssignable))
		{
			Room roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(this.targetAssignable.gameObject);
			if (roomOfGameObject != null)
			{
				RoomType roomType = roomOfGameObject.roomType;
				if (roomType.primary_constraint != null && !roomType.primary_constraint.building_criteria(this.targetAssignable.GetComponent<KPrefabID>()))
				{
					AssignableSideScreenRow freeElement = this.rowPool.GetFreeElement(this.rowGroup, true);
					freeElement.sideScreen = this;
					this.identityRowMap.Add(roomOfGameObject, freeElement);
					freeElement.SetContent(roomOfGameObject, new Action<IAssignableIdentity>(this.OnRowClicked), this);
					return;
				}
			}
		}
		if (this.targetAssignable.canBePublic)
		{
			AssignableSideScreenRow freeElement2 = this.rowPool.GetFreeElement(this.rowGroup, true);
			freeElement2.sideScreen = this;
			freeElement2.transform.SetAsFirstSibling();
			this.identityRowMap.Add(Game.Instance.assignmentManager.assignment_groups["public"], freeElement2);
			freeElement2.SetContent(Game.Instance.assignmentManager.assignment_groups["public"], new Action<IAssignableIdentity>(this.OnRowClicked), this);
		}
		foreach (MinionAssignablesProxy minionAssignablesProxy in identities)
		{
			AssignableSideScreenRow freeElement3 = this.rowPool.GetFreeElement(this.rowGroup, true);
			freeElement3.sideScreen = this;
			this.identityRowMap.Add(minionAssignablesProxy, freeElement3);
			freeElement3.SetContent(minionAssignablesProxy, new Action<IAssignableIdentity>(this.OnRowClicked), this);
		}
		this.ExecuteSort(this.activeSortFunction);
	}

	// Token: 0x06005D23 RID: 23843 RVA: 0x00220D84 File Offset: 0x0021EF84
	private void SortByName(bool reselect)
	{
		this.SelectSortToggle(this.dupeSortingToggle, reselect);
		this.ExecuteSort((IAssignableIdentity i1, IAssignableIdentity i2) => i1.GetProperName().CompareTo(i2.GetProperName()) * (this.sortReversed ? (-1) : 1));
	}

	// Token: 0x06005D24 RID: 23844 RVA: 0x00220DA8 File Offset: 0x0021EFA8
	private void SortByAssignment(bool reselect)
	{
		this.SelectSortToggle(this.generalSortingToggle, reselect);
		Comparison<IAssignableIdentity> comparison = delegate(IAssignableIdentity i1, IAssignableIdentity i2)
		{
			int num = this.targetAssignable.CanAssignTo(i1).CompareTo(this.targetAssignable.CanAssignTo(i2));
			if (num != 0)
			{
				return num * -1;
			}
			num = this.identityRowMap[i1].currentState.CompareTo(this.identityRowMap[i2].currentState);
			if (num != 0)
			{
				return num * (this.sortReversed ? (-1) : 1);
			}
			return i1.GetProperName().CompareTo(i2.GetProperName());
		};
		this.ExecuteSort(comparison);
	}

	// Token: 0x06005D25 RID: 23845 RVA: 0x00220DD8 File Offset: 0x0021EFD8
	private void SelectSortToggle(MultiToggle toggle, bool reselect)
	{
		this.dupeSortingToggle.ChangeState(0);
		this.generalSortingToggle.ChangeState(0);
		if (toggle != null)
		{
			if (reselect && this.activeSortToggle == toggle)
			{
				this.sortReversed = !this.sortReversed;
			}
			this.activeSortToggle = toggle;
		}
		this.activeSortToggle.ChangeState(this.sortReversed ? 2 : 1);
	}

	// Token: 0x06005D26 RID: 23846 RVA: 0x00220E44 File Offset: 0x0021F044
	private void ExecuteSort(Comparison<IAssignableIdentity> sortFunction)
	{
		if (sortFunction != null)
		{
			List<IAssignableIdentity> list = new List<IAssignableIdentity>(this.identityRowMap.Keys);
			list.Sort(sortFunction);
			for (int i = 0; i < list.Count; i++)
			{
				this.identityRowMap[list[i]].transform.SetSiblingIndex(i);
			}
			this.activeSortFunction = sortFunction;
		}
	}

	// Token: 0x06005D27 RID: 23847 RVA: 0x00220EA4 File Offset: 0x0021F0A4
	private void ClearContent()
	{
		if (this.rowPool != null)
		{
			this.rowPool.DestroyAll();
		}
		foreach (KeyValuePair<IAssignableIdentity, AssignableSideScreenRow> keyValuePair in this.identityRowMap)
		{
			keyValuePair.Value.targetIdentity = null;
		}
		this.identityRowMap.Clear();
	}

	// Token: 0x06005D28 RID: 23848 RVA: 0x00220F1C File Offset: 0x0021F11C
	private void HideScreen(bool hide)
	{
		if (hide)
		{
			base.transform.localScale = Vector3.zero;
			return;
		}
		if (base.transform.localScale != Vector3.one)
		{
			base.transform.localScale = Vector3.one;
		}
	}

	// Token: 0x06005D29 RID: 23849 RVA: 0x00220F59 File Offset: 0x0021F159
	private void OnRowClicked(IAssignableIdentity identity)
	{
		if (this.targetAssignable.assignee != identity)
		{
			this.ChangeAssignment(identity);
			return;
		}
		if (this.CanDeselect(identity))
		{
			this.ChangeAssignment(null);
		}
	}

	// Token: 0x06005D2A RID: 23850 RVA: 0x00220F81 File Offset: 0x0021F181
	private bool CanDeselect(IAssignableIdentity identity)
	{
		return identity is MinionAssignablesProxy;
	}

	// Token: 0x06005D2B RID: 23851 RVA: 0x00220F8C File Offset: 0x0021F18C
	private void ChangeAssignment(IAssignableIdentity new_identity)
	{
		this.targetAssignable.Unassign();
		if (!new_identity.IsNullOrDestroyed())
		{
			this.targetAssignable.Assign(new_identity);
		}
	}

	// Token: 0x06005D2C RID: 23852 RVA: 0x00220FAD File Offset: 0x0021F1AD
	private void OnValidStateChanged(bool state)
	{
		if (base.gameObject.activeInHierarchy)
		{
			this.Refresh(this.identityList);
		}
	}

	// Token: 0x04003FA7 RID: 16295
	[SerializeField]
	private AssignableSideScreenRow rowPrefab;

	// Token: 0x04003FA8 RID: 16296
	[SerializeField]
	private GameObject rowGroup;

	// Token: 0x04003FA9 RID: 16297
	[SerializeField]
	private LocText currentOwnerText;

	// Token: 0x04003FAA RID: 16298
	[SerializeField]
	private MultiToggle dupeSortingToggle;

	// Token: 0x04003FAB RID: 16299
	[SerializeField]
	private MultiToggle generalSortingToggle;

	// Token: 0x04003FAC RID: 16300
	private MultiToggle activeSortToggle;

	// Token: 0x04003FAD RID: 16301
	private Comparison<IAssignableIdentity> activeSortFunction;

	// Token: 0x04003FAE RID: 16302
	private bool sortReversed;

	// Token: 0x04003FAF RID: 16303
	private int targetAssignableSubscriptionHandle = -1;

	// Token: 0x04003FB1 RID: 16305
	private UIPool<AssignableSideScreenRow> rowPool;

	// Token: 0x04003FB2 RID: 16306
	private Dictionary<IAssignableIdentity, AssignableSideScreenRow> identityRowMap = new Dictionary<IAssignableIdentity, AssignableSideScreenRow>();

	// Token: 0x04003FB3 RID: 16307
	private List<MinionAssignablesProxy> identityList = new List<MinionAssignablesProxy>();
}
