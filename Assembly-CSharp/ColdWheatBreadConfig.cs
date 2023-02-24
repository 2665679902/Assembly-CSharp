using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000169 RID: 361
public class ColdWheatBreadConfig : IEntityConfig
{
	// Token: 0x060006F9 RID: 1785 RVA: 0x0002C388 File Offset: 0x0002A588
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x0002C390 File Offset: 0x0002A590
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("ColdWheatBread", ITEMS.FOOD.COLDWHEATBREAD.NAME, ITEMS.FOOD.COLDWHEATBREAD.DESC, 1f, false, Assets.GetAnim("frostbread_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.COLD_WHEAT_BREAD);
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x0002C3F4 File Offset: 0x0002A5F4
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x0002C3F6 File Offset: 0x0002A5F6
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004BC RID: 1212
	public const string ID = "ColdWheatBread";

	// Token: 0x040004BD RID: 1213
	public static ComplexRecipe recipe;
}
