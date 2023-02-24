using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000138 RID: 312
public class BeanPlantConfig : IEntityConfig
{
	// Token: 0x060005FB RID: 1531 RVA: 0x00026CD9 File Offset: 0x00024ED9
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x00026CE0 File Offset: 0x00024EE0
	public GameObject CreatePrefab()
	{
		string text = "BeanPlant";
		string text2 = STRINGS.CREATURES.SPECIES.BEAN_PLANT.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.BEAN_PLANT.DESC;
		float num = 2f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("beanplant_kanim"), "idle_empty", Grid.SceneLayer.BuildingFront, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 258.15f);
		GameObject gameObject2 = gameObject;
		float num2 = 198.15f;
		float num3 = 248.15f;
		float num4 = 273.15f;
		float num5 = 323.15f;
		string text4 = STRINGS.CREATURES.SPECIES.BEAN_PLANT.NAME;
		EntityTemplates.ExtendEntityToBasicPlant(gameObject2, num2, num3, num4, num5, new SimHashes[] { SimHashes.CarbonDioxide }, true, 0f, 0.025f, "BeanPlantSeed", true, true, true, true, 2400f, 0f, 9800f, "BeanPlantOriginal", text4);
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = SimHashes.Ethanol.CreateTag(),
				massConsumptionRate = 0.033333335f
			}
		});
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = SimHashes.Dirt.CreateTag(),
				massConsumptionRate = 0.008333334f
			}
		});
		gameObject.AddOrGet<StandardCropPlant>();
		GameObject gameObject3 = EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.DigOnly, "BeanPlantSeed", STRINGS.CREATURES.SPECIES.SEEDS.BEAN_PLANT.NAME, STRINGS.CREATURES.SPECIES.SEEDS.BEAN_PLANT.DESC, Assets.GetAnim("seed_beanplant_kanim"), "object", 1, new List<Tag> { GameTags.CropSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 3, STRINGS.CREATURES.SPECIES.BEAN_PLANT.DOMESTICATEDDESC, EntityTemplates.CollisionShape.RECTANGLE, 0.6f, 0.3f, null, "", true);
		EntityTemplates.ExtendEntityToFood(gameObject3, FOOD.FOOD_TYPES.BEAN);
		EntityTemplates.CreateAndRegisterPreviewForPlant(gameObject3, "BeanPlant_preview", Assets.GetAnim("beanplant_kanim"), "place", 1, 2);
		return gameObject;
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x00026EB9 File Offset: 0x000250B9
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x00026EBB File Offset: 0x000250BB
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400040D RID: 1037
	public const string ID = "BeanPlant";

	// Token: 0x0400040E RID: 1038
	public const string SEED_ID = "BeanPlantSeed";

	// Token: 0x0400040F RID: 1039
	public const float FERTILIZATION_RATE = 0.008333334f;

	// Token: 0x04000410 RID: 1040
	public const float WATER_RATE = 0.033333335f;
}
