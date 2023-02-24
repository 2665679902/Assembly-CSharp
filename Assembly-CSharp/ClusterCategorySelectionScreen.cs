using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A51 RID: 2641
public class ClusterCategorySelectionScreen : NewGameFlowScreen
{
	// Token: 0x0600502E RID: 20526 RVA: 0x001CB42C File Offset: 0x001C962C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		HierarchyReferences component = this.vanillaButton.GetComponent<HierarchyReferences>();
		this.vanillaButtonHeader = component.GetReference<RectTransform>("HeaderBackground").GetComponent<Image>();
		this.vanillalButtonSelectionFrame = component.GetReference<RectTransform>("SelectionFrame").GetComponent<Image>();
		MultiToggle multiToggle = this.vanillaButton;
		multiToggle.onEnter = (System.Action)Delegate.Combine(multiToggle.onEnter, new System.Action(this.OnHoverEnterVanilla));
		MultiToggle multiToggle2 = this.vanillaButton;
		multiToggle2.onExit = (System.Action)Delegate.Combine(multiToggle2.onExit, new System.Action(this.OnHoverExitVanilla));
		MultiToggle multiToggle3 = this.vanillaButton;
		multiToggle3.onClick = (System.Action)Delegate.Combine(multiToggle3.onClick, new System.Action(this.OnClickVanilla));
		HierarchyReferences component2 = this.spacedOutButton.GetComponent<HierarchyReferences>();
		this.spacedOutButtonHeader = component2.GetReference<RectTransform>("HeaderBackground").GetComponent<Image>();
		this.spacedOutButtonSelectionFrame = component2.GetReference<RectTransform>("SelectionFrame").GetComponent<Image>();
		MultiToggle multiToggle4 = this.spacedOutButton;
		multiToggle4.onEnter = (System.Action)Delegate.Combine(multiToggle4.onEnter, new System.Action(this.OnHoverEnterSpacedOut));
		MultiToggle multiToggle5 = this.spacedOutButton;
		multiToggle5.onExit = (System.Action)Delegate.Combine(multiToggle5.onExit, new System.Action(this.OnHoverExitSpacedOut));
		MultiToggle multiToggle6 = this.spacedOutButton;
		multiToggle6.onClick = (System.Action)Delegate.Combine(multiToggle6.onClick, new System.Action(this.OnClickSpacedOut));
		this.closeButton.onClick += base.NavigateBackward;
	}

	// Token: 0x0600502F RID: 20527 RVA: 0x001CB5B0 File Offset: 0x001C97B0
	private void OnHoverEnterVanilla()
	{
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Mouseover", false));
		this.vanillalButtonSelectionFrame.SetAlpha(1f);
		this.vanillaButtonHeader.color = new Color(0.7019608f, 0.3647059f, 0.53333336f, 1f);
		this.descriptionArea.text = UI.FRONTEND.CLUSTERCATEGORYSELECTSCREEN.VANILLA_DESC;
	}

	// Token: 0x06005030 RID: 20528 RVA: 0x001CB618 File Offset: 0x001C9818
	private void OnHoverExitVanilla()
	{
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Mouseover", false));
		this.vanillalButtonSelectionFrame.SetAlpha(0f);
		this.vanillaButtonHeader.color = new Color(0.30980393f, 0.34117648f, 0.38431373f, 1f);
		this.descriptionArea.text = UI.FRONTEND.CLUSTERCATEGORYSELECTSCREEN.BLANK_DESC;
	}

	// Token: 0x06005031 RID: 20529 RVA: 0x001CB67E File Offset: 0x001C987E
	private void OnClickVanilla()
	{
		this.Deactivate();
		DestinationSelectPanel.ChosenClusterCategorySetting = 1;
		base.NavigateForward();
	}

	// Token: 0x06005032 RID: 20530 RVA: 0x001CB694 File Offset: 0x001C9894
	private void OnHoverEnterSpacedOut()
	{
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Mouseover", false));
		this.spacedOutButtonSelectionFrame.SetAlpha(1f);
		this.spacedOutButtonHeader.color = new Color(0.7019608f, 0.3647059f, 0.53333336f, 1f);
		this.descriptionArea.text = UI.FRONTEND.CLUSTERCATEGORYSELECTSCREEN.SPACEDOUT_DESC;
	}

	// Token: 0x06005033 RID: 20531 RVA: 0x001CB6FC File Offset: 0x001C98FC
	private void OnHoverExitSpacedOut()
	{
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Mouseover", false));
		this.spacedOutButtonSelectionFrame.SetAlpha(0f);
		this.spacedOutButtonHeader.color = new Color(0.30980393f, 0.34117648f, 0.38431373f, 1f);
		this.descriptionArea.text = UI.FRONTEND.CLUSTERCATEGORYSELECTSCREEN.BLANK_DESC;
	}

	// Token: 0x06005034 RID: 20532 RVA: 0x001CB762 File Offset: 0x001C9962
	private void OnClickSpacedOut()
	{
		this.Deactivate();
		DestinationSelectPanel.ChosenClusterCategorySetting = 2;
		base.NavigateForward();
	}

	// Token: 0x040035DB RID: 13787
	[SerializeField]
	private MultiToggle spacedOutButton;

	// Token: 0x040035DC RID: 13788
	private Image spacedOutButtonHeader;

	// Token: 0x040035DD RID: 13789
	private Image spacedOutButtonSelectionFrame;

	// Token: 0x040035DE RID: 13790
	[SerializeField]
	private MultiToggle vanillaButton;

	// Token: 0x040035DF RID: 13791
	private Image vanillaButtonHeader;

	// Token: 0x040035E0 RID: 13792
	private Image vanillalButtonSelectionFrame;

	// Token: 0x040035E1 RID: 13793
	[SerializeField]
	private LocText descriptionArea;

	// Token: 0x040035E2 RID: 13794
	[SerializeField]
	private KButton closeButton;

	// Token: 0x040035E3 RID: 13795
	[SerializeField]
	private KBatchedAnimController nosweatAnim;

	// Token: 0x040035E4 RID: 13796
	[SerializeField]
	private KBatchedAnimController survivalAnim;
}
