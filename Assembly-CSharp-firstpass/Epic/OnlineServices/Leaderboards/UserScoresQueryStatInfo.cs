using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007FC RID: 2044
	public class UserScoresQueryStatInfo
	{
		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06004987 RID: 18823 RVA: 0x00091887 File Offset: 0x0008FA87
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06004988 RID: 18824 RVA: 0x0009188A File Offset: 0x0008FA8A
		// (set) Token: 0x06004989 RID: 18825 RVA: 0x00091892 File Offset: 0x0008FA92
		public string StatName { get; set; }

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x0600498A RID: 18826 RVA: 0x0009189B File Offset: 0x0008FA9B
		// (set) Token: 0x0600498B RID: 18827 RVA: 0x000918A3 File Offset: 0x0008FAA3
		public LeaderboardAggregation Aggregation { get; set; }
	}
}
