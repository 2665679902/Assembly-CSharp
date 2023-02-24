using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000295 RID: 661
public class OxidizerTankConfig : IBuildingConfig
{
	// Token: 0x06000D29 RID: 3369 RVA: 0x00049045 File Offset: 0x00047245
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_VANILLA_ONLY;
	}

	// Token: 0x06000D2A RID: 3370 RVA: 0x0004904C File Offset: 0x0004724C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "OxidizerTank";
		int num = 5;
		int num2 = 5;
		string text2 = "rocket_oxidizer_tank_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] fuel_TANK_DRY_MASS = TUNING.BUILDINGS.ROCKETRY_MASS_KG.FUEL_TANK_DRY_MASS;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.BuildingAttachPoint;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, fuel_TANK_DRY_MASS, array, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.DefaultAnimState = "grounded";
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

	// Token: 0x06000D2B RID: 3371 RVA: 0x00049108 File Offset: 0x00047308
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 5), GameTags.Rocket, null)
		};
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		Prioritizable.AddRef(go);
	}

	// Token: 0x06000D2C RID: 3372 RVA: 0x00049174 File Offset: 0x00047374
	public override void DoPostConfigureComplete(GameObject go)
	{
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 2700f;
		storage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
		{
			Storage.StoredItemModifier.Hide,
			Storage.StoredItemModifier.Seal,
			Storage.StoredItemModifier.Insulate
		});
		FlatTagFilterable flatTagFilterable = go.AddOrGet<FlatTagFilterable>();
		flatTagFilterable.tagOptions = new List<Tag> { SimHashes.OxyRock.CreateTag() };
		flatTagFilterable.headerText = STRINGS.BUILDINGS.PREFABS.OXIDIZERTANK.UI_FILTER_CATEGORY;
		flatTagFilterable.selectedTags.Add(SimHashes.OxyRock.CreateTag());
		OxidizerTank oxidizerTank = go.AddOrGet<OxidizerTank>();
		oxidizerTank.consumeOnLand = !DlcManager.FeatureClusterSpaceEnabled();
		oxidizerTank.storage = storage;
		oxidizerTank.supportsMultipleOxidizers = true;
		oxidizerTank.maxFillMass = 2700f;
		oxidizerTank.targetFillMass = 2700f;
		oxidizerTank.discoverResourcesOnSpawn = new List<SimHashes> { SimHashes.OxyRock };
		go.AddOrGet<CopyBuildingSettings>();
		go.AddOrGet<DropToUserCapacity>();
		go.AddOrGet<Prioritizable>();
		BuildingTemplates.ExtendBuildingToRocketModule(go, "rocket_oxidizer_tank_bg_kanim", false);
	}

	// Token: 0x040007A3 RID: 1955
	public const string ID = "OxidizerTank";

	// Token: 0x040007A4 RID: 1956
	public const float FuelCapacity = 2700f;
}
