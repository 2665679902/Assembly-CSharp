using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200014C RID: 332
public class MushroomPlantConfig : IEntityConfig
{
	// Token: 0x06000663 RID: 1635 RVA: 0x000298B3 File Offset: 0x00027AB3
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x000298BC File Offset: 0x00027ABC
	public GameObject CreatePrefab()
	{
		string text = "MushroomPlant";
		string text2 = STRINGS.CREATURES.SPECIES.MUSHROOMPLANT.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.MUSHROOMPLANT.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("fungusplant_kanim"), "idle_empty", Grid.SceneLayer.BuildingFront, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 228.15f, 278.15f, 308.15f, 398.15f, new SimHashes[] { SimHashes.CarbonDioxide }, true, 0f, 0.15f, MushroomConfig.ID, true, true, true, true, 2400f, 0f, 4600f, "MushroomPlantOriginal", STRINGS.CREATURES.SPECIES.MUSHROOMPLANT.NAME);
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = GameTags.SlimeMold,
				massConsumptionRate = 0.006666667f
			}
		});
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<IlluminationVulnerable>().SetPrefersDarkness(true);
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Harvest, "MushroomSeed", STRINGS.CREATURES.SPECIES.SEEDS.MUSHROOMPLANT.NAME, STRINGS.CREATURES.SPECIES.SEEDS.MUSHROOMPLANT.DESC, Assets.GetAnim("seed_fungusplant_kanim"), "object", 0, new List<Tag> { GameTags.CropSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 3, STRINGS.CREATURES.SPECIES.MUSHROOMPLANT.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.33f, 0.33f, null, "", false), "MushroomPlant_preview", Assets.GetAnim("fungusplant_kanim"), "place", 1, 2);
		SoundEventVolumeCache.instance.AddVolume("bristleblossom_kanim", "PrickleFlower_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("bristleblossom_kanim", "PrickleFlower_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		return gameObject;
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x00029A84 File Offset: 0x00027C84
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x00029A86 File Offset: 0x00027C86
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000467 RID: 1127
	public const float FERTILIZATION_RATE = 0.006666667f;

	// Token: 0x04000468 RID: 1128
	public const string ID = "MushroomPlant";

	// Token: 0x04000469 RID: 1129
	public const string SEED_ID = "MushroomSeed";
}
