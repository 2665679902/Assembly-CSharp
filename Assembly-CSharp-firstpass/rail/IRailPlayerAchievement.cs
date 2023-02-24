using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002D4 RID: 724
	public interface IRailPlayerAchievement : IRailComponent
	{
		// Token: 0x06002D6D RID: 11629
		RailID GetRailID();

		// Token: 0x06002D6E RID: 11630
		RailResult AsyncRequestAchievement(string user_data);

		// Token: 0x06002D6F RID: 11631
		RailResult HasAchieved(string name, out bool achieved);

		// Token: 0x06002D70 RID: 11632
		RailResult GetAchievementInfo(string name, out string achievement_info);

		// Token: 0x06002D71 RID: 11633
		RailResult AsyncTriggerAchievementProgress(string name, uint current_value, uint max_value, string user_data);

		// Token: 0x06002D72 RID: 11634
		RailResult AsyncTriggerAchievementProgress(string name, uint current_value, uint max_value);

		// Token: 0x06002D73 RID: 11635
		RailResult AsyncTriggerAchievementProgress(string name, uint current_value);

		// Token: 0x06002D74 RID: 11636
		RailResult MakeAchievement(string name);

		// Token: 0x06002D75 RID: 11637
		RailResult CancelAchievement(string name);

		// Token: 0x06002D76 RID: 11638
		RailResult AsyncStoreAchievement(string user_data);

		// Token: 0x06002D77 RID: 11639
		RailResult ResetAllAchievements();

		// Token: 0x06002D78 RID: 11640
		RailResult GetAllAchievementsName(List<string> names);

		// Token: 0x06002D79 RID: 11641
		RailResult GetAchievementInfo(string name, RailPlayerAchievementInfo achievement_info);
	}
}
