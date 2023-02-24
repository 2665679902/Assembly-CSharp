using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x02000296 RID: 662
public class OxidizerTankLiquidClusterConfig : IBuildingConfig
{
	// Token: 0x06000D2E RID: 3374 RVA: 0x0004926F File Offset: 0x0004746F
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x00049278 File Offset: 0x00047478
	public override BuildingDef CreateBuildingDef()
	{
		string text = "OxidizerTankLiquidCluster";
		int num = 5;
		int num2 = 2;
		string text2 = "rocket_cluster_oxidizer_tank_liquid_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] fuel_TANK_DRY_MASS = BUILDINGS.ROCKETRY_MASS_KG.FUEL_TANK_DRY_MASS;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, fuel_TANK_DRY_MASS, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.DefaultAnimState = "grounded";
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.UtilityInputOffset = new CellOffset(1, 1);
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.RequiresPowerInput = false;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x00049354 File Offset: 0x00047554
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 2), GameTags.Rocket, null)
		};
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x000493B8 File Offset: 0x000475B8
	public override void DoPostConfigureComplete(GameObject go)
	{
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 450f;
		storage.storageFilters = new List<Tag> { SimHashes.LiquidOxygen.CreateTag() };
		storage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
		{
			Storage.StoredItemModifier.Hide,
			Storage.StoredItemModifier.Seal,
			Storage.StoredItemModifier.Insulate
		});
		OxidizerTank oxidizerTank = go.AddOrGet<OxidizerTank>();
		oxidizerTank.supportsMultipleOxidizers = false;
		oxidizerTank.consumeOnLand = false;
		oxidizerTank.storage = storage;
		oxidizerTank.targetFillMass = 450f;
		oxidizerTank.maxFillMass = 450f;
		go.AddOrGet<CopyBuildingSettings>();
		go.AddOrGet<DropToUserCapacity>();
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.consumptionRate = 10f;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.LiquidOxygen).tag;
		conduitConsumer.capacityKG = storage.capacityKg;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MODERATE_PLUS, 0f, 0f);
		storage.showUnreachableStatus = false;
		go.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject inst)
		{
			Element element = ElementLoader.FindElementByHash(SimHashes.LiquidOxygen);
			if (!DiscoveredResources.Instance.IsDiscovered(element.tag))
			{
				DiscoveredResources.Instance.Discover(element.tag, element.GetMaterialCategoryTag());
			}
		};
	}

	// Token: 0x040007A5 RID: 1957
	public const string ID = "OxidizerTankLiquidCluster";

	// Token: 0x040007A6 RID: 1958
	public const float FuelCapacity = 450f;
}
