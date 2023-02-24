using System;
using Database;
using UnityEngine;

// Token: 0x02000AD9 RID: 2777
public class KleiPermitDioramaVis_ArtablePainting : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x0600551F RID: 21791 RVA: 0x001ED592 File Offset: 0x001EB792
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06005520 RID: 21792 RVA: 0x001ED59A File Offset: 0x001EB79A
	public void ConfigureSetup()
	{
		SymbolOverrideControllerUtil.AddToPrefab(this.buildingKAnim.gameObject);
	}

	// Token: 0x06005521 RID: 21793 RVA: 0x001ED5B0 File Offset: 0x001EB7B0
	public void ConfigureWith(PermitResource permit)
	{
		ArtableStage artableStage = (ArtableStage)permit;
		KleiPermitVisUtil.ConfigureToRenderBuilding(this.buildingKAnim, artableStage);
		BuildingDef value = KleiPermitVisUtil.GetBuildingDef(permit).Value;
		this.buildingKAnimPosition.SetOn(this.buildingKAnim);
		this.buildingKAnim.rectTransform().anchoredPosition += new Vector2(0f, -176f * (float)value.HeightInCells / 2f + 176f);
		this.buildingKAnim.rectTransform().localScale = Vector3.one * 0.9f;
	}

	// Token: 0x040039DA RID: 14810
	[SerializeField]
	private KBatchedAnimController buildingKAnim;

	// Token: 0x040039DB RID: 14811
	private PrefabDefinedUIPosition buildingKAnimPosition = new PrefabDefinedUIPosition();
}
