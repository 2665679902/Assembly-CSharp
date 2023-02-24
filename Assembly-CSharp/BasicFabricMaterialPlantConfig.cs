using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000134 RID: 308
public class BasicFabricMaterialPlantConfig : IEntityConfig
{
	// Token: 0x060005E6 RID: 1510 RVA: 0x00026790 File Offset: 0x00024990
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x00026798 File Offset: 0x00024998
	public GameObject CreatePrefab()
	{
		string id = BasicFabricMaterialPlantConfig.ID;
		string text = STRINGS.CREATURES.SPECIES.BASICFABRICMATERIALPLANT.NAME;
		string text2 = STRINGS.CREATURES.SPECIES.BASICFABRICMATERIALPLANT.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.BONUS.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(id, text, text2, num, Assets.GetAnim("swampreed_kanim"), "idle_empty", Grid.SceneLayer.BuildingBack, 1, 3, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		GameObject gameObject2 = gameObject;
		float num2 = 248.15f;
		float num3 = 295.15f;
		float num4 = 310.15f;
		float num5 = 398.15f;
		string id2 = BasicFabricConfig.ID;
		EntityTemplates.ExtendEntityToBasicPlant(gameObject2, num2, num3, num4, num5, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide,
			SimHashes.DirtyWater,
			SimHashes.Water
		}, false, 0f, 0.15f, id2, false, true, true, true, 2400f, 0f, 4600f, BasicFabricMaterialPlantConfig.ID + "Original", STRINGS.CREATURES.SPECIES.BASICFABRICMATERIALPLANT.NAME);
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = GameTags.DirtyWater,
				massConsumptionRate = 0.26666668f
			}
		});
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<LoopingSounds>();
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Harvest, BasicFabricMaterialPlantConfig.SEED_ID, STRINGS.CREATURES.SPECIES.SEEDS.BASICFABRICMATERIALPLANT.NAME, STRINGS.CREATURES.SPECIES.SEEDS.BASICFABRICMATERIALPLANT.DESC, Assets.GetAnim("seed_swampreed_kanim"), "object", 0, new List<Tag> { GameTags.WaterSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 20, STRINGS.CREATURES.SPECIES.BASICFABRICMATERIALPLANT.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.25f, 0.25f, null, "", false), BasicFabricMaterialPlantConfig.ID + "_preview", Assets.GetAnim("swampreed_kanim"), "place", 1, 3);
		SoundEventVolumeCache.instance.AddVolume("swampreed_kanim", "FabricPlant_grow", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("swampreed_kanim", "FabricPlant_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		return gameObject;
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x00026976 File Offset: 0x00024B76
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x00026978 File Offset: 0x00024B78
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000405 RID: 1029
	public static string ID = "BasicFabricPlant";

	// Token: 0x04000406 RID: 1030
	public static string SEED_ID = "BasicFabricMaterialPlantSeed";

	// Token: 0x04000407 RID: 1031
	public const float WATER_RATE = 0.26666668f;
}
