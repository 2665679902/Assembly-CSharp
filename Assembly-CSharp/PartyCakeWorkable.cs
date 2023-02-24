using System;
using TUNING;

// Token: 0x02000622 RID: 1570
public class PartyCakeWorkable : Workable
{
	// Token: 0x0600291C RID: 10524 RVA: 0x000D935C File Offset: 0x000D755C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Cooking;
		this.alwaysShowProgressBar = true;
		this.resetProgressOnStop = false;
		this.attributeConverter = Db.Get().AttributeConverters.CookingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_desalinator_kanim") };
		this.workAnims = PartyCakeWorkable.WORK_ANIMS;
		this.workingPstComplete = new HashedString[] { PartyCakeWorkable.PST_ANIM };
		this.workingPstFailed = new HashedString[] { PartyCakeWorkable.PST_ANIM };
		this.synchronizeAnims = false;
	}

	// Token: 0x0600291D RID: 10525 RVA: 0x000D9412 File Offset: 0x000D7612
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		base.OnWorkTick(worker, dt);
		base.GetComponent<KBatchedAnimController>().SetPositionPercent(this.GetPercentComplete());
		return false;
	}

	// Token: 0x0400182E RID: 6190
	private static readonly HashedString[] WORK_ANIMS = new HashedString[] { "salt_pre", "salt_loop" };

	// Token: 0x0400182F RID: 6191
	private static readonly HashedString PST_ANIM = new HashedString("salt_pst");
}
