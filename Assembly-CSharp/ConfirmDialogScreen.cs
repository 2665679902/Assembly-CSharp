using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A79 RID: 2681
public class ConfirmDialogScreen : KModalScreen
{
	// Token: 0x06005210 RID: 21008 RVA: 0x001DA469 File Offset: 0x001D8669
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06005211 RID: 21009 RVA: 0x001DA47D File Offset: 0x001D867D
	public override bool IsModal()
	{
		return true;
	}

	// Token: 0x06005212 RID: 21010 RVA: 0x001DA480 File Offset: 0x001D8680
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape))
		{
			this.OnSelect_CANCEL();
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06005213 RID: 21011 RVA: 0x001DA49C File Offset: 0x001D869C
	public void PopupConfirmDialog(string text, System.Action on_confirm, System.Action on_cancel, string configurable_text = null, System.Action on_configurable_clicked = null, string title_text = null, string confirm_text = null, string cancel_text = null, Sprite image_sprite = null)
	{
		while (base.transform.parent.GetComponent<Canvas>() == null && base.transform.parent.parent != null)
		{
			base.transform.SetParent(base.transform.parent.parent);
		}
		base.transform.SetAsLastSibling();
		this.confirmAction = on_confirm;
		this.cancelAction = on_cancel;
		this.configurableAction = on_configurable_clicked;
		int num = 0;
		if (this.confirmAction != null)
		{
			num++;
		}
		if (this.cancelAction != null)
		{
			num++;
		}
		if (this.configurableAction != null)
		{
			num++;
		}
		this.confirmButton.GetComponentInChildren<LocText>().text = ((confirm_text == null) ? UI.CONFIRMDIALOG.OK.text : confirm_text);
		this.cancelButton.GetComponentInChildren<LocText>().text = ((cancel_text == null) ? UI.CONFIRMDIALOG.CANCEL.text : cancel_text);
		this.confirmButton.GetComponent<KButton>().onClick += this.OnSelect_OK;
		this.cancelButton.GetComponent<KButton>().onClick += this.OnSelect_CANCEL;
		this.configurableButton.GetComponent<KButton>().onClick += this.OnSelect_third;
		this.cancelButton.SetActive(on_cancel != null);
		if (this.configurableButton != null)
		{
			this.configurableButton.SetActive(this.configurableAction != null);
			if (configurable_text != null)
			{
				this.configurableButton.GetComponentInChildren<LocText>().text = configurable_text;
			}
		}
		if (image_sprite != null)
		{
			this.image.sprite = image_sprite;
			this.image.gameObject.SetActive(true);
		}
		if (title_text != null)
		{
			this.titleText.key = "";
			this.titleText.text = title_text;
		}
		this.popupMessage.text = text;
	}

	// Token: 0x06005214 RID: 21012 RVA: 0x001DA671 File Offset: 0x001D8871
	public void OnSelect_OK()
	{
		if (this.deactivateOnConfirmAction)
		{
			this.Deactivate();
		}
		if (this.confirmAction != null)
		{
			this.confirmAction();
		}
	}

	// Token: 0x06005215 RID: 21013 RVA: 0x001DA694 File Offset: 0x001D8894
	public void OnSelect_CANCEL()
	{
		if (this.deactivateOnCancelAction)
		{
			this.Deactivate();
		}
		if (this.cancelAction != null)
		{
			this.cancelAction();
		}
	}

	// Token: 0x06005216 RID: 21014 RVA: 0x001DA6B7 File Offset: 0x001D88B7
	public void OnSelect_third()
	{
		if (this.deactivateOnConfigurableAction)
		{
			this.Deactivate();
		}
		if (this.configurableAction != null)
		{
			this.configurableAction();
		}
	}

	// Token: 0x06005217 RID: 21015 RVA: 0x001DA6DA File Offset: 0x001D88DA
	protected override void OnDeactivate()
	{
		if (this.onDeactivateCB != null)
		{
			this.onDeactivateCB();
		}
		base.OnDeactivate();
	}

	// Token: 0x0400374C RID: 14156
	private System.Action confirmAction;

	// Token: 0x0400374D RID: 14157
	private System.Action cancelAction;

	// Token: 0x0400374E RID: 14158
	private System.Action configurableAction;

	// Token: 0x0400374F RID: 14159
	public bool deactivateOnConfigurableAction = true;

	// Token: 0x04003750 RID: 14160
	public bool deactivateOnConfirmAction = true;

	// Token: 0x04003751 RID: 14161
	public bool deactivateOnCancelAction = true;

	// Token: 0x04003752 RID: 14162
	public System.Action onDeactivateCB;

	// Token: 0x04003753 RID: 14163
	[SerializeField]
	private GameObject confirmButton;

	// Token: 0x04003754 RID: 14164
	[SerializeField]
	private GameObject cancelButton;

	// Token: 0x04003755 RID: 14165
	[SerializeField]
	private GameObject configurableButton;

	// Token: 0x04003756 RID: 14166
	[SerializeField]
	private LocText titleText;

	// Token: 0x04003757 RID: 14167
	[SerializeField]
	private LocText popupMessage;

	// Token: 0x04003758 RID: 14168
	[SerializeField]
	private Image image;
}
