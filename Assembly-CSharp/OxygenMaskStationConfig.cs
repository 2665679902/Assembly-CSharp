using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x0200029A RID: 666
public class OxygenMaskStationConfig : IBuildingConfig
{
	// Token: 0x06000D42 RID: 3394 RVA: 0x0004994C File Offset: 0x00047B4C
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x00049954 File Offset: 0x00047B54
	public override BuildingDef CreateBuildingDef()
	{
		string text = "OxygenMaskStation";
		int num = 2;
		int num2 = 3;
		string text2 = "oxygen_mask_station_kanim";
		int num3 = 30;
		float num4 = 30f;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] array = raw_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, array, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 60f;
		buildingDef.ExhaustKilowattsWhenActive = 0.5f;
		buildingDef.SelfHeatKilowattsWhenActive = 1f;
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.PreventIdleTraversalPastBuilding = true;
		buildingDef.Deprecated = true;
		return buildingDef;
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x000499DC File Offset: 0x00047BDC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Storage storage = go.AddComponent<Storage>();
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		storage.showInUI = true;
		storage.storageFilters = new List<Tag> { GameTags.Metal };
		storage.capacityKg = 45f;
		Storage storage2 = go.AddComponent<Storage>();
		storage2.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		storage2.showInUI = true;
		storage2.storageFilters = new List<Tag> { GameTags.Breathable };
		MaskStation maskStation = go.AddOrGet<MaskStation>();
		maskStation.materialConsumedPerMask = 15f;
		maskStation.oxygenConsumedPerMask = 20f;
		maskStation.maxUses = 3;
		maskStation.materialTag = GameTags.Metal;
		maskStation.oxygenTag = GameTags.Breathable;
		maskStation.choreTypeID = this.fetchChoreType.Id;
		maskStation.PathFlag = PathFinder.PotentialPath.Flags.HasOxygenMask;
		maskStation.materialStorage = storage;
		maskStation.oxygenStorage = storage2;
		ElementConsumer elementConsumer = go.AddOrGet<ElementConsumer>();
		elementConsumer.elementToConsume = SimHashes.Oxygen;
		elementConsumer.configuration = ElementConsumer.Configuration.AllGas;
		elementConsumer.consumptionRate = 0.5f;
		elementConsumer.storeOnConsume = true;
		elementConsumer.showInStatusPanel = false;
		elementConsumer.consumptionRadius = 2;
		elementConsumer.storage = storage2;
		ElementConsumer elementConsumer2 = go.AddComponent<ElementConsumer>();
		elementConsumer2.elementToConsume = SimHashes.ContaminatedOxygen;
		elementConsumer2.configuration = ElementConsumer.Configuration.AllGas;
		elementConsumer2.consumptionRate = 0.5f;
		elementConsumer2.storeOnConsume = true;
		elementConsumer2.showInStatusPanel = false;
		elementConsumer2.consumptionRadius = 2;
		elementConsumer2.storage = storage2;
		Prioritizable.AddRef(go);
		go.AddOrGet<LoopingSounds>();
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x00049B39 File Offset: 0x00047D39
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040007AB RID: 1963
	public const string ID = "OxygenMaskStation";

	// Token: 0x040007AC RID: 1964
	public const float MATERIAL_PER_MASK = 15f;

	// Token: 0x040007AD RID: 1965
	public const float OXYGEN_PER_MASK = 20f;

	// Token: 0x040007AE RID: 1966
	public const int MASKS_PER_REFILL = 3;

	// Token: 0x040007AF RID: 1967
	public const float WORK_TIME = 5f;

	// Token: 0x040007B0 RID: 1968
	public ChoreType fetchChoreType = Db.Get().ChoreTypes.Fetch;
}
