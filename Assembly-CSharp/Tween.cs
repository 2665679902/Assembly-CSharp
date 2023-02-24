using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000C13 RID: 3091
public class Tween : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x060061F4 RID: 25076 RVA: 0x0024323B File Offset: 0x0024143B
	private void Awake()
	{
		this.Selectable = base.GetComponent<Selectable>();
	}

	// Token: 0x060061F5 RID: 25077 RVA: 0x00243249 File Offset: 0x00241449
	public void OnPointerEnter(PointerEventData data)
	{
		this.Direction = 1f;
	}

	// Token: 0x060061F6 RID: 25078 RVA: 0x00243256 File Offset: 0x00241456
	public void OnPointerExit(PointerEventData data)
	{
		this.Direction = -1f;
	}

	// Token: 0x060061F7 RID: 25079 RVA: 0x00243264 File Offset: 0x00241464
	private void Update()
	{
		if (this.Selectable.interactable)
		{
			float x = base.transform.localScale.x;
			float num = x + this.Direction * Time.unscaledDeltaTime * Tween.ScaleSpeed;
			num = Mathf.Min(num, Tween.Scale);
			num = Mathf.Max(num, 1f);
			if (num != x)
			{
				base.transform.localScale = new Vector3(num, num, 1f);
			}
		}
	}

	// Token: 0x040043BE RID: 17342
	private static float Scale = 1.025f;

	// Token: 0x040043BF RID: 17343
	private static float ScaleSpeed = 0.5f;

	// Token: 0x040043C0 RID: 17344
	private Selectable Selectable;

	// Token: 0x040043C1 RID: 17345
	private float Direction = -1f;
}
