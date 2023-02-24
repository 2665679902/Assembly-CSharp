using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000061 RID: 97
[ExecuteInEditMode]
[AddComponentMenu("KMonoBehaviour/Plugins/KRectStretcher")]
public class KRectStretcher : KMonoBehaviour
{
	// Token: 0x060003C7 RID: 967 RVA: 0x000134B3 File Offset: 0x000116B3
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.rectTracker = default(DrivenRectTransformTracker);
		this.UpdateStretching();
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x000134CD File Offset: 0x000116CD
	private void Update()
	{
		if (base.transform.parent.hasChanged || (this.OverrideLayoutElement != null && this.OverrideLayoutElement.transform.hasChanged))
		{
			this.UpdateStretching();
		}
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x00013508 File Offset: 0x00011708
	public void UpdateStretching()
	{
		if (this.rect == null)
		{
			this.rect = base.GetComponent<RectTransform>();
		}
		if (this.rect == null)
		{
			return;
		}
		if (base.transform.parent == null && this.OverrideLayoutElement == null)
		{
			return;
		}
		RectTransform rectTransform = base.transform.parent.rectTransform();
		Vector3 vector = Vector3.zero;
		if (this.SizeReferenceMethod == KRectStretcher.ParentSizeReferenceValue.SizeDelta)
		{
			vector = rectTransform.sizeDelta;
		}
		else if (this.SizeReferenceMethod == KRectStretcher.ParentSizeReferenceValue.RectDimensions)
		{
			vector = rectTransform.rect.size;
		}
		Vector2 zero = Vector2.zero;
		if (!this.PreserveAspectRatio)
		{
			zero = new Vector2(this.StretchX ? vector.x : this.rect.sizeDelta.x, this.StretchY ? vector.y : this.rect.sizeDelta.y);
		}
		else
		{
			switch (this.AspectFitOption)
			{
			case KRectStretcher.aspectFitOption.WidthDictatesHeight:
				zero = new Vector2(this.StretchX ? vector.x : this.rect.sizeDelta.x, this.StretchY ? (vector.x / this.aspectRatioToPreserve) : this.rect.sizeDelta.y);
				break;
			case KRectStretcher.aspectFitOption.HeightDictatesWidth:
				zero = new Vector2(this.StretchX ? (vector.y * this.aspectRatioToPreserve) : this.rect.sizeDelta.x, this.StretchY ? vector.y : this.rect.sizeDelta.y);
				break;
			case KRectStretcher.aspectFitOption.EnvelopeParent:
				if (rectTransform.sizeDelta.x / rectTransform.sizeDelta.y > this.aspectRatioToPreserve)
				{
					zero = new Vector2(this.StretchX ? vector.x : this.rect.sizeDelta.x, this.StretchY ? (vector.x / this.aspectRatioToPreserve) : this.rect.sizeDelta.y);
				}
				else
				{
					zero = new Vector2(this.StretchX ? (vector.y * this.aspectRatioToPreserve) : this.rect.sizeDelta.x, this.StretchY ? vector.y : this.rect.sizeDelta.y);
				}
				break;
			}
		}
		if (this.StretchX)
		{
			zero.x *= this.XStretchFactor;
		}
		if (this.StretchY)
		{
			zero.y *= this.YStretchFactor;
		}
		if (this.StretchX)
		{
			zero.x += this.Padding.x;
		}
		if (this.StretchY)
		{
			zero.y += this.Padding.y;
		}
		if (this.rect.sizeDelta != zero)
		{
			if (this.lerpToSize)
			{
				if (this.OverrideLayoutElement != null)
				{
					if (this.StretchX)
					{
						this.OverrideLayoutElement.minWidth = Mathf.Lerp(this.OverrideLayoutElement.minWidth, zero.x, Time.unscaledDeltaTime * this.lerpTime);
					}
					if (this.StretchY)
					{
						this.OverrideLayoutElement.minHeight = Mathf.Lerp(this.OverrideLayoutElement.minHeight, zero.y, Time.unscaledDeltaTime * this.lerpTime);
					}
				}
				else
				{
					this.rect.sizeDelta = Vector2.Lerp(this.rect.sizeDelta, zero, this.lerpTime * Time.unscaledDeltaTime);
				}
			}
			else
			{
				if (this.OverrideLayoutElement != null)
				{
					if (this.StretchX)
					{
						this.OverrideLayoutElement.minWidth = zero.x;
					}
					if (this.StretchY)
					{
						this.OverrideLayoutElement.minHeight = zero.y;
					}
				}
				this.rect.sizeDelta = zero;
			}
		}
		for (int i = 0; i < base.transform.childCount; i++)
		{
			KRectStretcher component = base.transform.GetChild(i).GetComponent<KRectStretcher>();
			if (component)
			{
				component.UpdateStretching();
			}
		}
		this.rectTracker.Clear();
		if (this.StretchX)
		{
			this.rectTracker.Add(this, this.rect, DrivenTransformProperties.SizeDeltaX);
		}
		if (this.StretchY)
		{
			this.rectTracker.Add(this, this.rect, DrivenTransformProperties.SizeDeltaY);
		}
	}

	// Token: 0x04000444 RID: 1092
	private RectTransform rect;

	// Token: 0x04000445 RID: 1093
	private DrivenRectTransformTracker rectTracker;

	// Token: 0x04000446 RID: 1094
	public bool StretchX;

	// Token: 0x04000447 RID: 1095
	public bool StretchY;

	// Token: 0x04000448 RID: 1096
	public float XStretchFactor = 1f;

	// Token: 0x04000449 RID: 1097
	public float YStretchFactor = 1f;

	// Token: 0x0400044A RID: 1098
	public KRectStretcher.ParentSizeReferenceValue SizeReferenceMethod;

	// Token: 0x0400044B RID: 1099
	public Vector2 Padding;

	// Token: 0x0400044C RID: 1100
	public bool lerpToSize;

	// Token: 0x0400044D RID: 1101
	public float lerpTime = 1f;

	// Token: 0x0400044E RID: 1102
	public LayoutElement OverrideLayoutElement;

	// Token: 0x0400044F RID: 1103
	public bool PreserveAspectRatio;

	// Token: 0x04000450 RID: 1104
	public float aspectRatioToPreserve = 1f;

	// Token: 0x04000451 RID: 1105
	public KRectStretcher.aspectFitOption AspectFitOption;

	// Token: 0x020009AF RID: 2479
	public enum ParentSizeReferenceValue
	{
		// Token: 0x0400219E RID: 8606
		SizeDelta,
		// Token: 0x0400219F RID: 8607
		RectDimensions
	}

	// Token: 0x020009B0 RID: 2480
	public enum aspectFitOption
	{
		// Token: 0x040021A1 RID: 8609
		WidthDictatesHeight,
		// Token: 0x040021A2 RID: 8610
		HeightDictatesWidth,
		// Token: 0x040021A3 RID: 8611
		EnvelopeParent
	}
}
