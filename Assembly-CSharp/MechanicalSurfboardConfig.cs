using System;
using TUNING;
using UnityEngine;

// Token: 0x0200022E RID: 558
public class MechanicalSurfboardConfig : IBuildingConfig
{
	// Token: 0x06000B02 RID: 2818 RVA: 0x0003E1B4 File Offset: 0x0003C3B4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "MechanicalSurfboard";
		int num = 2;
		int num2 = 3;
		string text2 = "mechanical_surfboard_kanim";
		int num3 = 30;
		float num4 = 60f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, none, 0.2f);
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.Floodable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = true;
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.UtilityInputOffset = new CellOffset(1, 0);
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(0, 0);
		buildingDef.EnergyConsumptionWhenActive = 480f;
		buildingDef.SelfHeatKilowattsWhenActive = 1f;
		return buildingDef;
	}

	// Token: 0x06000B03 RID: 2819 RVA: 0x0003E25C File Offset: 0x0003C45C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.RecBuilding, false);
		go.AddOrGet<Storage>().SetDefaultStoredItemModifiers(Storage.StandardFabricatorStorage);
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.Water).tag;
		conduitConsumer.capacityKG = 20f;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		go.AddOrGet<MechanicalSurfboardWorkable>().basePriority = RELAXATION.PRIORITY.TIER3;
		MechanicalSurfboard mechanicalSurfboard = go.AddOrGet<MechanicalSurfboard>();
		mechanicalSurfboard.waterSpillRateKG = 0.05f;
		mechanicalSurfboard.minOperationalWaterKG = 2f;
		mechanicalSurfboard.specificEffect = "MechanicalSurfboard";
		mechanicalSurfboard.trackingEffect = "RecentlyMechanicalSurfboard";
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.RecRoom.Id;
		roomTracker.requirement = RoomTracker.Requirement.Recommended;
		go.AddOrGetDef<RocketUsageRestriction.Def>();
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x0003E32B File Offset: 0x0003C52B
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000676 RID: 1654
	public const string ID = "MechanicalSurfboard";

	// Token: 0x04000677 RID: 1655
	private const float TANK_SIZE_KG = 20f;

	// Token: 0x04000678 RID: 1656
	private const float SPILL_RATE_KG = 0.05f;
}
