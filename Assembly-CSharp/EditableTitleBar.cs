using System;
using System.Collections;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000A94 RID: 2708
public class EditableTitleBar : TitleBar
{
	// Token: 0x14000022 RID: 34
	// (add) Token: 0x06005308 RID: 21256 RVA: 0x001E155C File Offset: 0x001DF75C
	// (remove) Token: 0x06005309 RID: 21257 RVA: 0x001E1594 File Offset: 0x001DF794
	public event Action<string> OnNameChanged;

	// Token: 0x14000023 RID: 35
	// (add) Token: 0x0600530A RID: 21258 RVA: 0x001E15CC File Offset: 0x001DF7CC
	// (remove) Token: 0x0600530B RID: 21259 RVA: 0x001E1604 File Offset: 0x001DF804
	public event System.Action OnStartedEditing;

	// Token: 0x0600530C RID: 21260 RVA: 0x001E163C File Offset: 0x001DF83C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.randomNameButton != null)
		{
			this.randomNameButton.onClick += this.GenerateRandomName;
		}
		if (this.editNameButton != null)
		{
			this.EnableEditButtonClick();
		}
		if (this.inputField != null)
		{
			this.inputField.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEdit));
		}
	}

	// Token: 0x0600530D RID: 21261 RVA: 0x001E16B4 File Offset: 0x001DF8B4
	public void UpdateRenameTooltip(GameObject target)
	{
		if (this.editNameButton != null && target != null)
		{
			if (target.GetComponent<MinionBrain>() != null)
			{
				this.editNameButton.GetComponent<ToolTip>().toolTip = UI.TOOLTIPS.EDITNAME;
			}
			if (target.GetComponent<ClustercraftExteriorDoor>() != null || target.GetComponent<CommandModule>() != null)
			{
				this.editNameButton.GetComponent<ToolTip>().toolTip = UI.TOOLTIPS.EDITNAMEROCKET;
				return;
			}
			this.editNameButton.GetComponent<ToolTip>().toolTip = string.Format(UI.TOOLTIPS.EDITNAMEGENERIC, target.GetProperName());
		}
	}

	// Token: 0x0600530E RID: 21262 RVA: 0x001E1764 File Offset: 0x001DF964
	private void OnEndEdit(string finalStr)
	{
		finalStr = Localization.FilterDirtyWords(finalStr);
		this.SetEditingState(false);
		if (string.IsNullOrEmpty(finalStr))
		{
			return;
		}
		if (this.OnNameChanged != null)
		{
			this.OnNameChanged(finalStr);
		}
		this.titleText.text = finalStr;
		if (this.postEndEdit != null)
		{
			base.StopCoroutine(this.postEndEdit);
		}
		if (base.gameObject.activeInHierarchy && base.enabled)
		{
			this.postEndEdit = base.StartCoroutine(this.PostOnEndEditRoutine());
		}
	}

	// Token: 0x0600530F RID: 21263 RVA: 0x001E17E4 File Offset: 0x001DF9E4
	private IEnumerator PostOnEndEditRoutine()
	{
		int i = 0;
		while (i < 10)
		{
			int num = i;
			i = num + 1;
			yield return SequenceUtil.WaitForEndOfFrame;
		}
		this.EnableEditButtonClick();
		if (this.randomNameButton != null)
		{
			this.randomNameButton.gameObject.SetActive(false);
		}
		yield break;
	}

	// Token: 0x06005310 RID: 21264 RVA: 0x001E17F3 File Offset: 0x001DF9F3
	private IEnumerator PreToggleNameEditingRoutine()
	{
		yield return SequenceUtil.WaitForEndOfFrame;
		this.ToggleNameEditing();
		this.preToggleNameEditing = null;
		yield break;
	}

	// Token: 0x06005311 RID: 21265 RVA: 0x001E1802 File Offset: 0x001DFA02
	private void EnableEditButtonClick()
	{
		this.editNameButton.onClick += delegate
		{
			if (this.preToggleNameEditing != null)
			{
				return;
			}
			this.preToggleNameEditing = base.StartCoroutine(this.PreToggleNameEditingRoutine());
		};
	}

	// Token: 0x06005312 RID: 21266 RVA: 0x001E181C File Offset: 0x001DFA1C
	private void GenerateRandomName()
	{
		if (this.postEndEdit != null)
		{
			base.StopCoroutine(this.postEndEdit);
		}
		string text = GameUtil.GenerateRandomDuplicantName();
		if (this.OnNameChanged != null)
		{
			this.OnNameChanged(text);
		}
		this.titleText.text = text;
		this.SetEditingState(true);
	}

	// Token: 0x06005313 RID: 21267 RVA: 0x001E186C File Offset: 0x001DFA6C
	private void ToggleNameEditing()
	{
		this.editNameButton.ClearOnClick();
		bool flag = !this.inputField.gameObject.activeInHierarchy;
		if (this.randomNameButton != null)
		{
			this.randomNameButton.gameObject.SetActive(flag);
		}
		this.SetEditingState(flag);
	}

	// Token: 0x06005314 RID: 21268 RVA: 0x001E18C0 File Offset: 0x001DFAC0
	private void SetEditingState(bool state)
	{
		this.titleText.gameObject.SetActive(!state);
		if (this.setCameraControllerState)
		{
			CameraController.Instance.DisableUserCameraControl = state;
		}
		if (this.inputField == null)
		{
			return;
		}
		this.inputField.gameObject.SetActive(state);
		if (state)
		{
			this.inputField.text = this.titleText.text;
			this.inputField.Select();
			this.inputField.ActivateInputField();
			if (this.OnStartedEditing != null)
			{
				this.OnStartedEditing();
				return;
			}
		}
		else
		{
			this.inputField.DeactivateInputField();
		}
	}

	// Token: 0x06005315 RID: 21269 RVA: 0x001E1962 File Offset: 0x001DFB62
	public void ForceStopEditing()
	{
		if (this.postEndEdit != null)
		{
			base.StopCoroutine(this.postEndEdit);
		}
		this.editNameButton.ClearOnClick();
		this.SetEditingState(false);
		this.EnableEditButtonClick();
	}

	// Token: 0x06005316 RID: 21270 RVA: 0x001E1990 File Offset: 0x001DFB90
	public void SetUserEditable(bool editable)
	{
		this.userEditable = editable;
		this.editNameButton.gameObject.SetActive(editable);
		this.editNameButton.ClearOnClick();
		this.EnableEditButtonClick();
	}

	// Token: 0x0400383A RID: 14394
	public KButton editNameButton;

	// Token: 0x0400383B RID: 14395
	public KButton randomNameButton;

	// Token: 0x0400383C RID: 14396
	public KInputTextField inputField;

	// Token: 0x0400383F RID: 14399
	private Coroutine postEndEdit;

	// Token: 0x04003840 RID: 14400
	private Coroutine preToggleNameEditing;
}
