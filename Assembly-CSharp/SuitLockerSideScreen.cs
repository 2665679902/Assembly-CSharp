using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000BE6 RID: 3046
public class SuitLockerSideScreen : SideScreenContent
{
	// Token: 0x06005FEF RID: 24559 RVA: 0x002319EA File Offset: 0x0022FBEA
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06005FF0 RID: 24560 RVA: 0x002319F2 File Offset: 0x0022FBF2
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<SuitLocker>() != null;
	}

	// Token: 0x06005FF1 RID: 24561 RVA: 0x00231A00 File Offset: 0x0022FC00
	public override void SetTarget(GameObject target)
	{
		this.suitLocker = target.GetComponent<SuitLocker>();
		this.initialConfigRequestSuitButton.GetComponentInChildren<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.SUIT_SIDE_SCREEN.CONFIG_REQUEST_SUIT_TOOLTIP);
		this.initialConfigNoSuitButton.GetComponentInChildren<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.SUIT_SIDE_SCREEN.CONFIG_NO_SUIT_TOOLTIP);
		this.initialConfigRequestSuitButton.ClearOnClick();
		this.initialConfigRequestSuitButton.onClick += delegate
		{
			this.suitLocker.ConfigRequestSuit();
		};
		this.initialConfigNoSuitButton.ClearOnClick();
		this.initialConfigNoSuitButton.onClick += delegate
		{
			this.suitLocker.ConfigNoSuit();
		};
		this.regularConfigRequestSuitButton.ClearOnClick();
		this.regularConfigRequestSuitButton.onClick += delegate
		{
			if (this.suitLocker.smi.sm.isWaitingForSuit.Get(this.suitLocker.smi))
			{
				this.suitLocker.ConfigNoSuit();
				return;
			}
			this.suitLocker.ConfigRequestSuit();
		};
		this.regularConfigDropSuitButton.ClearOnClick();
		this.regularConfigDropSuitButton.onClick += delegate
		{
			this.suitLocker.DropSuit();
		};
	}

	// Token: 0x06005FF2 RID: 24562 RVA: 0x00231AD8 File Offset: 0x0022FCD8
	private void Update()
	{
		bool flag = this.suitLocker.smi.sm.isConfigured.Get(this.suitLocker.smi);
		this.initialConfigScreen.gameObject.SetActive(!flag);
		this.regularConfigScreen.gameObject.SetActive(flag);
		bool flag2 = this.suitLocker.GetStoredOutfit() != null;
		bool flag3 = this.suitLocker.smi.sm.isWaitingForSuit.Get(this.suitLocker.smi);
		this.regularConfigRequestSuitButton.isInteractable = !flag2;
		if (!flag3)
		{
			this.regularConfigRequestSuitButton.GetComponentInChildren<LocText>().text = UI.UISIDESCREENS.SUIT_SIDE_SCREEN.CONFIG_REQUEST_SUIT;
			this.regularConfigRequestSuitButton.GetComponentInChildren<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.SUIT_SIDE_SCREEN.CONFIG_REQUEST_SUIT_TOOLTIP);
		}
		else
		{
			this.regularConfigRequestSuitButton.GetComponentInChildren<LocText>().text = UI.UISIDESCREENS.SUIT_SIDE_SCREEN.CONFIG_CANCEL_REQUEST;
			this.regularConfigRequestSuitButton.GetComponentInChildren<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.SUIT_SIDE_SCREEN.CONFIG_CANCEL_REQUEST_TOOLTIP);
		}
		if (flag2)
		{
			this.regularConfigDropSuitButton.isInteractable = true;
			this.regularConfigDropSuitButton.GetComponentInChildren<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.SUIT_SIDE_SCREEN.CONFIG_DROP_SUIT_TOOLTIP);
		}
		else
		{
			this.regularConfigDropSuitButton.isInteractable = false;
			this.regularConfigDropSuitButton.GetComponentInChildren<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.SUIT_SIDE_SCREEN.CONFIG_DROP_SUIT_NO_SUIT_TOOLTIP);
		}
		KSelectable component = this.suitLocker.GetComponent<KSelectable>();
		if (component != null)
		{
			StatusItemGroup.Entry statusItem = component.GetStatusItem(Db.Get().StatusItemCategories.Main);
			if (statusItem.item != null)
			{
				this.regularConfigLabel.text = statusItem.item.GetName(statusItem.data);
				this.regularConfigLabel.GetComponentInChildren<ToolTip>().SetSimpleTooltip(statusItem.item.GetTooltip(statusItem.data));
			}
		}
	}

	// Token: 0x040041AE RID: 16814
	[SerializeField]
	private GameObject initialConfigScreen;

	// Token: 0x040041AF RID: 16815
	[SerializeField]
	private GameObject regularConfigScreen;

	// Token: 0x040041B0 RID: 16816
	[SerializeField]
	private LocText initialConfigLabel;

	// Token: 0x040041B1 RID: 16817
	[SerializeField]
	private KButton initialConfigRequestSuitButton;

	// Token: 0x040041B2 RID: 16818
	[SerializeField]
	private KButton initialConfigNoSuitButton;

	// Token: 0x040041B3 RID: 16819
	[SerializeField]
	private LocText regularConfigLabel;

	// Token: 0x040041B4 RID: 16820
	[SerializeField]
	private KButton regularConfigRequestSuitButton;

	// Token: 0x040041B5 RID: 16821
	[SerializeField]
	private KButton regularConfigDropSuitButton;

	// Token: 0x040041B6 RID: 16822
	private SuitLocker suitLocker;
}
