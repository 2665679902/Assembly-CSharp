using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CF1 RID: 3313
	public class BuildALaunchPad : ColonyAchievementRequirement
	{
		// Token: 0x060066F8 RID: 26360 RVA: 0x00278459 File Offset: 0x00276659
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.BUILD_A_LAUNCHPAD;
		}

		// Token: 0x060066F9 RID: 26361 RVA: 0x00278468 File Offset: 0x00276668
		public override bool Success()
		{
			foreach (LaunchPad launchPad in Components.LaunchPads.Items)
			{
				WorldContainer myWorld = launchPad.GetMyWorld();
				if (!myWorld.IsStartWorld && Components.WarpReceivers.GetWorldItems(myWorld.id, false).Count == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
