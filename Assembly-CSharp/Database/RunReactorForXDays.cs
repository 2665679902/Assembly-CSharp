using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000CF8 RID: 3320
	public class RunReactorForXDays : ColonyAchievementRequirement
	{
		// Token: 0x0600670D RID: 26381 RVA: 0x002787CC File Offset: 0x002769CC
		public RunReactorForXDays(int numCycles)
		{
			this.numCycles = numCycles;
		}

		// Token: 0x0600670E RID: 26382 RVA: 0x002787DC File Offset: 0x002769DC
		public override string GetProgress(bool complete)
		{
			int num = 0;
			foreach (Reactor reactor in Components.NuclearReactors.Items)
			{
				if (reactor.numCyclesRunning > num)
				{
					num = reactor.numCyclesRunning;
				}
			}
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.RUN_A_REACTOR, complete ? this.numCycles : num, this.numCycles);
		}

		// Token: 0x0600670F RID: 26383 RVA: 0x0027886C File Offset: 0x00276A6C
		public override bool Success()
		{
			using (List<Reactor>.Enumerator enumerator = Components.NuclearReactors.Items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.numCyclesRunning >= this.numCycles)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04004AF8 RID: 19192
		private int numCycles;
	}
}
