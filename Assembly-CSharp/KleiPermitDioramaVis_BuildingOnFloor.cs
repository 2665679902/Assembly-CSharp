using System;
using Database;
using UnityEngine;

// Token: 0x02000ADE RID: 2782
public class KleiPermitDioramaVis_BuildingOnFloor : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x06005533 RID: 21811 RVA: 0x001ED8CB File Offset: 0x001EBACB
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06005534 RID: 21812 RVA: 0x001ED8D3 File Offset: 0x001EBAD3
	public void ConfigureSetup()
	{
	}

	// Token: 0x06005535 RID: 21813 RVA: 0x001ED8D8 File Offset: 0x001EBAD8
	public void ConfigureWith(PermitResource permit)
	{
		BuildingFacadeResource buildingFacadeResource = (BuildingFacadeResource)permit;
		KleiPermitVisUtil.ConfigureToRenderBuilding(this.buildingKAnim, buildingFacadeResource);
	}

	// Token: 0x040039E2 RID: 14818
	[SerializeField]
	private KBatchedAnimController buildingKAnim;
}
