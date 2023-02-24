using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200005D RID: 93
public class KInputField : KScreen
{
	// Token: 0x17000087 RID: 135
	// (get) Token: 0x060003A4 RID: 932 RVA: 0x00012F53 File Offset: 0x00011153
	public KInputTextField field
	{
		get
		{
			return this.inputField;
		}
	}

	// Token: 0x14000012 RID: 18
	// (add) Token: 0x060003A5 RID: 933 RVA: 0x00012F5C File Offset: 0x0001115C
	// (remove) Token: 0x060003A6 RID: 934 RVA: 0x00012F94 File Offset: 0x00011194
	public event System.Action onStartEdit;

	// Token: 0x14000013 RID: 19
	// (add) Token: 0x060003A7 RID: 935 RVA: 0x00012FCC File Offset: 0x000111CC
	// (remove) Token: 0x060003A8 RID: 936 RVA: 0x00013004 File Offset: 0x00011204
	public event System.Action onEndEdit;

	// Token: 0x060003A9 RID: 937 RVA: 0x0001303C File Offset: 0x0001123C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		KInputTextField kinputTextField = this.inputField;
		kinputTextField.onFocus = (System.Action)Delegate.Combine(kinputTextField.onFocus, new System.Action(this.OnEditStart));
		this.inputField.onEndEdit.AddListener(delegate
		{
			this.OnEditEnd(this.inputField.text);
		});
	}

	// Token: 0x060003AA RID: 938 RVA: 0x00013092 File Offset: 0x00011292
	private void OnEditStart()
	{
		base.isEditing = true;
		this.inputField.Select();
		this.inputField.ActivateInputField();
		KScreenManager.Instance.RefreshStack();
		if (this.onStartEdit != null)
		{
			this.onStartEdit();
		}
	}

	// Token: 0x060003AB RID: 939 RVA: 0x000130CE File Offset: 0x000112CE
	private void OnEditEnd(string input)
	{
		if (base.gameObject.activeInHierarchy)
		{
			this.ProcessInput(input);
			base.StartCoroutine(this.DelayedEndEdit());
			return;
		}
		this.StopEditing();
	}

	// Token: 0x060003AC RID: 940 RVA: 0x000130F8 File Offset: 0x000112F8
	private IEnumerator DelayedEndEdit()
	{
		if (base.isEditing)
		{
			yield return new WaitForEndOfFrame();
			this.StopEditing();
		}
		yield break;
	}

	// Token: 0x060003AD RID: 941 RVA: 0x00013107 File Offset: 0x00011307
	private void StopEditing()
	{
		base.isEditing = false;
		this.inputField.DeactivateInputField();
		if (this.onEndEdit != null)
		{
			this.onEndEdit();
		}
	}

	// Token: 0x060003AE RID: 942 RVA: 0x0001312E File Offset: 0x0001132E
	protected virtual void ProcessInput(string input)
	{
		this.SetDisplayValue(input);
	}

	// Token: 0x060003AF RID: 943 RVA: 0x00013137 File Offset: 0x00011337
	public void SetDisplayValue(string input)
	{
		this.inputField.text = input;
	}

	// Token: 0x04000437 RID: 1079
	[SerializeField]
	private KInputTextField inputField;
}
