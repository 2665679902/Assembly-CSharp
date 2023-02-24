using System;
using TUNING;
using UnityEngine;

// Token: 0x02000193 RID: 403
public class GasCargoBayClusterConfig : IBuildingConfig
{
	// Token: 0x060007D2 RID: 2002 RVA: 0x0002DDD9 File Offset: 0x0002BFD9
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x0002DDE0 File Offset: 0x0002BFE0
	public override BuildingDef CreateBuildingDef()
	{
		string text = "GasCargoBayCluster";
		int num = 5;
		int num2 = 5;
		string text2 = "rocket_cluster_storage_gas_kanim";
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
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x0002DE9C File Offset: 0x0002C09C
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

	// Token: 0x060007D5 RID: 2005 RVA: 0x0002DF00 File Offset: 0x0002C100
	public override void DoPostConfigureComplete(GameObject go)
	{
		go = BuildingTemplates.ExtendBuildingToClusterCargoBay(go, this.CAPACITY, STORAGEFILTERS.GASES, CargoBay.CargoType.Gasses);
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MODERATE, 0f, 0f);
	}

	// Token: 0x04000506 RID: 1286
	public const string ID = "GasCargoBayCluster";

	// Token: 0x04000507 RID: 1287
	public float CAPACITY = ROCKETRY.GAS_CARGO_BAY_CLUSTER_CAPACITY * ROCKETRY.CARGO_CAPACITY_SCALE;
}
