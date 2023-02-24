using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B86 RID: 2950
public class ShadowImage : ShadowRect
{
	// Token: 0x06005CB2 RID: 23730 RVA: 0x0021E438 File Offset: 0x0021C638
	protected override void MatchRect()
	{
		base.MatchRect();
		if (this.RectMain == null || this.RectShadow == null)
		{
			return;
		}
		if (this.shadowImage == null)
		{
			this.shadowImage = this.RectShadow.GetComponent<Image>();
		}
		if (this.mainImage == null)
		{
			this.mainImage = this.RectMain.GetComponent<Image>();
		}
		if (this.mainImage == null)
		{
			if (this.shadowImage != null)
			{
				this.shadowImage.color = Color.clear;
			}
			return;
		}
		if (this.shadowImage == null)
		{
			return;
		}
		if (this.shadowImage.sprite != this.mainImage.sprite)
		{
			this.shadowImage.sprite = this.mainImage.sprite;
		}
		if (this.shadowImage.color != this.shadowColor)
		{
			if (this.shadowImage.sprite != null)
			{
				this.shadowImage.color = this.shadowColor;
				return;
			}
			this.shadowImage.color = Color.clear;
		}
	}

	// Token: 0x04003F5D RID: 16221
	private Image shadowImage;

	// Token: 0x04003F5E RID: 16222
	private Image mainImage;
}
