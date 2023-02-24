using System;
using Database;
using UnityEngine;

// Token: 0x02000ADC RID: 2780
public class KleiPermitDioramaVis_BuildingHangingHook : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x0600552B RID: 21803 RVA: 0x001ED6E8 File Offset: 0x001EB8E8
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x0600552C RID: 21804 RVA: 0x001ED6F0 File Offset: 0x001EB8F0
	public void ConfigureSetup()
	{
	}

	// Token: 0x0600552D RID: 21805 RVA: 0x001ED6F4 File Offset: 0x001EB8F4
	public void ConfigureWith(PermitResource permit)
	{
		KleiPermitVisUtil.ConfigureToRenderBuilding(this.buildingKAnim, (BuildingFacadeResource)permit);
		KleiPermitVisUtil.ConfigureBuildingPosition(this.buildingKAnim.rectTransform(), this.buildingKAnimPosition, KleiPermitVisUtil.GetBuildingDef(permit).Unwrap(), Alignment.Top());
	}

	// Token: 0x040039DE RID: 14814
	[SerializeField]
	private KBatchedAnimController buildingKAnim;

	// Token: 0x040039DF RID: 14815
	private PrefabDefinedUIPosition buildingKAnimPosition = new PrefabDefinedUIPosition();
}
