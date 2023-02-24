using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200015A RID: 346
public class SwampLilyConfig : IEntityConfig
{
	// Token: 0x060006AD RID: 1709 RVA: 0x0002B03C File Offset: 0x0002923C
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x0002B044 File Offset: 0x00029244
	public GameObject CreatePrefab()
	{
		string text = "SwampLily";
		string text2 = STRINGS.CREATURES.SPECIES.SWAMPLILY.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.SWAMPLILY.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("swamplily_kanim"), "idle_empty", Grid.SceneLayer.BuildingBack, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 328.15f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 258.15f, 308.15f, 358.15f, 448.15f, new SimHashes[] { SimHashes.ChlorineGas }, true, 0f, 0.15f, SwampLilyFlowerConfig.ID, true, true, true, true, 2400f, 0f, 4600f, SwampLilyConfig.ID + "Original", STRINGS.CREATURES.SPECIES.SWAMPLILY.NAME);
		gameObject.AddOrGet<StandardCropPlant>();
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Harvest, "SwampLilySeed", STRINGS.CREATURES.SPECIES.SEEDS.SWAMPLILY.NAME, STRINGS.CREATURES.SPECIES.SEEDS.SWAMPLILY.DESC, Assets.GetAnim("seed_swampLily_kanim"), "object", 1, new List<Tag> { GameTags.CropSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 21, STRINGS.CREATURES.SPECIES.SWAMPLILY.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false), SwampLilyConfig.ID + "_preview", Assets.GetAnim("swamplily_kanim"), "place", 1, 2);
		SoundEventVolumeCache.instance.AddVolume("swamplily_kanim", "SwampLily_grow", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("swamplily_kanim", "SwampLily_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("swamplily_kanim", "SwampLily_death", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("swamplily_kanim", "SwampLily_death_bloom", NOISE_POLLUTION.CREATURES.TIER3);
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.HarvestableIDs, SwampLilyConfig.ID);
		return gameObject;
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x0002B220 File Offset: 0x00029420
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x0002B222 File Offset: 0x00029422
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000497 RID: 1175
	public static string ID = "SwampLily";

	// Token: 0x04000498 RID: 1176
	public const string SEED_ID = "SwampLilySeed";
}
