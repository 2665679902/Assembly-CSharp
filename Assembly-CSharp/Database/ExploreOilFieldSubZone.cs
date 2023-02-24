using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CDE RID: 3294
	public class ExploreOilFieldSubZone : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x060066AF RID: 26287 RVA: 0x0027734C File Offset: 0x0027554C
		public override bool Success()
		{
			return Game.Instance.savedInfo.discoveredOilField;
		}

		// Token: 0x060066B0 RID: 26288 RVA: 0x0027735D File Offset: 0x0027555D
		public void Deserialize(IReader reader)
		{
		}

		// Token: 0x060066B1 RID: 26289 RVA: 0x0027735F File Offset: 0x0027555F
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.ENTER_OIL_BIOME;
		}
	}
}
