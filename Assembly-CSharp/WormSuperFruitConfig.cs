using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200018F RID: 399
public class WormSuperFruitConfig : IEntityConfig
{
	// Token: 0x060007BE RID: 1982 RVA: 0x0002D980 File Offset: 0x0002BB80
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x0002D988 File Offset: 0x0002BB88
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("WormSuperFruit", ITEMS.FOOD.WORMSUPERFRUIT.NAME, ITEMS.FOOD.WORMSUPERFRUIT.DESC, 1f, false, Assets.GetAnim("wormwood_super_fruits_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.6f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.WORMSUPERFRUIT);
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x0002D9EC File Offset: 0x0002BBEC
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x0002D9EE File Offset: 0x0002BBEE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004FE RID: 1278
	public const string ID = "WormSuperFruit";
}
