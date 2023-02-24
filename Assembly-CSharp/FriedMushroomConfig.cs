using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000171 RID: 369
public class FriedMushroomConfig : IEntityConfig
{
	// Token: 0x06000721 RID: 1825 RVA: 0x0002C74C File Offset: 0x0002A94C
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x0002C754 File Offset: 0x0002A954
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("FriedMushroom", ITEMS.FOOD.FRIEDMUSHROOM.NAME, ITEMS.FOOD.FRIEDMUSHROOM.DESC, 1f, false, Assets.GetAnim("funguscapfried_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.6f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.FRIED_MUSHROOM);
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x0002C7B8 File Offset: 0x0002A9B8
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x0002C7BA File Offset: 0x0002A9BA
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004C9 RID: 1225
	public const string ID = "FriedMushroom";

	// Token: 0x040004CA RID: 1226
	public static ComplexRecipe recipe;
}
