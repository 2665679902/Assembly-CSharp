using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CEA RID: 3306
	public class BlockedCometWithBunkerDoor : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x060066E0 RID: 26336 RVA: 0x00278007 File Offset: 0x00276207
		public override bool Success()
		{
			return Game.Instance.savedInfo.blockedCometWithBunkerDoor;
		}

		// Token: 0x060066E1 RID: 26337 RVA: 0x00278018 File Offset: 0x00276218
		public void Deserialize(IReader reader)
		{
		}

		// Token: 0x060066E2 RID: 26338 RVA: 0x0027801A File Offset: 0x0027621A
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.BLOCKED_A_COMET;
		}
	}
}
