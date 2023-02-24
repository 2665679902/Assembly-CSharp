using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200016B RID: 363
public class CookedFishConfig : IEntityConfig
{
	// Token: 0x06000703 RID: 1795 RVA: 0x0002C478 File Offset: 0x0002A678
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x0002C480 File Offset: 0x0002A680
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("CookedFish", ITEMS.FOOD.COOKEDFISH.NAME, ITEMS.FOOD.COOKEDFISH.DESC, 1f, false, Assets.GetAnim("grilled_pacu_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.COOKED_FISH);
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x0002C4E4 File Offset: 0x0002A6E4
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x0002C4E6 File Offset: 0x0002A6E6
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004C0 RID: 1216
	public const string ID = "CookedFish";

	// Token: 0x040004C1 RID: 1217
	public static ComplexRecipe recipe;
}
