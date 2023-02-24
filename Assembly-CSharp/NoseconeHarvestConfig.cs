using System;
using TUNING;
using UnityEngine;

// Token: 0x02000281 RID: 641
public class NoseconeHarvestConfig : IBuildingConfig
{
	// Token: 0x06000CCD RID: 3277 RVA: 0x000474BF File Offset: 0x000456BF
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x000474C8 File Offset: 0x000456C8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "NoseconeHarvest";
		int num = 5;
		int num2 = 4;
		string text2 = "rocket_nosecone_gathering_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] hollow_TIER = BUILDINGS.ROCKETRY_MASS_KG.HOLLOW_TIER2;
		string[] array = new string[] { "RefinedMetal", "Plastic" };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, hollow_TIER, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.ForegroundLayer = Grid.SceneLayer.Front;
		buildingDef.RequiresPowerInput = false;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x00047588 File Offset: 0x00045788
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.GetComponent<KPrefabID>().AddTag(GameTags.NoseRocketModule, false);
		go.AddOrGetDef<ResourceHarvestModule.Def>().harvestSpeed = this.solidCapacity / this.timeToFill;
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 1000f;
		storage.useWideOffsets = true;
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = SimHashes.Diamond.CreateTag();
		manualDeliveryKG.MinimumMass = storage.capacityKg;
		manualDeliveryKG.capacity = storage.capacityKg;
		manualDeliveryKG.refillMass = storage.capacityKg;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x0004765E File Offset: 0x0004585E
	public override void DoPostConfigureComplete(GameObject go)
	{
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MINOR, 0f, 0f);
		go.GetComponent<ReorderableBuilding>().buildConditions.Add(new TopOnly());
	}

	// Token: 0x0400075F RID: 1887
	public const string ID = "NoseconeHarvest";

	// Token: 0x04000760 RID: 1888
	private float timeToFill = 3600f;

	// Token: 0x04000761 RID: 1889
	private float solidCapacity = ROCKETRY.SOLID_CARGO_BAY_CLUSTER_CAPACITY * ROCKETRY.CARGO_CAPACITY_SCALE;

	// Token: 0x04000762 RID: 1890
	public const float DIAMOND_CONSUMED_PER_HARVEST_KG = 0.05f;

	// Token: 0x04000763 RID: 1891
	public const float DIAMOND_STORAGE_CAPACITY_KG = 1000f;
}
