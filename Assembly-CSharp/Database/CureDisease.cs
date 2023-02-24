using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CE6 RID: 3302
	public class CureDisease : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x060066CF RID: 26319 RVA: 0x00277C38 File Offset: 0x00275E38
		public override bool Success()
		{
			return Game.Instance.savedInfo.curedDisease;
		}

		// Token: 0x060066D0 RID: 26320 RVA: 0x00277C49 File Offset: 0x00275E49
		public void Deserialize(IReader reader)
		{
		}

		// Token: 0x060066D1 RID: 26321 RVA: 0x00277C4B File Offset: 0x00275E4B
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.CURED_DISEASE;
		}
	}
}
