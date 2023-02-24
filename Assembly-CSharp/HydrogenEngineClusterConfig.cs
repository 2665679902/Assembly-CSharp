using System;
using TUNING;
using UnityEngine;

// Token: 0x020001C0 RID: 448
public class HydrogenEngineClusterConfig : IBuildingConfig
{
	// Token: 0x060008CA RID: 2250 RVA: 0x0003430A File Offset: 0x0003250A
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x00034314 File Offset: 0x00032514
	public override BuildingDef CreateBuildingDef()
	{
		string text = "HydrogenEngineCluster";
		int num = 7;
		int num2 = 5;
		string text2 = "rocket_cluster_hydrogen_engine_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] engine_MASS_LARGE = BUILDINGS.ROCKETRY_MASS_KG.ENGINE_MASS_LARGE;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, engine_MASS_LARGE, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.GeneratorWattageRating = 600f;
		buildingDef.GeneratorBaseCapacity = 40000f;
		buildingDef.RequiresPowerInput = false;
		buildingDef.RequiresPowerOutput = false;
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x000343EC File Offset: 0x000325EC
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

	// Token: 0x060008CD RID: 2253 RVA: 0x00034450 File Offset: 0x00032650
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x00034452 File Offset: 0x00032652
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x00034454 File Offset: 0x00032654
	public override void DoPostConfigureComplete(GameObject go)
	{
		RocketEngineCluster rocketEngineCluster = go.AddOrGet<RocketEngineCluster>();
		rocketEngineCluster.maxModules = 7;
		rocketEngineCluster.maxHeight = ROCKETRY.ROCKET_HEIGHT.VERY_TALL;
		rocketEngineCluster.fuelTag = ElementLoader.FindElementByHash(SimHashes.LiquidHydrogen).tag;
		rocketEngineCluster.efficiency = ROCKETRY.ENGINE_EFFICIENCY.STRONG;
		rocketEngineCluster.explosionEffectHash = SpawnFXHashes.MeteorImpactDust;
		rocketEngineCluster.exhaustElement = SimHashes.Steam;
		rocketEngineCluster.exhaustTemperature = 2000f;
		go.AddOrGet<ModuleGenerator>();
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MAJOR_PLUS, (float)ROCKETRY.ENGINE_POWER.LATE_VERY_STRONG, ROCKETRY.FUEL_COST_PER_DISTANCE.HIGH);
		go.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject inst)
		{
		};
	}

	// Token: 0x0400058C RID: 1420
	public const string ID = "HydrogenEngineCluster";
}
