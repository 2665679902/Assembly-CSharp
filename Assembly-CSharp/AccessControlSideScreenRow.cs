using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000B8C RID: 2956
public class AccessControlSideScreenRow : AccessControlSideScreenDoor
{
	// Token: 0x06005CDD RID: 23773 RVA: 0x0021F509 File Offset: 0x0021D709
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.defaultButton.onValueChanged += this.OnDefaultButtonChanged;
	}

	// Token: 0x06005CDE RID: 23774 RVA: 0x0021F528 File Offset: 0x0021D728
	private void OnDefaultButtonChanged(bool state)
	{
		this.UpdateButtonStates(!state);
		if (this.defaultClickedCallback != null)
		{
			this.defaultClickedCallback(this.targetIdentity, !state);
		}
	}

	// Token: 0x06005CDF RID: 23775 RVA: 0x0021F554 File Offset: 0x0021D754
	protected override void UpdateButtonStates(bool isDefault)
	{
		base.UpdateButtonStates(isDefault);
		this.defaultButton.GetComponent<ToolTip>().SetSimpleTooltip(isDefault ? UI.UISIDESCREENS.ACCESS_CONTROL_SIDE_SCREEN.SET_TO_CUSTOM : UI.UISIDESCREENS.ACCESS_CONTROL_SIDE_SCREEN.SET_TO_DEFAULT);
		this.defaultControls.SetActive(isDefault);
		this.customControls.SetActive(!isDefault);
	}

	// Token: 0x06005CE0 RID: 23776 RVA: 0x0021F5A8 File Offset: 0x0021D7A8
	public void SetMinionContent(MinionAssignablesProxy identity, AccessControl.Permission permission, bool isDefault, Action<MinionAssignablesProxy, AccessControl.Permission> onPermissionChange, Action<MinionAssignablesProxy, bool> onDefaultClick)
	{
		base.SetContent(permission, onPermissionChange);
		if (identity == null)
		{
			global::Debug.LogError("Invalid data received.");
			return;
		}
		if (this.portraitInstance == null)
		{
			this.portraitInstance = Util.KInstantiateUI<CrewPortrait>(this.crewPortraitPrefab.gameObject, this.defaultButton.gameObject, false);
			this.portraitInstance.SetAlpha(1f);
		}
		this.targetIdentity = identity;
		this.portraitInstance.SetIdentityObject(identity, false);
		this.portraitInstance.SetSubTitle(isDefault ? UI.UISIDESCREENS.ACCESS_CONTROL_SIDE_SCREEN.USING_DEFAULT : UI.UISIDESCREENS.ACCESS_CONTROL_SIDE_SCREEN.USING_CUSTOM);
		this.defaultClickedCallback = null;
		this.defaultButton.isOn = !isDefault;
		this.defaultClickedCallback = onDefaultClick;
	}

	// Token: 0x04003F81 RID: 16257
	[SerializeField]
	private CrewPortrait crewPortraitPrefab;

	// Token: 0x04003F82 RID: 16258
	private CrewPortrait portraitInstance;

	// Token: 0x04003F83 RID: 16259
	public KToggle defaultButton;

	// Token: 0x04003F84 RID: 16260
	public GameObject defaultControls;

	// Token: 0x04003F85 RID: 16261
	public GameObject customControls;

	// Token: 0x04003F86 RID: 16262
	private Action<MinionAssignablesProxy, bool> defaultClickedCallback;
}
