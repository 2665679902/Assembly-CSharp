using System;
using TUNING;
using UnityEngine;

// Token: 0x0200058D RID: 1421
[AddComponentMenu("KMonoBehaviour/Workable/CompostWorkable")]
public class CompostWorkable : Workable
{
	// Token: 0x060022E3 RID: 8931 RVA: 0x000BDABC File Offset: 0x000BBCBC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.attributeConverter = Db.Get().AttributeConverters.TidyingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Basekeeping.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
	}

	// Token: 0x060022E4 RID: 8932 RVA: 0x000BDB14 File Offset: 0x000BBD14
	protected override void OnStartWork(Worker worker)
	{
	}

	// Token: 0x060022E5 RID: 8933 RVA: 0x000BDB16 File Offset: 0x000BBD16
	protected override void OnStopWork(Worker worker)
	{
	}
}
