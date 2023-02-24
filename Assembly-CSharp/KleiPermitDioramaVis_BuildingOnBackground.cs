using System;
using Database;
using UnityEngine;

// Token: 0x02000ADD RID: 2781
public class KleiPermitDioramaVis_BuildingOnBackground : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x0600552F RID: 21807 RVA: 0x001ED750 File Offset: 0x001EB950
	public void ConfigureSetup()
	{
		this.buildingKAnimPrefab.gameObject.SetActive(false);
		this.buildingKAnimArray = new KBatchedAnimController[9];
		for (int i = 0; i < this.buildingKAnimArray.Length; i++)
		{
			this.buildingKAnimArray[i] = (KBatchedAnimController)UnityEngine.Object.Instantiate(this.buildingKAnimPrefab, this.buildingKAnimPrefab.transform.parent, false);
		}
		Vector2 anchoredPosition = this.buildingKAnimPrefab.rectTransform().anchoredPosition;
		Vector2 vector = 175f * Vector2.one;
		Vector2 vector2 = anchoredPosition + vector * new Vector2(-1f, 0f);
		int num = 0;
		for (int j = 0; j < 3; j++)
		{
			int k = 0;
			while (k < 3)
			{
				this.buildingKAnimArray[num].rectTransform().anchoredPosition = vector2 + vector * new Vector2((float)j, (float)k);
				this.buildingKAnimArray[num].gameObject.SetActive(true);
				k++;
				num++;
			}
		}
	}

	// Token: 0x06005530 RID: 21808 RVA: 0x001ED854 File Offset: 0x001EBA54
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06005531 RID: 21809 RVA: 0x001ED85C File Offset: 0x001EBA5C
	public void ConfigureWith(PermitResource permit)
	{
		BuildingFacadeResource buildingFacadeResource = (BuildingFacadeResource)permit;
		BuildingDef value = KleiPermitVisUtil.GetBuildingDef(permit).Value;
		DebugUtil.DevAssert(value.WidthInCells == 1, "assert failed", null);
		DebugUtil.DevAssert(value.HeightInCells == 1, "assert failed", null);
		KBatchedAnimController[] array = this.buildingKAnimArray;
		for (int i = 0; i < array.Length; i++)
		{
			KleiPermitVisUtil.ConfigureToRenderBuilding(array[i], buildingFacadeResource);
		}
	}

	// Token: 0x040039E0 RID: 14816
	[SerializeField]
	private KBatchedAnimController buildingKAnimPrefab;

	// Token: 0x040039E1 RID: 14817
	private KBatchedAnimController[] buildingKAnimArray;
}
