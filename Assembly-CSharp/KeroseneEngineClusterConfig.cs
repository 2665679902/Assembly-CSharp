using System;
using TUNING;
using UnityEngine;

// Token: 0x020001DA RID: 474
public class KeroseneEngineClusterConfig : IBuildingConfig
{
	// Token: 0x06000956 RID: 2390 RVA: 0x000362AA File Offset: 0x000344AA
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x000362B4 File Offset: 0x000344B4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "KeroseneEngineCluster";
		int num = 7;
		int num2 = 5;
		string text2 = "rocket_cluster_petroleum_engine_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] engine_MASS_SMALL = BUILDINGS.ROCKETRY_MASS_KG.ENGINE_MASS_SMALL;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, engine_MASS_SMALL, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.GeneratorWattageRating = 480f;
		buildingDef.GeneratorBaseCapacity = 20000f;
		buildingDef.RequiresPowerInput = false;
		buildingDef.RequiresPowerOutput = false;
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x0003638C File Offset: 0x0003458C
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

	// Token: 0x06000959 RID: 2393 RVA: 0x000363F0 File Offset: 0x000345F0
	public override void DoPostConfigureComplete(GameObject go)
	{
		RocketEngineCluster rocketEngineCluster = go.AddOrGet<RocketEngineCluster>();
		rocketEngineCluster.maxModules = 7;
		rocketEngineCluster.maxHeight = ROCKETRY.ROCKET_HEIGHT.VERY_TALL;
		rocketEngineCluster.fuelTag = SimHashes.Petroleum.CreateTag();
		rocketEngineCluster.efficiency = ROCKETRY.ENGINE_EFFICIENCY.MEDIUM;
		rocketEngineCluster.requireOxidizer = true;
		rocketEngineCluster.explosionEffectHash = SpawnFXHashes.MeteorImpactDust;
		rocketEngineCluster.exhaustElement = SimHashes.CarbonDioxide;
		rocketEngineCluster.exhaustTemperature = 1263.15f;
		go.AddOrGet<ModuleGenerator>();
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MAJOR, (float)ROCKETRY.ENGINE_POWER.MID_VERY_STRONG, ROCKETRY.FUEL_COST_PER_DISTANCE.VERY_HIGH);
		go.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject inst)
		{
		};
	}

	// Token: 0x040005D6 RID: 1494
	public const string ID = "KeroseneEngineCluster";

	// Token: 0x040005D7 RID: 1495
	public const SimHashes FUEL = SimHashes.Petroleum;
}
