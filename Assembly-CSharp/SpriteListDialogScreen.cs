using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C03 RID: 3075
public class SpriteListDialogScreen : KModalScreen
{
	// Token: 0x06006149 RID: 24905 RVA: 0x0023C70F File Offset: 0x0023A90F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.gameObject.SetActive(false);
		this.buttons = new List<SpriteListDialogScreen.Button>();
	}

	// Token: 0x0600614A RID: 24906 RVA: 0x0023C72E File Offset: 0x0023A92E
	public override bool IsModal()
	{
		return true;
	}

	// Token: 0x0600614B RID: 24907 RVA: 0x0023C731 File Offset: 0x0023A931
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape))
		{
			this.Deactivate();
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x0600614C RID: 24908 RVA: 0x0023C74C File Offset: 0x0023A94C
	public void AddOption(string text, System.Action action)
	{
		GameObject gameObject = Util.KInstantiateUI(this.buttonPrefab, this.buttonPanel, true);
		this.buttons.Add(new SpriteListDialogScreen.Button
		{
			label = text,
			action = action,
			gameObject = gameObject
		});
	}

	// Token: 0x0600614D RID: 24909 RVA: 0x0023C798 File Offset: 0x0023A998
	public void AddSprite(Sprite sprite, string text, float width = -1f, float height = -1f)
	{
		GameObject gameObject = Util.KInstantiateUI(this.listPrefab, this.listPanel, true);
		gameObject.GetComponentInChildren<LocText>().text = text;
		Image componentInChildren = gameObject.GetComponentInChildren<Image>();
		componentInChildren.sprite = sprite;
		if (width >= 0f || height >= 0f)
		{
			componentInChildren.GetComponent<AspectRatioFitter>().enabled = false;
			LayoutElement component = componentInChildren.GetComponent<LayoutElement>();
			component.minWidth = width;
			component.preferredWidth = width;
			component.minHeight = height;
			component.preferredHeight = height;
			return;
		}
		AspectRatioFitter component2 = componentInChildren.GetComponent<AspectRatioFitter>();
		float num = sprite.rect.width / sprite.rect.height;
		component2.aspectRatio = num;
	}

	// Token: 0x0600614E RID: 24910 RVA: 0x0023C83C File Offset: 0x0023AA3C
	public void PopupConfirmDialog(string text, string title_text = null)
	{
		foreach (SpriteListDialogScreen.Button button in this.buttons)
		{
			button.gameObject.GetComponentInChildren<LocText>().text = button.label;
			button.gameObject.GetComponent<KButton>().onClick += button.action;
		}
		if (title_text != null)
		{
			this.titleText.text = title_text;
		}
		this.popupMessage.text = text;
	}

	// Token: 0x0600614F RID: 24911 RVA: 0x0023C8D0 File Offset: 0x0023AAD0
	protected override void OnDeactivate()
	{
		if (this.onDeactivateCB != null)
		{
			this.onDeactivateCB();
		}
		base.OnDeactivate();
	}

	// Token: 0x04004307 RID: 17159
	public System.Action onDeactivateCB;

	// Token: 0x04004308 RID: 17160
	[SerializeField]
	private GameObject buttonPrefab;

	// Token: 0x04004309 RID: 17161
	[SerializeField]
	private GameObject buttonPanel;

	// Token: 0x0400430A RID: 17162
	[SerializeField]
	private LocText titleText;

	// Token: 0x0400430B RID: 17163
	[SerializeField]
	private LocText popupMessage;

	// Token: 0x0400430C RID: 17164
	[SerializeField]
	private GameObject listPanel;

	// Token: 0x0400430D RID: 17165
	[SerializeField]
	private GameObject listPrefab;

	// Token: 0x0400430E RID: 17166
	private List<SpriteListDialogScreen.Button> buttons;

	// Token: 0x02001A9E RID: 6814
	private struct Button
	{
		// Token: 0x04007815 RID: 30741
		public System.Action action;

		// Token: 0x04007816 RID: 30742
		public GameObject gameObject;

		// Token: 0x04007817 RID: 30743
		public string label;
	}
}
