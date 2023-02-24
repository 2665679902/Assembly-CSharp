using System;
using TUNING;
using UnityEngine;

// Token: 0x0200056F RID: 1391
public class AdvancedApothecary : ComplexFabricator
{
	// Token: 0x0600219E RID: 8606 RVA: 0x000B6FD5 File Offset: 0x000B51D5
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.choreType = Db.Get().ChoreTypes.Compound;
		this.fetchChoreTypeIdHash = Db.Get().ChoreTypes.DoctorFetch.IdHash;
		this.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
	}

	// Token: 0x0600219F RID: 8607 RVA: 0x000B7014 File Offset: 0x000B5214
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.workable.WorkerStatusItem = Db.Get().DuplicantStatusItems.Fabricating;
		this.workable.AttributeConverter = Db.Get().AttributeConverters.CompoundingSpeed;
		this.workable.SkillExperienceSkillGroup = Db.Get().SkillGroups.MedicalAid.Id;
		this.workable.SkillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.workable.requiredSkillPerk = Db.Get().SkillPerks.CanCompound.Id;
		this.workable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_medicine_nuclear_kanim") };
		this.workable.AnimOffset = new Vector3(-1f, 0f, 0f);
	}
}
