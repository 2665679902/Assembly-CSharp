using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000B84 RID: 2948
public class SelectablePanel : MonoBehaviour, IDeselectHandler, IEventSystemHandler
{
	// Token: 0x06005CAA RID: 23722 RVA: 0x0021E39F File Offset: 0x0021C59F
	public void OnDeselect(BaseEventData evt)
	{
		base.gameObject.SetActive(false);
	}
}
