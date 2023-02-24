using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000141 RID: 321
public class FilterPlantConfig : IEntityConfig
{
	// Token: 0x0600062A RID: 1578 RVA: 0x00027DFF File Offset: 0x00025FFF
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x00027E08 File Offset: 0x00026008
	public GameObject CreatePrefab()
	{
		string text = "FilterPlant";
		string text2 = STRINGS.CREATURES.SPECIES.FILTERPLANT.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.FILTERPLANT.DESC;
		float num = 2f;
		EffectorValues tier = DECOR.PENALTY.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("cactus_kanim"), "idle_empty", Grid.SceneLayer.BuildingFront, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 348.15f);
		GameObject gameObject2 = gameObject;
		float num2 = 253.15f;
		float num3 = 293.15f;
		float num4 = 383.15f;
		float num5 = 443.15f;
		string text4 = SimHashes.Water.ToString();
		string text5 = STRINGS.CREATURES.SPECIES.FILTERPLANT.NAME;
		EntityTemplates.ExtendEntityToBasicPlant(gameObject2, num2, num3, num4, num5, new SimHashes[] { SimHashes.Oxygen }, true, 0f, 0.025f, text4, true, true, true, true, 2400f, 0f, 2200f, "FilterPlantOriginal", text5);
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = SimHashes.Sand.CreateTag(),
				massConsumptionRate = 0.008333334f
			}
		});
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = GameTags.DirtyWater,
				massConsumptionRate = 0.108333334f
			}
		});
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<SaltPlant>();
		ElementConsumer elementConsumer = gameObject.AddOrGet<ElementConsumer>();
		elementConsumer.showInStatusPanel = true;
		elementConsumer.showDescriptor = true;
		elementConsumer.storeOnConsume = false;
		elementConsumer.elementToConsume = SimHashes.Oxygen;
		elementConsumer.configuration = ElementConsumer.Configuration.Element;
		elementConsumer.consumptionRadius = 4;
		elementConsumer.sampleCellOffset = new Vector3(0f, 0f);
		elementConsumer.consumptionRate = 0.008333334f;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Harvest, "FilterPlantSeed", STRINGS.CREATURES.SPECIES.SEEDS.FILTERPLANT.NAME, STRINGS.CREATURES.SPECIES.SEEDS.FILTERPLANT.DESC, Assets.GetAnim("seed_cactus_kanim"), "object", 1, new List<Tag> { GameTags.CropSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 21, STRINGS.CREATURES.SPECIES.FILTERPLANT.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.35f, 0.35f, null, "", false), "FilterPlant_preview", Assets.GetAnim("cactus_kanim"), "place", 1, 2);
		return gameObject;
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x0002803E File Offset: 0x0002623E
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x00028040 File Offset: 0x00026240
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000433 RID: 1075
	public const string ID = "FilterPlant";

	// Token: 0x04000434 RID: 1076
	public const string SEED_ID = "FilterPlantSeed";

	// Token: 0x04000435 RID: 1077
	public const float SAND_CONSUMPTION_RATE = 0.008333334f;

	// Token: 0x04000436 RID: 1078
	public const float WATER_CONSUMPTION_RATE = 0.108333334f;

	// Token: 0x04000437 RID: 1079
	public const float OXYGEN_CONSUMPTION_RATE = 0.008333334f;
}
