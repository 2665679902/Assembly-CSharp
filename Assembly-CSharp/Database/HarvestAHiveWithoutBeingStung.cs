using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CF6 RID: 3318
	public class HarvestAHiveWithoutBeingStung : ColonyAchievementRequirement
	{
		// Token: 0x06006707 RID: 26375 RVA: 0x002786F1 File Offset: 0x002768F1
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.HARVEST_HIVE;
		}

		// Token: 0x06006708 RID: 26376 RVA: 0x002786FD File Offset: 0x002768FD
		public override bool Success()
		{
			return SaveGame.Instance.GetComponent<ColonyAchievementTracker>().harvestAHiveWithoutGettingStung;
		}
	}
}
