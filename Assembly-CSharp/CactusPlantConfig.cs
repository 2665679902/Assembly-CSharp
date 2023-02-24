using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200013A RID: 314
public class CactusPlantConfig : IEntityConfig
{
	// Token: 0x06000605 RID: 1541 RVA: 0x000270A4 File Offset: 0x000252A4
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x000270AC File Offset: 0x000252AC
	public GameObject CreatePrefab()
	{
		string text = "CactusPlant";
		string text2 = STRINGS.CREATURES.SPECIES.CACTUSPLANT.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.CACTUSPLANT.DESC;
		float num = 1f;
		EffectorValues positive_DECOR_EFFECT = this.POSITIVE_DECOR_EFFECT;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("potted_cactus_kanim"), "grow_seed", Grid.SceneLayer.BuildingFront, 1, 1, positive_DECOR_EFFECT, default(EffectorValues), SimHashes.Creature, null, 293f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 200f, 273.15f, 373.15f, 400f, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		}, false, 0f, 0.15f, null, true, false, true, true, 2400f, 0f, 2200f, "CactusPlantOriginal", STRINGS.CREATURES.SPECIES.CACTUSPLANT.NAME);
		PrickleGrass prickleGrass = gameObject.AddOrGet<PrickleGrass>();
		prickleGrass.positive_decor_effect = this.POSITIVE_DECOR_EFFECT;
		prickleGrass.negative_decor_effect = this.NEGATIVE_DECOR_EFFECT;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Hidden, "CactusPlantSeed", STRINGS.CREATURES.SPECIES.SEEDS.CACTUSPLANT.NAME, STRINGS.CREATURES.SPECIES.SEEDS.CACTUSPLANT.DESC, Assets.GetAnim("seed_potted_cactus_kanim"), "object", 1, new List<Tag> { GameTags.DecorSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 13, STRINGS.CREATURES.SPECIES.CACTUSPLANT.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.25f, 0.25f, null, "", false), "CactusPlant_preview", Assets.GetAnim("potted_cactus_kanim"), "place", 1, 1);
		return gameObject;
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x00027217 File Offset: 0x00025417
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x00027219 File Offset: 0x00025419
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000415 RID: 1045
	public const string ID = "CactusPlant";

	// Token: 0x04000416 RID: 1046
	public const string SEED_ID = "CactusPlantSeed";

	// Token: 0x04000417 RID: 1047
	public readonly EffectorValues POSITIVE_DECOR_EFFECT = DECOR.BONUS.TIER3;

	// Token: 0x04000418 RID: 1048
	public readonly EffectorValues NEGATIVE_DECOR_EFFECT = DECOR.PENALTY.TIER3;
}
