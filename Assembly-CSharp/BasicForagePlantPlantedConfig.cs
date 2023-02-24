using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000136 RID: 310
public class BasicForagePlantPlantedConfig : IEntityConfig
{
	// Token: 0x060005F1 RID: 1521 RVA: 0x00026A10 File Offset: 0x00024C10
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x00026A18 File Offset: 0x00024C18
	public GameObject CreatePrefab()
	{
		string text = "BasicForagePlantPlanted";
		string text2 = STRINGS.CREATURES.SPECIES.BASICFORAGEPLANTPLANTED.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.BASICFORAGEPLANTPLANTED.DESC;
		float num = 100f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("muckroot_kanim"), "idle", Grid.SceneLayer.BuildingBack, 1, 1, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		gameObject.AddOrGet<SimTemperatureTransfer>();
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<EntombVulnerable>();
		gameObject.AddOrGet<DrowningMonitor>();
		gameObject.AddOrGet<Prioritizable>();
		gameObject.AddOrGet<Uprootable>();
		gameObject.AddOrGet<UprootedMonitor>();
		gameObject.AddOrGet<Harvestable>();
		gameObject.AddOrGet<HarvestDesignatable>();
		gameObject.AddOrGet<SeedProducer>().Configure("BasicForagePlant", SeedProducer.ProductionType.DigOnly, 1);
		gameObject.AddOrGet<BasicForagePlantPlanted>();
		gameObject.AddOrGet<KBatchedAnimController>().randomiseLoopedOffset = true;
		return gameObject;
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x00026AEF File Offset: 0x00024CEF
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x00026AF1 File Offset: 0x00024CF1
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000409 RID: 1033
	public const string ID = "BasicForagePlantPlanted";
}
