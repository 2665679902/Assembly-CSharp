using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200014A RID: 330
public class LeafyPlantConfig : IEntityConfig
{
	// Token: 0x06000659 RID: 1625 RVA: 0x000295EC File Offset: 0x000277EC
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x000295F4 File Offset: 0x000277F4
	public GameObject CreatePrefab()
	{
		string text = "LeafyPlant";
		string text2 = STRINGS.CREATURES.SPECIES.LEAFYPLANT.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.LEAFYPLANT.DESC;
		float num = 1f;
		EffectorValues positive_DECOR_EFFECT = this.POSITIVE_DECOR_EFFECT;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("potted_leafy_kanim"), "grow_seed", Grid.SceneLayer.BuildingFront, 1, 1, positive_DECOR_EFFECT, default(EffectorValues), SimHashes.Creature, null, 293f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 288f, 293.15f, 323.15f, 373f, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide,
			SimHashes.ChlorineGas,
			SimHashes.Hydrogen
		}, true, 0f, 0.15f, null, true, false, true, true, 2400f, 0f, 2200f, "LeafyPlantOriginal", STRINGS.CREATURES.SPECIES.LEAFYPLANT.NAME);
		PrickleGrass prickleGrass = gameObject.AddOrGet<PrickleGrass>();
		prickleGrass.positive_decor_effect = this.POSITIVE_DECOR_EFFECT;
		prickleGrass.negative_decor_effect = this.NEGATIVE_DECOR_EFFECT;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Hidden, "LeafyPlantSeed", STRINGS.CREATURES.SPECIES.SEEDS.LEAFYPLANT.NAME, STRINGS.CREATURES.SPECIES.SEEDS.LEAFYPLANT.DESC, Assets.GetAnim("seed_potted_leafy_kanim"), "object", 1, new List<Tag> { GameTags.DecorSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 12, STRINGS.CREATURES.SPECIES.LEAFYPLANT.DOMESTICATEDDESC, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.6f, null, "", false), "LeafyPlant_preview", Assets.GetAnim("potted_leafy_kanim"), "place", 1, 1);
		return gameObject;
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x0002975F File Offset: 0x0002795F
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x00029761 File Offset: 0x00027961
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000462 RID: 1122
	public const string ID = "LeafyPlant";

	// Token: 0x04000463 RID: 1123
	public const string SEED_ID = "LeafyPlantSeed";

	// Token: 0x04000464 RID: 1124
	public readonly EffectorValues POSITIVE_DECOR_EFFECT = DECOR.BONUS.TIER3;

	// Token: 0x04000465 RID: 1125
	public readonly EffectorValues NEGATIVE_DECOR_EFFECT = DECOR.PENALTY.TIER3;
}
