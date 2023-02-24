using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000187 RID: 391
public class SwampFruitConfig : IEntityConfig
{
	// Token: 0x06000796 RID: 1942 RVA: 0x0002D58C File Offset: 0x0002B78C
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x0002D594 File Offset: 0x0002B794
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity(SwampFruitConfig.ID, ITEMS.FOOD.SWAMPFRUIT.NAME, ITEMS.FOOD.SWAMPFRUIT.DESC, 1f, false, Assets.GetAnim("swampcrop_fruit_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 1f, 0.72f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.SWAMPFRUIT);
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x0002D5F8 File Offset: 0x0002B7F8
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x0002D5FA File Offset: 0x0002B7FA
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004EF RID: 1263
	public static string ID = "SwampFruit";
}
