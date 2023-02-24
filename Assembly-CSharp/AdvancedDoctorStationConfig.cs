using System;
using TUNING;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class AdvancedDoctorStationConfig : IBuildingConfig
{
	// Token: 0x0600004F RID: 79 RVA: 0x00003EF0 File Offset: 0x000020F0
	public override BuildingDef CreateBuildingDef()
	{
		string text = "AdvancedDoctorStation";
		int num = 2;
		int num2 = 3;
		string text2 = "bed_medical_kanim";
		int num3 = 100;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 480f;
		buildingDef.ExhaustKilowattsWhenActive = 0.25f;
		buildingDef.SelfHeatKilowattsWhenActive = 0.5f;
		buildingDef.AudioCategory = "Metal";
		return buildingDef;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00003F69 File Offset: 0x00002169
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.Clinic, false);
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00003F84 File Offset: 0x00002184
	public override void DoPostConfigureComplete(GameObject go)
	{
		Storage storage = go.AddOrGet<Storage>();
		storage.showInUI = true;
		Tag supplyTagForStation = MedicineInfo.GetSupplyTagForStation("AdvancedDoctorStation");
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = supplyTagForStation;
		manualDeliveryKG.capacity = 10f;
		manualDeliveryKG.refillMass = 5f;
		manualDeliveryKG.MinimumMass = 1f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.DoctorFetch.IdHash;
		manualDeliveryKG.operationalRequirement = Operational.State.Functional;
		DoctorStation doctorStation = go.AddOrGet<DoctorStation>();
		doctorStation.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_medical_bed_kanim") };
		doctorStation.workLayer = Grid.SceneLayer.BuildingFront;
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.Hospital.Id;
		roomTracker.requirement = RoomTracker.Requirement.CustomRecommended;
		roomTracker.customStatusItemID = Db.Get().BuildingStatusItems.ClinicOutsideHospital.Id;
		DoctorStationDoctorWorkable doctorStationDoctorWorkable = go.AddOrGet<DoctorStationDoctorWorkable>();
		doctorStationDoctorWorkable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_medical_bed_doctor_kanim") };
		doctorStationDoctorWorkable.SetWorkTime(60f);
		doctorStationDoctorWorkable.requiredSkillPerk = Db.Get().SkillPerks.CanAdvancedMedicine.Id;
	}

	// Token: 0x0400003A RID: 58
	public const string ID = "AdvancedDoctorStation";
}
