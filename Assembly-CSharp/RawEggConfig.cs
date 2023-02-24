using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200017E RID: 382
public class RawEggConfig : IEntityConfig
{
	// Token: 0x06000767 RID: 1895 RVA: 0x0002D087 File Offset: 0x0002B287
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x0002D090 File Offset: 0x0002B290
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("RawEgg", ITEMS.FOOD.RAWEGG.NAME, ITEMS.FOOD.RAWEGG.DESC, 1f, false, Assets.GetAnim("rawegg_kanim"), "object", Grid.SceneLayer.Ore, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.RAWEGG);
		TemperatureCookable temperatureCookable = gameObject.AddOrGet<TemperatureCookable>();
		temperatureCookable.cookTemperature = 344.15f;
		temperatureCookable.cookedID = "CookedEgg";
		return gameObject;
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x0002D111 File Offset: 0x0002B311
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x0002D113 File Offset: 0x0002B313
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004E1 RID: 1249
	public const string ID = "RawEgg";
}
