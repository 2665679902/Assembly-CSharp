using System;
using TUNING;

// Token: 0x0200098D RID: 2445
public class SplatWorkable : Workable
{
	// Token: 0x06004870 RID: 18544 RVA: 0x001964A4 File Offset: 0x001946A4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetOffsetTable(OffsetGroups.InvertedStandardTableWithCorners);
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Mopping;
		this.attributeConverter = Db.Get().AttributeConverters.TidyingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Basekeeping.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.multitoolContext = "disinfect";
		this.multitoolHitEffectTag = "fx_disinfect_splash";
		this.synchronizeAnims = false;
		Prioritizable.AddRef(base.gameObject);
	}

	// Token: 0x06004871 RID: 18545 RVA: 0x0019654E File Offset: 0x0019474E
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.SetWorkTime(5f);
	}
}
