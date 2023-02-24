using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000294 RID: 660
public class OxidizerTankClusterConfig : IBuildingConfig
{
	// Token: 0x06000D24 RID: 3364 RVA: 0x00048E17 File Offset: 0x00047017
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000D25 RID: 3365 RVA: 0x00048E20 File Offset: 0x00047020
	public override BuildingDef CreateBuildingDef()
	{
		string text = "OxidizerTankCluster";
		int num = 5;
		int num2 = 5;
		string text2 = "rocket_oxidizer_tank_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] fuel_TANK_DRY_MASS = TUNING.BUILDINGS.ROCKETRY_MASS_KG.FUEL_TANK_DRY_MASS;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, fuel_TANK_DRY_MASS, array, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.DefaultAnimState = "grounded";
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

	// Token: 0x06000D26 RID: 3366 RVA: 0x00048EE8 File Offset: 0x000470E8
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

	// Token: 0x06000D27 RID: 3367 RVA: 0x00048F54 File Offset: 0x00047154
	public override void DoPostConfigureComplete(GameObject go)
	{
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 900f;
		storage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
		{
			Storage.StoredItemModifier.Hide,
			Storage.StoredItemModifier.Seal,
			Storage.StoredItemModifier.Insulate
		});
		storage.useWideOffsets = true;
		FlatTagFilterable flatTagFilterable = go.AddOrGet<FlatTagFilterable>();
		flatTagFilterable.tagOptions = new List<Tag>
		{
			SimHashes.OxyRock.CreateTag(),
			SimHashes.Fertilizer.CreateTag()
		};
		flatTagFilterable.headerText = STRINGS.BUILDINGS.PREFABS.OXIDIZERTANK.UI_FILTER_CATEGORY;
		OxidizerTank oxidizerTank = go.AddOrGet<OxidizerTank>();
		oxidizerTank.consumeOnLand = !DlcManager.FeatureClusterSpaceEnabled();
		oxidizerTank.targetFillMass = 900f;
		oxidizerTank.maxFillMass = 900f;
		oxidizerTank.storage = storage;
		oxidizerTank.supportsMultipleOxidizers = true;
		go.AddOrGet<Prioritizable>();
		go.AddOrGet<CopyBuildingSettings>();
		go.AddOrGet<DropToUserCapacity>();
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MODERATE_PLUS, 0f, 0f);
	}

	// Token: 0x040007A1 RID: 1953
	public const string ID = "OxidizerTankCluster";

	// Token: 0x040007A2 RID: 1954
	public const float FuelCapacity = 900f;
}
