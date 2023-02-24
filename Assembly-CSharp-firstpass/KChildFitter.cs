using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000059 RID: 89
public class KChildFitter : MonoBehaviour
{
	// Token: 0x06000391 RID: 913 RVA: 0x000127DC File Offset: 0x000109DC
	private void Awake()
	{
		this.rect_transform = base.GetComponent<RectTransform>();
		this.VLG = base.GetComponent<VerticalLayoutGroup>();
		this.HLG = base.GetComponent<HorizontalLayoutGroup>();
		this.GLG = base.GetComponent<GridLayoutGroup>();
		if (this.overrideLayoutElement == null)
		{
			this.overrideLayoutElement = base.GetComponent<LayoutElement>();
		}
	}

	// Token: 0x06000392 RID: 914 RVA: 0x00012833 File Offset: 0x00010A33
	private void LateUpdate()
	{
		this.FitSize();
	}

	// Token: 0x06000393 RID: 915 RVA: 0x0001283C File Offset: 0x00010A3C
	public Vector2 GetPositionRelativeToTopLeftPivot(RectTransform element)
	{
		Vector2 zero = Vector2.zero;
		zero.x = element.anchoredPosition.x - element.sizeDelta.x * element.pivot.x;
		zero.y = element.anchoredPosition.y + element.sizeDelta.y * (1f - element.pivot.y);
		return zero;
	}

	// Token: 0x06000394 RID: 916 RVA: 0x000128AC File Offset: 0x00010AAC
	public void FitSize()
	{
		if (!this.fitWidth && !this.fitHeight)
		{
			return;
		}
		Vector2 sizeDelta = this.rect_transform.sizeDelta;
		if (this.fitWidth)
		{
			sizeDelta.x = 0f;
		}
		if (this.fitHeight)
		{
			sizeDelta.y = 0f;
		}
		float num = float.NegativeInfinity;
		float num2 = float.PositiveInfinity;
		float num3 = float.PositiveInfinity;
		float num4 = float.NegativeInfinity;
		int childCount = base.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			LayoutElement component = child.gameObject.GetComponent<LayoutElement>();
			if ((component == null || !component.ignoreLayout) && child.gameObject.activeSelf)
			{
				RectTransform rectTransform = child as RectTransform;
				if (this.fitWidth)
				{
					if (this.findTotalBounds)
					{
						float num5 = this.GetPositionRelativeToTopLeftPivot(rectTransform).x + rectTransform.sizeDelta.x;
						if (num5 > num4)
						{
							num4 = num5;
						}
						float x = this.GetPositionRelativeToTopLeftPivot(rectTransform).x;
						if (x < num3)
						{
							num3 = x;
						}
						sizeDelta.x = Mathf.Abs(num4 - num3);
						if (this.includeLayoutGroupPadding)
						{
							sizeDelta.x += (float)((this.VLG != null) ? (this.VLG.padding.left + this.VLG.padding.right) : 0);
							sizeDelta.x += (float)((this.HLG != null) ? (this.HLG.padding.left + this.HLG.padding.right) : 0);
							sizeDelta.x += (float)((this.GLG != null) ? (this.GLG.padding.left + this.GLG.padding.right) : 0);
						}
					}
					else
					{
						sizeDelta.x += rectTransform.sizeDelta.x;
						if (this.HLG)
						{
							sizeDelta.x += this.HLG.spacing;
						}
					}
				}
				if (this.fitHeight)
				{
					if (this.findTotalBounds)
					{
						if (this.GetPositionRelativeToTopLeftPivot(rectTransform).y > num)
						{
							num = this.GetPositionRelativeToTopLeftPivot(rectTransform).y;
						}
						if (this.GetPositionRelativeToTopLeftPivot(rectTransform).y - rectTransform.sizeDelta.y < num2)
						{
							num2 = this.GetPositionRelativeToTopLeftPivot(rectTransform).y - rectTransform.sizeDelta.y;
						}
						sizeDelta.y = Mathf.Abs(num - num2);
						if (this.includeLayoutGroupPadding)
						{
							sizeDelta.y += (float)((this.VLG != null) ? (this.VLG.padding.bottom + this.VLG.padding.top) : 0);
							sizeDelta.y += (float)((this.HLG != null) ? (this.HLG.padding.bottom + this.HLG.padding.top) : 0);
							sizeDelta.y += (float)((this.GLG != null) ? (this.GLG.padding.bottom + this.GLG.padding.top) : 0);
						}
					}
					else
					{
						sizeDelta.y += rectTransform.sizeDelta.y;
						if (this.VLG)
						{
							sizeDelta.y += this.VLG.spacing;
						}
					}
				}
			}
		}
		Vector2 vector = new Vector2(this.WidthPadding, this.HeightPadding);
		if (!this.fitWidth)
		{
			this.WidthPadding = 0f;
		}
		if (!this.fitHeight)
		{
			this.HeightPadding = 0f;
		}
		if (this.overrideLayoutElement != null)
		{
			if (this.fitWidth && this.overrideLayoutElement.minWidth != (sizeDelta.x + vector.x) * this.WidthScale)
			{
				this.overrideLayoutElement.minWidth = (sizeDelta.x + vector.x) * this.WidthScale;
			}
			if (this.fitHeight && this.overrideLayoutElement.minHeight != (sizeDelta.y + vector.y) * this.HeightScale)
			{
				this.overrideLayoutElement.minHeight = (sizeDelta.y + vector.y) * this.HeightScale;
			}
		}
		Vector2 vector2 = new Vector2(this.WidthScale * (sizeDelta.x + vector.x), this.HeightScale * (sizeDelta.y + vector.y));
		if (this.rect_transform.sizeDelta != vector2)
		{
			this.rect_transform.sizeDelta = vector2;
			if (base.transform.parent != null)
			{
				KChildFitter component2 = base.transform.parent.GetComponent<KChildFitter>();
				if (component2 != null)
				{
					component2.FitSize();
				}
			}
		}
	}

	// Token: 0x04000423 RID: 1059
	public bool fitWidth;

	// Token: 0x04000424 RID: 1060
	public bool fitHeight;

	// Token: 0x04000425 RID: 1061
	public float HeightPadding;

	// Token: 0x04000426 RID: 1062
	public float WidthPadding;

	// Token: 0x04000427 RID: 1063
	public float WidthScale = 1f;

	// Token: 0x04000428 RID: 1064
	public float HeightScale = 1f;

	// Token: 0x04000429 RID: 1065
	public LayoutElement overrideLayoutElement;

	// Token: 0x0400042A RID: 1066
	private RectTransform rect_transform;

	// Token: 0x0400042B RID: 1067
	private VerticalLayoutGroup VLG;

	// Token: 0x0400042C RID: 1068
	private HorizontalLayoutGroup HLG;

	// Token: 0x0400042D RID: 1069
	private GridLayoutGroup GLG;

	// Token: 0x0400042E RID: 1070
	public bool findTotalBounds = true;

	// Token: 0x0400042F RID: 1071
	public bool includeLayoutGroupPadding = true;
}
