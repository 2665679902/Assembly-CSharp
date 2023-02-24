using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200013C RID: 316
public class ColdBreatherConfig : IEntityConfig
{
	// Token: 0x0600060F RID: 1551 RVA: 0x0002736B File Offset: 0x0002556B
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x00027374 File Offset: 0x00025574
	public GameObject CreatePrefab()
	{
		string text = "ColdBreather";
		string text2 = STRINGS.CREATURES.SPECIES.COLDBREATHER.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.COLDBREATHER.DESC;
		float num = 400f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER2;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("coldbreather_kanim"), "grow_seed", Grid.SceneLayer.BuildingFront, 1, 2, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.AddOrGet<ReceptacleMonitor>();
		gameObject.AddOrGet<EntombVulnerable>();
		gameObject.AddOrGet<WiltCondition>();
		gameObject.AddOrGet<Prioritizable>();
		gameObject.AddOrGet<Uprootable>();
		gameObject.AddOrGet<UprootedMonitor>();
		gameObject.AddOrGet<DrowningMonitor>();
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = SimHashes.Phosphorite.CreateTag(),
				massConsumptionRate = 0.006666667f
			}
		});
		gameObject.AddOrGet<TemperatureVulnerable>().Configure(213.15f, 183.15f, 368.15f, 463.15f);
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		ColdBreather coldBreather = gameObject.AddOrGet<ColdBreather>();
		coldBreather.deltaEmitTemperature = -5f;
		coldBreather.emitOffsetCell = new Vector3(0f, 1f);
		coldBreather.consumptionRate = 1f;
		gameObject.AddOrGet<KBatchedAnimController>().randomiseLoopedOffset = true;
		BuildingTemplates.CreateDefaultStorage(gameObject, false).showInUI = false;
		ElementConsumer elementConsumer = gameObject.AddOrGet<ElementConsumer>();
		elementConsumer.storeOnConsume = true;
		elementConsumer.configuration = ElementConsumer.Configuration.AllGas;
		elementConsumer.capacityKG = 2f;
		elementConsumer.consumptionRate = 0.25f;
		elementConsumer.consumptionRadius = 1;
		elementConsumer.sampleCellOffset = new Vector3(0f, 0f);
		SimTemperatureTransfer component = gameObject.GetComponent<SimTemperatureTransfer>();
		component.SurfaceArea = 10f;
		component.Thickness = 0.001f;
		if (DlcManager.FeatureRadiationEnabled())
		{
			RadiationEmitter radiationEmitter = gameObject.AddComponent<RadiationEmitter>();
			radiationEmitter.emitType = RadiationEmitter.RadiationEmitterType.Constant;
			radiationEmitter.radiusProportionalToRads = false;
			radiationEmitter.emitRadiusX = 6;
			radiationEmitter.emitRadiusY = radiationEmitter.emitRadiusX;
			radiationEmitter.emitRads = 480f;
			radiationEmitter.emissionOffset = new Vector3(0f, 0f, 0f);
		}
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Hidden, "ColdBreatherSeed", STRINGS.CREATURES.SPECIES.SEEDS.COLDBREATHER.NAME, STRINGS.CREATURES.SPECIES.SEEDS.COLDBREATHER.DESC, Assets.GetAnim("seed_coldbreather_kanim"), "object", 1, new List<Tag> { GameTags.CropSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 21, STRINGS.CREATURES.SPECIES.COLDBREATHER.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false), "ColdBreather_preview", Assets.GetAnim("coldbreather_kanim"), "place", 1, 2);
		SoundEventVolumeCache.instance.AddVolume("coldbreather_kanim", "ColdBreather_grow", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("coldbreather_kanim", "ColdBreather_intake", NOISE_POLLUTION.CREATURES.TIER3);
		return gameObject;
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x00027632 File Offset: 0x00025832
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x00027634 File Offset: 0x00025834
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400041A RID: 1050
	public const string ID = "ColdBreather";

	// Token: 0x0400041B RID: 1051
	public static readonly Tag TAG = TagManager.Create("ColdBreather");

	// Token: 0x0400041C RID: 1052
	public const float FERTILIZATION_RATE = 0.006666667f;

	// Token: 0x0400041D RID: 1053
	public const SimHashes FERTILIZER = SimHashes.Phosphorite;

	// Token: 0x0400041E RID: 1054
	public const float TEMP_DELTA = -5f;

	// Token: 0x0400041F RID: 1055
	public const float CONSUMPTION_RATE = 1f;

	// Token: 0x04000420 RID: 1056
	public const float RADIATION_STRENGTH = 480f;

	// Token: 0x04000421 RID: 1057
	public const string SEED_ID = "ColdBreatherSeed";

	// Token: 0x04000422 RID: 1058
	public static readonly Tag SEED_TAG = TagManager.Create("ColdBreatherSeed");
}
