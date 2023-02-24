using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000140 RID: 320
public class EvilFlowerConfig : IEntityConfig
{
	// Token: 0x06000625 RID: 1573 RVA: 0x00027C26 File Offset: 0x00025E26
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x00027C30 File Offset: 0x00025E30
	public GameObject CreatePrefab()
	{
		string text = "EvilFlower";
		string text2 = STRINGS.CREATURES.SPECIES.EVILFLOWER.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.EVILFLOWER.DESC;
		float num = 1f;
		EffectorValues positive_DECOR_EFFECT = this.POSITIVE_DECOR_EFFECT;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("potted_evilflower_kanim"), "grow_seed", Grid.SceneLayer.BuildingFront, 1, 1, positive_DECOR_EFFECT, default(EffectorValues), SimHashes.Creature, null, 293f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 168.15f, 258.15f, 513.15f, 563.15f, new SimHashes[] { SimHashes.CarbonDioxide }, true, 0f, 0.15f, null, true, false, true, true, 2400f, 0f, 12200f, "EvilFlowerOriginal", STRINGS.CREATURES.SPECIES.EVILFLOWER.NAME);
		EvilFlower evilFlower = gameObject.AddOrGet<EvilFlower>();
		evilFlower.positive_decor_effect = this.POSITIVE_DECOR_EFFECT;
		evilFlower.negative_decor_effect = this.NEGATIVE_DECOR_EFFECT;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Hidden, "EvilFlowerSeed", STRINGS.CREATURES.SPECIES.SEEDS.EVILFLOWER.NAME, STRINGS.CREATURES.SPECIES.SEEDS.EVILFLOWER.DESC, Assets.GetAnim("seed_potted_evilflower_kanim"), "object", 1, new List<Tag> { GameTags.DecorSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 19, STRINGS.CREATURES.SPECIES.EVILFLOWER.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.4f, 0.4f, null, "", false), "EvilFlower_preview", Assets.GetAnim("potted_evilflower_kanim"), "place", 1, 1);
		DiseaseDropper.Def def = gameObject.AddOrGetDef<DiseaseDropper.Def>();
		def.diseaseIdx = Db.Get().Diseases.GetIndex("ZombieSpores");
		def.emitFrequency = 1f;
		def.averageEmitPerSecond = 1000;
		def.singleEmitQuantity = 100000;
		return gameObject;
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x00027DDD File Offset: 0x00025FDD
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x00027DDF File Offset: 0x00025FDF
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400042F RID: 1071
	public const string ID = "EvilFlower";

	// Token: 0x04000430 RID: 1072
	public const string SEED_ID = "EvilFlowerSeed";

	// Token: 0x04000431 RID: 1073
	public readonly EffectorValues POSITIVE_DECOR_EFFECT = DECOR.BONUS.TIER7;

	// Token: 0x04000432 RID: 1074
	public readonly EffectorValues NEGATIVE_DECOR_EFFECT = DECOR.PENALTY.TIER5;
}
