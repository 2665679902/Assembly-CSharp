using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CF5 RID: 3317
	public class RadBoltTravelDistance : ColonyAchievementRequirement
	{
		// Token: 0x06006704 RID: 26372 RVA: 0x00278698 File Offset: 0x00276898
		public RadBoltTravelDistance(int travelDistance)
		{
			this.travelDistance = travelDistance;
		}

		// Token: 0x06006705 RID: 26373 RVA: 0x002786A7 File Offset: 0x002768A7
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.RADBOLT_TRAVEL, SaveGame.Instance.GetComponent<ColonyAchievementTracker>().radBoltTravelDistance, this.travelDistance);
		}

		// Token: 0x06006706 RID: 26374 RVA: 0x002786D7 File Offset: 0x002768D7
		public override bool Success()
		{
			return SaveGame.Instance.GetComponent<ColonyAchievementTracker>().radBoltTravelDistance > (float)this.travelDistance;
		}

		// Token: 0x04004AF5 RID: 19189
		private int travelDistance;
	}
}
