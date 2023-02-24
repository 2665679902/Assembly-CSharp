using System;

namespace rail
{
	// Token: 0x0200039E RID: 926
	public class LeaderboardUploaded : EventBase
	{
		// Token: 0x04000D4F RID: 3407
		public int old_rank;

		// Token: 0x04000D50 RID: 3408
		public string leaderboard_name;

		// Token: 0x04000D51 RID: 3409
		public double score;

		// Token: 0x04000D52 RID: 3410
		public bool better_score;

		// Token: 0x04000D53 RID: 3411
		public int new_rank;
	}
}
