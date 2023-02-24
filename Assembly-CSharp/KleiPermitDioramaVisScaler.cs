using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000AD8 RID: 2776
[ExecuteAlways]
public class KleiPermitDioramaVisScaler : UIBehaviour
{
	// Token: 0x0600551B RID: 21787 RVA: 0x001ED487 File Offset: 0x001EB687
	protected override void OnRectTransformDimensionsChange()
	{
		this.Layout();
	}

	// Token: 0x0600551C RID: 21788 RVA: 0x001ED48F File Offset: 0x001EB68F
	public void Layout()
	{
		KleiPermitDioramaVisScaler.Layout(this.root, this.scaleTarget, this.slot);
	}

	// Token: 0x0600551D RID: 21789 RVA: 0x001ED4A8 File Offset: 0x001EB6A8
	public static void Layout(RectTransform root, RectTransform scaleTarget, RectTransform slot)
	{
		float num = 2.125f;
		AspectRatioFitter aspectRatioFitter = slot.FindOrAddComponent<AspectRatioFitter>();
		aspectRatioFitter.aspectRatio = num;
		aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
		float num2 = 1700f;
		float num3 = Mathf.Max(0.1f, root.rect.width) / num2;
		float num4 = 800f;
		float num5 = Mathf.Max(0.1f, root.rect.height) / num4;
		float num6 = Mathf.Max(num3, num5);
		scaleTarget.localScale = Vector3.one * num6;
		scaleTarget.sizeDelta = new Vector2(1700f, 800f);
		scaleTarget.anchorMin = Vector2.one * 0.5f;
		scaleTarget.anchorMax = Vector2.one * 0.5f;
		scaleTarget.pivot = Vector2.one * 0.5f;
		scaleTarget.anchoredPosition = Vector2.zero;
	}

	// Token: 0x040039D5 RID: 14805
	public const float REFERENCE_WIDTH = 1700f;

	// Token: 0x040039D6 RID: 14806
	public const float REFERENCE_HEIGHT = 800f;

	// Token: 0x040039D7 RID: 14807
	[SerializeField]
	private RectTransform root;

	// Token: 0x040039D8 RID: 14808
	[SerializeField]
	private RectTransform scaleTarget;

	// Token: 0x040039D9 RID: 14809
	[SerializeField]
	private RectTransform slot;
}
