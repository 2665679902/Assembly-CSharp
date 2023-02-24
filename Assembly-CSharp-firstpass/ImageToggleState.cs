using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000053 RID: 83
[AddComponentMenu("KMonoBehaviour/Plugins/ImageToggleState")]
public class ImageToggleState : KMonoBehaviour
{
	// Token: 0x17000080 RID: 128
	// (get) Token: 0x0600033C RID: 828 RVA: 0x000113E8 File Offset: 0x0000F5E8
	public bool IsDisabled
	{
		get
		{
			return this.currentState == ImageToggleState.State.Disabled || this.currentState == ImageToggleState.State.DisabledActive;
		}
	}

	// Token: 0x0600033D RID: 829 RVA: 0x000113FD File Offset: 0x0000F5FD
	public new void Awake()
	{
		base.Awake();
		this.RefreshColorStyle();
		if (this.useStartingState)
		{
			this.SetState(this.startingState);
		}
	}

	// Token: 0x0600033E RID: 830 RVA: 0x00011420 File Offset: 0x0000F620
	[ContextMenu("Refresh Colour Style")]
	public void RefreshColorStyle()
	{
		if (this.colorStyleSetting != null)
		{
			this.ActiveColour = this.colorStyleSetting.activeColor;
			this.InactiveColour = this.colorStyleSetting.inactiveColor;
			this.DisabledColour = this.colorStyleSetting.disabledColor;
			this.DisabledActiveColour = this.colorStyleSetting.disabledActiveColor;
			this.HoverColour = this.colorStyleSetting.hoverColor;
			this.DisabledHoverColor = this.colorStyleSetting.disabledhoverColor;
		}
	}

	// Token: 0x0600033F RID: 831 RVA: 0x000114A4 File Offset: 0x0000F6A4
	public void SetSprites(Sprite disabled, Sprite inactive, Sprite active, Sprite disabledActive)
	{
		if (disabled != null)
		{
			this.DisabledSprite = disabled;
		}
		if (inactive != null)
		{
			this.InactiveSprite = inactive;
		}
		if (active != null)
		{
			this.ActiveSprite = active;
		}
		if (disabledActive != null)
		{
			this.DisabledActiveSprite = disabledActive;
		}
		this.useSprites = true;
	}

	// Token: 0x06000340 RID: 832 RVA: 0x000114FA File Offset: 0x0000F6FA
	public bool GetIsActive()
	{
		return this.isActive;
	}

	// Token: 0x06000341 RID: 833 RVA: 0x00011502 File Offset: 0x0000F702
	private void SetTargetImageColor(Color color)
	{
		this.TargetImage.color = color;
	}

	// Token: 0x06000342 RID: 834 RVA: 0x00011510 File Offset: 0x0000F710
	public void SetState(ImageToggleState.State newState)
	{
		if (this.currentState == newState)
		{
			return;
		}
		switch (newState)
		{
		case ImageToggleState.State.Disabled:
			this.SetDisabled();
			return;
		case ImageToggleState.State.Inactive:
			this.SetInactive();
			return;
		case ImageToggleState.State.Active:
			this.SetActive();
			return;
		case ImageToggleState.State.DisabledActive:
			this.SetDisabledActive();
			return;
		default:
			return;
		}
	}

	// Token: 0x06000343 RID: 835 RVA: 0x0001154E File Offset: 0x0000F74E
	public void SetActiveState(bool active)
	{
		if (active)
		{
			this.SetActive();
			return;
		}
		this.SetInactive();
	}

	// Token: 0x06000344 RID: 836 RVA: 0x00011560 File Offset: 0x0000F760
	public void SetActive()
	{
		if (this.currentState == ImageToggleState.State.Active)
		{
			return;
		}
		this.isActive = true;
		this.currentState = ImageToggleState.State.Active;
		if (this.TargetImage == null)
		{
			return;
		}
		this.SetTargetImageColor(this.ActiveColour);
		if (this.useSprites)
		{
			if (this.ActiveSprite != null && this.TargetImage.sprite != this.ActiveSprite)
			{
				this.TargetImage.sprite = this.ActiveSprite;
				return;
			}
			if (this.ActiveSprite == null)
			{
				this.TargetImage.sprite = null;
			}
		}
	}

	// Token: 0x06000345 RID: 837 RVA: 0x000115FA File Offset: 0x0000F7FA
	public void SetColorStyle(ColorStyleSetting style)
	{
		this.colorStyleSetting = style;
		this.RefreshColorStyle();
		this.ResetColor();
	}

	// Token: 0x06000346 RID: 838 RVA: 0x00011610 File Offset: 0x0000F810
	public void ResetColor()
	{
		switch (this.currentState)
		{
		case ImageToggleState.State.Disabled:
			this.SetTargetImageColor(this.DisabledColour);
			return;
		case ImageToggleState.State.Inactive:
			this.SetTargetImageColor(this.InactiveColour);
			return;
		case ImageToggleState.State.Active:
			this.SetTargetImageColor(this.ActiveColour);
			return;
		case ImageToggleState.State.DisabledActive:
			this.SetTargetImageColor(this.DisabledActiveColour);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000347 RID: 839 RVA: 0x0001166E File Offset: 0x0000F86E
	public void OnHoverIn()
	{
		this.SetTargetImageColor((this.currentState == ImageToggleState.State.Disabled || this.currentState == ImageToggleState.State.DisabledActive) ? this.DisabledHoverColor : this.HoverColour);
	}

	// Token: 0x06000348 RID: 840 RVA: 0x00011695 File Offset: 0x0000F895
	public void OnHoverOut()
	{
		this.ResetColor();
	}

	// Token: 0x06000349 RID: 841 RVA: 0x000116A0 File Offset: 0x0000F8A0
	public void SetInactive()
	{
		if (this.currentState == ImageToggleState.State.Inactive)
		{
			return;
		}
		this.isActive = false;
		this.currentState = ImageToggleState.State.Inactive;
		this.SetTargetImageColor(this.InactiveColour);
		if (this.TargetImage == null)
		{
			return;
		}
		if (this.useSprites)
		{
			if (this.InactiveSprite != null && this.TargetImage.sprite != this.InactiveSprite)
			{
				this.TargetImage.sprite = this.InactiveSprite;
				return;
			}
			if (this.InactiveSprite == null)
			{
				this.TargetImage.sprite = null;
			}
		}
	}

	// Token: 0x0600034A RID: 842 RVA: 0x0001173C File Offset: 0x0000F93C
	public void SetDisabled()
	{
		if (this.currentState == ImageToggleState.State.Disabled)
		{
			this.SetTargetImageColor(this.DisabledColour);
			return;
		}
		this.isActive = false;
		this.currentState = ImageToggleState.State.Disabled;
		this.SetTargetImageColor(this.DisabledColour);
		if (this.TargetImage == null)
		{
			return;
		}
		if (this.useSprites)
		{
			if (this.DisabledSprite != null && this.TargetImage.sprite != this.DisabledSprite)
			{
				this.TargetImage.sprite = this.DisabledSprite;
				return;
			}
			if (this.DisabledSprite == null)
			{
				this.TargetImage.sprite = null;
			}
		}
	}

	// Token: 0x0600034B RID: 843 RVA: 0x000117E4 File Offset: 0x0000F9E4
	public void SetDisabledActive()
	{
		this.isActive = false;
		this.currentState = ImageToggleState.State.DisabledActive;
		if (this.TargetImage == null)
		{
			return;
		}
		this.SetTargetImageColor(this.DisabledActiveColour);
		if (this.useSprites)
		{
			if (this.DisabledActiveSprite != null && this.TargetImage.sprite != this.DisabledActiveSprite)
			{
				this.TargetImage.sprite = this.DisabledActiveSprite;
				return;
			}
			if (this.DisabledActiveSprite == null)
			{
				this.TargetImage.sprite = null;
			}
		}
	}

	// Token: 0x040003ED RID: 1005
	public Image TargetImage;

	// Token: 0x040003EE RID: 1006
	public Sprite ActiveSprite;

	// Token: 0x040003EF RID: 1007
	public Sprite InactiveSprite;

	// Token: 0x040003F0 RID: 1008
	public Sprite DisabledSprite;

	// Token: 0x040003F1 RID: 1009
	public Sprite DisabledActiveSprite;

	// Token: 0x040003F2 RID: 1010
	public bool useSprites;

	// Token: 0x040003F3 RID: 1011
	public Color ActiveColour = Color.white;

	// Token: 0x040003F4 RID: 1012
	public Color InactiveColour = Color.white;

	// Token: 0x040003F5 RID: 1013
	public Color DisabledColour = Color.white;

	// Token: 0x040003F6 RID: 1014
	public Color DisabledActiveColour = Color.white;

	// Token: 0x040003F7 RID: 1015
	public Color HoverColour = Color.white;

	// Token: 0x040003F8 RID: 1016
	public Color DisabledHoverColor = Color.white;

	// Token: 0x040003F9 RID: 1017
	public ColorStyleSetting colorStyleSetting;

	// Token: 0x040003FA RID: 1018
	private bool isActive;

	// Token: 0x040003FB RID: 1019
	private ImageToggleState.State currentState = ImageToggleState.State.Inactive;

	// Token: 0x040003FC RID: 1020
	public bool useStartingState;

	// Token: 0x040003FD RID: 1021
	public ImageToggleState.State startingState = ImageToggleState.State.Inactive;

	// Token: 0x020009A7 RID: 2471
	public enum State
	{
		// Token: 0x0400217B RID: 8571
		Disabled,
		// Token: 0x0400217C RID: 8572
		Inactive,
		// Token: 0x0400217D RID: 8573
		Active,
		// Token: 0x0400217E RID: 8574
		DisabledActive
	}
}
