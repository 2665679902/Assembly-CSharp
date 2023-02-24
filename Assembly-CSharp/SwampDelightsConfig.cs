using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000186 RID: 390
public class SwampDelightsConfig : IEntityConfig
{
	// Token: 0x06000791 RID: 1937 RVA: 0x0002D514 File Offset: 0x0002B714
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x0002D51C File Offset: 0x0002B71C
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("SwampDelights", ITEMS.FOOD.SWAMPDELIGHTS.NAME, ITEMS.FOOD.SWAMPDELIGHTS.DESC, 1f, false, Assets.GetAnim("swamp_delights_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.7f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.SWAMP_DELIGHTS);
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x0002D580 File Offset: 0x0002B780
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x0002D582 File Offset: 0x0002B782
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004EE RID: 1262
	public const string ID = "SwampDelights";
}
