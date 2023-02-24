using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000154 RID: 340
public class SeaLettuceConfig : IEntityConfig
{
	// Token: 0x0600068D RID: 1677 RVA: 0x0002A7BA File Offset: 0x000289BA
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x0002A7C4 File Offset: 0x000289C4
	public GameObject CreatePrefab()
	{
		string id = SeaLettuceConfig.ID;
		string text = STRINGS.CREATURES.SPECIES.SEALETTUCE.NAME;
		string text2 = STRINGS.CREATURES.SPECIES.SEALETTUCE.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.BONUS.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(id, text, text2, num, Assets.GetAnim("sea_lettuce_kanim"), "idle_empty", Grid.SceneLayer.BuildingBack, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 308.15f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 248.15f, 295.15f, 338.15f, 398.15f, new SimHashes[]
		{
			SimHashes.Water,
			SimHashes.SaltWater,
			SimHashes.Brine
		}, false, 0f, 0.15f, "Lettuce", true, true, true, true, 2400f, 0f, 7400f, SeaLettuceConfig.ID + "Original", STRINGS.CREATURES.SPECIES.SEALETTUCE.NAME);
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = SimHashes.SaltWater.CreateTag(),
				massConsumptionRate = 0.008333334f
			}
		});
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = SimHashes.BleachStone.CreateTag(),
				massConsumptionRate = 0.00083333335f
			}
		});
		gameObject.GetComponent<DrowningMonitor>().canDrownToDeath = false;
		gameObject.GetComponent<DrowningMonitor>().livesUnderWater = true;
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<LoopingSounds>();
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Harvest, "SeaLettuceSeed", STRINGS.CREATURES.SPECIES.SEEDS.SEALETTUCE.NAME, STRINGS.CREATURES.SPECIES.SEEDS.SEALETTUCE.DESC, Assets.GetAnim("seed_sealettuce_kanim"), "object", 0, new List<Tag> { GameTags.WaterSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 3, STRINGS.CREATURES.SPECIES.SEALETTUCE.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.25f, 0.25f, null, "", false), SeaLettuceConfig.ID + "_preview", Assets.GetAnim("sea_lettuce_kanim"), "place", 1, 2);
		SoundEventVolumeCache.instance.AddVolume("sea_lettuce_kanim", "SeaLettuce_grow", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("sea_lettuce_kanim", "SeaLettuce_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		return gameObject;
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x0002A9F5 File Offset: 0x00028BF5
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x0002A9F7 File Offset: 0x00028BF7
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000485 RID: 1157
	public static string ID = "SeaLettuce";

	// Token: 0x04000486 RID: 1158
	public const string SEED_ID = "SeaLettuceSeed";

	// Token: 0x04000487 RID: 1159
	public const float WATER_RATE = 0.008333334f;

	// Token: 0x04000488 RID: 1160
	public const float FERTILIZATION_RATE = 0.00083333335f;
}
