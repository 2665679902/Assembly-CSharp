using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000807 RID: 2055
[AddComponentMenu("KMonoBehaviour/scripts/LoreBearer")]
public class LoreBearer : KMonoBehaviour, ISidescreenButtonControl
{
	// Token: 0x17000436 RID: 1078
	// (get) Token: 0x06003B84 RID: 15236 RVA: 0x0014A255 File Offset: 0x00148455
	public string content
	{
		get
		{
			return Strings.Get("STRINGS.LORE.BUILDINGS." + base.gameObject.name + ".ENTRY");
		}
	}

	// Token: 0x06003B85 RID: 15237 RVA: 0x0014A27B File Offset: 0x0014847B
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06003B86 RID: 15238 RVA: 0x0014A283 File Offset: 0x00148483
	public LoreBearer Internal_SetContent(LoreBearerAction action)
	{
		this.displayContentAction = action;
		return this;
	}

	// Token: 0x06003B87 RID: 15239 RVA: 0x0014A28D File Offset: 0x0014848D
	public static InfoDialogScreen ShowPopupDialog()
	{
		return (InfoDialogScreen)GameScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, GameScreenManager.Instance.ssOverlayCanvas.gameObject, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay);
	}

	// Token: 0x06003B88 RID: 15240 RVA: 0x0014A2C0 File Offset: 0x001484C0
	private void OnClickRead()
	{
		InfoDialogScreen infoDialogScreen = LoreBearer.ShowPopupDialog().SetHeader(base.gameObject.GetComponent<KSelectable>().GetProperName()).AddDefaultOK(true);
		if (this.BeenClicked)
		{
			infoDialogScreen.AddPlainText(this.BeenSearched);
			return;
		}
		this.BeenClicked = true;
		if (DlcManager.IsExpansion1Active())
		{
			Scenario.SpawnPrefab(Grid.PosToCell(base.gameObject), 0, 1, "OrbitalResearchDatabank", Grid.SceneLayer.Front).SetActive(true);
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, Assets.GetPrefab("OrbitalResearchDatabank".ToTag()).GetProperName(), base.gameObject.transform, 1.5f, false);
		}
		if (this.displayContentAction != null)
		{
			this.displayContentAction(infoDialogScreen);
			return;
		}
		LoreBearerUtil.UnlockNextJournalEntry(infoDialogScreen);
	}

	// Token: 0x17000437 RID: 1079
	// (get) Token: 0x06003B89 RID: 15241 RVA: 0x0014A386 File Offset: 0x00148586
	public string SidescreenButtonText
	{
		get
		{
			return this.BeenClicked ? UI.USERMENUACTIONS.READLORE.ALREADYINSPECTED : UI.USERMENUACTIONS.READLORE.NAME;
		}
	}

	// Token: 0x17000438 RID: 1080
	// (get) Token: 0x06003B8A RID: 15242 RVA: 0x0014A3A1 File Offset: 0x001485A1
	public string SidescreenButtonTooltip
	{
		get
		{
			return this.BeenClicked ? UI.USERMENUACTIONS.READLORE.TOOLTIP_ALREADYINSPECTED : UI.USERMENUACTIONS.READLORE.TOOLTIP;
		}
	}

	// Token: 0x06003B8B RID: 15243 RVA: 0x0014A3BC File Offset: 0x001485BC
	public bool SidescreenEnabled()
	{
		return true;
	}

	// Token: 0x06003B8C RID: 15244 RVA: 0x0014A3BF File Offset: 0x001485BF
	public void OnSidescreenButtonPressed()
	{
		this.OnClickRead();
	}

	// Token: 0x06003B8D RID: 15245 RVA: 0x0014A3C7 File Offset: 0x001485C7
	public bool SidescreenButtonInteractable()
	{
		return !this.BeenClicked;
	}

	// Token: 0x06003B8E RID: 15246 RVA: 0x0014A3D2 File Offset: 0x001485D2
	public int ButtonSideScreenSortOrder()
	{
		return 20;
	}

	// Token: 0x06003B8F RID: 15247 RVA: 0x0014A3D6 File Offset: 0x001485D6
	public void SetButtonTextOverride(ButtonMenuTextOverride text)
	{
		throw new NotImplementedException();
	}

	// Token: 0x040026D8 RID: 9944
	[Serialize]
	private bool BeenClicked;

	// Token: 0x040026D9 RID: 9945
	public string BeenSearched = UI.USERMENUACTIONS.READLORE.ALREADY_SEARCHED;

	// Token: 0x040026DA RID: 9946
	private LoreBearerAction displayContentAction;
}
