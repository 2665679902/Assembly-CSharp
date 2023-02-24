using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200018D RID: 397
public class WormBasicFruitConfig : IEntityConfig
{
	// Token: 0x060007B4 RID: 1972 RVA: 0x0002D890 File Offset: 0x0002BA90
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x0002D898 File Offset: 0x0002BA98
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("WormBasicFruit", ITEMS.FOOD.WORMBASICFRUIT.NAME, ITEMS.FOOD.WORMBASICFRUIT.DESC, 1f, false, Assets.GetAnim("wormwood_basic_fruit_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.7f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.WORMBASICFRUIT);
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x0002D8FC File Offset: 0x0002BAFC
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x0002D8FE File Offset: 0x0002BAFE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004FB RID: 1275
	public const string ID = "WormBasicFruit";
}
