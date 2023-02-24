using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200013F RID: 319
public class CylindricaConfig : IEntityConfig
{
	// Token: 0x0600061F RID: 1567 RVA: 0x00027A93 File Offset: 0x00025C93
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x00027A9C File Offset: 0x00025C9C
	public GameObject CreatePrefab()
	{
		string text = "Cylindrica";
		string text2 = STRINGS.CREATURES.SPECIES.CYLINDRICA.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.CYLINDRICA.DESC;
		float num = 1f;
		EffectorValues positive_DECOR_EFFECT = CylindricaConfig.POSITIVE_DECOR_EFFECT;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("potted_cylindricafan_kanim"), "grow_seed", Grid.SceneLayer.BuildingFront, 1, 1, positive_DECOR_EFFECT, default(EffectorValues), SimHashes.Creature, null, 298.15f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 288.15f, 293.15f, 323.15f, 373.15f, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		}, true, 0f, 0.15f, null, true, false, true, true, 2400f, 0f, 2200f, "CylindricaOriginal", STRINGS.CREATURES.SPECIES.CYLINDRICA.NAME);
		PrickleGrass prickleGrass = gameObject.AddOrGet<PrickleGrass>();
		prickleGrass.positive_decor_effect = CylindricaConfig.POSITIVE_DECOR_EFFECT;
		prickleGrass.negative_decor_effect = CylindricaConfig.NEGATIVE_DECOR_EFFECT;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Hidden, "CylindricaSeed", STRINGS.CREATURES.SPECIES.SEEDS.CYLINDRICA.NAME, STRINGS.CREATURES.SPECIES.SEEDS.CYLINDRICA.DESC, Assets.GetAnim("seed_potted_cylindricafan_kanim"), "object", 1, new List<Tag> { GameTags.DecorSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 12, STRINGS.CREATURES.SPECIES.CYLINDRICA.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.25f, 0.25f, null, "", false), "Cylindrica_preview", Assets.GetAnim("potted_cylindricafan_kanim"), "place", 1, 1);
		return gameObject;
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x00027C04 File Offset: 0x00025E04
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x00027C06 File Offset: 0x00025E06
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400042B RID: 1067
	public const string ID = "Cylindrica";

	// Token: 0x0400042C RID: 1068
	public const string SEED_ID = "CylindricaSeed";

	// Token: 0x0400042D RID: 1069
	public static readonly EffectorValues POSITIVE_DECOR_EFFECT = DECOR.BONUS.TIER3;

	// Token: 0x0400042E RID: 1070
	public static readonly EffectorValues NEGATIVE_DECOR_EFFECT = DECOR.PENALTY.TIER3;
}
