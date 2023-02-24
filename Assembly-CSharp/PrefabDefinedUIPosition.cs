using System;
using UnityEngine;

// Token: 0x02000ACF RID: 2767
public class PrefabDefinedUIPosition
{
	// Token: 0x060054F0 RID: 21744 RVA: 0x001ECA87 File Offset: 0x001EAC87
	public void SetOn(GameObject gameObject)
	{
		if (this.position.HasValue)
		{
			gameObject.rectTransform().anchoredPosition = this.position.Value;
			return;
		}
		this.position = gameObject.rectTransform().anchoredPosition;
	}

	// Token: 0x060054F1 RID: 21745 RVA: 0x001ECAC3 File Offset: 0x001EACC3
	public void SetOn(Component component)
	{
		if (this.position.HasValue)
		{
			component.rectTransform().anchoredPosition = this.position.Value;
			return;
		}
		this.position = component.rectTransform().anchoredPosition;
	}

	// Token: 0x040039BC RID: 14780
	private Option<Vector2> position;
}
