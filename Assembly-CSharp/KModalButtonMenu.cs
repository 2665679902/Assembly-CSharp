using System;
using UnityEngine;

// Token: 0x02000AC8 RID: 2760
public class KModalButtonMenu : KButtonMenu
{
	// Token: 0x06005489 RID: 21641 RVA: 0x001EB209 File Offset: 0x001E9409
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.modalBackground = KModalScreen.MakeScreenModal(this);
	}

	// Token: 0x0600548A RID: 21642 RVA: 0x001EB21D File Offset: 0x001E941D
	protected override void OnCmpEnable()
	{
		KModalScreen.ResizeBackground(this.modalBackground);
		ScreenResize instance = ScreenResize.Instance;
		instance.OnResize = (System.Action)Delegate.Combine(instance.OnResize, new System.Action(this.OnResize));
	}

	// Token: 0x0600548B RID: 21643 RVA: 0x001EB250 File Offset: 0x001E9450
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		if (this.childDialog == null)
		{
			base.Trigger(476357528, null);
		}
		ScreenResize instance = ScreenResize.Instance;
		instance.OnResize = (System.Action)Delegate.Remove(instance.OnResize, new System.Action(this.OnResize));
	}

	// Token: 0x0600548C RID: 21644 RVA: 0x001EB2A3 File Offset: 0x001E94A3
	private void OnResize()
	{
		KModalScreen.ResizeBackground(this.modalBackground);
	}

	// Token: 0x0600548D RID: 21645 RVA: 0x001EB2B0 File Offset: 0x001E94B0
	public override bool IsModal()
	{
		return true;
	}

	// Token: 0x0600548E RID: 21646 RVA: 0x001EB2B3 File Offset: 0x001E94B3
	public override float GetSortKey()
	{
		return 100f;
	}

	// Token: 0x0600548F RID: 21647 RVA: 0x001EB2BC File Offset: 0x001E94BC
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (SpeedControlScreen.Instance != null)
		{
			if (show && !this.shown)
			{
				SpeedControlScreen.Instance.Pause(false, false);
			}
			else if (!show && this.shown)
			{
				SpeedControlScreen.Instance.Unpause(false);
			}
			this.shown = show;
		}
		if (CameraController.Instance != null)
		{
			CameraController.Instance.DisableUserCameraControl = show;
		}
	}

	// Token: 0x06005490 RID: 21648 RVA: 0x001EB32B File Offset: 0x001E952B
	public override void OnKeyDown(KButtonEvent e)
	{
		base.OnKeyDown(e);
		e.Consumed = true;
	}

	// Token: 0x06005491 RID: 21649 RVA: 0x001EB33B File Offset: 0x001E953B
	public override void OnKeyUp(KButtonEvent e)
	{
		base.OnKeyUp(e);
		e.Consumed = true;
	}

	// Token: 0x06005492 RID: 21650 RVA: 0x001EB34B File Offset: 0x001E954B
	public void SetBackgroundActive(bool active)
	{
	}

	// Token: 0x06005493 RID: 21651 RVA: 0x001EB350 File Offset: 0x001E9550
	protected GameObject ActivateChildScreen(GameObject screenPrefab)
	{
		GameObject gameObject = Util.KInstantiateUI(screenPrefab, base.transform.parent.gameObject, false);
		this.childDialog = gameObject;
		gameObject.Subscribe(476357528, new Action<object>(this.Unhide));
		this.Hide();
		return gameObject;
	}

	// Token: 0x06005494 RID: 21652 RVA: 0x001EB39B File Offset: 0x001E959B
	private void Hide()
	{
		this.panelRoot.rectTransform().localScale = Vector3.zero;
	}

	// Token: 0x06005495 RID: 21653 RVA: 0x001EB3B2 File Offset: 0x001E95B2
	private void Unhide(object data = null)
	{
		this.panelRoot.rectTransform().localScale = Vector3.one;
		this.childDialog.Unsubscribe(476357528, new Action<object>(this.Unhide));
		this.childDialog = null;
	}

	// Token: 0x04003983 RID: 14723
	private bool shown;

	// Token: 0x04003984 RID: 14724
	[SerializeField]
	private GameObject panelRoot;

	// Token: 0x04003985 RID: 14725
	private GameObject childDialog;

	// Token: 0x04003986 RID: 14726
	private RectTransform modalBackground;
}
