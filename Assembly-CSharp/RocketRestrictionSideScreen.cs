using System;
using UnityEngine;

// Token: 0x02000BD8 RID: 3032
public class RocketRestrictionSideScreen : SideScreenContent
{
	// Token: 0x06005F7A RID: 24442 RVA: 0x0022EECB File Offset: 0x0022D0CB
	protected override void OnSpawn()
	{
		this.unrestrictedButton.onClick += this.ClickNone;
		this.spaceRestrictedButton.onClick += this.ClickSpace;
	}

	// Token: 0x06005F7B RID: 24443 RVA: 0x0022EEFB File Offset: 0x0022D0FB
	public override int GetSideScreenSortOrder()
	{
		return 0;
	}

	// Token: 0x06005F7C RID: 24444 RVA: 0x0022EEFE File Offset: 0x0022D0FE
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetSMI<RocketControlStation.StatesInstance>() != null;
	}

	// Token: 0x06005F7D RID: 24445 RVA: 0x0022EF09 File Offset: 0x0022D109
	public override void SetTarget(GameObject new_target)
	{
		this.controlStation = new_target.GetComponent<RocketControlStation>();
		this.controlStationLogicSubHandle = this.controlStation.Subscribe(1861523068, new Action<object>(this.UpdateButtonStates));
		this.UpdateButtonStates(null);
	}

	// Token: 0x06005F7E RID: 24446 RVA: 0x0022EF40 File Offset: 0x0022D140
	public override void ClearTarget()
	{
		if (this.controlStationLogicSubHandle != -1 && this.controlStation != null)
		{
			this.controlStation.Unsubscribe(this.controlStationLogicSubHandle);
			this.controlStationLogicSubHandle = -1;
		}
		this.controlStation = null;
	}

	// Token: 0x06005F7F RID: 24447 RVA: 0x0022EF78 File Offset: 0x0022D178
	private void UpdateButtonStates(object data = null)
	{
		bool flag = this.controlStation.IsLogicInputConnected();
		if (!flag)
		{
			this.unrestrictedButton.isOn = !this.controlStation.RestrictWhenGrounded;
			this.spaceRestrictedButton.isOn = this.controlStation.RestrictWhenGrounded;
		}
		this.unrestrictedButton.gameObject.SetActive(!flag);
		this.spaceRestrictedButton.gameObject.SetActive(!flag);
		this.automationControlled.gameObject.SetActive(flag);
	}

	// Token: 0x06005F80 RID: 24448 RVA: 0x0022EFFC File Offset: 0x0022D1FC
	private void ClickNone()
	{
		this.controlStation.RestrictWhenGrounded = false;
		this.UpdateButtonStates(null);
	}

	// Token: 0x06005F81 RID: 24449 RVA: 0x0022F011 File Offset: 0x0022D211
	private void ClickSpace()
	{
		this.controlStation.RestrictWhenGrounded = true;
		this.UpdateButtonStates(null);
	}

	// Token: 0x04004166 RID: 16742
	private RocketControlStation controlStation;

	// Token: 0x04004167 RID: 16743
	[Header("Buttons")]
	public KToggle unrestrictedButton;

	// Token: 0x04004168 RID: 16744
	public KToggle spaceRestrictedButton;

	// Token: 0x04004169 RID: 16745
	public GameObject automationControlled;

	// Token: 0x0400416A RID: 16746
	private int controlStationLogicSubHandle = -1;
}
