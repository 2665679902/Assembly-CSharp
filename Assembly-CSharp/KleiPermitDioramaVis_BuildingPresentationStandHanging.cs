using System;
using Database;
using UnityEngine;

// Token: 0x02000AE0 RID: 2784
public class KleiPermitDioramaVis_BuildingPresentationStandHanging : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x0600553C RID: 21820 RVA: 0x001ED9D2 File Offset: 0x001EBBD2
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x0600553D RID: 21821 RVA: 0x001ED9DA File Offset: 0x001EBBDA
	public void ConfigureSetup()
	{
	}

	// Token: 0x0600553E RID: 21822 RVA: 0x001ED9DC File Offset: 0x001EBBDC
	public void ConfigureWith(PermitResource permit)
	{
		KleiPermitVisUtil.ConfigureToRenderBuilding(this.buildingKAnim, (BuildingFacadeResource)permit);
		KleiPermitVisUtil.ConfigureBuildingPosition(this.buildingKAnim.rectTransform(), this.buildingKAnimPosition, KleiPermitVisUtil.GetBuildingDef(permit).Unwrap(), Alignment.Top());
	}

	// Token: 0x040039E8 RID: 14824
	[SerializeField]
	private KBatchedAnimController buildingKAnim;

	// Token: 0x040039E9 RID: 14825
	private PrefabDefinedUIPosition buildingKAnimPosition = new PrefabDefinedUIPosition();
}
