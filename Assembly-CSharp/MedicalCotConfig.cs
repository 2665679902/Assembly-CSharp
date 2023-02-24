using System;
using TUNING;
using UnityEngine;

// Token: 0x0200022F RID: 559
public class MedicalCotConfig : IBuildingConfig
{
	// Token: 0x06000B06 RID: 2822 RVA: 0x0003E338 File Offset: 0x0003C538
	public override BuildingDef CreateBuildingDef()
	{
		string text = "MedicalCot";
		int num = 3;
		int num2 = 2;
		string text2 = "medical_cot_kanim";
		int num3 = 10;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		return buildingDef;
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x0003E390 File Offset: 0x0003C590
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.Clinic, false);
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x0003E3AC File Offset: 0x0003C5AC
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<KAnimControllerBase>().initialAnim = "off";
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.BedType, false);
		Clinic clinic = go.AddOrGet<Clinic>();
		clinic.doctorVisitInterval = 300f;
		clinic.workerInjuredAnims = new KAnimFile[] { Assets.GetAnim("anim_healing_bed_kanim") };
		clinic.workerDiseasedAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_med_cot_sick_kanim") };
		clinic.workLayer = Grid.SceneLayer.BuildingFront;
		string text = "MedicalCot";
		string text2 = "MedicalCotDoctored";
		clinic.healthEffect = text;
		clinic.doctoredHealthEffect = text2;
		clinic.diseaseEffect = text;
		clinic.doctoredDiseaseEffect = text2;
		clinic.doctoredPlaceholderEffect = "DoctoredOffCotEffect";
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.Hospital.Id;
		roomTracker.requirement = RoomTracker.Requirement.CustomRecommended;
		roomTracker.customStatusItemID = Db.Get().BuildingStatusItems.ClinicOutsideHospital.Id;
		go.AddOrGet<Sleepable>().overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_med_cot_sick_kanim") };
		DoctorChoreWorkable doctorChoreWorkable = go.AddOrGet<DoctorChoreWorkable>();
		doctorChoreWorkable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_med_cot_doctor_kanim") };
		doctorChoreWorkable.workTime = 45f;
		go.AddOrGet<Ownable>().slotID = Db.Get().AssignableSlots.Clinic.Id;
	}

	// Token: 0x04000679 RID: 1657
	public const string ID = "MedicalCot";
}
