using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000181 RID: 385
public class ShellfishMeatConfig : IEntityConfig
{
	// Token: 0x06000777 RID: 1911 RVA: 0x0002D26C File Offset: 0x0002B46C
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x0002D274 File Offset: 0x0002B474
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("ShellfishMeat", ITEMS.FOOD.SHELLFISHMEAT.NAME, ITEMS.FOOD.SHELLFISHMEAT.DESC, 1f, false, Assets.GetAnim("shellfish_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.SHELLFISH_MEAT);
		return gameObject;
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x0002D2DA File Offset: 0x0002B4DA
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x0002D2DC File Offset: 0x0002B4DC
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004E5 RID: 1253
	public const string ID = "ShellfishMeat";
}
