using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x02000287 RID: 647
public class OrbitalCargoModuleConfig : IBuildingConfig
{
	// Token: 0x06000CE9 RID: 3305 RVA: 0x00048130 File Offset: 0x00046330
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000CEA RID: 3306 RVA: 0x00048138 File Offset: 0x00046338
	public override BuildingDef CreateBuildingDef()
	{
		string text = "OrbitalCargoModule";
		int num = 3;
		int num2 = 2;
		string text2 = "rocket_orbital_deploy_cargo_module_kanim";
		int num3 = 1000;
		float num4 = 30f;
		float[] hollow_TIER = BUILDINGS.ROCKETRY_MASS_KG.HOLLOW_TIER2;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, hollow_TIER, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.DefaultAnimState = "deployed";
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.ForegroundLayer = Grid.SceneLayer.Front;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.RequiresPowerInput = false;
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		return buildingDef;
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x000481DC File Offset: 0x000463DC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		List<Tag> list = new List<Tag>();
		list.AddRange(STORAGEFILTERS.NOT_EDIBLE_SOLIDS);
		list.AddRange(STORAGEFILTERS.FOOD);
		Storage storage = go.AddComponent<Storage>();
		storage.showInUI = true;
		storage.capacityKg = OrbitalCargoModuleConfig.TOTAL_STORAGE_MASS;
		storage.showCapacityStatusItem = true;
		storage.showDescriptor = true;
		storage.storageFilters = list;
		storage.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		go.AddOrGet<StorageLocker>();
		go.AddOrGetDef<OrbitalDeployCargoModule.Def>().numCapsules = (float)OrbitalCargoModuleConfig.NUM_CAPSULES;
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 2), GameTags.Rocket, null)
		};
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x000482AC File Offset: 0x000464AC
	public override void DoPostConfigureComplete(GameObject go)
	{
		Prioritizable.AddRef(go);
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MODERATE, 0f, 0f);
		FakeFloorAdder fakeFloorAdder = go.AddOrGet<FakeFloorAdder>();
		fakeFloorAdder.floorOffsets = new CellOffset[]
		{
			new CellOffset(-1, -1),
			new CellOffset(0, -1),
			new CellOffset(1, -1)
		};
		fakeFloorAdder.initiallyActive = false;
	}

	// Token: 0x04000791 RID: 1937
	public const string ID = "OrbitalCargoModule";

	// Token: 0x04000792 RID: 1938
	public static int NUM_CAPSULES = 3 * Mathf.RoundToInt(ROCKETRY.CARGO_CAPACITY_SCALE);

	// Token: 0x04000793 RID: 1939
	public static float TOTAL_STORAGE_MASS = 200f * (float)OrbitalCargoModuleConfig.NUM_CAPSULES;
}
