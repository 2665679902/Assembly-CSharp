using System;
using Database;
using UnityEngine;

// Token: 0x02000ADB RID: 2779
public class KleiPermitDioramaVis_ArtableSticker : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x06005527 RID: 21799 RVA: 0x001ED6A4 File Offset: 0x001EB8A4
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06005528 RID: 21800 RVA: 0x001ED6AC File Offset: 0x001EB8AC
	public void ConfigureSetup()
	{
		SymbolOverrideControllerUtil.AddToPrefab(this.buildingKAnim.gameObject);
	}

	// Token: 0x06005529 RID: 21801 RVA: 0x001ED6C0 File Offset: 0x001EB8C0
	public void ConfigureWith(PermitResource permit)
	{
		DbStickerBomb dbStickerBomb = (DbStickerBomb)permit;
		KleiPermitVisUtil.ConfigureToRenderBuilding(this.buildingKAnim, dbStickerBomb);
	}

	// Token: 0x040039DD RID: 14813
	[SerializeField]
	private KBatchedAnimController buildingKAnim;
}
