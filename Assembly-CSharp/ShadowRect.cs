using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B87 RID: 2951
public class ShadowRect : MonoBehaviour
{
	// Token: 0x06005CB4 RID: 23732 RVA: 0x0021E56C File Offset: 0x0021C76C
	private void OnEnable()
	{
		if (this.RectShadow != null)
		{
			this.RectShadow.name = "Shadow_" + this.RectMain.name;
			this.MatchRect();
			return;
		}
		global::Debug.LogWarning("Shadowrect is missing rectshadow: " + base.gameObject.name);
	}

	// Token: 0x06005CB5 RID: 23733 RVA: 0x0021E5C8 File Offset: 0x0021C7C8
	private void Update()
	{
		this.MatchRect();
	}

	// Token: 0x06005CB6 RID: 23734 RVA: 0x0021E5D0 File Offset: 0x0021C7D0
	protected virtual void MatchRect()
	{
		if (this.RectShadow == null || this.RectMain == null)
		{
			return;
		}
		if (this.shadowLayoutElement == null)
		{
			this.shadowLayoutElement = this.RectShadow.GetComponent<LayoutElement>();
		}
		if (this.shadowLayoutElement != null && !this.shadowLayoutElement.ignoreLayout)
		{
			this.shadowLayoutElement.ignoreLayout = true;
		}
		if (this.RectShadow.transform.parent != this.RectMain.transform.parent)
		{
			this.RectShadow.transform.SetParent(this.RectMain.transform.parent);
		}
		if (this.RectShadow.GetSiblingIndex() >= this.RectMain.GetSiblingIndex())
		{
			this.RectShadow.SetAsFirstSibling();
		}
		this.RectShadow.transform.localScale = Vector3.one;
		if (this.RectShadow.pivot != this.RectMain.pivot)
		{
			this.RectShadow.pivot = this.RectMain.pivot;
		}
		if (this.RectShadow.anchorMax != this.RectMain.anchorMax)
		{
			this.RectShadow.anchorMax = this.RectMain.anchorMax;
		}
		if (this.RectShadow.anchorMin != this.RectMain.anchorMin)
		{
			this.RectShadow.anchorMin = this.RectMain.anchorMin;
		}
		if (this.RectShadow.sizeDelta != this.RectMain.sizeDelta)
		{
			this.RectShadow.sizeDelta = this.RectMain.sizeDelta;
		}
		if (this.RectShadow.anchoredPosition != this.RectMain.anchoredPosition + this.ShadowOffset)
		{
			this.RectShadow.anchoredPosition = this.RectMain.anchoredPosition + this.ShadowOffset;
		}
		if (this.RectMain.gameObject.activeInHierarchy != this.RectShadow.gameObject.activeInHierarchy)
		{
			this.RectShadow.gameObject.SetActive(this.RectMain.gameObject.activeInHierarchy);
		}
	}

	// Token: 0x04003F5F RID: 16223
	public RectTransform RectMain;

	// Token: 0x04003F60 RID: 16224
	public RectTransform RectShadow;

	// Token: 0x04003F61 RID: 16225
	[SerializeField]
	protected Color shadowColor = new Color(0f, 0f, 0f, 0.6f);

	// Token: 0x04003F62 RID: 16226
	[SerializeField]
	protected Vector2 ShadowOffset = new Vector2(1.5f, -1.5f);

	// Token: 0x04003F63 RID: 16227
	private LayoutElement shadowLayoutElement;
}
