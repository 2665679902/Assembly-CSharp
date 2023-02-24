using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000A00 RID: 2560
public class FileNameDialog : KModalScreen
{
	// Token: 0x06004CDB RID: 19675 RVA: 0x001B0C57 File Offset: 0x001AEE57
	public override float GetSortKey()
	{
		return 150f;
	}

	// Token: 0x06004CDC RID: 19676 RVA: 0x001B0C5E File Offset: 0x001AEE5E
	public void SetTextAndSelect(string text)
	{
		if (this.inputField == null)
		{
			return;
		}
		this.inputField.text = text;
		this.inputField.Select();
	}

	// Token: 0x06004CDD RID: 19677 RVA: 0x001B0C88 File Offset: 0x001AEE88
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.confirmButton.onClick += this.OnConfirm;
		this.cancelButton.onClick += this.OnCancel;
		this.closeButton.onClick += this.OnCancel;
		this.inputField.onValueChanged.AddListener(delegate
		{
			Util.ScrubInputField(this.inputField, false, false);
		});
		this.inputField.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEdit));
	}

	// Token: 0x06004CDE RID: 19678 RVA: 0x001B0D18 File Offset: 0x001AEF18
	protected override void OnActivate()
	{
		base.OnActivate();
		this.inputField.Select();
		this.inputField.ActivateInputField();
		CameraController.Instance.DisableUserCameraControl = true;
	}

	// Token: 0x06004CDF RID: 19679 RVA: 0x001B0D41 File Offset: 0x001AEF41
	protected override void OnDeactivate()
	{
		CameraController.Instance.DisableUserCameraControl = false;
		base.OnDeactivate();
	}

	// Token: 0x06004CE0 RID: 19680 RVA: 0x001B0D54 File Offset: 0x001AEF54
	public void OnConfirm()
	{
		if (this.onConfirm != null && !string.IsNullOrEmpty(this.inputField.text))
		{
			string text = this.inputField.text;
			if (!text.EndsWith(".sav"))
			{
				text += ".sav";
			}
			this.onConfirm(text);
			this.Deactivate();
		}
	}

	// Token: 0x06004CE1 RID: 19681 RVA: 0x001B0DB2 File Offset: 0x001AEFB2
	private void OnEndEdit(string str)
	{
		if (Localization.HasDirtyWords(str))
		{
			this.inputField.text = "";
		}
	}

	// Token: 0x06004CE2 RID: 19682 RVA: 0x001B0DCC File Offset: 0x001AEFCC
	public void OnCancel()
	{
		if (this.onCancel != null)
		{
			this.onCancel();
		}
		this.Deactivate();
	}

	// Token: 0x06004CE3 RID: 19683 RVA: 0x001B0DE7 File Offset: 0x001AEFE7
	public override void OnKeyUp(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape))
		{
			this.Deactivate();
		}
		else if (e.TryConsume(global::Action.DialogSubmit))
		{
			this.OnConfirm();
		}
		e.Consumed = true;
	}

	// Token: 0x06004CE4 RID: 19684 RVA: 0x001B0E14 File Offset: 0x001AF014
	public override void OnKeyDown(KButtonEvent e)
	{
		e.Consumed = true;
	}

	// Token: 0x040032A6 RID: 12966
	public Action<string> onConfirm;

	// Token: 0x040032A7 RID: 12967
	public System.Action onCancel;

	// Token: 0x040032A8 RID: 12968
	[SerializeField]
	private KInputTextField inputField;

	// Token: 0x040032A9 RID: 12969
	[SerializeField]
	private KButton confirmButton;

	// Token: 0x040032AA RID: 12970
	[SerializeField]
	private KButton cancelButton;

	// Token: 0x040032AB RID: 12971
	[SerializeField]
	private KButton closeButton;
}
