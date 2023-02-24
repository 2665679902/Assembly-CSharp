using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200015D RID: 349
public class WineCupsConfig : IEntityConfig
{
	// Token: 0x060006BE RID: 1726 RVA: 0x0002B654 File Offset: 0x00029854
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x0002B65C File Offset: 0x0002985C
	public GameObject CreatePrefab()
	{
		string text = "WineCups";
		string text2 = STRINGS.CREATURES.SPECIES.WINECUPS.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.WINECUPS.DESC;
		float num = 1f;
		EffectorValues positive_DECOR_EFFECT = WineCupsConfig.POSITIVE_DECOR_EFFECT;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("potted_cups_kanim"), "grow_seed", Grid.SceneLayer.BuildingFront, 1, 1, positive_DECOR_EFFECT, default(EffectorValues), SimHashes.Creature, null, 293f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 218.15f, 283.15f, 303.15f, 398.15f, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		}, true, 0f, 0.15f, null, true, false, true, true, 2400f, 0f, 900f, "WineCupsOriginal", STRINGS.CREATURES.SPECIES.WINECUPS.NAME);
		PrickleGrass prickleGrass = gameObject.AddOrGet<PrickleGrass>();
		prickleGrass.positive_decor_effect = WineCupsConfig.POSITIVE_DECOR_EFFECT;
		prickleGrass.negative_decor_effect = WineCupsConfig.NEGATIVE_DECOR_EFFECT;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Hidden, "WineCupsSeed", STRINGS.CREATURES.SPECIES.SEEDS.WINECUPS.NAME, STRINGS.CREATURES.SPECIES.SEEDS.WINECUPS.DESC, Assets.GetAnim("seed_potted_cups_kanim"), "object", 1, new List<Tag> { GameTags.DecorSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 11, STRINGS.CREATURES.SPECIES.WINECUPS.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.25f, 0.25f, null, "", false), "WineCups_preview", Assets.GetAnim("potted_cups_kanim"), "place", 1, 1);
		return gameObject;
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x0002B7C4 File Offset: 0x000299C4
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x0002B7C6 File Offset: 0x000299C6
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004A4 RID: 1188
	public const string ID = "WineCups";

	// Token: 0x040004A5 RID: 1189
	public const string SEED_ID = "WineCupsSeed";

	// Token: 0x040004A6 RID: 1190
	public static readonly EffectorValues POSITIVE_DECOR_EFFECT = DECOR.BONUS.TIER3;

	// Token: 0x040004A7 RID: 1191
	public static readonly EffectorValues NEGATIVE_DECOR_EFFECT = DECOR.PENALTY.TIER3;
}
