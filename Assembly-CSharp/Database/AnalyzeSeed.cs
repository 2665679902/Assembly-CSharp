using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CF2 RID: 3314
	public class AnalyzeSeed : ColonyAchievementRequirement
	{
		// Token: 0x060066FB RID: 26363 RVA: 0x002784EC File Offset: 0x002766EC
		public AnalyzeSeed(string seedname)
		{
			this.seedName = seedname;
		}

		// Token: 0x060066FC RID: 26364 RVA: 0x002784FB File Offset: 0x002766FB
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.ANALYZE_SEED, this.seedName.ProperName());
		}

		// Token: 0x060066FD RID: 26365 RVA: 0x0027851C File Offset: 0x0027671C
		public override bool Success()
		{
			return SaveGame.Instance.GetComponent<ColonyAchievementTracker>().analyzedSeeds.Contains(this.seedName);
		}

		// Token: 0x04004AF3 RID: 19187
		private string seedName;
	}
}
