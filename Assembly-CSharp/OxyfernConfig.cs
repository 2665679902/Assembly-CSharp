using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200014F RID: 335
public class OxyfernConfig : IEntityConfig
{
	// Token: 0x06000672 RID: 1650 RVA: 0x00029C50 File Offset: 0x00027E50
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x00029C58 File Offset: 0x00027E58
	public GameObject CreatePrefab()
	{
		string text = "Oxyfern";
		string text2 = STRINGS.CREATURES.SPECIES.OXYFERN.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.OXYFERN.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.PENALTY.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("oxy_fern_kanim"), "idle_full", Grid.SceneLayer.BuildingBack, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		gameObject = EntityTemplates.ExtendEntityToBasicPlant(gameObject, 253.15f, 273.15f, 313.15f, 373.15f, new SimHashes[] { SimHashes.CarbonDioxide }, true, 0f, 0.025f, null, true, false, true, true, 2400f, 0f, 2200f, "OxyfernOriginal", STRINGS.CREATURES.SPECIES.OXYFERN.NAME);
		Tag tag = ElementLoader.FindElementByHash(SimHashes.Water).tag;
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = tag,
				massConsumptionRate = 0.031666666f
			}
		});
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = GameTags.Dirt,
				massConsumptionRate = 0.006666667f
			}
		});
		gameObject.AddOrGet<Oxyfern>();
		gameObject.AddOrGet<LoopingSounds>();
		Storage storage = gameObject.AddOrGet<Storage>();
		storage.showInUI = false;
		storage.capacityKg = 1f;
		ElementConsumer elementConsumer = gameObject.AddOrGet<ElementConsumer>();
		elementConsumer.showInStatusPanel = false;
		elementConsumer.storeOnConsume = true;
		elementConsumer.storage = storage;
		elementConsumer.elementToConsume = SimHashes.CarbonDioxide;
		elementConsumer.configuration = ElementConsumer.Configuration.Element;
		elementConsumer.consumptionRadius = 2;
		elementConsumer.EnableConsumption(true);
		elementConsumer.sampleCellOffset = new Vector3(0f, 0f);
		elementConsumer.consumptionRate = 0.00015625001f;
		ElementConverter elementConverter = gameObject.AddOrGet<ElementConverter>();
		elementConverter.OutputMultiplier = 50f;
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(SimHashes.CarbonDioxide.ToString().ToTag(), 0.00062500004f, true)
		};
		elementConverter.outputElements = new ElementConverter.OutputElement[]
		{
			new ElementConverter.OutputElement(0.031250004f, SimHashes.Oxygen, 0f, true, false, 0f, 1f, 0.75f, byte.MaxValue, 0, true)
		};
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Hidden, "OxyfernSeed", STRINGS.CREATURES.SPECIES.SEEDS.OXYFERN.NAME, STRINGS.CREATURES.SPECIES.SEEDS.OXYFERN.DESC, Assets.GetAnim("seed_oxyfern_kanim"), "object", 1, new List<Tag> { GameTags.CropSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 20, STRINGS.CREATURES.SPECIES.OXYFERN.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false), "Oxyfern_preview", Assets.GetAnim("oxy_fern_kanim"), "place", 1, 2);
		SoundEventVolumeCache.instance.AddVolume("oxy_fern_kanim", "MealLice_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("oxy_fern_kanim", "MealLice_LP", NOISE_POLLUTION.CREATURES.TIER4);
		return gameObject;
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x00029F59 File Offset: 0x00028159
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x00029F5B File Offset: 0x0002815B
	public void OnSpawn(GameObject inst)
	{
		inst.GetComponent<Oxyfern>().SetConsumptionRate();
	}

	// Token: 0x0400046C RID: 1132
	public const string ID = "Oxyfern";

	// Token: 0x0400046D RID: 1133
	public const string SEED_ID = "OxyfernSeed";

	// Token: 0x0400046E RID: 1134
	public const float WATER_CONSUMPTION_RATE = 0.031666666f;

	// Token: 0x0400046F RID: 1135
	public const float FERTILIZATION_RATE = 0.006666667f;

	// Token: 0x04000470 RID: 1136
	public const float CO2_RATE = 0.00062500004f;

	// Token: 0x04000471 RID: 1137
	private const float CONVERSION_RATIO = 50f;

	// Token: 0x04000472 RID: 1138
	public const float OXYGEN_RATE = 0.031250004f;
}
