using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000183 RID: 387
public class SpiceNutConfig : IEntityConfig
{
	// Token: 0x06000781 RID: 1921 RVA: 0x0002D360 File Offset: 0x0002B560
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x0002D368 File Offset: 0x0002B568
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity(SpiceNutConfig.ID, ITEMS.FOOD.SPICENUT.NAME, ITEMS.FOOD.SPICENUT.DESC, 1f, false, Assets.GetAnim("spicenut_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.SPICENUT);
		SoundEventVolumeCache.instance.AddVolume("vinespicenut_kanim", "VineSpiceNut_grow", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("vinespicenut_kanim", "VineSpiceNut_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		return gameObject;
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x0002D400 File Offset: 0x0002B600
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x0002D402 File Offset: 0x0002B602
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004E8 RID: 1256
	public static float SEEDS_PER_FRUIT = 1f;

	// Token: 0x040004E9 RID: 1257
	public static string ID = "SpiceNut";
}
