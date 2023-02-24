using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000143 RID: 323
public class ForestForagePlantPlantedConfig : IEntityConfig
{
	// Token: 0x06000634 RID: 1588 RVA: 0x000280C4 File Offset: 0x000262C4
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x000280CC File Offset: 0x000262CC
	public GameObject CreatePrefab()
	{
		string text = "ForestForagePlantPlanted";
		string text2 = STRINGS.CREATURES.SPECIES.FORESTFORAGEPLANTPLANTED.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.FORESTFORAGEPLANTPLANTED.DESC;
		float num = 100f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("podmelon_kanim"), "idle", Grid.SceneLayer.BuildingBack, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		gameObject.AddOrGet<SimTemperatureTransfer>();
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<EntombVulnerable>();
		gameObject.AddOrGet<DrowningMonitor>();
		gameObject.AddOrGet<Prioritizable>();
		gameObject.AddOrGet<Uprootable>();
		gameObject.AddOrGet<UprootedMonitor>();
		gameObject.AddOrGet<Harvestable>();
		gameObject.AddOrGet<HarvestDesignatable>();
		gameObject.AddOrGet<SeedProducer>().Configure("ForestForagePlant", SeedProducer.ProductionType.DigOnly, 1);
		gameObject.AddOrGet<BasicForagePlantPlanted>();
		gameObject.AddOrGet<KBatchedAnimController>().randomiseLoopedOffset = true;
		return gameObject;
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x000281A3 File Offset: 0x000263A3
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x000281A5 File Offset: 0x000263A5
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000439 RID: 1081
	public const string ID = "ForestForagePlantPlanted";
}
