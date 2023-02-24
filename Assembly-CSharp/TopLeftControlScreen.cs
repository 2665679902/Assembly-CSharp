using System;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000C12 RID: 3090
public class TopLeftControlScreen : KScreen
{
	// Token: 0x060061EA RID: 25066 RVA: 0x00242FF3 File Offset: 0x002411F3
	public static void DestroyInstance()
	{
		TopLeftControlScreen.Instance = null;
	}

	// Token: 0x060061EB RID: 25067 RVA: 0x00242FFC File Offset: 0x002411FC
	protected override void OnActivate()
	{
		base.OnActivate();
		TopLeftControlScreen.Instance = this;
		this.RefreshName();
		KInputManager.InputChange.AddListener(new UnityAction(this.ResetToolTip));
		this.UpdateSandboxToggleState();
		MultiToggle multiToggle = this.sandboxToggle;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(this.OnClickSandboxToggle));
		Game.Instance.Subscribe(-1948169901, delegate(object data)
		{
			this.UpdateSandboxToggleState();
		});
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.secondaryRow);
	}

	// Token: 0x060061EC RID: 25068 RVA: 0x00243085 File Offset: 0x00241285
	protected override void OnForcedCleanUp()
	{
		KInputManager.InputChange.RemoveListener(new UnityAction(this.ResetToolTip));
		base.OnForcedCleanUp();
	}

	// Token: 0x060061ED RID: 25069 RVA: 0x002430A3 File Offset: 0x002412A3
	public void RefreshName()
	{
		if (SaveGame.Instance != null)
		{
			this.locText.text = SaveGame.Instance.BaseName;
		}
	}

	// Token: 0x060061EE RID: 25070 RVA: 0x002430C8 File Offset: 0x002412C8
	public void ResetToolTip()
	{
		if (this.CheckSandboxModeLocked())
		{
			this.sandboxToggle.GetComponent<ToolTip>().SetSimpleTooltip(GameUtil.ReplaceHotkeyString(UI.SANDBOX_TOGGLE.TOOLTIP_LOCKED, global::Action.ToggleSandboxTools));
			return;
		}
		this.sandboxToggle.GetComponent<ToolTip>().SetSimpleTooltip(GameUtil.ReplaceHotkeyString(UI.SANDBOX_TOGGLE.TOOLTIP_UNLOCKED, global::Action.ToggleSandboxTools));
	}

	// Token: 0x060061EF RID: 25071 RVA: 0x00243128 File Offset: 0x00241328
	public void UpdateSandboxToggleState()
	{
		if (this.CheckSandboxModeLocked())
		{
			this.sandboxToggle.GetComponent<ToolTip>().SetSimpleTooltip(GameUtil.ReplaceHotkeyString(UI.SANDBOX_TOGGLE.TOOLTIP_LOCKED, global::Action.ToggleSandboxTools));
			this.sandboxToggle.ChangeState(0);
		}
		else
		{
			this.sandboxToggle.GetComponent<ToolTip>().SetSimpleTooltip(GameUtil.ReplaceHotkeyString(UI.SANDBOX_TOGGLE.TOOLTIP_UNLOCKED, global::Action.ToggleSandboxTools));
			this.sandboxToggle.ChangeState(Game.Instance.SandboxModeActive ? 2 : 1);
		}
		this.sandboxToggle.gameObject.SetActive(SaveGame.Instance.sandboxEnabled);
	}

	// Token: 0x060061F0 RID: 25072 RVA: 0x002431C8 File Offset: 0x002413C8
	private void OnClickSandboxToggle()
	{
		if (this.CheckSandboxModeLocked())
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Negative", false));
		}
		else
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click", false));
			Game.Instance.SandboxModeActive = !Game.Instance.SandboxModeActive;
		}
		this.UpdateSandboxToggleState();
	}

	// Token: 0x060061F1 RID: 25073 RVA: 0x0024321C File Offset: 0x0024141C
	private bool CheckSandboxModeLocked()
	{
		return !SaveGame.Instance.sandboxEnabled;
	}

	// Token: 0x040043BA RID: 17338
	public static TopLeftControlScreen Instance;

	// Token: 0x040043BB RID: 17339
	[SerializeField]
	private MultiToggle sandboxToggle;

	// Token: 0x040043BC RID: 17340
	[SerializeField]
	private LocText locText;

	// Token: 0x040043BD RID: 17341
	[SerializeField]
	private RectTransform secondaryRow;
}
