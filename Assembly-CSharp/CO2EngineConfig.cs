using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class CO2EngineConfig : IBuildingConfig
{
	// Token: 0x060000DA RID: 218 RVA: 0x00007070 File Offset: 0x00005270
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00007078 File Offset: 0x00005278
	public override BuildingDef CreateBuildingDef()
	{
		string text = "CO2Engine";
		int num = 3;
		int num2 = 2;
		string text2 = "rocket_co2_engine_kanim";
		int num3 = 1000;
		float num4 = 30f;
		float[] dense_TIER = BUILDINGS.ROCKETRY_MASS_KG.DENSE_TIER1;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, dense_TIER, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.UtilityInputOffset = new CellOffset(0, 1);
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.RequiresPowerInput = false;
		buildingDef.RequiresPowerOutput = false;
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		return buildingDef;
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00007130 File Offset: 0x00005330
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 2), GameTags.Rocket, null)
		};
	}

	// Token: 0x060000DD RID: 221 RVA: 0x00007194 File Offset: 0x00005394
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00007196 File Offset: 0x00005396
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x060000DF RID: 223 RVA: 0x00007198 File Offset: 0x00005398
	public override void DoPostConfigureComplete(GameObject go)
	{
		RocketEngineCluster rocketEngineCluster = go.AddOrGet<RocketEngineCluster>();
		rocketEngineCluster.maxModules = 3;
		rocketEngineCluster.maxHeight = ROCKETRY.ROCKET_HEIGHT.VERY_SHORT;
		rocketEngineCluster.fuelTag = SimHashes.CarbonDioxide.CreateTag();
		rocketEngineCluster.efficiency = ROCKETRY.ENGINE_EFFICIENCY.WEAK;
		rocketEngineCluster.explosionEffectHash = SpawnFXHashes.MeteorImpactDust;
		rocketEngineCluster.requireOxidizer = false;
		rocketEngineCluster.exhaustElement = SimHashes.CarbonDioxide;
		rocketEngineCluster.exhaustTemperature = ElementLoader.FindElementByHash(SimHashes.CarbonDioxide).lowTemp + 20f;
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = BUILDINGS.ROCKETRY_MASS_KG.FUEL_TANK_WET_MASS_GAS[0];
		storage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
		{
			Storage.StoredItemModifier.Hide,
			Storage.StoredItemModifier.Seal,
			Storage.StoredItemModifier.Insulate
		});
		FuelTank fuelTank = go.AddOrGet<FuelTank>();
		fuelTank.consumeFuelOnLand = false;
		fuelTank.storage = storage;
		fuelTank.FuelType = SimHashes.CarbonDioxide.CreateTag();
		fuelTank.targetFillMass = storage.capacityKg;
		fuelTank.physicalFuelCapacity = storage.capacityKg;
		go.AddOrGet<CopyBuildingSettings>();
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Gas;
		conduitConsumer.consumptionRate = 10f;
		conduitConsumer.capacityTag = fuelTank.FuelType;
		conduitConsumer.capacityKG = storage.capacityKg;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MINOR_PLUS, (float)ROCKETRY.ENGINE_POWER.EARLY_STRONG, ROCKETRY.FUEL_COST_PER_DISTANCE.GAS_LOW);
		go.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject inst)
		{
		};
	}

	// Token: 0x04000082 RID: 130
	public const string ID = "CO2Engine";

	// Token: 0x04000083 RID: 131
	public const SimHashes FUEL = SimHashes.CarbonDioxide;
}
