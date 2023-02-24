using System;
using TUNING;
using UnityEngine;

// Token: 0x02000725 RID: 1829
[AddComponentMenu("KMonoBehaviour/Workable/DoctorStationDoctorWorkable")]
public class DoctorStationDoctorWorkable : Workable
{
	// Token: 0x06003213 RID: 12819 RVA: 0x0010BFBB File Offset: 0x0010A1BB
	private DoctorStationDoctorWorkable()
	{
		this.synchronizeAnims = false;
	}

	// Token: 0x06003214 RID: 12820 RVA: 0x0010BFCC File Offset: 0x0010A1CC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.attributeConverter = Db.Get().AttributeConverters.DoctorSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.BARELY_EVER_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.MedicalAid.Id;
		this.skillExperienceMultiplier = SKILLS.BARELY_EVER_EXPERIENCE;
	}

	// Token: 0x06003215 RID: 12821 RVA: 0x0010C024 File Offset: 0x0010A224
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06003216 RID: 12822 RVA: 0x0010C02C File Offset: 0x0010A22C
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		this.station.SetHasDoctor(true);
	}

	// Token: 0x06003217 RID: 12823 RVA: 0x0010C041 File Offset: 0x0010A241
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		this.station.SetHasDoctor(false);
	}

	// Token: 0x06003218 RID: 12824 RVA: 0x0010C056 File Offset: 0x0010A256
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
		this.station.CompleteDoctoring();
	}

	// Token: 0x04001E64 RID: 7780
	[MyCmpReq]
	private DoctorStation station;
}
