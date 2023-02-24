using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000173 RID: 371
public class GammaMushConfig : IEntityConfig
{
	// Token: 0x0600072B RID: 1835 RVA: 0x0002C85F File Offset: 0x0002AA5F
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x0002C868 File Offset: 0x0002AA68
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("GammaMush", ITEMS.FOOD.GAMMAMUSH.NAME, ITEMS.FOOD.GAMMAMUSH.DESC, 1f, false, Assets.GetAnim("mushbarfried_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.GAMMAMUSH);
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x0002C8CC File Offset: 0x0002AACC
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x0002C8CE File Offset: 0x0002AACE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004CD RID: 1229
	public const string ID = "GammaMush";

	// Token: 0x040004CE RID: 1230
	public static ComplexRecipe recipe;
}
