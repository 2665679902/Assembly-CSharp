using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000ABA RID: 2746
public class IncrementorToggle : MultiToggle
{
	// Token: 0x060053F7 RID: 21495 RVA: 0x001E82F0 File Offset: 0x001E64F0
	protected override void Update()
	{
		if (this.clickHeldDown)
		{
			this.totalHeldTime += Time.unscaledDeltaTime;
			if (this.timeToNextIncrement <= 0f)
			{
				this.PlayClickSound();
				this.onClick();
				this.timeToNextIncrement = Mathf.Lerp(this.timeBetweenIncrementsMax, this.timeBetweenIncrementsMin, this.totalHeldTime / 2.5f);
				return;
			}
			this.timeToNextIncrement -= Time.unscaledDeltaTime;
		}
	}

	// Token: 0x060053F8 RID: 21496 RVA: 0x001E836C File Offset: 0x001E656C
	private void PlayClickSound()
	{
		if (this.play_sound_on_click)
		{
			if (this.states[this.state].on_click_override_sound_path == "")
			{
				KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Click", false));
				return;
			}
			KFMOD.PlayUISound(GlobalAssets.GetSound(this.states[this.state].on_click_override_sound_path, false));
		}
	}

	// Token: 0x060053F9 RID: 21497 RVA: 0x001E83D5 File Offset: 0x001E65D5
	public override void OnPointerUp(PointerEventData eventData)
	{
		base.OnPointerUp(eventData);
		this.timeToNextIncrement = this.timeBetweenIncrementsMax;
	}

	// Token: 0x060053FA RID: 21498 RVA: 0x001E83EC File Offset: 0x001E65EC
	public override void OnPointerDown(PointerEventData eventData)
	{
		if (!this.clickHeldDown)
		{
			this.clickHeldDown = true;
			this.PlayClickSound();
			if (this.onClick != null)
			{
				this.onClick();
			}
		}
		if (this.states.Length - 1 < this.state)
		{
			global::Debug.LogWarning("Multi toggle has too few / no states");
		}
		base.RefreshHoverColor();
	}

	// Token: 0x060053FB RID: 21499 RVA: 0x001E8443 File Offset: 0x001E6643
	public override void OnPointerClick(PointerEventData eventData)
	{
		base.RefreshHoverColor();
	}

	// Token: 0x04003910 RID: 14608
	private float timeBetweenIncrementsMin = 0.033f;

	// Token: 0x04003911 RID: 14609
	private float timeBetweenIncrementsMax = 0.25f;

	// Token: 0x04003912 RID: 14610
	private const float incrementAccelerationScale = 2.5f;

	// Token: 0x04003913 RID: 14611
	private float timeToNextIncrement;
}
