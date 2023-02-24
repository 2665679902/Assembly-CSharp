﻿using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200032B RID: 811
public class StorageLockerSmartConfig : IBuildingConfig
{
	// Token: 0x0600102F RID: 4143 RVA: 0x00057690 File Offset: 0x00055890
	public override BuildingDef CreateBuildingDef()
	{
		string text = "StorageLockerSmart";
		int num = 1;
		int num2 = 2;
		string text2 = "smartstoragelocker_kanim";
		int num3 = 30;
		float num4 = 60f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Logic.ID;
		buildingDef.RequiresPowerInput = true;
		buildingDef.AddLogicPowerPort = false;
		buildingDef.EnergyConsumptionWhenActive = 60f;
		buildingDef.ExhaustKilowattsWhenActive = 0.125f;
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort(FilteredStorage.FULL_PORT_ID, new CellOffset(0, 1), STRINGS.BUILDINGS.PREFABS.STORAGELOCKERSMART.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.STORAGELOCKERSMART.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.STORAGELOCKERSMART.LOGIC_PORT_INACTIVE, true, false) };
		return buildingDef;
	}

	// Token: 0x06001030 RID: 4144 RVA: 0x00057760 File Offset: 0x00055960
	public override void DoPostConfigureComplete(GameObject go)
	{
		SoundEventVolumeCache.instance.AddVolume("storagelocker_kanim", "StorageLocker_Hit_metallic_low", NOISE_POLLUTION.NOISY.TIER1);
		Prioritizable.AddRef(go);
		Storage storage = go.AddOrGet<Storage>();
		storage.showInUI = true;
		storage.allowItemRemoval = true;
		storage.showDescriptor = true;
		storage.storageFilters = STORAGEFILTERS.NOT_EDIBLE_SOLIDS;
		storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
		storage.fetchCategory = Storage.FetchCategory.GeneralStorage;
		storage.showCapacityStatusItem = true;
		storage.showCapacityAsMainStatus = true;
		go.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.StorageLocker;
		go.AddOrGet<StorageLockerSmart>();
		go.AddOrGet<UserNameable>();
		go.AddOrGetDef<StorageController.Def>();
		go.AddOrGetDef<RocketUsageRestriction.Def>();
	}

	// Token: 0x040008D3 RID: 2259
	public const string ID = "StorageLockerSmart";
}
