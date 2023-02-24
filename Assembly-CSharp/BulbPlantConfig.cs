using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000139 RID: 313
public class BulbPlantConfig : IEntityConfig
{
	// Token: 0x06000600 RID: 1536 RVA: 0x00026EC5 File Offset: 0x000250C5
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x00026ECC File Offset: 0x000250CC
	public GameObject CreatePrefab()
	{
		string text = "BulbPlant";
		string text2 = STRINGS.CREATURES.SPECIES.BULBPLANT.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.BULBPLANT.DESC;
		float num = 1f;
		EffectorValues positive_DECOR_EFFECT = this.POSITIVE_DECOR_EFFECT;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("potted_bulb_kanim"), "grow_seed", Grid.SceneLayer.BuildingFront, 1, 1, positive_DECOR_EFFECT, default(EffectorValues), SimHashes.Creature, null, 293f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 288f, 293.15f, 313.15f, 333.15f, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		}, true, 0f, 0.15f, null, true, false, true, true, 2400f, 0f, 2200f, "BulbPlantOriginal", STRINGS.CREATURES.SPECIES.BULBPLANT.NAME);
		PrickleGrass prickleGrass = gameObject.AddOrGet<PrickleGrass>();
		prickleGrass.positive_decor_effect = this.POSITIVE_DECOR_EFFECT;
		prickleGrass.negative_decor_effect = this.NEGATIVE_DECOR_EFFECT;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Hidden, "BulbPlantSeed", STRINGS.CREATURES.SPECIES.SEEDS.BULBPLANT.NAME, STRINGS.CREATURES.SPECIES.SEEDS.BULBPLANT.DESC, Assets.GetAnim("seed_potted_bulb_kanim"), "object", 1, new List<Tag> { GameTags.DecorSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 12, STRINGS.CREATURES.SPECIES.BULBPLANT.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.4f, 0.4f, null, "", false), "BulbPlant_preview", Assets.GetAnim("potted_bulb_kanim"), "place", 1, 1);
		DiseaseDropper.Def def = gameObject.AddOrGetDef<DiseaseDropper.Def>();
		def.diseaseIdx = Db.Get().Diseases.GetIndex(Db.Get().Diseases.PollenGerms.id);
		def.singleEmitQuantity = 0;
		def.averageEmitPerSecond = 5000;
		def.emitFrequency = 5f;
		return gameObject;
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x00027082 File Offset: 0x00025282
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x00027084 File Offset: 0x00025284
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000411 RID: 1041
	public const string ID = "BulbPlant";

	// Token: 0x04000412 RID: 1042
	public const string SEED_ID = "BulbPlantSeed";

	// Token: 0x04000413 RID: 1043
	public readonly EffectorValues POSITIVE_DECOR_EFFECT = DECOR.BONUS.TIER1;

	// Token: 0x04000414 RID: 1044
	public readonly EffectorValues NEGATIVE_DECOR_EFFECT = DECOR.PENALTY.TIER3;
}
