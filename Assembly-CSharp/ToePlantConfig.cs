using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200015B RID: 347
public class ToePlantConfig : IEntityConfig
{
	// Token: 0x060006B3 RID: 1715 RVA: 0x0002B238 File Offset: 0x00029438
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x0002B240 File Offset: 0x00029440
	public GameObject CreatePrefab()
	{
		string text = "ToePlant";
		string text2 = STRINGS.CREATURES.SPECIES.TOEPLANT.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.TOEPLANT.DESC;
		float num = 1f;
		EffectorValues positive_DECOR_EFFECT = ToePlantConfig.POSITIVE_DECOR_EFFECT;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("potted_toes_kanim"), "grow_seed", Grid.SceneLayer.BuildingFront, 1, 1, positive_DECOR_EFFECT, default(EffectorValues), SimHashes.Creature, null, TUNING.CREATURES.TEMPERATURE.FREEZING_3);
		GameObject gameObject2 = gameObject;
		SimHashes[] array = new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		};
		EntityTemplates.ExtendEntityToBasicPlant(gameObject2, TUNING.CREATURES.TEMPERATURE.FREEZING_10, TUNING.CREATURES.TEMPERATURE.FREEZING_9, TUNING.CREATURES.TEMPERATURE.FREEZING, TUNING.CREATURES.TEMPERATURE.COOL, array, true, 0f, 0.15f, null, true, false, true, true, 2400f, 0f, 2200f, "ToePlantOriginal", STRINGS.CREATURES.SPECIES.TOEPLANT.NAME);
		PrickleGrass prickleGrass = gameObject.AddOrGet<PrickleGrass>();
		prickleGrass.positive_decor_effect = ToePlantConfig.POSITIVE_DECOR_EFFECT;
		prickleGrass.negative_decor_effect = ToePlantConfig.NEGATIVE_DECOR_EFFECT;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Hidden, "ToePlantSeed", STRINGS.CREATURES.SPECIES.SEEDS.TOEPLANT.NAME, STRINGS.CREATURES.SPECIES.SEEDS.TOEPLANT.DESC, Assets.GetAnim("seed_potted_toes_kanim"), "object", 1, new List<Tag> { GameTags.DecorSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 12, STRINGS.CREATURES.SPECIES.TOEPLANT.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.25f, 0.25f, null, "", false), "ToePlant_preview", Assets.GetAnim("potted_toes_kanim"), "place", 1, 1);
		return gameObject;
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x0002B3AB File Offset: 0x000295AB
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x0002B3AD File Offset: 0x000295AD
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000499 RID: 1177
	public const string ID = "ToePlant";

	// Token: 0x0400049A RID: 1178
	public const string SEED_ID = "ToePlantSeed";

	// Token: 0x0400049B RID: 1179
	public static readonly EffectorValues POSITIVE_DECOR_EFFECT = DECOR.BONUS.TIER3;

	// Token: 0x0400049C RID: 1180
	public static readonly EffectorValues NEGATIVE_DECOR_EFFECT = DECOR.PENALTY.TIER3;
}
