using System;
using TUNING;
using UnityEngine;

// Token: 0x02000195 RID: 405
public class GasCargoBaySmallConfig : IBuildingConfig
{
	// Token: 0x060007DC RID: 2012 RVA: 0x0002E0EC File Offset: 0x0002C2EC
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x0002E0F4 File Offset: 0x0002C2F4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "GasCargoBaySmall";
		int num = 3;
		int num2 = 3;
		string text2 = "rocket_storage_gas_small_kanim";
		int num3 = 1000;
		float num4 = 30f;
		float[] hollow_TIER = BUILDINGS.ROCKETRY_MASS_KG.HOLLOW_TIER1;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, hollow_TIER, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		return buildingDef;
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x0002E18C File Offset: 0x0002C38C
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

	// Token: 0x060007DF RID: 2015 RVA: 0x0002E1F0 File Offset: 0x0002C3F0
	public override void DoPostConfigureComplete(GameObject go)
	{
		go = BuildingTemplates.ExtendBuildingToClusterCargoBay(go, this.CAPACITY, STORAGEFILTERS.GASES, CargoBay.CargoType.Gasses);
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MINOR, 0f, 0f);
	}

	// Token: 0x04000509 RID: 1289
	public const string ID = "GasCargoBaySmall";

	// Token: 0x0400050A RID: 1290
	public float CAPACITY = 360f * ROCKETRY.CARGO_CAPACITY_SCALE;
}
