using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000151 RID: 337
public class PrickleGrassConfig : IEntityConfig
{
	// Token: 0x0600067C RID: 1660 RVA: 0x0002A211 File Offset: 0x00028411
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x0002A218 File Offset: 0x00028418
	public GameObject CreatePrefab()
	{
		string text = "PrickleGrass";
		string text2 = STRINGS.CREATURES.SPECIES.PRICKLEGRASS.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.PRICKLEGRASS.DESC;
		float num = 1f;
		EffectorValues positive_DECOR_EFFECT = PrickleGrassConfig.POSITIVE_DECOR_EFFECT;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("bristlebriar_kanim"), "grow_seed", Grid.SceneLayer.BuildingFront, 1, 1, positive_DECOR_EFFECT, default(EffectorValues), SimHashes.Creature, null, 293f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 218.15f, 283.15f, 303.15f, 398.15f, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		}, true, 0f, 0.15f, null, true, false, true, true, 2400f, 0f, 900f, "PrickleGrassOriginal", STRINGS.CREATURES.SPECIES.PRICKLEGRASS.NAME);
		PrickleGrass prickleGrass = gameObject.AddOrGet<PrickleGrass>();
		prickleGrass.positive_decor_effect = PrickleGrassConfig.POSITIVE_DECOR_EFFECT;
		prickleGrass.negative_decor_effect = PrickleGrassConfig.NEGATIVE_DECOR_EFFECT;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Hidden, "PrickleGrassSeed", STRINGS.CREATURES.SPECIES.SEEDS.PRICKLEGRASS.NAME, STRINGS.CREATURES.SPECIES.SEEDS.PRICKLEGRASS.DESC, Assets.GetAnim("seed_bristlebriar_kanim"), "object", 1, new List<Tag> { GameTags.DecorSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 10, STRINGS.CREATURES.SPECIES.PRICKLEGRASS.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.25f, 0.25f, null, "", false), "PrickleGrass_preview", Assets.GetAnim("bristlebriar_kanim"), "place", 1, 1);
		return gameObject;
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0002A380 File Offset: 0x00028580
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x0002A382 File Offset: 0x00028582
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000476 RID: 1142
	public const string ID = "PrickleGrass";

	// Token: 0x04000477 RID: 1143
	public const string SEED_ID = "PrickleGrassSeed";

	// Token: 0x04000478 RID: 1144
	public static readonly EffectorValues POSITIVE_DECOR_EFFECT = DECOR.BONUS.TIER3;

	// Token: 0x04000479 RID: 1145
	public static readonly EffectorValues NEGATIVE_DECOR_EFFECT = DECOR.PENALTY.TIER3;
}
