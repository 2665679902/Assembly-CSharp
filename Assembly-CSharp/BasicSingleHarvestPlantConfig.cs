using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000137 RID: 311
public class BasicSingleHarvestPlantConfig : IEntityConfig
{
	// Token: 0x060005F6 RID: 1526 RVA: 0x00026AFB File Offset: 0x00024CFB
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x00026B04 File Offset: 0x00024D04
	public GameObject CreatePrefab()
	{
		string text = "BasicSingleHarvestPlant";
		string text2 = STRINGS.CREATURES.SPECIES.BASICSINGLEHARVESTPLANT.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.BASICSINGLEHARVESTPLANT.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.PENALTY.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("meallice_kanim"), "idle_empty", Grid.SceneLayer.BuildingBack, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 218.15f, 283.15f, 303.15f, 398.15f, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		}, true, 0f, 0.15f, "BasicPlantFood", true, false, true, true, 2400f, 0f, 4600f, "BasicSingleHarvestPlantOriginal", STRINGS.CREATURES.SPECIES.BASICSINGLEHARVESTPLANT.NAME);
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<LoopingSounds>();
		GameObject gameObject2 = EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Harvest, "BasicSingleHarvestPlantSeed", STRINGS.CREATURES.SPECIES.SEEDS.BASICSINGLEHARVESTPLANT.NAME, STRINGS.CREATURES.SPECIES.SEEDS.BASICSINGLEHARVESTPLANT.DESC, Assets.GetAnim("seed_meallice_kanim"), "object", 0, new List<Tag> { GameTags.CropSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 1, STRINGS.CREATURES.SPECIES.BASICSINGLEHARVESTPLANT.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false);
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = GameTags.Dirt,
				massConsumptionRate = 0.016666668f
			}
		});
		EntityTemplates.CreateAndRegisterPreviewForPlant(gameObject2, "BasicSingleHarvestPlant_preview", Assets.GetAnim("meallice_kanim"), "place", 1, 2);
		SoundEventVolumeCache.instance.AddVolume("meallice_kanim", "MealLice_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("meallice_kanim", "MealLice_LP", NOISE_POLLUTION.CREATURES.TIER4);
		return gameObject;
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x00026CCD File Offset: 0x00024ECD
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x00026CCF File Offset: 0x00024ECF
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400040A RID: 1034
	public const string ID = "BasicSingleHarvestPlant";

	// Token: 0x0400040B RID: 1035
	public const string SEED_ID = "BasicSingleHarvestPlantSeed";

	// Token: 0x0400040C RID: 1036
	public const float DIRT_RATE = 0.016666668f;
}
