using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000ABF RID: 2751
[AddComponentMenu("KMonoBehaviour/scripts/InfoScreenSpriteItem")]
public class InfoScreenSpriteItem : KMonoBehaviour
{
	// Token: 0x0600541C RID: 21532 RVA: 0x001E8894 File Offset: 0x001E6A94
	public void SetSprite(Sprite sprite)
	{
		this.image.sprite = sprite;
		float num = sprite.rect.width / sprite.rect.height;
		this.layout.preferredWidth = this.layout.preferredHeight * num;
	}

	// Token: 0x04003929 RID: 14633
	[SerializeField]
	private Image image;

	// Token: 0x0400392A RID: 14634
	[SerializeField]
	private LayoutElement layout;
}
