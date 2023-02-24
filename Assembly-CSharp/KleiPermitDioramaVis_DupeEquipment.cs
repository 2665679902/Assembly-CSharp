using System;
using Database;
using UnityEngine;

// Token: 0x02000AE1 RID: 2785
public class KleiPermitDioramaVis_DupeEquipment : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x06005540 RID: 21824 RVA: 0x001EDA36 File Offset: 0x001EBC36
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06005541 RID: 21825 RVA: 0x001EDA3E File Offset: 0x001EBC3E
	public void ConfigureSetup()
	{
	}

	// Token: 0x06005542 RID: 21826 RVA: 0x001EDA40 File Offset: 0x001EBC40
	public void ConfigureWith(PermitResource permit)
	{
		ClothingItemResource clothingItemResource = permit as ClothingItemResource;
		if (clothingItemResource != null)
		{
			this.uiMannequin.SetOutfit(new ClothingItemResource[] { clothingItemResource });
			this.uiMannequin.ReactToClothingItemChange(clothingItemResource.Category);
		}
	}

	// Token: 0x040039EA RID: 14826
	[SerializeField]
	private UIMannequin uiMannequin;
}
