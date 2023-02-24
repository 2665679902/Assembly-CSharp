using System;
using TUNING;
using UnityEngine;

// Token: 0x020005B0 RID: 1456
[AddComponentMenu("KMonoBehaviour/Workable/EggIncubatorWorkable")]
public class EggIncubatorWorkable : Workable
{
	// Token: 0x0600240F RID: 9231 RVA: 0x000C31B0 File Offset: 0x000C13B0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.synchronizeAnims = false;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_incubator_kanim") };
		base.SetWorkTime(15f);
		this.showProgressBar = true;
		this.requiredSkillPerk = Db.Get().SkillPerks.CanWrangleCreatures.Id;
		this.attributeConverter = Db.Get().AttributeConverters.RanchingEffectDuration;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.BARELY_EVER_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Ranching.Id;
		this.skillExperienceMultiplier = SKILLS.BARELY_EVER_EXPERIENCE;
	}

	// Token: 0x06002410 RID: 9232 RVA: 0x000C325C File Offset: 0x000C145C
	protected override void OnCompleteWork(Worker worker)
	{
		EggIncubator component = base.GetComponent<EggIncubator>();
		if (component && component.Occupant)
		{
			IncubationMonitor.Instance smi = component.Occupant.GetSMI<IncubationMonitor.Instance>();
			if (smi != null)
			{
				smi.ApplySongBuff();
			}
		}
	}
}
