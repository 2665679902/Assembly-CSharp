using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000308 RID: 776
public class SmallOxidizerTankConfig : IBuildingConfig
{
	// Token: 0x06000F74 RID: 3956 RVA: 0x00054233 File Offset: 0x00052433
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000F75 RID: 3957 RVA: 0x0005423C File Offset: 0x0005243C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SmallOxidizerTank";
		int num = 3;
		int num2 = 2;
		string text2 = "rocket_oxidizer_tank_small_kanim";
		int num3 = 1000;
		float num4 = 30f;
		float[] dense_TIER = TUNING.BUILDINGS.ROCKETRY_MASS_KG.DENSE_TIER0;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, dense_TIER, all_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.NONE, tier, 0.2f);
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

	// Token: 0x06000F76 RID: 3958 RVA: 0x000542EC File Offset: 0x000524EC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 2), GameTags.Rocket, null)
		};
		Prioritizable.AddRef(go);
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x00054358 File Offset: 0x00052558
	public override void DoPostConfigureComplete(GameObject go)
	{
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 450f;
		storage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
		{
			Storage.StoredItemModifier.Hide,
			Storage.StoredItemModifier.Seal,
			Storage.StoredItemModifier.Insulate
		});
		FlatTagFilterable flatTagFilterable = go.AddOrGet<FlatTagFilterable>();
		flatTagFilterable.tagOptions = new List<Tag>
		{
			SimHashes.OxyRock.CreateTag(),
			SimHashes.Fertilizer.CreateTag()
		};
		flatTagFilterable.headerText = STRINGS.BUILDINGS.PREFABS.OXIDIZERTANK.UI_FILTER_CATEGORY;
		OxidizerTank oxidizerTank = go.AddOrGet<OxidizerTank>();
		oxidizerTank.consumeOnLand = !DlcManager.FeatureClusterSpaceEnabled();
		oxidizerTank.storage = storage;
		oxidizerTank.targetFillMass = 450f;
		oxidizerTank.maxFillMass = 450f;
		oxidizerTank.supportsMultipleOxidizers = true;
		go.AddOrGet<Prioritizable>();
		go.AddOrGet<CopyBuildingSettings>();
		go.AddOrGet<DropToUserCapacity>();
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MINOR, 0f, 0f);
	}

	// Token: 0x04000881 RID: 2177
	public const string ID = "SmallOxidizerTank";

	// Token: 0x04000882 RID: 2178
	public const float FuelCapacity = 450f;
}
