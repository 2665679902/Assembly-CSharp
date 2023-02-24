using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000158 RID: 344
public class SwampForagePlantPlantedConfig : IEntityConfig
{
	// Token: 0x060006A3 RID: 1699 RVA: 0x0002AD9C File Offset: 0x00028F9C
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x0002ADA4 File Offset: 0x00028FA4
	public GameObject CreatePrefab()
	{
		string text = "SwampForagePlantPlanted";
		string text2 = STRINGS.CREATURES.SPECIES.SWAMPFORAGEPLANTPLANTED.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.SWAMPFORAGEPLANTPLANTED.DESC;
		float num = 100f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("swamptuber_kanim"), "idle", Grid.SceneLayer.BuildingBack, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		gameObject.AddOrGet<SimTemperatureTransfer>();
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<EntombVulnerable>();
		gameObject.AddOrGet<Prioritizable>();
		gameObject.AddOrGet<Uprootable>();
		gameObject.AddOrGet<UprootedMonitor>();
		gameObject.AddOrGet<Harvestable>();
		gameObject.AddOrGet<HarvestDesignatable>();
		gameObject.AddOrGet<SeedProducer>().Configure("SwampForagePlant", SeedProducer.ProductionType.DigOnly, 1);
		gameObject.AddOrGet<BasicForagePlantPlanted>();
		gameObject.AddOrGet<KBatchedAnimController>().randomiseLoopedOffset = true;
		return gameObject;
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x0002AE74 File Offset: 0x00029074
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x0002AE76 File Offset: 0x00029076
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000493 RID: 1171
	public const string ID = "SwampForagePlantPlanted";
}
