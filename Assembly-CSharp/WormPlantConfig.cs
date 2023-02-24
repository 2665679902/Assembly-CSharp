using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200015E RID: 350
public class WormPlantConfig : IEntityConfig
{
	// Token: 0x060006C4 RID: 1732 RVA: 0x0002B7E6 File Offset: 0x000299E6
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x0002B7F0 File Offset: 0x000299F0
	public static GameObject BaseWormPlant(string id, string name, string desc, string animFile, EffectorValues decor, string cropID)
	{
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(id, name, desc, 1f, Assets.GetAnim(animFile), "idle_empty", Grid.SceneLayer.BuildingBack, 1, 2, decor, default(EffectorValues), SimHashes.Creature, null, 307.15f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 273.15f, 288.15f, 323.15f, 373.15f, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		}, true, 0f, 0.15f, cropID, true, true, true, true, 2400f, 0f, 9800f, id + "Original", name);
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = SimHashes.Sulfur.CreateTag(),
				massConsumptionRate = 0.016666668f
			}
		});
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<LoopingSounds>();
		return gameObject;
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x0002B8DC File Offset: 0x00029ADC
	public GameObject CreatePrefab()
	{
		GameObject gameObject = WormPlantConfig.BaseWormPlant("WormPlant", STRINGS.CREATURES.SPECIES.WORMPLANT.NAME, STRINGS.CREATURES.SPECIES.WORMPLANT.DESC, "wormwood_kanim", WormPlantConfig.BASIC_DECOR, "WormBasicFruit");
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Harvest, "WormPlantSeed", STRINGS.CREATURES.SPECIES.SEEDS.WORMPLANT.NAME, STRINGS.CREATURES.SPECIES.SEEDS.WORMPLANT.DESC, Assets.GetAnim("seed_wormwood_kanim"), "object", 0, new List<Tag> { GameTags.CropSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 3, STRINGS.CREATURES.SPECIES.WORMPLANT.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false), "WormPlant_preview", Assets.GetAnim("wormwood_kanim"), "place", 1, 2);
		return gameObject;
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x0002B9A4 File Offset: 0x00029BA4
	public void OnPrefabInit(GameObject prefab)
	{
		TransformingPlant transformingPlant = prefab.AddOrGet<TransformingPlant>();
		transformingPlant.transformPlantId = "SuperWormPlant";
		transformingPlant.SubscribeToTransformEvent(GameHashes.CropTended);
		transformingPlant.useGrowthTimeRatio = true;
		transformingPlant.eventDataCondition = delegate(object data)
		{
			CropTendingStates.CropTendingEventData cropTendingEventData = (CropTendingStates.CropTendingEventData)data;
			if (cropTendingEventData != null)
			{
				CreatureBrain component = cropTendingEventData.source.GetComponent<CreatureBrain>();
				if (component != null && component.species == GameTags.Creatures.Species.DivergentSpecies)
				{
					return true;
				}
			}
			return false;
		};
		transformingPlant.fxKAnim = "plant_transform_fx_kanim";
		transformingPlant.fxAnim = "plant_transform";
		prefab.AddOrGet<StandardCropPlant>().anims = WormPlantConfig.animSet;
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x0002BA1E File Offset: 0x00029C1E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004A8 RID: 1192
	public const string ID = "WormPlant";

	// Token: 0x040004A9 RID: 1193
	public const string SEED_ID = "WormPlantSeed";

	// Token: 0x040004AA RID: 1194
	public const float SULFUR_CONSUMPTION_RATE = 0.016666668f;

	// Token: 0x040004AB RID: 1195
	public static readonly EffectorValues BASIC_DECOR = DECOR.PENALTY.TIER0;

	// Token: 0x040004AC RID: 1196
	public const string BASIC_CROP_ID = "WormBasicFruit";

	// Token: 0x040004AD RID: 1197
	private static StandardCropPlant.AnimSet animSet = new StandardCropPlant.AnimSet
	{
		grow = "basic_grow",
		grow_pst = "basic_grow_pst",
		idle_full = "basic_idle_full",
		wilt_base = "basic_wilt",
		harvest = "basic_harvest"
	};
}
