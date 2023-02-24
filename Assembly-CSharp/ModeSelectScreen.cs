using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B32 RID: 2866
public class ModeSelectScreen : NewGameFlowScreen
{
	// Token: 0x060058A6 RID: 22694 RVA: 0x00202009 File Offset: 0x00200209
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.LoadWorldAndClusterData();
	}

	// Token: 0x060058A7 RID: 22695 RVA: 0x00202018 File Offset: 0x00200218
	protected override void OnSpawn()
	{
		base.OnSpawn();
		HierarchyReferences component = this.survivalButton.GetComponent<HierarchyReferences>();
		this.survivalButtonHeader = component.GetReference<RectTransform>("HeaderBackground").GetComponent<Image>();
		this.survivalButtonSelectionFrame = component.GetReference<RectTransform>("SelectionFrame").GetComponent<Image>();
		MultiToggle multiToggle = this.survivalButton;
		multiToggle.onEnter = (System.Action)Delegate.Combine(multiToggle.onEnter, new System.Action(this.OnHoverEnterSurvival));
		MultiToggle multiToggle2 = this.survivalButton;
		multiToggle2.onExit = (System.Action)Delegate.Combine(multiToggle2.onExit, new System.Action(this.OnHoverExitSurvival));
		MultiToggle multiToggle3 = this.survivalButton;
		multiToggle3.onClick = (System.Action)Delegate.Combine(multiToggle3.onClick, new System.Action(this.OnClickSurvival));
		HierarchyReferences component2 = this.nosweatButton.GetComponent<HierarchyReferences>();
		this.nosweatButtonHeader = component2.GetReference<RectTransform>("HeaderBackground").GetComponent<Image>();
		this.nosweatButtonSelectionFrame = component2.GetReference<RectTransform>("SelectionFrame").GetComponent<Image>();
		MultiToggle multiToggle4 = this.nosweatButton;
		multiToggle4.onEnter = (System.Action)Delegate.Combine(multiToggle4.onEnter, new System.Action(this.OnHoverEnterNosweat));
		MultiToggle multiToggle5 = this.nosweatButton;
		multiToggle5.onExit = (System.Action)Delegate.Combine(multiToggle5.onExit, new System.Action(this.OnHoverExitNosweat));
		MultiToggle multiToggle6 = this.nosweatButton;
		multiToggle6.onClick = (System.Action)Delegate.Combine(multiToggle6.onClick, new System.Action(this.OnClickNosweat));
		this.closeButton.onClick += base.NavigateBackward;
	}

	// Token: 0x060058A8 RID: 22696 RVA: 0x0020219C File Offset: 0x0020039C
	private void OnHoverEnterSurvival()
	{
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Mouseover", false));
		this.survivalButtonSelectionFrame.SetAlpha(1f);
		this.survivalButtonHeader.color = new Color(0.7019608f, 0.3647059f, 0.53333336f, 1f);
		this.descriptionArea.text = UI.FRONTEND.MODESELECTSCREEN.SURVIVAL_DESC;
	}

	// Token: 0x060058A9 RID: 22697 RVA: 0x00202204 File Offset: 0x00200404
	private void OnHoverExitSurvival()
	{
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Mouseover", false));
		this.survivalButtonSelectionFrame.SetAlpha(0f);
		this.survivalButtonHeader.color = new Color(0.30980393f, 0.34117648f, 0.38431373f, 1f);
		this.descriptionArea.text = UI.FRONTEND.MODESELECTSCREEN.BLANK_DESC;
	}

	// Token: 0x060058AA RID: 22698 RVA: 0x0020226A File Offset: 0x0020046A
	private void OnClickSurvival()
	{
		this.Deactivate();
		CustomGameSettings.Instance.SetSurvivalDefaults();
		base.NavigateForward();
	}

	// Token: 0x060058AB RID: 22699 RVA: 0x00202282 File Offset: 0x00200482
	private void LoadWorldAndClusterData()
	{
		if (ModeSelectScreen.dataLoaded)
		{
			return;
		}
		CustomGameSettings.Instance.LoadClusters();
		Global.Instance.modManager.Report(base.gameObject);
		ModeSelectScreen.dataLoaded = true;
	}

	// Token: 0x060058AC RID: 22700 RVA: 0x002022B4 File Offset: 0x002004B4
	private void OnHoverEnterNosweat()
	{
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Mouseover", false));
		this.nosweatButtonSelectionFrame.SetAlpha(1f);
		this.nosweatButtonHeader.color = new Color(0.7019608f, 0.3647059f, 0.53333336f, 1f);
		this.descriptionArea.text = UI.FRONTEND.MODESELECTSCREEN.NOSWEAT_DESC;
	}

	// Token: 0x060058AD RID: 22701 RVA: 0x0020231C File Offset: 0x0020051C
	private void OnHoverExitNosweat()
	{
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Mouseover", false));
		this.nosweatButtonSelectionFrame.SetAlpha(0f);
		this.nosweatButtonHeader.color = new Color(0.30980393f, 0.34117648f, 0.38431373f, 1f);
		this.descriptionArea.text = UI.FRONTEND.MODESELECTSCREEN.BLANK_DESC;
	}

	// Token: 0x060058AE RID: 22702 RVA: 0x00202382 File Offset: 0x00200582
	private void OnClickNosweat()
	{
		this.Deactivate();
		CustomGameSettings.Instance.SetNosweatDefaults();
		base.NavigateForward();
	}

	// Token: 0x04003BE3 RID: 15331
	[SerializeField]
	private MultiToggle nosweatButton;

	// Token: 0x04003BE4 RID: 15332
	private Image nosweatButtonHeader;

	// Token: 0x04003BE5 RID: 15333
	private Image nosweatButtonSelectionFrame;

	// Token: 0x04003BE6 RID: 15334
	[SerializeField]
	private MultiToggle survivalButton;

	// Token: 0x04003BE7 RID: 15335
	private Image survivalButtonHeader;

	// Token: 0x04003BE8 RID: 15336
	private Image survivalButtonSelectionFrame;

	// Token: 0x04003BE9 RID: 15337
	[SerializeField]
	private LocText descriptionArea;

	// Token: 0x04003BEA RID: 15338
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003BEB RID: 15339
	[SerializeField]
	private KBatchedAnimController nosweatAnim;

	// Token: 0x04003BEC RID: 15340
	[SerializeField]
	private KBatchedAnimController survivalAnim;

	// Token: 0x04003BED RID: 15341
	private static bool dataLoaded;
}
