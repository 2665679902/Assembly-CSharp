using System;
using TUNING;
using UnityEngine;

// Token: 0x0200030D RID: 781
public class SolidBoosterConfig : IBuildingConfig
{
	// Token: 0x06000F8B RID: 3979 RVA: 0x00054A07 File Offset: 0x00052C07
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_VANILLA_ONLY;
	}

	// Token: 0x06000F8C RID: 3980 RVA: 0x00054A10 File Offset: 0x00052C10
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SolidBooster";
		int num = 7;
		int num2 = 5;
		string text2 = "rocket_solid_booster_kanim";
		int num3 = 1000;
		float num4 = 480f;
		float[] engine_MASS_SMALL = BUILDINGS.ROCKETRY_MASS_KG.ENGINE_MASS_SMALL;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.BuildingAttachPoint;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, engine_MASS_SMALL, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.Invincible = true;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.RequiresPowerInput = false;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.CanMove = true;
		return buildingDef;
	}

	// Token: 0x06000F8D RID: 3981 RVA: 0x00054AC8 File Offset: 0x00052CC8
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

	// Token: 0x06000F8E RID: 3982 RVA: 0x00054B2C File Offset: 0x00052D2C
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x06000F8F RID: 3983 RVA: 0x00054B2E File Offset: 0x00052D2E
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x06000F90 RID: 3984 RVA: 0x00054B30 File Offset: 0x00052D30
	public override void DoPostConfigureComplete(GameObject go)
	{
		SolidBooster solidBooster = go.AddOrGet<SolidBooster>();
		solidBooster.mainEngine = false;
		solidBooster.efficiency = ROCKETRY.ENGINE_EFFICIENCY.BOOSTER;
		solidBooster.fuelTag = ElementLoader.FindElementByHash(SimHashes.Iron).tag;
		Storage storage = go.AddOrGet<Storage>();
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		storage.capacityKg = 800f;
		solidBooster.fuelStorage = storage;
		ManualDeliveryKG manualDeliveryKG = go.AddComponent<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = solidBooster.fuelTag;
		manualDeliveryKG.refillMass = storage.capacityKg / 2f;
		manualDeliveryKG.capacity = storage.capacityKg / 2f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
		ManualDeliveryKG manualDeliveryKG2 = go.AddComponent<ManualDeliveryKG>();
		manualDeliveryKG2.SetStorage(storage);
		manualDeliveryKG2.RequestedItemTag = ElementLoader.FindElementByHash(SimHashes.OxyRock).tag;
		manualDeliveryKG2.refillMass = storage.capacityKg / 2f;
		manualDeliveryKG2.capacity = storage.capacityKg / 2f;
		manualDeliveryKG2.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
		BuildingTemplates.ExtendBuildingToRocketModule(go, "rocket_solid_booster_bg_kanim", false);
	}

	// Token: 0x0400088D RID: 2189
	public const string ID = "SolidBooster";

	// Token: 0x0400088E RID: 2190
	public const float capacity = 400f;
}
