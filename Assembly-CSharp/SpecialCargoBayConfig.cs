using System;
using TUNING;
using UnityEngine;

// Token: 0x0200031E RID: 798
public class SpecialCargoBayConfig : IBuildingConfig
{
	// Token: 0x06000FE3 RID: 4067 RVA: 0x000560EE File Offset: 0x000542EE
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_VANILLA_ONLY;
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x000560F8 File Offset: 0x000542F8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SpecialCargoBay";
		int num = 5;
		int num2 = 5;
		string text2 = "rocket_storage_live_kanim";
		int num3 = 1000;
		float num4 = 480f;
		float[] cargo_MASS = BUILDINGS.ROCKETRY_MASS_KG.CARGO_MASS;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.BuildingAttachPoint;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, cargo_MASS, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.RequiresPowerInput = false;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.CanMove = true;
		return buildingDef;
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x000561A8 File Offset: 0x000543A8
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

	// Token: 0x06000FE6 RID: 4070 RVA: 0x0005620C File Offset: 0x0005440C
	public override void DoPostConfigureComplete(GameObject go)
	{
		CargoBay cargoBay = go.AddOrGet<CargoBay>();
		cargoBay.storage = go.AddOrGet<Storage>();
		cargoBay.storageType = CargoBay.CargoType.Entities;
		cargoBay.storage.capacityKg = 100f;
		BuildingTemplates.ExtendBuildingToRocketModule(go, "rocket_storage_live_bg_kanim", false);
	}

	// Token: 0x040008A8 RID: 2216
	public const string ID = "SpecialCargoBay";
}
