using System;
using Database;
using UnityEngine;

// Token: 0x02000ADA RID: 2778
public class KleiPermitDioramaVis_ArtableSculpture : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x06005523 RID: 21795 RVA: 0x001ED660 File Offset: 0x001EB860
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06005524 RID: 21796 RVA: 0x001ED668 File Offset: 0x001EB868
	public void ConfigureSetup()
	{
		SymbolOverrideControllerUtil.AddToPrefab(this.buildingKAnim.gameObject);
	}

	// Token: 0x06005525 RID: 21797 RVA: 0x001ED67C File Offset: 0x001EB87C
	public void ConfigureWith(PermitResource permit)
	{
		ArtableStage artableStage = (ArtableStage)permit;
		KleiPermitVisUtil.ConfigureToRenderBuilding(this.buildingKAnim, artableStage);
	}

	// Token: 0x040039DC RID: 14812
	[SerializeField]
	private KBatchedAnimController buildingKAnim;
}
