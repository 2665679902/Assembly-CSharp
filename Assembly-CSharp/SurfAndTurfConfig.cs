using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000185 RID: 389
public class SurfAndTurfConfig : IEntityConfig
{
	// Token: 0x0600078C RID: 1932 RVA: 0x0002D49C File Offset: 0x0002B69C
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x0002D4A4 File Offset: 0x0002B6A4
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("SurfAndTurf", ITEMS.FOOD.SURFANDTURF.NAME, ITEMS.FOOD.SURFANDTURF.DESC, 1f, false, Assets.GetAnim("surfnturf_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.SURF_AND_TURF);
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x0002D508 File Offset: 0x0002B708
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x0002D50A File Offset: 0x0002B70A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004EC RID: 1260
	public const string ID = "SurfAndTurf";

	// Token: 0x040004ED RID: 1261
	public static ComplexRecipe recipe;
}
