using System;
using TUNING;
using UnityEngine;

// Token: 0x02000194 RID: 404
public class GasCargoBayConfig : IBuildingConfig
{
	// Token: 0x060007D7 RID: 2007 RVA: 0x0002DF46 File Offset: 0x0002C146
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_VANILLA_ONLY;
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x0002DF50 File Offset: 0x0002C150
	public override BuildingDef CreateBuildingDef()
	{
		string text = "GasCargoBay";
		int num = 5;
		int num2 = 5;
		string text2 = "rocket_storage_gas_kanim";
		int num3 = 1000;
		float num4 = 60f;
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
		buildingDef.OutputConduitType = ConduitType.Gas;
		buildingDef.UtilityOutputOffset = new CellOffset(0, 3);
		buildingDef.RequiresPowerInput = false;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.CanMove = true;
		return buildingDef;
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x0002E014 File Offset: 0x0002C214
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

	// Token: 0x060007DA RID: 2010 RVA: 0x0002E078 File Offset: 0x0002C278
	public override void DoPostConfigureComplete(GameObject go)
	{
		CargoBay cargoBay = go.AddOrGet<CargoBay>();
		cargoBay.storage = go.AddOrGet<Storage>();
		cargoBay.storageType = CargoBay.CargoType.Gasses;
		cargoBay.storage.capacityKg = 1000f;
		cargoBay.storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Gas;
		conduitDispenser.storage = cargoBay.storage;
		BuildingTemplates.ExtendBuildingToRocketModule(go, "rocket_storage_gas_bg_kanim", false);
	}

	// Token: 0x04000508 RID: 1288
	public const string ID = "GasCargoBay";
}
