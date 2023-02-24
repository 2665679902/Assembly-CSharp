using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000150 RID: 336
public class PrickleFlowerConfig : IEntityConfig
{
	// Token: 0x06000677 RID: 1655 RVA: 0x00029F70 File Offset: 0x00028170
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x00029F78 File Offset: 0x00028178
	public GameObject CreatePrefab()
	{
		string text = "PrickleFlower";
		string text2 = STRINGS.CREATURES.SPECIES.PRICKLEFLOWER.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.PRICKLEFLOWER.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("bristleblossom_kanim"), "idle_empty", Grid.SceneLayer.BuildingFront, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 218.15f, 278.15f, 303.15f, 398.15f, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		}, true, 0f, 0.15f, PrickleFruitConfig.ID, true, true, true, true, 2400f, 0f, 4600f, "PrickleFlowerOriginal", STRINGS.CREATURES.SPECIES.PRICKLEFLOWER.NAME);
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = GameTags.Water,
				massConsumptionRate = 0.033333335f
			}
		});
		gameObject.AddOrGet<StandardCropPlant>();
		DiseaseDropper.Def def = gameObject.AddOrGetDef<DiseaseDropper.Def>();
		def.diseaseIdx = Db.Get().Diseases.GetIndex(Db.Get().Diseases.PollenGerms.id);
		def.singleEmitQuantity = 1000000;
		Modifiers component = gameObject.GetComponent<Modifiers>();
		Db.Get().traits.Get(component.initialTraits[0]).Add(new AttributeModifier(Db.Get().PlantAttributes.MinLightLux.Id, 200f, STRINGS.CREATURES.SPECIES.PRICKLEFLOWER.NAME, false, false, true));
		component.initialAttributes.Add(Db.Get().PlantAttributes.MinLightLux.Id);
		gameObject.AddOrGet<IlluminationVulnerable>().SetPrefersDarkness(false);
		gameObject.AddOrGet<BlightVulnerable>();
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Harvest, "PrickleFlowerSeed", STRINGS.CREATURES.SPECIES.SEEDS.PRICKLEFLOWER.NAME, STRINGS.CREATURES.SPECIES.SEEDS.PRICKLEFLOWER.DESC, Assets.GetAnim("seed_bristleblossom_kanim"), "object", 0, new List<Tag> { GameTags.CropSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 2, STRINGS.CREATURES.SPECIES.PRICKLEFLOWER.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.25f, 0.25f, null, "", false), "PrickleFlower_preview", Assets.GetAnim("bristleblossom_kanim"), "place", 1, 2);
		SoundEventVolumeCache.instance.AddVolume("bristleblossom_kanim", "PrickleFlower_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("bristleblossom_kanim", "PrickleFlower_grow", NOISE_POLLUTION.CREATURES.TIER3);
		return gameObject;
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x0002A1F5 File Offset: 0x000283F5
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<PrimaryElement>().Temperature = 288.15f;
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x0002A207 File Offset: 0x00028407
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000473 RID: 1139
	public const float WATER_RATE = 0.033333335f;

	// Token: 0x04000474 RID: 1140
	public const string ID = "PrickleFlower";

	// Token: 0x04000475 RID: 1141
	public const string SEED_ID = "PrickleFlowerSeed";
}
