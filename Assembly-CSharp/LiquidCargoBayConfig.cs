using System;
using TUNING;
using UnityEngine;

// Token: 0x020001E9 RID: 489
public class LiquidCargoBayConfig : IBuildingConfig
{
	// Token: 0x060009A8 RID: 2472 RVA: 0x0003805A File Offset: 0x0003625A
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_VANILLA_ONLY;
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x00038064 File Offset: 0x00036264
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LiquidCargoBay";
		int num = 5;
		int num2 = 5;
		string text2 = "rocket_storage_liquid_kanim";
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
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.UtilityOutputOffset = new CellOffset(0, 3);
		buildingDef.RequiresPowerInput = false;
		buildingDef.CanMove = true;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		return buildingDef;
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x00038128 File Offset: 0x00036328
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

	// Token: 0x060009AB RID: 2475 RVA: 0x0003818C File Offset: 0x0003638C
	public override void DoPostConfigureComplete(GameObject go)
	{
		CargoBay cargoBay = go.AddOrGet<CargoBay>();
		cargoBay.storage = go.AddOrGet<Storage>();
		cargoBay.storageType = CargoBay.CargoType.Liquids;
		cargoBay.storage.capacityKg = 1000f;
		cargoBay.storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Liquid;
		conduitDispenser.storage = cargoBay.storage;
		BuildingTemplates.ExtendBuildingToRocketModule(go, "rocket_storage_liquid_bg_kanim", false);
	}

	// Token: 0x040005F3 RID: 1523
	public const string ID = "LiquidCargoBay";
}
