using System;
using TUNING;
using UnityEngine;

// Token: 0x0200030A RID: 778
public class SodaFountainConfig : IBuildingConfig
{
	// Token: 0x06000F7D RID: 3965 RVA: 0x00054518 File Offset: 0x00052718
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SodaFountain";
		int num = 2;
		int num2 = 2;
		string text2 = "sodamaker_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, none, 0.2f);
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.Floodable = true;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = true;
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.UtilityInputOffset = new CellOffset(1, 1);
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(1, 0);
		buildingDef.EnergyConsumptionWhenActive = 480f;
		buildingDef.SelfHeatKilowattsWhenActive = 1f;
		return buildingDef;
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x000545C0 File Offset: 0x000527C0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.RecBuilding, false);
		Storage storage = go.AddOrGet<Storage>();
		storage.SetDefaultStoredItemModifiers(Storage.StandardFabricatorStorage);
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.Water).tag;
		conduitConsumer.capacityKG = 20f;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = SimHashes.CarbonDioxide.CreateTag();
		manualDeliveryKG.capacity = 4f;
		manualDeliveryKG.refillMass = 1f;
		manualDeliveryKG.MinimumMass = 0.5f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
		go.AddOrGet<SodaFountainWorkable>().basePriority = RELAXATION.PRIORITY.TIER5;
		SodaFountain sodaFountain = go.AddOrGet<SodaFountain>();
		sodaFountain.specificEffect = "SodaFountain";
		sodaFountain.trackingEffect = "RecentlyRecDrink";
		sodaFountain.ingredientTag = SimHashes.CarbonDioxide.CreateTag();
		sodaFountain.ingredientMassPerUse = 1f;
		sodaFountain.waterMassPerUse = 5f;
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.RecRoom.Id;
		roomTracker.requirement = RoomTracker.Requirement.Recommended;
		go.AddOrGetDef<RocketUsageRestriction.Def>();
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x000546F8 File Offset: 0x000528F8
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000884 RID: 2180
	public const string ID = "SodaFountain";
}
