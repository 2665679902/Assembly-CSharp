using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000B93 RID: 2963
[AddComponentMenu("KMonoBehaviour/scripts/AssignableSideScreenRow")]
public class AssignableSideScreenRow : KMonoBehaviour
{
	// Token: 0x06005D32 RID: 23858 RVA: 0x002210B0 File Offset: 0x0021F2B0
	public void Refresh(object data = null)
	{
		if (!this.sideScreen.targetAssignable.CanAssignTo(this.targetIdentity))
		{
			this.currentState = AssignableSideScreenRow.AssignableState.Disabled;
			this.assignmentText.text = UI.UISIDESCREENS.ASSIGNABLESIDESCREEN.DISABLED;
		}
		else if (this.sideScreen.targetAssignable.assignee == this.targetIdentity)
		{
			this.currentState = AssignableSideScreenRow.AssignableState.Selected;
			this.assignmentText.text = UI.UISIDESCREENS.ASSIGNABLESIDESCREEN.ASSIGNED;
		}
		else
		{
			bool flag = false;
			KMonoBehaviour kmonoBehaviour = this.targetIdentity as KMonoBehaviour;
			if (kmonoBehaviour != null)
			{
				Ownables component = kmonoBehaviour.GetComponent<Ownables>();
				if (component != null)
				{
					AssignableSlotInstance slot = component.GetSlot(this.sideScreen.targetAssignable.slot);
					if (slot != null && slot.IsAssigned())
					{
						this.currentState = AssignableSideScreenRow.AssignableState.AssignedToOther;
						this.assignmentText.text = slot.assignable.GetProperName();
						flag = true;
					}
				}
				Equipment component2 = kmonoBehaviour.GetComponent<Equipment>();
				if (component2 != null)
				{
					AssignableSlotInstance slot2 = component2.GetSlot(this.sideScreen.targetAssignable.slot);
					if (slot2 != null && slot2.IsAssigned())
					{
						this.currentState = AssignableSideScreenRow.AssignableState.AssignedToOther;
						this.assignmentText.text = slot2.assignable.GetProperName();
						flag = true;
					}
				}
			}
			if (!flag)
			{
				this.currentState = AssignableSideScreenRow.AssignableState.Unassigned;
				this.assignmentText.text = UI.UISIDESCREENS.ASSIGNABLESIDESCREEN.UNASSIGNED;
			}
		}
		this.toggle.ChangeState((int)this.currentState);
	}

	// Token: 0x06005D33 RID: 23859 RVA: 0x00221222 File Offset: 0x0021F422
	protected override void OnCleanUp()
	{
		if (this.refreshHandle == -1)
		{
			Game.Instance.Unsubscribe(this.refreshHandle);
		}
		base.OnCleanUp();
	}

	// Token: 0x06005D34 RID: 23860 RVA: 0x00221244 File Offset: 0x0021F444
	public void SetContent(IAssignableIdentity identity_object, Action<IAssignableIdentity> selectionCallback, AssignableSideScreen assignableSideScreen)
	{
		if (this.refreshHandle == -1)
		{
			Game.Instance.Unsubscribe(this.refreshHandle);
		}
		this.refreshHandle = Game.Instance.Subscribe(-2146166042, delegate(object o)
		{
			if (this != null && this.gameObject != null && this.gameObject.activeInHierarchy)
			{
				this.Refresh(null);
			}
		});
		this.toggle = base.GetComponent<MultiToggle>();
		this.sideScreen = assignableSideScreen;
		this.targetIdentity = identity_object;
		if (this.portraitInstance == null)
		{
			this.portraitInstance = Util.KInstantiateUI<CrewPortrait>(this.crewPortraitPrefab.gameObject, base.gameObject, false);
			this.portraitInstance.transform.SetSiblingIndex(1);
			this.portraitInstance.SetAlpha(1f);
		}
		this.toggle.onClick = delegate
		{
			selectionCallback(this.targetIdentity);
		};
		this.portraitInstance.SetIdentityObject(identity_object, false);
		base.GetComponent<ToolTip>().OnToolTip = new Func<string>(this.GetTooltip);
		this.Refresh(null);
	}

	// Token: 0x06005D35 RID: 23861 RVA: 0x00221348 File Offset: 0x0021F548
	private string GetTooltip()
	{
		ToolTip component = base.GetComponent<ToolTip>();
		component.ClearMultiStringTooltip();
		if (this.targetIdentity != null && !this.targetIdentity.IsNull())
		{
			AssignableSideScreenRow.AssignableState assignableState = this.currentState;
			if (assignableState != AssignableSideScreenRow.AssignableState.Selected)
			{
				if (assignableState != AssignableSideScreenRow.AssignableState.Disabled)
				{
					component.AddMultiStringTooltip(string.Format(UI.UISIDESCREENS.ASSIGNABLESIDESCREEN.ASSIGN_TO_TOOLTIP, this.targetIdentity.GetProperName()), null);
				}
				else
				{
					component.AddMultiStringTooltip(string.Format(UI.UISIDESCREENS.ASSIGNABLESIDESCREEN.DISABLED_TOOLTIP, this.targetIdentity.GetProperName()), null);
				}
			}
			else
			{
				component.AddMultiStringTooltip(string.Format(UI.UISIDESCREENS.ASSIGNABLESIDESCREEN.UNASSIGN_TOOLTIP, this.targetIdentity.GetProperName()), null);
			}
		}
		return "";
	}

	// Token: 0x04003FB4 RID: 16308
	[SerializeField]
	private CrewPortrait crewPortraitPrefab;

	// Token: 0x04003FB5 RID: 16309
	[SerializeField]
	private LocText assignmentText;

	// Token: 0x04003FB6 RID: 16310
	public AssignableSideScreen sideScreen;

	// Token: 0x04003FB7 RID: 16311
	private CrewPortrait portraitInstance;

	// Token: 0x04003FB8 RID: 16312
	[MyCmpReq]
	private MultiToggle toggle;

	// Token: 0x04003FB9 RID: 16313
	public IAssignableIdentity targetIdentity;

	// Token: 0x04003FBA RID: 16314
	public AssignableSideScreenRow.AssignableState currentState;

	// Token: 0x04003FBB RID: 16315
	private int refreshHandle = -1;

	// Token: 0x02001A4B RID: 6731
	public enum AssignableState
	{
		// Token: 0x04007726 RID: 30502
		Selected,
		// Token: 0x04007727 RID: 30503
		AssignedToOther,
		// Token: 0x04007728 RID: 30504
		Unassigned,
		// Token: 0x04007729 RID: 30505
		Disabled
	}
}
