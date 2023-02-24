using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007F6 RID: 2038
	public class QueryLeaderboardDefinitionsOptions
	{
		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x0600495C RID: 18780 RVA: 0x000915B6 File Offset: 0x0008F7B6
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x0600495D RID: 18781 RVA: 0x000915B9 File Offset: 0x0008F7B9
		// (set) Token: 0x0600495E RID: 18782 RVA: 0x000915C1 File Offset: 0x0008F7C1
		public DateTimeOffset? StartTime { get; set; }

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x0600495F RID: 18783 RVA: 0x000915CA File Offset: 0x0008F7CA
		// (set) Token: 0x06004960 RID: 18784 RVA: 0x000915D2 File Offset: 0x0008F7D2
		public DateTimeOffset? EndTime { get; set; }
	}
}
