using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CF4 RID: 3316
	public class LandOnAllWorlds : ColonyAchievementRequirement
	{
		// Token: 0x06006701 RID: 26369 RVA: 0x00278598 File Offset: 0x00276798
		public override string GetProgress(bool complete)
		{
			int num = 0;
			int num2 = 0;
			foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
			{
				if (!worldContainer.IsModuleInterior)
				{
					num++;
					if (worldContainer.IsDupeVisited || worldContainer.IsRoverVisted)
					{
						num2++;
					}
				}
			}
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.LAND_DUPES_ON_ALL_WORLDS, num2, num);
		}

		// Token: 0x06006702 RID: 26370 RVA: 0x00278624 File Offset: 0x00276824
		public override bool Success()
		{
			foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
			{
				if (!worldContainer.IsModuleInterior && !worldContainer.IsDupeVisited && !worldContainer.IsRoverVisted)
				{
					return false;
				}
			}
			return true;
		}
	}
}
