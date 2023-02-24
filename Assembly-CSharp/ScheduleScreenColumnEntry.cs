using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000B80 RID: 2944
public class ScheduleScreenColumnEntry : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerDownHandler
{
	// Token: 0x06005C90 RID: 23696 RVA: 0x0021D890 File Offset: 0x0021BA90
	public void OnPointerEnter(PointerEventData event_data)
	{
		this.RunCallbacks();
	}

	// Token: 0x06005C91 RID: 23697 RVA: 0x0021D898 File Offset: 0x0021BA98
	private void RunCallbacks()
	{
		if (Input.GetMouseButton(0) && this.onLeftClick != null)
		{
			this.onLeftClick();
		}
	}

	// Token: 0x06005C92 RID: 23698 RVA: 0x0021D8B5 File Offset: 0x0021BAB5
	public void OnPointerDown(PointerEventData event_data)
	{
		this.RunCallbacks();
	}

	// Token: 0x04003F41 RID: 16193
	public Image image;

	// Token: 0x04003F42 RID: 16194
	public System.Action onLeftClick;
}
