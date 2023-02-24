using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x0200032D RID: 813
public class SugarEngineConfig : IBuildingConfig
{
	// Token: 0x06001037 RID: 4151 RVA: 0x000579D5 File Offset: 0x00055BD5
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06001038 RID: 4152 RVA: 0x000579DC File Offset: 0x00055BDC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SugarEngine";
		int num = 3;
		int num2 = 3;
		string text2 = "rocket_sugar_engine_kanim";
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
		buildingDef.InputConduitType = ConduitType.None;
		buildingDef.GeneratorWattageRating = 60f;
		buildingDef.GeneratorBaseCapacity = 2000f;
		buildingDef.RequiresPowerInput = false;
		buildingDef.RequiresPowerOutput = false;
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		return buildingDef;
	}

	// Token: 0x06001039 RID: 4153 RVA: 0x00057AA0 File Offset: 0x00055CA0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 3), GameTags.Rocket, null)
		};
	}

	// Token: 0x0600103A RID: 4154 RVA: 0x00057B04 File Offset: 0x00055D04
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x0600103B RID: 4155 RVA: 0x00057B06 File Offset: 0x00055D06
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x0600103C RID: 4156 RVA: 0x00057B08 File Offset: 0x00055D08
	public override void DoPostConfigureComplete(GameObject go)
	{
		RocketEngineCluster rocketEngineCluster = go.AddOrGet<RocketEngineCluster>();
		rocketEngineCluster.maxModules = 5;
		rocketEngineCluster.maxHeight = ROCKETRY.ROCKET_HEIGHT.SHORT;
		rocketEngineCluster.fuelTag = SimHashes.Sucrose.CreateTag();
		rocketEngineCluster.efficiency = ROCKETRY.ENGINE_EFFICIENCY.STRONG;
		rocketEngineCluster.explosionEffectHash = SpawnFXHashes.MeteorImpactDust;
		rocketEngineCluster.requireOxidizer = true;
		rocketEngineCluster.exhaustElement = SimHashes.CarbonDioxide;
		go.AddOrGet<ModuleGenerator>();
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 450f;
		storage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
		{
			Storage.StoredItemModifier.Hide,
			Storage.StoredItemModifier.Seal,
			Storage.StoredItemModifier.Insulate
		});
		FuelTank fuelTank = go.AddOrGet<FuelTank>();
		fuelTank.consumeFuelOnLand = false;
		fuelTank.storage = storage;
		fuelTank.FuelType = SimHashes.Sucrose.CreateTag();
		fuelTank.targetFillMass = storage.capacityKg;
		fuelTank.physicalFuelCapacity = storage.capacityKg;
		go.AddOrGet<CopyBuildingSettings>();
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = ElementLoader.FindElementByHash(SimHashes.Sucrose).tag;
		manualDeliveryKG.refillMass = storage.capacityKg;
		manualDeliveryKG.capacity = storage.capacityKg;
		manualDeliveryKG.operationalRequirement = Operational.State.None;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.INSIGNIFICANT, (float)ROCKETRY.ENGINE_POWER.EARLY_WEAK, SugarEngineConfig.FUEL_EFFICIENCY);
		go.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject inst)
		{
		};
	}

	// Token: 0x040008D9 RID: 2265
	public const string ID = "SugarEngine";

	// Token: 0x040008DA RID: 2266
	public const SimHashes FUEL = SimHashes.Sucrose;

	// Token: 0x040008DB RID: 2267
	public const float FUEL_CAPACITY = 450f;

	// Token: 0x040008DC RID: 2268
	public static float FUEL_EFFICIENCY = 0.125f;
}
