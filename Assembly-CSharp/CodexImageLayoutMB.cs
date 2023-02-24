using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000A5D RID: 2653
public class CodexImageLayoutMB : UIBehaviour
{
	// Token: 0x06005126 RID: 20774 RVA: 0x001D4D60 File Offset: 0x001D2F60
	protected override void OnRectTransformDimensionsChange()
	{
		base.OnRectTransformDimensionsChange();
		if (this.image.preserveAspect && this.image.sprite != null && this.image.sprite)
		{
			float num = this.image.sprite.rect.height / this.image.sprite.rect.width;
			this.layoutElement.preferredHeight = num * this.rectTransform.sizeDelta.x;
			this.layoutElement.minHeight = this.layoutElement.preferredHeight;
			return;
		}
		this.layoutElement.preferredHeight = -1f;
		this.layoutElement.preferredWidth = -1f;
		this.layoutElement.minHeight = -1f;
		this.layoutElement.minWidth = -1f;
		this.layoutElement.flexibleHeight = -1f;
		this.layoutElement.flexibleWidth = -1f;
		this.layoutElement.ignoreLayout = false;
	}

	// Token: 0x04003663 RID: 13923
	public RectTransform rectTransform;

	// Token: 0x04003664 RID: 13924
	public LayoutElement layoutElement;

	// Token: 0x04003665 RID: 13925
	public Image image;
}
