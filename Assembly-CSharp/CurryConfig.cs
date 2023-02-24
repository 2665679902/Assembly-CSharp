using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200016D RID: 365
public class CurryConfig : IEntityConfig
{
	// Token: 0x0600070D RID: 1805 RVA: 0x0002C568 File Offset: 0x0002A768
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x0002C570 File Offset: 0x0002A770
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("Curry", ITEMS.FOOD.CURRY.NAME, ITEMS.FOOD.CURRY.DESC, 1f, false, Assets.GetAnim("curried_beans_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.9f, 0.5f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.CURRY);
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x0002C5D4 File Offset: 0x0002A7D4
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x0002C5D6 File Offset: 0x0002A7D6
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004C4 RID: 1220
	public const string ID = "Curry";
}
