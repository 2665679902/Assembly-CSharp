using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CCD RID: 3277
	public class OpenTemporalTear : VictoryColonyAchievementRequirement
	{
		// Token: 0x06006663 RID: 26211 RVA: 0x00275F6F File Offset: 0x0027416F
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.OPEN_TEMPORAL_TEAR;
		}

		// Token: 0x06006664 RID: 26212 RVA: 0x00275F7B File Offset: 0x0027417B
		public override string Description()
		{
			return this.GetProgress(this.Success());
		}

		// Token: 0x06006665 RID: 26213 RVA: 0x00275F89 File Offset: 0x00274189
		public override bool Success()
		{
			return ClusterManager.Instance.GetComponent<ClusterPOIManager>().IsTemporalTearOpen();
		}

		// Token: 0x06006666 RID: 26214 RVA: 0x00275F9A File Offset: 0x0027419A
		public override string Name()
		{
			return COLONY_ACHIEVEMENTS.DISTANT_PLANET_REACHED.REQUIREMENTS.OPEN_TEMPORAL_TEAR;
		}
	}
}
