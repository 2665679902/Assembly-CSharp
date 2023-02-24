using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000052 RID: 82
[AddComponentMenu("KMonoBehaviour/Plugins/HoverCallback")]
public class HoverCallback : KMonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06000339 RID: 825 RVA: 0x000113B4 File Offset: 0x0000F5B4
	public void OnPointerEnter(PointerEventData data)
	{
		if (this.OnHover != null)
		{
			this.OnHover(true);
		}
	}

	// Token: 0x0600033A RID: 826 RVA: 0x000113CA File Offset: 0x0000F5CA
	public void OnPointerExit(PointerEventData data)
	{
		if (this.OnHover != null)
		{
			this.OnHover(false);
		}
	}

	// Token: 0x040003EC RID: 1004
	public Action<bool> OnHover;
}
