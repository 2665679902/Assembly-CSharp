using System;
using TUNING;
using UnityEngine;

// Token: 0x02000333 RID: 819
public class SweepBotStationConfig : IBuildingConfig
{
	// Token: 0x06001055 RID: 4181 RVA: 0x00058F4C File Offset: 0x0005714C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SweepBotStation";
		int num = 2;
		int num2 = 2;
		string text2 = "sweep_bot_base_station_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] array = new float[] { BUILDINGS.CONSTRUCTION_MASS_KG.TIER2[0] - SweepBotConfig.MASS };
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.Floodable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 240f;
		buildingDef.ExhaustKilowattsWhenActive = 0f;
		buildingDef.SelfHeatKilowattsWhenActive = 1f;
		return buildingDef;
	}

	// Token: 0x06001056 RID: 4182 RVA: 0x00058FE8 File Offset: 0x000571E8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Prioritizable.AddRef(go);
		Storage storage = go.AddComponent<Storage>();
		storage.showInUI = true;
		storage.allowItemRemoval = false;
		storage.ignoreSourcePriority = true;
		storage.showDescriptor = false;
		storage.storageFilters = STORAGEFILTERS.NOT_EDIBLE_SOLIDS;
		storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
		storage.fetchCategory = Storage.FetchCategory.Building;
		storage.capacityKg = 25f;
		storage.allowClearable = false;
		Storage storage2 = go.AddComponent<Storage>();
		storage2.showInUI = true;
		storage2.allowItemRemoval = true;
		storage2.ignoreSourcePriority = true;
		storage2.showDescriptor = true;
		storage2.storageFilters = STORAGEFILTERS.NOT_EDIBLE_SOLIDS;
		storage2.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
		storage2.fetchCategory = Storage.FetchCategory.StorageSweepOnly;
		storage2.capacityKg = 1000f;
		storage2.allowClearable = true;
		storage2.showCapacityStatusItem = true;
		go.AddOrGet<CharacterOverlay>().shouldShowName = true;
		go.AddOrGet<SweepBotStation>().SetStorages(storage, storage2);
	}

	// Token: 0x06001057 RID: 4183 RVA: 0x000590BF File Offset: 0x000572BF
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<StorageController.Def>();
	}

	// Token: 0x040008E5 RID: 2277
	public const string ID = "SweepBotStation";

	// Token: 0x040008E6 RID: 2278
	public const float POWER_USAGE = 240f;
}
