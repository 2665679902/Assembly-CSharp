using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200016C RID: 364
public class CookedMeatConfig : IEntityConfig
{
	// Token: 0x06000708 RID: 1800 RVA: 0x0002C4F0 File Offset: 0x0002A6F0
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x0002C4F8 File Offset: 0x0002A6F8
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("CookedMeat", ITEMS.FOOD.COOKEDMEAT.NAME, ITEMS.FOOD.COOKEDMEAT.DESC, 1f, false, Assets.GetAnim("barbeque_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.COOKED_MEAT);
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x0002C55C File Offset: 0x0002A75C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x0002C55E File Offset: 0x0002A75E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004C2 RID: 1218
	public const string ID = "CookedMeat";

	// Token: 0x040004C3 RID: 1219
	public static ComplexRecipe recipe;
}
