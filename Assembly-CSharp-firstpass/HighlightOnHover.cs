using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000051 RID: 81
[AddComponentMenu("KMonoBehaviour/Plugins/HighlightOnHover")]
public class HighlightOnHover : KMonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06000336 RID: 822 RVA: 0x00011390 File Offset: 0x0000F590
	public void OnPointerEnter(PointerEventData data)
	{
		this.image.ColorState = KImage.ColorSelector.Hover;
	}

	// Token: 0x06000337 RID: 823 RVA: 0x0001139E File Offset: 0x0000F59E
	public void OnPointerExit(PointerEventData data)
	{
		this.image.ColorState = KImage.ColorSelector.Inactive;
	}

	// Token: 0x040003EB RID: 1003
	public KImage image;
}
