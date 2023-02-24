using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000146 RID: 326
public class GasGrassConfig : IEntityConfig
{
	// Token: 0x06000643 RID: 1603 RVA: 0x00028487 File Offset: 0x00026687
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x00028490 File Offset: 0x00026690
	public GameObject CreatePrefab()
	{
		string text = "GasGrass";
		string text2 = STRINGS.CREATURES.SPECIES.GASGRASS.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.GASGRASS.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.BONUS.TIER3;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gassygrass_kanim"), "idle_empty", Grid.SceneLayer.BuildingFront, 1, 3, tier, default(EffectorValues), SimHashes.Creature, null, 255f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 218.15f, 0f, 348.15f, 373.15f, null, true, 0f, 0.15f, "GasGrassHarvested", true, true, true, true, 2400f, 0f, 12200f, "GasGrassOriginal", STRINGS.CREATURES.SPECIES.GASGRASS.NAME);
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = GameTags.Chlorine,
				massConsumptionRate = 0.00083333335f
			}
		});
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<HarvestDesignatable>().defaultHarvestStateWhenPlanted = false;
		Modifiers component = gameObject.GetComponent<Modifiers>();
		Db.Get().traits.Get(component.initialTraits[0]).Add(new AttributeModifier(Db.Get().PlantAttributes.MinLightLux.Id, 20000f, STRINGS.CREATURES.SPECIES.GASGRASS.NAME, false, false, true));
		component.initialAttributes.Add(Db.Get().PlantAttributes.MinLightLux.Id);
		gameObject.AddOrGetDef<CropSleepingMonitor.Def>().prefersDarkness = false;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, DlcManager.FeaturePlantMutationsEnabled() ? SeedProducer.ProductionType.Harvest : SeedProducer.ProductionType.Hidden, "GasGrassSeed", STRINGS.CREATURES.SPECIES.SEEDS.GASGRASS.NAME, STRINGS.CREATURES.SPECIES.SEEDS.GASGRASS.DESC, Assets.GetAnim("seed_gassygrass_kanim"), "object", 1, new List<Tag> { GameTags.CropSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 22, STRINGS.CREATURES.SPECIES.GASGRASS.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.2f, 0.2f, null, "", false), "GasGrass_preview", Assets.GetAnim("gassygrass_kanim"), "place", 1, 1);
		SoundEventVolumeCache.instance.AddVolume("gassygrass_kanim", "GasGrass_grow", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("gassygrass_kanim", "GasGrass_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		return gameObject;
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x000286D4 File Offset: 0x000268D4
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x000286D6 File Offset: 0x000268D6
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000442 RID: 1090
	public const string ID = "GasGrass";

	// Token: 0x04000443 RID: 1091
	public const string SEED_ID = "GasGrassSeed";

	// Token: 0x04000444 RID: 1092
	public const float FERTILIZATION_RATE = 0.00083333335f;
}
