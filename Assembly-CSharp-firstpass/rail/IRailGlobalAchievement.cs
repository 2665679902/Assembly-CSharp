using System;

namespace rail
{
	// Token: 0x020002D3 RID: 723
	public interface IRailGlobalAchievement : IRailComponent
	{
		// Token: 0x06002D6A RID: 11626
		RailResult AsyncRequestAchievement(string user_data);

		// Token: 0x06002D6B RID: 11627
		RailResult GetGlobalAchievedPercent(string name, out double percent);

		// Token: 0x06002D6C RID: 11628
		RailResult GetGlobalAchievedPercentDescending(int index, out string name, out double percent);
	}
}
