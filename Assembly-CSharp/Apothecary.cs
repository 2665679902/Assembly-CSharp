using System;
using TUNING;
using UnityEngine;

// Token: 0x02000574 RID: 1396
public class Apothecary : ComplexFabricator
{
	// Token: 0x060021BA RID: 8634 RVA: 0x000B7A99 File Offset: 0x000B5C99
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.choreType = Db.Get().ChoreTypes.Compound;
		this.fetchChoreTypeIdHash = Db.Get().ChoreTypes.DoctorFetch.IdHash;
		this.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
	}

	// Token: 0x060021BB RID: 8635 RVA: 0x000B7AD8 File Offset: 0x000B5CD8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.workable.WorkerStatusItem = Db.Get().DuplicantStatusItems.Fabricating;
		this.workable.AttributeConverter = Db.Get().AttributeConverters.CompoundingSpeed;
		this.workable.SkillExperienceSkillGroup = Db.Get().SkillGroups.MedicalAid.Id;
		this.workable.SkillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.workable.requiredSkillPerk = Db.Get().SkillPerks.CanCompound.Id;
		this.workable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_apothecary_kanim") };
		this.workable.AnimOffset = new Vector3(-1f, 0f, 0f);
	}
}
