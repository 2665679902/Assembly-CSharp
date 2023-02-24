using System;
using UnityEngine.EventSystems;

// Token: 0x02000058 RID: 88
public class KButtonDrag : KButton, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
{
	// Token: 0x1400000F RID: 15
	// (add) Token: 0x06000386 RID: 902 RVA: 0x00012648 File Offset: 0x00010848
	// (remove) Token: 0x06000387 RID: 903 RVA: 0x00012680 File Offset: 0x00010880
	public event System.Action onBeginDrag;

	// Token: 0x14000010 RID: 16
	// (add) Token: 0x06000388 RID: 904 RVA: 0x000126B8 File Offset: 0x000108B8
	// (remove) Token: 0x06000389 RID: 905 RVA: 0x000126F0 File Offset: 0x000108F0
	public event System.Action onDrag;

	// Token: 0x14000011 RID: 17
	// (add) Token: 0x0600038A RID: 906 RVA: 0x00012728 File Offset: 0x00010928
	// (remove) Token: 0x0600038B RID: 907 RVA: 0x00012760 File Offset: 0x00010960
	public event System.Action onEndDrag;

	// Token: 0x0600038C RID: 908 RVA: 0x00012795 File Offset: 0x00010995
	public void ClearOnDragEvents()
	{
		this.onBeginDrag = null;
		this.onDrag = null;
		this.onEndDrag = null;
	}

	// Token: 0x0600038D RID: 909 RVA: 0x000127AC File Offset: 0x000109AC
	public void OnBeginDrag(PointerEventData data)
	{
		this.onBeginDrag.Signal();
	}

	// Token: 0x0600038E RID: 910 RVA: 0x000127B9 File Offset: 0x000109B9
	public void OnDrag(PointerEventData data)
	{
		this.onDrag.Signal();
	}

	// Token: 0x0600038F RID: 911 RVA: 0x000127C6 File Offset: 0x000109C6
	public void OnEndDrag(PointerEventData data)
	{
		this.onEndDrag.Signal();
	}
}
