﻿using System;
using UnityEngine;

// Token: 0x02000BCE RID: 3022
public class PlayerControlledToggleSideScreen : SideScreenContent, IRenderEveryTick
{
	// Token: 0x06005F0A RID: 24330 RVA: 0x0022C810 File Offset: 0x0022AA10
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.toggleButton.onClick += this.ClickToggle;
		this.togglePendingStatusItem = new StatusItem("PlayerControlledToggleSideScreen", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
	}

	// Token: 0x06005F0B RID: 24331 RVA: 0x0022C863 File Offset: 0x0022AA63
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<IPlayerControlledToggle>() != null;
	}

	// Token: 0x06005F0C RID: 24332 RVA: 0x0022C870 File Offset: 0x0022AA70
	public void RenderEveryTick(float dt)
	{
		if (base.isActiveAndEnabled)
		{
			if (!this.keyDown && (Input.GetKeyDown(KeyCode.Return) & (Time.unscaledTime - this.lastKeyboardShortcutTime > 0.1f)))
			{
				if (SpeedControlScreen.Instance.IsPaused)
				{
					this.RequestToggle();
				}
				else
				{
					this.Toggle();
				}
				this.lastKeyboardShortcutTime = Time.unscaledTime;
				this.keyDown = true;
			}
			if (this.keyDown && Input.GetKeyUp(KeyCode.Return))
			{
				this.keyDown = false;
			}
		}
	}

	// Token: 0x06005F0D RID: 24333 RVA: 0x0022C8EE File Offset: 0x0022AAEE
	private void ClickToggle()
	{
		if (SpeedControlScreen.Instance.IsPaused)
		{
			this.RequestToggle();
			return;
		}
		this.Toggle();
	}

	// Token: 0x06005F0E RID: 24334 RVA: 0x0022C90C File Offset: 0x0022AB0C
	private void RequestToggle()
	{
		this.target.ToggleRequested = !this.target.ToggleRequested;
		if (this.target.ToggleRequested && SpeedControlScreen.Instance.IsPaused)
		{
			this.target.GetSelectable().SetStatusItem(Db.Get().StatusItemCategories.Main, this.togglePendingStatusItem, this);
		}
		else
		{
			this.target.GetSelectable().SetStatusItem(Db.Get().StatusItemCategories.Main, null, null);
		}
		this.UpdateVisuals(this.target.ToggleRequested ? (!this.target.ToggledOn()) : this.target.ToggledOn(), true);
	}

	// Token: 0x06005F0F RID: 24335 RVA: 0x0022C9C8 File Offset: 0x0022ABC8
	public override void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.target = new_target.GetComponent<IPlayerControlledToggle>();
		if (this.target == null)
		{
			global::Debug.LogError("The gameObject received is not an IPlayerControlledToggle");
			return;
		}
		this.UpdateVisuals(this.target.ToggleRequested ? (!this.target.ToggledOn()) : this.target.ToggledOn(), false);
		this.titleKey = this.target.SideScreenTitleKey;
	}

	// Token: 0x06005F10 RID: 24336 RVA: 0x0022CA48 File Offset: 0x0022AC48
	private void Toggle()
	{
		this.target.ToggledByPlayer();
		this.UpdateVisuals(this.target.ToggledOn(), true);
		this.target.ToggleRequested = false;
		this.target.GetSelectable().RemoveStatusItem(this.togglePendingStatusItem, false);
	}

	// Token: 0x06005F11 RID: 24337 RVA: 0x0022CA98 File Offset: 0x0022AC98
	private void UpdateVisuals(bool state, bool smooth)
	{
		if (state != this.currentState)
		{
			if (smooth)
			{
				this.kbac.Play(state ? PlayerControlledToggleSideScreen.ON_ANIMS : PlayerControlledToggleSideScreen.OFF_ANIMS, KAnim.PlayMode.Once);
			}
			else
			{
				this.kbac.Play(state ? PlayerControlledToggleSideScreen.ON_ANIMS[1] : PlayerControlledToggleSideScreen.OFF_ANIMS[1], KAnim.PlayMode.Once, 1f, 0f);
			}
		}
		this.currentState = state;
	}

	// Token: 0x0400410D RID: 16653
	public IPlayerControlledToggle target;

	// Token: 0x0400410E RID: 16654
	public KButton toggleButton;

	// Token: 0x0400410F RID: 16655
	protected static readonly HashedString[] ON_ANIMS = new HashedString[] { "on_pre", "on" };

	// Token: 0x04004110 RID: 16656
	protected static readonly HashedString[] OFF_ANIMS = new HashedString[] { "off_pre", "off" };

	// Token: 0x04004111 RID: 16657
	public float animScaleBase = 0.25f;

	// Token: 0x04004112 RID: 16658
	private StatusItem togglePendingStatusItem;

	// Token: 0x04004113 RID: 16659
	[SerializeField]
	private KBatchedAnimController kbac;

	// Token: 0x04004114 RID: 16660
	private float lastKeyboardShortcutTime;

	// Token: 0x04004115 RID: 16661
	private const float KEYBOARD_COOLDOWN = 0.1f;

	// Token: 0x04004116 RID: 16662
	private bool keyDown;

	// Token: 0x04004117 RID: 16663
	private bool currentState;
}
