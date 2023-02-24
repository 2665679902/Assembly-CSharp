using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000CF7 RID: 3319
	public class SurviveARocketWithMinimumMorale : ColonyAchievementRequirement
	{
		// Token: 0x0600670A RID: 26378 RVA: 0x00278716 File Offset: 0x00276916
		public SurviveARocketWithMinimumMorale(float minimumMorale, int numberOfCycles)
		{
			this.minimumMorale = minimumMorale;
			this.numberOfCycles = numberOfCycles;
		}

		// Token: 0x0600670B RID: 26379 RVA: 0x0027872C File Offset: 0x0027692C
		public override string GetProgress(bool complete)
		{
			if (complete)
			{
				return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.SURVIVE_SPACE_COMPLETE, this.minimumMorale, this.numberOfCycles);
			}
			return base.GetProgress(complete);
		}

		// Token: 0x0600670C RID: 26380 RVA: 0x00278760 File Offset: 0x00276960
		public override bool Success()
		{
			foreach (KeyValuePair<int, int> keyValuePair in SaveGame.Instance.GetComponent<ColonyAchievementTracker>().cyclesRocketDupeMoraleAboveRequirement)
			{
				if (keyValuePair.Value >= this.numberOfCycles)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04004AF6 RID: 19190
		public float minimumMorale;

		// Token: 0x04004AF7 RID: 19191
		public int numberOfCycles;
	}
}
