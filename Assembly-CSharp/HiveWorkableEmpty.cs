using System;
using TUNING;
using UnityEngine;

// Token: 0x020005D1 RID: 1489
[AddComponentMenu("KMonoBehaviour/Workable/HiveWorkableEmpty")]
public class HiveWorkableEmpty : Workable
{
	// Token: 0x0600252B RID: 9515 RVA: 0x000C8DA0 File Offset: 0x000C6FA0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Emptying;
		this.attributeConverter = Db.Get().AttributeConverters.TidyingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Basekeeping.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.workAnims = HiveWorkableEmpty.WORK_ANIMS;
		this.workingPstComplete = new HashedString[] { HiveWorkableEmpty.PST_ANIM };
		this.workingPstFailed = new HashedString[] { HiveWorkableEmpty.PST_ANIM };
	}

	// Token: 0x0600252C RID: 9516 RVA: 0x000C8E48 File Offset: 0x000C7048
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
		if (!this.wasStung)
		{
			SaveGame.Instance.GetComponent<ColonyAchievementTracker>().harvestAHiveWithoutGettingStung = true;
		}
	}

	// Token: 0x04001587 RID: 5511
	private static readonly HashedString[] WORK_ANIMS = new HashedString[] { "working_pre", "working_loop" };

	// Token: 0x04001588 RID: 5512
	private static readonly HashedString PST_ANIM = new HashedString("working_pst");

	// Token: 0x04001589 RID: 5513
	public bool wasStung;
}
