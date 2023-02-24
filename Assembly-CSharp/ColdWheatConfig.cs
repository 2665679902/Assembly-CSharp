using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200013D RID: 317
public class ColdWheatConfig : IEntityConfig
{
	// Token: 0x06000615 RID: 1557 RVA: 0x0002765E File Offset: 0x0002585E
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x00027668 File Offset: 0x00025868
	public GameObject CreatePrefab()
	{
		string text = "ColdWheat";
		string text2 = STRINGS.CREATURES.SPECIES.COLDWHEAT.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.COLDWHEAT.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("coldwheat_kanim"), "idle_empty", Grid.SceneLayer.BuildingFront, 1, 1, tier, default(EffectorValues), SimHashes.Creature, null, 255f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 118.149994f, 218.15f, 278.15f, 358.15f, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		}, true, 0f, 0.15f, "ColdWheatSeed", true, true, true, true, 2400f, 0f, 12200f, "ColdWheatOriginal", STRINGS.CREATURES.SPECIES.COLDWHEAT.NAME);
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = GameTags.Dirt,
				massConsumptionRate = 0.008333334f
			}
		});
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = GameTags.Water,
				massConsumptionRate = 0.033333335f
			}
		});
		gameObject.AddOrGet<StandardCropPlant>();
		GameObject gameObject2 = EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.DigOnly, "ColdWheatSeed", STRINGS.CREATURES.SPECIES.SEEDS.COLDWHEAT.NAME, STRINGS.CREATURES.SPECIES.SEEDS.COLDWHEAT.DESC, Assets.GetAnim("seed_coldwheat_kanim"), "object", 1, new List<Tag> { GameTags.CropSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 3, STRINGS.CREATURES.SPECIES.COLDWHEAT.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.2f, 0.2f, null, "", true);
		EntityTemplates.ExtendEntityToFood(gameObject2, FOOD.FOOD_TYPES.COLD_WHEAT_SEED);
		EntityTemplates.CreateAndRegisterPreviewForPlant(gameObject2, "ColdWheat_preview", Assets.GetAnim("coldwheat_kanim"), "place", 1, 1);
		SoundEventVolumeCache.instance.AddVolume("coldwheat_kanim", "ColdWheat_grow", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("coldwheat_kanim", "ColdWheat_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		return gameObject;
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x00027868 File Offset: 0x00025A68
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x0002786A File Offset: 0x00025A6A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000423 RID: 1059
	public const string ID = "ColdWheat";

	// Token: 0x04000424 RID: 1060
	public const string SEED_ID = "ColdWheatSeed";

	// Token: 0x04000425 RID: 1061
	public const float FERTILIZATION_RATE = 0.008333334f;

	// Token: 0x04000426 RID: 1062
	public const float WATER_RATE = 0.033333335f;
}
