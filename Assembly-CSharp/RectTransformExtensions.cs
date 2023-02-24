using System;
using UnityEngine;

// Token: 0x02000365 RID: 869
public static class RectTransformExtensions
{
	// Token: 0x060011B1 RID: 4529 RVA: 0x0005DE2C File Offset: 0x0005C02C
	public static RectTransform Fill(this RectTransform rectTransform)
	{
		rectTransform.anchorMin = new Vector2(0f, 0f);
		rectTransform.anchorMax = new Vector2(1f, 1f);
		rectTransform.anchoredPosition = new Vector2(0f, 0f);
		rectTransform.sizeDelta = new Vector2(0f, 0f);
		return rectTransform;
	}

	// Token: 0x060011B2 RID: 4530 RVA: 0x0005DE90 File Offset: 0x0005C090
	public static RectTransform Fill(this RectTransform rectTransform, Padding padding)
	{
		rectTransform.anchorMin = new Vector2(0f, 0f);
		rectTransform.anchorMax = new Vector2(1f, 1f);
		rectTransform.anchoredPosition = new Vector2(padding.left, padding.bottom);
		rectTransform.sizeDelta = new Vector2(-padding.right, -padding.top);
		return rectTransform;
	}

	// Token: 0x060011B3 RID: 4531 RVA: 0x0005DEF8 File Offset: 0x0005C0F8
	public static RectTransform Pivot(this RectTransform rectTransform, float x, float y)
	{
		rectTransform.pivot = new Vector2(x, y);
		return rectTransform;
	}

	// Token: 0x060011B4 RID: 4532 RVA: 0x0005DF08 File Offset: 0x0005C108
	public static RectTransform Pivot(this RectTransform rectTransform, Vector2 pivot)
	{
		rectTransform.pivot = pivot;
		return rectTransform;
	}
}
