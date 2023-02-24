using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000168 RID: 360
public class BurgerConfig : IEntityConfig
{
	// Token: 0x060006F4 RID: 1780 RVA: 0x0002C310 File Offset: 0x0002A510
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x0002C318 File Offset: 0x0002A518
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("Burger", ITEMS.FOOD.BURGER.NAME, ITEMS.FOOD.BURGER.DESC, 1f, false, Assets.GetAnim("frost_burger_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.BURGER);
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x0002C37C File Offset: 0x0002A57C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x0002C37E File Offset: 0x0002A57E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004BA RID: 1210
	public const string ID = "Burger";

	// Token: 0x040004BB RID: 1211
	public static ComplexRecipe recipe;
}
