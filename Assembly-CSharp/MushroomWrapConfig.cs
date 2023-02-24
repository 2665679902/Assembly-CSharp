using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000179 RID: 377
public class MushroomWrapConfig : IEntityConfig
{
	// Token: 0x0600074C RID: 1868 RVA: 0x0002CCFF File Offset: 0x0002AEFF
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x0002CD08 File Offset: 0x0002AF08
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("MushroomWrap", ITEMS.FOOD.MUSHROOMWRAP.NAME, ITEMS.FOOD.MUSHROOMWRAP.DESC, 1f, false, Assets.GetAnim("mushroom_wrap_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.5f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.MUSHROOM_WRAP);
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x0002CD6C File Offset: 0x0002AF6C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x0002CD6E File Offset: 0x0002AF6E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004D8 RID: 1240
	public const string ID = "MushroomWrap";

	// Token: 0x040004D9 RID: 1241
	public static ComplexRecipe recipe;
}
