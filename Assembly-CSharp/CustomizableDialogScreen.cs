using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A7E RID: 2686
public class CustomizableDialogScreen : KModalScreen
{
	// Token: 0x0600522C RID: 21036 RVA: 0x001DABA6 File Offset: 0x001D8DA6
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.gameObject.SetActive(false);
		this.buttons = new List<CustomizableDialogScreen.Button>();
	}

	// Token: 0x0600522D RID: 21037 RVA: 0x001DABC5 File Offset: 0x001D8DC5
	public override bool IsModal()
	{
		return true;
	}

	// Token: 0x0600522E RID: 21038 RVA: 0x001DABC8 File Offset: 0x001D8DC8
	public void AddOption(string text, System.Action action)
	{
		GameObject gameObject = Util.KInstantiateUI(this.buttonPrefab, this.buttonPanel, true);
		this.buttons.Add(new CustomizableDialogScreen.Button
		{
			label = text,
			action = action,
			gameObject = gameObject
		});
	}

	// Token: 0x0600522F RID: 21039 RVA: 0x001DAC14 File Offset: 0x001D8E14
	public void PopupConfirmDialog(string text, string title_text = null, Sprite image_sprite = null)
	{
		foreach (CustomizableDialogScreen.Button button in this.buttons)
		{
			button.gameObject.GetComponentInChildren<LocText>().text = button.label;
			button.gameObject.GetComponent<KButton>().onClick += button.action;
		}
		if (image_sprite != null)
		{
			this.image.sprite = image_sprite;
			this.image.gameObject.SetActive(true);
		}
		if (title_text != null)
		{
			this.titleText.text = title_text;
		}
		this.popupMessage.text = text;
	}

	// Token: 0x06005230 RID: 21040 RVA: 0x001DACD0 File Offset: 0x001D8ED0
	protected override void OnDeactivate()
	{
		if (this.onDeactivateCB != null)
		{
			this.onDeactivateCB();
		}
		base.OnDeactivate();
	}

	// Token: 0x04003767 RID: 14183
	public System.Action onDeactivateCB;

	// Token: 0x04003768 RID: 14184
	[SerializeField]
	private GameObject buttonPrefab;

	// Token: 0x04003769 RID: 14185
	[SerializeField]
	private GameObject buttonPanel;

	// Token: 0x0400376A RID: 14186
	[SerializeField]
	private LocText titleText;

	// Token: 0x0400376B RID: 14187
	[SerializeField]
	private LocText popupMessage;

	// Token: 0x0400376C RID: 14188
	[SerializeField]
	private Image image;

	// Token: 0x0400376D RID: 14189
	private List<CustomizableDialogScreen.Button> buttons;

	// Token: 0x0200190B RID: 6411
	private struct Button
	{
		// Token: 0x04007325 RID: 29477
		public System.Action action;

		// Token: 0x04007326 RID: 29478
		public GameObject gameObject;

		// Token: 0x04007327 RID: 29479
		public string label;
	}
}
