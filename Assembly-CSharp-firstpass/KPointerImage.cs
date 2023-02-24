using System;
using UnityEngine.EventSystems;

// Token: 0x02000060 RID: 96
public class KPointerImage : KImage, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	// Token: 0x14000014 RID: 20
	// (add) Token: 0x060003B9 RID: 953 RVA: 0x0001327C File Offset: 0x0001147C
	// (remove) Token: 0x060003BA RID: 954 RVA: 0x000132B4 File Offset: 0x000114B4
	public event System.Action onPointerEnter;

	// Token: 0x14000015 RID: 21
	// (add) Token: 0x060003BB RID: 955 RVA: 0x000132EC File Offset: 0x000114EC
	// (remove) Token: 0x060003BC RID: 956 RVA: 0x00013324 File Offset: 0x00011524
	public event System.Action onPointerExit;

	// Token: 0x14000016 RID: 22
	// (add) Token: 0x060003BD RID: 957 RVA: 0x0001335C File Offset: 0x0001155C
	// (remove) Token: 0x060003BE RID: 958 RVA: 0x00013394 File Offset: 0x00011594
	public event System.Action onPointerDown;

	// Token: 0x14000017 RID: 23
	// (add) Token: 0x060003BF RID: 959 RVA: 0x000133CC File Offset: 0x000115CC
	// (remove) Token: 0x060003C0 RID: 960 RVA: 0x00013404 File Offset: 0x00011604
	public event System.Action onPointerUp;

	// Token: 0x060003C1 RID: 961 RVA: 0x00013439 File Offset: 0x00011639
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.onPointerEnter != null)
		{
			this.onPointerEnter();
		}
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x0001344E File Offset: 0x0001164E
	public void OnPointerExit(PointerEventData eventData)
	{
		if (this.onPointerExit != null)
		{
			this.onPointerExit();
		}
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x00013463 File Offset: 0x00011663
	public void OnPointerDown(PointerEventData eventData)
	{
		if (this.onPointerDown != null)
		{
			this.onPointerDown();
		}
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x00013478 File Offset: 0x00011678
	public void OnPointerUp(PointerEventData eventData)
	{
		if (this.onPointerUp != null)
		{
			this.onPointerUp();
		}
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x0001348D File Offset: 0x0001168D
	public void ClearPointerEvents()
	{
		this.onPointerEnter = null;
		this.onPointerExit = null;
		this.onPointerDown = null;
		this.onPointerUp = null;
	}
}
