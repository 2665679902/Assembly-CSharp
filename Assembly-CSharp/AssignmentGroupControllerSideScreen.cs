using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000B94 RID: 2964
public class AssignmentGroupControllerSideScreen : KScreen
{
	// Token: 0x06005D37 RID: 23863 RVA: 0x00221405 File Offset: 0x0021F605
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (show)
		{
			this.RefreshRows();
		}
	}

	// Token: 0x06005D38 RID: 23864 RVA: 0x00221418 File Offset: 0x0021F618
	protected override void OnCmpDisable()
	{
		for (int i = 0; i < this.identityRowMap.Count; i++)
		{
			UnityEngine.Object.Destroy(this.identityRowMap[i]);
		}
		this.identityRowMap.Clear();
		base.OnCmpDisable();
	}

	// Token: 0x06005D39 RID: 23865 RVA: 0x0022145D File Offset: 0x0021F65D
	public void SetTarget(GameObject target)
	{
		this.target = target.GetComponent<AssignmentGroupController>();
		this.RefreshRows();
	}

	// Token: 0x06005D3A RID: 23866 RVA: 0x00221474 File Offset: 0x0021F674
	private void RefreshRows()
	{
		int num = 0;
		WorldContainer worldContainer = this.target.GetMyWorld();
		ClustercraftExteriorDoor component = this.target.GetComponent<ClustercraftExteriorDoor>();
		if (component != null)
		{
			worldContainer = component.GetInteriorDoor().GetMyWorld();
		}
		List<AssignmentGroupControllerSideScreen.RowSortHelper> list = new List<AssignmentGroupControllerSideScreen.RowSortHelper>();
		for (int i = 0; i < Components.MinionAssignablesProxy.Count; i++)
		{
			MinionAssignablesProxy minionAssignablesProxy = Components.MinionAssignablesProxy[i];
			GameObject targetGameObject = minionAssignablesProxy.GetTargetGameObject();
			WorldContainer myWorld = targetGameObject.GetMyWorld();
			if (!(targetGameObject == null) && !targetGameObject.HasTag(GameTags.Dead))
			{
				MinionResume component2 = minionAssignablesProxy.GetTargetGameObject().GetComponent<MinionResume>();
				bool flag = false;
				if (component2 != null && component2.HasPerk(Db.Get().SkillPerks.CanUseRocketControlStation))
				{
					flag = true;
				}
				bool flag2 = myWorld.ParentWorldId == worldContainer.ParentWorldId;
				list.Add(new AssignmentGroupControllerSideScreen.RowSortHelper
				{
					minion = minionAssignablesProxy,
					isPilot = flag,
					isSameWorld = flag2
				});
			}
		}
		list.Sort(delegate(AssignmentGroupControllerSideScreen.RowSortHelper a, AssignmentGroupControllerSideScreen.RowSortHelper b)
		{
			int num2 = b.isSameWorld.CompareTo(a.isSameWorld);
			if (num2 != 0)
			{
				return num2;
			}
			return b.isPilot.CompareTo(a.isPilot);
		});
		foreach (AssignmentGroupControllerSideScreen.RowSortHelper rowSortHelper in list)
		{
			MinionAssignablesProxy minion = rowSortHelper.minion;
			GameObject gameObject;
			if (num >= this.identityRowMap.Count)
			{
				gameObject = Util.KInstantiateUI(this.minionRowPrefab, this.minionRowContainer, true);
				this.identityRowMap.Add(gameObject);
			}
			else
			{
				gameObject = this.identityRowMap[num];
				gameObject.SetActive(true);
			}
			num++;
			HierarchyReferences component3 = gameObject.GetComponent<HierarchyReferences>();
			MultiToggle toggle = component3.GetReference<MultiToggle>("Toggle");
			toggle.ChangeState(this.target.CheckMinionIsMember(minion) ? 1 : 0);
			component3.GetReference<CrewPortrait>("Portrait").SetIdentityObject(minion, false);
			LocText reference = component3.GetReference<LocText>("Label");
			LocText reference2 = component3.GetReference<LocText>("Designation");
			if (rowSortHelper.isSameWorld)
			{
				if (rowSortHelper.isPilot)
				{
					reference2.text = UI.UISIDESCREENS.ASSIGNMENTGROUPCONTROLLER.PILOT;
				}
				else
				{
					reference2.text = "";
				}
				reference.color = Color.black;
				reference2.color = Color.black;
			}
			else
			{
				reference.color = Color.grey;
				reference2.color = Color.grey;
				reference2.text = UI.UISIDESCREENS.ASSIGNMENTGROUPCONTROLLER.OFFWORLD;
				gameObject.transform.SetAsLastSibling();
			}
			toggle.onClick = delegate
			{
				this.target.SetMember(minion, !this.target.CheckMinionIsMember(minion));
				toggle.ChangeState(this.target.CheckMinionIsMember(minion) ? 1 : 0);
				this.RefreshRows();
			};
			string text = this.UpdateToolTip(minion, !rowSortHelper.isSameWorld);
			toggle.GetComponent<ToolTip>().SetSimpleTooltip(text);
		}
		for (int j = num; j < this.identityRowMap.Count; j++)
		{
			this.identityRowMap[j].SetActive(false);
		}
		this.minionRowContainer.GetComponent<QuickLayout>().ForceUpdate();
	}

	// Token: 0x06005D3B RID: 23867 RVA: 0x002217D8 File Offset: 0x0021F9D8
	private string UpdateToolTip(MinionAssignablesProxy minion, bool offworld)
	{
		string text = (this.target.CheckMinionIsMember(minion) ? UI.UISIDESCREENS.ASSIGNMENTGROUPCONTROLLER.TOOLTIPS.UNASSIGN : UI.UISIDESCREENS.ASSIGNMENTGROUPCONTROLLER.TOOLTIPS.ASSIGN);
		if (offworld)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n\n",
				UIConstants.ColorPrefixYellow,
				UI.UISIDESCREENS.ASSIGNMENTGROUPCONTROLLER.TOOLTIPS.DIFFERENT_WORLD,
				UIConstants.ColorSuffix
			});
		}
		return text;
	}

	// Token: 0x04003FBC RID: 16316
	[SerializeField]
	private GameObject header;

	// Token: 0x04003FBD RID: 16317
	[SerializeField]
	private GameObject minionRowPrefab;

	// Token: 0x04003FBE RID: 16318
	[SerializeField]
	private GameObject footer;

	// Token: 0x04003FBF RID: 16319
	[SerializeField]
	private GameObject minionRowContainer;

	// Token: 0x04003FC0 RID: 16320
	private AssignmentGroupController target;

	// Token: 0x04003FC1 RID: 16321
	private List<GameObject> identityRowMap = new List<GameObject>();

	// Token: 0x02001A4D RID: 6733
	private struct RowSortHelper
	{
		// Token: 0x0400772C RID: 30508
		public MinionAssignablesProxy minion;

		// Token: 0x0400772D RID: 30509
		public bool isPilot;

		// Token: 0x0400772E RID: 30510
		public bool isSameWorld;
	}
}
