using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x0200031F RID: 799
public class SpiceGrinderConfig : IBuildingConfig
{
	// Token: 0x06000FE8 RID: 4072 RVA: 0x0005624C File Offset: 0x0005444C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SpiceGrinder";
		int num = 2;
		int num2 = 3;
		string text2 = "spice_grinder_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.ViewMode = OverlayModes.Rooms.ID;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		return buildingDef;
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x000562CC File Offset: 0x000544CC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.SpiceStation, false);
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x000562E8 File Offset: 0x000544E8
	public override void DoPostConfigureComplete(GameObject go)
	{
		SpiceGrinder.InitializeSpices();
		SymbolOverrideControllerUtil.AddToPrefab(go);
		go.AddOrGetDef<SpiceGrinder.Def>();
		go.AddOrGet<SpiceGrinderWorkable>();
		go.AddOrGet<LogicOperationalController>();
		go.AddOrGet<TreeFilterable>().uiHeight = TreeFilterable.UISideScreenHeight.Short;
		go.AddOrGet<Prioritizable>().SetMasterPriority(new PrioritySetting(PriorityScreen.PriorityClass.basic, SpiceGrinderConfig.STORAGE_PRIORITY));
		Storage storage = go.AddComponent<Storage>();
		storage.showInUI = true;
		storage.showDescriptor = true;
		storage.storageFilters = new List<Tag> { GameTags.Edible };
		storage.allowItemRemoval = false;
		storage.capacityKg = 1f;
		storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
		storage.fetchCategory = Storage.FetchCategory.Building;
		storage.showCapacityStatusItem = false;
		storage.allowSettingOnlyFetchMarkedItems = false;
		storage.showSideScreenTitleBar = true;
		Storage storage2 = go.AddComponent<Storage>();
		storage2.showInUI = true;
		storage2.showDescriptor = true;
		storage2.storageFilters = new List<Tag> { GameTags.Seed };
		storage2.allowItemRemoval = false;
		storage2.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
		storage2.fetchCategory = Storage.FetchCategory.Building;
		storage2.showCapacityStatusItem = true;
		storage2.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		ManualDeliveryKG manualDeliveryKG = go.AddComponent<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage2);
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.CookFetch.IdHash;
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.Kitchen.Id;
		roomTracker.requirement = RoomTracker.Requirement.Required;
	}

	// Token: 0x040008A9 RID: 2217
	public const string ID = "SpiceGrinder";

	// Token: 0x040008AA RID: 2218
	public static Tag MATERIAL_FOR_TINKER = GameTags.CropSeed;

	// Token: 0x040008AB RID: 2219
	public static Tag TINKER_TOOLS = FarmStationToolsConfig.tag;

	// Token: 0x040008AC RID: 2220
	public const float MASS_PER_TINKER = 5f;

	// Token: 0x040008AD RID: 2221
	public const float OUTPUT_TEMPERATURE = 313.15f;

	// Token: 0x040008AE RID: 2222
	public const float WORK_TIME_PER_1000KCAL = 5f;

	// Token: 0x040008AF RID: 2223
	public const short SPICE_CAPACITY_PER_INGREDIENT = 10;

	// Token: 0x040008B0 RID: 2224
	public const string PrimaryColorSymbol = "stripe_anim2";

	// Token: 0x040008B1 RID: 2225
	public const string SecondaryColorSymbol = "stripe_anim1";

	// Token: 0x040008B2 RID: 2226
	public const string GrinderColorSymbol = "grinder";

	// Token: 0x040008B3 RID: 2227
	public static StatusItem SpicedStatus = Db.Get().MiscStatusItems.SpicedFood;

	// Token: 0x040008B4 RID: 2228
	private static int STORAGE_PRIORITY = Chore.DefaultPrioritySetting.priority_value - 1;
}
