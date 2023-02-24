using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BF2 RID: 3058
public class WarpPortalSideScreen : SideScreenContent
{
	// Token: 0x06006093 RID: 24723 RVA: 0x00234FA4 File Offset: 0x002331A4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.buttonLabel.SetText(UI.UISIDESCREENS.WARPPORTALSIDESCREEN.BUTTON);
		this.cancelButtonLabel.SetText(UI.UISIDESCREENS.WARPPORTALSIDESCREEN.CANCELBUTTON);
		this.button.onClick += this.OnButtonClick;
		this.cancelButton.onClick += this.OnCancelClick;
		this.Refresh(null);
	}

	// Token: 0x06006094 RID: 24724 RVA: 0x00235016 File Offset: 0x00233216
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<WarpPortal>() != null;
	}

	// Token: 0x06006095 RID: 24725 RVA: 0x00235024 File Offset: 0x00233224
	public override void SetTarget(GameObject target)
	{
		WarpPortal component = target.GetComponent<WarpPortal>();
		if (component == null)
		{
			global::Debug.LogError("Target doesn't have a WarpPortal associated with it.");
			return;
		}
		this.target = component;
		target.GetComponent<Assignable>().OnAssign += new Action<IAssignableIdentity>(this.Refresh);
		this.Refresh(null);
	}

	// Token: 0x06006096 RID: 24726 RVA: 0x00235074 File Offset: 0x00233274
	private void Update()
	{
		if (this.progressBar.activeSelf)
		{
			RectTransform rectTransform = this.progressBar.GetComponentsInChildren<Image>()[1].rectTransform;
			float num = this.target.rechargeProgress / 3000f;
			rectTransform.sizeDelta = new Vector2(rectTransform.transform.parent.GetComponent<LayoutElement>().minWidth * num, 24f);
			this.progressLabel.text = GameUtil.GetFormattedPercent(num * 100f, GameUtil.TimeSlice.None);
		}
	}

	// Token: 0x06006097 RID: 24727 RVA: 0x002350F0 File Offset: 0x002332F0
	private void OnButtonClick()
	{
		if (this.target.ReadyToWarp)
		{
			this.target.StartWarpSequence();
			this.Refresh(null);
		}
	}

	// Token: 0x06006098 RID: 24728 RVA: 0x00235111 File Offset: 0x00233311
	private void OnCancelClick()
	{
		this.target.CancelAssignment();
		this.Refresh(null);
	}

	// Token: 0x06006099 RID: 24729 RVA: 0x00235128 File Offset: 0x00233328
	private void Refresh(object data = null)
	{
		this.progressBar.SetActive(false);
		this.cancelButton.gameObject.SetActive(false);
		if (!(this.target != null))
		{
			this.label.text = UI.UISIDESCREENS.WARPPORTALSIDESCREEN.IDLE;
			this.button.gameObject.SetActive(false);
			return;
		}
		if (this.target.ReadyToWarp)
		{
			this.label.text = UI.UISIDESCREENS.WARPPORTALSIDESCREEN.WAITING;
			this.button.gameObject.SetActive(true);
			this.cancelButton.gameObject.SetActive(true);
			return;
		}
		if (this.target.IsConsumed)
		{
			this.button.gameObject.SetActive(false);
			this.progressBar.SetActive(true);
			this.label.text = UI.UISIDESCREENS.WARPPORTALSIDESCREEN.CONSUMED;
			return;
		}
		if (this.target.IsWorking)
		{
			this.label.text = UI.UISIDESCREENS.WARPPORTALSIDESCREEN.UNDERWAY;
			this.button.gameObject.SetActive(false);
			this.cancelButton.gameObject.SetActive(true);
			return;
		}
		this.label.text = UI.UISIDESCREENS.WARPPORTALSIDESCREEN.IDLE;
		this.button.gameObject.SetActive(false);
	}

	// Token: 0x0400422B RID: 16939
	[SerializeField]
	private LocText label;

	// Token: 0x0400422C RID: 16940
	[SerializeField]
	private KButton button;

	// Token: 0x0400422D RID: 16941
	[SerializeField]
	private LocText buttonLabel;

	// Token: 0x0400422E RID: 16942
	[SerializeField]
	private KButton cancelButton;

	// Token: 0x0400422F RID: 16943
	[SerializeField]
	private LocText cancelButtonLabel;

	// Token: 0x04004230 RID: 16944
	[SerializeField]
	private WarpPortal target;

	// Token: 0x04004231 RID: 16945
	[SerializeField]
	private GameObject contents;

	// Token: 0x04004232 RID: 16946
	[SerializeField]
	private GameObject progressBar;

	// Token: 0x04004233 RID: 16947
	[SerializeField]
	private LocText progressLabel;
}
