using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x020001DB RID: 475
public class KeroseneEngineClusterSmallConfig : IBuildingConfig
{
	// Token: 0x0600095B RID: 2395 RVA: 0x000364A8 File Offset: 0x000346A8
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x000364B0 File Offset: 0x000346B0
	public override BuildingDef CreateBuildingDef()
	{
		string text = "KeroseneEngineClusterSmall";
		int num = 3;
		int num2 = 4;
		string text2 = "rocket_petro_engine_small_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] engine_MASS_SMALL = BUILDINGS.ROCKETRY_MASS_KG.ENGINE_MASS_SMALL;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, engine_MASS_SMALL, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.UtilityInputOffset = new CellOffset(0, 2);
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.GeneratorWattageRating = 240f;
		buildingDef.GeneratorBaseCapacity = 4000f;
		buildingDef.RequiresPowerInput = false;
		buildingDef.RequiresPowerOutput = false;
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x00036588 File Offset: 0x00034788
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 4), GameTags.Rocket, null)
		};
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x000365EC File Offset: 0x000347EC
	public override void DoPostConfigureComplete(GameObject go)
	{
		RocketEngineCluster rocketEngineCluster = go.AddOrGet<RocketEngineCluster>();
		rocketEngineCluster.maxModules = 4;
		rocketEngineCluster.maxHeight = ROCKETRY.ROCKET_HEIGHT.MEDIUM;
		rocketEngineCluster.fuelTag = SimHashes.Petroleum.CreateTag();
		rocketEngineCluster.efficiency = ROCKETRY.ENGINE_EFFICIENCY.MEDIUM;
		rocketEngineCluster.requireOxidizer = true;
		rocketEngineCluster.explosionEffectHash = SpawnFXHashes.MeteorImpactDust;
		rocketEngineCluster.exhaustElement = SimHashes.CarbonDioxide;
		rocketEngineCluster.exhaustTemperature = 1263.15f;
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
		fuelTank.FuelType = SimHashes.Petroleum.CreateTag();
		fuelTank.targetFillMass = storage.capacityKg;
		fuelTank.physicalFuelCapacity = storage.capacityKg;
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MODERATE_PLUS, (float)ROCKETRY.ENGINE_POWER.MID_STRONG, ROCKETRY.FUEL_COST_PER_DISTANCE.MEDIUM);
		go.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject inst)
		{
		};
	}

	// Token: 0x040005D8 RID: 1496
	public const string ID = "KeroseneEngineClusterSmall";

	// Token: 0x040005D9 RID: 1497
	public const SimHashes FUEL = SimHashes.Petroleum;

	// Token: 0x040005DA RID: 1498
	public const float FUEL_CAPACITY = 450f;
}
