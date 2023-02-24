using System;
using UnityEngine;

// Token: 0x02000C0E RID: 3086
[AddComponentMenu("KMonoBehaviour/scripts/TimeOfDayPositioner")]
public class TimeOfDayPositioner : KMonoBehaviour
{
	// Token: 0x060061D3 RID: 25043 RVA: 0x002428F8 File Offset: 0x00240AF8
	private void Update()
	{
		float num = GameClock.Instance.GetCurrentCycleAsPercentage() * this.targetRect.rect.width;
		(base.transform as RectTransform).anchoredPosition = this.targetRect.anchoredPosition + new Vector2(Mathf.Round(num), 0f);
	}

	// Token: 0x040043A2 RID: 17314
	[SerializeField]
	private RectTransform targetRect;
}
