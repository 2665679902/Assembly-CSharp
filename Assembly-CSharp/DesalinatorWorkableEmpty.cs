using System;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x020005A3 RID: 1443
[AddComponentMenu("KMonoBehaviour/Workable/DesalinatorWorkableEmpty")]
public class DesalinatorWorkableEmpty : Workable
{
	// Token: 0x06002398 RID: 9112 RVA: 0x000C024C File Offset: 0x000BE44C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Cleaning;
		this.workingStatusItem = Db.Get().MiscStatusItems.Cleaning;
		this.attributeConverter = Db.Get().AttributeConverters.TidyingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_desalinator_kanim") };
		this.workAnims = DesalinatorWorkableEmpty.WORK_ANIMS;
		this.workingPstComplete = new HashedString[] { DesalinatorWorkableEmpty.PST_ANIM };
		this.workingPstFailed = new HashedString[] { DesalinatorWorkableEmpty.PST_ANIM };
		this.synchronizeAnims = false;
	}

	// Token: 0x06002399 RID: 9113 RVA: 0x000C0309 File Offset: 0x000BE509
	protected override void OnCompleteWork(Worker worker)
	{
		this.timesCleaned++;
		base.OnCompleteWork(worker);
	}

	// Token: 0x0400146D RID: 5229
	[Serialize]
	public int timesCleaned;

	// Token: 0x0400146E RID: 5230
	private static readonly HashedString[] WORK_ANIMS = new HashedString[] { "salt_pre", "salt_loop" };

	// Token: 0x0400146F RID: 5231
	private static readonly HashedString PST_ANIM = new HashedString("salt_pst");
}
