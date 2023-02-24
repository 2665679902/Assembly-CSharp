using System;
using UnityEngine;

// Token: 0x0200006E RID: 110
public class ShowOptimizedKScreen : KScreen
{
	// Token: 0x0600048A RID: 1162 RVA: 0x000168AC File Offset: 0x00014AAC
	public override void Show(bool show = true)
	{
		this.mouseOver = false;
		foreach (Canvas canvas in base.GetComponentsInChildren<Canvas>(true))
		{
			if (canvas.enabled != show)
			{
				canvas.enabled = show;
			}
		}
		CanvasGroup component = base.GetComponent<CanvasGroup>();
		if (component != null)
		{
			component.interactable = show;
			component.blocksRaycasts = show;
			component.ignoreParentGroups = true;
		}
		this.isHiddenButActive = !show;
		this.OnShow(show);
	}
}
