using System;
using UnityEngine;

// Token: 0x02000B1B RID: 2843
public class MessageDialogFrame : KScreen
{
	// Token: 0x060057A7 RID: 22439 RVA: 0x001FCD8B File Offset: 0x001FAF8B
	public override float GetSortKey()
	{
		return 15f;
	}

	// Token: 0x060057A8 RID: 22440 RVA: 0x001FCD94 File Offset: 0x001FAF94
	protected override void OnActivate()
	{
		this.closeButton.onClick += this.OnClickClose;
		this.nextMessageButton.onClick += this.OnClickNextMessage;
		MultiToggle multiToggle = this.dontShowAgainButton;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(this.OnClickDontShowAgain));
		bool flag = KPlayerPrefs.GetInt("HideTutorial_CheckState", 0) == 1;
		this.dontShowAgainButton.ChangeState(flag ? 0 : 1);
		base.Subscribe(Messenger.Instance.gameObject, -599791736, new Action<object>(this.OnMessagesChanged));
		this.OnMessagesChanged(null);
	}

	// Token: 0x060057A9 RID: 22441 RVA: 0x001FCE40 File Offset: 0x001FB040
	protected override void OnDeactivate()
	{
		base.Unsubscribe(Messenger.Instance.gameObject, -599791736, new Action<object>(this.OnMessagesChanged));
	}

	// Token: 0x060057AA RID: 22442 RVA: 0x001FCE63 File Offset: 0x001FB063
	private void OnClickClose()
	{
		this.TryDontShowAgain();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060057AB RID: 22443 RVA: 0x001FCE76 File Offset: 0x001FB076
	private void OnClickNextMessage()
	{
		this.TryDontShowAgain();
		UnityEngine.Object.Destroy(base.gameObject);
		NotificationScreen.Instance.OnClickNextMessage();
	}

	// Token: 0x060057AC RID: 22444 RVA: 0x001FCE94 File Offset: 0x001FB094
	private void OnClickDontShowAgain()
	{
		this.dontShowAgainButton.NextState();
		bool flag = this.dontShowAgainButton.CurrentState == 0;
		KPlayerPrefs.SetInt("HideTutorial_CheckState", flag ? 1 : 0);
	}

	// Token: 0x060057AD RID: 22445 RVA: 0x001FCECC File Offset: 0x001FB0CC
	private void OnMessagesChanged(object data)
	{
		this.nextMessageButton.gameObject.SetActive(Messenger.Instance.Count != 0);
	}

	// Token: 0x060057AE RID: 22446 RVA: 0x001FCEEC File Offset: 0x001FB0EC
	public void SetMessage(MessageDialog dialog, Message message)
	{
		this.title.text = message.GetTitle().ToUpper();
		dialog.GetComponent<RectTransform>().SetParent(this.body.GetComponent<RectTransform>());
		RectTransform component = dialog.GetComponent<RectTransform>();
		component.offsetMin = Vector2.zero;
		component.offsetMax = Vector2.zero;
		dialog.transform.SetLocalPosition(Vector3.zero);
		dialog.SetMessage(message);
		dialog.OnClickAction();
		if (dialog.CanDontShowAgain)
		{
			this.dontShowAgainElement.SetActive(true);
			this.dontShowAgainDelegate = new System.Action(dialog.OnDontShowAgain);
			return;
		}
		this.dontShowAgainElement.SetActive(false);
		this.dontShowAgainDelegate = null;
	}

	// Token: 0x060057AF RID: 22447 RVA: 0x001FCF99 File Offset: 0x001FB199
	private void TryDontShowAgain()
	{
		if (this.dontShowAgainDelegate != null && this.dontShowAgainButton.CurrentState == 0)
		{
			this.dontShowAgainDelegate();
		}
	}

	// Token: 0x04003B5A RID: 15194
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003B5B RID: 15195
	[SerializeField]
	private KToggle nextMessageButton;

	// Token: 0x04003B5C RID: 15196
	[SerializeField]
	private GameObject dontShowAgainElement;

	// Token: 0x04003B5D RID: 15197
	[SerializeField]
	private MultiToggle dontShowAgainButton;

	// Token: 0x04003B5E RID: 15198
	[SerializeField]
	private LocText title;

	// Token: 0x04003B5F RID: 15199
	[SerializeField]
	private RectTransform body;

	// Token: 0x04003B60 RID: 15200
	private System.Action dontShowAgainDelegate;
}
