using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200013E RID: 318
public class CritterTrapPlantConfig : IEntityConfig
{
	// Token: 0x0600061A RID: 1562 RVA: 0x00027874 File Offset: 0x00025A74
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x0002787C File Offset: 0x00025A7C
	public GameObject CreatePrefab()
	{
		string text = "CritterTrapPlant";
		string text2 = STRINGS.CREATURES.SPECIES.CRITTERTRAPPLANT.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.CRITTERTRAPPLANT.DESC;
		float num = 4f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("venus_critter_trap_kanim"), "idle_open", Grid.SceneLayer.BuildingBack, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, TUNING.CREATURES.TEMPERATURE.FREEZING_3);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, TUNING.CREATURES.TEMPERATURE.FREEZING_10, TUNING.CREATURES.TEMPERATURE.FREEZING_9, TUNING.CREATURES.TEMPERATURE.FREEZING, TUNING.CREATURES.TEMPERATURE.COOL, null, false, 0f, 0.15f, "PlantMeat", true, true, true, false, 2400f, 0f, 2200f, "CritterTrapPlantOriginal", STRINGS.CREATURES.SPECIES.CRITTERTRAPPLANT.NAME);
		UnityEngine.Object.DestroyImmediate(gameObject.GetComponent<MutantPlant>());
		TrapTrigger trapTrigger = gameObject.AddOrGet<TrapTrigger>();
		trapTrigger.trappableCreatures = new Tag[]
		{
			GameTags.Creatures.Walker,
			GameTags.Creatures.Hoverer
		};
		trapTrigger.trappedOffset = new Vector2(0.5f, 0f);
		trapTrigger.enabled = false;
		CritterTrapPlant critterTrapPlant = gameObject.AddOrGet<CritterTrapPlant>();
		critterTrapPlant.gasOutputRate = 0.041666668f;
		critterTrapPlant.outputElement = SimHashes.Hydrogen;
		critterTrapPlant.gasVentThreshold = 33.25f;
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.AddOrGet<Storage>();
		Tag tag = ElementLoader.FindElementByHash(SimHashes.DirtyWater).tag;
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = tag,
				massConsumptionRate = 0.016666668f
			}
		});
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Hidden, "CritterTrapPlantSeed", STRINGS.CREATURES.SPECIES.SEEDS.CRITTERTRAPPLANT.NAME, STRINGS.CREATURES.SPECIES.SEEDS.CRITTERTRAPPLANT.DESC, Assets.GetAnim("seed_critter_trap_kanim"), "object", 1, new List<Tag> { GameTags.CropSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 21, STRINGS.CREATURES.SPECIES.CRITTERTRAPPLANT.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false), "CritterTrapPlant_preview", Assets.GetAnim("venus_critter_trap_kanim"), "place", 1, 2);
		return gameObject;
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x00027A87 File Offset: 0x00025C87
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x00027A89 File Offset: 0x00025C89
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000427 RID: 1063
	public const string ID = "CritterTrapPlant";

	// Token: 0x04000428 RID: 1064
	public const float WATER_RATE = 0.016666668f;

	// Token: 0x04000429 RID: 1065
	public const float GAS_RATE = 0.041666668f;

	// Token: 0x0400042A RID: 1066
	public const float GAS_VENT_THRESHOLD = 33.25f;
}
