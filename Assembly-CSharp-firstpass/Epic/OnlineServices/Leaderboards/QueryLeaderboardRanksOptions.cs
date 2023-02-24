using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007F8 RID: 2040
	public class QueryLeaderboardRanksOptions
	{
		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06004969 RID: 18793 RVA: 0x0009167F File Offset: 0x0008F87F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x0600496A RID: 18794 RVA: 0x00091682 File Offset: 0x0008F882
		// (set) Token: 0x0600496B RID: 18795 RVA: 0x0009168A File Offset: 0x0008F88A
		public string LeaderboardId { get; set; }
	}
}
