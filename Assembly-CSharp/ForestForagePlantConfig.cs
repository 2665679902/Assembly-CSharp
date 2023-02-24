using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000142 RID: 322
public class ForestForagePlantConfig : IEntityConfig
{
	// Token: 0x0600062F RID: 1583 RVA: 0x0002804A File Offset: 0x0002624A
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x00028054 File Offset: 0x00026254
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("ForestForagePlant", ITEMS.FOOD.FORESTFORAGEPLANT.NAME, ITEMS.FOOD.FORESTFORAGEPLANT.DESC, 1f, false, Assets.GetAnim("podmelon_fruit_kanim"), "object", Grid.SceneLayer.BuildingBack, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.FORESTFORAGEPLANT);
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x000280B8 File Offset: 0x000262B8
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x000280BA File Offset: 0x000262BA
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000438 RID: 1080
	public const string ID = "ForestForagePlant";
}
