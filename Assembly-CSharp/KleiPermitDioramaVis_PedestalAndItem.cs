using System;
using Database;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AE4 RID: 2788
public class KleiPermitDioramaVis_PedestalAndItem : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x06005552 RID: 21842 RVA: 0x001EDD7E File Offset: 0x001EBF7E
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06005553 RID: 21843 RVA: 0x001EDD86 File Offset: 0x001EBF86
	public void ConfigureSetup()
	{
	}

	// Token: 0x06005554 RID: 21844 RVA: 0x001EDD88 File Offset: 0x001EBF88
	public void ConfigureWith(PermitResource permit)
	{
		PermitPresentationInfo permitPresentationInfo = permit.GetPermitPresentationInfo();
		RectTransform rectTransform = this.pedestalKAnim.rectTransform();
		RectTransform rectTransform2 = this.itemSprite.rectTransform();
		rectTransform.pivot = new Vector2(0.5f, 0f);
		KleiPermitVisUtil.ConfigureToRenderBuilding(this.pedestalKAnim, Assets.GetBuildingDef("ItemPedestal"));
		rectTransform2.pivot = new Vector2(0.5f, 0f);
		rectTransform2.anchoredPosition = rectTransform.anchoredPosition + Vector2.up * 0.79f * 176f;
		rectTransform2.sizeDelta = Vector2.one * 176f;
		this.itemSprite.sprite = permitPresentationInfo.sprite;
	}

	// Token: 0x040039FA RID: 14842
	[SerializeField]
	private KBatchedAnimController pedestalKAnim;

	// Token: 0x040039FB RID: 14843
	[SerializeField]
	private Image itemSprite;

	// Token: 0x040039FC RID: 14844
	private const float TILE_COUNT_TO_PEDESTAL_SLOT = 0.79f;
}
