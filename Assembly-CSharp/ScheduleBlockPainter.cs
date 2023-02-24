using System;
using UnityEngine;

// Token: 0x02000B7B RID: 2939
[AddComponentMenu("KMonoBehaviour/scripts/ScheduleBlockPainter")]
public class ScheduleBlockPainter : KMonoBehaviour
{
	// Token: 0x06005C68 RID: 23656 RVA: 0x0021CBD1 File Offset: 0x0021ADD1
	public void Setup(Action<float> blockPaintHandler)
	{
		this.blockPaintHandler = blockPaintHandler;
		this.button.onPointerDown += this.OnPointerDown;
		this.button.onDrag += this.OnDrag;
	}

	// Token: 0x06005C69 RID: 23657 RVA: 0x0021CC08 File Offset: 0x0021AE08
	private void OnPointerDown()
	{
		this.Transmit();
	}

	// Token: 0x06005C6A RID: 23658 RVA: 0x0021CC10 File Offset: 0x0021AE10
	private void OnDrag()
	{
		this.Transmit();
	}

	// Token: 0x06005C6B RID: 23659 RVA: 0x0021CC18 File Offset: 0x0021AE18
	private void Transmit()
	{
		float num = (base.transform.InverseTransformPoint(KInputManager.GetMousePos()).x - this.rectTransform.rect.x) / this.rectTransform.rect.width;
		this.blockPaintHandler(num);
	}

	// Token: 0x04003F23 RID: 16163
	[SerializeField]
	private KButtonDrag button;

	// Token: 0x04003F24 RID: 16164
	private Action<float> blockPaintHandler;

	// Token: 0x04003F25 RID: 16165
	[MyCmpGet]
	private RectTransform rectTransform;
}
