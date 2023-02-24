using System;
using TUNING;
using UnityEngine;

// Token: 0x020002FC RID: 764
public class SaunaConfig : IBuildingConfig
{
	// Token: 0x06000F3D RID: 3901 RVA: 0x00052FF0 File Offset: 0x000511F0
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Sauna";
		int num = 3;
		int num2 = 3;
		string text2 = "sauna_kanim";
		int num3 = 30;
		float num4 = 60f;
		float[] array = new float[] { 100f, 100f };
		string[] array2 = new string[] { "Metal", "BuildingWood" };
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER2, none, 0.2f);
		buildingDef.ViewMode = OverlayModes.GasConduits.ID;
		buildingDef.Floodable = true;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = true;
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.UtilityInputOffset = new CellOffset(-1, 0);
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.UtilityOutputOffset = new CellOffset(1, 0);
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(0, 2);
		buildingDef.EnergyConsumptionWhenActive = 60f;
		buildingDef.SelfHeatKilowattsWhenActive = 0.5f;
		return buildingDef;
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x000530D0 File Offset: 0x000512D0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.RecBuilding, false);
		go.AddOrGet<Storage>().SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Gas;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.Steam).tag;
		conduitConsumer.capacityKG = 50f;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		conduitConsumer.alwaysConsume = true;
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Liquid;
		conduitDispenser.elementFilter = new SimHashes[] { SimHashes.Water };
		go.AddOrGet<SaunaWorkable>().basePriority = RELAXATION.PRIORITY.TIER3;
		Sauna sauna = go.AddOrGet<Sauna>();
		sauna.steamPerUseKG = 25f;
		sauna.waterOutputTemp = 353.15f;
		sauna.specificEffect = "Sauna";
		sauna.trackingEffect = "RecentlySauna";
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.RecRoom.Id;
		roomTracker.requirement = RoomTracker.Requirement.Recommended;
		go.AddOrGetDef<RocketUsageRestriction.Def>();
	}

	// Token: 0x06000F3F RID: 3903 RVA: 0x000531C6 File Offset: 0x000513C6
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<RequireInputs>().requireConduitHasMass = false;
	}

	// Token: 0x04000874 RID: 2164
	public const string ID = "Sauna";

	// Token: 0x04000875 RID: 2165
	private const float STEAM_PER_USE_KG = 25f;

	// Token: 0x04000876 RID: 2166
	private const float WATER_OUTPUT_TEMP = 353.15f;
}
