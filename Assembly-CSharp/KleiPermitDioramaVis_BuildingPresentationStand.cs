using System;
using Database;
using UnityEngine;

// Token: 0x02000ADF RID: 2783
public class KleiPermitDioramaVis_BuildingPresentationStand : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x06005537 RID: 21815 RVA: 0x001ED900 File Offset: 0x001EBB00
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06005538 RID: 21816 RVA: 0x001ED908 File Offset: 0x001EBB08
	public void ConfigureSetup()
	{
	}

	// Token: 0x06005539 RID: 21817 RVA: 0x001ED90C File Offset: 0x001EBB0C
	public void ConfigureWith(PermitResource permit)
	{
		BuildingFacadeResource buildingFacadeResource = (BuildingFacadeResource)permit;
		KleiPermitVisUtil.ConfigureToRenderBuilding(this.buildingKAnim, buildingFacadeResource);
		KleiPermitVisUtil.ConfigureBuildingPosition(this.buildingKAnim.rectTransform(), this.anchorPos, KleiPermitVisUtil.GetBuildingDef(permit).Unwrap(), this.lastAlignment);
	}

	// Token: 0x0600553A RID: 21818 RVA: 0x001ED958 File Offset: 0x001EBB58
	public KleiPermitDioramaVis_BuildingPresentationStand WithAlignment(Alignment alignment)
	{
		this.lastAlignment = alignment;
		this.anchorPos = new Vector2(alignment.x.Remap(new ValueTuple<float, float>(0f, 1f), new ValueTuple<float, float>(-160f, 160f)), alignment.y.Remap(new ValueTuple<float, float>(0f, 1f), new ValueTuple<float, float>(-156f, 156f)));
		return this;
	}

	// Token: 0x040039E3 RID: 14819
	[SerializeField]
	private KBatchedAnimController buildingKAnim;

	// Token: 0x040039E4 RID: 14820
	private Alignment lastAlignment;

	// Token: 0x040039E5 RID: 14821
	private Vector2 anchorPos;

	// Token: 0x040039E6 RID: 14822
	public const float LEFT = -160f;

	// Token: 0x040039E7 RID: 14823
	public const float TOP = 156f;
}
