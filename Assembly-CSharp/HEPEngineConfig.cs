using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001B7 RID: 439
public class HEPEngineConfig : IBuildingConfig
{
	// Token: 0x06000899 RID: 2201 RVA: 0x00032D02 File Offset: 0x00030F02
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x00032D0C File Offset: 0x00030F0C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "HEPEngine";
		int num = 5;
		int num2 = 5;
		string text2 = "rocket_hep_engine_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] engine_MASS_LARGE = TUNING.BUILDINGS.ROCKETRY_MASS_KG.ENGINE_MASS_LARGE;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, engine_MASS_LARGE, array, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.RequiresPowerInput = false;
		buildingDef.RequiresPowerOutput = false;
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		buildingDef.ShowInBuildMenu = false;
		buildingDef.UseHighEnergyParticleInputPort = true;
		buildingDef.HighEnergyParticleInputOffset = new CellOffset(0, 3);
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort("HEP_STORAGE", new CellOffset(0, 2), STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE, STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE_ACTIVE, STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE_INACTIVE, false, false) };
		return buildingDef;
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x00032E2C File Offset: 0x0003102C
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

	// Token: 0x0600089C RID: 2204 RVA: 0x00032E90 File Offset: 0x00031090
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x00032E92 File Offset: 0x00031092
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x00032E94 File Offset: 0x00031094
	public override void DoPostConfigureComplete(GameObject go)
	{
		RadiationEmitter radiationEmitter = go.AddOrGet<RadiationEmitter>();
		radiationEmitter.emitType = RadiationEmitter.RadiationEmitterType.Constant;
		radiationEmitter.emitRadiusX = 10;
		radiationEmitter.emitRadiusY = 10;
		radiationEmitter.emitRads = 8400f / ((float)radiationEmitter.emitRadiusX / 6f);
		radiationEmitter.emissionOffset = new Vector3(0f, 3f, 0f);
		HighEnergyParticleStorage highEnergyParticleStorage = go.AddOrGet<HighEnergyParticleStorage>();
		highEnergyParticleStorage.capacity = 4000f;
		highEnergyParticleStorage.autoStore = true;
		highEnergyParticleStorage.PORT_ID = "HEP_STORAGE";
		highEnergyParticleStorage.showCapacityStatusItem = true;
		go.AddOrGet<HEPFuelTank>().physicalFuelCapacity = 4000f;
		RocketEngineCluster rocketEngineCluster = go.AddOrGet<RocketEngineCluster>();
		rocketEngineCluster.maxModules = 4;
		rocketEngineCluster.maxHeight = ROCKETRY.ROCKET_HEIGHT.MEDIUM;
		rocketEngineCluster.efficiency = ROCKETRY.ENGINE_EFFICIENCY.STRONG;
		rocketEngineCluster.explosionEffectHash = SpawnFXHashes.MeteorImpactDust;
		rocketEngineCluster.requireOxidizer = false;
		rocketEngineCluster.exhaustElement = SimHashes.Fallout;
		rocketEngineCluster.exhaustTemperature = 873.15f;
		rocketEngineCluster.exhaustEmitRate = 25f;
		rocketEngineCluster.exhaustDiseaseIdx = Db.Get().Diseases.GetIndex(Db.Get().Diseases.RadiationPoisoning.Id);
		rocketEngineCluster.exhaustDiseaseCount = 100000;
		rocketEngineCluster.emitRadiation = true;
		rocketEngineCluster.fuelTag = GameTags.HighEnergyParticle;
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MODERATE_PLUS, (float)ROCKETRY.ENGINE_POWER.LATE_STRONG, ROCKETRY.FUEL_COST_PER_DISTANCE.PARTICLES);
		go.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject inst)
		{
			inst.GetComponent<RadiationEmitter>().SetEmitting(false);
		};
	}

	// Token: 0x04000564 RID: 1380
	private const int PARTICLES_PER_HEX = 200;

	// Token: 0x04000565 RID: 1381
	private const int RANGE = 20;

	// Token: 0x04000566 RID: 1382
	private const int PARTICLE_STORAGE_CAPACITY = 4000;

	// Token: 0x04000567 RID: 1383
	private const int PORT_OFFSET_Y = 3;

	// Token: 0x04000568 RID: 1384
	public const string ID = "HEPEngine";

	// Token: 0x04000569 RID: 1385
	public const string PORT_ID = "HEP_STORAGE";
}
