using System;

namespace rail
{
	// Token: 0x020002D2 RID: 722
	public interface IRailAchievementHelper
	{
		// Token: 0x06002D68 RID: 11624
		IRailPlayerAchievement CreatePlayerAchievement(RailID player);

		// Token: 0x06002D69 RID: 11625
		IRailGlobalAchievement GetGlobalAchievement();
	}
}
