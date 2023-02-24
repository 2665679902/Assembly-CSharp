using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x02000326 RID: 806
public class SteamEngineConfig : IBuildingConfig
{
	// Token: 0x06001015 RID: 4117 RVA: 0x00056DBB File Offset: 0x00054FBB
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_VANILLA_ONLY;
	}

	// Token: 0x06001016 RID: 4118 RVA: 0x00056DC4 File Offset: 0x00054FC4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SteamEngine";
		int num = 7;
		int num2 = 5;
		string text2 = "rocket_steam_engine_kanim";
		int num3 = 1000;
		float num4 = 480f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER7;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.UtilityInputOffset = new CellOffset(2, 3);
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.RequiresPowerInput = false;
		buildingDef.CanMove = true;
		return buildingDef;
	}

	// Token: 0x06001017 RID: 4119 RVA: 0x00056E88 File Offset: 0x00055088
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 5), GameTags.Rocket, null)
		};
	}

	// Token: 0x06001018 RID: 4120 RVA: 0x00056EEC File Offset: 0x000550EC
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x06001019 RID: 4121 RVA: 0x00056EEE File Offset: 0x000550EE
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x0600101A RID: 4122 RVA: 0x00056EF0 File Offset: 0x000550F0
	public override void DoPostConfigureComplete(GameObject go)
	{
		RocketEngine rocketEngine = go.AddOrGet<RocketEngine>();
		rocketEngine.fuelTag = ElementLoader.FindElementByHash(SimHashes.Steam).tag;
		rocketEngine.efficiency = ROCKETRY.ENGINE_EFFICIENCY.WEAK;
		rocketEngine.explosionEffectHash = SpawnFXHashes.MeteorImpactDust;
		rocketEngine.requireOxidizer = false;
		rocketEngine.exhaustElement = SimHashes.Steam;
		rocketEngine.exhaustTemperature = ElementLoader.FindElementByHash(SimHashes.Steam).lowTemp + 50f;
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = BUILDINGS.ROCKETRY_MASS_KG.FUEL_TANK_WET_MASS[0];
		storage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
		{
			Storage.StoredItemModifier.Hide,
			Storage.StoredItemModifier.Seal,
			Storage.StoredItemModifier.Insulate
		});
		FuelTank fuelTank = go.AddOrGet<FuelTank>();
		fuelTank.consumeFuelOnLand = !DlcManager.FeatureClusterSpaceEnabled();
		fuelTank.storage = storage;
		fuelTank.FuelType = ElementLoader.FindElementByHash(SimHashes.Steam).tag;
		fuelTank.physicalFuelCapacity = storage.capacityKg;
		go.AddOrGet<CopyBuildingSettings>();
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Gas;
		conduitConsumer.consumptionRate = 10f;
		conduitConsumer.capacityTag = fuelTank.FuelType;
		conduitConsumer.capacityKG = storage.capacityKg;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		BuildingTemplates.ExtendBuildingToRocketModule(go, "rocket_steam_engine_bg_kanim", false);
	}

	// Token: 0x040008C7 RID: 2247
	public const string ID = "SteamEngine";
}
