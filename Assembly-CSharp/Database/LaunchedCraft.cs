using System;
using System.Collections;
using STRINGS;

namespace Database
{
	// Token: 0x02000CEE RID: 3310
	public class LaunchedCraft : ColonyAchievementRequirement
	{
		// Token: 0x060066EF RID: 26351 RVA: 0x00278349 File Offset: 0x00276549
		public override string GetProgress(bool completed)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.LAUNCHED_ROCKET;
		}

		// Token: 0x060066F0 RID: 26352 RVA: 0x00278358 File Offset: 0x00276558
		public override bool Success()
		{
			using (IEnumerator enumerator = Components.Clustercrafts.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((Clustercraft)enumerator.Current).Status == Clustercraft.CraftStatus.InFlight)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
