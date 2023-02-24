using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000166 RID: 358
public class BasicPlantFoodConfig : IEntityConfig
{
	// Token: 0x060006EA RID: 1770 RVA: 0x0002C21B File Offset: 0x0002A41B
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x0002C224 File Offset: 0x0002A424
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("BasicPlantFood", ITEMS.FOOD.BASICPLANTFOOD.NAME, ITEMS.FOOD.BASICPLANTFOOD.DESC, 1f, false, Assets.GetAnim("meallicegrain_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.CIRCLE, 0.25f, 0.25f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.BASICPLANTFOOD);
		return gameObject;
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x0002C28A File Offset: 0x0002A48A
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x0002C28C File Offset: 0x0002A48C
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004B7 RID: 1207
	public const string ID = "BasicPlantFood";
}
