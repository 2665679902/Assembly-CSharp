using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000155 RID: 341
public class SpiceVineConfig : IEntityConfig
{
	// Token: 0x06000693 RID: 1683 RVA: 0x0002AA0D File Offset: 0x00028C0D
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x0002AA14 File Offset: 0x00028C14
	public GameObject CreatePrefab()
	{
		string text = "SpiceVine";
		string text2 = STRINGS.CREATURES.SPECIES.SPICE_VINE.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.SPICE_VINE.DESC;
		float num = 2f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("vinespicenut_kanim"), "idle_empty", Grid.SceneLayer.BuildingFront, 1, 3, tier, default(EffectorValues), SimHashes.Creature, new List<Tag> { GameTags.Hanging }, 320f);
		EntityTemplates.MakeHangingOffsets(gameObject, 1, 3);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 258.15f, 308.15f, 358.15f, 448.15f, null, true, 0f, 0.15f, SpiceNutConfig.ID, true, true, true, true, 2400f, 0f, 9800f, "SpiceVineOriginal", STRINGS.CREATURES.SPECIES.SPICE_VINE.NAME);
		Tag tag = ElementLoader.FindElementByHash(SimHashes.DirtyWater).tag;
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = tag,
				massConsumptionRate = 0.058333334f
			}
		});
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = GameTags.Phosphorite,
				massConsumptionRate = 0.0016666667f
			}
		});
		gameObject.GetComponent<UprootedMonitor>().monitorCells = new CellOffset[]
		{
			new CellOffset(0, 1)
		};
		gameObject.AddOrGet<StandardCropPlant>();
		EntityTemplates.MakeHangingOffsets(EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Harvest, "SpiceVineSeed", STRINGS.CREATURES.SPECIES.SEEDS.SPICE_VINE.NAME, STRINGS.CREATURES.SPECIES.SEEDS.SPICE_VINE.DESC, Assets.GetAnim("seed_spicenut_kanim"), "object", 1, new List<Tag> { GameTags.CropSeed }, SingleEntityReceptacle.ReceptacleDirection.Bottom, default(Tag), 4, STRINGS.CREATURES.SPECIES.SPICE_VINE.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false), "SpiceVine_preview", Assets.GetAnim("vinespicenut_kanim"), "place", 1, 3), 1, 3);
		return gameObject;
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x0002AC12 File Offset: 0x00028E12
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x0002AC14 File Offset: 0x00028E14
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000489 RID: 1161
	public const string ID = "SpiceVine";

	// Token: 0x0400048A RID: 1162
	public const string SEED_ID = "SpiceVineSeed";

	// Token: 0x0400048B RID: 1163
	public const float FERTILIZATION_RATE = 0.0016666667f;

	// Token: 0x0400048C RID: 1164
	public const float WATER_RATE = 0.058333334f;
}
