using System;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x0200065E RID: 1630
[AddComponentMenu("KMonoBehaviour/Workable/ToiletWorkableClean")]
public class ToiletWorkableClean : Workable
{
	// Token: 0x06002BC9 RID: 11209 RVA: 0x000E6058 File Offset: 0x000E4258
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Cleaning;
		this.workingStatusItem = Db.Get().MiscStatusItems.Cleaning;
		this.attributeConverter = Db.Get().AttributeConverters.TidyingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Basekeeping.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.workAnims = ToiletWorkableClean.CLEAN_ANIMS;
		this.workingPstComplete = new HashedString[] { ToiletWorkableClean.PST_ANIM };
		this.workingPstFailed = new HashedString[] { ToiletWorkableClean.PST_ANIM };
	}

	// Token: 0x06002BCA RID: 11210 RVA: 0x000E6115 File Offset: 0x000E4315
	protected override void OnCompleteWork(Worker worker)
	{
		this.timesCleaned++;
		base.OnCompleteWork(worker);
	}

	// Token: 0x040019EB RID: 6635
	[Serialize]
	public int timesCleaned;

	// Token: 0x040019EC RID: 6636
	private static readonly HashedString[] CLEAN_ANIMS = new HashedString[] { "unclog_pre", "unclog_loop" };

	// Token: 0x040019ED RID: 6637
	private static readonly HashedString PST_ANIM = new HashedString("unclog_pst");
}
