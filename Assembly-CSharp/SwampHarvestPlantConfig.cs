using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000159 RID: 345
public class SwampHarvestPlantConfig : IEntityConfig
{
	// Token: 0x060006A8 RID: 1704 RVA: 0x0002AE80 File Offset: 0x00029080
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x0002AE88 File Offset: 0x00029088
	public GameObject CreatePrefab()
	{
		string text = "SwampHarvestPlant";
		string text2 = STRINGS.CREATURES.SPECIES.SWAMPHARVESTPLANT.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.SWAMPHARVESTPLANT.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.PENALTY.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("swampcrop_kanim"), "idle_empty", Grid.SceneLayer.BuildingBack, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		GameObject gameObject2 = gameObject;
		float num2 = 218.15f;
		float num3 = 283.15f;
		float num4 = 303.15f;
		float num5 = 398.15f;
		string id = SwampFruitConfig.ID;
		EntityTemplates.ExtendEntityToBasicPlant(gameObject2, num2, num3, num4, num5, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		}, true, 0f, 0.15f, id, true, true, true, true, 2400f, 0f, 4600f, "SwampHarvestPlantOriginal", gameObject.PrefabID().Name);
		gameObject.AddOrGet<IlluminationVulnerable>().SetPrefersDarkness(true);
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = GameTags.DirtyWater,
				massConsumptionRate = 0.06666667f
			}
		});
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<LoopingSounds>();
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Harvest, "SwampHarvestPlantSeed", STRINGS.CREATURES.SPECIES.SEEDS.SWAMPHARVESTPLANT.NAME, STRINGS.CREATURES.SPECIES.SEEDS.SWAMPHARVESTPLANT.DESC, Assets.GetAnim("seed_swampcrop_kanim"), "object", 0, new List<Tag> { GameTags.CropSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 2, STRINGS.CREATURES.SPECIES.SWAMPHARVESTPLANT.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false), "SwampHarvestPlant_preview", Assets.GetAnim("swampcrop_kanim"), "place", 1, 2);
		return gameObject;
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x0002B030 File Offset: 0x00029230
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x0002B032 File Offset: 0x00029232
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000494 RID: 1172
	public const string ID = "SwampHarvestPlant";

	// Token: 0x04000495 RID: 1173
	public const string SEED_ID = "SwampHarvestPlantSeed";

	// Token: 0x04000496 RID: 1174
	public const float WATER_RATE = 0.06666667f;
}
