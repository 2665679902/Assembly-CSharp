using System;
using System.Collections;
using Database;
using UnityEngine;

// Token: 0x02000ACE RID: 2766
public class KleiItemDropScreen : KModalScreen
{
	// Token: 0x060054DB RID: 21723 RVA: 0x001EC7DC File Offset: 0x001EA9DC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		KleiItemDropScreen.Instance = this;
		this.closeButton.onClick += delegate
		{
			this.Show(false);
		};
		if (string.IsNullOrEmpty(KleiAccount.KleiToken))
		{
			base.Show(false);
		}
	}

	// Token: 0x060054DC RID: 21724 RVA: 0x001EC814 File Offset: 0x001EAA14
	protected override void OnActivate()
	{
		KleiItemDropScreen.Instance = this;
		this.Show(false);
	}

	// Token: 0x060054DD RID: 21725 RVA: 0x001EC824 File Offset: 0x001EAA24
	public override void Show(bool show = true)
	{
		if (show)
		{
			base.Show(true);
			return;
		}
		if (this.activePresentationRoutine != null)
		{
			base.StopCoroutine(this.activePresentationRoutine);
		}
		if (this.shouldDoCloseRoutine)
		{
			this.closeButton.gameObject.SetActive(false);
			Updater.RunRoutine(this, this.AnimateScreenOutRoutine()).Then(delegate
			{
				base.Show(false);
			});
			this.shouldDoCloseRoutine = false;
			return;
		}
		base.Show(false);
	}

	// Token: 0x060054DE RID: 21726 RVA: 0x001EC896 File Offset: 0x001EAA96
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.Show(false);
		}
		base.OnKeyDown(e);
	}

	// Token: 0x060054DF RID: 21727 RVA: 0x001EC8B8 File Offset: 0x001EAAB8
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (!show)
		{
			return;
		}
		if (KleiItems.HasUnopenedItem())
		{
			this.PresentNextUnopenedItem(true);
			this.shouldDoCloseRoutine = true;
			return;
		}
		base.Show(false);
	}

	// Token: 0x060054E0 RID: 21728 RVA: 0x001EC8E4 File Offset: 0x001EAAE4
	public void PresentNextUnopenedItem(bool firstItemPresentation = true)
	{
		foreach (KleiItems.ItemData itemData in PermitItems.IterateInventory())
		{
			global::Debug.LogError("UNIMPLEMENTED");
		}
		this.Show(false);
	}

	// Token: 0x060054E1 RID: 21729 RVA: 0x001EC93C File Offset: 0x001EAB3C
	public void PresentItem(KleiItems.ItemData item, bool firstItemPresentation)
	{
		this.giftRevealed = false;
		this.giftAcknowledged = false;
		this.activePresentationRoutine = base.StartCoroutine(this.PresentItemRoutine(item, firstItemPresentation));
		this.acceptButton.ClearOnClick();
		this.acknowledgeButton.ClearOnClick();
		this.acceptButton.onClick += delegate
		{
			this.giftRevealed = true;
		};
		this.acknowledgeButton.onClick += delegate
		{
			if (this.giftRevealed)
			{
				this.giftAcknowledged = true;
			}
		};
	}

	// Token: 0x060054E2 RID: 21730 RVA: 0x001EC9AF File Offset: 0x001EABAF
	private IEnumerator AnimateScreenInRoutine()
	{
		KFMOD.PlayUISound(GlobalAssets.GetSound("GiftItemDrop_Screen_Open", false));
		yield return Updater.Ease(delegate(Vector2 v2)
		{
			this.shieldMaskRect.sizeDelta = v2;
		}, this.shieldMaskRect.sizeDelta, new Vector2(this.shieldMaskRect.sizeDelta.x, 720f), 0.5f, Easing.CircInOut);
		yield return Updater.Ease(delegate(Vector2 v2)
		{
			this.shieldMaskRect.sizeDelta = v2;
		}, this.shieldMaskRect.sizeDelta, new Vector2(1152f, this.shieldMaskRect.sizeDelta.y), 0.25f, Easing.CircInOut);
		yield break;
	}

	// Token: 0x060054E3 RID: 21731 RVA: 0x001EC9BE File Offset: 0x001EABBE
	private IEnumerator AnimateScreenOutRoutine()
	{
		KFMOD.PlayUISound(GlobalAssets.GetSound("GiftItemDrop_Screen_Close", false));
		yield return Updater.Ease(delegate(Vector2 v2)
		{
			this.shieldMaskRect.sizeDelta = v2;
		}, this.shieldMaskRect.sizeDelta, new Vector2(8f, this.shieldMaskRect.sizeDelta.y), 0.25f, Easing.CircInOut);
		yield return Updater.Ease(delegate(Vector2 v2)
		{
			this.shieldMaskRect.sizeDelta = v2;
		}, this.shieldMaskRect.sizeDelta, new Vector2(this.shieldMaskRect.sizeDelta.x, 0f), 0.25f, Easing.CircInOut);
		yield break;
	}

	// Token: 0x060054E4 RID: 21732 RVA: 0x001EC9CD File Offset: 0x001EABCD
	private IEnumerator PresentItemRoutine(KleiItems.ItemData item, bool firstItem)
	{
		yield return null;
		if (item.ItemId == 0UL)
		{
			global::Debug.LogError("Could not find dropped item inventory.");
			yield break;
		}
		this.itemNameLabel.SetText("");
		this.itemDescriptionLabel.SetText("");
		this.permitVisualizer.ResetState();
		if (firstItem)
		{
			this.animatedPod.Play("idle", KAnim.PlayMode.Loop, 1f, 0f);
			this.acceptButtonRect.gameObject.SetActive(false);
			this.shieldMaskRect.sizeDelta = new Vector2(8f, 0f);
			this.shieldMaskRect.gameObject.SetActive(true);
		}
		if (firstItem)
		{
			this.closeButton.gameObject.SetActive(false);
			yield return Updater.WaitForSeconds(0.5f);
			yield return this.AnimateScreenInRoutine();
			yield return Updater.WaitForSeconds(0.125f);
			this.closeButton.gameObject.SetActive(true);
		}
		else
		{
			yield return Updater.WaitForSeconds(0.25f);
		}
		Vector2 animate_offset = new Vector2(0f, -30f);
		this.acceptButtonRect.FindOrAddComponent<CanvasGroup>().alpha = 0f;
		this.acceptButtonRect.gameObject.SetActive(true);
		this.acceptButtonPosition.SetOn(this.acceptButtonRect);
		this.animatedPod.Play("powerup", KAnim.PlayMode.Once, 1f, 0f);
		this.animatedPod.Queue("working_loop", KAnim.PlayMode.Loop, 1f, 0f);
		yield return Updater.WaitForSeconds(1.25f);
		yield return PresUtil.OffsetToAndFade(this.acceptButton.rectTransform(), animate_offset, 1f, 0.125f, Easing.ExpoOut);
		yield return Updater.Until(() => this.giftRevealed);
		yield return PresUtil.OffsetFromAndFade(this.acceptButton.rectTransform(), animate_offset, 0f, 0.125f, Easing.SmoothStep);
		this.animatedPod.Play("additional_pre", KAnim.PlayMode.Once, 1f, 0f);
		this.animatedPod.Queue("working_loop", KAnim.PlayMode.Loop, 1f, 0f);
		yield return Updater.WaitForSeconds(1f);
		PermitResource permit = Db.Get().Permits.Get(item.PermitId);
		this.permitVisualizer.ConfigureWith(permit);
		yield return this.permitVisualizer.AnimateIn();
		this.itemNameLabel.SetText(permit.Name);
		this.itemDescriptionLabel.SetText(permit.Description);
		this.itemNameLabelPosition.SetOn(this.itemNameLabel);
		this.itemDescriptionLabelPosition.SetOn(this.itemDescriptionLabel);
		yield return Updater.Parallel(new Updater[]
		{
			PresUtil.OffsetToAndFade(this.itemNameLabel.rectTransform(), animate_offset, 1f, 0.125f, Easing.CircInOut),
			PresUtil.OffsetToAndFade(this.itemDescriptionLabel.rectTransform(), animate_offset, 1f, 0.125f, Easing.CircInOut)
		});
		yield return Updater.Until(() => this.giftAcknowledged);
		this.animatedPod.Play("working_pst", KAnim.PlayMode.Once, 1f, 0f);
		this.animatedPod.Queue("idle", KAnim.PlayMode.Loop, 1f, 0f);
		yield return Updater.Parallel(new Updater[]
		{
			PresUtil.OffsetFromAndFade(this.itemNameLabel.rectTransform(), animate_offset, 0f, 0.125f, Easing.CircInOut),
			PresUtil.OffsetFromAndFade(this.itemDescriptionLabel.rectTransform(), animate_offset, 0f, 0.125f, Easing.CircInOut)
		});
		this.itemNameLabel.SetText("");
		this.itemDescriptionLabel.SetText("");
		yield return this.permitVisualizer.AnimateOut();
		permit = null;
		this.PresentNextUnopenedItem(false);
		yield break;
	}

	// Token: 0x040039AA RID: 14762
	[SerializeField]
	private RectTransform shieldMaskRect;

	// Token: 0x040039AB RID: 14763
	[SerializeField]
	private KButton closeButton;

	// Token: 0x040039AC RID: 14764
	[Header("Animated Item")]
	[SerializeField]
	private KleiItemDropScreen_PermitVis permitVisualizer;

	// Token: 0x040039AD RID: 14765
	[SerializeField]
	private KBatchedAnimController animatedPod;

	// Token: 0x040039AE RID: 14766
	[Header("Item Info")]
	[SerializeField]
	private LocText itemNameLabel;

	// Token: 0x040039AF RID: 14767
	[SerializeField]
	private LocText itemDescriptionLabel;

	// Token: 0x040039B0 RID: 14768
	[Header("Accept Button")]
	[SerializeField]
	private RectTransform acceptButtonRect;

	// Token: 0x040039B1 RID: 14769
	[SerializeField]
	private KButton acceptButton;

	// Token: 0x040039B2 RID: 14770
	[SerializeField]
	private KButton acknowledgeButton;

	// Token: 0x040039B3 RID: 14771
	private Coroutine activePresentationRoutine;

	// Token: 0x040039B4 RID: 14772
	private bool giftRevealed;

	// Token: 0x040039B5 RID: 14773
	private bool giftAcknowledged;

	// Token: 0x040039B6 RID: 14774
	public static KleiItemDropScreen Instance;

	// Token: 0x040039B7 RID: 14775
	private bool shouldDoCloseRoutine;

	// Token: 0x040039B8 RID: 14776
	private const float TEXT_AND_BUTTON_ANIMATE_OFFSET_Y = -30f;

	// Token: 0x040039B9 RID: 14777
	private PrefabDefinedUIPosition acceptButtonPosition = new PrefabDefinedUIPosition();

	// Token: 0x040039BA RID: 14778
	private PrefabDefinedUIPosition itemNameLabelPosition = new PrefabDefinedUIPosition();

	// Token: 0x040039BB RID: 14779
	private PrefabDefinedUIPosition itemDescriptionLabelPosition = new PrefabDefinedUIPosition();
}
