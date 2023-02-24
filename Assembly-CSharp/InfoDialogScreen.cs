using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000ABC RID: 2748
public class InfoDialogScreen : KModalScreen
{
	// Token: 0x06005401 RID: 21505 RVA: 0x001E850F File Offset: 0x001E670F
	public InfoScreenPlainText GetSubHeaderPrefab()
	{
		return this.subHeaderTemplate;
	}

	// Token: 0x06005402 RID: 21506 RVA: 0x001E8517 File Offset: 0x001E6717
	public InfoScreenPlainText GetPlainTextPrefab()
	{
		return this.plainTextTemplate;
	}

	// Token: 0x06005403 RID: 21507 RVA: 0x001E851F File Offset: 0x001E671F
	public InfoScreenLineItem GetLineItemPrefab()
	{
		return this.lineItemTemplate;
	}

	// Token: 0x06005404 RID: 21508 RVA: 0x001E8527 File Offset: 0x001E6727
	public GameObject GetPrimaryButtonPrefab()
	{
		return this.leftButtonPrefab;
	}

	// Token: 0x06005405 RID: 21509 RVA: 0x001E852F File Offset: 0x001E672F
	public GameObject GetSecondaryButtonPrefab()
	{
		return this.rightButtonPrefab;
	}

	// Token: 0x06005406 RID: 21510 RVA: 0x001E8537 File Offset: 0x001E6737
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06005407 RID: 21511 RVA: 0x001E854B File Offset: 0x001E674B
	public override bool IsModal()
	{
		return true;
	}

	// Token: 0x06005408 RID: 21512 RVA: 0x001E8550 File Offset: 0x001E6750
	public override void OnKeyDown(KButtonEvent e)
	{
		if (!this.escapeCloses)
		{
			e.TryConsume(global::Action.Escape);
			return;
		}
		if (e.TryConsume(global::Action.Escape))
		{
			this.Deactivate();
			return;
		}
		if (PlayerController.Instance != null && PlayerController.Instance.ConsumeIfNotDragging(e, global::Action.MouseRight))
		{
			this.Deactivate();
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06005409 RID: 21513 RVA: 0x001E85A7 File Offset: 0x001E67A7
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (!show && this.onDeactivateFn != null)
		{
			this.onDeactivateFn();
		}
	}

	// Token: 0x0600540A RID: 21514 RVA: 0x001E85C6 File Offset: 0x001E67C6
	public InfoDialogScreen AddDefaultOK(bool escapeCloses = false)
	{
		this.AddOption(UI.CONFIRMDIALOG.OK, delegate(InfoDialogScreen d)
		{
			d.Deactivate();
		}, true);
		this.escapeCloses = escapeCloses;
		return this;
	}

	// Token: 0x0600540B RID: 21515 RVA: 0x001E8601 File Offset: 0x001E6801
	public InfoDialogScreen AddDefaultCancel()
	{
		this.AddOption(UI.CONFIRMDIALOG.CANCEL, delegate(InfoDialogScreen d)
		{
			d.Deactivate();
		}, false);
		this.escapeCloses = true;
		return this;
	}

	// Token: 0x0600540C RID: 21516 RVA: 0x001E863C File Offset: 0x001E683C
	public InfoDialogScreen AddOption(string text, Action<InfoDialogScreen> action, bool rightSide = false)
	{
		GameObject gameObject = Util.KInstantiateUI(rightSide ? this.rightButtonPrefab : this.leftButtonPrefab, rightSide ? this.rightButtonPanel : this.leftButtonPanel, true);
		gameObject.gameObject.GetComponentInChildren<LocText>().text = text;
		gameObject.gameObject.GetComponent<KButton>().onClick += delegate
		{
			action(this);
		};
		return this;
	}

	// Token: 0x0600540D RID: 21517 RVA: 0x001E86B4 File Offset: 0x001E68B4
	public InfoDialogScreen AddOption(bool rightSide, out KButton button, out LocText buttonText)
	{
		GameObject gameObject = Util.KInstantiateUI(rightSide ? this.rightButtonPrefab : this.leftButtonPrefab, rightSide ? this.rightButtonPanel : this.leftButtonPanel, true);
		button = gameObject.GetComponent<KButton>();
		buttonText = gameObject.GetComponentInChildren<LocText>();
		return this;
	}

	// Token: 0x0600540E RID: 21518 RVA: 0x001E86FB File Offset: 0x001E68FB
	public InfoDialogScreen SetHeader(string header)
	{
		this.header.text = header;
		return this;
	}

	// Token: 0x0600540F RID: 21519 RVA: 0x001E870A File Offset: 0x001E690A
	public InfoDialogScreen AddSprite(Sprite sprite)
	{
		Util.KInstantiateUI<InfoScreenSpriteItem>(this.spriteItemTemplate.gameObject, this.contentContainer, false).SetSprite(sprite);
		return this;
	}

	// Token: 0x06005410 RID: 21520 RVA: 0x001E872A File Offset: 0x001E692A
	public InfoDialogScreen AddPlainText(string text)
	{
		Util.KInstantiateUI<InfoScreenPlainText>(this.plainTextTemplate.gameObject, this.contentContainer, false).SetText(text);
		return this;
	}

	// Token: 0x06005411 RID: 21521 RVA: 0x001E874A File Offset: 0x001E694A
	public InfoDialogScreen AddLineItem(string text, string tooltip)
	{
		InfoScreenLineItem infoScreenLineItem = Util.KInstantiateUI<InfoScreenLineItem>(this.lineItemTemplate.gameObject, this.contentContainer, false);
		infoScreenLineItem.SetText(text);
		infoScreenLineItem.SetTooltip(tooltip);
		return this;
	}

	// Token: 0x06005412 RID: 21522 RVA: 0x001E8771 File Offset: 0x001E6971
	public InfoDialogScreen AddSubHeader(string text)
	{
		Util.KInstantiateUI<InfoScreenPlainText>(this.subHeaderTemplate.gameObject, this.contentContainer, false).SetText(text);
		return this;
	}

	// Token: 0x06005413 RID: 21523 RVA: 0x001E8794 File Offset: 0x001E6994
	public InfoDialogScreen AddSpacer(float height)
	{
		GameObject gameObject = new GameObject("spacer");
		gameObject.SetActive(false);
		gameObject.transform.SetParent(this.contentContainer.transform, false);
		LayoutElement layoutElement = gameObject.AddComponent<LayoutElement>();
		layoutElement.minHeight = height;
		layoutElement.preferredHeight = height;
		layoutElement.flexibleHeight = 0f;
		gameObject.SetActive(true);
		return this;
	}

	// Token: 0x06005414 RID: 21524 RVA: 0x001E87EE File Offset: 0x001E69EE
	public InfoDialogScreen AddUI<T>(T prefab, out T spawn) where T : MonoBehaviour
	{
		spawn = Util.KInstantiateUI<T>(prefab.gameObject, this.contentContainer, true);
		return this;
	}

	// Token: 0x06005415 RID: 21525 RVA: 0x001E8810 File Offset: 0x001E6A10
	public InfoDialogScreen AddDescriptors(List<Descriptor> descriptors)
	{
		for (int i = 0; i < descriptors.Count; i++)
		{
			this.AddLineItem(descriptors[i].IndentedText(), descriptors[i].tooltipText);
		}
		return this;
	}

	// Token: 0x04003918 RID: 14616
	[SerializeField]
	private InfoScreenPlainText subHeaderTemplate;

	// Token: 0x04003919 RID: 14617
	[SerializeField]
	private InfoScreenPlainText plainTextTemplate;

	// Token: 0x0400391A RID: 14618
	[SerializeField]
	private InfoScreenLineItem lineItemTemplate;

	// Token: 0x0400391B RID: 14619
	[SerializeField]
	private InfoScreenSpriteItem spriteItemTemplate;

	// Token: 0x0400391C RID: 14620
	[Space(10f)]
	[SerializeField]
	private LocText header;

	// Token: 0x0400391D RID: 14621
	[SerializeField]
	private GameObject contentContainer;

	// Token: 0x0400391E RID: 14622
	[SerializeField]
	private GameObject leftButtonPrefab;

	// Token: 0x0400391F RID: 14623
	[SerializeField]
	private GameObject rightButtonPrefab;

	// Token: 0x04003920 RID: 14624
	[SerializeField]
	private GameObject leftButtonPanel;

	// Token: 0x04003921 RID: 14625
	[SerializeField]
	private GameObject rightButtonPanel;

	// Token: 0x04003922 RID: 14626
	private bool escapeCloses;

	// Token: 0x04003923 RID: 14627
	public System.Action onDeactivateFn;
}
