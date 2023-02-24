using System;
using UnityEngine;

// Token: 0x02000362 RID: 866
public static class PresUtil
{
	// Token: 0x06001192 RID: 4498 RVA: 0x0005DA68 File Offset: 0x0005BC68
	public static Promise MoveAndFade(RectTransform rect, Vector2 targetAnchoredPosition, float targetAlpha, float duration, Easing.EasingFn easing = null)
	{
		CanvasGroup canvasGroup = rect.FindOrAddComponent<CanvasGroup>();
		return rect.FindOrAddComponent<CoroutineRunner>().Run(Updater.Parallel(new Updater[]
		{
			Updater.Ease(delegate(float f)
			{
				canvasGroup.alpha = f;
			}, canvasGroup.alpha, targetAlpha, duration, easing),
			Updater.Ease(delegate(Vector2 v2)
			{
				rect.anchoredPosition = v2;
			}, rect.anchoredPosition, targetAnchoredPosition, duration, easing)
		}));
	}

	// Token: 0x06001193 RID: 4499 RVA: 0x0005DB04 File Offset: 0x0005BD04
	public static Promise OffsetFromAndFade(RectTransform rect, Vector2 offset, float targetAlpha, float duration, Easing.EasingFn easing = null)
	{
		Vector2 anchoredPosition = rect.anchoredPosition;
		return PresUtil.MoveAndFade(rect, offset + anchoredPosition, targetAlpha, duration, easing);
	}

	// Token: 0x06001194 RID: 4500 RVA: 0x0005DB2C File Offset: 0x0005BD2C
	public static Promise OffsetToAndFade(RectTransform rect, Vector2 offset, float targetAlpha, float duration, Easing.EasingFn easing = null)
	{
		Vector2 anchoredPosition = rect.anchoredPosition;
		rect.anchoredPosition = offset + anchoredPosition;
		return PresUtil.MoveAndFade(rect, anchoredPosition, targetAlpha, duration, easing);
	}
}
