using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000184 RID: 388
public class SpicyTofuConfig : IEntityConfig
{
	// Token: 0x06000787 RID: 1927 RVA: 0x0002D422 File Offset: 0x0002B622
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x0002D42C File Offset: 0x0002B62C
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("SpicyTofu", ITEMS.FOOD.SPICYTOFU.NAME, ITEMS.FOOD.SPICYTOFU.DESC, 1f, false, Assets.GetAnim("spicey_tofu_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.9f, 0.6f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.SPICY_TOFU);
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x0002D490 File Offset: 0x0002B690
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x0002D492 File Offset: 0x0002B692
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004EA RID: 1258
	public const string ID = "SpicyTofu";

	// Token: 0x040004EB RID: 1259
	public static ComplexRecipe recipe;
}
