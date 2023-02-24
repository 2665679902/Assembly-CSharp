using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200004D RID: 77
public class ButtonLock : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	// Token: 0x0600031F RID: 799 RVA: 0x00010FD3 File Offset: 0x0000F1D3
	public void OnPointerClick(PointerEventData eventData)
	{
		this.target.SendMessage("ToggleLock", SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06000320 RID: 800 RVA: 0x00010FE6 File Offset: 0x0000F1E6
	public void OnDrag(PointerEventData eventData)
	{
		this.target.SendMessage("OnDrag", SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06000321 RID: 801 RVA: 0x00010FF9 File Offset: 0x0000F1F9
	public void OnBeginDrag(PointerEventData eventData)
	{
		this.target.SendMessage("Lock", true, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06000322 RID: 802 RVA: 0x00011012 File Offset: 0x0000F212
	public void OnEndDrag(PointerEventData eventData)
	{
		this.target.SendMessage("Lock", false, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x040003D9 RID: 985
	public GameObject target;
}
