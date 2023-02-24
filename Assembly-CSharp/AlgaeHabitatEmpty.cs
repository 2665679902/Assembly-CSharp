using System;
using TUNING;
using UnityEngine;

// Token: 0x02000573 RID: 1395
[AddComponentMenu("KMonoBehaviour/Workable/AlgaeHabitatEmpty")]
public class AlgaeHabitatEmpty : Workable
{
	// Token: 0x060021B7 RID: 8631 RVA: 0x000B79B4 File Offset: 0x000B5BB4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Cleaning;
		this.workingStatusItem = Db.Get().MiscStatusItems.Cleaning;
		this.attributeConverter = Db.Get().AttributeConverters.TidyingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.workAnims = AlgaeHabitatEmpty.CLEAN_ANIMS;
		this.workingPstComplete = new HashedString[] { AlgaeHabitatEmpty.PST_ANIM };
		this.workingPstFailed = new HashedString[] { AlgaeHabitatEmpty.PST_ANIM };
		this.synchronizeAnims = false;
	}

	// Token: 0x04001371 RID: 4977
	private static readonly HashedString[] CLEAN_ANIMS = new HashedString[] { "sponge_pre", "sponge_loop" };

	// Token: 0x04001372 RID: 4978
	private static readonly HashedString PST_ANIM = new HashedString("sponge_pst");
}
