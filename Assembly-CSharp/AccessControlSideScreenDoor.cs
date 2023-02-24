using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000B8B RID: 2955
[AddComponentMenu("KMonoBehaviour/scripts/AccessControlSideScreenDoor")]
public class AccessControlSideScreenDoor : KMonoBehaviour
{
	// Token: 0x06005CD7 RID: 23767 RVA: 0x0021F36B File Offset: 0x0021D56B
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.leftButton.onClick += this.OnPermissionButtonClicked;
		this.rightButton.onClick += this.OnPermissionButtonClicked;
	}

	// Token: 0x06005CD8 RID: 23768 RVA: 0x0021F3A4 File Offset: 0x0021D5A4
	private void OnPermissionButtonClicked()
	{
		AccessControl.Permission permission;
		if (this.leftButton.isOn)
		{
			if (this.rightButton.isOn)
			{
				permission = AccessControl.Permission.Both;
			}
			else
			{
				permission = AccessControl.Permission.GoLeft;
			}
		}
		else if (this.rightButton.isOn)
		{
			permission = AccessControl.Permission.GoRight;
		}
		else
		{
			permission = AccessControl.Permission.Neither;
		}
		this.UpdateButtonStates(false);
		this.permissionChangedCallback(this.targetIdentity, permission);
	}

	// Token: 0x06005CD9 RID: 23769 RVA: 0x0021F400 File Offset: 0x0021D600
	protected virtual void UpdateButtonStates(bool isDefault)
	{
		ToolTip component = this.leftButton.GetComponent<ToolTip>();
		ToolTip component2 = this.rightButton.GetComponent<ToolTip>();
		if (this.isUpDown)
		{
			component.SetSimpleTooltip(this.leftButton.isOn ? UI.UISIDESCREENS.ACCESS_CONTROL_SIDE_SCREEN.GO_UP_ENABLED : UI.UISIDESCREENS.ACCESS_CONTROL_SIDE_SCREEN.GO_UP_DISABLED);
			component2.SetSimpleTooltip(this.rightButton.isOn ? UI.UISIDESCREENS.ACCESS_CONTROL_SIDE_SCREEN.GO_DOWN_ENABLED : UI.UISIDESCREENS.ACCESS_CONTROL_SIDE_SCREEN.GO_DOWN_DISABLED);
			return;
		}
		component.SetSimpleTooltip(this.leftButton.isOn ? UI.UISIDESCREENS.ACCESS_CONTROL_SIDE_SCREEN.GO_LEFT_ENABLED : UI.UISIDESCREENS.ACCESS_CONTROL_SIDE_SCREEN.GO_LEFT_DISABLED);
		component2.SetSimpleTooltip(this.rightButton.isOn ? UI.UISIDESCREENS.ACCESS_CONTROL_SIDE_SCREEN.GO_RIGHT_ENABLED : UI.UISIDESCREENS.ACCESS_CONTROL_SIDE_SCREEN.GO_RIGHT_DISABLED);
	}

	// Token: 0x06005CDA RID: 23770 RVA: 0x0021F4BE File Offset: 0x0021D6BE
	public void SetRotated(bool rotated)
	{
		this.isUpDown = rotated;
	}

	// Token: 0x06005CDB RID: 23771 RVA: 0x0021F4C7 File Offset: 0x0021D6C7
	public void SetContent(AccessControl.Permission permission, Action<MinionAssignablesProxy, AccessControl.Permission> onPermissionChange)
	{
		this.permissionChangedCallback = onPermissionChange;
		this.leftButton.isOn = permission == AccessControl.Permission.Both || permission == AccessControl.Permission.GoLeft;
		this.rightButton.isOn = permission == AccessControl.Permission.Both || permission == AccessControl.Permission.GoRight;
		this.UpdateButtonStates(false);
	}

	// Token: 0x04003F7C RID: 16252
	public KToggle leftButton;

	// Token: 0x04003F7D RID: 16253
	public KToggle rightButton;

	// Token: 0x04003F7E RID: 16254
	private Action<MinionAssignablesProxy, AccessControl.Permission> permissionChangedCallback;

	// Token: 0x04003F7F RID: 16255
	private bool isUpDown;

	// Token: 0x04003F80 RID: 16256
	protected MinionAssignablesProxy targetIdentity;
}
