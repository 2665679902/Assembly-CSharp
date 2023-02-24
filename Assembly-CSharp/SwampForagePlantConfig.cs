using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000157 RID: 343
public class SwampForagePlantConfig : IEntityConfig
{
	// Token: 0x0600069E RID: 1694 RVA: 0x0002AD24 File Offset: 0x00028F24
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x0002AD2C File Offset: 0x00028F2C
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("SwampForagePlant", ITEMS.FOOD.SWAMPFORAGEPLANT.NAME, ITEMS.FOOD.SWAMPFORAGEPLANT.DESC, 1f, false, Assets.GetAnim("swamptuber_vegetable_kanim"), "object", Grid.SceneLayer.BuildingBack, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.SWAMPFORAGEPLANT);
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x0002AD90 File Offset: 0x00028F90
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x0002AD92 File Offset: 0x00028F92
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000492 RID: 1170
	public const string ID = "SwampForagePlant";
}
