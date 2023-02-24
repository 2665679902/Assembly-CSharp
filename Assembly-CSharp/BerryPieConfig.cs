using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000167 RID: 359
public class BerryPieConfig : IEntityConfig
{
	// Token: 0x060006EF RID: 1775 RVA: 0x0002C296 File Offset: 0x0002A496
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x0002C2A0 File Offset: 0x0002A4A0
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("BerryPie", ITEMS.FOOD.BERRYPIE.NAME, ITEMS.FOOD.BERRYPIE.DESC, 1f, false, Assets.GetAnim("wormwood_berry_pie_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.55f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.BERRY_PIE);
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x0002C304 File Offset: 0x0002A504
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x0002C306 File Offset: 0x0002A506
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004B8 RID: 1208
	public const string ID = "BerryPie";

	// Token: 0x040004B9 RID: 1209
	public static ComplexRecipe recipe;
}
