using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000A8E RID: 2702
[AddComponentMenu("KMonoBehaviour/scripts/ScalerMask")]
public class ScalerMask : KMonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x17000629 RID: 1577
	// (get) Token: 0x060052B1 RID: 21169 RVA: 0x001DE606 File Offset: 0x001DC806
	private RectTransform ThisTransform
	{
		get
		{
			if (this._thisTransform == null)
			{
				this._thisTransform = base.GetComponent<RectTransform>();
			}
			return this._thisTransform;
		}
	}

	// Token: 0x1700062A RID: 1578
	// (get) Token: 0x060052B2 RID: 21170 RVA: 0x001DE628 File Offset: 0x001DC828
	private LayoutElement ThisLayoutElement
	{
		get
		{
			if (this._thisLayoutElement == null)
			{
				this._thisLayoutElement = base.GetComponent<LayoutElement>();
			}
			return this._thisLayoutElement;
		}
	}

	// Token: 0x060052B3 RID: 21171 RVA: 0x001DE64C File Offset: 0x001DC84C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		DetailsScreen componentInParent = base.GetComponentInParent<DetailsScreen>();
		if (componentInParent)
		{
			DetailsScreen detailsScreen = componentInParent;
			detailsScreen.pointerEnterActions = (KScreen.PointerEnterActions)Delegate.Combine(detailsScreen.pointerEnterActions, new KScreen.PointerEnterActions(this.OnPointerEnterGrandparent));
			DetailsScreen detailsScreen2 = componentInParent;
			detailsScreen2.pointerExitActions = (KScreen.PointerExitActions)Delegate.Combine(detailsScreen2.pointerExitActions, new KScreen.PointerExitActions(this.OnPointerExitGrandparent));
		}
	}

	// Token: 0x060052B4 RID: 21172 RVA: 0x001DE6B4 File Offset: 0x001DC8B4
	protected override void OnCleanUp()
	{
		DetailsScreen componentInParent = base.GetComponentInParent<DetailsScreen>();
		if (componentInParent)
		{
			DetailsScreen detailsScreen = componentInParent;
			detailsScreen.pointerEnterActions = (KScreen.PointerEnterActions)Delegate.Remove(detailsScreen.pointerEnterActions, new KScreen.PointerEnterActions(this.OnPointerEnterGrandparent));
			DetailsScreen detailsScreen2 = componentInParent;
			detailsScreen2.pointerExitActions = (KScreen.PointerExitActions)Delegate.Remove(detailsScreen2.pointerExitActions, new KScreen.PointerExitActions(this.OnPointerExitGrandparent));
		}
		base.OnCleanUp();
	}

	// Token: 0x060052B5 RID: 21173 RVA: 0x001DE71C File Offset: 0x001DC91C
	private void Update()
	{
		if (this.SourceTransform != null)
		{
			this.SourceTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.ThisTransform.rect.width);
		}
		if (this.SourceTransform != null && (!this.hoverLock || !this.grandparentIsHovered || this.isHovered || this.queuedSizeUpdate))
		{
			this.ThisLayoutElement.minHeight = this.SourceTransform.rect.height + this.topPadding + this.bottomPadding;
			this.SourceTransform.anchoredPosition = new Vector2(0f, -this.topPadding);
			this.queuedSizeUpdate = false;
		}
		if (this.hoverIndicator != null)
		{
			if (this.SourceTransform != null && this.SourceTransform.rect.height > this.ThisTransform.rect.height)
			{
				this.hoverIndicator.SetActive(true);
				return;
			}
			this.hoverIndicator.SetActive(false);
		}
	}

	// Token: 0x060052B6 RID: 21174 RVA: 0x001DE830 File Offset: 0x001DCA30
	public void UpdateSize()
	{
		this.queuedSizeUpdate = true;
	}

	// Token: 0x060052B7 RID: 21175 RVA: 0x001DE839 File Offset: 0x001DCA39
	public void OnPointerEnterGrandparent(PointerEventData eventData)
	{
		this.grandparentIsHovered = true;
	}

	// Token: 0x060052B8 RID: 21176 RVA: 0x001DE842 File Offset: 0x001DCA42
	public void OnPointerExitGrandparent(PointerEventData eventData)
	{
		this.grandparentIsHovered = false;
	}

	// Token: 0x060052B9 RID: 21177 RVA: 0x001DE84B File Offset: 0x001DCA4B
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.isHovered = true;
	}

	// Token: 0x060052BA RID: 21178 RVA: 0x001DE854 File Offset: 0x001DCA54
	public void OnPointerExit(PointerEventData eventData)
	{
		this.isHovered = false;
	}

	// Token: 0x040037F0 RID: 14320
	public RectTransform SourceTransform;

	// Token: 0x040037F1 RID: 14321
	private RectTransform _thisTransform;

	// Token: 0x040037F2 RID: 14322
	private LayoutElement _thisLayoutElement;

	// Token: 0x040037F3 RID: 14323
	public GameObject hoverIndicator;

	// Token: 0x040037F4 RID: 14324
	public bool hoverLock;

	// Token: 0x040037F5 RID: 14325
	private bool grandparentIsHovered;

	// Token: 0x040037F6 RID: 14326
	private bool isHovered;

	// Token: 0x040037F7 RID: 14327
	private bool queuedSizeUpdate = true;

	// Token: 0x040037F8 RID: 14328
	public float topPadding;

	// Token: 0x040037F9 RID: 14329
	public float bottomPadding;
}
