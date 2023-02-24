using System;
using TUNING;
using UnityEngine;

// Token: 0x02000584 RID: 1412
[AddComponentMenu("KMonoBehaviour/Workable/DoctorChoreWorkable")]
public class DoctorChoreWorkable : Workable
{
	// Token: 0x0600226A RID: 8810 RVA: 0x000BA930 File Offset: 0x000B8B30
	private DoctorChoreWorkable()
	{
		this.synchronizeAnims = false;
	}

	// Token: 0x0600226B RID: 8811 RVA: 0x000BA940 File Offset: 0x000B8B40
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.attributeConverter = Db.Get().AttributeConverters.DoctorSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.BARELY_EVER_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.MedicalAid.Id;
		this.skillExperienceMultiplier = SKILLS.BARELY_EVER_EXPERIENCE;
	}
}
