using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AB9 RID: 2745
[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
public class ImageAspectRatioFitter : AspectRatioFitter
{
	// Token: 0x060053F3 RID: 21491 RVA: 0x001E8288 File Offset: 0x001E6488
	private void UpdateAspectRatio()
	{
		base.aspectRatio = this.targetImage.sprite.rect.width / this.targetImage.sprite.rect.height;
	}

	// Token: 0x060053F4 RID: 21492 RVA: 0x001E82CC File Offset: 0x001E64CC
	protected override void OnTransformParentChanged()
	{
		this.UpdateAspectRatio();
		base.OnTransformParentChanged();
	}

	// Token: 0x060053F5 RID: 21493 RVA: 0x001E82DA File Offset: 0x001E64DA
	protected override void OnRectTransformDimensionsChange()
	{
		this.UpdateAspectRatio();
		base.OnRectTransformDimensionsChange();
	}

	// Token: 0x0400390F RID: 14607
	[SerializeField]
	private Image targetImage;
}
