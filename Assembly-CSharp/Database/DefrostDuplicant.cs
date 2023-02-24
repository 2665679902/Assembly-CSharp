using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CF0 RID: 3312
	public class DefrostDuplicant : ColonyAchievementRequirement
	{
		// Token: 0x060066F5 RID: 26357 RVA: 0x00278434 File Offset: 0x00276634
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.DEFROST_DUPLICANT;
		}

		// Token: 0x060066F6 RID: 26358 RVA: 0x00278440 File Offset: 0x00276640
		public override bool Success()
		{
			return SaveGame.Instance.GetComponent<ColonyAchievementTracker>().defrostedDuplicant;
		}
	}
}
