using System;
using TUNING;
using UnityEngine;

// Token: 0x020001E8 RID: 488
public class LiquidCargoBayClusterConfig : IBuildingConfig
{
	// Token: 0x060009A3 RID: 2467 RVA: 0x00037EEA File Offset: 0x000360EA
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x00037EF4 File Offset: 0x000360F4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LiquidCargoBayCluster";
		int num = 5;
		int num2 = 5;
		string text2 = "rocket_cluster_storage_liquid_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] cargo_MASS = BUILDINGS.ROCKETRY_MASS_KG.CARGO_MASS;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, cargo_MASS, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.RequiresPowerInput = false;
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x00037FB0 File Offset: 0x000361B0
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

	// Token: 0x060009A6 RID: 2470 RVA: 0x00038014 File Offset: 0x00036214
	public override void DoPostConfigureComplete(GameObject go)
	{
		go = BuildingTemplates.ExtendBuildingToClusterCargoBay(go, this.CAPACITY, STORAGEFILTERS.LIQUIDS, CargoBay.CargoType.Liquids);
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MODERATE_PLUS, 0f, 0f);
	}

	// Token: 0x040005F1 RID: 1521
	public const string ID = "LiquidCargoBayCluster";

	// Token: 0x040005F2 RID: 1522
	public float CAPACITY = ROCKETRY.LIQUID_CARGO_BAY_CLUSTER_CAPACITY * ROCKETRY.CARGO_CAPACITY_SCALE;
}
