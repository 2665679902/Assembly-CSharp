using System;

namespace rail
{
	// Token: 0x020002D6 RID: 726
	public class PlayerAchievementStored : EventBase
	{
		// Token: 0x04000A70 RID: 2672
		public bool group_achievement;

		// Token: 0x04000A71 RID: 2673
		public string achievement_name;

		// Token: 0x04000A72 RID: 2674
		public uint current_progress;

		// Token: 0x04000A73 RID: 2675
		public uint max_progress;
	}
}
